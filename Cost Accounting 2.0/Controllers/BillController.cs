using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Cost_billing_2._0.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager; 

        public BillController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Bills.Include(a => a.User).Include(b => b.TypeBill);
            var billList = !User.IsInRole(Role.Admin.ToString()) ? 
                applicationContext.Where(bill => bill.User == _userManager.FindByNameAsync(User.Identity.Name).Result)
                : applicationContext;

            return View(await billList.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(a => a.User)
                .Include(a => a.TypeBill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TypeBill"] = new SelectList(_context.TypeBills, "Id", "Type");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, UserId, StartAmount, TypeBillId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole(Role.Admin.ToString()))
                {
                    bill.UserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                }
                _context.Add(bill);
                await _context.SaveChangesAsync();
                var typeParam = new SqlParameter("@TypeObject", "Bill");
                var idParam = new SqlParameter("@ObjectId", _context.Bills.ToList().LastOrDefault().Id);
                var userId = new SqlParameter("@UserId", _userManager.FindByNameAsync(User.Identity.Name).Result.Id);
                _context.Database.ExecuteSqlRaw("ActionInsert @TypeObject, @ObjectId, @UserId", typeParam, idParam, userId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
            ViewData["TypeBill"] = new SelectList(_context.TypeBills, "Id", "Type", bill.TypeBillId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = _context.Bills.Include(a => a.TypeBill).Where(a => a.Id == id).FirstOrDefault();
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
            ViewData["TypeBill"] = new SelectList(_context.TypeBills, "Id", "Type", bill.TypeBillId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartAmount,TypeBillId")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Bill bill1 = _context.Bills.ToList().Where(b => b.Id == id).FirstOrDefault();
                    bill1.Name = bill.Name;
                    bill1.UserId = bill.UserId;
                    bill1.TypeBillId = bill.TypeBillId;
                    bill1.StartAmount = bill.StartAmount;
                    if (bill.UserId == null)
                        bill1.UserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                    
                    _context.Update(bill1);
                    await _context.SaveChangesAsync();
                    var typeParam = new SqlParameter("@TypeObject", "Bill");
                    var idParam = new SqlParameter("@ObjectId", _context.Bills.ToList().Where(b => b.Name == bill.Name).FirstOrDefault().Id);
                    var userId = new SqlParameter("@UserId", _userManager.FindByNameAsync(User.Identity.Name).Result.Id);
                    _context.Database.ExecuteSqlRaw("ActionUpdate @TypeObject, @ObjectId, @UserId", typeParam, idParam, userId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!billExists(bill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TypeBill"] = new SelectList(_context.TypeBills, "Id", "Type", bill.TypeBillId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(a => a.User)
                .Include(a => a.TypeBill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var bill = await _context.Bills.FindAsync(id);
            var typeParam = new SqlParameter("@TypeObject", "Bill");
            var idParam = new SqlParameter("@ObjectId", _context.Bills.ToList().Where(b => b.Id == id).FirstOrDefault().Id);
            var userId = new SqlParameter("@UserId", _userManager.FindByNameAsync(User.Identity.Name).Result.Id);
            _context.Database.ExecuteSqlRaw("ActionDelete @TypeObject, @ObjectId, @UserId", typeParam, idParam, userId);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool billExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }

        [AcceptVerbs("Post")]
        public IActionResult GetAmount(int id)
        {
            decimal startAmount = _context.Bills.Where(b => b.Id == id).FirstOrDefault().StartAmount;
            decimal amountCredit =  _context.Transactions.Where(t => t.CreditBillId == id).Sum(t => t.Amount);
            decimal amountDebit = _context.Transactions.Where(t => t.DebitBillId == id).Sum(t => t.Amount);
            decimal result = startAmount - amountCredit + amountDebit;
            return PartialView(result);
        }
    }
}

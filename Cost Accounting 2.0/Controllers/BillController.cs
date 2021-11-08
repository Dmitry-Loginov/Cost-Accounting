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
            var applicationContext = _context.Bills.Include(a => a.User);
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
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, UserId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole(Role.Admin.ToString()))
                {
                    bill.UserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                }
                if (_context.Bills.ToList().Where(b => b.Name == bill.Name && b.UserId == _userManager.FindByNameAsync(User.Identity.Name).Result.Id).Count() != 0)
                {
                    ModelState.AddModelError("Name", "This bill is exist");
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
                    return View(bill);
                }
                _context.Add(bill);
                await _context.SaveChangesAsync();
                var typeParam = new SqlParameter("@TypeObject", "Bill");
                var idParam = new SqlParameter("@ObjectId", _context.Bills.ToList().Where(b => b.Id == bill.Id).FirstOrDefault().Id);
                var userId = new SqlParameter("@UserId", _userManager.FindByNameAsync(User.Identity.Name).Result.Id);
                _context.Database.ExecuteSqlRaw("ActionInsert @TypeObject, @ObjectId, @UserId", typeParam, idParam, userId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Bills.ToList().Where(b => b.Name == bill.Name && b.UserId == _userManager.FindByNameAsync(User.Identity.Name).Result.Id).Count() != 0)
                {
                    ModelState.AddModelError("Name", "This bill is exist");
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
                    return View(bill);
                }
                try
                {
                    _context.Update(bill);
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", bill.UserId);
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
    }
}

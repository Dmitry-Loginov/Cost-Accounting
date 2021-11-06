using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cost_Accounting_2._0.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Cost_Accounting_2._0.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ApplicationContext _context;
        UserManager<User> UserManager { get; set; }

        public TransactionController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.Include(t => t.CreditBill).ThenInclude(cr => cr.User)
                .Include(t => t.DebitBill).ThenInclude(dt => dt.User).ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            var billList = (from bill in _context.Bills.
                               Where(bill => bill.User == UserManager.FindByNameAsync(User.Identity.Name).Result)
                                select new SelectListItem()
                                {
                                    Text = bill.Id.ToString() + " " + bill.Name,
                                    Value = bill.Id.ToString(),
                                }).ToList();

            TransactionViewModel transactionViewModel = new TransactionViewModel();
            transactionViewModel.CreditListBills = billList;
            transactionViewModel.DebitListBills = billList;

            return View(transactionViewModel);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(FillTransaction(transactionViewModel));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionViewModel);
        }

        Transaction FillTransaction(TransactionViewModel transactionViewModel, Transaction transaction = null)
        {
            if (transaction == null) transaction = new Transaction();
            transaction.Date = transactionViewModel.Date;
            transaction.Amount = transactionViewModel.Amount;
            transaction.CreditBillId = Convert.ToInt32(transactionViewModel.Credit);
            transaction.DebitBillId = Convert.ToInt32(transactionViewModel.Debit);
            return transaction;
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var accountList = (from account in _context.Bills.
                                Where(bill => bill.User == UserManager.FindByNameAsync(User.Identity.Name).Result)
                               select new SelectListItem()
                               {
                                   Text = account.Id.ToString() + " " + account.Name,
                                   Value = account.Id.ToString(),
                               }).ToList();

            return View(new TransactionViewModel {
                Amount = transaction.Amount,
                CreditListBills = accountList,
                DebitListBills = accountList,
                Credit = transaction.CreditBillId.ToString(),
                Debit = transaction.DebitBillId.ToString(),
                Date = transaction.Date,
                Id = transaction.Id
            });
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionViewModel transactionViewModel)
        {
            if (id != transactionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Transaction transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionViewModel.Id);
                    _context.Update(FillTransaction(transactionViewModel, transaction));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transactionViewModel.Id))
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
            return View(transactionViewModel);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}

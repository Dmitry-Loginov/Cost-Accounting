using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cost_Accounting_2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cost_Accounting_2._0.ViewModels;
using System;

namespace Cost_Accounting_2._0.Controllers
{
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
            return View(await _context.Transactions.Include(t => t.CreditAccount).ThenInclude(cr => cr.User)
                .Include(t => t.DebitAccount).ThenInclude(dt => dt.User).ToListAsync());
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
            var accountList = (from account in _context.Accounts
                                select new SelectListItem()
                                {
                                    Text = account.Id.ToString() + " " + account.Name,
                                    Value = account.Id.ToString(),
                                }).ToList();

            accountList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });

            TransactionViewModel transactionViewModel = new TransactionViewModel();
            transactionViewModel.CreditListAccounts = accountList;
            transactionViewModel.DebitListAccounts = accountList;

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
                Transaction transaction = new Transaction();
                transaction.Date = transactionViewModel.Date;
                transaction.Amount = transactionViewModel.Amount;
                transaction.CreditAccountId = Convert.ToInt32(transactionViewModel.Credit);
                transaction.DebitAccountId = Convert.ToInt32(transactionViewModel.Debit);
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionViewModel);
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
            var accountList = (from account in _context.Accounts
                               select new SelectListItem()
                               {
                                   Text = account.Id.ToString() + " " + account.Name,
                                   Value = account.Id.ToString(),
                               }).ToList();

            accountList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return View(new TransactionViewModel {
                Amount = transaction.Amount,
                CreditListAccounts = accountList,
                DebitListAccounts = accountList,
                Credit = transaction.CreditAccountId.ToString(),
                Debit = transaction.DebitAccountId.ToString(),
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
                    transaction.Date = transactionViewModel.Date;
                    transaction.Amount = transactionViewModel.Amount;
                    transaction.CreditAccountId = Convert.ToInt32(transactionViewModel.Credit);
                    transaction.DebitAccountId = Convert.ToInt32(transactionViewModel.Debit);
                    _context.Update(transaction);
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

using firstmvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace firstmvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Expenses()
        {
            var allExpenses=_context.Expenses.ToList();
            var totalExpenses = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? id )
        {
            if(id!=null)
            {

                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expenseInDb);
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var expenseInDb=_context.Expenses.SingleOrDefault(expense =>expense.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges(); 
            return RedirectToAction("Expense");

        }
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                _context.Expenses.Add(model);
            }
            else
            {
                _context.Expenses.Update(model);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

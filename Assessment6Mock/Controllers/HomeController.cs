using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assessment6Mock.Models;
using Assessment6Mock.Model;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assessment6Mock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDBContext _context;

        public HomeController(ILogger<HomeController> logger, EmployeeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Employees()
        {
            var employees = await _context.Employee.ToListAsync();

            return View(employees);
        }

        public async Task<IActionResult> RetirementInfo(int employeeId)
        {
            var employee = await _context.Employee.FindAsync(employeeId);

            if (employee.Age >= 60)
                ViewBag.CanRetire = true;
            else
                ViewBag.CanRetire = false;

            ViewBag.Benefits = (employee.Salary * 0.6m).ToString("c");

            return View(employee);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

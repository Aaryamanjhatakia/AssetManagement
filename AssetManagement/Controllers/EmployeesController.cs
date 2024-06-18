using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Controllers
{
    public class EmployeesController : Controller
    {
        public AssetProjectContext AssetprojContext { get; }

        public EmployeesController(AssetProjectContext assetprojContext)
        {
            AssetprojContext = assetprojContext;
            
        }



        public async Task<IActionResult> Index(string searchString)
        {
            var employees = await AssetprojContext.Employees.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                employees = employees.Where(n => n.EmployeeName.ToLower().Contains(searchString) || n.EmployeeId.ToLower().Contains(searchString)).ToList();
            }

            return View(employees);
        }






        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await AssetprojContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmp(AddEmployeeViewModel addEmployeeRequest)
        {
            // Check if employee with the same Employee ID already exists
            var existingEmployee = await AssetprojContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == addEmployeeRequest.EmployeeId);

            if (existingEmployee != null)
            {
                // Add a model error and return the view with the current model
                ModelState.AddModelError("EmployeeId", "An employee with the same Employee ID already exists.");
                return View(addEmployeeRequest);
            }


            var employee = new Employee()
            {
                EmployeeId = addEmployeeRequest.EmployeeId,
                EmployeeName = addEmployeeRequest.EmployeeName,
                PhoneNo = addEmployeeRequest.PhoneNo,
                Email = addEmployeeRequest.Email,
                Address = addEmployeeRequest.Address,
                Dob = addEmployeeRequest.Dob,
                Department = addEmployeeRequest.Department,
                Salary = addEmployeeRequest.Salary

            };

            await AssetprojContext.Employees.AddAsync(employee);
            await AssetprojContext.SaveChangesAsync();

            // Add success message to TempData
            TempData["SuccessMessage"] = "Employee added successfully.";

            return RedirectToAction("AddEmp");

        }

        [HttpGet]
        public async Task<IActionResult> View(string EmployeeId)
        {
            var employee = await AssetprojContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    PhoneNo = employee.PhoneNo,
                    Email = employee.Email,
                    Address = employee.Address,
                    Dob = employee.Dob,
                    Department = employee.Department,
                    Salary = employee.Salary
                };
                return await Task.Run(() => View("View", viewModel));
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            // Retrieve the employee from the database
            var employee = await AssetprojContext.Employees.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                // Update employee properties
                employee.EmployeeName = model.EmployeeName;
                employee.PhoneNo = model.PhoneNo;
                employee.Email = model.Email;
                employee.Address = model.Address;
                employee.Dob = model.Dob;
                employee.Department = model.Department;
                employee.Salary = model.Salary;

                // Mark the entity as modified
                AssetprojContext.Entry(employee).State = EntityState.Modified;

                // Save changes to the database
                await AssetprojContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await AssetprojContext.Employees.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                AssetprojContext.Employees.Remove(employee);
                await AssetprojContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }


        
    }
}

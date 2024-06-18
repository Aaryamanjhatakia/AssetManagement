using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

namespace AssetManagement.Controllers
{
    public class EmployeeHavingAssetsController : Controller
    {
        private readonly AssetProjectContext _context;

        public EmployeeHavingAssetsController(AssetProjectContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var output = await _context.EmployeeAssetsViews.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                output = output.Where(n => n.AssetId.ToLower().Contains(searchString) || n.AssetName.ToLower().Contains(searchString) || n.EmployeeName.ToLower().Contains(searchString)
                || n.EmployeeId.ToLower().Contains(searchString)).ToList();
            }

            return View(output);
        }




        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employeeHavingAssets = await _context.EmployeeAssetsViews.ToListAsync();
            return View(employeeHavingAssets);
        }

        [HttpGet]
        public IActionResult Assign(string EmployeeId, string RequestId)
        {
            var viewModel = new AddEmployeeHavingAssetsVM
            {
                EmployeeId = EmployeeId,
                ReqId = RequestId,
                DateOfAssign = DateTime.Now // Set a default date
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(AddEmployeeHavingAssetsVM addEmployeeHavingAssetRequest)
        {
            var employeeHavingAsset = new EmployeeHavingAsset()
            {
                EmployeeId = addEmployeeHavingAssetRequest.EmployeeId,
                AssetId = addEmployeeHavingAssetRequest.AssetId,
                DateOfAssign = addEmployeeHavingAssetRequest.DateOfAssign,
                ReqId = addEmployeeHavingAssetRequest.ReqId
            };

            await _context.EmployeeHavingAssets.AddAsync(employeeHavingAsset);






            await _context.SaveChangesAsync();

            // Add success message to TempData
            TempData["SuccessMessage"] = "Asset assigned successfully and status updated.";


            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult AssignAsset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignAsset(AddEmployeeHavingAssetsVM addEmployeeHavingAssetRequest)
        {
            var employeeHavingAsset = new EmployeeHavingAsset()
            {
                EmployeeId = addEmployeeHavingAssetRequest.EmployeeId,
                AssetId = addEmployeeHavingAssetRequest.AssetId,
                DateOfAssign = addEmployeeHavingAssetRequest.DateOfAssign,
                ReqId = addEmployeeHavingAssetRequest.ReqId
            };

            await _context.EmployeeHavingAssets.AddAsync(employeeHavingAsset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

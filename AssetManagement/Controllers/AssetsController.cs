using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Controllers
{
    public class AssetsController : Controller
    {

        public AssetProjectContext AssetprojContext { get; }

        public AssetsController(AssetProjectContext assetprojContext)
        {
            AssetprojContext = assetprojContext;
        }



        public async Task<IActionResult> Index(string searchString)
        {
            var assets = await AssetprojContext.Assets.ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                assets = assets.Where(n => n.AssetId.ToLower().Contains(searchString) || n.AssetName.ToLower().Contains(searchString)).ToList();
            }

            return View(assets);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assets = await AssetprojContext.Assets.ToListAsync();
            return View(assets);
        }

        [HttpGet]
        public IActionResult AddAsset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(AddAssetViewModel addAssetRequest)
        {
            // Check if asset with the same Asset ID already exists
            var existingAsset = await AssetprojContext.Assets
                .FirstOrDefaultAsync(a => a.AssetId == addAssetRequest.AssetId);

            if (existingAsset != null)
            {
                // Add a model error and return the view with the current model
                ModelState.AddModelError("AssetId", "An asset with the same Asset ID already exists.");
                return View(addAssetRequest);
            }


            var asset = new Asset()
            {
                AssetId = addAssetRequest.AssetId,
                AssetName = addAssetRequest.AssetName,
                ModelNo = addAssetRequest.ModelNo,
                SerialNo = addAssetRequest.SerialNo,
                MakeCompany = addAssetRequest.MakeCompany,
                Value = addAssetRequest.Value,
                Status = addAssetRequest.Status,
                

            };

            await AssetprojContext.Assets.AddAsync(asset);
            await AssetprojContext.SaveChangesAsync();

            // Add success message to TempData
            TempData["SuccessMessage"] = "Asset added successfully.";

            return RedirectToAction("AddAsset");

        }

        [HttpGet]
        public async Task<IActionResult> View(string AssetId)
        {
            var asset = await AssetprojContext.Assets.FirstOrDefaultAsync(x => x.AssetId == AssetId);

            if (asset != null)
            {
                var viewModel = new UpdateAssetViewModel()
                {
                    AssetId = asset.AssetId,
                    AssetName = asset.AssetName,
                    ModelNo = asset.ModelNo,
                    SerialNo = asset.SerialNo,
                    MakeCompany = asset.MakeCompany,
                    Value = asset.Value,
                    Status = asset.Status,
                    
                };
                return await Task.Run(() => View("View", viewModel));
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateAssetViewModel model)
        {
            // Retrieve the employee from the database
            var asset = await AssetprojContext.Assets.FindAsync(model.AssetId);

            if (asset != null)
            {
                // Update asset properties
                asset.AssetId = model.AssetId;
                asset.AssetName = model.AssetName;
                asset.ModelNo = model.ModelNo;
                asset.SerialNo = model.SerialNo;
                asset.MakeCompany = model.MakeCompany;
                asset.Value = model.Value;
                

                // Mark the entity as modified
                AssetprojContext.Entry(asset).State = EntityState.Modified;

                // Save changes to the database
                await AssetprojContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateAssetViewModel model)
        {
            var asset = await AssetprojContext.Employees.FindAsync(model.AssetId);

            if (asset != null)
            {
                AssetprojContext.Employees.Remove(asset);
                await AssetprojContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}

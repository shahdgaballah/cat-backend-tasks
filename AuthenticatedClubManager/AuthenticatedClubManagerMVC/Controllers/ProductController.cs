using Microsoft.AspNetCore.Mvc;
using AuthenticatedClubManagerMVC.Data;
using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.Services.Interfaces;

namespace AuthenticatedClubManagerMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IFileService _fileService;
        public ProductController(ApplicationDbContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _db.products;
            return View(ProductList);
        }
        //create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.File == null || product.File.Length == 0)
                {
                    ModelState.AddModelError("File", "Image is required");
                    return View(product);
                }

                var path = _fileService.UploadFile(product.File, "/images/");
                product.ImagePath = path;

                _db.products.Add(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);

        }

        //update

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0) return NotFound();
            var product = _db.products.Find(id);
            if(product==null) return NotFound();

            return View(product);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                //if (product.File == null || product.File.Length == 0)
                //{
                //    ModelState.AddModelError("File", "Image is required");
                //    return View(product);
                //}
                var currentPath = product.ImagePath;
                if (product.File!=null || product.File?.Length > 0)
                {
                    //delete old file
                    _fileService.DeleteFile(currentPath);

                    //upload new image
                    var newPath = _fileService.UploadFile(product.File, "/images/");
                    currentPath=newPath;

                }
                product.ImagePath = currentPath;

                _db.products.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var product = _db.products.Find(id);
            if (product == null) return NotFound();

            return View(product);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var product = _db.products.Find(id);
            if (product == null) return NotFound();

            string path = product.ImagePath;

            _db.products.Remove(product);
            _db.SaveChanges();

            if (!string.IsNullOrEmpty(path))
            {
                _fileService.DeleteFile(path);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        //param name should match the model attribute
        public IActionResult IsProductNameExist(string name)
        {
            var exists = _db.products.Any(p => p.Name==name);
            if (exists)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}

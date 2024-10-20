using AbuOdeh_Electromechanical.Repository.Entities;
using AbuOdeh_Electromechanical.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbuOdeh_Electromechanical.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductSvc _ProductSvc;

        public ProductController(IProductSvc productSvc)
        {
            _ProductSvc = productSvc;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _ProductSvc.GetAll();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [Route("Admin/Product/Update/{objectKey}")]
        public async Task<IActionResult> Update(Guid objectKey)
        {
            var result = await _ProductSvc.GetById(objectKey);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _ProductSvc.Add(product);
            if (result > 0)
            {
                return Redirect("Admin/Categories");
            }
            return View();
        }

        [HttpPost]
        [Route("Product/Update/")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var result = await _ProductSvc.Update(product);
            if (result > 0)
            {
                return Redirect("Admin/Categories");
            }
            return View();
        }

        [HttpDelete]
        [Route("Admin/Product/Delete/{objectKey}")]
        public async Task<IActionResult> Delete(Guid objectKey)
        {
            if (objectKey == Guid.Empty)
            {
                return BadRequest("Invalid object key.");
            }
            var result = await _ProductSvc.Delete(objectKey);

            if (result > 0)
            {
                return Ok(new { message = "Category deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Category not found or failed to delete." });
            }
        }
    }
}

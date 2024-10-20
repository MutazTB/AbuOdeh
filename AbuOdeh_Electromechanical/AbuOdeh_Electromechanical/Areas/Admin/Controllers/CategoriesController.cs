using AbuOdeh_Electromechanical.Repository.Entities;
using AbuOdeh_Electromechanical.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbuOdeh_Electromechanical.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategorySvc _CategorySvc;

        public CategoriesController(ICategorySvc categorySvc)
        {
            _CategorySvc = categorySvc;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _CategorySvc.GetAll();
            return View(result);
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _CategorySvc.GetAll();
            return Json(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [Route("Admin/Categories/Update/{objectKey}")]
        public async Task<IActionResult> Update(Guid objectKey)
        {
            var result = await _CategorySvc.GetById(objectKey);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var result = await _CategorySvc.Add(category);
            if(result > 0 )
            {
                return Redirect("Admin/Categories");
            }
            return View();
        }

        [HttpPost]
        [Route("Categories/Update/")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            var result = await _CategorySvc.Update(category);
            if(result > 0 )
            {
                return Redirect("Admin/Categories");
            }
            return View();
        }

        [HttpDelete]
        [Route("Admin/Categories/Delete/{objectKey}")]
        public async Task<IActionResult> Delete(Guid objectKey)
        {
            if (objectKey == Guid.Empty)
            {
                return BadRequest("Invalid object key.");
            }
            var result = await _CategorySvc.Delete(objectKey);

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

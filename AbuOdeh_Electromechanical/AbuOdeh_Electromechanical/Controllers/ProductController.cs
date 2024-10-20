using AbuOdeh_Electromechanical.Models;
using AbuOdeh_Electromechanical.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbuOdeh_Electromechanical.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductSvc _productSvc;

        public ProductController(IProductSvc productSvc)
        {
            _productSvc = productSvc;
        }

        public async Task<IActionResult> GetAllProducts(ProductPagenation productPagenation)
        {
            var result = await _productSvc.GetAll(productPagenation);
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

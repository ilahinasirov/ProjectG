using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;


namespace WebApi.Controllers
{
	public class ProductController : Controller
	{
		private IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpGet]
		public IActionResult GetList()
		{
			var result = _productService.GetList();
			if (ModelState.IsValid)
			{
				var productList = result; // Assuming result.Data is a List<Product>
				return View(productList);

			}

			return BadRequest(result);
		}


		[HttpGet]
		public IActionResult GetListByCategory(int categoryId)
		{
			var result = _productService.GetListByCategory(categoryId);
			if (ModelState.IsValid)
			{
				return View(result);
			}

			return BadRequest();
		}


		[HttpGet]
		public IActionResult GetById(int productId)
		{
			var result = _productService.GetById(productId);
			if (ModelState.IsValid)
			{
				return View(result);

			}

			return BadRequest();
		}



		[HttpPost]
		public IActionResult Add(Product product)
		{
			 _productService.Add(product);

			 return View(product);

		}


		[HttpPost]
		public IActionResult Update(Product product)
		{
			_productService.Update(product);
			
				return View();
			

		}



		[HttpPost]
		public IActionResult Delete(Product product)
		{
			_productService.Delete(product);


			return View();

		}
	}
}	
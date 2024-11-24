using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P50_4_22.Models;

namespace P50_4_22.Controllers
{
	public class UserController : Controller
	{

		public PetStoreRpmContext db;

		public UserController(PetStoreRpmContext context)
		{
			db = context;
		}

		public async Task<IActionResult> Catalog()
		{
			return View(await db.CatalogProducts.ToListAsync());

		}
		public IActionResult Bucket()
		{
			return View();
		}
		public IActionResult Map()
		{
			return View();
		}

	}
}

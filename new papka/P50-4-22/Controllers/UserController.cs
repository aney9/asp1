using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P50_4_22.Models;
using System.Drawing.Drawing2D;

namespace P50_4_22.Controllers
{
	public class UserController : Controller
	{

		public PetStoreRpmContext db;

		public UserController(PetStoreRpmContext context)
		{
			db = context;
		}
        public async Task<IActionResult> Catalog(string[] brand)
        {
            IQueryable<CatalogProduct> products = db.CatalogProducts.Include(p => p.Brands);

            if (brand?.Any() == true) 
            {
                products = products.Where(p => brand.Contains(p.Brands.Brand1));
            }

            return View(await products.ToListAsync());
        }

        public IActionResult Map()
		{
			return View();
		}

        public async Task<IActionResult> AddToCart(int catalogId, int quantity)
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return RedirectToAction("Profile", "Profile");
            }

            var catalog = await db.CatalogProducts.FindAsync(catalogId);
            if (catalog == null)
            {
                return NotFound("Такого товара нет");
            }

			if (catalog.Quantity < quantity)
			{
				return BadRequest("Столько нет, кыш");
			}
			var cart= await db.Carts
				.FirstOrDefaultAsync(c => c.CatalogId == catalogId && c.UserId == userId);

            if (cart == null)
            {
				cart = new Cart
				{
					Quantity = quantity,
					Price = catalog.PriceOfProduct * quantity,
					CatalogId = catalogId,
					UserId = userId
					
                };
				db.Carts.Add(cart);
            }
			else
			{
				cart.Quantity += quantity;
				if (cart.Quantity > catalog.Quantity) 
				{
					return BadRequest("Мало товара эген");
				}
				cart.Price = cart.Quantity * catalog.PriceOfProduct;
			}
			await db.SaveChangesAsync();
			return RedirectToAction("Cart");
        }

		public IActionResult Cart()
		{
			var userId = User.Identity?.Name;
			if (userId == null)
			{
				return RedirectToAction("Profile", "Profile");
			}

			var cart = db.Carts
				.Where(c => c.UserId == userId)
				.Include(c => c.Catalog)
				.ToList();

			return View(cart);
		}
		[HttpPost]
		public IActionResult EditQuantityCart(int CatalogId, int quantity)
		{
			var cart = db.Carts.FirstOrDefault(c => c.CatalogId == CatalogId);
            var catalog = db.CatalogProducts.Find(CatalogId);
            if (cart != null)
			{
				cart.Quantity = quantity;
				cart.Price = quantity * catalog.PriceOfProduct;
                db.SaveChanges();
			}
			return RedirectToAction("Cart");
		}

		

    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P50_4_22.Models;

namespace P50_4_22.Controllers
{
	public class CRUDController : Controller
	{

		public PetStoreRpmContext db;

		public CRUDController(PetStoreRpmContext context)
		{
			db = context;
		}

		public async Task<IActionResult> AddProduct()
		{
			return View(await db.CatalogProducts.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CatalogProduct cp)
		{
			db.CatalogProducts.Add(cp);
			await db.SaveChangesAsync();
			return RedirectToAction("AddProduct");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id != null)
			{
				CatalogProduct cp = await db.CatalogProducts.FirstOrDefaultAsync(p => p.IdCatalogproducts == id);
				if (cp != null)
				{
					return View(cp);
				}
			}
			return NotFound();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id != null)
			{
				CatalogProduct cp = await db.CatalogProducts.FirstOrDefaultAsync(p => p.IdCatalogproducts == id);
				if (cp != null)
				{
					return View(cp);
				}
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CatalogProduct cp)
		{
			db.CatalogProducts.Update(cp);
			await db.SaveChangesAsync();
			return RedirectToAction("AddProduct");
		}

		[HttpGet]
		[ActionName("Delete")]
		public async Task<IActionResult> ConfirmDelete(int? id)
		{
			if (id != null)
			{
				CatalogProduct cp = await db.CatalogProducts.FirstOrDefaultAsync(p => p.IdCatalogproducts == id);
				if (cp != null)
				{
					return View(cp);
				}
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				CatalogProduct cp = await db.CatalogProducts.FirstOrDefaultAsync(p => p.IdCatalogproducts == id);
				if (cp != null)
				{
					db.CatalogProducts.Remove(cp);
					await db.SaveChangesAsync();
					return RedirectToAction("AddProduct");
				}
			}
			return NotFound();
		}
	}
}

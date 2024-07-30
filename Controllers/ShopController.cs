using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Addaliil_MVC.Data;
using Addaliil_MVC.Models;
using Addaliil_MVC.Attributes;

namespace Addaliil_MVC.Controllers
{
    //[Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly AdaliiDbContext _context;

        public ShopController(AdaliiDbContext context)
        {
            _context = context;
        }

        //// GET: Shops
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Shops.ToListAsync());
        //}

        // GET: Shops/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var shop = await _context.Shops
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (shop == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(shop);
        //}
        //[HttpGet("{shopName?}")]
        public async Task<IActionResult> Details(string shopName)
        {
            // Your logic to handle the request
            if (string.IsNullOrEmpty(shopName))
            {
                // Handle case when productName is not provided
                return RedirectToRoute("default"); // Your default view
            }
            else
            {
                // Handle case when productName is provided
                var shop = await _context.Shops.Where(s => s.Name.ToLower() == shopName.ToLower()).FirstOrDefaultAsync();
                if (shop == null)
                {
                    return RedirectToRoute("default"); // Your default view
                }
                return View("Details", shop); // Your view for the product
            }
        }

        // GET: Shops/Create
        [HttpGet("Shop/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DisplayName,Description")] Shop shop)
        {
            
            if (await _context.Shops.SingleOrDefaultAsync(x=> x.Name .Contains(shop.Name) ) == null)
            {
                ModelState.AddModelError("name", "this name reserved"); // adding custom error inside the post method,
                                                                        // if we didn't specify the property name "name" it will not display
                                                                        // error below the field but it will display in summury
            }
            if (ModelState.IsValid)
            {
                _context.Add(shop);
                await _context.SaveChangesAsync();
                TempData["success"] = "Created successfully";
                return RedirectToRoute("shop", new { shopName = shop.Name });
            }
            //in create view if we want to display only errors that's not related to Model properties we change asp-validation-summary="ModelOnly" instead of "All"
            return View();
        }

        // GET: Shops/Edit/5
        [HttpGet("{shopName?}/edit")]
        public async Task<IActionResult> Edit(string? shopName)
        {
            if (string.IsNullOrEmpty(shopName))
            {
                return NotFound();
            }

            var shop = await _context.Shops.Where(s => s.Name.ToLower() == shopName.ToLower()).FirstAsync();
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{shopName?}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string shopName, [Bind("Id,Name,DisplayName,Description")] Shop shop)
        {
            if (string.IsNullOrEmpty(shop.Name))
            {
                return NotFound();
            }

            //var shopNew = await _context.Shops.AsNoTracking().Where(s => s.Name.ToLower().Equals(shop.Name.ToLower())).FirstOrDefaultAsync();
            var shopNew = await _context.Shops.Where(s => s.Name.ToLower().Equals(shop.Name.ToLower())).FirstOrDefaultAsync();
            if (shopNew != null)
            {
                shopNew.DisplayName = shop.DisplayName;
                shopNew.Description = shop.Description;
                try
                {
                    if (ModelState.IsValid)
                    {
                        _context.Update(shopNew);
                        await _context.SaveChangesAsync();
                        TempData["success"] = "Updated successfully";

                    }
                    else { return View(shopNew); }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopExists(shopNew.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var url = Url.Action("Details", new { shopName = shop.Name });
                return Redirect(url);
            }
            return View();
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop != null)
            {
                _context.Shops.Remove(shop);
            }
            TempData["success"] = "Deteled successfully";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(long id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }
    }
}

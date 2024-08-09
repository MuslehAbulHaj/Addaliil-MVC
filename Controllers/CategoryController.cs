using Microsoft.AspNetCore.Mvc;
using Addaliil.DataAccess.Interfaces;
using Addaliil.Models;

namespace Addaliil.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View( _category.FindAll());
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _category.GetFirstOrDefault(x=>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                _category.Add(category);
                _category.Save();
                newCategory = _category.GetFirstOrDefault(x=>x.Name == category.Name);
                return RedirectToAction(nameof(Details), new { id = newCategory.Id });
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _category.GetFirstOrDefault(x=>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _category.Update(category);
                    _category.Save();
                }
                catch (Exception ex)
                {
                    if (CategoryNotExists(category.Id))
                    {
                        return NotFound(ex);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _category.GetFirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _category.GetFirstOrDefault(m => m.Id == id);
            if (category != null)
            {
                _category.Remove(category);
            }

            _category.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryNotExists(int id)
        {
            //return true if Not existed
            return (_category.GetFirstOrDefault(e => e.Id == id) == null);
        }
    }
}

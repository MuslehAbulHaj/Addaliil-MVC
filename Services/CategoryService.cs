using Addaliil_MVC.Data;
using Addaliil_MVC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Addaliil_MVC.Services
{
    public class CategoryService : ICategory
    {
        private readonly AdaliiDbContext _context;

        public CategoryService(AdaliiDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null) { return null; }
            else { return category; }
        }

        public Category GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            _context.Categories.Remove(await GetCategoryById(id));
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
        }

        public Task<Category> EditCategory(int id)
        {
            throw new NotImplementedException();
        }

    }
}

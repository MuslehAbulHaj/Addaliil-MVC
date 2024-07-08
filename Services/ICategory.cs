using Addaliil_MVC.Models;

namespace Addaliil_MVC.Services
{
    public interface ICategory
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategoryById(int id);
        Category GetCategoryByName(string id);
        Task<Category> AddCategory(Category category);
        Task<Category> EditCategory(int id);
        Task<List<Category>> DeleteCategory(int id);




    }
}

using BO;
using DAO;
namespace Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        public void CreateCategory(Category category)
        {
            CategoryDAO.Instance.CreateCategory(category);
        }

        public void DeleteCategory(short id)
        {
            //use DAO to delete category
            CategoryDAO.Instance.DeleteCategory(id);
        }

        public List<Category> GetCategories(bool status)
        {
            return CategoryDAO.Instance.GetAllCategories(status);
        }

        public Category GetCategory(short id)
           => CategoryDAO.Instance.FindCategoryById(id);


        public NewsArticle GetNewsArticleByCategoryId(short id)
        => CategoryDAO.Instance.GetNewsArticleByCategoryId(id);

        public void UpdateCategory(Category category)
        {
            //use DAO to update category
            CategoryDAO.Instance.UpdateCategory(category);
        }
    }
}

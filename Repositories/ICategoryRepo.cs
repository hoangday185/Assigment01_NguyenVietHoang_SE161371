using BO;
namespace Repositories
{
    public interface ICategoryRepo
    {
        //interface for list of categories
        public List<Category> GetCategories();

        public Category GetCategory(short id);

        public void CreateCategory(Category category);

        public void UpdateCategory(Category category);

        public void DeleteCategory(short id);

        public NewsArticle GetNewsArticleByCategoryId(short id);

    }
}

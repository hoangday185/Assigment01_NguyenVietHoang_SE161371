using BO;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext() => new FunewsManagementContext();

        // Get all categories
        public List<Category> GetAllCategories()
        {
            using var context = CreateDbContext();
            return context.Categories.ToList();
        }

        // Find category by ID
        public Category FindCategoryById(short categoryId)
        {
            using var context = CreateDbContext();
            {
                return context.Categories.AsNoTracking().FirstOrDefault(m => m.CategoryId == categoryId);
            }

        }

        // Create new category
        public void CreateCategory(Category category)
        {
            using var context = CreateDbContext();
            //find category by id use method FindCategoryById
            var existingCategory = FindCategoryById(category.CategoryId);
            // Check if the category exists
            if (existingCategory == null)
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        // Update category
        public void UpdateCategory(Category category)
        {
            using var context = CreateDbContext();
            //use method FindCategoryById to find category by id

            var existingCategory = this.FindCategoryById(category.CategoryId);
            if (existingCategory != null)
            {
                context.Categories.Update(category);
                context.SaveChanges();
            }
        }

        // Delete category
        public void DeleteCategory(short categoryId)
        {
            using var context = CreateDbContext();
            //use method FindCategoryById to find category by id
            var category = this.FindCategoryById(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public NewsArticle? GetNewsArticleByCategoryId(short id)
        {
            using (var context = CreateDbContext())
            {
                return context.NewsArticles.AsNoTracking().FirstOrDefault(m => m.CategoryId == id);
            }
        }
    }
}
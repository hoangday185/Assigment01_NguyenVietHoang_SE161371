using BO;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class NewArticleDAO
    {
        private static NewArticleDAO instance;

        private NewArticleDAO() { }

        public static NewArticleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NewArticleDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext() => new FunewsManagementContext();

        // Create new article
        public void CreateArticle(NewsArticle article)
        {
            using var context = CreateDbContext();
            //kiểm tra xem article có tồn tại không
            if (context.NewsArticles.Find(article.NewsArticleId) == null)
            {
                context.NewsArticles.Add(article);
                context.SaveChanges();

            }
        }

        // Read all articles
        public List<NewsArticle> GetArticles()
        {
            using var context = CreateDbContext();
            return context.NewsArticles.AsNoTracking().Include(n => n.Category).Include(n => n.CreatedBy).ToList();
        }

        // Find article by ID
        public NewsArticle FindArticleById(string articleId)
        {
            using var context = CreateDbContext();
            return context.NewsArticles.AsNoTracking().SingleOrDefault(a => a.NewsArticleId.Equals(articleId));
        }

        // Update article
        public void UpdateArticle(NewsArticle article)
        {
            using var context = CreateDbContext();
            var existingArticle = context.NewsArticles.AsNoTracking().FirstOrDefault(m => m.NewsArticleId == article.NewsArticleId);
            if (existingArticle != null)
            {
                context.NewsArticles.Update(article);
                context.SaveChanges();
            }
        }

        // Delete article
        public void DeleteArticle(string articleId)
        {
            using var context = CreateDbContext();
            var article = FindArticleById(articleId);
            if (article != null)
            {
                context.NewsArticles.Remove(article);
                context.SaveChanges();
            }
        }


    }
}


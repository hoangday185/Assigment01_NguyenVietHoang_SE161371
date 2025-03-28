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
            using (var context = CreateDbContext())
            {
                return context.NewsArticles
                    .AsNoTracking()
                    .Include(n => n.Category)
                    .Include(n => n.CreatedBy)
                    .Where(n => n.NewsStatus == true).ToList();
            }

        }

        // Find article by ID
        public NewsArticle FindArticleById(string articleId)
        {
            using var context = CreateDbContext();
            return context.NewsArticles.AsNoTracking().Include(a => a.Tags).SingleOrDefault(a => a.NewsArticleId.Equals(articleId));
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

        public List<Tag> GetTagsWithArticle(string id, List<int> ahihi)
        {
            //use createDbContext 
            using (var context = CreateDbContext())
            {
                return context.Tags.Where(t => ahihi.Contains(t.TagId)).ToList();
            }

        }

        public List<Tag>? GetArticleTags(string articleId)
        {
            using (var db = CreateDbContext())
            {
                var article = db.NewsArticles.Include(a => a.Tags)
                    .FirstOrDefault(a => a.NewsArticleId == articleId);

                return article?.Tags.ToList();
            }
        }

        public void AddTagsToArticle(string articleId, List<int> tagIds)
        {
            using (var db = CreateDbContext())
            {
                var article = db.NewsArticles.Include(a => a.Tags)
                                             .FirstOrDefault(a => a.NewsArticleId == articleId);
                if (article == null)
                {
                    Console.WriteLine($"Article {articleId} not found!");
                    return;
                }

                Console.WriteLine($"NewsArticleId: {article.NewsArticleId}");

                // Lấy danh sách Tags cần thêm vào bài báo
                var newTags = db.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();

                Console.WriteLine($"Adding tags: {string.Join(", ", newTags.Select(t => t.TagId))} to article {article.NewsArticleId}");

                // Thêm tag nếu chưa có
                foreach (var tag in newTags)
                {
                    if (!article.Tags.Any(t => t.TagId == tag.TagId))
                    {
                        article.Tags.Add(tag);
                        Console.WriteLine($"Added TagId {tag.TagId} to article {article.NewsArticleId}");
                    }
                }

                db.SaveChanges();
                Console.WriteLine($"Article {article.NewsArticleId} now has {article.Tags.Count} tags.");
            }
        }

        public void UpdateArticleTags(string articleId, List<int> newTagIds)
        {
            using (var db = CreateDbContext())
            {
                var article = db.NewsArticles.Include(a => a.Tags)
                                             .FirstOrDefault(a => a.NewsArticleId == articleId);
                if (article == null)
                {
                    Console.WriteLine($"Article {articleId} not found!");
                    return;
                }

                // Lấy danh sách Tag hiện có
                var existingTags = article.Tags.ToList();
                var existingTagIds = existingTags.Select(t => t.TagId).ToList();

                // Xác định các tag cần xóa (có trong DB nhưng không có trong newTagIds)
                var tagsToRemove = existingTags.Where(t => !newTagIds.Contains(t.TagId)).ToList();

                // Xác định các tag cần thêm (có trong newTagIds nhưng chưa có trong DB)
                var tagsToAdd = db.Tags.Where(t => newTagIds.Contains(t.TagId) && !existingTagIds.Contains(t.TagId)).ToList();

                Console.WriteLine($"Removing tags: {string.Join(", ", tagsToRemove.Select(t => t.TagId))}");
                Console.WriteLine($"Adding tags: {string.Join(", ", tagsToAdd.Select(t => t.TagId))}");

                // Xóa tag không còn trong danh sách
                foreach (var tag in tagsToRemove)
                {
                    article.Tags.Remove(tag);
                }

                // Thêm tag mới vào
                foreach (var tag in tagsToAdd)
                {
                    article.Tags.Add(tag);
                }

                db.SaveChanges();
                Console.WriteLine($"Updated Tags for article {article.NewsArticleId}. New total: {article.Tags.Count}");
            }
        }

    }

}

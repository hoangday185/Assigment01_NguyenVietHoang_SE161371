using BO;

namespace Repositories
{
    public interface INewArticleRepo
    {
        //do 5 methods crud for new article
        public void CreateArticle(NewsArticle article);

        public List<NewsArticle> GetArticles();

        public NewsArticle FindArticleById(string articleId);

        public void UpdateArticle(NewsArticle article);

        public void DeleteArticle(string articleId);

    }
}

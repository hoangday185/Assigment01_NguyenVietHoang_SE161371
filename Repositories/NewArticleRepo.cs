using BO;
using DAO;

namespace Repositories
{
    public class NewArticleRepo : INewArticleRepo
    {
        public void CreateArticle(NewsArticle article)
        {
            //gọi đến NewArticleDAO để thực hiện
            NewArticleDAO.Instance.CreateArticle(article);
        }

        public void DeleteArticle(string articleId)
        {
            //gọi đến NewArticleDAO để thực hiện
            NewArticleDAO.Instance.DeleteArticle(articleId);
        }

        public NewsArticle FindArticleById(string articleId)
        {
            //gọi đến NewArticleDAO để thực hiện
            return NewArticleDAO.Instance.FindArticleById(articleId);
        }

        public List<NewsArticle> GetArticles()
        {
            //gọi đến NewArticleDAO để thực hiện
            return NewArticleDAO.Instance.GetArticles();
        }

        public void UpdateArticle(NewsArticle article)
        {
            //gọi đến NewArticleDAO để thực hiện
            NewArticleDAO.Instance.UpdateArticle(article);
        }
    }
}

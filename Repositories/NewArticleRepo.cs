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

        public List<Tag> HandleTag(string id, List<int> ahihi)
        {
            //gọi đến NewArticleDAO để thực hiện
            return NewArticleDAO.Instance.GetTagsWithArticle(id, ahihi);
        }

        public List<Tag> GetArticleTags(string articleId)
        {
            //gọi đến NewArticleDAO để thực hiện
            return NewArticleDAO.Instance.GetArticleTags(articleId);
        }

        public void AddTagsToArticle(string articleId, List<int> tagIds)
        {
            //gọi đến NewArticleDAO để thực hiện
            NewArticleDAO.Instance.AddTagsToArticle(articleId, tagIds);
        }

        public void UpdateArticleTags(string articleId, List<int> newTagIds)
        {
            //gọi đến DAO để thực hiện 
            NewArticleDAO.Instance.UpdateArticleTags(articleId, newTagIds);
        }
    }
}

using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Page.Pages.Article
{
    public class IndexModel : PageModel
    {
        private readonly INewArticleRepo _newsArticleRepo;

        public IndexModel(INewArticleRepo newArticleRepo)
        {
            _newsArticleRepo = newArticleRepo;
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = _newsArticleRepo.GetArticles();
        }
    }
}

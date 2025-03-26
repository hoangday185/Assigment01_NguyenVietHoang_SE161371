using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.Article
{
    public class DetailsModel : PageModel
    {
        private readonly INewArticleRepo _newArticleRepo;

        public DetailsModel(
            //add attribute
            INewArticleRepo newArticleRepo
            )
        {
            _newArticleRepo = newArticleRepo;

        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = _newArticleRepo.FindArticleById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }
    }
}

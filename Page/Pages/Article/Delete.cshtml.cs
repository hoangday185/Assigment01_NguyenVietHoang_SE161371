using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Page.Pages.Article
{
    public class DeleteModel : PageModel
    {
        //create property INewsArticleRepo
        private readonly INewArticleRepo _newArticleRepo;

        public DeleteModel(
            //add attribute
            INewArticleRepo newArticleRepo)

        {
            //init attribute
            _newArticleRepo = newArticleRepo;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get newsarticle by id
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = _newArticleRepo.FindArticleById(id);
            if (newsarticle != null)
            {
                NewsArticle = newsarticle;
                _newArticleRepo.DeleteArticle(NewsArticle.NewsArticleId);
            }

            return RedirectToPage("./Index");
        }
    }
}

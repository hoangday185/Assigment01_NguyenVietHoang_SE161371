using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
namespace Page.Pages.Article
{
    public class CreateModel : PageModel
    {
        //tạo 2 attribute từ ICategoryRepo va IAccountRepo
        private readonly ICategoryRepo _categoryRepo;
        private readonly IAccountRepo _accountRepo;
        //khởi tạo INewArticleRepo
        private readonly INewArticleRepo _newArticleRepo;

        public CreateModel(
            ICategoryRepo categoryRepo,
            IAccountRepo accountRepo,
            INewArticleRepo newArticleRepo)
        {
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
            _newArticleRepo = newArticleRepo;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
                ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");
                return Page();
            }

            _newArticleRepo.CreateArticle(NewsArticle);

            return RedirectToPage("./Index");
        }
    }
}

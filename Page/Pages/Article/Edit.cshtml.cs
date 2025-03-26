using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;
namespace Page.Pages.Article
{
    public class EditModel : PageModel
    {
        //khởi tạo 3 attribute từ ICategoryRepo va IAccountRepo va INewArticleRepo
        private readonly ICategoryRepo _categoryRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly INewArticleRepo _newArticleRepo;


        public EditModel(

            ICategoryRepo categoryRepo,
            IAccountRepo accountRepo,
            INewArticleRepo newArticleRepo)
        {
            //khởi tạo 3 attribute
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
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


            var newsarticle = _newArticleRepo.FindArticleById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsarticle;
            ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //print to terminal modelstate

            var errors = ModelState.Values
                          .SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();

            Console.WriteLine("ModelState Errors: " + string.Join("; ", errors));



            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
                ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");

                return Page();
            }

            //_context.Attach(NewsArticle).State = EntityState.Modified;

            try
            {
                _newArticleRepo.UpdateArticle(NewsArticle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsArticleExists(NewsArticle.NewsArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewsArticleExists(string id)
        => _newArticleRepo.FindArticleById(id) == null ? false : true;
    }
}

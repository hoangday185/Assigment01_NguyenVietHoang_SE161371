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
        private readonly ITagRepo _tagRepo;


        public EditModel(

            ICategoryRepo categoryRepo,
            IAccountRepo accountRepo,
            INewArticleRepo newArticleRepo
            //add tagRepo
            , ITagRepo tagRepo
            )
        {
            //khởi tạo 3 attribute
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
            _newArticleRepo = newArticleRepo;
            //thêm tagRepo
            _tagRepo = tagRepo;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> Tags { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedTags { get; set; } = new List<int>();

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
            LoadData();
            NewsArticle = newsarticle;

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
                LoadData();
                return Page();
            }

            //_context.Attach(NewsArticle).State = EntityState.Modified;

            try
            {
                NewsArticle.ModifiedDate = DateTime.Now;
                NewsArticle.UpdatedById = 1;
                _newArticleRepo.UpdateArticle(NewsArticle);
                var existingTags = _newArticleRepo.GetArticleTags(NewsArticle.NewsArticleId);
                var newTags = SelectedTags.Except(existingTags.Select(t => t.TagId)).ToList();

                if (newTags.Any())
                {

                    _newArticleRepo.UpdateArticleTags(NewsArticle.NewsArticleId, newTags);
                }


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

        private void LoadData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");
            Tags = _tagRepo.Tags().Select(t => new SelectListItem
            {
                Value = t.TagId.ToString(),
                Text = t.TagName
            }).ToList();
        }
    }
}

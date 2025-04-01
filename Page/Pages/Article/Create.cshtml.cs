using Assigment01_NguyenVietHoang_SE161371;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
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
        //create _hubcontext 
        private readonly IHubContext<SignalR> _hubContext;
        //create tagRepo
        private readonly ITagRepo _tagRepo;

        public CreateModel(
            ICategoryRepo categoryRepo,
            IAccountRepo accountRepo,
            INewArticleRepo newArticleRepo,
            //add IHubContext
            IHubContext<SignalR> hubContext
            //add tagRepo
            , ITagRepo tagRepo
            )
        {
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
            _newArticleRepo = newArticleRepo;
            //add IHubContext
            _hubContext = hubContext;
            //add tagRepo
            _tagRepo = tagRepo;
        }

        [BindProperty]
        public List<SelectListItem> Tags { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedTags { get; set; } = new List<int>();

        public IActionResult OnGet()
        {

            LoadData();
            return Page();
        }



        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;
            NewsArticle.CreatedById = (short)HttpContext.Session.GetInt32("idUser");
            NewsArticle.UpdatedById = (short)HttpContext.Session.GetInt32("idUser");

            //log error in case of model state is not valid

            Console.WriteLine("Selected Tags: " + string.Join(", ", SelectedTags));

            if (!ModelState.IsValid)
            {
                LoadData();
                return Page();
            }


            _newArticleRepo.CreateArticle(NewsArticle);

            var existingTags = _newArticleRepo.GetArticleTags(NewsArticle.NewsArticleId);
            var newTags = SelectedTags.Except(existingTags.Select(t => t.TagId)).ToList();

            if (newTags.Any())
            {
                _newArticleRepo.AddTagsToArticle(NewsArticle.NewsArticleId, newTags);
            }

            return RedirectToPage("./Index");
        }

        private void LoadData()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepo.GetCategories(true), "CategoryId", "CategoryDesciption");
            ViewData["CreatedById"] = new SelectList(_accountRepo.GetAccounts(), "AccountId", "AccountId");
            Tags = _tagRepo.Tags().Select(t => new SelectListItem
            {
                Value = t.TagId.ToString(),
                Text = t.TagName
            }).ToList();
        }
    }
}

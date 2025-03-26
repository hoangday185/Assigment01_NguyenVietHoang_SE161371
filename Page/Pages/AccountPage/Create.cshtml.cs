using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.AccountPage
{
    public class CreateModel : PageModel
    {
        //create private IAccountRepo
        private readonly IAccountRepo _accountRepo;

        //create constructor with IAccountRepo and remove DAO.FunewsManagementContext context
        public CreateModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [BindProperty]
        public int LenghtOfListAccount { get; set; } = default!;


        public IActionResult OnGet()
        {
            LenghtOfListAccount = _accountRepo.GetAccounts().Count();
            return Page();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;



        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _accountRepo.AddAccount(SystemAccount);

            return RedirectToPage("./Index");
        }
    }
}

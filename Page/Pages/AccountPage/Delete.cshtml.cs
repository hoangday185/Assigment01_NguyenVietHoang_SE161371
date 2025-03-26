using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
namespace Page.Pages.AccountPage
{
    public class DeleteModel : PageModel
    {
        //create private IAccountRepo
        private readonly IAccountRepo _accountRepo;

        //create constructor with IAccountRepo and remove DAO.FunewsManagementContext context
        public DeleteModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }


        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = _accountRepo.FindAccountById((short)id);
            if (systemaccount == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = _accountRepo.FindAccountById(id.Value);
            if (systemaccount != null)
            {
                SystemAccount = systemaccount;
                _accountRepo.DeleteAccount(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}

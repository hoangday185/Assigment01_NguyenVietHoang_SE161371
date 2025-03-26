using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
namespace Page.Pages.AccountPage
{
    public class EditModel : PageModel
    {
        //create private IAccountRepo
        private readonly IAccountRepo _accountRepo;

        //create constructor with IAccountRepo and remove DAO.FunewsManagementContext context
        public EditModel(IAccountRepo accountRepo)
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

            //using method FindAccountById from IAccountRepo
            var systemaccount = _accountRepo.FindAccountById(id.Value);
            if (systemaccount == null)
            {
                return NotFound();
            }
            SystemAccount = systemaccount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(SystemAccount).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                _accountRepo.UpdateAccount(SystemAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemAccountExists(SystemAccount.AccountId))
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

        private bool SystemAccountExists(short id)
        => _accountRepo.FindAccountById(id) != null;
    }
}

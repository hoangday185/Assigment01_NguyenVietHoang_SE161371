using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace Page.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepo _accountRepo;

        public LoginModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //lấy ra biến email và password đi 
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            //kiểm tra xem email và password có đúng không
            var account = _accountRepo.GetAccount(email, password);
            if (account != null)
            {
                //add email và role vào session
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("role", account.AccountRole.ToString());
                return RedirectToPage("/Article/Index");
            }
            else
            {
                //đưa biến lỗi vào tempdata
                TempData["error"] = "Email or password is incorrect";
                return RedirectToPage("Login");
            }
        }
    }
}

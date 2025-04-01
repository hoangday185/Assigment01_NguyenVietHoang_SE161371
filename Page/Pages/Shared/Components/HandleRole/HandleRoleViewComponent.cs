using Microsoft.AspNetCore.Mvc;

namespace Page.Pages.Shared.Components.HandleRole
{
    public class HandleRoleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var role = HttpContext.Session.GetString("role") ?? "Guest";
            var userId = HttpContext.Session.GetInt32("idUser") ?? 0;// Lấy ID từ session

            var model = new HandleRole
            {
                Role = role,
                UserId = userId
            };
            return View("Default", model);
        }
    }
    public class HandleRole
    {
        public string Role { get; set; }
        public int UserId { get; set; }
    }
}
//public class HandleRoleViewComponent : ViewComponent
//{
//    public async Task<IViewComponentResult> InvokeAsync()
//    {
//        var role = HttpContext.Session.GetString("role") ?? "Guest";
//        var userId = HttpContext.Session.GetInt32("idUser") ?? 0;// Lấy ID từ session

//        var model = new HandleRole
//        {
//            Role = role,
//            UserId = userId
//        };

//        return View("Default", model);
//    }
//}

//public class HandleRole
//{
//    public string Role { get; set; }
//    public int UserId { get; set; }
//}
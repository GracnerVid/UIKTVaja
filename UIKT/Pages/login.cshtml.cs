using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UIKT.Pages
{
    public class loginModel : PageModel
    {
        public void OnGet()
        {
        }

        //protected void loginBtn_Click(object sender, EventArgs e)
        //{
        //    if (true)
        //    {
        //        return RedirectToPage("");
        //    }
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            var username = Request.Form["fname"];
            var password = Request.Form["fpass"];

            if (username == "admin" && password == "password")
            {
                //HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToPage("/MainWindow");
            }
            else
            {
                //HttpContext.Session.SetString("IsLoggedIn", "false");
                return Page();
            }
        }
    }
}

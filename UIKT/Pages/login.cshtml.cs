using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace UIKT.Pages
{
    public class loginModel : PageModel
    {
        public string response { get; set; }
        public bool usrData { get; set; }
        public bool privacy { get; set; }
        public bool cookies { get; set; }
        public void OnGet()
        {
            response = "";
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
            var usrDataRaw = Request.Form["usr"];
            var privacyRaw = Request.Form["privacy"];
            var cookiesRaw = Request.Form["cookies"];            

            if (username == "admin" && password == "admin")
            {
                return RedirectToPage("/MainWindowUprava");
            }

            HttpContext.Session.SetString("usr", usrDataRaw.ToString());
            HttpContext.Session.SetString("privacy", privacyRaw.ToString());
            HttpContext.Session.SetString("cookies", cookiesRaw.ToString());

            if (username == "ime" && password == "geslo")
            {
                return RedirectToPage("/MainWindow");
            }
            else
            {
                //HttpContext.Session.SetString("IsLoggedIn", "false");
                response = "Nepravilni podatki!";
                return Page();
            }
        }
    }
}
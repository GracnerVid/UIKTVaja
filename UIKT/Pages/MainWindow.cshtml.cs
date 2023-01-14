using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UIKT.Resources;
using Newtonsoft.Json;
using System.IO;

namespace UIKT.Pages
{
    public class MainWindowModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var ime = Request.Form["fname"];
            var priimek = Request.Form["fpass"];
            var emso = Request.Form["femso"];
            var davcnast = Request.Form["fdst"];
            var iban = Request.Form["fiban"];
            var naziv = Request.Form["fnaziv"];
            var opis = Request.Form["fopis"];

            if (ime == "" || priimek == "" || emso == "" || davcnast == "" ||
                iban == "" || naziv == "" || opis == "")
            {
                //return RedirectToPage("/MainWindow");
                return Page();
            }
            else
            {
                Vloga vloga = new Vloga(ime, priimek, emso, davcnast, iban, naziv, opis);
                string json = JsonConvert.SerializeObject(vloga);
                string fileName = "vloga" + DateTime.Today.ToString("dd-MM-yyyy") + ".json";
                string folderPath = Path.GetFullPath(@"Vloge");
                string filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllText(filePath, json);
                return RedirectToPage("/Index");
            }
        }
    }
}

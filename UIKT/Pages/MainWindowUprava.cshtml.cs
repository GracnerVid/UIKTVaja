using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UIKT.Resources;
using Newtonsoft.Json;
using System.IO;

namespace UIKT.Pages
{
    public class MainWindowModelUprava : PageModel
    {
        public string ime { get; set; }
        public string priimek { get; set; }
        public string emso { get; set; }
        public string dst { get; set; }
        public string iban { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }

        public void OnGet()
        {
            // prikaz podatkov na strani
            string folderPath = Path.GetFullPath(@"Vloge");
            var newestFile = new FileInfo(Directory.GetFiles(folderPath)
                .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                .First());
            string json = System.IO.File.ReadAllText(newestFile.ToString());
            Vloga vloga = JsonConvert.DeserializeObject<Vloga>(json);

            ime = vloga.ime;
            priimek = vloga.priimek;
            emso = vloga.emso;
            dst = vloga.dst;
            iban = vloga.iban;
            naziv = vloga.naziv;
            opis = vloga.opis;

            bool valid = ValidateEmso(emso);
            if (valid)
            {
                emsoT = "Ok";
            }
            else
            {
                emsoT = "Neveljavno";
            }

            imeT = "Ok";
            priimekT = "Ok";
            dstT = "Ok";
            ibanT = "Ok";
            nazivT = "Ok";
            opisT = "Ok";
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

        public bool ValidateEmso(string emso)
        {
            if ((emso == null) || (emso.Length != 13) || (!emso.All(c => char.IsDigit(c))))
                return false;

            int emso_sum = 0;
            for (int i = 7; i > 1; i--)
                emso_sum += i * (int.Parse(emso.Substring(7 - i, 1)) + int.Parse(emso.Substring(13 - i, 1)));

            int control_digit = emso_sum % 11 == 0 ? 0 : 11 - (emso_sum % 11);

            if (emso.Substring(12, 1) == control_digit.ToString())
                return true;

            return false;
        }

        public string imeT { get; set; }
        public string priimekT { get; set; }
        public string emsoT { get; set; }
        public string dstT { get; set; }
        public string ibanT { get; set; }
        public string nazivT { get; set; }
        public string opisT { get; set; }
    }
}

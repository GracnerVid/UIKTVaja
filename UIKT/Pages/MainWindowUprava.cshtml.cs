using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UIKT.Resources;
using Newtonsoft.Json;
using System.IO;
using Xceed.Words.NET;
using System.Reflection.Metadata;
using Microsoft.Office.Interop.Word;
using Microsoft.AspNetCore.Http.Extensions;

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
        public string vlogaime { get; set; }
        public string response { get; set; }

        public void OnGet()
        {
            string url = PageContext.HttpContext.Request.GetDisplayUrl();
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

            vlogaime = "Vloga za: " + ime + " " + priimek;

            bool valid = ValidateEmso(emso);
            if (valid)
            {
                emsoT = "Ok";
            }
            else
            {
                emsoT = "Neveljavno";
            }

            // warcrime
            if (url[url.Length - 1] == 'f')
            {
                response = "Prosimo izpolnite zgornje polje!";
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
            var priimek = Request.Form["fpriimek"];
            var emso = Request.Form["femso"];
            var davcnast = Request.Form["fdst"];
            var iban = Request.Form["fiban"];
            var naziv = Request.Form["fnaziv"];
            var opis = Request.Form["fopis"];
            var sub = Request.Form["submit"];
            var zavrnitev = Request.Form["zavrnitev"];
            var table = Request.Form["myTable"];

            string folderPathv = Path.GetFullPath(@"Vloge");
            var newestFile = new FileInfo(Directory.GetFiles(folderPathv)
                .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                .First());
            string json = System.IO.File.ReadAllText(newestFile.ToString());
            Vloga vloga = JsonConvert.DeserializeObject<Vloga>(json);
            ime = vloga.ime;
            priimek = vloga.priimek;
            emso = vloga.emso;
            davcnast = vloga.dst;
            iban = vloga.iban;
            naziv = vloga.naziv;
            opis = vloga.opis;


            switch (sub)
            {
                case "Potrdi":
                    string folderPathPotrdi = Path.GetFullPath(@"Sporocila_Potrdila");
                    string tempfileNamePotrdi = folderPathPotrdi + "\\Potrdilo" + DateTime.Today.ToString("dd-MM-yyyy") + ime + "_" + priimek + ".docx";

                    var docPotrdi = DocX.Create(tempfileNamePotrdi);
                    docPotrdi.InsertParagraph("");
                    var paragraphPotrdi = docPotrdi.InsertParagraph();

                    string sporociloPotrdi = $"Spostovani {ime} {priimek}, \n" +
                        $"\n" +
                        $"Vase socialno podjetje: \n" +
                        $"{naziv}," +
                        $" je bilo potrjeno in registrirano.\n" +
                        $"LP,\n" +
                        $"Registerski Organ\n";


                    paragraphPotrdi.Append(sporociloPotrdi).Alignment = Xceed.Document.NET.Alignment.center;
                    docPotrdi.SaveAs(tempfileNamePotrdi);
                    break;

                case "Zavrni":
                    if (string.IsNullOrEmpty(zavrnitev))
                    {
                        return RedirectToAction("f");
                    }
                    else
                    {
                        string folderPath = Path.GetFullPath(@"Sporocila_Potrdila");
                        string tempfileName = folderPath + "\\Sporocilo" + DateTime.Today.ToString("dd-MM-yyyy") + ime + "_" + priimek + ".docx";
                        string fileName = folderPath + "\\Sporocilo" + DateTime.Today.ToString("dd-MM-yyyy") + ".pdf";

                        var doc = DocX.Create(tempfileName);
                        doc.InsertParagraph("");
                        var paragraph = doc.InsertParagraph();

                        string sporocilo = $"Spostovani {ime} {priimek}, \n" +
                            $"\n" +
                            $"Vasa vloga za registracijo novega socialnega podjetja, \n" +
                            $" je bila zavrnjena z razlogom: \n" +
                            $"{zavrnitev}.\n" +
                            $"Ce zelite lahko ponovno poskusete preko spletnega portala.\n" +
                            $"LP,\n" +
                            $"Registerski Organ\n";

                        paragraph.Append(sporocilo).Alignment = Xceed.Document.NET.Alignment.center;
                        doc.SaveAs(tempfileName);
                    }
                    break;

                default:
                    break;
            }

            response = "Ni še nobene nove vloge.";
            return Page();
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

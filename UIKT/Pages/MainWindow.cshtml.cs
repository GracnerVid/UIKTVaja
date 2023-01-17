using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using UIKT.Resources;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;

namespace UIKT.Pages
{
    public class MainWindowModel : PageModel
    {
        public string ime { get; set; }
        public string priimek { get; set; }
        public string emso { get; set; }
        public string dst { get; set; }
        public string iban { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public bool usrData { get; set; }
        public bool privacy { get; set; }
        public bool cookies { get; set; }
        public string uporabnik { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MainWindowModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var usrDataRaw = httpContext.Session.GetString("usr");
            var privacyRaw = httpContext.Session.GetString("privacy");
            var cookiesRaw = httpContext.Session.GetString("cookies");

            #region bool translate
            if (usrDataRaw == "on")
            {
                usrData = true;
            }
            else
            {
                usrData = false;
            }
            if (privacyRaw == "on")
            {
                privacy = true;
            }
            else
            {
                privacy = false;
            }
            if (cookiesRaw == "on")
            {
                cookies = true;
            }
            else
            {
                cookies = false;
            }
            #endregion

            uporabnik = httpContext.Session.GetString("uporabnik");
            if (httpContext != null)
            {
                if (cookies == true)
                {
                    ime = httpContext.Session.GetString("fname");
                    priimek = httpContext.Session.GetString("fpass");
                    emso = httpContext.Session.GetString("femso");
                    dst = httpContext.Session.GetString("fdst");
                    iban = httpContext.Session.GetString("fiban");
                    naziv = httpContext.Session.GetString("fnaziv");
                    opis = httpContext.Session.GetString("fopis");
                }
            }
            else
            {
                ime = "";
                priimek = "";
                emso = "";
                dst = "";
                iban = "";
                naziv = "";
                opis = "";
            }
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
            var btn = Request.Form["submit"];

            var httpContext = _httpContextAccessor.HttpContext;
            var usrDataRaw = httpContext.Session.GetString("usr");
            var privacyRaw = httpContext.Session.GetString("privacy");
            var cookiesRaw = httpContext.Session.GetString("cookies");

            #region bool translate
            if (usrDataRaw == "on")
            {
                usrData = true;
            }
            else
            {
                usrData = false;
            }
            if (privacyRaw == "on")
            {
                privacy = true;
            }
            else
            {
                privacy = false;
            }
            if (cookiesRaw == "on")
            {
                cookies = true;
            }
            else
            {
                cookies = false;
            }
            #endregion


            switch (btn)
            {
                case "Shrani":
                    if (cookies == true)
                    {
                        HttpContext.Session.SetString("fname", ime);
                        HttpContext.Session.SetString("fpass", priimek);
                        HttpContext.Session.SetString("femso", emso);
                        HttpContext.Session.SetString("fdst", davcnast);
                        HttpContext.Session.SetString("fiban", iban);
                        HttpContext.Session.SetString("fnaziv", naziv);
                        HttpContext.Session.SetString("fopis", opis);
                    }
                    return Page();

                case "Pošlji":
                    Vloga vloga = new Vloga(ime, priimek, emso, davcnast, iban, naziv, opis);
                    string json = JsonConvert.SerializeObject(vloga);
                    string fileName = "vloga" + DateTime.Today.ToString("dd-MM-yyyy") + ime + "_" + priimek + ".json";
                    string folderPath = Path.GetFullPath(@"Vloge");
                    string filePath = Path.Combine(folderPath, fileName);
                    System.IO.File.WriteAllText(filePath, json);
                    return RedirectToPage("/Index");

                default:
                    break;
            }

            //if (ime == "" || priimek == "" || emso == "" || davcnast == "" ||
            //    iban == "" || naziv == "" || opis == "")
            //{
            //    //return RedirectToPage("/MainWindow");
            //    if (cookies == true)
            //    {
            //        HttpContext.Session.SetString("fname", ime);
            //        HttpContext.Session.SetString("fpass", priimek);
            //        HttpContext.Session.SetString("femso", emso);
            //        HttpContext.Session.SetString("fdst", davcnast);
            //        HttpContext.Session.SetString("fiban", iban);
            //        HttpContext.Session.SetString("fnaziv", naziv);
            //        HttpContext.Session.SetString("fopis", opis);
            //    }
            //    return Page();
            //}
            //else
            //{
            //    Vloga vloga = new Vloga(ime, priimek, emso, davcnast, iban, naziv, opis);
            //    string json = JsonConvert.SerializeObject(vloga);
            //    string fileName = "vloga" + DateTime.Today.ToString("dd-MM-yyyy") + ime + "_" + priimek + ".json";
            //    string folderPath = Path.GetFullPath(@"Vloge");
            //    string filePath = Path.Combine(folderPath, fileName);
            //    System.IO.File.WriteAllText(filePath, json);
            //    return RedirectToPage("/Index");
            //}
            return Page();
        }
    }
}

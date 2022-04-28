using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Route.Domain.Model;
using Serviceapplication.ServiceCity;
using Serviceapplication.ServiceDoc;
using Serviceapplication.ServiceWorkTeam;

namespace RouteSystem.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {

        private static IWebHostEnvironment _hostEnvironment;
        public static List<List<string>> routes = new();
        public static List<string> headers = new();
        public static IEnumerable<string> serviceList;
        public static string service;
        public static string city;
        public static string downloadFile;

        public RouteController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadExcel(IFormFile file)
        { 
            var checkFile = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];

            if (checkFile == ".xlsx" || checkFile == ".xls")
            {

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage excel = new(file.OpenReadStream());
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.FirstOrDefault();
              
                List<string> header = new();
                List<string> listService = new();
                var colmuns = worksheet.Dimension.End.Column;
                var lines = worksheet.Dimension.End.Row;
                List<List<string>> content = new();
                bool check = false;

                int Cep = 0;
                int cols = 0;

            
                for (int i = 1; i <= colmuns ; i++)
                {
                    header.Add(worksheet.Cells[1, i].Value.ToString());

                    if (worksheet.Cells[1, i].Value.ToString().ToUpper().Equals("CEP"))
                        Cep = i - 1;

                    if (worksheet.Cells[1, i].Value.ToString().ToUpper().Equals("SERVIÇO"))
                        cols = i;
                }

           
                for (int row = 2; row <= lines; row++)
                {
                    for (int line = cols; line <= cols; line++)
                    {
                        listService.Add(worksheet.Cells[row, cols].Value?.ToString() ?? null);
                    }
                }
                worksheet.Cells[2, 1, lines, colmuns].Sort(Cep, false);          
             
                for (int rows = 1; rows < lines; rows++)
                {
                    List<string> contentLine = new();
                    check = false;
                    for (int k = 1; k < colmuns; k++)
                    {
                        var conteudo = worksheet.Cells[rows, k].Value?.ToString() ?? "";
                        contentLine.Add(conteudo);
                        check = true;

                    }
                    if (check)
                        content.Add(contentLine);
                }

                var removeRepeat = listService;
                var listaSemDuplicidade = removeRepeat.Distinct().ToList();
                headers = header;
                routes = content;
                serviceList = listaSemDuplicidade;

                return RedirectToAction(nameof(CitySelect));
            }
            else
            {

                TempData["error"] = "Arquivo não compativo. " +
                    "\nFavor verificar se é .xlsx.";

                return RedirectToRoute(new { controller = "Home", action = "Index" });
          
            }

           
        }


        public async Task<IActionResult> CitySelect()
        {
            var seachCity = ServiceCityApp.GetCity();
            ViewBag.AllCity = await seachCity;
            ViewBag.AllService = serviceList;
            return View();
        }

       public async Task<IActionResult> FileSelect()
        {
            city = Request.Form["Name"].ToString();
            service = Request.Form["serviceName"].ToString();

            var team = ServiceTeamApp.SeachCityTeam(city);
            ViewBag.TeamCity = await team;

            ViewBag.ReadFile = headers;

            
                if (team.Result.Count != 0) {
                    return View();
                }
                else
                {
                    TempData["error"] = "Não tem equipe para cidade selecionada!!";
                    return RedirectToRoute(new { controller = "Route", action = "CitySelect" });
                }
          
        }
     
        [HttpPost]
        public async Task<IActionResult> GenereatorDoc()
        {
            List<WorkTeam> teams = new();
            var teamSelect = Request.Form["Team"].ToList();
            var headerSelect = Request.Form["Header"].ToList();



            foreach (var item in teamSelect)
            {
                var seachTeam = await ServiceTeamApp.SeachWorkTeam(item);
                teams.Add(seachTeam);
            }

            var seachCity = await ServiceCityApp.SeachCityName(city);

            var filename = await ServiceDocApp.CreateDoc(teams, headerSelect, routes, service, seachCity, _hostEnvironment.WebRootPath);
            downloadFile = $"{_hostEnvironment.WebRootPath}//files//{filename}";

            return View();
        }
        public FileContentResult Download()
        {
            var fileName = downloadFile.Split("//").ToList();
            var file = System.IO.File.ReadAllBytes(downloadFile);
            return File(file, "application/octet-stream", fileName.Last().ToString());
        }

    }
}

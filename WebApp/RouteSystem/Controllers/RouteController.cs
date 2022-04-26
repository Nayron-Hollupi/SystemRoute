using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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


        public static List<List<string>> routes = new();
        public static List<string> headers = new();
        public static IEnumerable<string> serviceList;
        public static string service;
        public static string city;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TeamOperationCity(IFormFile file)
        {
       
            int cepColumn = 0;
            int serviceColumn = 0;
            bool check = false;
            List<string> header = new();
            List<string> listService = new();
            List<List<string>> content = new();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage fileexcel = new(file.OpenReadStream());
            ExcelWorksheet worksheet = fileexcel.Workbook.Worksheets.FirstOrDefault();

            var totalColumn = worksheet.Dimension.End.Column;
            var totalRow = worksheet.Dimension.End.Row;

            for (int column = 1; column < totalColumn; column++)
            {
                header.Add(worksheet.Cells[1, column].Value.ToString());

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("CEP"))
                    cepColumn = column - 1;

                if (worksheet.Cells[1, column].Value.ToString().ToUpper().Equals("SERVIÇO"))
                    serviceColumn = column;
            }

            for (int row = 2; row < totalRow; row++)
            {
                for (int line = serviceColumn; line <= serviceColumn; line++)
                {
                    listService.Add(worksheet.Cells[row, serviceColumn].Value?.ToString() ?? null);
                }
            }

            worksheet.Cells[2, 1, totalRow, totalColumn].Sort(cepColumn, false);

            for (int rows = 1; rows < totalRow; rows++)
            {
                List<string> contentLine = new();
                check = false;
                for (int columns = 1; columns < totalColumn; columns++)
                {
                    var conteudo = worksheet.Cells[rows, columns].Value?.ToString() ?? "";
                    contentLine.Add(conteudo);
                    check = true;

                }
                if (check)
                    content.Add(contentLine);
            }

            var removeRepeatService = listService;
            var listaSemDuplicidade = removeRepeatService.Distinct();
            headers = header;
            routes = content;
            serviceList = listaSemDuplicidade;
            return RedirectToAction(nameof(SelectCity));
        }

        public async Task<IActionResult> SelectCity()
        {
            var localizarcity = ServiceCityApp.GetCity();
            ViewBag.AllCity = await localizarcity;
            ViewBag.AllService = serviceList;
            return View();
        }

       public async Task<IActionResult> SelectFile()
        {
            city = Request.Form["Name"].ToString();
            service = Request.Form["serviceName"].ToString();

            var team = ServiceTeamApp.SeachCityTeam(city); // Qual equipe vai em qual cidade
            ViewBag.TeamCity = await team;

            ViewBag.ReadFile = headers;
            return View();
        }
     
        [HttpPost]
        public async Task<IActionResult> GenereatorDoc()
        {
            List<WorkTeam> teams = new();
            var teamSelect = Request.Form["checkTeamService"].ToList();
            var headerSelect = Request.Form["checkHeader"].ToList();



            foreach (var item in teamSelect)
            {
                var seachTeam = await ServiceTeamApp.SeachWorkTeam(item);
                teams.Add(seachTeam);
            }

            var seachCity = await ServiceCityApp.SeachCityName(city);

            ServiceDocApp.CreateDoc(teams, headerSelect, routes, service, seachCity);
         

            return View();
        }


    }
}

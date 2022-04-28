using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Domain.Model;

namespace Serviceapplication.ServiceDoc
{
    public class ServiceDocApp
    {
        public static async Task<string> CreateDoc(List<WorkTeam> team, List<string> checkOption, List<List<string>> routes, string serviceSelect, City city, string rootPath)
        {
            var routesCount = routes.Count;
            var allColumn = routes[0];

            var serviceColumn = routes[0].FindIndex(column => column == "SERVIÇO" || column == "serviço");
            var cityColumn = routes[0].FindIndex(column => column == "CIDADE" || column == "cidade");
            var cepColumn = routes[0].FindIndex(column => column == "CEP" || column == "cep");

            for (int i = 0; i < routesCount; i++)
            {
                routes.Remove(routes.Find(route => route[cityColumn].ToUpper() != city.Name.ToUpper()));
                routes.Remove(routes.Find(route => route[serviceColumn].ToUpper() != serviceSelect.ToUpper()));
            }

            var divisionTeam = routes.Count / team.Count;
            var restDivision = routes.Count % team.Count;

            var index = 0;
            var pathFiles = $"{rootPath}//files";

            if (!Directory.Exists(pathFiles))
                Directory.CreateDirectory(pathFiles);

            var filename = $"Rota-{serviceSelect}.docx";

            var pathFile = $"{pathFiles}//{filename}";
            // var filename = $@"C:\Users\Nayron\OneDrive\Área de Trabalho\ArquivoRotas\-{serviceSelect}-{DateTime.Now:dd-MM-yyyy}.docx";

            using (FileStream fileStream = new(pathFile, FileMode.Create))
            {
                await using (StreamWriter writer = new(fileStream, Encoding.UTF8))
                {
                    writer.WriteLine($"{serviceSelect} - {DateTime.Now:dd/MM/yyyy}\t {city.Name}\n\n");

                    foreach (var item in team)
                    {
                        writer.WriteLine("Time: " + item.Name + "\nRotas:\n");

                       for (int i = 0; i < divisionTeam; i++)
                        {
                            if (i == 0 && restDivision > 0)
                                divisionTeam++;

                            if (i == 0)
                                restDivision--;

                            foreach (var check in checkOption)
                            {
                                writer.WriteLine($"{allColumn[int.Parse(check)]}: {routes[i + index][int.Parse(check)]}");
                            }

                            if ((i + 1) >= divisionTeam)
                            {
                                index += 1 + i;
                            }

                            writer.WriteLine("\n");
                        }

                        if (restDivision >= 0)
                            divisionTeam--;

                        writer.WriteLine("");
                    }

                    writer.Close();
                }
                fileStream.Close();
            }
            return filename;
        }
    }
}

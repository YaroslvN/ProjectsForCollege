using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsForCollege.Controllers
{
    public class HomeController : Controller
    {
       private static List<Teacher> teachers = new List<Teacher>
       {
            new Teacher { Id = 1, Name = "Василиса Александровна", Projects = new List<Project>{
            new Project { Id = 1, Name = "PracticalOne", Description = "Дано целое число N и набор из N прямоугольников, заданных своими сторонами - парами чисел (a, b). Найти минимальную площадь прямоугольника из данного набора" },
            new Project { Id = 2, Name = "PracticalTwo", Description = "Описать класс, представляющий треугольник. Предусмотреть методы для создания объектов, вычисления площади, периметра и точки пересечения медиан. Описать свойства для получения состояния объекта" },
            new Project { Id = 3, Name = "PracticalThree", Description = "В заданном массиве найти максимальный элемент. Элементы, стоящие после максимального элемента заменить нулями." },
            }},
            new Teacher {Id = 2, Name = "Сергей Алексеевич", Projects = new List<Project>()}
       };

        public IActionResult Index()
        {
            return View(teachers);
        }

        public IActionResult Details(int teacherId, int projectId)
        {
            Teacher? teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null) return NotFound();

            Project? project = teacher.Projects?.FirstOrDefault(p => p.Id == projectId);
            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost]
        public IActionResult SetHeader(int teacherId, int projectId)
        {
            var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
            var project = teacher?.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null) return NotFound();

           return View(project);
        }
    }
}
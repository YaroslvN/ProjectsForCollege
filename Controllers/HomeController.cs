using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectsForCollege.Controllers
{
    public class HomeController : Controller
    {
      private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var teachers = _context.Teachers.Include(t => t.Projects).ToList();
        return View(teachers);
    }

    public IActionResult ProjectDetails(int projectId)
    {
        Project project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null) return NotFound();

        string partialViewName = project.Id switch
            {
                1 => "Sp_ProjectOne",
                2 => "Sp_ProjectTwo",
                3 => "Sp_ProjectThree",
                _ => "Sp_ProjectOne"
            };

        return PartialView(partialViewName, project); // Вернуть частичное представление
    }
[HttpPost]
    public IActionResult GenerateRectangles(int n)
    {
        ViewBag.RectangleCount = n;
        return PartialView("_RectangleDimensions");
    }

    [HttpPost]
    public IActionResult CalculateMinArea(int[] a, int[] b)
    {
        if (a.Length != b.Length || a.Length == 0)
            return BadRequest("Ошибка в данных.");

        int minArea = int.MaxValue;

        for (int i = 0; i < a.Length; i++)
        {
            int area = a[i] * b[i];
            if (area < minArea)
            {
                minArea = area;
            }
        }

        return Json(new { minArea });
    }
        [HttpPost]
        public IActionResult Calculate(double sideA, double sideB, double sideC)
        {
            var triangle = new Triangle(sideA, sideB, sideC);

            if (!triangle.IsValid())
            {
                return BadRequest("Invalid triangle sides.");
            }

            var result = new
            {
                perimeter = triangle.CalculatePerimeter(),
                area = triangle.CalculateArea(),
                centroid = triangle.CalculateCentroid()
            };

            return Json(result);
        }

         public IActionResult ProcessArray([FromBody] int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return BadRequest("Invalid array.");
            }

            // Нахождение максимального элемента
            int maxIndex = FindMaxIndex(array);

            // Замена элементов после максимального на 0
            for (int i = maxIndex + 1; i < array.Length; i++)
            {
                array[i] = 0;
            }

            // Подготовка ответа
            var result = new
            {
                array = array,
                maxElement = array[maxIndex]
            };

            return Json(result);
        }

        // Метод для нахождения индекса максимального элемента
        private int FindMaxIndex(int[] array)
        {
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

    [HttpPost]
    public IActionResult SetHeader(int teacherId, int projectId)
    {
        var project = _context.Projects
            .FirstOrDefault(p => p.Id == projectId && p.TeacherId == teacherId);
        if (project == null) return NotFound();

        return View(project);
    }

    }
}
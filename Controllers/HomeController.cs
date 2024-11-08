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
    public IActionResult SetHeader(int teacherId, int projectId)
    {
        var project = _context.Projects
            .FirstOrDefault(p => p.Id == projectId && p.TeacherId == teacherId);
        if (project == null) return NotFound();

        return View(project);
    }

    }
}
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
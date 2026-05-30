using Microsoft.AspNetCore.Mvc;
using MiniCourseCatalog.Mvc.Models;
using MiniCourseCatalog.Mvc.Services;
using MiniCourseCatalog.Mvc.ViewModels;

namespace MiniCourseCatalog.Mvc.Controllers;

public class CoursesController : Controller
{
    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService; 
    }

    public IActionResult Index(string keyword = "", string category = "", string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        var rawCourses = _courseService.GetAll();
        var categories = rawCourses
            .Select(c => c.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        var filteredCourses = rawCourses.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            filteredCourses = filteredCourses.Where(c =>
                c.Code.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Instructor.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            filteredCourses = filteredCourses.Where(c =>
                string.Equals(c.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        var courseItems = filteredCourses.Select(c => new CourseListItemViewModel
        {
            Id = c.Id,
            Code = c.Code,
            Name = c.Name,
            Category = c.Category,
            Instructor = c.Instructor,
            TuitionFee = c.TuitionFee,
            CurrentEnrollment = c.CurrentEnrollment,
            MaxCapacity = c.MaxCapacity
        }).ToList();

        var viewModel = new CourseIndexViewModel
        {
            Courses = courseItems,
            Categories = categories,
            Keyword = keyword,
            Category = category,
            Theme = theme,
            TotalCoursesBeforeFilter = rawCourses.Count
        };

        return View(viewModel);
    }

    public IActionResult Detail(int id, string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        var course = _courseService.GetById(id);
        if (course == null)
        {
            return NotFound($"Không thể tìm thấy thông tin khóa học với mã ID = {id}"); 
        }

        var detailVm = new CourseDetailViewModel
        {
            Id = course.Id,
            Code = course.Code,
            Name = course.Name,
            Category = course.Category,
            Instructor = course.Instructor,
            TuitionFee = course.TuitionFee,
            CurrentEnrollment = course.CurrentEnrollment,
            MaxCapacity = course.MaxCapacity,
            StartDate = course.StartDate
        };

        return View(detailVm);
    }

    public IActionResult Stats(string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        var statsVm = _courseService.GetStats(); 
        return View(statsVm);
    }

    public IActionResult Welcome()
    {
        return Content("Hệ thống quản lý đào tạo Mini Training Center xin chào học viên!"); 
    }

    public IActionResult CourseJson()
    {
        var data = _courseService.GetAll();
        return Json(data); 
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index)); 
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là trang phản hồi mẫu 404 thử nghiệm từ hệ thống."); 
    }

    public IActionResult CategoryInfo()
    {
        return Content("Danh mục hiện có: Khoa học Cơ bản, Công nghệ Thông tin, AI & Data Science, Ngoại Ngữ, Marketing, Thiết kế đồ họa");
    }

    private static string NormalizeTheme(string theme)
    {
        return string.Equals(theme, "dark", StringComparison.OrdinalIgnoreCase) ? "dark" : "light";
    }

    [HttpGet]
    public IActionResult Search(string keyword = "", string category = "", string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        var results = _courseService.Search(keyword, category)
            .Select(c => new CourseListItemViewModel
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
                Category = c.Category,
                Instructor = c.Instructor,
                TuitionFee = c.TuitionFee,
                CurrentEnrollment = c.CurrentEnrollment,
                MaxCapacity = c.MaxCapacity
            })
            .ToList();

        var viewModel = new CourseSearchViewModel
        {
            Keyword = keyword,
            Category = category,
            Theme = theme,
            Categories = _courseService.GetCategories(),
            Results = results
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create(string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        var viewModel = new CourseCreateViewModel
        {
            StartDate = DateTime.Today,
            MaxCapacity = 20
        };

        return View(viewModel);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CourseCreateViewModel viewModel, string theme = "light")
    {
        theme = NormalizeTheme(theme);
        ViewData["Theme"] = theme;

        if (viewModel.CurrentEnrollment > viewModel.MaxCapacity)
        {
            ModelState.AddModelError(
                nameof(viewModel.CurrentEnrollment),
                "Số học viên hiện tại không được lớn hơn sức chứa tối đa");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var course = new Course
        {
            Code = viewModel.Code,
            Name = viewModel.Name,
            Category = viewModel.Category,
            Instructor = viewModel.Instructor,
            TuitionFee = viewModel.TuitionFee,
            CurrentEnrollment = viewModel.CurrentEnrollment,
            MaxCapacity = viewModel.MaxCapacity,
            StartDate = viewModel.StartDate
        };

        _courseService.Add(course);

        TempData["SuccessMessage"] = $"Đã thêm khóa học {course.Name} thành công.";

        return RedirectToAction(nameof(Index), new { theme });
    }
}

using Microsoft.AspNetCore.Mvc;
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

    public IActionResult Index()
    {
        var rawCourses = _courseService.GetAll(); 
        var viewModels = rawCourses.Select(c => new CourseListItemViewModel
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

        return View(viewModels); 
    }

    public IActionResult Detail(int id)
    {
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

    public IActionResult Stats()
    {
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
}

using MiniCourseCatalog.Mvc.Models;
using MiniCourseCatalog.Mvc.ViewModels;

namespace MiniCourseCatalog.Mvc.Services;

public class CourseService
{
    private readonly List<Course> _courses = new()
    {
        new Course { Id = 1, Code = "MATH-101", Name = "Toán Cao Cấp A1", Category = "Khoa học Cơ bản", Instructor = "Thầy Nguyễn Văn Hải", TuitionFee = 1200000, CurrentEnrollment = 40, MaxCapacity = 40, StartDate = new DateTime(2026, 01, 05) },
        new Course { Id = 2, Code = "PRG-201", Name = "Lập Trình Hướng Đối Tượng C#", Category = "Công nghệ Thông tin", Instructor = "Cô Lê Thị Hoa", TuitionFee = 2500000, CurrentEnrollment = 28, MaxCapacity = 30, StartDate = new DateTime(2026, 01, 10) },
        new Course { Id = 3, Code = "DATA-302", Name = "Nhập môn Khoa Học Dữ Liệu", Category = "AI & Data Science", Instructor = "Thầy Trần Đức Hùng", TuitionFee = 3500000, CurrentEnrollment = 12, MaxCapacity = 25, StartDate = new DateTime(2026, 03, 15) },
        new Course { Id = 4, Code = "ENG-105", Name = "Tiếng Anh Giao Tiếp VSTEP B1", Category = "Ngoại Ngữ", Instructor = "Ms. Emily Smith", TuitionFee = 1800000, CurrentEnrollment = 0, MaxCapacity = 20, StartDate = new DateTime(2026, 02, 20) },
        new Course { Id = 5, Code = "DIG-101", Name = "Digital Marketing Cơ Bản", Category = "Marketing", Instructor = "Cô Trần Thanh Mai", TuitionFee = 2000000, CurrentEnrollment = 8, MaxCapacity = 30, StartDate = new DateTime(2026, 04, 01) },
        new Course { Id = 6, Code = "GRA-201", Name = "Graphic Design với Illustrator", Category = "Thiết kế đồ họa", Instructor = "Thầy Lê Văn Cường", TuitionFee = 2200000, CurrentEnrollment = 25, MaxCapacity = 25, StartDate = new DateTime(2025, 12, 15) }
    };

    public List<Course> GetAll() => _courses;

    public Course? GetById(int id) => _courses.FirstOrDefault(c => c.Id == id); 

    public CourseStatsViewModel GetStats()
    {
        var totalCourses = _courses.Count;
        var totalStudents = _courses.Sum(c => c.CurrentEnrollment);
        var totalRevenue = _courses.Sum(c => c.TuitionFee * c.CurrentEnrollment);
        var fullCourses = _courses.Count(c => c.CurrentEnrollment >= c.MaxCapacity); 
        var pendingCourses = _courses.Count(c => c.CurrentEnrollment > 0 && c.CurrentEnrollment < c.MaxCapacity && c.MaxCapacity - c.CurrentEnrollment <= 3); 

        return new CourseStatsViewModel
        {
            TotalCourses = totalCourses,
            TotalStudents = totalStudents,
            TotalExpectedRevenue = totalRevenue,
            FullCoursesCount = fullCourses,
            PendingCoursesCount = pendingCourses
        };
    }
}

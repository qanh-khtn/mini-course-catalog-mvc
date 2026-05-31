using MiniCourseCatalog.Mvc.Models;
using MiniCourseCatalog.Mvc.ViewModels;

namespace MiniCourseCatalog.Mvc.Services;

public class CourseService
{
    private readonly List<Course> _courses = new()
    {
        new Course { Id = 1, Code = "MATH-101", Name = "Toán Cao Cấp A1", Category = "Khoa học Cơ bản", Instructor = "Thầy Nguyễn Văn Hải", TuitionFee = 1200000, CurrentEnrollment = 40, MaxCapacity = 40, StartDate = new DateTime(2026, 09, 05) },
        new Course { Id = 2, Code = "PRG-201", Name = "Lập Trình Hướng Đối Tượng C#", Category = "Công nghệ Thông tin", Instructor = "Cô Lê Thị Hoa", TuitionFee = 2500000, CurrentEnrollment = 28, MaxCapacity = 30, StartDate = new DateTime(2026, 09, 10) },
        new Course { Id = 3, Code = "DATA-302", Name = "Nhập môn Khoa Học Dữ Liệu", Category = "AI & Data Science", Instructor = "Thầy Trần Đức Hùng", TuitionFee = 3500000, CurrentEnrollment = 9, MaxCapacity = 25, StartDate = new DateTime(2026, 09, 15) },
        new Course { Id = 4, Code = "ENG-105", Name = "Tiếng Anh Giao Tiếp VSTEP B1", Category = "Ngoại Ngữ", Instructor = "Ms. Emily Smith", TuitionFee = 1800000, CurrentEnrollment = 0, MaxCapacity = 20, StartDate = new DateTime(2026, 09, 20) },
        new Course { Id = 5, Code = "DIG-101", Name = "Digital Marketing Cơ Bản", Category = "Marketing", Instructor = "Cô Trần Thanh Mai", TuitionFee = 2000000, CurrentEnrollment = 8, MaxCapacity = 30, StartDate = new DateTime(2026, 10, 01) },
        new Course { Id = 6, Code = "GRA-201", Name = "Graphic Design với Illustrator", Category = "Thiết kế đồ họa", Instructor = "Thầy Lê Văn Cường", TuitionFee = 2200000, CurrentEnrollment = 25, MaxCapacity = 25, StartDate = new DateTime(2026, 10, 15) }
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

        var totalCapacity = _courses.Sum(c => c.MaxCapacity);
        double overallFillRate = totalCapacity > 0 ? Math.Round((double)totalStudents / totalCapacity * 100, 1) : 0;
        
        var topInstructor = _courses.GroupBy(c => c.Instructor)
                                    .OrderByDescending(g => g.Sum(c => c.CurrentEnrollment))
                                    .Select(g => g.Key)
                                    .FirstOrDefault() ?? "Chưa có";

        var categoryStats = _courses
            .GroupBy(c => c.Category)
            .OrderBy(g => g.Key)
            .Select(g =>
            {
                var studentCount = g.Sum(c => c.CurrentEnrollment);
                var capacity = g.Sum(c => c.MaxCapacity);

                return new CategoryStatsViewModel
                {
                    Category = g.Key,
                    CourseCount = g.Count(),
                    StudentCount = studentCount,
                    Capacity = capacity,
                    Revenue = g.Sum(c => c.TuitionFee * c.CurrentEnrollment),
                    FillRate = capacity > 0 ? Math.Round((double)studentCount / capacity * 100, 1) : 0
                };
            })
            .ToList();

        return new CourseStatsViewModel
        {
            TotalCourses = totalCourses,
            TotalStudents = totalStudents,
            TotalExpectedRevenue = totalRevenue,
            FullCoursesCount = fullCourses,
            PendingCoursesCount = pendingCourses,
            OverallFillRate = overallFillRate, 
            TopInstructor = topInstructor,
            CategoryStats = categoryStats
        };
    }

    public void Add(Course course)
    {
        var nextId = _courses.Count == 0 ? 1 : _courses.Max(c => c.Id) + 1;
        course.Id = nextId;
        _courses.Add(course);
    }
    public List<Course> Search(string keyword, string category)
    {
        var query = _courses.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(c =>
                c.Code.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Instructor.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(c =>
                string.Equals(c.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        return query.ToList();
    }
    public List<string> GetCategories()
    {
        return _courses
            .Select(c => c.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    public bool ExistsSameClass(string code, string instructor, DateTime startDate)
    {
        return _courses.Any(c =>
            string.Equals(c.Code?.Trim(), code?.Trim(), StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c.Instructor?.Trim(), instructor?.Trim(), StringComparison.OrdinalIgnoreCase) &&
            c.StartDate.Date == startDate.Date);
    }
}

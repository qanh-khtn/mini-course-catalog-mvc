namespace MiniCourseCatalog.Mvc.ViewModels;

public class CourseStatsViewModel
{
    public int TotalCourses { get; set; }
    public int TotalStudents { get; set; }
    public decimal TotalExpectedRevenue { get; set; }
    public int FullCoursesCount { get; set; }
    public int PendingCoursesCount { get; set; }   

    public string TotalExpectedRevenueText => $"{TotalExpectedRevenue:N0} VND";
}

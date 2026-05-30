using System.ComponentModel.DataAnnotations;

namespace MiniCourseCatalog.Mvc.ViewModels;

public class CourseCreateViewModel
{
    [Required(ErrorMessage = "Mã khóa học không được để trống")]
    [StringLength(20, ErrorMessage = "Mã khóa học tối đa 20 ký tự")]
    [Display(Name = "Mã khóa học")]
    public string Code { get; set; } = "";

    [Required(ErrorMessage = "Tên khóa học không được để trống")]
    [StringLength(100, ErrorMessage = "Tên khóa học tối đa 100 ký tự")]
    [Display(Name = "Tên khóa học")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Chuyên ngành không được để trống")]
    [Display(Name = "Chuyên ngành")]
    public string Category { get; set; } = "";

    [Required(ErrorMessage = "Giảng viên không được để trống")]
    [Display(Name = "Giảng viên")]
    public string Instructor { get; set; } = "";

    [Range(0, 100000000, ErrorMessage = "Học phí phải lớn hơn hoặc bằng 0")]
    [Display(Name = "Học phí")]
    public decimal TuitionFee { get; set; }

    [Range(0, 1000, ErrorMessage = "Số học viên hiện tại không hợp lệ")]
    [Display(Name = "Số học viên hiện tại")]
    public int CurrentEnrollment { get; set; }

    [Range(1, 1000, ErrorMessage = "Sức chứa tối đa phải lớn hơn 0")]
    [Display(Name = "Sức chứa tối đa")]
    public int MaxCapacity { get; set; } = 20;

    [DataType(DataType.Date)]
    [Display(Name = "Ngày khai giảng")]
    public DateTime StartDate { get; set; } = DateTime.Today;
}
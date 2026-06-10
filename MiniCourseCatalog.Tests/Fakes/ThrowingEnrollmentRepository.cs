using MiniCourseCatalog.Mvc.Models;
using MiniCourseCatalog.Mvc.Repositories.Interfaces;

namespace MiniCourseCatalog.Tests.Fakes;

/// <summary>
/// Fake Repository cố tình ném exception ở bước AddAsync
/// để mô phỏng lỗi giữa chừng trong nghiệp vụ đăng ký —
/// dùng kiểm chứng transaction phải ROLLBACK toàn bộ (không trừ ghế, không lưu enrollment).
/// </summary>
public class ThrowingEnrollmentRepository : IEnrollmentRepository
{
    public Task<List<Enrollment>> GetAllWithDetailsReadOnlyAsync() =>
        Task.FromResult(new List<Enrollment>());

    public Task<bool> IsAlreadyEnrolledAsync(int courseId, int studentId) =>
        Task.FromResult(false);

    public Task AddAsync(Enrollment enrollment) =>
        throw new InvalidOperationException("Mô phỏng lỗi ghi dữ liệu giữa transaction.");
}

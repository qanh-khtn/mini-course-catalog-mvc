# Mini Training Center Course Catalog MVC

Ứng dụng ASP.NET Core MVC quản lý danh mục khóa học cho một trung tâm đào tạo nhỏ. Project được phát triển tiếp từ Lab02 và nâng cấp theo yêu cầu Lab03: Layout, Partial View, Tag Helpers, Model Binding, form tìm kiếm GET, form thêm dữ liệu POST và validation bằng DataAnnotations.

## Chủ Đề

**Mini Training Center Course Catalog MVC**

Hệ thống hỗ trợ xem danh sách khóa học, xem chi tiết lớp học, thống kê dữ liệu đào tạo, tìm kiếm khóa học và thêm khóa học mới thông qua form có kiểm tra dữ liệu.

## Công Nghệ Sử Dụng

- ASP.NET Core MVC
- C#
- Razor View Engine
- Bootstrap 5
- Bootstrap Icons
- HTML, CSS, JavaScript
- LINQ
- DataAnnotations

## Cấu Trúc Chính

```text
MiniCourseCatalog.Mvc
├── Controllers
│   ├── HomeController.cs
│   └── CoursesController.cs
├── Models
│   └── Course.cs
├── Services
│   └── CourseService.cs
├── ViewModels
│   ├── CategoryStatsViewModel.cs
│   ├── CourseCreateViewModel.cs
│   ├── CourseDetailViewModel.cs
│   ├── CourseIndexViewModel.cs
│   ├── CourseListItemViewModel.cs
│   ├── CourseSearchViewModel.cs
│   └── CourseStatsViewModel.cs
├── Views
│   ├── Courses
│   │   ├── Create.cshtml
│   │   ├── Detail.cshtml
│   │   ├── Index.cshtml
│   │   ├── Search.cshtml
│   │   ├── Stats.cshtml
│   │   └── _CourseStatusBadge.cshtml
│   ├── Home
│   │   └── Index.cshtml
│   └── Shared
│       ├── _CourseCard.cshtml
│       ├── _CourseSearchResultList.cshtml
│       ├── _Layout.cshtml
│       ├── _SearchFilterForm.cshtml
│       └── _ValidationScriptsPartial.cshtml
└── wwwroot
    ├── css
    └── js
```

## Chức Năng Theo Yêu Cầu Lab03

### Layout Dùng Chung

- Cập nhật `Views/Shared/_Layout.cshtml` làm layout chung cho toàn bộ website.
- Menu điều hướng dùng Tag Helpers, gồm: Trang chủ, Khóa học, Thống kê, Tìm kiếm, Thêm khóa học.
- Header, footer, CSS và nút chuyển giao diện sáng/tối được dùng chung trên các trang.

### Partial View

- Tạo `_CourseCard.cshtml` để hiển thị từng khóa học dưới dạng card.
- Trang `/Courses` dùng Partial View để render danh sách khóa học thay vì bảng đơn giản.
- Tạo `_CourseSearchResultList.cshtml` để tách phần hiển thị kết quả tìm kiếm.
- Tạo `_SearchFilterForm.cshtml` để tái sử dụng form lọc nhanh.

### Trang Danh Sách Khóa Học

- Route: `/Courses`
- Hiển thị danh sách khóa học bằng card grid.
- Mỗi card có mã khóa học, tên khóa học, chuyên ngành, giảng viên, học phí, sĩ số, trạng thái và nút xem chi tiết.
- Sau khi thêm khóa học thành công, trang danh sách hiển thị thông báo bằng `TempData`.

### Tìm Kiếm Khóa Học Bằng GET

- Route: `/Courses/Search`
- ViewModel: `CourseSearchViewModel`
- Action: `CoursesController.Search(string keyword, string category, string theme)`
- Form dùng phương thức `GET` và nhận dữ liệu từ Query String.
- Hỗ trợ tìm theo mã khóa học, tên khóa học và giảng viên.
- Hỗ trợ lọc theo chuyên ngành.
- Nếu không có kết quả, hệ thống hiển thị thông báo cảnh báo thay vì để trang trống.

### Thêm Khóa Học Bằng POST

- Route: `/Courses/Create`
- ViewModel: `CourseCreateViewModel`
- Có action GET để hiển thị form và action POST để nhận dữ liệu submit.
- Form dùng Tag Helpers: `asp-for`, `asp-validation-for`, `asp-controller`, `asp-action`.
- Form có các field: mã khóa học, tên khóa học, chuyên ngành, giảng viên, học phí, số học viên hiện tại, sức chứa tối đa, ngày khai giảng.
- Dùng `ModelState.IsValid` để kiểm tra dữ liệu hợp lệ.
- Sau khi thêm thành công, dùng `RedirectToAction(nameof(Index))` để quay về trang danh sách.

## Tính Năng Mở Rộng

### Validation Nâng Cao

- Trường `Code` dùng `RegularExpression` để bắt buộc mã khóa học đúng định dạng, ví dụ `CS-101`, `ENG-005`, `MATH-005`.
- `CourseCreateViewModel` kế thừa `IValidatableObject` để kiểm tra nghiệp vụ: số học viên hiện tại không được lớn hơn sức chứa tối đa.
- `Create.cshtml` dùng `_ValidationScriptsPartial` để hỗ trợ client-side validation.

### Kiểm Tra Trùng Lớp Học

- Hệ thống không chặn trùng mã khóa học tuyệt đối vì một khóa học có thể mở nhiều lớp khác nhau.
- Hàm `ExistsSameClass()` trong `CourseService` chỉ báo trùng khi trùng đồng thời `Code`, `Instructor` và `StartDate`.

### Auto-Dismiss Alert

- Sau khi thêm khóa học thành công, Controller lưu thông báo vào `TempData`.
- JavaScript trong `_Layout.cshtml` tự động ẩn thông báo sau vài giây.

### Theme Switcher

- Ứng dụng hỗ trợ chuyển giao diện sáng/tối bằng tham số `theme` trên URL.
- Controller chuẩn hóa giá trị theme và truyền sang View bằng `ViewData`.
- Nút chuyển sáng/tối được đặt trong Layout nên dùng chung trên nhiều trang.

### Thống Kê Nâng Cao

- Tính tổng doanh thu dự kiến.
- Tính tổng số học viên.
- Tính tỷ lệ lấp đầy trung bình toàn trung tâm.
- Xác định giảng viên tiêu biểu bằng LINQ.
- Thống kê theo chuyên ngành: số lớp, số học viên, sức chứa, doanh thu và tỷ lệ lấp đầy.

## Chức Năng Từ Lab02 Vẫn Giữ Lại

- Trang chủ giới thiệu hệ thống.
- Trang chi tiết khóa học: `/Courses/Detail/{id}`.
- Trang thống kê: `/Courses/Stats`.
- Action trả về text bằng `Content()`: `/Courses/Welcome`.
- Action trả về JSON bằng `Json()`: `/Courses/CourseJson`.
- Action chuyển hướng bằng `RedirectToAction()`: `/Courses/GoToList`.
- Action xử lý không tìm thấy dữ liệu bằng `NotFound()`: `/Courses/Detail/999` và `/Courses/Force404`.

## Các URL Kiểm Thử

```text
/
/Courses
/Courses?theme=dark
/Courses/Detail/1
/Courses/Detail/999
/Courses/Stats
/Courses/Search
/Courses/Search?keyword=data
/Courses/Search?keyword=xyzabc
/Courses/Search?category=Marketing
/Courses/Create
/Courses/Welcome
/Courses/CourseJson
/Courses/GoToList
/Courses/Force404
/Courses/CategoryInfo
```

## Ảnh Minh Chứng

Các ảnh chụp kết quả chạy ứng dụng được lưu trong:

```text
screenshots-lab03
screenshots-lab03-extra
```

Một số ảnh quan trọng:

- `screenshots-lab03/01-home.png`: Trang chủ.
- `screenshots-lab03/02-courses-card-list.png`: Danh sách khóa học dạng card.
- `screenshots-lab03/03-search-page.png`: Trang tìm kiếm.
- `screenshots-lab03/06-create-form.png`: Form thêm khóa học.
- `screenshots-lab03/07-stats-dashboard.png`: Dashboard thống kê.
- `screenshots-lab03-extra/extra-01-invalid-code-validation.png`: Lỗi mã khóa học sai định dạng.
- `screenshots-lab03-extra/extra-02-invalid-capacity-validation.png`: Lỗi sĩ số lớn hơn sức chứa.
- `screenshots-lab03-extra/extra-03-duplicate-class-validation.png`: Lỗi trùng lớp học.
- `screenshots-lab03-extra/extra-04-create-success-alert.png`: Thêm thành công và hiển thị alert.
- `screenshots-lab03-extra/extra-05-auto-dismiss-after-4s.png`: Alert tự động biến mất.
- `screenshots-lab03-extra/extra-06-search-no-result.png`: Không tìm thấy kết quả tìm kiếm.

## Hướng Dẫn Chạy Project

Mở terminal tại thư mục chứa file `.csproj`:

```powershell
cd E:\Nam_4\Hocki2\ASP_vscode\asp-lab03\MiniCourseCatalog.Mvc
dotnet run
```

Sau đó mở URL được hiển thị trong terminal, ví dụ:

```text
http://localhost:5063
```

Có thể kiểm tra build bằng:

```powershell
dotnet build
```

## Ghi Chú

- Dữ liệu hiện đang lưu trong `List<Course>` hard-code bên trong `CourseService`, phù hợp cho phạm vi Lab03.
- Khi tắt ứng dụng, dữ liệu thêm mới trong phiên chạy hiện tại sẽ mất vì chưa kết nối database.
- Nếu trình duyệt chưa cập nhật CSS/HTML mới, nhấn `Ctrl + F5` để refresh mạnh.

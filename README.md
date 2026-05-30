# Mini Training Center Course Catalog MVC

Ứng dụng ASP.NET Core MVC quản lý danh mục khóa học cho một trung tâm đào tạo nhỏ. Project được phát triển tiếp từ Lab02 và nâng cấp theo yêu cầu Lab03 - Layout, Partial View, Tag Helpers & Model Binding.

## Chủ đề

**Mini Training Center Course Catalog MVC**

Hệ thống hỗ trợ xem danh sách khóa học, xem chi tiết lớp học, thống kê tình hình đào tạo, tìm kiếm khóa học và thêm khóa học mới thông qua form có validation.

## Công nghệ sử dụng

- ASP.NET Core MVC
- C#
- Razor View Engine
- Bootstrap 5
- Bootstrap Icons
- HTML, CSS, JavaScript
- LINQ
- DataAnnotations

## Cấu trúc chính sau Lab03

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

## Chức năng đã hoàn thành theo Lab03

### 1. Layout dùng chung

- Cập nhật `Views/Shared/_Layout.cshtml` làm khung giao diện chung cho toàn bộ website.
- Menu điều hướng dùng Tag Helpers, gồm: Trang chủ, Khóa học, Thống kê, Tìm kiếm, Thêm khóa học.
- Thêm nút chuyển giao diện sáng/tối dùng chung trên navbar.

### 2. Partial View tái sử dụng giao diện

- Tạo `Views/Shared/_CourseCard.cshtml` để hiển thị từng khóa học dưới dạng card.
- Refactor trang `/Courses` để render danh sách bằng Partial View thay vì viết toàn bộ HTML trực tiếp trong view.
- Tạo `Views/Shared/_CourseSearchResultList.cshtml` để tách phần hiển thị kết quả tìm kiếm.
- Tạo `Views/Shared/_SearchFilterForm.cshtml` để tái sử dụng form lọc nhanh khi cần.

### 3. Trang danh sách khóa học

- Route: `/Courses`
- Hiển thị danh sách khóa học bằng card grid.
- Mỗi card có mã khóa học, tên khóa học, chuyên ngành, giảng viên, học phí, sĩ số và nút xem chi tiết.
- Có hiển thị thông báo thành công từ `TempData` sau khi thêm khóa học mới.

### 4. Tìm kiếm khóa học bằng form GET

- Route: `/Courses/Search`
- ViewModel: `CourseSearchViewModel`
- Action: `CoursesController.Search(string keyword, string category, string theme)`
- Form dùng phương thức `GET`, nhận dữ liệu từ Query String.
- Hỗ trợ tìm kiếm theo mã khóa học, tên khóa học và giảng viên.
- Hỗ trợ lọc theo chuyên ngành.
- Kết quả tìm kiếm được render bằng partial `_CourseSearchResultList.cshtml`.
- Nếu không có kết quả, hệ thống hiển thị thông báo cảnh báo màu vàng.

### 5. Thêm khóa học bằng form POST

- Route: `/Courses/Create`
- ViewModel: `CourseCreateViewModel`
- Có action GET `Create` để hiển thị form.
- Có action POST `Create` để nhận dữ liệu submit.
- Form dùng Tag Helpers: `asp-for`, `asp-validation-for`, `asp-controller`, `asp-action`.
- Có nhiều hơn 4 field nhập liệu: mã khóa học, tên khóa học, chuyên ngành, giảng viên, học phí, số học viên hiện tại, sức chứa tối đa, ngày khai giảng.
- Dùng DataAnnotations để validation: `Required`, `StringLength`, `Range`, `DataType`.
- Dùng `ModelState.IsValid` để kiểm tra dữ liệu hợp lệ.
- Kiểm tra thêm nghiệp vụ: số học viên hiện tại không được lớn hơn sức chứa tối đa.
- Sau khi thêm thành công, dùng `TempData` để báo thành công và `RedirectToAction(nameof(Index))` để quay về danh sách.
- Dữ liệu mới được thêm bằng `CourseService.Add()` và xuất hiện trong danh sách khi ứng dụng đang chạy.

### 6. Tag Helpers và Model Binding

- Dùng Anchor Tag Helpers trong layout, card, search, create và detail.
- Dùng Form Tag Helpers trong Search và Create.
- Model Binding từ Query String: `keyword`, `category`, `theme`.
- Model Binding từ Form POST: `CourseCreateViewModel`.

## Chức năng từ Lab02 vẫn giữ lại

- Trang chủ giới thiệu hệ thống.
- Trang chi tiết khóa học: `/Courses/Detail/{id}`.
- Trang thống kê: `/Courses/Stats`.
- Action trả về text bằng `Content()`: `/Courses/Welcome`.
- Action trả về JSON bằng `Json()`: `/Courses/CourseJson`.
- Action chuyển hướng bằng `RedirectToAction()`: `/Courses/GoToList`.
- Action xử lý không tìm thấy dữ liệu bằng `NotFound()`: `/Courses/Detail/999` và `/Courses/Force404`.

## Tính năng mở rộng đã làm thêm

- Dark/Light theme switcher bằng query string `theme`.
- Giao diện card hiện đại cho danh sách khóa học.
- Thống kê nâng cao: tổng doanh thu, tổng lượt học viên, giảng viên tiêu biểu, tỷ lệ lấp đầy trung bình.
- Thống kê theo danh mục chuyên ngành: số lớp, số học viên, sức chứa, doanh thu, tỷ lệ lấp đầy.
- Trạng thái nghiệp vụ cho khóa học: Đã đầy lớp, Sắp kín chỗ, Lớp vắng học viên, Còn chỗ đăng ký.
- Tách kết quả tìm kiếm thành partial riêng.
- Hiển thị box cảnh báo khi không tìm thấy khóa học phù hợp.

## Các URL kiểm thử

```text
/
/Courses
/Courses?theme=dark
/Courses/Detail/1
/Courses/Detail/999
/Courses/Stats
/Courses/Search
/Courses/Search?keyword=data
/Courses/Search?category=Marketing
/Courses/Create
/Courses/Welcome
/Courses/CourseJson
/Courses/GoToList
/Courses/Force404
/Courses/CategoryInfo
```

## Hướng dẫn chạy project

Mở terminal tại thư mục chứa file `.csproj`:

```powershell
cd E:\Nam_4\Hocki2\ASP_vscode\asp-lab03\MiniCourseCatalog.Mvc
dotnet run
```

Sau đó mở URL được hiển thị trong terminal, ví dụ:

```text
http://localhost:5063
```

## Ghi chú

- Dữ liệu hiện đang lưu trong `List<Course>` hard-code bên trong `CourseService`, phù hợp cho Lab03.
- Khi tắt ứng dụng, dữ liệu thêm mới trong phiên chạy hiện tại sẽ mất vì chưa kết nối database.
- Nếu trình duyệt chưa cập nhật CSS/HTML mới, nhấn `Ctrl + F5` để refresh mạnh.

// Hàm load dữ liệu phân trang
function loadPage(controller, page, pageSize) {
    $.ajax({
        url: `/Admin/${controller}/LoadTable/`,
        type: 'GET',
        data: { page: page, pageSize: pageSize },
        beforeSend: function () {
            // Hiện preloader
            $("#preloader").show();
            $("#table-container").hide(); // ẩn bảng cũ trong lúc loading
        },
        success: function (data) {
            $("#table-container").html(data).fadeIn();
        },
        complete: function () {
            // Ẩn preloader khi Ajax xong
            $("#preloader").fadeOut();
        },
        error: function () {
            alert("Có lỗi xảy ra khi tải dữ liệu!");
            $("#preloader").fadeOut();
        }
    });
}

// Mở form modal (Create/Edit)
function openForm(controller,formname, id) {
    $.get(`/Admin/${controller}/Form`, { id: id }, function (res) {
        $("#ModalBody").html(res); // đổi thành container modal chung
        $(formname).modal('show');
    });
}

// Xoá 1 bản ghi
function deleteItem(controller, id) {
    if (confirm("Bạn có chắc chắn muốn xóa bản ghi này không?")) {
        $.ajax({
            url: `/Admin/${controller}/Delete/${id}`,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    alert(res.message || "Xóa thành công!");
                    loadPage(controller, 1, 10);
                } else {
                    alert(res.message || "Có lỗi xảy ra!");
                }
            },
            error: function (xhr, status, error) {
                alert("Có lỗi xảy ra: " + error);
            }
        });
    }
}

// Xoá nhiều bản ghi
function deleteSelected(controller, containerSelector) {
    // Tìm tất cả checkbox trong container được chọn
    var ids = $(containerSelector).find('.rowCheckbox:checked').map(function () {
        return $(this).data('id'); // hoặc $(this).val() nếu bạn dùng value
    }).get();
    console.log(ids);
    if (ids.length === 0) {
        alert("Vui lòng chọn ít nhất một bản ghi!");
        return;
    }

    if (confirm("Bạn có chắc chắn muốn xóa các bản ghi đã chọn không?")) {
        $.ajax({
            url: `/Admin/${controller}/DeleteSelected`,
            type: 'POST',
            traditional: true, // để gửi mảng
            data: { request: ids },
            success: function (response) {
                if (response.success) {
                    // Sau khi xóa thì load lại bảng
                    loadPage(controller, 1, 10);
                    alert(response.message || "Xóa thành công!");
                } else {
                    alert("Xóa thất bại: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                alert("Có lỗi xảy ra: " + error);
            }
        });
    }
}

// Lưu dữ liệu (Create/Update)
function saveItem(controller, formSelector, modalSelector) {
    var dataObj = {};
    $(formSelector).serializeArray().forEach(function (item) {
        dataObj[item.name] = item.value;
    });
    if (!dataObj.Id) dataObj.Id = 0;
    $.ajax({
        url: `/Admin/${controller}/Save`,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(dataObj),
        success: function (res) {
            if (res.success) {
                $(modalSelector).modal('hide');
                // alert(res.message || "Lưu thành công!");
                loadPage(controller, 1, 10);
            } else {
                alert(res.message || "Có lỗi xảy ra!");
            }
        },
        error: function () {
            alert("Lỗi khi gửi dữ liệu!");
        }
    });
}

// Search
let debounceTimer;

function searchItem(controller, inputId, tableContainer = "#table-container") {
    const keyword = $(inputId).val();

    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(function () {
        $.ajax({
            url: `/Admin/${controller}/Search`,
            type: 'GET',
            data: { searchName: keyword },
            success: function (data) {
                $(tableContainer).html(data);
            },
            error: function () {
                console.error("Search error");
            }
        });
    }, 300); // delay 300ms
}

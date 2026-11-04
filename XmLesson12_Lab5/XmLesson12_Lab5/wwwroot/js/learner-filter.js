// Lab 5: chỉ thay bảng người học khi chọn chuyên ngành ở chế độ AJAX.
$(function () {
    const filter = $("#major-filter");
    if (filter.attr("data-use-ajax") !== "true") {
        return;
    }

    const content = $("#content");
    const status = $("#filter-status");
    const error = $("#filter-error");
    let pendingRequest;

    filter.on("click", "a[data-major-id]", function (event) {
        // Giữ thao tác mở liên kết trong tab mới của trình duyệt.
        if (event.ctrlKey || event.metaKey || event.shiftKey || event.altKey || event.which > 1) {
            return;
        }
        event.preventDefault();

        if (pendingRequest) {
            pendingRequest.abort();
        }

        const link = $(this);
        const majorId = link.attr("data-major-id");
        error.prop("hidden", true);
        content.attr("aria-busy", "true");
        status.text("Đang tải danh sách…");

        pendingRequest = $.ajax({
            url: filter.attr("data-filter-url"),
            type: "GET",
            data: majorId ? { mid: majorId } : {},
            dataType: "html"
        }).done(function (response) {
            content.html(response);
            filter.find("a[data-major-id]").removeClass("active").removeAttr("aria-current");
            link.addClass("active").attr("aria-current", "page");
            status.text("Đã hiển thị: " + link.text().trim() + ".");

            // Khi tải lại hoặc đổi chế độ, giữ chuyên ngành vừa chọn.
            window.history.replaceState(null, "", link.attr("href"));
            $("a[data-load-mode]").each(function () {
                const modeLink = new URL(this.href);
                if (majorId) {
                    modeLink.searchParams.set("mid", majorId);
                } else {
                    modeLink.searchParams.delete("mid");
                }
                this.href = modeLink.toString();
            });
        }).fail(function (_, textStatus) {
            if (textStatus !== "abort") {
                status.text("");
                error.text("Không tải được danh sách. Hãy thử lại hoặc chọn chế độ Tải lại trang.")
                    .prop("hidden", false);
            }
        }).always(function () {
            content.attr("aria-busy", "false");
        });
    });
});

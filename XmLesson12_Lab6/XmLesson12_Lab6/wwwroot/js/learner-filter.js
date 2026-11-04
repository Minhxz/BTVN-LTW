// Lab6: cùng một action xử lý tìm kiếm, lọc chuyên ngành và phân trang.
$(function () {
    const filter = $('#major-filter');
    if (filter.attr('data-use-ajax') !== 'true') return;

    const content = $('#content');
    const form = $('#learner-search');
    const status = $('#filter-status');
    const error = $('#filter-error');
    let pendingRequest;

    function isPlainClick(event) {
        return !event.ctrlKey && !event.metaKey && !event.shiftKey && !event.altKey && event.which <= 1;
    }

    function setParam(url, key, value) {
        if (value) url.searchParams.set(key, value);
        else url.searchParams.delete(key);
    }

    function syncControls(url) {
        const result = content.find('#learner-results');
        const mid = result.attr('data-mid') || '';
        const keyword = result.attr('data-keyword') || '';
        const page = result.attr('data-page-index');
        setParam(url, 'mid', mid);
        setParam(url, 'keyword', keyword);
        url.searchParams.set('pageIndex', page);
        window.history.replaceState(null, '', url.toString());
        form.find('[name=mid]').val(mid);
        form.find('[name=keyword]').val(keyword);

        filter.find('a[data-major-id]').each(function () {
            const major = $(this).attr('data-major-id');
            const href = new URL(this.href);
            setParam(href, 'keyword', keyword);
            href.searchParams.delete('pageIndex');
            this.href = href.toString();
            $(this).toggleClass('active', major === mid);
            if (major === mid) $(this).attr('aria-current', 'page');
            else $(this).removeAttr('aria-current');
        });
        $('a[data-load-mode]').each(function () {
            const href = new URL(this.href);
            setParam(href, 'mid', mid);
            setParam(href, 'keyword', keyword);
            href.searchParams.set('pageIndex', page);
            this.href = href.toString();
        });
        status.text('Đã tải trang ' + page + ' · ' + result.attr('data-total-count') + ' người học.');
    }

    function loadResults(url) {
        if (pendingRequest) pendingRequest.abort();
        error.prop('hidden', true);
        content.attr('aria-busy', 'true');
        status.text('Đang tải danh sách…');
        pendingRequest = $.ajax({
            url: filter.attr('data-filter-url'),
            type: 'GET',
            dataType: 'html',
            data: {
                mid: url.searchParams.get('mid') || '',
                keyword: url.searchParams.get('keyword') || '',
                pageIndex: url.searchParams.get('pageIndex') || 1,
                useAjax: true
            }
        }).done(function (response) {
            content.html(response);
            syncControls(url);
        }).fail(function (_, textStatus) {
            if (textStatus !== 'abort') {
                status.text('');
                error.text('Không tải được danh sách. Hãy thử lại hoặc chọn Tải lại trang.').prop('hidden', false);
            }
        }).always(function () {
            content.attr('aria-busy', 'false');
        });
    }

    filter.on('click', 'a[data-major-id]', function (event) {
        if (!isPlainClick(event)) return;
        event.preventDefault();
        loadResults(new URL(this.href));
    });
    // Phân trang nằm trong partial nên dùng delegated event sau mỗi lần thay bảng.
    content.on('click', 'a[data-page-index]', function (event) {
        if (!isPlainClick(event)) return;
        event.preventDefault();
        loadResults(new URL(this.href));
    });
    form.on('submit', function (event) {
        event.preventDefault();
        const url = new URL(this.action);
        url.search = new URLSearchParams(new FormData(this)).toString();
        loadResults(url);
    });
});
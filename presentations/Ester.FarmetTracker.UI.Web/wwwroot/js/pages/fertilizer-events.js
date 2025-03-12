var fertilizerEvents = {
    loadDetail: function (fertilizerId) {
        get("/fertilizerhistory/createpartial?fertilizerId=" + fertilizerId, null, function (data) {
            $('#dynamic-content').html(data);
        });
    },
    loadActionDetail: function (action) {
        debugger;
        get("/fertilizerhistory/createdetailpartial?type=" + action, null, function (data) {
            $('#action-content').html(data);
        });
    }
}

$(document).on('click', '[data-fertilizer-history]', function () {
    fertilizerEvents.loadDetail($(this).data('fertilizer-history'));
});

$(document).on('change', '#Action', function () {
    fertilizerEvents.loadActionDetail($(this).val());
});
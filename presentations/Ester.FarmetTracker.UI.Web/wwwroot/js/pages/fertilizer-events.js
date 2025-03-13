var fertilizerEvents = {
    loadDetail: function (fertilizerId) {
        get("/fertilizerhistory/createpartial?fertilizerId=" + fertilizerId, null, function (data) {
            $('#dynamic-content').html(data);
        });
    },
    loadActionDetail: function (action) {
        get("/fertilizerhistory/createdetailpartial?type=" + action, null, function (data) {
            $('#action-content').html(data);
            pageEvents.setDynamicDropdowns();
        });
    },
    saveHistory: function (form) {
        debugger;

        post("/fertilizerhistory/create", pageEvents.formToJSON(form), function (data) {
            if (data == "OK") {
                notificationEvents.showInfo("İşlem Başarılı.");
            }
        });
    }
}

$(document).on('click', '[data-fertilizer-history]', function () {
    fertilizerEvents.loadDetail($(this).data('fertilizer-history'));
});

$(document).on('change', '#Action', function () {
    fertilizerEvents.loadActionDetail($(this).val());
});

$(document).on('click', '#save-history', function () {
    fertilizerEvents.saveHistory($(this).closest('form'));
});


$(document).on('change', '#recipient', function (e) {
    $('#recipient-name').val($("#recipient option:selected").text());

});

$(document).on('change', '#giver', function (e) {
    $('#giver-name').val($("#giver option:selected").text());
});
$(document).on('change', '#crop-selector', function (e) {
    $('#crop-name').val($("#crop-selector option:selected").text());
});

$(document).on('click', '[data-field]', function (e) {
    var action = $(this).attr('data-action');
    var field = $(this).attr('data-field');

    if (action == "crop") {
        get("/harvest/create?fieldId=" + field, null, function (data) {
            $('#harvest-content').html(data);
            pageEvents.setDynamicDropdowns();
        });
    }
    else if (action == "fertilizer") {
        get("/fertilizerhistory/throwpartial?fieldId=" + field + "&customerId=" + $(this).attr('data-customer'), null, function (data) {
            $('#harvest-content').html(data);
        });
    }
    else if (action == "harvest") {
        var messageOptions = {
            Type: "info",
            Title: "Hasat işlemi",
            Description: "Hasat yapılacak. Emin misiniz ?",
            ThenTrue: function () {
                window.location.href = "/harvest/complete?fieldId=" + field;
            },
            ThenFalse: function () {

            }
        };
        messageEvents.show(messageOptions);

    }

});





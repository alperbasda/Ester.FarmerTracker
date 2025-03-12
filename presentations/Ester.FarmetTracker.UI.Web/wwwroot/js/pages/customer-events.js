var customerEvents = {
    loadFields: function (customerId) {
        get("/field/customerfieldspartial?customerId=" + customerId, null, function (data) {
            $('#dynamic-content').html(data);
        });
    }
}

$(document).on('click', '[data-customer-field]', function () {
    customerEvents.loadFields($(this).data('customer-field'));
});
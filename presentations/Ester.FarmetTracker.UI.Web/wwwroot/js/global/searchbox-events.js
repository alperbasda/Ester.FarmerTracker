$(function () {
    $(".search_box").select2({
        ajax: {
            url: '/member/selectboxdata',
            dataType: 'json',
            data: function (params) {
                var query = {
                    search: params.term,
                    clientId: gridEvents.qsGetParams("ClientId")
                }
                // Query parameters will be ?search=[term]
                return query;
            },
            // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
            processResults: function (data) {
                
                // Transforms the top-level key of the response object from 'items' to 'results'
                return {
                    results: data.map(function (value, label) {
                        return {
                            "id": value.id,
                            "text": value.phoneNumber
                        };
                    })
                };
            },
        },
        delay: 250,
        minimumInputLength: 2,
        selectOnClose: true
    });
});
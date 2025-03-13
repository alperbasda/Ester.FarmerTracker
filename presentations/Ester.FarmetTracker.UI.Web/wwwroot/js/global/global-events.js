var pageEvents = {

    formToJSONExtractNull: function ($form) {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};

        $.map(unindexed_array, function (n, i) {
            if (n['value'] != undefined && n['value'] != '' && n['value'] != null) {
                indexed_array[n['name']] = n['value'];
            }

        });

        return indexed_array;
    },
    JSONToFormIfInputEmpty: function (form, obj, nestedName) {

    },

    formToJSON: function ($form) {
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};

        $.map(unindexed_array, function (n, i) {
            indexed_array[n['name']] = n['value'];
        });

        return indexed_array;
    },
    JSONToform: function (form, obj) {
        for (var prop in obj) {
            $(form).find('[name="' + prop + '"]').val(obj[prop]);
        }
    },
    loadPartials: function (callback) {

        var partialLength = $('[data-partial]').length;
        $('[data-partial]').each(function (index, item) {
            var url = $(item).data('partial');
            if (url && url.length > 0) {

                var qsParam = $(item).attr('data-qs-param');
                if (qsParam == undefined) {
                    qsParam = "";
                }

                pageEvents.setPartialQueryString(url, qsParam.split(','), function (urlWithQs) {

                    get(urlWithQs,
                        null,
                        function (response) {
                            $(item).replaceWith(response);
                            if (callback && partialLength == index + 1) {
                                callback();
                            }
                        });

                });


            }
        });
        if (partialLength == 0) {
            callback();
        }
    },

    setDynamicReferences: function () {

        var grouped = groupBy($('[data-fill-controller]').toArray(), w => $(w).attr('data-fill-controller'));
        grouped.forEach((item, key) => {

            var service = $(item[0]).attr('data-service');
            var filterProp = $(item[0]).attr('data-filter-prop');
            var showProp = $(item[0]).attr('data-filter-show');
            
            post("/dropdown/getdatabyids",
                {
                    ids: item.map(q => $(q).attr('data-fill-ref')),
                    endpoint: key,
                    service: service,
                    filterProp: filterProp,
                    showProp: showProp
                },
                function (response) {
                    
                    var datas = JSON.parse(response);
                    for (index in datas) {
                        $('[data-fill-ref="' + datas[index].id.toUpperCase() + '"]').html(datas[index].text);
                    }
                });
        });
    },

    setDynamicDropdowns: function () {
        $('[data-dynamic-for]').each(function (index, item) {
            var url = $(item).attr('data-dynamic-for');

            if (url && url.length > 0) {

                var service = $(item).attr('data-service');
                var qsParam = $(item).attr('data-qs-param');
                var filterProp = $(item).attr('data-prop');
                var showProp = $(item).attr('data-show-prop');
                if (qsParam == undefined) {
                    qsParam = "";
                }

                pageEvents.setPartialQueryString(url, qsParam.split(','), function (urlWithQs) {

                    $(item).select2({

                        //if item has parent
                        dropdownParent: $(item).attr('data-parent'),

                        ajax: {
                            url: getBaseUrl() + "/dropdown/getdata",
                            dataType: 'json',
                            data: function (params) {
                                var query = {
                                    searchterm: params.term,
                                    endpoint: url,
                                    service: service,
                                    filterProp: filterProp,
                                    showProp: showProp
                                };

                                // Query parameters will be ?searchterm=[term]
                                return query;
                            },
                            // Additional AJAX parameters go here; see the end of this chapter for the full code of this example
                            processResults: function (data) {
                                var parsedData = JSON.parse(data);
                                // Transforms the top-level key of the response object from 'items' to 'results'
                                return {

                                    results: parsedData.map(function (value, label) {


                                        return {

                                            "id": value.id,
                                            "text": value.text

                                        };


                                    })
                                };
                            },
                            delay: 250,
                        },
                        placeholder: 'Veri Seçin',
                        minimumInputLength: $(item).attr('data-min-length') != undefined
                            ? $(item).attr('data-min-length')
                            : 2,
                        templateSelection: formatState,
                        allowClear: true,
                        matcher: matchStart,
                        tags: true


                    });
                    $(item).removeAttr('data-qs-param');
                    $(item).removeAttr('data-parent');
                    $(item).removeAttr('data-ep');
                    $(item).removeAttr('data-service');
                });






            }
        });

    },
    setPartialQueryString: function (url, qsParams, callback) {
        var newQs = qsParams.map(w => w + "=" + gridEvents.qsGetParams(w));

        var data = newQs.filter(function (element) {
            return element !== undefined && element !== '=null';
        });
        if (data.length == 0) {
            callback(url);
        }
        else {
            if (url.indexOf('?') != -1) {
                callback(url + '&' + newQs.join('&'));
            }
            else {
                callback(url + '?' + newQs.join('&'));
            }

        }

    },
    setTooltips: function () {
        $('[data-bs-toggle="tooltip"]').tooltip();
    }
}

$('form[data-async-form]').on('submit', function (e) {
    e.stopPropagation();
    e.preventDefault();
});

function formatState(state) {
    if (state.text.trim() != "") {
        return state.text;
    }

    var id = state.id.toUpperCase();
    var querySelector = document.querySelector('[data-fill-ref="' + id + '"]');
    var controller = "";
    if (querySelector != null) {
        controller = querySelector.dataset.fillController;
        var $state = $(
            '<span data-fill-controller="' + controller + '" data-fill-ref="' + id + '"> </span>'
        );
        pageEvents.setDynamicReferences();

    }

    // Use .text() instead of HTML string concatenation to avoid script injection issues
    return $state;
};

function matchStart(params, data) {
    // If there are no search terms, return all of the data
    if ($.trim(params.term) === '') {
        return data;
    }

    // Skip if there is no 'children' property
    if (typeof data.children === 'undefined') {
        return null;
    }

    // `data.children` contains the actual options that we are matching against
    var filteredChildren = [];
    $.each(data.children, function (idx, child) {
        if (child.text.toUpperCase().indexOf(params.term.toUpperCase()) == 0) {
            filteredChildren.push(child);
        }
    });

    // If we matched any of the timezone group's children, then set the matched children on the group
    // and return the group object
    if (filteredChildren.length) {
        var modifiedData = $.extend({}, data, true);
        modifiedData.children = filteredChildren;

        // You can return modified objects from here
        // This includes matching the `children` how you want in nested data sets
        return modifiedData;
    }

    // Return `null` if the term should not be displayed
    return null;
}
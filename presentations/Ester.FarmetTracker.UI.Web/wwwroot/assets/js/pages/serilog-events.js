


var serilogEvents = {
    deleteEmptyInputs: function () {
        $('[type="radio"]').each(function () {

            if (!$(this).is(':checked')) {
                
                $($('[id="' + $(this).attr("href").split('#')[1] + '"]').find('input')).each(function (index,item) {
                    $(item).val('');
                });
                
            }
        });
    }


}

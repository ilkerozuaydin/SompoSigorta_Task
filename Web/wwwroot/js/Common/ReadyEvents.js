
$(document).ajaxSend(function (event, xhr, options) {
    showPageLoading(true);
});

$(document).ajaxSuccess(function (event, request, settings) {

    var readyState = request.readyState;
    if (readyState === 4) {
        if (request.responseJSON !== undefined) {
            if (request.status === 200) {
                if (settings.successtype === SUCCESS_TYPES.NOTIFY) {
                    $.notify("İşlem Başarılı", "success");
                }
            }
        }
    } else {
        $.notify("Sistem Hatası", "danger")
    }
});

$(document).ajaxComplete(function (event, request, settings) {

    //tüm ajax işlemlerinin bitiş anında burası çalışır
    //console.log("ajax tamamlandı");
    if (request.status !== 200) {

        $.notify("Sistem Hatası", "danger")
    }
    showPageLoading(false);

});


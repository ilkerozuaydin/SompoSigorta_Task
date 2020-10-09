
var AjaxFunctionGET = function (data, url, contentType, successType) {
    return AjaxFunction("GET", data, url, contentType, successType);
};

var AjaxFunctionPOST = function (data, url, contentType, successType) {
    return AjaxFunction("POST", data, url, contentType, successType);
};

function AjaxFunction(metod, data, url, contentType, successtype) {
    if (contentType === undefined || contentType.length > 0) {
        contentType = CONTENT_TYPES.JSON;
    }

    var resultData = data;
    switch (contentType) {
        case CONTENT_TYPES.JSON:
            resultData = JSON.stringify(data);
            break;
        default:
    }

    var fnx = function () {
        var deferredObject = $.Deferred();
        $.ajax({
            type: metod,
            data: resultData,
            contentType: contentType,
            successtype: successtype,
            url: url,
            success: function (result) {
                deferredObject.resolve(result);
            },
            error: function (e) {
                deferredObject.resolve(e);
            }

        });

        return deferredObject.promise();
    };
    return fnx;
}

function permissionControl(permissionArray) {
    var result = true;
    $.each(ACCOUNT_PERMISSIONS, function (index, value) {

        if ($.inArray(value, permissionArray) !== -1) {
            result = true;
        }

        if (result) {
            return false; //break
        }
    });
    return result;
}

function showPageLoading(status) {
    $("#loading").toggle();
}

function groupArrayOfObjects(list, key) {
    return list.reduce(function (rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
    }, {});
};

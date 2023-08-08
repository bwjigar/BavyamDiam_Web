var validator = $('#frmOfferCre').validate({ // initialize the plugin
    ignore: "",
    rules: {
        txtOfferPer: {
            required: true,
            maxlength: 6
        }
    },
    messages: {
        txtOfferPer: {
            required: "Enter Offer (%)",
            maxlength: "Enter maximum 3 number Offer (%)"
        },
    },
    errorElement: "div",
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function GetOfferPer() {
    $.ajax({
        url: "/Offer/GetOfferCriteria",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $('#txtOfferPer').val(data.Data[0].OfferPer);
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
$(document).ready(function () {
    GetOfferPer();
    $('#btnSave').click(function () {
        var isvalid = $('#frmOfferCre').valid();
        if (!isvalid) {
            return false;
        }
        var obj = {
            OfferPer: $('#txtOfferPer').val()
        };
        if (isvalid) {
            $.ajax({
                url: "/Offer/SaveOfferCriterias",
                type: "POST",
                data: obj,
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "1") {
                        toastr.success(data.Message);
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    });

});
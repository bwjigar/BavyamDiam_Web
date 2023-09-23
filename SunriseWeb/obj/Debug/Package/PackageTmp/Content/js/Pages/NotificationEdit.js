var ddlCountryObj = null;
function GetCountries() {
    $("#ddlCountry").html("");
    $.ajax({
        url: "/Common/CountryList",
        async: false,
        type: "POST",
        //data: formData,
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {
            if (data.Status != undefined) {
                var list = data.Data, tot = list.length, i = 0;
                if (list != null) {
                    for (; i < tot; i++) {
                        $("#ddlCountry").append("<option value='" + list[i].iCountryId + "'>" + list[i].sCountryName + "</option>");
                    }
                    ddlCountryObj = $('#ddlCountry').multiselect({
                        includeSelectAllOption: true
                    });
                }
                loaderHide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
            loaderHide();
        }
    });
}

$(document).ready(function () {
    $('#txtDate').daterangepicker({
        singleDatePicker: true,
        //startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    });
    GetCountries();
});
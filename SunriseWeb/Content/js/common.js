var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

function isPositiveNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode == 46)
        return true;
    else {
        toastr.warning($("#hdn_Enter_only_positive_numbers").val() + " !");
        return false;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        toastr.warning($("#hdn_Please_Enter_Only_Number_only").val() + " !");
        return false;
    }

    return true;
}

function formatNumberWithPoint(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

function greaterThanDate(evt) {
    var fDate = $.trim($('#txtFromDate').val());
    var tDate = $.trim($('#txtToDate').val());
    if (fDate != "" && tDate != "") {
        if (new Date(tDate) >= new Date(fDate)) {
            return true;
        }
        else {
            evt.currentTarget.value = "";
            toastr.warning($("#hdn_To_date_must_be_greater_than_From_date").val() + " !");
            return false;
        }
    }
    else {
        return true;
    }
}

$(document).ready(function () {

    $('.commonFromRange').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDate(e);
    });

    $('.commonToRange').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10),
    }).on('change', function (e) {
        greaterThanDate(e);
    });

});
var GridpgSize = 50;
var gridOptions = {};
var IsObj = false, samitIsObj = false;
var searchSummary = {};
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var rowData = [];
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 6));
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");
var DisValPPC_Array = [];

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}

if ($('#hdnisadminflg').val() == 1 || $('#hdn_iUserid_41').val() == 1) {
    IsObj = false;
} else {
    IsObj = true;
}
if ($("#hdn_samit").val() == 1) {
    samitIsObj = false;
} else {
    samitIsObj = true;
}
//function SupplierStatus_ddlIndicator(params) {
//    var element = document.createElement("span");
//    element.title = '';
//    if (params.data.ByRequestStatus == "Approved") {
//        var SupplierStatus = "";
//        if (DisValPPC_Array.length > 0) {
//            for (var i = 0; i < DisValPPC_Array.length; i++) {
//                if (DisValPPC_Array[i][0] == params.data.Userid && DisValPPC_Array[i][1] == params.data.REF_NO) {
//                    SupplierStatus = (DisValPPC_Array[i][2]);
//                }
//            }
//        }
//        element.innerHTML = '<select onchange="SupplierStatusCal(this);"' +
//            'class="SupplierStatus supp-status"' +
//            'id="' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
//            'name="' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
//            'RAP_VALUE="' + parseFloat(params.data.RAP_VALUE) + '"' +
//            'CTS="' + parseFloat(params.data.CTS) + '"' +
//            'Userid="' + params.data.Userid + '"' +
//            'REF_NO="' + params.data.REF_NO + '"> ' +
//            '<option value="0">Select</option>' +
//            '<option value="Confirm" ' + (SupplierStatus == "Confirm"? 'selected': '') + '>Confirm</option>' +
//            '<option value="Hold" ' + (SupplierStatus == "Hold" ? 'selected' : '') + '>Hold</option>' +
//            '<option value="Bidded" ' + (SupplierStatus == "Bidded" ? 'selected' : '') + '>Bidded</option>' +
//            '<option value="Waiting" ' + (SupplierStatus == "Waiting" ? 'selected' : '') + '>Waiting</option>' +
//            '<option value="Qc Pending" ' + (SupplierStatus == "Qc Pending" ? 'selected' : '') + '>Qc Pending</option>' +
//            '<option value="Qc Reject" ' + (SupplierStatus == "Qc Reject" ? 'selected' : '') + '>Qc Reject</option>' +
//            '<option value="Bid Reject" ' + (SupplierStatus == "Bid Reject" ? 'selected' : '') + '>Bid Reject</option>' +
//            '<option value="Sold" ' + (SupplierStatus == "Sold" ? 'selected' : '') + '>Sold</option>' +
//            '<option value="Other" ' + (SupplierStatus == "Other" ? 'selected' : '') + '>Other</option>' +
//            '<option value="Transit" ' + (SupplierStatus == "Transit" ? 'selected' : '') + '>Transit</option>' +
//            '<option value="Cancel" ' + (SupplierStatus == "Cancel" ? 'selected' : '') + '>Cancel</option>' +
//            '</select>';
//    }
//    //element.appendChild(document.createTextNode(params.value));
//    return element;
//}
function CostDisc_txtIndicator(params) {
    var element = document.createElement("span");
    element.title = '';
    if (params.data.ByRequestStatus == "Approved") {
        var CostDisc = "";
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == params.data.Userid && DisValPPC_Array[i][1] == params.data.REF_NO) {
                    CostDisc = (DisValPPC_Array[i][2]);
                }
            }
        }
        if (Supp_Status == false) {
            element.innerHTML = '<input type="text"' +
                'value="' + CostDisc + '"' +
                'id="CostDisc' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'name="CostDisc' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'RAP_VALUE="' + parseFloat(params.data.RAP_VALUE) + '"' +
                'CTS="' + parseFloat(params.data.CTS) + '"' +
                'Userid="' + params.data.Userid + '"' +
                'REF_NO="' + params.data.REF_NO + '"' +
                'onkeypress="return isNumberKeyWithNegative(event)"' +
                'class="supp-status CostDisc"' +
                'onblur="CostDiscCal(this);"> ';
        }
        else {
            element.innerHTML = "<span style='font-weight:600;color:#003d66;text-align:center;font-size:11px;'>" + formatNumber(CostDisc) + "</span>";
        }
    }
    return element;
}
function CostValue_txtIndicator(params) {
    var element = document.createElement("span");
    element.title = '';
    if (params.data.ByRequestStatus == "Approved") {
        var CostValue = "";
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == params.data.Userid && DisValPPC_Array[i][1] == params.data.REF_NO) {
                    CostValue = (DisValPPC_Array[i][3]);
                }
            }
        }

        if (Supp_Status == false) {
            element.innerHTML = '<input type="text"' +
                'value="' + CostValue + '"' +
                'id="CostValue' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'name="CostValue' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'RAP_VALUE="' + parseFloat(params.data.RAP_VALUE) + '"' +
                'CTS="' + parseFloat(params.data.CTS) + '"' +
                'Userid="' + params.data.Userid + '"' +
                'REF_NO="' + params.data.REF_NO + '"' +
                'onkeypress="return isNumberKeyWithNegative(event)"' +
                'class="supp-status CostValue"' +
                'onblur="CostValueCal(this);" > ';
        }
        else {
            element.innerHTML = "<span style='font-weight:600;color:#003d66;text-align:center;font-size:11px;'>" + formatNumber(CostValue) + "</span>";
        }
    }
    return element;
}
function CostPriceCts_txtIndicator(params) {
    var element = document.createElement("span");
    element.title = '';
    if (params.data.ByRequestStatus == "Approved") {
        var CostPriceCts = "";
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == params.data.Userid && DisValPPC_Array[i][1] == params.data.REF_NO) {
                    CostPriceCts = (DisValPPC_Array[i][4]);
                }
            }
        }

        if (Supp_Status == false) {
            element.innerHTML = '<input type="text"' +
                'value="' + CostPriceCts + '"' +
                'id="CostPriceCts' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'name="CostPriceCts' + params.data.Userid + '' + params.data.PARTY_STONE_NO + '' + params.data.CERTI_NO + '"' +
                'RAP_VALUE="' + parseFloat(params.data.RAP_VALUE) + '"' +
                'CTS="' + parseFloat(params.data.CTS) + '"' +
                'Userid="' + params.data.Userid + '"' +
                'REF_NO="' + params.data.REF_NO + '"' +
                'onkeypress="return isNumberKeyWithNegative(event)"' +
                'class="supp-status CostPriceCts"' +
                'onblur="CostPriceCtsCal(this);">';
        }
        else {
            element.innerHTML = "<span style='font-weight:600;color:#003d66;text-align:center;font-size:11px;'>" + formatNumber(CostPriceCts) + "</span>";
        }
    }
    return element;
}
//function Apply_btnIndicator(params) {
//    var element = document.createElement("span");
//    element.title = '';
//    if (params.data.ByRequestStatus == "Approved") {
//        element.innerHTML = '<button style="width: 51px;height: 22px !important;margin-top: 2px;font-size: 11px;font-weight: 700;padding: 0px 0px 0px 0px;-webkit-box-shadow: none;"' +
//            'Userid="' + params.data.Userid + '"' +
//            'REF_NO="' + params.data.REF_NO + '"' +
//            'onclick="SaveApply(this)"' +
//            'class="offer-btn"> APPLY</button>';
//    }
//    return element;
//}
function SupplierStatusCal(e) {
    //var SupplierStatus = $(e).parent().parent().parent().find('.SupplierStatus').val();
    var CostDisc = $(e).parent().parent().parent().find('.CostDisc').val();
    var CostValue = $(e).parent().parent().parent().find('.CostValue').val();
    var CostPriceCts = $(e).parent().parent().parent().find('.CostPriceCts').val();
    var RAP_VALUE = parseFloat($(e).attr("RAP_VALUE"));
    var CTS = parseFloat($(e).attr("CTS"));

    var exist = false;
    if (DisValPPC_Array.length > 0) {
        for (var i = 0; i < DisValPPC_Array.length; i++) {
            if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                //DisValPPC_Array[i][2] = SupplierStatus;
                DisValPPC_Array[i][2] = CostDisc;
                DisValPPC_Array[i][3] = CostValue;
                DisValPPC_Array[i][4] = CostPriceCts;
                exist = true;
            }
        }
    }
    if (exist == false) {
        //DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), SupplierStatus, CostDisc, CostValue, CostPriceCts]);
        DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), CostDisc, CostValue, CostPriceCts]);
    }
}
function CostDiscCal(e) {
    if ($(e).parent().parent().parent().find('.CostDisc').val() != "") {
        //var SupplierStatus = $(e).parent().parent().parent().find('.SupplierStatus').val();
        var CostDisc = $(e).parent().parent().parent().find('.CostDisc').val();
        var CostValue = $(e).parent().parent().parent().find('.CostValue').val();
        var CostPriceCts = $(e).parent().parent().parent().find('.CostPriceCts').val();
        var RAP_VALUE = parseFloat($(e).attr("RAP_VALUE"));
        var CTS = parseFloat($(e).attr("CTS"));

        var val, pricects;
        val = (RAP_VALUE * (100 - parseFloat(CostDisc)) / 100);
        pricects = (val / CTS);

        $(e).parent().parent().parent().find('.CostValue').val(val.toFixed(2));
        $(e).parent().parent().parent().find('.CostPriceCts').val(pricects.toFixed(2));

        var exist = false;
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    //DisValPPC_Array[i][2] = SupplierStatus;
                    DisValPPC_Array[i][2] = CostDisc;
                    DisValPPC_Array[i][3] = val.toFixed(2);
                    DisValPPC_Array[i][4] = pricects.toFixed(2);
                    exist = true;
                }
            }
        }
        if (exist == false) {
            //DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), SupplierStatus, CostDisc, val.toFixed(2), pricects.toFixed(2)]);
            DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), CostDisc, val.toFixed(2), pricects.toFixed(2)]);
        }
    }
    else {
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    DisValPPC_Array[i][3] = "";
                    DisValPPC_Array[i][4] = "";
                    DisValPPC_Array[i][5] = "";
                }
            }
        }
        $(e).parent().parent().parent().find('.CostDisc').val("");
        $(e).parent().parent().parent().find('.CostValue').val("");
        $(e).parent().parent().parent().find('.CostPriceCts').val("");
    }
}
function CostValueCal(e) {
    if ($(e).parent().parent().parent().find('.CostValue').val() != "") {
        //var SupplierStatus = $(e).parent().parent().parent().find('.SupplierStatus').val();
        var CostDisc = $(e).parent().parent().parent().find('.CostDisc').val();
        var CostValue = $(e).parent().parent().parent().find('.CostValue').val();
        var CostPriceCts = $(e).parent().parent().parent().find('.CostPriceCts').val();
        var RAP_VALUE = parseFloat($(e).attr("RAP_VALUE"));
        var CTS = parseFloat($(e).attr("CTS"));

        var disc, pricects;
        disc = ((1 - (parseFloat(CostValue) / RAP_VALUE)) * 100) * 1;
        pricects = (parseFloat(CostValue) / CTS);

        $(e).parent().parent().parent().find('.CostDisc').val(disc.toFixed(2));
        $(e).parent().parent().parent().find('.CostPriceCts').val(pricects.toFixed(2));

        var exist = false;
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    //DisValPPC_Array[i][2] = SupplierStatus;
                    DisValPPC_Array[i][2] = disc.toFixed(2);
                    DisValPPC_Array[i][3] = CostValue;
                    DisValPPC_Array[i][4] = pricects.toFixed(2);
                    exist = true;
                }
            }
        }
        if (exist == false) {
            //DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), SupplierStatus, disc.toFixed(2), CostValue, pricects.toFixed(2)]);
            DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), disc.toFixed(2), CostValue, pricects.toFixed(2)]);
        }
    }
    else {
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    DisValPPC_Array[i][3] = "";
                    DisValPPC_Array[i][4] = "";
                    DisValPPC_Array[i][5] = "";
                }
            }
        }
        $(e).parent().parent().parent().find('.CostDisc').val("");
        $(e).parent().parent().parent().find('.CostValue').val("");
        $(e).parent().parent().parent().find('.CostPriceCts').val("");
    }
}
function CostPriceCtsCal(e) {
    if ($(e).parent().parent().parent().find('.CostPriceCts').val() != "") {
        //var SupplierStatus = $(e).parent().parent().parent().find('.SupplierStatus').val();
        var CostDisc = $(e).parent().parent().parent().find('.CostDisc').val();
        var CostValue = $(e).parent().parent().parent().find('.CostValue').val();
        var CostPriceCts = $(e).parent().parent().parent().find('.CostPriceCts').val();
        var RAP_VALUE = parseFloat($(e).attr("RAP_VALUE"));
        var CTS = parseFloat($(e).attr("CTS"));

        var disc, val;
        val = (parseFloat(CostPriceCts) * CTS);
        disc = ((1 - (val / RAP_VALUE)) * 100) * 1;

        $(e).parent().parent().parent().find('.CostDisc').val(disc.toFixed(2));
        $(e).parent().parent().parent().find('.CostValue').val(val.toFixed(2));

        var exist = false;
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    //DisValPPC_Array[i][2] = SupplierStatus;
                    DisValPPC_Array[i][2] = disc.toFixed(2);
                    DisValPPC_Array[i][3] = val.toFixed(2);
                    DisValPPC_Array[i][4] = CostPriceCts;
                    exist = true;
                }
            }
        }
        if (exist == false) {
            //DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), SupplierStatus, disc.toFixed(2), val.toFixed(2), CostPriceCts]);
            DisValPPC_Array.push([$(e).attr("Userid"), $(e).attr("REF_NO"), disc.toFixed(2), val.toFixed(2), CostPriceCts]);
        }
    }
    else {
        if (DisValPPC_Array.length > 0) {
            for (var i = 0; i < DisValPPC_Array.length; i++) {
                if (DisValPPC_Array[i][0] == $(e).attr("Userid") && DisValPPC_Array[i][1] == $(e).attr("REF_NO")) {
                    DisValPPC_Array[i][3] = "";
                    DisValPPC_Array[i][4] = "";
                    DisValPPC_Array[i][5] = "";
                }
            }
        }
        $(e).parent().parent().parent().find('.CostDisc').val("");
        $(e).parent().parent().parent().find('.CostValue').val("");
        $(e).parent().parent().parent().find('.CostPriceCts').val("");
    }
}
var List = [];
function SaveApply() {
    var SelectedRows = gridOptions.api.getSelectedRows();
    List = [];
    if (SelectedRows.length != 0) {
        if (SelectedRows.length == (_.filter(SelectedRows, function (e) { return e.SupplierStatus != "Confirm" && e.ByRequestStatus == "Approved" }).length)) {
            if ($("#ddlSuppStatus").val() != 0) {
                $.each(SelectedRows, function (propName, propVal) {
                    if ($("#CostDisc" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val() == "") {
                        toastr.warning("Please enter Cost Disc(%)");
                        $("#CostDisc" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).focus();
                        return;
                    }
                    else if ($("#CostValue" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val() == "") {
                        toastr.warning("Please enter Cost Value");
                        $("#CostValue" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).focus();
                        return;
                    }
                    else if ($("#CostPriceCts" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val() == "") {
                        toastr.warning("Please enter Cost Price/Cts");
                        $("#CostPriceCts" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).focus();
                        return;
                    }
                    else {
                        List.push({
                            SupplierStatus: $("#ddlSuppStatus").val(),
                            CostDisc: $("#CostDisc" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val(),
                            CostValue: $("#CostValue" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val(),
                            CostPriceCts: $("#CostPriceCts" + propVal.Userid + '' + propVal.PARTY_STONE_NO + '' + propVal.CERTI_NO).val(),
                            REF_NO_Userid: propVal.Userid,
                            REF_NO: propVal.REF_NO,
                        });
                    }
                });

                if (List.length != 0) {
                    var obj = {};
                    obj.CostCharges = List;

                    $.ajax({
                        url: "/LabStock/ByRequest_Apply_Disc",
                        async: false,
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify({ req: obj }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data, textStatus, jqXHR) {
                            if (data.Status == "0") {
                                if (data.Message.indexOf('Something Went wrong') > -1) {
                                    MoveToErrorPage(0);
                                }
                                toastr.error(data.Message);
                            } else if (data.Status == "1") {
                                toastr.success(data.Message);
                                GetLabByRequest();
                            }
                            $('.loading-overlay-image-container').hide();
                            $('.loading-overlay').hide();
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            $('.loading-overlay-image-container').hide();
                            $('.loading-overlay').hide();
                        }
                    });
                }
            }
            else {
                toastr.warning("Please Select Supplier Status");
                return;
            }
        }
        else {
            toastr.warning("Please Select only Approved or Without 'Confirm' Supp.Status Buy Request");
            return;
        }
    }
    else {
        toastr.warning("Please Select Buy Request");
        return;
    }

    //var SupplierStatus = $(e).parent().parent().parent().find('.SupplierStatus').val();
    //var SupplierStatus = $("#ddlSuppStatus").val();
    //var CostDisc = $(e).parent().parent().parent().find('.CostDisc').val();
    //var CostValue = $(e).parent().parent().parent().find('.CostValue').val();
    //var CostPriceCts = $(e).parent().parent().parent().find('.CostPriceCts').val();
    //var Userid = $(e).attr("Userid");
    //var REF_NO = $(e).attr("REF_NO");

    //if (CostDisc == "" || CostValue == "" || CostPriceCts == "") {
    //    toastr.warning("Please fill all the value in Ref No " + REF_NO);
    //}
    //else if (SupplierStatus == "0") {
    //    toastr.warning("Please select Supplier Status");
    //}
    //else {
    //    var obj = {};
    //    obj.REF_NO = REF_NO;
    //    obj.Userid = Userid;
    //    obj.SupplierStatus = SupplierStatus;
    //    obj.CostDisc = CostDisc;
    //    obj.CostValue = CostValue;
    //    obj.CostPriceCts = CostPriceCts;

    //    $.ajax({
    //        url: "/LabStock/ByRequest_Apply_Disc",
    //        async: false,
    //        type: "POST",
    //        dataType: "json",
    //        data: JSON.stringify({ obj: obj }),
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data, textStatus, jqXHR) {
    //            if (data.Status == "0") {
    //                if (data.Message.indexOf('Something Went wrong') > -1) {
    //                    MoveToErrorPage(0);
    //                }
    //                toastr.error(data.Message);
    //            } else if (data.Status == "1") {
    //                toastr.success(data.Message);
    //                GetLabByRequest();
    //            }
    //            $('.loading-overlay-image-container').hide();
    //            $('.loading-overlay').hide();
    //        },
    //        error: function (jqXHR, textStatus, errorThrown) {
    //            $('.loading-overlay-image-container').hide();
    //            $('.loading-overlay').hide();
    //        }
    //    });
    //}
}
function isNumberKeyWithNegative(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 45)
        return true;

    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function selectAllRendererDetail(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');
    cb.setAttribute('id', 'checkboxAll');
    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            if (Filtered_Data.length > 0) {
                gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        }
        else {
            params.api.deselectAll();
            onSelectionChanged();
        }

    });

    return eHeader;
}

$(document).ready(function (e) {
    reset();
});
function Five_Day_Date_Set() {
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());

    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    }).on('change', function (e) {
        greaterThanDate(e);
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    }).on('change', function (e) {
        greaterThanDate(e);
    });
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
var gridDiv = document.querySelector('#myGrid');
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 35,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "Userid", field: "Userid", hide: true },
    { headerName: "Entry Date Time", field: "ByRequestEntryDate", tooltip: function (params) { return (params.value); }, width: 105, sortable: true },
    { headerName: "Entry User Name", field: "ByRequestUsername", tooltip: function (params) { return (params.value); }, width: 100, sortable: true, hide: IsObj },
    {
        headerName: "Status", field: "ByRequestStatus", width: 85, sortable: true
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.ByRequestStatus != "" && params.data.ByRequestStatus != null) {
                if (params.data.ByRequestStatus == "Approved") {
                    //return '<label style="color:green;font-weight:bold;">APPROVED</label>'
                    return '<p class="spn-Yes1">Approved</p>';
                }
                else if (params.data.ByRequestStatus == "Rejected") {
                    //return '<label style="color:red;font-weight:bold;">REJECTED</label>'
                    return '<p class="spn-No1">Rejected</p>';
                }
                else if (params.data.ByRequestStatus == "Pending") {
                    //return '<label style="color:black;font-weight:bold;">PENDING</label>'
                    return '<p class="spn-Pend1">Pending</p>';
                }
            }
            else {
                return '';
            }
        }
    },
    //{
    //    headerName: "Supplier Status",
    //    field: "",
    //    hide: samitIsObj,
    //    width: 115,
    //    cellRenderer: 'SupplierStatus_ddlIndicator',
    //    sortable: false,
    //    cellClass: ['co-input']
    //},
    { headerName: "Supplier Status", field: "SupplierStatus", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    {
        headerName: "Cost Disc(%)",
        field: "CostDisc",
        hide: samitIsObj,
        width: 100,
        cellRenderer: 'CostDisc_txtIndicator',
        sortable: false,
        cellClass: ['co-input']
    },
    {
        headerName: "Cost Value",
        field: "CostValue",
        hide: samitIsObj,
        width: 100,
        cellRenderer: 'CostValue_txtIndicator',
        sortable: false,
        cellClass: ['co-input']
    },
    {
        headerName: "Cost Price/Cts",
        field: "CostPriceCts",
        hide: samitIsObj,
        width: 100,
        cellRenderer: 'CostPriceCts_txtIndicator',
        sortable: false,
        cellClass: ['co-input']
    },
    //{
    //    headerName: "Make",
    //    field: "",
    //    hide: samitIsObj,
    //    width: 70,
    //    cellRenderer: 'Apply_btnIndicator',
    //    sortable: false,
    //    cellClass: ['but-input']
    //},
    {
        headerName: "Ref No", field: "REF_NO", tooltip: function (params) { return (params.value); }, width: 85, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Lab", field: "LAB", width: 40, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.CER_PATH != "" && params.data.CER_PATH != null) {
                return '<a href="' + params.data.CER_PATH + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.LAB + '</a>'
            }
            else if (params.data.CER_PATH == "" || params.data.CER_PATH == null) {
                return params.data.LAB;
            }
            else {
                return '';
            }
        }
    },
    {
        headerName: "HD Image", field: "IMG_PATH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.IMG_PATH != "" && params.data.IMG_PATH != null) {
                return '<ul class="flat-icon-ul"><li><a href="' + params.data.IMG_PATH + '" target="_blank" title="View Diamond Image">' +
                    '<img src="../Content/images/frame.svg" class="frame-icon"></a></li></ul>';
            }
            else {
                return '<ul class="flat-icon-ul"><li><a href="javascript:void(0);" title="View Diamond Image">' +
                    '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li></ul>';
            }
        }
    },
    {
        headerName: "HD Video", field: "VDO_PATH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.VDO_PATH != "" && params.data.VDO_PATH != null) {
                return '<ul class="flat-icon-ul"><li><a href="' + params.data.VDO_PATH + '" target="_blank" title="View Diamond Video">' +
                    '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li></ul>';
            }
            else {
                return '<ul class="flat-icon-ul"><li><a href="javascript:void(0);" title="View Diamond Video">' +
                    '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li></ul>';
            }
        }
    },
    {
        headerName: "Supplier Stock ID", field: "PARTY_STONE_NO", tooltip: function (params) { return (params.value); }, width: 80, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Certi No", field: "CERTI_NO", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Supplier", field: "PARTY_NAME", tooltip: function (params) { return (params.value); }, width: 220, sortable: false },
    { headerName: "Shape", field: "SHAPE", tooltip: function (params) { return (params.value); }, width: 120, sortable: false },
    { headerName: "Pointer", field: "POINTER", tooltip: function (params) { return (params.value); }, width: 65, sortable: false },
    { headerName: "BGM", field: "BGM", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Color", field: "COLOR", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Clarity", field: "PURITY", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    {
        headerName: "Cts", field: "CTS", tooltip: function (params) { return (params.value); }, width: 50, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Rap Rate", field: "RAP_PRICE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Rap Value", field: "RAP_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Supplier Cost Disc(%)", field: "OFFER_DISC_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ff99cc', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Supplier Cost Value", field: "OFFER_DISC_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ff99cc', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Sunrise Disc(%)", field: "SALES_DISC_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ccffff', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Sunrise Value US($)", field: "SALES_DISC_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ccffff', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Supp Base Offer(%)", field: "SUPP_BASE_OFFER_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Supp Base Offer Value", field: "SUPP_BASE_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Cut", field: "CUT", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.value == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: "Polish", field: "POLISH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.data.CUT == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: "Symm", field: "SYMM", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.data.CUT == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    { headerName: "Fls", field: "FLS", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    {
        headerName: "Length", field: "LENGTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Width", field: "WIDTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Depth", field: "DEPTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Depth(%)", field: "DEPTH_PER", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Table(%)", field: "TABLE_PER", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Key to Symbol", field: "SYMBOL", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    { headerName: "Gia Lab Comment", field: "COMMENTS", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    {
        headerName: "Girdle(%)", field: "GIRDLE_PER", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Crown Angle", field: "CROWN_ANGLE", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Crown Height", field: "CROWN_HEIGHT", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Pav Angle", field: "PAV_ANGLE", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Pav Height", field: "PAV_HEIGHT", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Table Natts", field: "TABLE_NATTS", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Natts", field: "CROWN_NATTS", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Table Inclusion", field: "TABLE_INCLUSION", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Inclusion", field: "CROWN_INCLUSION", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Culet", field: "CULET", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Table Open", field: "TABLE_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Girdle Open", field: "GIRDLE_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Open", field: "CROWN_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Pavilion Open", field: "PAV_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
];
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
var showEntryVar = "";
var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';
new WOW().init();
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    GridpgSize = Number(value);
    GetLabByRequest();
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbar = $(".navbar").height(),
        resultHei = $(".order-history-data").height(),
        contentHei = winH - (navbar + tabsmarkerHei + resultHei + 75);
    contentHei = (contentHei <= 100 ? 450 : contentHei);
    $("#myGrid").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetLabByRequest() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $("#ddlSuppStatus").val("0");
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 2,
        components: {
            //SupplierStatus_ddlIndicator: SupplierStatus_ddlIndicator,
            CostDisc_txtIndicator: CostDisc_txtIndicator,
            CostValue_txtIndicator: CostValue_txtIndicator,
            CostPriceCts_txtIndicator: CostPriceCts_txtIndicator,
            //Apply_btnIndicator : Apply_btnIndicator
        },
        defaultColDef: {
            enableValue: false,
            enableRowGroup: false,
            enableSorting: false,
            sortable: false,
            resizable: true,
            enablePivot: false,
            filter: true
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        onSelectionChanged: onSelectionChanged,
        onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize,
        paginationPageSize: GridpgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
    gridOptions.api.setServerSideDatasource(datasource1);
    setTimeout(function () {
        var allColumnIds = [];
        gridOptions.columnApi.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });

        //gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);

    var a = $('.ag-header-select-all')[0];
    $(a).removeClass('ag-hidden');
}
function onBodyScroll(params) {
    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
}
function onSelectionChanged(event) {
    var TOT_PCS = 0, TOT_CTS = 0, TOT_RAP_AMOUNT = 0, SALES_DISC_VALUE = 0, RAP_VALUE = 0, SALES_DISC_PER = 0
        , OFFER_DISC_VALUE = 0, OFFER_DISC_PER = 0, SUPP_BASE_VALUE = 0, SUPP_BASE_OFFER_PER = 0;

    if (gridOptions.api.getSelectedRows().length > 0) {
        TOT_PCS = gridOptions.api.getSelectedRows().length;
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CTS'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'RAP_VALUE'), function (memo, num) { return memo + num; }, 0);

        SALES_DISC_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SALES_DISC_VALUE'), function (memo, num) { return memo + num; }, 0);
        RAP_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'RAP_VALUE'), function (memo, num) { return memo + num; }, 0);
        SALES_DISC_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(SALES_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        OFFER_DISC_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'OFFER_DISC_VALUE'), function (memo, num) { return memo + num; }, 0);
        OFFER_DISC_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(OFFER_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        SUPP_BASE_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SUPP_BASE_VALUE'), function (memo, num) { return memo + num; }, 0);
        SUPP_BASE_OFFER_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(OFFER_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        $('#tab1Pcs').html(formatIntNumber(TOT_PCS));
        $('#tab1CTS').html(formatNumber(TOT_CTS));
        $('#tab1RapValue').html(formatNumber(TOT_RAP_AMOUNT));
        $('#tab1SunriseDiscPer').html(formatNumber(SALES_DISC_PER));
        $('#tab1SunriseValueUSDoller').html(formatNumber(SALES_DISC_VALUE));

        var stoneList = gridOptions.api.getSelectedRows();
        var Pending = _.filter(stoneList, function (e) { return e.ByRequestStatus == "Pending" });
        var Approved = _.filter(stoneList, function (e) { return e.ByRequestStatus == "Approved" });
        var Rejected = _.filter(stoneList, function (e) { return e.ByRequestStatus == "Rejected" });

        $("#li_Approve").hide();
        $("#li_Reject").hide();

        if (Pending.length == stoneList.length) {
            $("#li_Approve").show();
            $("#li_Reject").show();
        }
        else if (Approved.length == stoneList.length) {
            $("#li_Approve").hide();
            $("#li_Reject").hide();
        }
        else if (Rejected.length == stoneList.length) {
            $("#li_Approve").hide();
            $("#li_Reject").hide();
        }

    } else {
        $('#tab1Pcs').html(formatIntNumber(searchSummary.TOT_PCS));
        $('#tab1CTS').html(formatNumber(searchSummary.TOT_CTS));
        $('#tab1RapValue').html(formatNumber(searchSummary.TOT_RAP_AMOUNT));
        $('#tab1SunriseDiscPer').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
        $('#tab1SunriseValueUSDoller').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
        $("#li_Approve").show();
        $("#li_Reject").show();
    }
}
var orderBy = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = '';
        }

        $.ajax({
            url: "/LabStock/LabByRequestGet",
            async: false,
            type: "POST",
            data: {
                PgNo: PageNo
                , OrderBy: orderBy
                , PgSize: GridpgSize
                , REF_NO: $('#txtRefNo').val()
                , EntryBy: $('#txtEntryBy').val()
                , Pending: Pending
                , Approve: Approve
                , Reject: Reject
                , Supp_Status: Supp_Status
                , FromDate: $('#txtFromDate').val()
                , ToDate: $('#txtToDate').val()
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data && data.Data.length > 0) {
                    searchSummary = data.Data[0].DataSummary;
                    rowData = data.Data[0].DataList;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);
                    $('#tab1TCount').show();
                    $('#tab1Pcs').html(formatIntNumber(searchSummary.TOT_PCS));
                    $('#tab1CTS').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1RapValue').html(formatNumber(searchSummary.TOT_RAP_AMOUNT));
                    $('#tab1SunriseDiscPer').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    $('#tab1SunriseValueUSDoller').html(formatNumber(searchSummary.TOT_NET_AMOUNT));

                    DisValPPC_Array = [];
                    data = data.Data[0].DataList;
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].ByRequestStatus == "Approved") {
                            if (data[i].CostValue != null && data[i].CostValue != "") {
                                //DisValPPC_Array.push([data[i].Userid, data[i].REF_NO, data[i].SupplierStatus, data[i].CostDiscPer.toFixed(2), data[i].CostValue.toFixed(2), data[i].CostPriceCts.toFixed(2)]);
                                DisValPPC_Array.push([data[i].Userid, data[i].REF_NO, data[i].CostDiscPer.toFixed(2), data[i].CostValue.toFixed(2), data[i].CostPriceCts.toFixed(2)]);
                            }
                            else {
                                //var SupplierStatus = "0"
                                var CostDiscPer = data[i].OFFER_DISC_PER;
                                var CostValue = data[i].OFFER_DISC_VALUE;
                                var CostPriceCts = (data[i].OFFER_DISC_VALUE / data[i].CTS);

                                //DisValPPC_Array.push([data[i].Userid, data[i].REF_NO, SupplierStatus, CostDiscPer.toFixed(2), CostValue.toFixed(2), CostPriceCts.toFixed(2)]);
                                DisValPPC_Array.push([data[i].Userid, data[i].REF_NO, CostDiscPer.toFixed(2), CostValue.toFixed(2), CostPriceCts.toFixed(2)]);
                            }
                        }
                    }

                } else {
                    if (data.Data.length == 0) {
                        $("#divFilter").show();
                        $("#divGridView").hide();
                        if (data.Message != "No Data Found") {
                            toastr.error(data.Message);
                        }
                    }
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1TCount').hide();
                    $('#tab1Pcs').html('0');
                    $('#tab1CTS').html('0');
                    $('#tab1RapValue').html('0');
                    $('#tab1SunriseDiscPer').html('0');
                    $('#tab1SunriseValueUSDoller').html('0');
                }
                if ($('#myGrid .ag-paging-panel').length > 0) {
                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $('#ddlPagesize').val(GridpgSize);
                    clearInterval(showEntryVar);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                    contentHeight();
                }, 30);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
var ClearRemoveModel = function () {
    $("#ApproveMsgBox").modal("hide");
    $("#RejectMsgBox").modal("hide");
}
function reset() {
    $('#txtRefNo').val("");
    $('#txtEntryBy').val("");
    setTimeout(function () {
        Five_Day_Date_Set();
        if ($("#hdn_samit").val() == 1) {
            Pending = false;
            Approve = false;
            Reject = false;
            $(".btn-spn-opt").removeClass("btn-spn-opt-active");
            ActiveOrNot("Approve");
        }
        else {
            Pending = false;
            Approve = false;
            Reject = false;
            $(".btn-spn-opt").removeClass("btn-spn-opt-active");
            ActiveOrNot("Pending");
        }
        contentHeight();
    }, 1);
}
function ApproveModal() {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'Userid') != "" && count != 0) {
        $("#ApproveMsgBox").modal("show");
    }
    else {
        toastr.warning("No Stone selected for Approve Buy Request");
    }
}
function RejectModal() {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'Userid') != "" && count != 0) {
        $("#RejectMsgBox").modal("show");
    }
    else {
        toastr.warning("No Stone selected for Reject Buy Request");
    }
}
function Action(type) {
    var Userid = _.pluck(gridOptions.api.getSelectedRows(), 'Userid');
    var REF_NO = _.pluck(gridOptions.api.getSelectedRows(), 'REF_NO');
    var Userid_REF_NO = ([Userid, REF_NO].reduce((a, b) => a.map((v, i) => v + b[i]))).join(",");

    if (Userid_REF_NO != "") {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        ClearRemoveModel();

        setTimeout(function () {
            var obj = {};
            obj.REF_NO = Userid_REF_NO;
            obj.Type = type;

            $.ajax({
                url: "/LabStock/ByRequest_ApproveReject",
                async: false,
                type: "POST",
                dataType: "json",
                data: JSON.stringify({ obj: obj }),
                contentType: "application/json; charset=utf-8",
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else if (data.Status == "1") {
                        toastr.success(data.Message);
                        GetLabByRequest();
                    }
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        }, 30);
    }
}
var Pending = true;
var Approve = false;
var Reject = false;
var Supp_Status = false;
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        $("#" + id).removeClass("btn-spn-opt-active");
        if (id == "Pending") {
            Pending = false;
            GetLabByRequest();
        }
        if (id == "Approve") {
            Approve = false;
            GetLabByRequest();
        }
        if (id == "Reject") {
            Reject = false;
            GetLabByRequest();
        }
        if (id == "Supp_Status") {
            Supp_Status = false;
            GetLabByRequest();
        }
    }
    else {
        $(".btn-spn-opt").removeClass("btn-spn-opt-active");
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "Pending") {
            Pending = true;
            Approve = false;
            Reject = false;
            Supp_Status = false;
            GetLabByRequest();
        }
        if (id == "Approve") {
            Pending = false;
            Approve = true;
            Reject = false;
            Supp_Status = false;
            GetLabByRequest();
        }
        if (id == "Reject") {
            Pending = false;
            Approve = false;
            Reject = true;
            Supp_Status = false;
            GetLabByRequest();
        }
        if (id == "Supp_Status") {
            Pending = false;
            Approve = false;
            Reject = false;
            Supp_Status = true;
            GetLabByRequest();
        }
    }
}
/// <reference path="revisedprice.js" />
var ShapeList = [];
var CaratList = [];
var LabList = [];
var ColorList = [];
var PolishList = [];
var FlouList = [];
var ClarityList = [];
var CutList = [];
var SymList = [];
var LocationList = [];
var IsObj = false;
var IsObj1 = false;
var Obj = {};
var Filtered_Data = [];
var OfferPercentage = 0;
var ToDate = F_date;
var FromDate = F_date;
var currMonthInd = parseInt(mnth) - 1;

if (currMonthInd == 0)
    FromDate = [day, m_names[11], (date.getFullYear() - 1)].join("-");
else
{
    var d = new Date(moment().add(-30, 'days'))
    var curr_date = ("0" + d.getDate()).slice(-2);
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    FromDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
}

if ($('#hdnisadminflg').val() == 1) {
    IsObj1 = false;
} else {
    IsObj1 = true;
}
if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1)
    IsObj = false;
else
    IsObj = true;

var today = new Date();
var lastWeekDate = new Date();
if (IsObj) {
    lastWeekDate = new Date(today.setDate(today.getDate() - 30));
} else {
    lastWeekDate = new Date(today.setDate(today.getDate() - 7));
}

function SetCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul", "Aug", "Sep",
        "Oct", "Nov", "Dec");
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month]
        + "-" + curr_year);

    return FinalDate;
}
function reset() {
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());
    if ($("#txtStoneId").length > 0)
        $('#txtStoneId').val("");
    if ($("#txtCompanyName").length > 0)
        $('#txtCompanyName').val("");
    $("#ddlStatus").multiselect("clearSelection");

    $("#ddlStatus").multiselect('refresh');
    GetOrderData();
}

function GetSearchParameter() {
    loaderShow();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var ParameterList = data.Data;
            ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
            CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
            LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
            ColorList = _.filter(ParameterList, function (e) { return e.ListType == 'COLOR' });
            PolishList = _.filter(ParameterList, function (e) { return e.ListType == 'POLISH' });
            FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });
            ClarityList = _.filter(ParameterList, function (e) { return e.ListType == 'CLARITY' });
            CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
            SymList = _.filter(ParameterList, function (e) { return e.ListType == 'SYMM' });
            LocationList = _.filter(ParameterList, function (e) { return e.ListType == 'LOCATION' });

            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
GetSearchParameter();

function GetOfferCriteria() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/Offer/GetOfferCriteria",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            OfferPercentage = data.Data[0].OfferPer;
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}

$(document).ready(function (e) {
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: FromDate,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: ToDate,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });

    GetOfferCriteria();
});
function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

function getValuesAsync1(field) {
    if (field == "shape" || field == "lab" || field == "pointer" || field == "color" || field == "clarity" || field == "cut" || field == "symm" || field == "fls"
        || field == "polish" || field == "Location") {
        return "agSetColumnFilter";
    }
    else if (field == "cts") {
        return "agNumberColumnFilter";
    }
    else {
        return false;
    }
}

function getValuesAsync(field) {
    if (field == "shape") {
        return _.pluck(ShapeList, 'Value');
    }
    else if (field == "lab") {
        return _.pluck(LabList, 'Value');
    }
    else if (field == "pointer") {
        return _.pluck(CaratList, 'Value');
    }
    else if (field == "color") {
        return _.pluck(ColorList, 'Value');
    }
    else if (field == "clarity") {
        return _.pluck(ClarityList, 'Value');
    }
    else if (field == "cut") {
        return _.pluck(CutList, 'Value');
    }
    else if (field == "symm") {
        return _.pluck(SymList, 'Value');
    }
    else if (field == "fls") {
        return _.pluck(FlouList, 'Value');
    }
    else if (field == "polish") {
        return _.pluck(PolishList, 'Value');
    }
    else if (field == "Location") {
        return _.pluck(LocationList, 'Value');
    }
    else {
        return null;
    }
}

function inputAmtIndicator(params) {
    
    var offerAmt = "";
    if (params.data != undefined) {
        var disc = parseFloat(params.data.Disc);
        //var validdays = parseInt(params.data.SOffer_Validity);
        var min, max;
        if (disc > 0) {
            min = disc - OfferPercentage;
            max = disc + OfferPercentage;
        }
        else {
            min = disc + OfferPercentage;
            max = disc - OfferPercentage;
        }
        var val = parseFloat(params.data.SOfferPer);

        if (!isNaN(val)) {
            
            if ((disc > 0 && val >= min && val <= max) || (disc < 0 && val <= min && val >= max)) {
                //if (isNaN(validdays)) {
                //    validdays = 1;
                //}
                var cts = parseFloat(params.data.Cts);
                var rapaport = parseFloat(params.data.cur_rap_rate);
                var newRate;
                if (val > 0) {
                    newRate = rapaport - ((rapaport * ((-1) * val)) / 100);
                    offerAmt = newRate * cts;
                }
                else {
                    newRate = rapaport + ((rapaport * val) / 100);
                    offerAmt = newRate * cts;
                }
                offerAmt = formatNumber(offerAmt);
            }
        }
    }
    return offerAmt;
}

//function onSelectionChanged() {
//    var TOT_CTS = 0;
//    var AVG_SALES_DISC_PER = 0;
//    var AVG_PRICE_PER_CTS = 0;
//    var TOT_NET_AMOUNT = 0;
//    var TOT_PCS = 0;
//    var TOT_RAP_AMOUNT = 0;
//    if (gridOptions.api.getSelectedRows().length > 0) {
//        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Cts'), function (memo, num) { return memo + num; }, 0);
//        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'NetAmount'), function (memo, num) { return memo + num; }, 0);
//        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'RapAmount'), function (memo, num) { return memo + num; }, 0);
//        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
//        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
//        TOT_PCS = gridOptions.api.getSelectedRows().length;
//    } else {
//        TOT_CTS = summary1.TOT_CTS;
//        AVG_SALES_DISC_PER = summary1.AVG_SALES_DISC_PER;
//        AVG_PRICE_PER_CTS = summary1.AVG_PRICE_PER_CTS;
//        TOT_NET_AMOUNT = summary1.TOT_NET_AMOUNT;
//        TOT_PCS = summary1.TOT_PCS;
//    }

//    $('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TOT_CTS) + '');
//    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
//    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
//    $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TOT_NET_AMOUNT) + '');
//    $('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');
//}

function onSelectionChanged() {
    var Totalpcs = 0;
    var TotalCts = 0.0;
    var TotalNetAmt = 0.0;
    var TotalRapAmt = 0.0;
    var net_amount = 0.0;
    var rap_amount = 0.0;
    var TotalPricePerCts = 0.0;
    var i = 0;

    var SearchResultData = gridOptions.api.getSelectedRows();
    if (SearchResultData.length != 0) {
        for (i = 0; i < SearchResultData.length; i++) {
            Totalpcs = Totalpcs + 1;
            TotalCts += parseFloat(SearchResultData[i].Cts);

            net_amount = parseFloat(SearchResultData[i].NetAmount);
            rap_amount = parseFloat(SearchResultData[i].RapAmount);
            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

            TotalNetAmt += parseFloat(net_amount);
            TotalRapAmt += parseFloat(rap_amount);
        }
    }
    else {
        SearchStoneDataList = Filtered_Data;
        for (i = 0; i < SearchStoneDataList.length; i++) {
            Totalpcs = Totalpcs + 1;
            TotalCts += parseFloat(SearchStoneDataList[i].Cts);

            net_amount = parseFloat(SearchStoneDataList[i].NetAmount);
            rap_amount = parseFloat(SearchStoneDataList[i].RapAmount);
            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

            TotalNetAmt += parseFloat(net_amount);
            TotalRapAmt += parseFloat(rap_amount);
        }
    }
    TotalPricePerCts = (TotalNetAmt / TotalCts).toFixed(2);
    AvgDis = ((1 - (TotalNetAmt / TotalRapAmt)) * (-100)).toFixed(2);

    TotalPricePerCts = isNaN(TotalPricePerCts) ? 0 : TotalPricePerCts;
    AvgDis = isNaN(AvgDis) ? 0 : AvgDis;

    $('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TotalCts) + '');
    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AvgDis) + '');
    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : ' + formatNumber(TotalPricePerCts) + '');
    $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : ' + formatNumber(TotalNetAmt) + '');
    $('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + Totalpcs + '');
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

var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    {
        headerName: $("#hdn_View_Image").val(),
        field: 'ImagesLink',
        width: 80,
        cellRenderer: function (params) {
            return params.value;
        },
        sortable: false,
        suppressSorting: true,
        suppressMenu: true,
    },
    {
        headerName: $("#hdn_Offer_Id").val(), field: "iOfferId", tooltip: function (params) { return (params.value); }, width: 85, sortable: true },
    {
        headerName: $("#hdn_offerdate").val(), field: "OfferDate", tooltip: function (params) { return (params.value); }, width: 100, sortable: true },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "sRefNo", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            else if (params.data.Stock_Avail == "0") {
                return '<div class="stock-font"><a target="_blank">' + params.data.sRefNo + '</a></div>';
            }
            else {
                //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
                //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
                return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
            }
            
        },
        filter: false,
        sortable: false
    },
    {
        headerName: $("#hdn_CompanyName").val(), hide: IsObj, field: "sCompName", tooltip: function (params) { return (params.value); },
        width: 100, sortable: true
    },
    {
        headerName: $("#hdn_username").val(), hide: IsObj, field: "sUsername", tooltip: function (params) { return (params.value); },
        width: 100, sortable: true },
    {
        headerName: $("#hdn_Location").val(), field: "sLocation", tooltip: function (params) { return (params.value); }, width: 70,
        sortable: true,
        filter: getValuesAsync1("Location"),
        filterParams: {
            values: getValuesAsync("Location"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    //{ headerName: "Status", field: "sStatus", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    {
        headerName: $("#hdn_Status").val(), field: "StoneStatus", tooltip: function (params) { return (params.value); }, width: 50,
        cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            return params.data.StoneStatus;
        }, filter: false, sortable: false
    },
    {
        headerName: $("#hdn_Shape").val(), field: "sShape", tooltip: function (params) { return (params.value); }, width: 95,
        sortable: true,
        filter: getValuesAsync1("shape"),
        filterParams: {
            values: getValuesAsync("shape"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Pointer").val(), field: "sPointer", tooltip: function (params) { return (params.value); }, width: 60,
        sortable: true,
        filter: getValuesAsync1("pointer"),
        filterParams: {
            values: getValuesAsync("pointer"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Lab").val(), field: "sLab", tooltip: function (params) { return (params.value); }, width: 40,
        sortable: true,
        cellRenderer: function (params) {
            if (params.data.sCertiNo != "") {
                if (params.data != undefined) {
                    if (params.data.sLab == "GIA") {
                        return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.sLab + '</a>';
                    }
                    else if (params.data.sLab == "HRD") {
                        return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.sLab + '</a>';
                    }
                    else if (params.data.sLab == "IGI") {
                        return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.sLab + '</a>';
                    }
                    else {
                        return '';
                    }
                }
                else {
                    return '';
                }
            } 
            else {
                return '<span style="color :blue;">' + params.data.sLab + '</span>';
            }
        },
        filter: getValuesAsync1("lab"),
        filterParams: {
            values: getValuesAsync("lab"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    { headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 70, filter: false, sortable: true },
    {
        headerName: $("#hdn_Color").val(), field: "sColor", tooltip: function (params) { return (params.value); }, width: 50,
        sortable: true,
        filter: getValuesAsync1("color"),
        filterParams: {
            values: getValuesAsync("color"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Clarity").val(), field: "sClarity", tooltip: function (params) { return (params.value); }, width: 60,
        sortable: true,
        filter: getValuesAsync1("clarity"),
        filterParams: {
            values: getValuesAsync("clarity"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_CTS").val(), field: "Cts", tooltip: function (params) { return (params.value); }, width: 50,
        sortable: true,
        filter: getValuesAsync1("cts"),
        filterParams: {
            values: getValuesAsync('cts'),
            resetButton: true,
            applyButton: true,
            filterOptions: ['inRange']
        },
    },
    {
        headerName: $("#hdn_Rap_Amt_Doller").val(), field: "RapAmount", width: 85, tooltip: function (params) {
            return formatNumber(params.value);
        }, cellRenderer: function (params) { return formatNumber(params.value); }, filter: false, sortable: true
    },

    {
        headerName: $("#hdn_Disc_Per").val(), field: "Disc", width: 60, cellRenderer: function (params) {
            return formatNumber(params.value);
        }, cellStyle: { color: 'red', 'font-weight': 'bold' }, sortable: true
    },
    {
        headerName: $("#hdn_Net_Amt").val(), field: "NetAmount", width: 90, cellRenderer: function (params) {
            return formatNumber(params.value);
        }, cellStyle: { color: 'red', 'font-weight': 'bold' }, sortable: true
    },
    {
        headerName: $("#hdn_Offer_Disc").val(), field: "SOfferPer", width: 75,
        cellStyle: { color: 'blue', 'font-weight': 'bold' },
        cellRenderer: function (params) {
            return '<span>' + (params.data.SOfferPer == "0" ? "" : formatNumber(params.data.SOfferPer)) + '</span>';
        },
        sortable: true,
        cellClass: ['co-input']
    },
    {
        headerName: $("#hdn_Offer_Amt").val(),
        field: "SOfferPer",
        width: 95,
        cellStyle: { color: 'blue', 'font-weight': 'bold' },
        cellRenderer: 'inputAmtIndicator',
        sortable: true,
        cellClass: ['co-input']
    },
    {
        headerName: $("#hdn_Offer_Valid_Days").val(),
        field: "SOffer_Validity",
        width: 110,
        cellRenderer: function (params) {
            return '<span>' + (params.data.SOffer_Validity == "0" || params.data.SOffer_Validity == "" ? "1" : params.data.SOffer_Validity) + '</span>';
        },
        sortable: false,
        cellClass: ['co-input']
    },
    {
        headerName: $("#hdn_Cut").val(), field: "sCut", tooltip: function (params) { return (params.value); }, width: 50,
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        sortable: true,
        cellStyle: function (params) {
            if (params.value == undefined) {
                return '';
            }
            if (params.value == '3EX')
                return { 'font-weight': 'bold' };
        },
        filter: getValuesAsync1("cut"),
        filterParams: {
            values: getValuesAsync("cut"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Polish").val(), field: "sPolish", tooltip: function (params) { return (params.value); }, width: 60,
        sortable: true,
        cellStyle: function (params) {
            if (params.data != undefined) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        },
        filter: getValuesAsync1("polish"),
        filterParams: {
            values: getValuesAsync("polish"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Symm").val(), field: "sSymm", tooltip: function (params) { return (params.value); }, width: 50,
        sortable: true,
        cellStyle: function (params) {
            if (params.data != undefined) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        },
        filter: getValuesAsync1("symm"),
        filterParams: {
            values: getValuesAsync("symm"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    {
        headerName: $("#hdn_Fls").val(), field: "sFls", tooltip: function (params) { return (params.value); }, width: 50,
        sortable: true,
        filter: getValuesAsync1("fls"),
        filterParams: {
            values: getValuesAsync("fls"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }
    },
    { headerName: $("#hdn_Length").val(), field: "Length", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Width").val(), field: "Width", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_Depth").val(), field: "Depth", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_Depth_Per").val(), field: "DepthPer", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Table_Per").val(), field: "TablePer", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Table_Black").val(), field: "sTableNatts", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    { headerName: $("#hdn_Crown_Natts").val(), field: "sCrownNatts", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    { headerName: $("#hdn_Table_White").val(), field: "sInclusion", tooltip: function (params) { return (params.value); }, width: 80, sortable: true },
    { headerName: $("#hdn_Crown_White").val(), field: "sCrownInclusion", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    {
        headerName: $("#hdn_Crown_Angle").val(), field: "CrAng", tooltip: function (params) { return formatNumber(params.value); },
        cellRenderer: function (params) {
            return formatNumber(params.value);
        },
        width: 60, sortable: true
    },
    {
        headerName: $("#hdn_CR_HT").val(), field: "CrHt", tooltip: function (params) { return formatNumber(params.value); },
        cellRenderer: function (params) {
            return formatNumber(params.value);
        },
        width: 50, sortable: true
    },
    {
        headerName: $("#hdn_Pav_Ang").val(), field: "PavAng", tooltip: function (params) { return formatNumber(params.value); },
        cellRenderer: function (params) {
            return formatNumber(params.value);
        },
        width: 60, sortable: true
    },
    {
        headerName: $("#hdn_Pav_HT").val(), field: "PavHt", tooltip: function (params) { return formatNumber(params.value); },
        cellRenderer: function (params) {
            return formatNumber(params.value);
        },
        width: 60, sortable: true
    },
];

var gridDiv = document.querySelector('#myGrid');
var gridOptions = {
    masterDetail: true,
    detailCellRenderer: 'myDetailCellRenderer',
    detailRowHeight: 70,
    groupDefaultExpanded: 1,
    components: {
        inputAmtIndicator: inputAmtIndicator
    },
    defaultColDef: {
        enableSorting: true,
        sortable: false,
        resizable: true
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
    onRowSelected: onSelectionChanged,
    onBodyScroll: onBodyScroll,
    rowModelType: 'serverSide',
    cacheBlockSize: 50, // you can have your custom page size
    paginationPageSize: 50, //pagesize
    paginationNumberFormatter: function (params) {
        return '[' + params.value.toLocaleString() + ']';
    }
};
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

function GetDataList() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    setTimeout(function () {
        $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
    }, 100);
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

const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();
        var CompanyName = '', StoneId = '', Pointer = '', Shape = '', Lab = '', Color = '', Polish = '', Clarity = '', Fls = '', Cut = '', Symm = '', Location = '', OrderBy = '';

        if (params.request.sortModel.length > 0) {
            OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }
        if ($("#txtCompanyName").length > 0)
            CompanyName = $('#txtCompanyName').val();
        if ($("#txtStoneId").length > 0)
            StoneId = $('#txtStoneId').val();

        if (params.request.filterModel.Cts) {
            var str = "";
            if (params.request.filterModel.Cts.operator == "AND" || params.request.filterModel.Cts.operator == "OR") {
                if (params.request.filterModel.cts.condition1) {
                    str = params.request.filterModel.cts.condition1.filter + "-";
                    if (params.request.filterModel.cts.condition1.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition1.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition1.filter
                    }
                }
                if (params.request.filterModel.cts.condition2) {
                    if (str != "")
                        str = str + ",";
                    str = params.request.filterModel.cts.condition2.filter + "-";
                    if (params.request.filterModel.cts.condition2.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition2.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition2.filter
                    }
                }
            }
            else {
                str = params.request.filterModel.Cts.filter + "-";
                if (params.request.filterModel.Cts.filterTo != null) {
                    str = str + params.request.filterModel.Cts.filterTo
                } else {
                    str = str + params.request.filterModel.Cts.filter
                }
            }
            Pointer = str;
        }
        else {
            Pointer = "";
        }

        if (params.request.filterModel.sShape) {
            Shape = params.request.filterModel.sShape.values.join(",");
        }
        else {
            Shape = "";
        }

        if (params.request.filterModel.sPointer) {
            Pointer = params.request.filterModel.sPointer.values.join(",");
        }
        else {
            if (Pointer == undefined || Pointer == "")
                Pointer = "";
        }

        if (params.request.filterModel.sLab) {
            Lab = params.request.filterModel.sLab.values.join(",");
        }
        else {
            Lab = "";
        }

        if (params.request.filterModel.sColor) {
            Color = params.request.filterModel.sColor.values.join(",");
        }
        else {
            Color = "";
        }

        if (params.request.filterModel.sPolish) {
            Polish = params.request.filterModel.sPolish.values.join(",");
        }
        else {
            Polish = "";
        }

        if (params.request.filterModel.sClarity) {
            Clarity = params.request.filterModel.sClarity.values.join(",");
        }
        else {
            Clarity = "";
        }

        if (params.request.filterModel.sFls) {
            Fls = params.request.filterModel.sFls.values.join(",");
        }
        else {
            Fls = "";
        }

        if (params.request.filterModel.sCut) {
            Cut = params.request.filterModel.sCut.values.join(",");
        }
        else {
            Cut = "";
        }

        if (params.request.filterModel.sSymm) {
            Symm = params.request.filterModel.sSymm.values.join(",");
        }
        else {
            Symm = "";
        }

        if (params.request.filterModel.sLocation) {
            Location = params.request.filterModel.sLocation.values.join(",");
        }
        else {
            Location = "";
        }

        Obj = {
            FromDate: $('#txtFromDate').val(), ToDate: $('#txtToDate').val(), CompanyName: CompanyName, RefNo: StoneId, PageNo: PageNo,
            PageSize: PageSize, Pointer: Pointer, Shape: Shape, Lab: Lab, Color: Color, Polish: Polish, Clarity: Clarity,
            Fls: Fls, Cut: Cut, Symm: Symm, Location: Location, OrderBy: OrderBy
        };
        $.ajax({
            url: "/Offer/GetOfferHistoryList",
            async: false,
            type: "POST",
            data: Obj,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data && data.Data.length > 0) {
                    Filtered_Data = data.Data[0].DataList;
                    var searchSummary = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);

                    $('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(searchSummary.TOT_CTS) + '');
                    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
                    //$('#tab1ppcts').html('Price Per Cts : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
                    $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
                    $('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + searchSummary.TOT_PCS + '');
                } else {
                    Filtered_Data = [];
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1cts').html($("#hdn_Cts").val() +' : 0');
                    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab1ppcts').html('Price Per Cts : 0');
                    $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    $('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Filtered_Data = [];
                params.successCallback([], 0);
                MoveToErrorPage(0);
                loaderHide();
            }
        });

    }
};

function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        resultHei = $(".order-history-data").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 70;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {

    GetDataList();

    $('#btnSearch').click(function () {
        GetDataList();
    });

    $('#btnExcel').click(function () {
        loaderShow();

        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'iOfferId').join(",");

        var CompanyName = '';
        if ($("#txtCompanyName").length > 0)
            CompanyName = $('#txtCompanyName').val();
        if ($("#txtStoneId").length > 0 && stoneno == "")
            stoneno = $('#txtStoneId').val();

        Obj.FromDate = $('#txtFromDate').val();
        Obj.ToDate = $('#txtToDate').val();
        Obj.CompanyName = CompanyName;
        
        Obj.isAdmin = $('#hdnisadminflg').val();
        Obj.isEmp = $('#hdnisempflg').val();
        Obj.RefNo = stoneno;
        Obj.FormName = 'Offer History';
        Obj.ActivityType = 'Excel Export';

        $.ajax({
            url: "/Offer/DownloadOfferHistoryList",
            type: "POST",
            async: false,
            data: Obj,
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    data.Message = data.Message.replace('No data found', $("#hdn_No_data_found").val());
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data);
                    }
                    else if (data.Status == "1") {
                        if (data.Message.indexOf('ExcelFile') > -1) {
                            location.href = data.Message;
                        }
                        else {
                            toastr.error(data.Message);
                        }
                    }
                    loaderHide();
                }
                else {
                    window.location = GetLoginUrl();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                MoveToErrorPage(0);
                loaderHide();
            }
        });
    });

    $('#btnReset').click(function () {
        $("#ddlType").val("");
        if ($("#txtCompanyName").length > 0)
            $("#txtCompanyName").val("");
        $('#txtFromDate').val(F_date);
        $('#txtToDate').val(SetCurrentDate());
        if ($("#txtStoneId").length > 0)
            $("#txtStoneId").val("");
        $("#ddlActive").val("");
        GetDataList();
    });

    contentHeight();
});

$(window).resize(function () {
    contentHeight();
});

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
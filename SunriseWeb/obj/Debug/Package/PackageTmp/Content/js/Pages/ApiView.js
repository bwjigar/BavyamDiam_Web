var gridOptions = {};
var rowData = [];
var iTransId = 0;

var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

var SetCurrentDate = function () {
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

var lastWeekDate = new Date();
var m_names = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");


var Back = function () {
    window.location.href = '/Api/Filter';
}

var GetDropDownList = function (Type) {
    if ($(Type).val() != "") {
        loaderShow();

        $.ajax({
            type: "POST",
            async: false,
            url: '/Common/GetParameter',
            data: { ListValue: $(Type).val() },
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                ParameterList = data.Data;
                $('#ddlValue').empty();
                $('#ddlValue').html(" <option value=\"\" selected=\"selected\">Select an Option</option>");
                _(ParameterList).each(function (obj, i) {
                    if ($(Type).val() == "CM" || $(Type).val() == "CT" || $(Type).val() == "CUN") {
                        $('#ddlValue').append(" <option value=\"" + obj.Value + "\">" + obj.Value + "</option>");
                    } else {
                        $('#ddlValue').append(" <option value=\"" + obj.Id + "\">" + obj.Value + "</option>");
                    }
                });
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    }
    else {
        $('#ddlValue').empty();
        $('#ddlValue').html(" <option value=\"\" selected=\"selected\">Select an Option</option>");
    }
}

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
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
});

function contentHeight() {
    var winH = $(window).height(),
        header = $(".viewcartdata-header").height(),
        navbarHei = $(".apiViews-form").height(),
        contentHei = winH - (header + navbarHei + 150);
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function (e) {
    contentHeight();

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
    });
});

var Clear = function () {
    //  window.location = '/Admin_APIView/Admin_APIView';
    $('#ddlType').val("");
    $('#ddlValue').val("");
    $("#txtFromDate").val(SetCurrentDate());
    $("#ToDate").val(SetCurrentDate());

    $('#txtFromDate').daterangepicker({
        "setDate": SetCurrentDate(),
        startDate: moment(),
        showDropdowns: true,
        singleDatePicker: true,
        "locale": {
            "format": "DD-MMM-YYYY",
            "separator": "-",
        }
    });
    $('#txtToDate').daterangepicker({
        "setDate": SetCurrentDate(),
        startDate: moment(),
        showDropdowns: true,
        singleDatePicker: true,
        "locale": {
            "format": "DD-MMM-YYYY",
            "separator": "-",
        }
    });

    FillGrid();
}

var columnDefs = [

    { headerName: "Sr. No.", field: "iSr", width: 60 },
    { headerName: "User Name", field: "sfullname", width: 150 },
    { headerName: "Date", field: "dTransDate1", width: 170, },
    { headerName: "API Name", field: "sApiName", width: 250 },
    { headerName: "API URL", field: "APIURL1", width: 400, },
    { headerName: "API Url", field: "ApiUrl", width: 350, hide: true },
    { headerName: "Format", field: "export_type", width: 120 },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 120, cellRenderer: UserDetailPage, },
    { headerName: "Tarns.Id", field: "iTransId", width: 60, hide: true },
    { headerName: "User.Id", field: "iUserId", width: 60, hide: true },
    { headerName: "Format", field: "sShape", width: 120, hide: true },
    { headerName: "Format", field: "sColor", width: 120, hide: true },
    { headerName: "Format", field: "sClarity", width: 120, hide: true },
    { headerName: "Format", field: "sCut", width: 120, hide: true },
    { headerName: "Format", field: "sPolish", width: 120, hide: true },
    { headerName: "Format", field: "sSymm", width: 120, hide: true },
    { headerName: "Format", field: "sFls", width: 120, hide: true },
    { headerName: "Format", field: "sLab", width: 120, hide: true },
    { headerName: "Format", field: "Cts", width: 120, hide: true },
    { headerName: "Format", field: "Disc", width: 120, hide: true },
    { headerName: "Format", field: "sPointer", width: 120, hide: true },
    { headerName: "Format", field: "dFromLength", width: 120, hide: true },
    { headerName: "Format", field: "dToLength", width: 120, hide: true },
    { headerName: "Format", field: "dFromWidth", width: 120, hide: true },
    { headerName: "Format", field: "dToWidth", width: 120, hide: true },
    { headerName: "Format", field: "dFromDepth", width: 120, hide: true },
    { headerName: "Format", field: "dToDepth", width: 120, hide: true },
    { headerName: "Format", field: "dFromDepthPer", width: 120, hide: true },
    { headerName: "Format", field: "dToDepthPer", width: 120, hide: true },
    { headerName: "Format", field: "dFromTablePer", width: 120, hide: true },
    { headerName: "Format", field: "dToTablePer", width: 120, hide: true },
    { headerName: "Format", field: "dFromCrAng", width: 120, hide: true },
    { headerName: "Format", field: "dToCrAng", width: 120, hide: true },
    { headerName: "Format", field: "dFromCrHt", width: 120, hide: true },
    { headerName: "Format", field: "dToCrHt", width: 120, hide: true },
    { headerName: "Format", field: "dFromPavAng", width: 120, hide: true },
    { headerName: "Format", field: "dToPavAng", width: 120, hide: true },
    { headerName: "Format", field: "dFromPavHt", width: 120, hide: true },
    { headerName: "Format", field: "dToPavHt", width: 120, hide: true },
    { headerName: "Format", field: "sShade", width: 120, hide: true },
    { headerName: "Format", field: "sInclusion", width: 120, hide: true },
    { headerName: "Format", field: "sTableNatts", width: 120, hide: true },
    { headerName: "Format", field: "sCrownNatts", width: 120, hide: true },
    { headerName: "Format", field: "sCrownInclusion", width: 120, hide: true },
    { headerName: "Format", field: "FTP_NAME", width: 120, hide: true },
    { headerName: "Format", field: "FTP_PASSWORD", width: 120, hide: true },
    { headerName: "Format", field: "FTP_UPLOAD_TIME", width: 120, hide: true },
    { headerName: "Format", field: "Ftp_User", width: 120, hide: true },
    { headerName: "Format", field: "sEmail", width: 120, hide: true },
    { headerName: "Format", field: "sSeprator", width: 120, hide: true },
    { headerName: "Format", field: "sRepeat", width: 120, hide: true },
    { headerName: "Format", field: "iLocation", width: 120, hide: true },
    { headerName: "Date", field: "dTransDate", width: 100, hide: true },
    { headerName: "mailtime", field: "sMailUploadTime", width: 120, hide: true },
];
function UserDetailPage(params) {
    //params.$scope.EditData = EditData;
    //params.$scope.ExcelExport = ExcelExport;
    //params.$scope.DeleteData = DeleteData;
    return '<div class="Customer-action-cel"> <a href="#" title="Edit" onclick="EditData(' + params.data.iTransId + ')"><i class="flaticon-edit"></i></a>&nbsp;&nbsp;' +
        ' <a href="javascript:void(0);" title="Export Excel" onclick="ExcelExport(\'' + params.data.iUserId + '\',\'' + params.data.iTransId + '\',\'' + params.data.sApiName + '\',\'' + params.data.export_type + '\')"><i class="fa fa-file-excel-o" style="font-size:25px;"></i></a>&nbsp;&nbsp;&nbsp;' +
        '<a href="#" title="Delete" onclick="DeleteData(' + params.data.iTransId + ')"><i class="fa fa-trash" style="font-size:25px;"></i></a>' + '</div>'
    //  return '<a href="" target="_blank" style="text-decoration: underline; color: #003d66;" ng-click="GoToUserDetail(data)">Edit</span></a>&nbsp;&nbsp;<a href="" target="_blank" style="text-decoration: underline; color: #003d66;" ng-click="DeleteUserDetail(data)">Delete</span></a>';
}

function EditData(row) {
    /* 
 
     var FromCarat = "";
     var ToCarat = "";
     var FromDisc = "";
     var ToDisc = "";
     if (row.Cts != null) {
         var Carat = row.Cts.split("-");
         FromCarat = Carat[0];
         ToCarat = Carat[1];
     }
     if (row.Disc != null) {
         var Disc = row.Disc.split("-");
         FromDisc = Disc[0];
         ToDisc = Disc[1];
     }
     var obj = {
         "iTransId": row.iTransId,
         "sTransDate": row.dTransDate1,
         // "sTransDate": row.dTransDate,
         "sShape": row.sShape,
         "sColor": row.sColor,
         "sClarity": row.sClarity,
         "sCut": row.sCut,
         "sPolish": row.sPolish,
         "sSymm": row.sSymm,
         "sFls": row.sFls,
         "sLab": row.sLab,
         "dFromcts": FromCarat,
         "dToCts": ToCarat,
         "sFromDisc": FromDisc,
         "dToDisc": ToDisc,
         "sPointer": row.sPointer,//SizeLst,
         "dFromLength": row.dFromLength == 0 ? "" : row.dFromLength,
         "dToLength": row.dToLength == 0 ? "" : row.dToLength,
         "dFromWidth": row.dFromWidth == 0 ? "" : row.dFromWidth,
         "dToWidth": row.dToWidth == 0 ? "" : row.dToWidth,
         "dFromDepth": row.dFromDepth == 0 ? "" : row.dFromDepth,
         "dToDepth": row.dToDepth == 0 ? "" : row.dToDepth,
         "dFromDepthPer": row.dFromDepthPer == 0 ? "" : row.dFromDepthPer,
         "dToDepthPer": row.dToDepthPer == 0 ? "" : row.dToDepthPer,
         "dFromTablePer": row.dFromTablePer == 0 ? "" : row.dFromTablePer,
         "dToTablePer": row.dToTablePer == 0 ? "" : row.dToTablePer,
         "dFromCrAng": row.dFromCrAng == 0 ? "" : row.dFromCrAng,
         "dToCrAng": row.dToCrAng == 0 ? "" : row.dToCrAng,
         "dFromCrHt": row.dFromCrHt == 0 ? "" : row.dFromCrHt,
         "dToCrHt": row.dToCrHt == 0 ? "" : row.dToCrHt,
         "dFromPavAng": row.dFromPavAng == 0 ? "" : row.dFromPavAng,
         "dToPavAng": row.dToPavAng == 0 ? "" : row.dToPavAng,
         "dFromPavHt": row.dFromPavHt == 0 ? "" : row.dFromPavHt,
         "dToPavHt": row.dToPavHt == 0 ? "" : row.dToPavHt,
         "sShade": row.sShade,
         "sInclusion": row.sInclusion,
         "sTableNatts": row.sTableNatts,
         "sApiName": row.sApiName,
         "sExpType": row.export_type,
         "sCrownNatts": row.sCrownNatts,//$scope.APIView.NetAmountFrom == undefined ? "" : $scope.StockSearch.NetAmountFrom,
         "sCrownInclusion": row.sCrownInclusion,//$scope.StockSearch.NetAmountTo == undefined ? "" : $scope.StockSearch.NetAmountTo,
         "sFtpName": row.FTP_NAME,
         "sftpUser": row.Ftp_User,
         "sftpPass": row.FTP_PASSWORD,
         "iFtpUploadTime": row.FTP_UPLOAD_TIME,
         "sSeprator": row.sSeprator,
         "sRepeat": row.sRepeat,//$scope.APIView.Repeat == undefined ? "" : $scope.APIView.Repeat,
         "sEmail": row.sEmail,
         "sMailUploadTime": row.sMailUploadTime,
         "iLocation": row.iLocation,
         "ApiUrl": row.ApiUrl,
         "APIURL1": row.APIURL1,
 
     };
     /*loaderShow();
     $http({
         method: "POST",
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         url: '/APIFilter/GetStoneCountnewAPI',
         data: JSON.stringify({ model: obj }),
     }).then(function (data) {
         loaderHide();
         window.open('/APIFilter/APIFilter?Type=' + 'modify' + '&TransId=' + row.iTransId);
     });*/
    window.open('/API/Filter?Type=' + 'modify' + '&TransId=' + row);
}

function DeleteData(TransId) {
    iTransId = TransId;
    $("#Remove").modal("show");
}
ClearRemoveModel = function () {
    iTransId = 0;
    $("#Remove").modal("hide");
}

var DeleteApiData = function () {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/API/DeleteApi',
        data: "{ 'TransID': \'" + iTransId + "\' }",
        success: function (data) {
            loaderHide();
            ClearRemoveModel();
            toastr.success(data.Message);
            Clear();
            FillGrid();
        }
    });
}
//==========================================  Excel Export ========================================================//
function ExcelExport(UserId, TransId, ApiName, ExportType) {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/Api/DownloadApi',
        data: "{" +
            "'UserId': '" + UserId + "', 'TransId': '" + TransId + "', 'ApiName': '" + ApiName + "','ExportType': '" + ExportType + "'}",
        success: function (data) {
            if (data.indexOf("Error") >= 0) {
                toastr.error(data);
                loaderHide();
                return;
            }
            location.href = data;
            loaderHide();
        }
    });
}
//==========================================  DOWNLOAD ========================================================//

var DownloadExcel = function (filepath) {
    // var url = '/SearchStock/DownloadExcel/?filepath=' + filepath;
    var url = filepath;
    var hiddenIFrameID = 'hiddenDownloaderReportResult';
    var iframe = document.getElementById(hiddenIFrameID);
    if (iframe === null) {
        iframe = document.createElement('iframe');
        iframe.id = hiddenIFrameID;
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    }
    iframe.src = url;
};

function GetSearch(data) {
    rowData = data;
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        defaultColDef: {
            enableSorting: true,
            sortable: true,
            resizable: true,
            filter: 'agTextColumnFilter',
            filterParams: {
                applyButton: true,
                resetButton: true,
            }
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        rowData: data,
        cacheBlockSize: 50, // you can have your custom page size
        paginationPageSize: 50, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        },
        overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>'
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}

var FillGrid = function (APIView) {
    var FromDate = $("#txtFromDate").val();
    var ToDate = $("#txtToDate").val();
    var selectedValue = "";
    if ($("#ddlType").val() == "CT") {
        selectedValue = $("#ddlValue").val();
    }
    else if ($("#ddlType").val() == "UN") {
        selectedValue = $("#ddlValue").val();
    }
    else if ($("#ddlType").val() == "CUN") {
        selectedValue = $("#ddlValue").val();
    }
    else if ($("#ddlType").val() == "CM") {
        selectedValue = $("#ddlValue").val();
    }

    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/Api/GetApiViews',
        data: "{'fromDate':'" + FromDate + "', 'ToDate':'" + ToDate + "', 'type': '" + $("#ddlType").val() + "', 'value': '" + selectedValue + "'}",
        success: function (data) {
            if (data.Data.length > 0) {
                _.each(data.Data, function (itm) {
                    itm.APIURL1 = "https://www.sunrisediamonds.com.hk/inventory/" + itm.sApiName + "." + itm.export_type;
                });

                GetSearch(data.Data);
                loaderHide();
            }
            else {
                toastr.error("Data Not Available", { timeOut: 2500 });
                GetSearch([]);
                loaderHide();
            }
        }
    });

}
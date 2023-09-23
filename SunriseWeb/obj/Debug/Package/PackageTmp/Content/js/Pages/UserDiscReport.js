function DeleteGetter(params) {

    var parameter =
        params.data.iTransId + ',' + params.data.CustId + ',' + params.data.iVendorId + ',\'' +
        params.data.sFromShape + '\',\'' + params.data.sToShape + '\',\'' +
        params.data.sLab + '\',\'' + '' + '\',\'' + '' + '\',' +
        (params.data.rFromCts == "0" ? "''" : params.data.rFromCts) + ',' +
        (params.data.rToCts == "0" ? "''" : params.data.rToCts) + ',\'' + 
        params.data.sFromColor + '\',\'' + params.data.sToColor + '\',\'' + params.data.sFromClarity + '\',\'' + params.data.sToClarity + '\',\'' +
        params.data.sFromCut + '\',\'' + params.data.sToCut + '\',\'' + params.data.sFromFls + '\',\'' + params.data.sToFls + '\',' +
        (params.data.rDisc == "0" ? "''" : params.data.rDisc) + ',' +
        (params.data.rValDisc == "0" ? "''" : params.data.rValDisc) + '\,\'' +
        params.data.Stock + '\'';
    
    return '<a href="javascript:void(0);" onClick="RemoveDisc(' + parameter + ');" title="Delete"><i class="fa fa-trash fa-2x"></i></a>';
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

var columnDefs = [
    { headerName: "Entry Date", field: "EntryDate", width: 135 },
    { headerName: "Customer Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 170 },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "User Name", field: "Username", tooltip: function (params) { return (params.value); }, width: 170 },
    { headerName: "Assist By", field: "AssistBy", tooltip: function (params) { return (params.value); }, width: 170 },
    { headerName: "Active", height:50, field: "IsActive", tooltip: function (params) { return (params.value); }, width: 58, cellRenderer: Status, },
    { headerName: "CustomerId", hide: true, field: "CustId", tooltip: function (params) { return (params.value); } },
    { headerName: "iVendorId", hide: true, field: "iVendorId", tooltip: function (params) { return (params.value); } },
    
    { headerName: "Vendor", field: "sPartyName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Stock", field: "Stock", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Location", field: "Location", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "From Shape", field: "sFromShape", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "To Shape", field: "sToShape", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Lab", field: "sLab", tooltip: function (params) { return (params.value); }, width: 80 },
    //{ headerName: "From Pointer", field: "sFromPointer", tooltip: function (params) { return (params.value); }, width: 80 },
    //{ headerName: "To Pointer", field: "sToPointer", tooltip: function (params) { return (params.value); }, width: 80 },
    {
        headerName: "From Cts", field: "rFromCts", tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            var cts = formatNumber(params.value).toString();
            if (cts == '0' || cts == '0.00')
                return '';
            else
                return cts;
        }, width: 80
    },
    {
        headerName: "To Cts", field: "rToCts", tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            var cts = formatNumber(params.value).toString();
            if (cts == '0' || cts == '0.00')
                return '';
            else
                return cts;
        }, width: 80
    },
    { headerName: "From Color", field: "sFromColor", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Color", field: "sToColor", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Clarity", field: "sFromClarity", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Clarity", field: "sToClarity", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Cut", field: "sFromCut", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Cut", field: "sToCut", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Fls", field: "sFromFls", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Fls", field: "sToFls", tooltip: function (params) { return (params.value); }, width: 80 },
    {
        headerName: "Discount", field: "rDisc", tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            var cts = formatNumber(params.value).toString();
            if (cts == '0' || cts == '0.00')
                return '';
            else
                return cts;
        }, width: 80
    },
    {
        headerName: "Val Discount", field: "rValDisc", tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            var cts = formatNumber(params.value).toString();
            if (cts == '0' || cts == '0.00')
                return '';
            else
                return cts;
        }, width: 90
    },
    {
        headerName: $("#hdn_Remove").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, field: "Remove", width: 90, cellRenderer: DeleteGetter, suppressSorting: true,
        suppressMenu: true,
    }
];
function Status(params) {
    if (params.data.IsActive == true) {
        return "<span class='Yes'> Yes </span>";
    }
    else {
        return "<span class='No'> No </span>";
    }
}
var gridDiv = document.querySelector('#myGrid');
var gridOptions = {};
var showEntryVar = null;
var pgSize = 50;
function GetDataList() {

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        components: {
            DeleteGetter: DeleteGetter
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
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize,
        paginationPageSize: pgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);


    var showEntryHtml = '<div class="show_entry"><label>'
        + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
        + '<option value="50">50</option>'
        + '<option value="100">100</option>'
        + '<option value="200">200</option>'
        + '<option value="500">500</option>'
        + '</select> entries'
        + '</label>'
        + '</div>';

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(pgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);
}

function RemoveDisc(iTransId, CustId, iVendorId,sFromShape,sToShape, sLab,sFromPointer,sToPointer,
    rFromCts,rToCts, sFromColor,sToColor,sFromClarity,sToClarity,
    sFromCut, sToCut, sFromFls, sToFls, rDisc, rValDisc, Stock) {
    
    var xmlStr = '<REQUEST>';
    xmlStr += '<ROW>';
    xmlStr += '<VENDOR>' + iVendorId + '</VENDOR>';
    xmlStr += '<SHAPEFROM>' + (sFromShape == "null" ? "" : sFromShape) + '</SHAPEFROM>';
    xmlStr += '<SHAPETO>' + (sToShape == "null" ? "" : sToShape) + '</SHAPETO>';
    xmlStr += '<LAB>' + (sLab == "null" ? "" : sLab) + '</LAB>';
    //xmlStr += '<POINTERFROM>' + sFromPointer + '</POINTERFROM>';
    //xmlStr += '<POINTERTO>' + sToPointer + '</POINTERTO>';
    xmlStr += '<FROMCTS>' + (rFromCts == "null" ? "0" : rFromCts) + '</FROMCTS>';
    xmlStr += '<TOCTS>' + (rToCts == "null" ? "0" : rToCts)  + '</TOCTS>';
    xmlStr += '<FROMCOLOR>' + (sFromColor == "null" ? "" : sFromColor) + '</FROMCOLOR>';
    xmlStr += '<TOCOLOR>' + (sToColor == "null" ? "" : sToColor) + '</TOCOLOR>';
    xmlStr += '<FROMCLARITY>' + (sFromClarity == "null" ? "" : sFromClarity) + '</FROMCLARITY>';
    xmlStr += '<TOCLARITY>' + (sToClarity == "null" ? "" : sToClarity) + '</TOCLARITY>';
    xmlStr += '<FROMCUT>' + (sFromCut == "null" ? "" : sFromCut) + '</FROMCUT>';
    xmlStr += '<TOCUT>' + (sToCut == "null" ? "" : sToCut) + '</TOCUT>';
    xmlStr += '<FROMFLS>' + (sFromFls == "null" ? "" : sFromFls) + '</FROMFLS>';
    xmlStr += '<TOFLS>' + (sToFls == "null" ? "" : sToFls) + '</TOFLS>';
    xmlStr += '<DISC>' + (rDisc == "null" ? "0" : rDisc) + '</DISC>';
    xmlStr += '<VALDISC>' + (rValDisc == "null" ? "0" : rValDisc) + '</VALDISC>';
    xmlStr += '<STOCK>' + (Stock == "null" ? "" : Stock) + '</STOCK>';
    xmlStr += '</ROW>';
    xmlStr += '</REQUEST>';
    
    $.ajax({
        url: "/Customer/SaveUserDisc",
        async: false,
        type: "POST",
        data: { CustId: CustId, Oper: 'Delete', Input: escape(xmlStr), TransId: iTransId },
        success: function (data) {
            if (data.Message != undefined) {
                if (data.Status == '1') {
                    toastr.success("Deleted successfully...!");
                    GetDataList();
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            }
            else {
                window.location = GetLoginUrl();
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

const datasource1 = {
    getRows(params) {
        loaderShow();
        
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();

        $.ajax({
            url: "/Customer/GetUserDisc",
            async: false,
            type: "POST",
            data: { UserName: $("#txtCommonName").val(), PageNo: PageNo, PageSize: PageSize },
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Data != null && data.Data.length > 0) {
                            var searchSummary = data.Data[0].DataSummary;
                            params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);
                        }
                        else {
                            params.successCallback([], 0);
                        }
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        params.successCallback([], 0);
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
    }
};

function ExcelGet() {
    GetDataList();
    loaderShow();
    $.ajax({
        url: "/Customer/GetUserDisc_Excel",
        async: false,
        type: "POST",
        data: { UserName: $("#txtCommonName").val(), PageNo: 0, PageSize: 0 },
        success: function (data, textStatus, jqXHR) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data') > -1) {
                toastr.error(data);
            }
            else {
                location.href = data;
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            MoveToErrorPage(0);
            loaderHide();
        }
    });
    loaderHide();
}

function contentHeight() {
    var winH = $(window).height(),
        header = $(".order-title").height(),
        navbarHei = $(".order-history-data").height(),
        contentHei = winH - header - navbarHei - 120;
    $("#myGrid").css("height", contentHei);
}

function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetDataList();
}

$(document).ready(function () {
    GetDataList();
    contentHeight();

    $('#btnSearch').click(function () {
        GetDataList();
    });

    $('#btnReset').click(function () {
        $("#txtCommonName").val("");
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});

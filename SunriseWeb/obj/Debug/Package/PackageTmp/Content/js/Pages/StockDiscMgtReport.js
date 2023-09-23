var gridDiv = document.querySelector('#myGrid');
var gridOptions = {};
var showEntryVar = null;
var pgSize = 50;
var IsObj = false;
if ($('#hdnisadminflg').val() == 1) {
    IsObj = false;
} else {
    IsObj = true;
}
var columnDefs = [
    { headerName: "RowNo", field: "RowNo", width: 135, hide: true },
    { headerName: "Id", hide: true, field: "Id", tooltip: function (params) { return (params.value); } },
    { headerName: "iTransId", hide: true, field: "iTransId", tooltip: function (params) { return (params.value); } },

    { headerName: "Entry Date", field: "CreationDate", width: 135 },
    { headerName: "Customer Name", field: "CustName", tooltip: function (params) { return (params.value); }, width: 130 },
    { headerName: "Country", field: "sCompCountry", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "User Name", field: "Username", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Assist By", field: "AssistBy", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 130 },
    {
        headerName: "Active",
        field: "IsActive",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 50,
        sortable: true,
        cellRenderer: function (params) {
            if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; }
            else if (params.value == false) { return '<p class="spn-No1">NO</p>'; }
        }
    },
    { headerName: "Supplier", field: "iVendor", tooltip: function (params) { return (params.value); }, width: 300 },
    { headerName: "Location", field: "iLocation", tooltip: function (params) { return (params.value); }, width: 300 },
    { headerName: "Shape", field: "sShape", tooltip: function (params) { return (params.value); }, width: 300 },
    { headerName: "Carat", field: "sPointer", tooltip: function (params) { return (params.value); }, width: 300 },
    //{ headerName: "Color", field: "sColor", tooltip: function (params) { return (params.value); }, width: 190 },
    { headerName: "Color", field: "sColor", tooltip: function (params) { return (params.value); }, width: 300, cellRenderer: 'ColorData' },
    { headerName: "Clarity", field: "sClarity", tooltip: function (params) { return (params.value); }, width: 230 },
    { headerName: "Cut", field: "sCut", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Polish", field: "sPolish", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Symm", field: "sSymm", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Fls", field: "sFls", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Lab", field: "sLab", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "From Length", field: "dFromLength", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Length", field: "dToLength", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Width", field: "dFromWidth", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Width", field: "dToWidth", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Depth", field: "dFromDepth", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Depth", field: "dToDepth", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Depth Per.", field: "dFromDepthPer", tooltip: function (params) { return (params.value); }, width: 110 },
    { headerName: "To Depth Per.", field: "dToDepthPer", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Table Per.", field: "dFromTablePer", tooltip: function (params) { return (params.value); }, width: 110 },
    { headerName: "To Table Per.", field: "dToTablePer", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Cr Ang", field: "dFromCrAng", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Cr Ang", field: "dToCrAng", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Cr Ht", field: "dFromCrHt", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Cr Ht", field: "dToCrHt", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Pav Ang", field: "dFromPavAng", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Pav Ang", field: "dToPavAng", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "From Pav Ht", field: "dFromPavHt", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "To Pav Ht", field: "dToPavHt", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Key To Symbol", field: "dKeyToSymbol", tooltip: function (params) { return (params.value); }, width: 300 },
    { headerName: "BGM", field: "sBGM", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Crown Black", field: "sCrownBlack", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Table Black", field: "sTableBlack", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Crown White", field: "sCrownWhite", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Table White", field: "sTableWhite", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Used For", field: "View", tooltip: function (params) { return (params.value); }, width: 105, cellRenderer: 'UsedFor' },
    { headerName: "Image", field: "Img", tooltip: function (params) { return (params.value); }, width: 55 },
    { headerName: "Video", field: "Vdo", tooltip: function (params) { return (params.value); }, width: 55 },
    { headerName: "Price Method", field: "PriceMethod", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Price Per.", field: "PricePer", tooltip: function (params) { return (params.value); }, width: 80 },
    {
        headerName: $("#hdn_Remove").val(), field: "Action", width: 65, sortable: false,
        cellRenderer: function (params) {
            return '<img onClick="DeleteFilterModal(' + params.data.Id + ');" src="/Content/images/trash-delete-icon.png" style="cursor:pointer;width:17px;margin-top:-1px;" title="Delete Filter">';
        }
    },
];

var ColorData = function (params) {
    var element = "";
    if (params.data.sColor != null) {
        element += params.data.sColor;
    }
    if (params.data.sINTENSITY != null) {
        element += "<div style='margin-bottom:-9px;float:left;'><b> INTENSITY : </b>" + params.data.sINTENSITY + "</div>";
    }
    if (params.data.sOVERTONE != null) {
        element += "<div style='margin-bottom:-9px;float:left;'><b> OVERTONE : </b>" + params.data.sOVERTONE + "</div>";
    }
    if (params.data.sFANCY_COLOR != null) {
        element += "<div style='margin-bottom:-9px;float:left;'><b> FANCY COLOR : </b>" + params.data.sFANCY_COLOR + "</div>";
    }
    if (params.data.sColorType != null) {
        if (params.data.sColorType == "Regular") {
            element = "<b>REGULAR ALL</b>";
        }
        else if (params.data.sColorType == "Fancy") {
            element = "<b>FANCY ALL</b>";
        }
    }
    return element;
}
var UsedFor = function (params) {
    var UsedFor = "", View = "", Download = "";
    View = (params.data.View == true ? true : false);
    Download = (params.data.Download == true ? true : false);
    UsedFor = (View == true ? "View" : "");
    UsedFor += (Download == true ? (View == true ? ", Download" : "Download") : "");
    return UsedFor;
}

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetDataList() {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        rowHeight: 60,
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        defaultColDef: {
            enableSorting: true,
            sortable: false,
            resizable: true
        },
        components: {
            ColorData: ColorData,
            UsedFor: UsedFor
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
        //onGridReady: onGridReady,
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
const datasource1 = {
    getRows(params) {
        loaderShow();

        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();

        $.ajax({
            url: "/Customer/Get_StockDiscMgtReport",
            async: false,
            type: "POST",
            data: { UserName: $("#txtCommonName").val(), PageNo: PageNo, PageSize: PageSize },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        var rec = data.Data[0].iTransId;
                        data.Data.splice(0, 1);
                        params.successCallback(data.Data, rec);
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
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                MoveToErrorPage(0);
                loaderHide();
            }
        });
    }
};
var DeleteFilterModal = function (id) {
    $("#hdnDelId").val(id);
    $("#Remove").modal("show");
}

var ClearRemoveModel = function () {
    $("#hdnDelId").val("0");
    $("#Remove").modal("hide");
}
function RemoveDisc() {
    if ($("#hdnDelId").val() != "0" && $("#hdnDelId").val() != "") {
        var obj = {}
        obj.Type = "Delete";
        obj.Id = $("#hdnDelId").val();

        $.ajax({
            url: "/Customer/SaveStockDisc",
            async: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ savestockdiscreq: obj }),
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();

                if (data.Status == "1") {
                    $("#Remove").modal("hide");
                    toastr.success(data.Message);
                    GetDataList();
                }
                else if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.warning(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                toastr.error(textStatus);
            }
        });
    }
}

function ExcelGet() {
    GetDataList();
    loaderShow();
    $.ajax({
        url: "/Customer/Excel_StockDiscMgtReport",
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

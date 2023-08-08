var gridDiv = document.querySelector('#myGrid');
var gridOptions = {};
var showEntryVar = null;
var pgSize = 50;
var columnDefs = [
    { headerName: "RowNo", field: "RowNo", width: 135, hide: true },
    { headerName: "Id", hide: true, field: "Id", tooltip: function (params) { return (params.value); } },
    { headerName: "iTransId", hide: true, field: "iTransId", tooltip: function (params) { return (params.value); } },

    { headerName: "Entry Date", field: "CreationDate", width: 135 },
    { headerName: "Customer Name", field: "CustName", tooltip: function (params) { return (params.value); }, width: 130 },
    { headerName: "Country", field: "sCompCountry", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "User Name", field: "Username", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Assist By", field: "AssistBy", tooltip: function (params) { return (params.value); }, width: 130 },
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
    { headerName: "Color", field: "sColor", tooltip: function (params) { return (params.value); }, width: 190 },
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
    { headerName: "Image", field: "Img", tooltip: function (params) { return (params.value); }, width: 55 },
    { headerName: "Video", field: "Vdo", tooltip: function (params) { return (params.value); }, width: 55 },
    { headerName: "Price Method", field: "PriceMethod", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Price Per.", field: "PricePer", tooltip: function (params) { return (params.value); }, width: 80 },
];

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
            url: "/Api/Get_UploadMethodReport",
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

function ExcelGet() {
    GetDataList();
    loaderShow();
    $.ajax({
        url: "/Api/Excel_UploadMethodReport",
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

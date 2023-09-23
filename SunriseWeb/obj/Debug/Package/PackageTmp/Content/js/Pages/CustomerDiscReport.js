function DeleteGetter(params) {
    var parameter = params.data.CustDiscId + ',' + params.data.CustId  + ',\'' +
        params.data.sShape + '\',\'' + params.data.sLab + '\',' + params.data.rFromCts + ',' + params.data.rToCts + ',\'' +
        params.data.sFromColor + '\',\'' + params.data.sToColor + '\',\'' + params.data.sFromClarity + '\',\'' + params.data.sToClarity + '\',\'' +
        params.data.sFromCut + '\',\'' + params.data.sToCut + '\',\'' + params.data.sFromPolish + '\',\'' + params.data.sToPolish + '\',\'' + 
        params.data.sFromSymm + '\',\'' + params.data.sToSymm + '\',\'' + params.data.sFromFls + '\',\'' + params.data.sToFls + '\',' +
        params.data.rDisc + ',\'' + params.data.DiscType + '\',\'' + params.data.sSign + '\',\'' + params.data.Supplier + '\'';

    return '<a href="javascript:void(0);" onClick="RemoveDisc(' + parameter + ');" title="Delete"><i class="fa fa-trash fa-2x"></i></a>';
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

var columnDefs = [
    { headerName: "CustomerId", hide: true, field: "CustId", tooltip: function (params) { return (params.value); } },
    { headerName: "User Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Supplier", field: "Supplier", tooltip: function (params) { return (params.value); }, width: 200 },
    //{ headerName: "Vendor", field: "sPartyName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Shape", field: "sShape", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Lab", field: "sLab", tooltip: function (params) { return (params.value); }, width: 80 },
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
    { headerName: "From Clarity", field: "sFromClarity", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "To Clarity", field: "sToClarity", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Cut", field: "sFromCut", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Cut", field: "sToCut", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Polish", field: "sFromPolish", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Polish", field: "sToPolish", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "From Symm", field: "sFromSymm", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "To Symm", field: "sToSymm", tooltip: function (params) { return (params.value); }, width: 80 },
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
    { headerName: "Disc Type", field: "DiscType", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Sign", field: "sSign", tooltip: function (params) { return (params.value); }, width: 80 },
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

function RemoveDisc(iTransId, CustId, sShape, sLab,
    rFromCts, rToCts, sFromColor, sToColor, sFromClarity, sToClarity,
    sFromCut, sToCut, sFromPolish, sToPolish, sFromSymm, sToSymm, sFromFls, sToFls, rDisc, DiscType, sSign, Supplier) {

    var xmlStr = '<REQUEST>';
    xmlStr += '<ROW><SHAPE>' + sShape + '</SHAPE>';
    xmlStr += '<VENDOR>' + Supplier + '</VENDOR>';
    xmlStr += '<LAB>' + sLab + '</LAB>';
    xmlStr += '<FROMCTS>' + rFromCts + '</FROMCTS>';
    xmlStr += '<TOCTS>' + rToCts + '</TOCTS>';
    xmlStr += '<FROMCOLOR>' + sFromColor + '</FROMCOLOR>';
    xmlStr += '<TOCOLOR>' + sToColor + '</TOCOLOR>';
    xmlStr += '<FROMCLARITY>' + sFromClarity + '</FROMCLARITY>';
    xmlStr += '<TOCLARITY>' + sToClarity + '</TOCLARITY>';
    xmlStr += '<FROMCUT>' + sFromCut + '</FROMCUT>';
    xmlStr += '<TOCUT>' + sToCut + '</TOCUT>';
    xmlStr += '<FROMPOLISH>' + sFromPolish + '</FROMPOLISH>';
    xmlStr += '<TOPOLISH>' + sToPolish + '</TOPOLISH>';
    xmlStr += '<FROMSYMM>' + sFromSymm + '</FROMSYMM>';
    xmlStr += '<TOSYMM>' + sToSymm + '</TOSYMM>';
    xmlStr += '<FROMFLS>' + sFromFls + '</FROMFLS>';
    xmlStr += '<TOFLS>' + sToFls + '</TOFLS>';
    xmlStr += '<DISC>' + rDisc + '</DISC>';
    xmlStr += '<DISCTYPE>' + DiscType + '</DISCTYPE>';
    xmlStr += '<SIGN>' + sSign + '</SIGN>';
    xmlStr += '</ROW></REQUEST>';

    $.ajax({
        url: "/Customer/SaveCustomerDisc",
        async: false,
        type: "POST",
        data: { CustId: CustId, Oper: 'Delete', Input: escape(xmlStr), TransId: iTransId },
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Message != undefined) {
                if (data.Status == '1') {
                    toastr.success("Deleted successfully...!");
                    GetDataList();
                    //GetCustomerDiscData();
                }
                else {
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
            url: "/Customer/GetCustomerDisc",
            async: false,
            type: "POST",
            data: { UserName: $("#txtCommonName").val(), PageNo: PageNo, PageSize: PageSize },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
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

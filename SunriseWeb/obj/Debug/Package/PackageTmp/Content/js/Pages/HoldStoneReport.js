var GridpgSize = 50;
var gridOptions = {};
var IsObj = false;
var searchSummary = {};
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var rowData = [];

if ($('#hdnisadminflg').val() == 1) {
    IsObj = false;
} else {
    IsObj = true;
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
    GET_Scheme_Disc();
    GetHoldData();
    contentHeight();
});

var gridDiv = document.querySelector('#myGrid');
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 20,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "Hold Date Time", field: "Trans_date", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "stone_ref_no", width: 80, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
        }
    },
    {
        headerName: $("#hdn_View_Image").val(), field: "ImagesLink", width: 80, tooltip: function (params) { return (""); }, cellRenderer: ImageValueGetter, suppressSorting: true,
        suppressMenu: true,
    },
    {
        headerName: $("#hdn_Status").val(), field: "StoneStatus", width: 50,
        cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            return params.data.StoneStatus;
        }, filter: false, sortable: false
    },
    { headerName: "Hold Party Name", field: "Party_Name", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: "Hold Assist By", field: "Hold_Username", tooltip: function (params) { return (params.value); }, width: 60, sortable: true, hide: IsObj },
    {
        headerName: "Hold Party Code", field: "Hold_Party_Code", tooltip: function (params) { return (params.value == 0 ? '' : params.value); },
        cellRenderer: function (params) { return (params.value == 0 ? '' : params.value); },
        width: 60, sortable: true
    },
    { headerName: $("#hdn_Location").val(), field: "Location", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Shape").val(), field: "shape", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Pointer").val(), field: "pointer", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Lab").val(), field: "lab", tooltip: function (params) { return (params.value); }, width: 40, cellRenderer: LotValueGetter, sortable: false },
    { headerName: $("#hdn_Certi_No").val(), field: "certi_no", tooltip: function (params) { return (params.value); }, width: 80, sortable: true },
    { headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    { headerName: $("#hdn_Color").val(), field: "color", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_Clarity").val(), field: "clarity", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_CTS").val(), field: "cts", tooltip: function (params) { return (params.value); }, width: 75, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Rap_Price_Doller").val(), field: "cur_rap_rate", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Rap_Amt_Doller").val(), field: "rap_amount", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Offer_Disc_Per").val(), field: "sales_disc_per", tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 90, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { if (params.value != 0) { return parseFloat(params.value).toFixed(2); } }, sortable: true },
    { headerName: $("#hdn_Offer_Value_Dollar").val(), field: "net_amount", tooltip: function (params) { return formatNumber(params.value); }, width: 105, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Final_Disc_Per").val(), field: "Final_Disc", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1' }, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true },
    { headerName: $("#hdn_Final_Value").val(), field: "Final_Value", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1' }, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    {
        headerName: $("#hdn_Cut").val(), field: "cut", tooltip: function (params) { return (params.value); }, width: 50,
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        cellStyle: function (params) {
            if (params.data) {
                if (params.value == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: $("#hdn_Polish").val(), field: "polish", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },
    {
        headerName: $("#hdn_Symm").val(), field: "symm", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },

    { headerName: $("#hdn_Fls").val(), field: "fls", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_Length").val(), field: "length", tooltip: function (params) { return (params.value); }, width: 65, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Width").val(), field: "width", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Depth").val(), field: "depth", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Depth_Per").val(), field: "depth_per", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Table_Per").val(), field: "table_per", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Key_to_symbol").val(), field: "symbol", tooltip: function (params) { return (params.value); }, width: 350 },
    { headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    { headerName: $("#hdn_Table_Black").val(), field: "table_natts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Natts").val(), field: "Crown_Natts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Table_White").val(), field: "inclusion", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: $("#hdn_Crown_White").val(), field: "Crown_Inclusion", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "crown_angle", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_CR_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "crown_height", width: 50, cellRenderer: function (params) { return formatNumber(params.value); }, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "pav_angle", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "pav_height", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, cellStyle: function (params) { return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' }; } },
    { headerName: $("#hdn_Table_Open").val(), tooltip: function (params) { return (params.value); }, field: "Table_Open", width: 75, filter: false },
    { headerName: $("#hdn_Crown_Open").val(), tooltip: function (params) { return (params.value); }, field: "Crown_Open", width: 80, filter: false },
    { headerName: $("#hdn_Pav_Open").val(), tooltip: function (params) { return (params.value); }, field: "Pav_Open", width: 70, filter: false },
    { headerName: $("#hdn_Girdle_Open").val(), tooltip: function (params) { return (params.value); }, field: "Girdle_Open", width: 80, filter: false },
    { headerName: ($("#hdn_girdle").val() + "(%)"), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) { return (params.value); }, field: "girdle_type", width: 90 },
    { headerName: $("#hdn_Laser_in_SC").val(), field: "sInscription", width: 90, tooltip: function (params) { return (params.value); }, },
    { headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Cur_Status", field: "Cur_Status", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Party_Name", field: "Party_Name", cellRenderer: function (params) { return params.value; }, hide: true },
];
function ImageValueGetter(params) {
    if (params.data != undefined) {
        return params.data.ImagesLink;
    }
    else {
        return '';
    }
}
function LotValueGetter(params) {
    $('.offercls').parent().addClass('offerrow');
    $('.upcomingcls').parent().addClass('upcomingrow');

    if (params.data.sCertiNo != "") {
        if (params.data != undefined) {
            if (params.value == "GIA") {
                return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
            }
            else if (params.value == "HRD") {
                return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
            }
            else if (params.value == "IGI") {
                return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
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
        return '<span style="color :blue;">' + params.value + '</span>';
    }
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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
    GetHoldData();
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
function GetHoldData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        components: {
            ImageValueGetter: ImageValueGetter,
            LotValueGetter: LotValueGetter
        },
        defaultColDef: {
            enableSorting: true,
            sortable: true,
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
        onSelectionChanged: onSelectionChanged,
        onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize,
        paginationPageSize: GridpgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        },
    };

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

        gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
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
function onSelectionChanged() {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var CUR_RAP_RATE = 0;
    var Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    if (gridOptions.api.getSelectedRows().length > 0) {
        dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
        CUR_RAP_RATE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;

        if (Scheme_Disc_Type == "Discount") {
            Net_Value = 0;
            Final_Disc = 0;
            Web_Benefit = 0;
        }
        else if (Scheme_Disc_Type == "Value") {
            Net_Value = (parseFloat(TOT_NET_AMOUNT) + (parseFloat(TOT_NET_AMOUNT) * parseFloat(Scheme_Disc) / 100)).toFixed(2);
            Final_Disc = (((1 - parseFloat(Net_Value) / parseFloat(TOT_RAP_AMOUNT)) * 100) * -1).toFixed(2);
            Web_Benefit = (parseFloat(TOT_NET_AMOUNT) - parseFloat(Net_Value)).toFixed(2);
        }
        else {
            Net_Value = parseFloat(TOT_NET_AMOUNT);
            Final_Disc = parseFloat(AVG_SALES_DISC_PER);
            Web_Benefit = 0;
        }
        if (CUR_RAP_RATE == 0) {
            Final_Disc = 0;
            AVG_SALES_DISC_PER = 0;
        }
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();

        $('#tab1TCount').show();
        $('#tab1pcs').html(TOT_PCS);
        $('#tab1cts').html(formatNumber(TOT_CTS));
        $('#tab1disc').html(formatNumber(AVG_SALES_DISC_PER));
        $('#tab1totAmt').html(formatNumber(TOT_NET_AMOUNT));
        $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
        $('#tab1Net_Value').html(formatNumber(Net_Value));
        $('#tab1Final_Disc').html(formatNumber(Final_Disc));
    } else {
        TOT_CTS = searchSummary.TOT_CTS;
        AVG_SALES_DISC_PER = searchSummary.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = searchSummary.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = searchSummary.TOT_NET_AMOUNT;
        TOT_PCS = searchSummary.TOT_PCS;
        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
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
        var CommonName = $('#txtCompanyName').val();
        var RefNo = $('#txtStoneId').val();
        $.ajax({
            url: "/Order/GetHoldHistory",
            async: false,
            type: "POST",
            data: {
                PageNo: PageNo, OrderBy: orderBy, PageSize: GridpgSize, CommonName: CommonName, RefNo: RefNo
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
                    $('#tab1pcs').html(searchSummary.TOT_PCS);
                    $('#tab1cts').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1disc').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    $('#tab1totAmt').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1TCount').hide();
                    $('#tab1pcs').html('0');
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                }
                if ($('#myGrid .ag-paging-panel').length > 0) {

                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $('#ddlPagesize').val(GridpgSize);
                    clearInterval(showEntryVar);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                contentHeight();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
function GET_Scheme_Disc() {
    $.ajax({
        url: "/SearchStock/GET_Scheme_Disc",
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            Scheme_Disc_Type = '';
            Scheme_Disc = "0";
            if (data.Data != null) {
                if (data.Data.length != 0) {
                    if (data.Data[0].Discount != null) {
                        Scheme_Disc_Type = 'Discount';
                        Scheme_Disc = data.Data[0].Discount;
                    }
                    if (data.Data[0].Value != null) {
                        Scheme_Disc_Type = 'Value';
                        Scheme_Disc = data.Data[0].Value;
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
var ReleaseStoneModal = function () {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no') != "" && count != 0) {
        $("#ReleaseMsgBox").modal("show");
    }
    else {
        toastr.warning($("#hdn_No_stone_selected_for_release").val());
    }
}
var ClearRemoveModel = function () {
    $("#ReleaseMsgBox").modal("hide");
}
var ReleaseStone = function () {
    debugger
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no') != "" && count != 0) {
        debugger
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $("#ReleaseMsgBox").modal("hide");

        setTimeout(function () {
            debugger
            var SelectedRows = gridOptions.api.getSelectedRows();
            var List = [], StoneID = "";

            StoneID = _.pluck(_.filter(SelectedRows), 'stone_ref_no').join(",");
            debugger
            $.each(SelectedRows, function (propName, propVal) {
                debugger
                List.push({
                    sRefNo: propVal.stone_ref_no,
                    Hold_Party_Code: (propVal.Hold_Party_Code == 0 ? "0" : propVal.Hold_Party_Code),
                    Hold_CompName: propVal.Party_Name,
                    Status: propVal.Cur_Status
                });
            });
            debugger
            var obj = {};
            obj.StoneID = StoneID;
            obj.Hold_Stone_List = List;
            debugger
            $.ajax({
                url: "/SearchStock/ReleaseStone_1",
                async: false,
                type: "POST",
                dataType: "json",
                data: JSON.stringify({ req: obj }),
                contentType: "application/json; charset=utf-8",
                success: function (data, textStatus, jqXHR) {
                    debugger
                    if (data.Status == "0") {
                        debugger
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else {
                        debugger
                        if (data.Status == "SUCCESS") {
                            debugger
                            toastr.success(data.Message.toString());
                            GetHoldData();
                        }
                        else if (data.Status == "FAIL") {
                            debugger
                            toastr.warning(data.Message.toString());
                        }
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
    else {
        $("#ReleaseMsgBox").modal("hide");
        toastr.warning($("#hdn_No_stone_selected_for_release").val() + '!');
    }
}
function reset() {
    $('#txtStoneId').val("");
    $('#txtCompanyName').val("");
    setTimeout(function () {
        GetHoldData();
    }, 1);
}
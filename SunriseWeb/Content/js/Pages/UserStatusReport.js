var Excel_OrderBy = "", Excel_PageNo = "", Excel_PageSize = "";
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 7));
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function Seven_Day_Date_Set() {
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
$(document).ready(function () {
    Seven_Day_Date_Set();
    GetSearch();
});

function greaterThanDate(evt) {
    var fDate = $.trim($('#txtFromDate').val());
    var tDate = $.trim($('#txtToDate').val());
    if (fDate != "" && tDate != "") {
        if (new Date(tDate) >= new Date(fDate)) {
            return true;
        }
        else {
            evt.currentTarget.value = "";
            toastr.warning("To date must be greater than From date !");
            return false;
        }
    }
    else {
        return true;
    }
}

var pgSize = 50;
var showEntryVar = null;
var orderBy = '';
var columnDefs = [
    { headerName: "SR", field: "iSr", tooltip: function (params) { return (params.value); }, width: 40, sortable: false },
    { headerName: "Created Date", field: "sCreatedDate", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    { headerName: "Customer Name", field: "sFullName", tooltip: function (params) { return (params.value); }, width: 115, sortable: false },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 115, sortable: false },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    { headerName: "Activity", field: "Activity", tooltip: function (params) { return (params.value); }, width: 85, sortable: true },
    { headerName: "Activity Date", field: "ActivityDate", tooltip: function (params) { return (params.value); }, width: 130, sortable: true },
];
var gridOptions = {};
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetSearch();
}
function Reset() {
    Seven_Day_Date_Set();
    $('#ddlActivityStatus').val("");
    GetSearch();
}
function GetSearch() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 2,
        components: {
            myDetailCellRenderer: DetailCellRenderer
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
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize, // you can have your custom page size
        paginationPageSize: pgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

    var showEntryHtml = '<div class="show_entry">'
        + '<label>Show <select id="ddlPagesize" onchange="onPageSizeChanged()">'
        + '<option value="50">50</option>'
        + '<option value="100">100</option>'
        + '<option value="500">500</option>'
        + '</select> entries</label></div>';

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(pgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);

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
    });
}

const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;;
        var PageSize = pgSize;
        if (params.request.sortModel.length > 0) {
            orderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        Excel_OrderBy = orderBy, Excel_PageNo = PageNo, Excel_PageSize = PageSize;

        var obj1 = {
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            ActivityStatus: $('#ddlActivityStatus').val(),
            OrderBy: orderBy,
            //PageNo: PageNo,
            //PageSize: PageSize,
        };

        $.ajax({
            url: "/User/Get_UserStatusReport",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                }
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
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
$(window).resize(function () {
    contentHeight();
});
function ExcelData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    setTimeout(function () {
        var formData1 = {
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            ActivityStatus: $('#ddlActivityStatus').val(),
            OrderBy: Excel_OrderBy,
            PageNo: Excel_PageNo,
            PageSize: Excel_PageSize,
        };
        $.ajax({
            url: "/User/Excel_UserStatusReport",
            async: false,
            async: false,
            type: "POST",
            data: formData1,
            success: function (data, textStatus, jqXHR) {
                if (data.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                else if (data.indexOf('No data') > -1) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                    toastr.error(data);
                }
                else {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                    location.href = data;
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }, 500);
}
var pgSize = 50;
var showEntryVar = null;
var orderBy = '';
var columnDefs = [
    { headerName: "TotalStock", field: "TotalStock", tooltip: function (params) { return (params.value); }, width: 130, sortable: false },
    { headerName: "Message", field: "Message", tooltip: function (params) { return (params.value); }, width: 575, sortable: false },
    { headerName: "Trans Date", field: "DateTime", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    { headerName: "Status", field: "Status", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
];

var gridOptions = {};

function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetSearch();
}

var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 5));
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
function One_Day_Date_Set() {
    $('#txtFromDate').val(SetCurrentDate());
    $('#txtToDate').val(SetCurrentDate());

    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: SetCurrentDate(),
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
    One_Day_Date_Set();
    GetSearch();

    $('#btnSearch').on('click', function () {
        GetSearch();
    });
    
});
function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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
function ResetData() {
    One_Day_Date_Set();
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

        var obj1 = {
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            PageNo: PageNo,
            PageSize: PageSize,
        };

        $.ajax({
            url: "/Login/Ora_Stock_History",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                    
                    $("#a_Last_Excel_Gen_Link").attr("href", data.Data[0].Full_Stock_Excel_Gen_URL);
                    $("#a_Last_Excel_Gen_Link").html(data.Data[0].Full_Stock_Excel_Gen_DateTime);
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();

                    $("#a_Last_Excel_Gen_Link").attr("href", "");
                    $("#a_Last_Excel_Gen_Link").html("-");
                }
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

$(document).ready(function () {
    $('.sym-sec').on('click', function () {
        $('.sym-sec').toggleClass('active');
    });
});
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        resultHei = $(".order-history-data").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 70;
        contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    contentHeight();
});
$(window).resize(function () {
    contentHeight();
});

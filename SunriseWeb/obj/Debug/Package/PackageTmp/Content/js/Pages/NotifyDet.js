var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 7));
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

var GridpgSize = 50;
var gridOptions = {};
var EditView = 0;
var SEARCH = '';
var NotifyId = null;
var IsFrist = 1;
var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';
function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    GridpgSize = Number(value);
    GetDataList();
}


var columnDefs = [
    { headerName: "Sr.", field: "SrNo", sortable: false, hide: true, tooltip: function (params) { return (params.value); }, width: 30 },
    { headerName: "NotifyId", field: "NotifyId", sortable: false, hide: true, tooltip: function (params) { return (params.value); }, width: 30 },
    { headerName: "Valid From", field: "ValidityFromDate", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Valid To", field: "ValidityToDate", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Message", field: "Message", sortable: true, tooltip: function (params) { return (params.value); }, width: 600 },
    { headerName: "Entry Date", field: "CreationDate", sortable: true, tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Total User", field: "TotalUser", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Dismiss User", field: "DismissUser", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: ActionPage, },
];

function ActionPage(params) {
    return '<a hrfe="#" onclick="EditData(\'' + params.data.NotifyId + '\')" ><i class="fa fa-eye" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
}
function EditData(row) {
    //window.open('/User/Notify?NotifyId=' + row);
    location.href = '/User/Notify?NotifyId=' + row;
}
function AddNew() {
    location.href = '/User/Notify';
}

var gridDiv = document.querySelector('#myGrid');
function GetDataList() {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        rowHeight: 35,
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
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize,
        paginationPageSize: GridpgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(GridpgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);
}

const datasource1 = {
    getRows(params) {
        var OrderBy = '', PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var formData = new FormData();


        formData.append('FromDate', $("#txtFromDate").val());
        formData.append('ToDate', $("#txtToDate").val());
        formData.append('PageNo', PageNo);
        formData.append('PageSize', GridpgSize);
        formData.append('OrderBy', OrderBy);

        $.ajax({
            url: "/User/NotifyDetList",
            async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Data != null && data.Data.length > 0) {
                            params.successCallback(data.Data, data.Data[0].iTotalRec);
                        }
                        else {
                            params.successCallback([], 0);
                            if (IsFrist == 0) {
                                toastr.warning(data.Message);
                            }
                            IsFrist = 0;
                        }
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        params.successCallback([], 0);
                        toastr.error(data.Message);
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                MoveToErrorPage(0);
            }
        });

    }
};
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-history-data").height(),
        contentHei = winH - navbarHei - 170;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    $('#txtFromDate').val(F_date);
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

    GetDataList();
    contentHeight();

   
});

$(window).resize(function () {
    contentHeight();
});

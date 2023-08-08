var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var GridpgSize = 50;
var gridOptions = {};
var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
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

function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

var columnDefs = [
    { headerName: "ID", field: "iId", sortable: true, tooltip: function (params) { return (params.value); }, width: 50 },
    { headerName: "Error Date", field: "ErrorDate", sortable: true, tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "User Name", field: "sUsername", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Full Name", field: "sFullName", sortable: true, tooltip: function (params) { return (params.value); }, width: 130 },
    { headerName: "Company Name", field: "sCompName", sortable: true, tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "IP Address", field: "sIPAddress", sortable: true, tooltip: function (params) { return (params.value); }, width: 100 },
    {
        headerName: "Error Trace", field: "sErrorTrace", sortable: true, tooltip: function (params) { return (params.value); }, width: 1000,
        cellStyle: function (params) {
            return { alignment: 'left' };
        }},
    { headerName: "Error Message", field: "sErrorMsg", sortable: true, tooltip: function (params) { return (params.value); }, width: 1000 },
    { headerName: "Error Site", field: "sErrorSite", sortable: true, tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Error Page", field: "sErrorPage", sortable: true, tooltip: function (params) { return (params.value); }, width: 150 },
];


var gridDiv = document.querySelector('#myGrid');
function GetDataList() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        rowHeight: 80,
        wrapText: true, 
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        //detailRowHeight: 70,
        groupDefaultExpanded: 1,
        //components: {
        //    dateIndicator: dateIndicator
        //},
        defaultColDef: {
            enableSorting: true,
            sortable: false,
            width: 150,
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
        cacheBlockSize: GridpgSize, // you can have your custom page size
        paginationPageSize: GridpgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(GridpgSize);
        }
        clearInterval(showEntryVar);
    }, 1000);

    setTimeout(function () {
        var allColumnIds = [];
        gridOptions.columnApi.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });

        //gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);
}

const datasource1 = {
    getRows(params) {
        
        //console.log(JSON.stringify(params.request, null, 1));
        var OrderBy = '', PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        } 
        var formData = new FormData();
        
        if ($("#txtMSearch").val() != '' && $("#txtMSearch").val() != null) {
            formData.append('MSearch', $("#txtMSearch").val());
        }
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('PageNo', PageNo);
        formData.append('PageSize', GridpgSize);
        formData.append('OrderBy', OrderBy);
        $.ajax({
            url: "/User/ErrorLogMst",
            async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Data != null && data.Data.length > 0) {
                            params.successCallback(data.Data, data.Data[0].iTotalRec);
                        }
                        else {
                            params.successCallback([], 0);
                            //toastr.error('No data found.');
                        }
                    } else {
                        params.successCallback([], 0);
                        //toastr.error(data.Message);
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
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 70;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    GetDataList();
    contentHeight();
    $('#btnSearch').click(function () {
        GetDataList();
    });

    

    $('#btnReset').click(function () {
        $("#txtMSearch").val("");
        $('#txtFromDate').val(SetCurrentDate());
        $('#txtToDate').val(SetCurrentDate());
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});

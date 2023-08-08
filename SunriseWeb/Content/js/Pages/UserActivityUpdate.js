var OrderBy = '';
var GridpgSize = 50;
var gridOptions = {};
//var FromDate = new Date();

//FromDate = new Date(FromDate.setDate(FromDate.getDate() - 7));

//var ToDate = F_date;

//var date = new Date(FromDate),
//    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
//    day = ("0" + date.getDate()).slice(-2);
//FromDate = [day, m_names[mnth - 1], date.getFullYear()].join("-");

//var currMonthInd = parseInt(mnth) - 1;

//if (currMonthInd == 0)
//    FromDate = [day, m_names[11], (date.getFullYear() - 1)].join("-");
//else
//    FromDate = [day, m_names[currMonthInd - 1], (date.getFullYear())].join("-");

var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 0));
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var FromDate = [day, m_names[mnth - 1], date.getFullYear()].join("-");

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function Thirty_Day_Date_Set() {
    $('#txtFromDate').val(FromDate);
    $('#txtToDate').val(SetCurrentDate());

    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: FromDate,
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

var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    GridpgSize = Number(value);
    GetDataList();
    //GetOrderData();
}

function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

var columnDefs = [
    { headerName: "Trans ID", field: "iTransId", sortable:true, tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Trans Date", field: "dTransFullDate", sortable: true, tooltip: function (params) { return (params.value); }, width: 125 },
    { headerName: "User Name", field: "sUsername", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Company Name", field: "sCompName", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Lab", field: "sLab", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Cts", field: "Cts", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Shape", field: "sShape", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Color", field: "sColor", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Clarity", field: "sClarity", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Cut", field: "sCut", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Polish", field: "sPolish", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Symm", field: "sSymm", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Fls", field: "sFls", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Disc", field: "Disc", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Pointer", field: "sPointer", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Rap Amount", field: "RapAmount", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Net Amount", field: "NetAmount", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Length", field: "Length", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Width", field: "Width", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Depth", field: "Depth", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Depth(%)", field: "DepthPer", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Table(%)", field: "TablePer", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Crn Ang", field: "CrAng", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Crn Ht", field: "CrHt", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Pav Ang", field: "PavAng", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Pav Ht", field: "PavHt", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Shade", field: "sShade", sortable: true,tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: "Table Inclusion", field: "sInclusion", sortable: true,tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Table Natts", field: "sTableNatts", sortable: true,tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Crown Inclusion", field: "sCrownInclusion", sortable: true,tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Crown Natts", field: "sCrownNatts", sortable: true,tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Milky Luster", field: "sLuster", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Location", field: "sLocation", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Status", field: "sStatus", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Form Name", field: "sFormName", sortable: true,tooltip: function (params) { return (params.value); }, width: 135 },
    { headerName: "Activity", field: "sActivityType", sortable: true,tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Browser Name", field: "BrowserName", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "IP Address", field: "IPAddr", sortable: true,tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Login Type", field: "LoginType", sortable: true,tooltip: function (params) { return (params.value); }, width: 80 },
];

var gridDiv = document.querySelector('#myGrid');

function GetDataList() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        //components: {
        //    dateIndicator: dateIndicator
        //},
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

        gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);
}

const datasource1 = {
    getRows(params) {
        
        //console.log(JSON.stringify(params.request, null, 1));
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();
        var UserFullName = '', UserName = '', CompanyName = '', CountryName = '', ComUserName = '';
        if ($("#ddlType").val() == "UserName") {
            UserName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Company") {
            CompanyName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Country") {
            CountryName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Customer") {
            UserFullName = $("#txtCommonName").val();
        }
        if (params.request.sortModel.length > 0) {
            OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        ComUserName = $("#txtCompanyName").val();
        var formData = new FormData();
        
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('UserFullName', UserFullName);
        formData.append('UserName', UserName);
        formData.append('CompanyName', CompanyName);
        formData.append('CountryName', CountryName);
        formData.append('PageNo', PageNo);
        formData.append('PageSize', GridpgSize);
        formData.append('OrderBy', OrderBy);
        formData.append('ComUserName', ComUserName);

        $.ajax({
            url: "/UserActivity/GetUserActivityUpdateList",
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
                            //toastr.error('No data found.');
                        }
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
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
        header = $(".order-title").height(),
        navbarHei = $(".order-history-data").height(),
        contentHei = winH - header - navbarHei - 110;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    Thirty_Day_Date_Set();
    GetDataList();
    contentHeight();
    $('#btnSearch').click(function () {
        GetDataList();
    });

    $('#btnExcel').click(function () {
        loaderShow();

        var UserFullName = '', UserName = '', CompanyName = '', CountryName = '', ComUserName = '';
        if ($("#ddlType").val() == "UserName") {
            UserName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Company") {
            CompanyName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Country") {
            CountryName = $("#txtCommonName").val();
        }
        else if ($("#ddlType").val() == "Customer") {
            UserFullName = $("#txtCommonName").val();
        }
        ComUserName = $("#txtCompanyName").val();

        var formData = new FormData();
        
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('UserFullName', UserFullName);
        formData.append('UserName', UserName);
        formData.append('CompanyName', CompanyName);
        formData.append('CountryName', CountryName);
        formData.append('ComUserName', ComUserName);

        $.ajax({
            url: "/UserActivity/DownloadActivityUpdateList",
            //async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {debugger
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        else if (data.Message.indexOf('No data found') > -1) {
                            toastr.error(data.Message);
                        }
                        else if (data.Message.indexOf('ExcelFile') > -1) {
                            location.href = data.Message;
                        }
                        else {
                            toastr.error(data.Message);
                        }
                    }
                    else {
                        toastr.error(data.Message);
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
    });

    $('#btnReset').click(function () {
        Thirty_Day_Date_Set();
        $("#ddlType").val("");
        $("#txtCommonName").val("");
        $("#txtCompanyName").val("");
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});

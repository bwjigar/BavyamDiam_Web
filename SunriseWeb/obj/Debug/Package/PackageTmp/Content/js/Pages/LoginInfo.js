function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

var columnDefs = [
    { headerName: "LoginDate", field: "LoginDate", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "LoginTime", field: "LoginTime", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Customer Name", field: "CustomerName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Country Name", field: "sCompCountry", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Login Type", field: "LoginType", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Browser", field: "BrowserName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "OS", field: "Platform", tooltip: function (params) { return (params.value); }, width: 200 },
];

var gridDiv = document.querySelector('#myGrid');
var gridOptions = {
    masterDetail: true,
    detailCellRenderer: 'myDetailCellRenderer',
    detailRowHeight: 70,
    groupDefaultExpanded: 1,
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
    rowModelType: 'serverSide',
    cacheBlockSize: 50, // you can have your custom page size
    paginationPageSize: 50, //pagesize
    paginationNumberFormatter: function (params) {
        return '[' + params.value.toLocaleString() + ']';
    }
};

function GetDataList() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);
}

function contentHeight() {
    var winH = $(window).height(),
        header = $(".order-title").height(),
        navbarHei = $(".order-history-data").height(),
        contentHei = winH - header - navbarHei - 110;
    $("#myGrid").css("height", contentHei);
}

const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();
        var OrderBy = '';
        if (params.request.sortModel.length > 0) {
            OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }

        var UserFullName = '', UserName = '', CompanyName = '', CountryName = '';
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

        var formData = new FormData();

        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('UserFullName', UserFullName);
        formData.append('UserName', UserName);
        formData.append('CompanyName', CompanyName);
        formData.append('CountryName', CountryName);
        formData.append('PageNo', PageNo);
        formData.append('PageSize', PageSize);
        formData.append('OrderBy', OrderBy);
        
        $.ajax({
            url: "/LoginInfo/GetLoginInfoList",
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

$(document).ready(function () {
    GetDataList();
    contentHeight();

    $('#btnSearch').click(function () {
        GetDataList();
    });

    $('#btnExcel').click(function () {
        loaderShow();

        var UserFullName = '', UserName = '', CompanyName = '', CountryName = '';
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

        var formData = new FormData();

        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('UserFullName', UserFullName);
        formData.append('UserName', UserName);
        formData.append('CompanyName', CompanyName);
        formData.append('CountryName', CountryName);
        formData.append('FormName', 'Login Info');
        formData.append('ActivityType', 'Excel Export');

        $.ajax({
            url: "/LoginInfo/DownloadLoginInfoList",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        else if (data.Message.indexOf('No data found.') > -1) {
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
        $("#ddlType").val("");
        $("#txtCommonName").val("");
        $('#txtFromDate').val(F_date);
        $('#txtToDate').val(F_date);
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});

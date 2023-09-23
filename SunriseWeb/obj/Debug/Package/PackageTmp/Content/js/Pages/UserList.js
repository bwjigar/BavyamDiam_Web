function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

var columnDefs = [
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "User Name", field: "UserName", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "User Type", field: "UserType", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Country", field: "Country", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Join Date", field: "JoinDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Email", field: "Email", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Mobile No", field: "MobileNo", tooltip: function (params) { return (params.value); }, width: 120 },
];


var gridDiv = document.querySelector('#myGrid');
var gridOptions = {
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
    rowModelType: 'serverSide',
    cacheBlockSize: 50, // you can have your custom page size
    paginationPageSize: 50, //pagesize
    paginationNumberFormatter: function (params) {
        return '[' + params.value.toLocaleString() + ']';
    }
};

function GetDataList() {

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);
}

const datasource1 = {
    getRows(params) {
        loaderShow();
        //console.log(JSON.stringify(params.request, null, 1));
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();
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
        formData.append('UserType', $('#ddlUserType').val());
        formData.append('PageNo', PageNo);
        formData.append('PageSize', PageSize);

        $.ajax({
            url: "/User/GetUserList",
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
        formData.append('UserType', $('#ddlUserType').val());
        formData.append('FormName', 'User List');
        formData.append('ActivityType', 'Excel Export');

        $.ajax({
            url: "/User/DownloadUserList",
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
                        else if (data.Message.indexOf('No data') > -1) {
                            toastr.error(data);
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
        $('#ddlUserType').val("");
        $("#txtCommonName").val("");
        $('#txtFromDate').val(F_date);
        $('#txtToDate').val(F_date);
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});

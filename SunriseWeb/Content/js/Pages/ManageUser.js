var gridOptions = {};
var iUserid = 0;
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
function ddlFilterType() {
    if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LAD" || $("#ddlFilterType").val() == "LLD") {
        $("#divDatetime").show();
        $("#divWithoutDatetime").hide();
        $("#txtCommonName").val("");
        fromto_dt();
    }
    else {
        $("#divDatetime").hide();
        $("#divWithoutDatetime").show();
    }
}
function fromto_dt() {
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
    }).on('change', function (e) {
        greaterThanDate(e);
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: SetCurrentDate(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDate(e);
    });
}
function greaterThanDate(evt) {
    if ($.trim($('#txtToDate').val()) != "") {
        var fDate = $.trim($('#txtFromDate').val());
        var tDate = $.trim($('#txtToDate').val());
        if (fDate != "" && tDate != "") {
            if (new Date(tDate) >= new Date(fDate)) {
                return true;
            }
            else {
                evt.currentTarget.value = "";
                toastr.warning($("#hdn_To_date_must_be_greater_than_From_date").val());
                fromto_dt();
                return false;
            }
        }
        else {
            return true;
        }
    }
}

var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

var columnDefs = [
    { headerName: "iUserid", field: "iUserid", hide: true },
    { headerName: "Sr", field: "iSr", tooltip: function (params) { return (params.value); }, sortable: false, width: 40 },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 60, cellRenderer: 'deltaIndicator', sortable: false },
    { headerName: "Create Date", field: "sCreatedDate", tooltip: function (params) { return ('<span style="background-dolor:red;">' + params.value + '<span>'); }, width: 90 },
    { headerName: "Last Activation Date", field: "LastActivationDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Last Login Date", field: "LastLoginDate", tooltip: function (params) { return (params.value); }, width: 90 },
    {
        headerName: "Days from Last Activation", field: "DaysFromLastActivation", width: 100,
        tooltip: function (params) { return params.value; },
        cellRenderer: function (params) { return params.value; },
    },
    {
        headerName: "Days from Last Login", field: "DaysFromLastLogin", width: 100,
        tooltip: function (params) { return params.value; },
        cellRenderer: function (params) { return params.value; },
    },
    { headerName: "User Type", field: "sUserType", tooltip: function (params) { return (params.value); }, width: 78 },
    { headerName: "Account Suspended", field: "Suspended", tooltip: function (params) { return (params.value); }, cellClass: ['muser-red-font'], width: 80 },
    { headerName: "Active", field: "bIsActive", cellRenderer: 'faIndicator', tooltip: function (params) { if (params.value == true) { return 'Yes'; } else { return 'No'; } }, cellClass: ['muser-fa-font'], width: 55 },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 95 },
    { headerName: "Customer Name", field: "sFullName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 180 },
    { headerName: "Email", field: "sCompEmail", tooltip: function (params) { return (params.value); }, width: 140 },
    {
        headerName: "Mobile No / WhatsApp No", field: "sCompMobile", width: 110,
        tooltip: function (params) {
            if (params.value != null)
                return params.value.replace("-"," ");
        },
        cellRenderer: function (params) {
            if (params.value != null)
                return params.value.replace("-", " ");
        },
    },

];
var CustomerManageActiveCol = function (params) {
    if (params.data.bIsActive == true) {
        var data = '<div class="Customer-active-cel"> <a href=""><i class="fa fa-check"></i></a></div>';
        return data;
    }
    else {
        var data = '';
        return data;
    }
}function CustomerManageAction(params) {
    var Url = ''.replace("", params.data.bIsAction);
    var data = '<div class="Customer-action-cel"> <a href=""><i class="flaticon-edit"></i></a>&nbsp;&nbsp;' +
        '<a href=""><i class="flaticon-trash-2"></i></a>' + '</div>';
    return data;
}

var UserDetailPage = function (params) {

    return '';
}

var deltaIndicator = function (params) {
    var element = "";
    if ($("#hdnisadminflg").val() == "1") {
        var element = '<a title="Edit" onclick="GoToUserDetail(\'' + params.data.sUserType + '\',' + params.data.iUserid + ',\'' + params.data.sUsername + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
        element += '&nbsp;&nbsp;&nbsp;<a title="Delete" onclick="DeleteUserDetail(' + params.data.iUserid + ')"><i class="fa fa-trash-o" aria-hidden="true" style="cursor:pointer;"></i></a>';
    }
    //else if ($("#hdnisempflg").val() == "1") {
    //    if (params.data.Suspended == "Suspended") {
    //        var element = '<a title="Edit" onclick="GoToUserDetail(\'' + params.data.sUserType + '\',' + params.data.iUserid + ',\'' + params.data.sUsername + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
    //        element += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
    //    }
    //}
    return element;
}
//function Go_UserMgt_Module(iUserid) {
//    $.ajax({
//        url: "/User/Set_ManageUser_To_UserMgt",
//        async: false,
//        type: "POST",
//        data: { iUserId: iUserid, GetType: "MU" },
//        success: function (data, textStatus, jqXHR) {
//            window.open('/User/UserMgt?Type=MU');
//        }
//    });
//}
//function AddNewSecondayUser(sUsername, sCompName, AnyPrimaryinCpny) {
//    if (AnyPrimaryinCpny == "false") {
//        toastr.warning("No any Primary user in " + sCompName + " Company.");
//        return;
//    }
//    else {
//        window.open('/User/UserMgt?UN=' + sUsername);
//    }
//}
var faIndicator = function (params) {
    var element = document.createElement("a");
    element.title = '';
    element.innerHTML = '<i class="fa fa-check" aria-hidden="true"></i>';
    //element.href = '#';
    if (params.value) {
        return element;
    }
}

var GoToUserDetail = function (sUserType, iUserid, sUsername) {
    window.location = '/User/Edit?UserType=' + sUserType + '&UserID=' + iUserid + '&UserName=' + sUsername;
}

var DeleteUserDetail = function (iUserid) {
    $("#hdnDelUserId").val(iUserid);
    $("#Remove").modal("show");
}

var ClearRemoveModel = function () {
    $("#hdnDelUserId").val("0");
    $("#Remove").modal("hide");
}

var DeleteUser = function () {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/User/Delete',
        data: '{ "UserID": ' + $("#hdnDelUserId").val() + '}',
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            loaderHide();
            ClearRemoveModel();
            if (data.Status == "-1") {
                toastr.warning(data.Message, { timeOut: 3000 });
            }
            else {
                toastr.success(data.Message, { timeOut: 3000 });
            }
            GetSearch();
        }
    });
}

function GetSearch() {
    loaderShow();
    //rowData = data;
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
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
        components: {
            deltaIndicator: deltaIndicator,
            faIndicator: faIndicator,
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
        //rowData: data,
        rowModelType: 'serverSide',
        //onGridReady: onGridReady,
        cacheBlockSize: 50, // you can have your custom page size
        paginationPageSize: 50, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#Cart-Gride');
    new agGrid.Grid(gridDiv, gridOptions);

    $(".ag-header-cell-text").addClass("grid_prewrap");

    gridOptions.api.setServerSideDatasource(datasource1);
}var SortColumn = "";
var SortDirection = "";const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var CountryName = "";
        var UserName = "";
        var UserFullName = "";
        var CompanyName = "";
        var _FortunePartyCode = "";


       
        if ($("#ddlFilterType").val() == "UN") {
            UserName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CM") {
            CompanyName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CUN") {
            UserFullName = $("#txtCommonName").val();
        }

        var _FilterType, _FromDate, _ToDate;
        if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LAD" || $("#ddlFilterType").val() == "LLD") {
            _FilterType = $("#ddlFilterType").val();
            _FromDate = $("#txtFromDate").val();
            _ToDate = $("#txtToDate").val();
        }

        var UserType = $('#ddlUserType').val();
        var UserStatus = $('#ddlIsActive').val();

        if (params.request.sortModel.length > 0) {
            SortColumn = params.request.sortModel[0].colId;
            SortDirection = params.request.sortModel[0].sort;
        }

        $.ajax({
            url: "/User/GetUsers",
            async: false,
            type: "POST",
            data: {
                CompanyName: CompanyName,
                CountryName: CountryName,
                UserName: UserName,
                UserFullName: UserFullName,
                UserType: UserType,
                UserStatus: UserStatus,
                PageNo: PageNo,
                IsEmployee: $("#hdn_IsEmployee").val(),
                SortColumn: SortColumn,
                SortDirection: SortDirection,
                PrimaryUser: true,
                FilterType: _FilterType,
                FromDate: _FromDate,
                ToDate: _ToDate,
                FortunePartyCode: _FortunePartyCode
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                }
                else {
                    toastr.error(data.Message, { timeOut: 2500 });
                    params.successCallback([], 0);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                loaderHide();
            }
        });
    }
};function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}var Reset = function () {
    $('#ddlFilterType').val('UN');
    $('#txtCommonName').val('');
    $('#ddlUserType').val('');
    $('#ddlIsActive').val('');
    ddlFilterType();
    GetSearch();
}var DownloadUser = function () {
    loaderShow();

    setTimeout(function () {

        var CountryName = "";
        var UserName = "";
        var UserFullName = "";
        var CompanyName = "";
        var _FortunePartyCode = "";
        
        if ($("#ddlFilterType").val() == "UN") {
            UserName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CM") {
            CompanyName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CUN") {
            UserFullName = $("#txtCommonName").val();
        }

        var _FilterType, _FromDate, _ToDate;
        if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LAD" || $("#ddlFilterType").val() == "LLD") {
            _FilterType = $("#ddlFilterType").val();
            _FromDate = $("#txtFromDate").val();
            _ToDate = $("#txtToDate").val();
        }

        var UserType = $('#ddlUserType').val();
        var UserStatus = $('#ddlIsActive').val();

        var FormName = 'Manage User';
        var ActivityType = 'Excel Export';

        $.ajax({
            url: '/User/DownloadUser',
            async: false,
            type: "POST",
            data: {
                CompanyName: CompanyName,
                CountryName: CountryName,
                UserName: UserName,
                UserFullName: UserFullName,
                UserType: UserType,
                UserStatus: UserStatus,
                IsEmployee: $("#hdn_IsEmployee").val(),
                SortColumn: SortColumn,
                SortDirection: SortDirection,
                PrimaryUser: true,
                FilterType: _FilterType,
                FromDate: _FromDate,
                ToDate: _ToDate,
                FortunePartyCode: _FortunePartyCode,
                FormName: FormName,
                ActivityType: ActivityType
            },
            success: function (data) {
                if (data.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                else if (data.indexOf('No record found') > -1) {
                    toastr.error(data);
                }
                else {
                    location.href = data;
                }
                loaderHide();
            }
        });    }, 15);}function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 130;
    $("#Cart-Gride").css("height", contentHei);
}$(document).ready(function (e) {
    GetSearch();
    contentHeight();
});

$(window).resize(function () {
    contentHeight();
});
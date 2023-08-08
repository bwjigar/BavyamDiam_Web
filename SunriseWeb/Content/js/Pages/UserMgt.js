var CompanyList = [];
var _TYPE = "", _USER_ID = "", _COMPANY_NAME = "", txtCompanyName_hidden = ""
var pgSize = 50;
var Active = "", InActive = "";
var showEntryVar = null;
var total_record = null;
var UserType = null;
var orderBy = '';
var showEntryHtml = '<div class="show_entry">'
    + '<label>Show <select id="ddlPagesize" onchange="onPageSizeChanged()">'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="500">500</option>'
    + '</select> entries</label></div>';
var gridOptions = {};
var ErrorMsg = [];
var columnDefs = [
    { headerName: "Sr", field: "iSr", width: 40, tooltip: function (params) { return (params.value); }, sortable: false },
    { headerName: "Create Date", field: "CreatedDate", tooltip: function (params) { return (params.value); }, width: 100, sortable: true },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 110, sortable: true },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200, sortable: true },
    { headerName: "Customer Name", field: "CustName", tooltip: function (params) { return (params.value); }, width: 123, sortable: true },
    { headerName: "Email ID", field: "sCompEmail", tooltip: function (params) { return (params.value); }, width: 165, sortable: true },
    { headerName: "Mobile No.", field: "sCompMobile", tooltip: function (params) { return (params.value); }, width: 130, sortable: true },
    { headerName: "Assist 1", field: "Assist1", tooltip: function (params) { return (params.value); }, width: 120, sortable: true, hide: true },
    { headerName: "Assist 2", field: "Assist2", tooltip: function (params) { return (params.value); }, width: 120, sortable: true, hide: true },
    {
        headerName: "Active",
        field: "IsActive",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 70,
        sortable: true,
        cellRenderer: function (params) {
            if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; }
            else if (params.value == false) { return '<p class="spn-No1">NO</p>'; }
        }
    },
    {
        headerName: "Search Stock",
        field: "SearchStock",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 95,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Place Order",
        field: "PlaceOrder",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 85,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Order Hisrory",
        field: "OrderHisrory",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 95,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "My Cart",
        field: "MyCart",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 60,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "My Wishlist",
        field: "MyWishlist",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 85,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Quick Search",
        field: "QuickSearch",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 90,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Action", field: "Action", width: 65, cellRenderer: 'deltaIndicator', sortable: false,
        cellRenderer: function (params) {
            var element = '<a title="Edit User" onclick="EditUser(\'' + params.data.iUserid + '\',' + '\'' + params.data.sUsername + '\')"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            element += '&nbsp;&nbsp;&nbsp;<a title="Delete User" onclick="DeleteUserDetail(' + params.data.iUserid + ')"><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            return element;
        }
    },
];
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetSearch();
}
function DeleteUserDetail(iUserid) {
    $("#hdnDelUserId").val(iUserid);
    $("#Remove").modal("show");
}
function ClearRemoveModel() {
    $("#hdnDelUserId").val("0");
    $("#Remove").modal("hide");
}
function DeleteUser() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
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
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            ClearRemoveModel();
            if (data.Status == "-1") {
                toastr.warning(data.Message, { timeOut: 3000 });
            }
            else {
                toastr.success(data.Message, { timeOut: 3000 });
            }
            GetSearch();
            contentHeight();
        }
    });
}
function AddNewUser() {
    AddUser_TitleView();
    if ($("#hdnIsPrimary").val() == "True") {
        $("#txtCompanyName_hidden").val("__"+$("#hdn_iUserid").val());
        $("#txtCompanyName").val($("#hdn_Primary_sCompName").val());

        document.getElementById("txtCompanyName").disabled = true;
        $("#txtCompanyName_dropdown").hide();
        $("#txtCompanyName").addClass("disabled");
    }
}
function EditUser(iUserid, sUsername) {
    $("#txtCompanyName_hidden").val(iUserid);
    EditUser_TitleView();
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/User/GetUsers",
        async: false,
        type: "POST",
        data: { UserName: sUsername, UserType: 3, UserID: iUserid },
        success: function (data, textStatus, jqXHR) {
            var Data = [];
            if (data.Data.length > 0) {
                $.each(data.Data, function (i, item) {
                    if (data.Data[i].sUsername == sUsername) {
                        Data = data.Data[i];
                    }
                });
                if (Data != []) {
                    $("#txtUserName").val(Data.sUsername);
                    document.getElementById("txtUserName").disabled = true;
                    $("#txtUserName").addClass("disabled");

                    $("#txtCompanyName").val(Data.sCompName);
                    $("#txtCompanyName_hidden").val(Data.iUserid);
                    document.getElementById("txtCompanyName").disabled = true;
                    $("#txtCompanyName_dropdown").hide();

                    $("#txtCompanyName").addClass("disabled");
                    $("#txtPassword").val(Data.sPassword);
                    $("#txtCPassword").val(Data.sPassword);
                    $("#txtFirstName").val(Data.sFirstName);
                    $("#txtLastName").val(Data.sLastName);
                    $("#txtEmailId").val(Data.sCompEmail);
                    var Mob = Data.sCompMobile.split("-");
                    if (Mob.length == 2) {
                        $("#txtMobileNo").val(Mob[1]);
                    }
                    else {
                        $("#txtMobileNo").val(Data.sCompMobile);
                    }
                    $("#ChkActive").prop("checked", Data.bIsActive);
                    $("#ChkSearchStock").prop("checked", Data.SearchStock);
                    $("#ChkPlaceOrder").prop("checked", Data.PlaceOrder);
                    $("#ChkOrderHisrory").prop("checked", Data.OrderHisrory);
                    $("#ChkMyCart").prop("checked", Data.MyCart);
                    $("#ChkMyWishlist").prop("checked", Data.MyWishlist);
                    $("#ChkQuickSearch").prop("checked", Data.QuickSearch);
                    Chkblur();
                    $("#ChkPwd").prop("checked", false);
                    $('#txtPassword').addClass("pwd_field");
                    $('#txtCPassword').addClass("pwd_field");
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

}

function ResetData() {
    $("#txtSearch").val("");
    $("#Active").removeClass("btn-spn-opt-active");
    $("#InActive").removeClass("btn-spn-opt-active");
    Active = "";
    InActive = "";
    GetSearch();
}
function GetSearch(type = "") {
    if (type != "onkey" && type != "filter") {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }
    if (type == "onkey") {
        if ($("#txtSearch").val().replace(' ', '') == "") {
            $("#txtSearch").val("");
            return;
        }
    }
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
        onGridReady: onGridReady,
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
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = pgSize;
        if (params.request.sortModel.length > 0) {
            orderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var obj1 = {
            iPgNo: PageNo,
            iPgSize: PageSize,
            sOrderBy: orderBy,
            Search: $("#txtSearch").val(),
            Active: Active,
            InActive: InActive
        };
        $.ajax({
            url: "/User/Get_UserMgt",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    total_record = data.Data[0].total_record;
                    UserType = data.Data[0].UserType;
                    data.Data.splice(0, 1);
                    params.successCallback(data.Data, total_record);
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
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
function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}
function Excel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    var obj1 = {
        iPgNo: 0,
        iPgSize: 1,
        sOrderBy: orderBy,
        Search: $("#txtSearch").val(),
        Active: Active,
        InActive: InActive
    };
    $.ajax({
        url: "/User/Excel_UserMgt",
        type: "POST",
        data: obj1,
        success: function (data, textStatus, jqXHR) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data') > -1) {
                toastr.error(data);
            }
            else {
                location.href = data;
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        resultHei = $(".Filters").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 75;
    contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        //toastr.warning("Please Enter Only Number only.");
        return false;
    }

    return true;
}
var checkemail1 = function (valemail) {
    //var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var forgetfilter = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
    }
}
function AvoidSpace(event) {
    var k = event ? event.which : window.event.keyCode;
    if (k == 32) return false;
}
function SpaceRemoved() {
    $('#txtUserName').val($('#txtUserName').val().split(' ').join(''));
}
var GetError = function () {
    ErrorMsg = [];
    var P = false;
    if (_TYPE == "Add") {
        if ($("#txtCompanyName_hidden").val() == "" || $("#txtCompanyName_hidden").val().slice(0, 2) != "__") {
            $('#txtCompanyName').addClass("FildValid");
            ErrorMsg.push({
                'Error': "Please Select Company",
            });
        }
    }

    if ($("#txtFirstName").val() == "") {
        $('#txtFirstName').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter First Name.",
        });
    }
    if ($("#txtLastName").val() == "") {
        $('#txtLastName').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Last Name.",
        });
    }
    if ($("#txtMobileNo").val() == "") {
        $('#txtMobileNo').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Mobile No.",
        });
    }
    if ($("#txtEmailId").val() == "") {
        $('#txtEmailId').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Email Id.",
        });
    }
    else {
        if (!checkemail1($("#txtEmailId").val())) {
            $('#txtEmailId').addClass("FildValid");
            ErrorMsg.push({
                'Error': "Please Enter Valid Email Id Format.",
            });
        }
    }
    if (_TYPE == "Add") {
        if ($("#txtUserName").val() == "") {
            $('#txtUserName').addClass("FildValid");
            ErrorMsg.push({
                'Error': "Please Enter User Name.",
            });
        }
        else {
            var newlength = $("#txtUserName").val().length;

            if (newlength < 5) {
                ErrorMsg.push({
                    'Error': "Please Enter Minimum 5 Character User Name.",
                });
                $('#txtUserName').addClass("FildValid");
            }
        }
    }
    if ($("#txtPassword").val() == "") {
        $('#txtPassword').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Password.",
        });
        P = true;
    }
    else {
        var newlength = $("#txtPassword").val().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character Password.",
            });
            $('#txtPassword').addClass("FildValid");
            P = true;
        }
    }
    if ($("#txtCPassword").val() == "") {
        $('#txtCPassword').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Confirm Password.",
        });
        P = true;
    }
    else {
        var newlength = $("#txtCPassword").val().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character Confirm Password.",
            });
            $('#txtCPassword').addClass("FildValid");
            P = true;
        }
    }
    if (P == false) {
        if ($("#txtPassword").val() != $("#txtCPassword").val()) {
            ErrorMsg.push({
                'Error': "Please Enter Confirm Password Same as Password.",
            });
            $('#txtCPassword').addClass("FildValid");
        }
    }
    return ErrorMsg;
}
function ClearUser() {
    if (_TYPE == "Add") {
        if ($("#hdnIsPrimary").val() != "True") {
            $("#txtCompanyName").val("");
            $("#txtCompanyName_hidden").val("");
            document.getElementById("txtCompanyName").disabled = false;
            $("#txtCompanyName_dropdown").show();
            $("#txtCompanyName").removeClass("disabled");
        }

        $("#txtUserName").val("");
        document.getElementById("txtUserName").disabled = false;
        $("#txtUserName").removeClass("disabled");
    }
    $("#txtPassword").val("");
    $("#txtCPassword").val("");
    $("#txtFirstName").val("");
    $("#txtLastName").val("");
    $("#txtEmailId").val("");
    $("#txtMobileNo").val("");
    $("#ChkActive").prop("checked", false);
    $("#ChkSearchStock").prop("checked", false);
    $("#ChkPlaceOrder").prop("checked", false);
    $("#ChkOrderHisrory").prop("checked", false);
    $("#ChkMyCart").prop("checked", false);
    $("#ChkMyWishlist").prop("checked", false);
    //$("#ChkOffer").prop("checked", false);
    //$("#ChkOfferHistory").prop("checked", false);
    $("#ChkQuickSearch").prop("checked", false);
    $("#ChkAll").prop("checked", false);
    txtCompanyName_hidden = "";
}
var ErroClearRemoveModel = function () {
    $("#ErrorModel").modal("hide");
}
function ChkAll() {
    if ($("#ChkAll").is(":checked")) {
        $("#ChkSearchStock").prop("checked", true);
        $("#ChkPlaceOrder").prop("checked", true);
        $("#ChkOrderHisrory").prop("checked", true);
        $("#ChkMyCart").prop("checked", true);
        $("#ChkMyWishlist").prop("checked", true);
        $("#ChkQuickSearch").prop("checked", true);
    }
    else {
        $("#ChkSearchStock").prop("checked", false);
        $("#ChkPlaceOrder").prop("checked", false);
        $("#ChkOrderHisrory").prop("checked", false);
        $("#ChkMyCart").prop("checked", false);
        $("#ChkMyWishlist").prop("checked", false);
        $("#ChkQuickSearch").prop("checked", false);
    }
}
function Chkblur() {
    if ($("#ChkSearchStock").prop("checked") == true && $("#ChkPlaceOrder").prop("checked") == true &&
        $("#ChkOrderHisrory").prop("checked") == true && $("#ChkMyCart").prop("checked") == true &&
        $("#ChkMyWishlist").prop("checked") == true && $("#ChkQuickSearch").prop("checked") == true) {
        $("#ChkAll").prop("checked", true);
    }
    else {
        $("#ChkAll").prop("checked", false);
    }
}
function Back() {
    $(".GidData").show();
    $("#divAddNewUser").hide();
    $("#h2Title").show();
    $("#h2EditTitle").hide();
    $("#h2AddTitle").hide();
    ClearUser();
    GetSearch();
    $("#txtCompanyName_hidden").val("");
    _TYPE = "";
}
function AddUser_TitleView() {
    _TYPE = "Add";
    $(".GidData").hide();
    $("#divAddNewUser").show();
    ClearUser();
    document.getElementById("txtUserName").disabled = false;
    $("#txtUserName").removeClass("disabled");
    $("#ChkPwd").prop("checked", false);
    $('#txtPassword').addClass("pwd_field");
    $('#txtCPassword').addClass("pwd_field");
    $("#h2Title").hide();
    $("#h2EditTitle").hide();
    $("#h2AddTitle").show();
    $("#btnSave").html("<i class='fa fa-floppy-o' aria-hidden='true'></i>Save");
}
function EditUser_TitleView() {
    _TYPE = "Edit";
    ClearUser();
    $(".GidData").hide();
    $("#divAddNewUser").show();
    $("#h2Title").hide();
    $("#h2AddTitle").hide();
    $("#h2EditTitle").show();
    $("#btnSave").html("<i class='fa fa-floppy-o' aria-hidden='true'></i>Update");
}
function SaveUser() {
    ErrorMsg = GetError();
    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        txtCompanyName_hidden = "";
        if ($("#txtCompanyName_hidden").val().slice(0, 2) == "__") {
            txtCompanyName_hidden = $("#txtCompanyName_hidden").val().substring(2);
        }
        else {
            txtCompanyName_hidden = $("#txtCompanyName_hidden").val();
        }

        var User = {
            "Type": _TYPE,
            "iUserId": txtCompanyName_hidden,
            "UserName": $("#txtUserName").val(),
            "Password": $("#txtPassword").val(),
            "FirstName": $("#txtFirstName").val(),
            "LastName": $("#txtLastName").val(),
            "MobileNo": $("#txtMobileNo").val(),
            "EmailId": $("#txtEmailId").val(),
            "IsActive": $("#ChkActive").is(":checked"),
            "SearchStock": $("#ChkSearchStock").is(":checked"),
            "PlaceOrder": $("#ChkPlaceOrder").is(":checked"),
            "OrderHisrory": $("#ChkOrderHisrory").is(":checked"),
            "MyCart": $("#ChkMyCart").is(":checked"),
            "MyWishlist": $("#ChkMyWishlist").is(":checked"),
            "QuickSearch": $("#ChkQuickSearch").is(":checked")
        };
        $.ajax({
            url: '/User/Save_UserMgt',
            type: "POST",
            data: { UserMgt: User },
            success: function (data) {
                if (data != null) {
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                    }
                    else if (data.Status == "1") {
                        toastr.success(data.Message);
                        //if (_TYPE == "Add") {
                        //    ClearUser();
                        //}
                        //else if (_TYPE == "Edit"){
                        //    Back();
                        //}
                        Back();
                    }
                    else if (data.Status == "-1") {
                        toastr.warning(data.Message);
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
    }
}
$(document).ready(function () {
    if ($("#IsPageShow").val() == "false") {
        window.location.href = "/Dashboard";
    }
    else {
        GetSearch();
        contentHeight();
        GetCompanyList();
    }
    $("#txtCompanyName").focusout(function () {
        CmpnynmSelectRequired();
    });
});
$(window).resize(function () {
    contentHeight();
});
function Get_UserMgt_Module() {
    $.ajax({
        url: "/User/Get_ManageUser_To_UserMgt",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            _MU_UserId = data.iUserId;
            _MU_GetType = data.GetType;
            GetSearch();
            contentHeight();
        }
    });
}
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        $("#" + id).removeClass("btn-spn-opt-active");
        if (id == "Active") {
            Active = "";
        }
        if (id == "InActive") {
            InActive = "";
        }
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "Active") {
            Active = true;
            $("#InActive").removeClass("btn-spn-opt-active");
            InActive = "";
        }
        if (id == "InActive") {
            InActive = true;
            $("#Active").removeClass("btn-spn-opt-active");
            Active = "";
        }
    }
    GetSearch('filter');
}
function tick(el) {
    if (el.checked == true) {
        $('#txtPassword').removeClass("pwd_field");
        $('#txtCPassword').removeClass("pwd_field");
    }
    else {
        $('#txtPassword').addClass("pwd_field");
        $('#txtCPassword').addClass("pwd_field");
    }
}
//$(function () {
//    $("#txtCompanyName").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/User/GetCompanyForUserMgt",
//                data: "{ 'Search': '" + request.term + "'}",
//                dataType: "json",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                success: function (data) {
//                    response($.map(data.Data, function (item) {
//                        return {
//                            label: item.CompName,
//                            val: item.iUserid,
//                            FortunePartyCode: item.FortunePartyCode,
//                            Username: item.Username,
//                            CustName: item.CustName
//                        }
//                    }))


//                },
//                error: function (response) {
//                    toastr.error(response.responseText);
//                },
//                failure: function (response) {
//                    toastr.error(response.responseText);
//                }
//            });
//        },
//        select: function (e, i) {
//            _USER_ID = i.item.val;
//            _COMPANY_NAME = i.item.label;
//            $("#lblFortunePartyCode").html(i.item.FortunePartyCode)
//        },
//        change: function (event, ui) {
//            if (!ui.item) {
//                _USER_ID = "";
//                _COMPANY_NAME = "";
//                $("#lblFortunePartyCode").html("");
//            }
//        },
//        minLength: 1
//    });
//});
function CmpnynmSelectRequired() {
    setTimeout(function () {
        if ($("#txtCompanyName_hidden").val().slice(0, 2) != "__") {
            $("#txtCompanyName").val("");
            $("#txtCompanyName_hidden").val("");
        }
    }, 250);
}
function CmpnynmRst() {
    $("#txtCompanyName_hidden").val("");
}


function GetCompanyList() {
    $.ajax({
        url: "/User/GetCompanyForUserMgt",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Data != null) {
                CompanyList = data.Data;
                for (var i = 0; i < CompanyList.length; i++) {
                    CompanyList[i].iUserid = "__" + CompanyList[i].iUserid;
                }

                if ($("#hdnUserType").val() == "1") {
                    $('#txtCompanyName').ejAutocomplete({
                        dataSource: CompanyList,
                        filterType: 'contains',
                        fields: { key: "iUserid" },
                        highlightSearch: true,
                        watermarkText: "Search with Company Name, Assist By, Party Code, Customer Name",
                        width: "100%",
                        showPopupButton: true,
                        multiColumnSettings: {
                            enable: true,
                            showHeader: true,
                            stringFormat: "{0}",
                            searchColumnIndices: [0, 1, 2, 3],
                            columns: [
                                { "field": "CompName", "headerText": "COMPANY NAME" },
                                { "field": "AssistBy", "headerText": "ASSIST BY" },
                                { "field": "FortunePartyCode", "headerText": "PARTY CODE" },
                                { "field": "CustName", "headerText": "CUSTOMER NAME" }
                            ]
                        }
                    });
                }
                else {
                    $('#txtCompanyName').ejAutocomplete({
                        dataSource: CompanyList,
                        filterType: 'contains',
                        fields: { key: "iUserid" },
                        highlightSearch: true,
                        watermarkText: "Search with Company Name, Party Code, Customer Name",
                        width: "100%",
                        showPopupButton: true,
                        multiColumnSettings: {
                            enable: true,
                            showHeader: true,
                            stringFormat: "{0}",
                            searchColumnIndices: [0, 1, 2],
                            columns: [
                                { "field": "CompName", "headerText": "COMPANY NAME" },
                                { "field": "FortunePartyCode", "headerText": "PARTY CODE" },
                                { "field": "CustName", "headerText": "CUSTOMER NAME" }
                            ]
                        }
                    });
                }
            }
        }
    });
}


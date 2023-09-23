var CRUD_Id = 0;
var CRUD_Type = "Insert";
var today = new Date();
var NextDate = new Date(today.setDate(today.getDate() + 30));
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(NextDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var L_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");
function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function Thirty_Day_Date_Set() {
    $('#txtFromDate').val(SetCurrentDate());
    $('#txtToDate').val(L_date);

    $('#txtFromDate').daterangepicker({
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
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: L_date,
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
function ddlType() {
    if ($("#ddlType").val() == "Discount") {
        $("#lblval").html("Discount<span class='error'>*</span>");
    }
    else if ($("#ddlType").val() == "Value") {
        $("#lblval").html("Value<span class='error'>*</span>");
    }
}
function clearControls() {
    Thirty_Day_Date_Set();
    $("#ddlType").val("Discount");
    ddlType();
    $("#txtVal").val("");
    $("#btnSave").html('<i class="fa fa-save" aria-hidden="true"></i>Save');
}
var ClearRemoveModel = function () {
    CRUD_Id = 0;
    $("#Remove").modal("hide");
}
function Delete() {
    var formData = new FormData();
    formData.append('IUType', CRUD_Type);
    formData.append('Id', CRUD_Id);
    loaderShow();
    $.ajax({
        url: "/User/OrderDisc_InsUpd",
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
                    clearControls();
                    toastr.success(data.Message);
                    GetList();
                    ClearRemoveModel();
                }
                else {
                    toastr.error(data.Message);
                }
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}
function Insert_Update() {
    var isvalid = $('#frm').valid();
    if (!isvalid) {
        return;
    }
    var formData = new FormData();
    formData.append('IUType', CRUD_Type);
    formData.append('Id', CRUD_Id);
    formData.append('FromDate', $('#txtFromDate').val());
    formData.append('ToDate', $('#txtToDate').val());
    formData.append('Type', $('#ddlType').val());
    if ($('#ddlType').val() == "Discount") {
        formData.append('Discount', $('#txtVal').val());
    }
    else if ($('#ddlType').val() == "Value") {
        formData.append('Value', $('#txtVal').val());
    }
    if (isvalid) {
        loaderShow();
        $.ajax({
            url: "/User/OrderDisc_InsUpd",
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
                        clearControls();
                        toastr.success(data.Message);
                        GetList();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                MoveToErrorPage(0);
            }
        });
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

var gridOptions = {};
var columnDefs = [
    { headerName: "Sr.", field: "Sr", width: 45, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Id", field: "Id", hide: true, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "From Date", field: "FromDate", sortable: false, width: 130, tooltip: function (params) { return (params.value); } },
    { headerName: "To Date", field: "ToDate", sortable: false, width: 130, tooltip: function (params) { return (params.value); } },
    { headerName: "Type", field: "Type", width: 85, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Discount", field: "Discount", width: 110, hide: true, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Value", field: "Value", width: 110, hide: true, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Value", field: "Values", width: 100, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Modify Date", field: "LastModifyDate", width: 165, sortable: false, tooltip: function (params) { return (params.value); } },
    { headerName: "Modify By", field: "LastModifyBy", sortable: false, hide: true },
    { headerName: "Action", field: "bIsAction", width: 90, tooltip: function (params) { return (params.value); }, cellRenderer: 'deltaIndicator', }
];
function GetList() {
    loaderShow();
    $.ajax({
        url: "/User/OrderDisc_Select",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        BindList(data.Data);
                    }
                    else {
                        BindList([]);
                    }
                } else {
                    BindList([]);
                }
                loaderHide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}
var deltaIndicator = function (params) {
    var element = '<a href="javascript:void(0);" onclick="EditInfo(' + params.data.Id + ',\'' + params.data.FromDate + '\',\'' + params.data.ToDate + '\',\'' + params.data.Type + '\',\'' + params.data.Discount + '\',\'' + params.data.Value + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;"></i></a>';
    element += '&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="DeleteInfo(' + params.data.Id + ',\'' + params.data.FromDate + '\',\'' + params.data.ToDate + '\',\'' + params.data.Type + '\',\'' + params.data.Discount + '\',\'' + params.data.Value + '\')" ><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;"></i></a>';
    return element;
}
function BindList(data) {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        detailRowHeight: 70,
        defaultColDef: {
            //enableSorting: true,
            //sortable: true,
            resizable: true,
            filter: 'agTextColumnFilter',
            filterParams: {
                applyButton: true,
                resetButton: true,
            }
        },
        components: {
            deltaIndicator: deltaIndicator,
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
        rowData: data,
        cacheBlockSize: 50,
        paginationPageSize: 10,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}
function EditInfo(Id, FromDate, ToDate, Type, Discount, Value) {
    CRUD_Type = "Update";
    CRUD_Id = Id;
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
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: ToDate,
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
    $("#ddlType").val(Type);
    ddlType();
    if (Type == "Discount") {
        $("#txtVal").val(Discount);
    }
    else if (Type == "Value") {
        $("#txtVal").val(Value);
    }
    $("#btnSave").html('<i class="fa fa-save" aria-hidden="true"></i>Update');
}
function DeleteInfo(Id, FromDate, ToDate, Type, Discount, Value) {
    CRUD_Type = "Delete";
    CRUD_Id = Id;
    $("#Remove").modal("show");
}
function contentHeight(deduct) {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - deduct;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    Thirty_Day_Date_Set();
    GetList();
    $('#btnSearch').on('click', function () {
        //pgSize = 50;
        //GetSearch();
    });
    $('#btnSave').on('click', function () {
        Insert_Update();
    });
    $("#ddlType").change(function () {
        ddlType();
    });
    $('#btnReset').click(function () {
        clearControls();
    });

    if (navigator.userAgent.indexOf('iPhone') > -1) {
        contentHeight(-200);
    }
    else if (navigator.userAgent.indexOf('iPad') > -1) {
        contentHeight(100);
    }
    else if (navigator.userAgent.indexOf('Windows') > -1) {
        contentHeight(90);
    }
    else {
        contentHeight(-100);
    }
});
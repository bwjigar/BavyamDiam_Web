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
    { headerName: "Sr", field: "iSr", width: 30, tooltip: function (params) { return (params.value); }, sortable: false },
    { headerName: "Id", field: "Id", tooltip: function (params) { return (params.value); }, hide: true },
    { headerName: "Create Date", field: "CreatedDate", tooltip: function (params) { return (params.value); }, width: 130, sortable: true },
    { headerName: "Message Name", field: "MessageName", tooltip: function (params) { return (params.value); }, width: 150, sortable: true },
    { headerName: "Message", field: "Message", tooltip: function (params) { return (params.value); }, width: 590, sortable: true },
    {
        headerName: "Logout",
        field: "IsLogout",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 70,
        sortable: true,
        cellRenderer: function (params) {
            if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; }
            else if (params.value == false) { return '<p class="spn-No1">NO</p>'; }
        }
    },
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
    { headerName: "Update Date", field: "UpdatedDate", tooltip: function (params) { return (params.value); }, width: 130, sortable: true },
    {
        headerName: "Action", field: "Action", width: 65, cellRenderer: 'deltaIndicator', sortable: false,
        cellRenderer: function (params) {
            var element = '<a title="Edit" onclick="EditMsg(' + params.data.Id + ')"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            element += '&nbsp;&nbsp;&nbsp;<a title="Delete" onclick="DeleteMsg(' + params.data.Id + ')"><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            return element;
        }
    },
];
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetSearch();
}
function DeleteMsg(Id) {
    $("#hdnMsgId").val(Id);
    $("#Remove").modal("show");
}
function ClearRemoveModel() {
    $("#hdnMsgId").val("0");
    $("#Remove").modal("hide");
}
function Delete() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var Msg = {
        Type: "Delete",
        Id: $("#hdnMsgId").val()
    }
    $.ajax({
        url: '/User/MessageMst_Save',
        type: "POST",
        data: { req: Msg },
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            ClearRemoveModel();
            if (data.Status == "0") {
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
function AddNew() {
    Add_TitleView();
}
function EditMsg(_Id) {
    Edit_TitleView();
    $("#hdnMsgId").val(_Id);
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/User/Get_MessageMst",
        async: false,
        type: "POST",
        data: { Id: _Id },
        success: function (data, textStatus, jqXHR) {
            if (data.Data != null && data.Data.length > 0) {
                $("#txtMessageName").val(data.Data[0].MessageName);
                $("#ChkLogout").prop("checked", data.Data[0].IsLogout);
                $("#ChkActive").prop("checked", data.Data[0].IsActive);
                $("#txtMessage").val(data.Data[0].Message);
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
function GetSearch() {
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
        //onGridReady: onGridReady,
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
            sOrderBy: orderBy
        };
        $.ajax({
            url: "/User/Get_MessageMst",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    total_record = data.Data[0].iTotalRec;
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
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        resultHei = $(".Filters").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 75;
    contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}
var GetError = function () {
    ErrorMsg = [];

    if ($("#txtMessageName").val() == "") {
        $('#txtMessageName').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Message Name.",
        });
    }
    if ($("#txtMessage").val() == "") {
        $('#txtMessage').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Message.",
        });
    }
    
    return ErrorMsg;
}
function Clear() {
    $("#txtMessageName").val("");
    $("#ChkLogout").prop("checked", false);
    $("#ChkActive").prop("checked", true);
    $("#txtMessage").val("");
    $("#hdnMsgId").val("0");
    _TYPE = "";
}
var ErroClearRemoveModel = function () {
    $("#ErrorModel").modal("hide");
}
function Back() {
    $(".GidData").show();
    $("#divAddNew").hide();
    $("#h2Title").show();
    $("#h2EditTitle").hide();
    $("#h2AddTitle").hide();
    Clear();
    GetSearch();
    _TYPE = "";
}
function Add_TitleView() {
    Clear();
    _TYPE = "Add";
    $(".GidData").hide();
    $("#divAddNew").show();
    $("#h2Title").hide();
    $("#h2EditTitle").hide();
    $("#h2AddTitle").show();
    $("#btnSave").html("<i class='fa fa-floppy-o' aria-hidden='true'></i>Save");
}
function Edit_TitleView() {
    Clear();
    _TYPE = "Edit";
    $(".GidData").hide();
    $("#divAddNew").show();
    $("#h2Title").hide();
    $("#h2AddTitle").hide();
    $("#h2EditTitle").show();
    $("#btnSave").html("<i class='fa fa-floppy-o' aria-hidden='true'></i>Update");
}
function Save() {
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

        var Msg = {
            "Type": _TYPE,
            "Id": $("#hdnMsgId").val(),
            "MessageName": $("#txtMessageName").val(),
            "Message": $("#txtMessage").val(),
            "IsLogout": $("#ChkLogout").is(":checked"),
            "IsActive": $("#ChkActive").is(":checked"),
        };
        $.ajax({
            url: '/User/MessageMst_Save',
            type: "POST",
            data: { req: Msg },
            success: function (data) {
                if (data != null) {
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        else {
                            toastr.warning(data.Message);
                        }
                    }
                    else if (data.Status == "1") {
                        toastr.success(data.Message);
                        Back();
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
    GetSearch();
    contentHeight();
});
$(window).resize(function () {
    contentHeight();
});
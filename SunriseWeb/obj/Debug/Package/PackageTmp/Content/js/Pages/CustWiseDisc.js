var gridOptions = {};
var ErrorMsg = [];
var pgSize = 50;
var showEntryHtml = '<div class="show_entry">'
    + '<label>Show <select id="ddlPagesize" onchange="onPageSizeChanged()">'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="500">500</option>'
    + '</select> entries</label></div>';
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
function ddlFilterType() {
    if ($("#ddlFilterType").val() == "ED") {
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
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 35,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "Sr", field: "iSr", width: 40, sortable: false },
    { headerName: "Id", field: "Id", hide: true },
    { headerName: "iUserid", field: "iUserid", hide: true },
    { headerName: "Entry Date", field: "CreationDate", tooltip: function (params) { return (params.value); }, width: 130, sortable: true },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 150, sortable: false },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 110, sortable: false },
    { headerName: "Full Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 130, sortable: false },
    { headerName: "Disc (%)", field: "PricePer", tooltip: function (params) { return (params.value); }, width: 75, sortable: true },
    {
        headerName: "Active",
        field: "bIsActive",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 60,
        sortable: true,
        cellRenderer: function (params) {
            if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; }
            else if (params.value == false) { return '<p class="spn-No1">NO</p>'; }
        }
    },

];
function selectAllRendererDetail(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');
    cb.setAttribute('id', 'checkboxAll');
    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            if (Filtered_Data.length > 0) {
                gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        }
        else {
            params.api.deselectAll();
            var data = [];
            gridOptions_Selected_Calculation(data);
        }

    });

    return eHeader;
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
        var orderBy = "";
        if (params.request.sortModel.length > 0) {
            orderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var obj = {
            iPgNo: PageNo,
            iPgSize: PageSize,
            sOrderBy: orderBy
        };
        
        if ($("#ddlFilterType").val() == "UN") {
            obj.UserName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CM") {
            obj.CompanyName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CUN") {
            obj.UserFullName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "ED") {
            obj.FilterType = $("#ddlFilterType").val();
            obj.FromDate = $("#txtFromDate").val();
            obj.ToDate = $("#txtToDate").val();
        }

        $.ajax({
            url: "/Customer/GetCustWiseDisc",
            async: false,
            type: "POST",
            data: obj,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
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
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetSearch();
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".col-xl-12").height(),
        contentHei = winH - tabsmarkerHei - 165;
    contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}
function isNumberKey1(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function GetCustomerData() {
    $("#ddlCustomer").html("");
    if ($.trim($("#txtCompanyName").val()).length == 0) {
        $("#ddlCustomer").html("");
    }
    else {
        $.ajax({
            url: "/Customer/GetCustomer",
            async: false,
            type: "POST",
            data: { SearchText: $("#txtCompanyName").val() },
            success: function (data) {
                if (data.Status == '1') {
                    var list = data.Data;
                    var tot = list.length, i = 0;
                    for (; i < tot; i++) {
                        $("#ddlCustomer").append("<option value='" + list[i].iUserid + "'>" + list[i].sFullName + "</option>");
                    }
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}
$(window).resize(function () {
    contentHeight();
});
var ClearRemoveModel = function () {
    $("#Remove").modal("hide");
}
var DeleteUser = function () {
    ClearRemoveModel();
    var obj = {
        UserList: _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'iUserid').join(","),
        Type: "Delete"
    };
    $.ajax({
        url: "/Customer/SaveCustWiseDisc",
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1") {
                toastr.success("Delete Successfully");
                setTimeout(function () {
                    location.href = window.location.href;
                }, 2000);
            } else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
$(document).ready(function () {
    GetSearch();
    contentHeight();

    $('#btnReset').click(function () {
        $("#txtCompanyName").val("");
        GetCustomerData();
    });
    $('#btnDelete').click(function () {
        if (gridOptions.api.getSelectedRows().length == 0) {
            toastr.error("User Selection is Required");
        }
        else {
            $("#Remove").modal("show");
        }
    });
    $('#btnAdd').click(function () {
        $("#divGrid").hide();
        $(".Add").show();
        $(".btn-dlt").hide();
        $(".btn-add").hide();
        $(".btn-back").show();
        $(".searchfilter").hide();
    });
    $('#btnBack, #btnGridReset').click(function () {
        location.href = window.location.href;
    });
    $('#btnSave').click(function () {
        if ($("#ddlCustomer").val().join(',') == "") {
            toastr.error("User Selection is Required");
            $("#ddlCustomer").focus();
        }
        else if ($("#txtDiscPer").val() == "") {
            toastr.error("Disc is Required");
            $("#txtDiscPer").focus();
        }
        else {
            var obj = {
                UserList: $("#ddlCustomer").val().join(','),
                DiscPer: $('#txtDiscPer').val(),
                Type: "Insert"
            };
            $.ajax({
                url: "/Customer/SaveCustWiseDisc",
                type: "POST",
                data: obj,
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "1") {
                        toastr.success("Saved Successfully");
                        setTimeout(function () {
                            location.href = window.location.href;
                        }, 2000);
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    });

});
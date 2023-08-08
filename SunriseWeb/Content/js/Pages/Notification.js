var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        toastr.warning("Please Enter Only Number only.");
        return false;
    }

    return true;
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
            toastr.warning("To date must be greater than From date.");
            return false;
        }
    }
    else {
        return true;
    }
}

function EditNotification(iTransId) {
    window.location.href = '/Notification/NotificationEdit?Id=' + iTransId;
}

var deltaIndicator = function (params) {
    var element = '<a hrfe="javascript:void(0);" onclick="EditNotification(' + params.data.iTransId + ')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;"></i></a>';
    element += '&nbsp;&nbsp;&nbsp;<a hrfe="#" onclick="DeleteNotification(' + params.data.iTransId + ')" ><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;"></i></a>';
    return element;
}

var columnDefs = [
    { headerName: "Sr", field: "iSr", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Trans ID", field: "iTransId", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "Notification Name", field: "sName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 120, cellRenderer: 'deltaIndicator', },
];


var gridDiv = document.querySelector('#myGrid');
var gridOptions = {
    masterDetail: true,
    detailCellRenderer: 'myDetailCellRenderer',
    detailRowHeight: 70,
    groupDefaultExpanded: 1,
    components: {
        deltaIndicator: deltaIndicator
    },
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
        
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = gridOptions.api.paginationGetPageSize();

        var formData = new FormData();

        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('TransId', $('#txtTransId').val());
        formData.append('NotificationName', $('#txtNotification').val());
        formData.append('PageNo', PageNo);
        formData.append('PageSize', PageSize);

        $.ajax({
            url: "/Notification/GetNotificationList",
            async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
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

    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        //startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDate(e)
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        //startDate: moment(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10),
    }).on('change', function (e) {
        greaterThanDate(e);
    });

    GetDataList();

    $('#btnSearch').click(function () {
        GetDataList();
    });

    $('#btnReset').click(function () {
        $("#txtTransId").val("");
        $("#txtNotification").val("");
        $('#txtFromDate').val("");
        $('#txtToDate').val("");
        GetDataList();
    });
});

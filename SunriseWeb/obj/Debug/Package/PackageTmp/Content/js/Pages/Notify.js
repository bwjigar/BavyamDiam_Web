var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var GridpgSize = 50;
var gridOptions = {};
var EditView = 0;
var SEARCH = '';
var NotifyId = null;

var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';
function Search() {
    var x = document.getElementById("txtMSearch");
    SEARCH = x.value.replace(/ /g, '');
    EditView = 0;
    GetDataList();
}
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
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        suppressMovable: false
    },
    { headerName: "Sr.", field: "SrNo", sortable: false, hide: true, tooltip: function (params) { return (params.value); }, width: 30 },
    { headerName: "Full Name", field: "Fullname", sortable: true, tooltip: function (params) { return (params.value); }, width: 140 },
    { headerName: "Company Name", field: "CompName", sortable: true, tooltip: function (params) { return (params.value); }, width: 300 },
    { headerName: "User Name", field: "Username", sortable: true, tooltip: function (params) { return (params.value); }, width: 180 },
    { headerName: "Country Name", field: "Country", sortable: true, tooltip: function (params) { return (params.value); }, width: 180 },

    { headerName: "Userid", field: "Userid", hide: true, tooltip: function (params) { return (params.value); }, width: 180 },
    { headerName: "Email ID", field: "Email", sortable: true, tooltip: function (params) { return (params.value); }, width: 180 },
    { headerName: "Mobile No.", field: "Mobile", sortable: true, tooltip: function (params) { return (params.value); }, width: 130 },

    { headerName: "Is Dismiss", field: "IsDismiss", sortable: true, tooltip: function (params) { return (params.value); }, width: 78, cellRenderer: IsDismiss, },
];
function IsDismiss(params) {
    if (params.data.IsDismiss == "1") {
        return "<span class='Yes'>Yes</span>";
    }
}

var gridDiv = document.querySelector('#myGrid');
function GetDataList() {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        //rowHeight: 45,
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
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize,
        paginationPageSize: GridpgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(GridpgSize);
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
        var OrderBy = '', PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var formData = new FormData();


        formData.append('NotifyId', NotifyId);
        formData.append('SearchList', SEARCH);
        formData.append('PageNo', PageNo);
        formData.append('PageSize', GridpgSize);
        formData.append('OrderBy', OrderBy);

        $.ajax({
            url: "/User/NotifyList",
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
                            toastr.warning(data.Message);
                        }
                        if (parseInt(NotifyId) > 0) {
                            $('#txtFromDate').daterangepicker({
                                singleDatePicker: true,
                                startDate: data.Data[0].FromDate,
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
                                startDate: data.Data[0].ToDate,
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

                            $("#txtMessage").val(data.Data[0].Message);

                            //gridOptions.api.forEachNode(function (node) {
                            //    node.setSelected(true);
                            //});
                        }
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        params.successCallback([], 0);
                        toastr.error(data.Message);
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                MoveToErrorPage(0);
            }
        });

    }
};
function Publish() {
    var UserIdList = _.pluck(gridOptions.api.getSelectedRows(), 'Userid').join(",");
    if (UserIdList != "") {
        if ($("#txtFromDate").val() == undefined || $("#txtFromDate").val() == "") {
            toastr.warning("Please Select Validite From Date !", { timeOut: 2500 });
            $("#txtFromDate").focus();
            return;
        }
        if ($("#txtToDate").val() == undefined || $("#txtToDate").val() == "") {
            toastr.warning("Please Select Validite To Date !", { timeOut: 2500 });
            $("#txtToDate").focus();
            return;
        }
        if ($("#txtMessage").val() == undefined || $("#txtMessage").val() == "") {
            toastr.warning("Please Enter Message !", { timeOut: 2500 });
            $("#txtMessage").focus();
            return;
        }
        else {
            $.ajax({
                url: "/User/SaveNotifyList",
                type: "POST",
                data: {
                    FromDate: $("#txtFromDate").val(),
                    ToDate: $('#txtToDate').val(),
                    Message: $('#txtMessage').val(),
                    UserIdList: UserIdList,
                    NotifyId: NotifyId
                },
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "1") {
                        toastr.success("Notification Save Successfully !!", { timeOut: 2500 });
                        //location.href = window.location.href + "?NotifyId=" + data.Message;
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        }
    }
    else {
        toastr.warning('Please select any one user !');
        return;
    }
}
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".container-fluid order-history-section").height(),
        navbarHei1 = $(".container-fluid search_result viewcart-fluid").height(),
        contentHei = winH - navbarHei - navbarHei1 - 95;
    $("#myGrid").css("height", contentHei);
}
function Back() {
    window.location.href = '/User/NotifyDet';
}
$(document).ready(function () {
    NotifyId = getParameterByName('NotifyId') == "" || getParameterByName('NotifyId') == null || getParameterByName('NotifyId') == "0" ? '' : getParameterByName('NotifyId');

    if (parseInt(NotifyId) > 0) {
        $("#btnPublish").hide();
    }

    GetDataList();
    contentHeight();

    $('#frmnotify').validate({
        rules: {
            txtFromDate: {
                required: true
            },
            txtToDate: {
                required: true
            },
            txtMessage: {
                required: true,
            }
        },
        messages: {
            txtFromDate: "From Date is Required",
            txtToDate: "To Date is Required",
            txtMessage: "Message is Required"
        }
    });
});

$(window).resize(function () {
    contentHeight();
});

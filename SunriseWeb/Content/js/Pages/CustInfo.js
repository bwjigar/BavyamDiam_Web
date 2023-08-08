var apiUrl = '';
var gridOptions = {};
var columnDefs = [
    { headerName: "InformationID", field: "InformationID", hide: true, tooltip: function (params) { return (params.value); } },
    //{ headerName: "CustomerID", field: "CustomerID", hide: true, tooltip: function (params) { return (params.value); } },
    //{ headerName: "Customer Name", field: "UserName", tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "Information Name", field: "InformationName", tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "From Date", field: "FromDate", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "To Date", field: "ToDate", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Action", field: "Action", tooltip: function (params) { return (params.value); }, width: 100 },
    {
        headerName: "View Image", tooltip: function (params) { return (params.value); }, field: "FileName", width: 80, cellRenderer: function (params) {
            var image_url = (params.value != null) ? 'frame.svg' : 'image-not-available.svg';
            var image_url1 = (params.value != null) ? (apiUrl + params.value) : 'javascript:void(0);';
            return '<ul class="flat-icon-ul"><li><a href="' + image_url1 + '" target="_blank" title="View Image"><img src="../Content/images/' + image_url + '" class="frame-icon"></li></ul></a>';
        },
    }
];

function contentHeight(deduct) {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - deduct;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    apiUrl = getApiUrl();
    apiUrl = apiUrl.replace(/api/g, 'InfoImages/');
    GetList();
});

$(document).ready(function () {
    
    if (navigator.userAgent.indexOf('iPhone') > -1) {
        contentHeight(80);
    }
    else if (navigator.userAgent.indexOf('iPad') > -1) {
        contentHeight(100);
    }
    else if (navigator.userAgent.indexOf('Windows') > -1) {
        contentHeight(75);
    }
    else {
        contentHeight(100);
    }
});

$(window).resize(function (e) {

    if (navigator.userAgent.indexOf('iPhone') > -1) {
        contentHeight(80);
    }
    else if (navigator.userAgent.indexOf('iPad') > -1) {
        contentHeight(100);
    }
    else if (navigator.userAgent.indexOf('Windows') > -1) {
        contentHeight(75);
    }
    else {
        contentHeight(100);
    }
});

function loaderShow() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

function loaderHide() {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

function GetList() {
    loaderShow();

    $.ajax({
        url: "/Information/GetCustList",
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
                    toastr.error(data.Message);
                }
                loaderHide();
            }
            else {
                window.location = GetLoginUrl();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}

function BindList(data) {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        detailRowHeight: 70,
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
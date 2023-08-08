var GridpgSize = 50;
var gridOptions = {};
var IsObj = false;
var searchSummary = {};
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var rowData = [];
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 6));
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

if ($('#hdnisadminflg').val() == 1 || $('#hdn_iUserid_41').val() == 1) {
    IsObj = false;
} else {
    IsObj = true;
}

function isNumberKeyWithNegative(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 45)
        return true;

    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
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
            onSelectionChanged();
        }

    });

    return eHeader;
}

$(document).ready(function (e) {
    Five_Day_Date_Set();
    reset();
    contentHeight();
});
function Five_Day_Date_Set() {
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
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    }).on('change', function (e) {
        greaterThanDate(e);
    });
    $('#txtToDate').daterangepicker({
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
var gridDiv = document.querySelector('#myGrid');
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
    { headerName: "Userid", field: "Userid", hide: true },
    { headerName: "Entry Date Time", field: "ByRequestEntryDate", tooltip: function (params) { return (params.value); }, width: 105, sortable: true },
    { headerName: "Entry User Name", field: "ByRequestUsername", tooltip: function (params) { return (params.value); }, width: 100, sortable: true, hide: IsObj },
    {
        headerName: "Ref No", field: "REF_NO", tooltip: function (params) { return (params.value); }, width: 85, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Lab", field: "LAB", width: 40, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.CER_PATH != "" && params.data.CER_PATH != null) {
                return '<a href="' + params.data.CER_PATH + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.LAB + '</a>'
            }
            else if (params.data.CER_PATH == "" || params.data.CER_PATH == null) {
                return params.data.LAB;
            }
            else {
                return '';
            }
        }
    },
    {
        headerName: "HD Image", field: "IMG_PATH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.IMG_PATH != "" && params.data.IMG_PATH != null) {
                return '<ul class="flat-icon-ul"><li><a href="' + params.data.IMG_PATH + '" target="_blank" title="View Diamond Image">' +
                    '<img src="../Content/images/frame.svg" class="frame-icon"></a></li></ul>';
            }
            else {
                return '<ul class="flat-icon-ul"><li><a href="javascript:void(0);" title="View Diamond Image">' +
                    '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li></ul>';
            }
        }
    },
    {
        headerName: "HD Video", field: "VDO_PATH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.data.VDO_PATH != "" && params.data.VDO_PATH != null) {
                return '<ul class="flat-icon-ul"><li><a href="' + params.data.VDO_PATH + '" target="_blank" title="View Diamond Video">' +
                    '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li></ul>';
            }
            else {
                return '<ul class="flat-icon-ul"><li><a href="javascript:void(0);" title="View Diamond Video">' +
                    '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li></ul>';
            }
        }
    },
    {
        headerName: "Supplier Stock ID", field: "PARTY_STONE_NO", tooltip: function (params) { return (params.value); }, width: 80, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Certi No", field: "CERTI_NO", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Supplier", field: "PARTY_NAME", tooltip: function (params) { return (params.value); }, width: 220, sortable: false },
    { headerName: "Shape", field: "SHAPE", tooltip: function (params) { return (params.value); }, width: 120, sortable: false },
    { headerName: "Pointer", field: "POINTER", tooltip: function (params) { return (params.value); }, width: 65, sortable: false },
    { headerName: "BGM", field: "BGM", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Color", field: "COLOR", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Clarity", field: "PURITY", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    {
        headerName: "Cts", field: "CTS", tooltip: function (params) { return (params.value); }, width: 50, sortable: false
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Rap Rate", field: "RAP_PRICE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Rap Value", field: "RAP_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Supplier Cost Disc(%)", field: "OFFER_DISC_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ff99cc', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Supplier Cost Value", field: "OFFER_DISC_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ff99cc', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Sunrise Disc(%)", field: "SALES_DISC_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ccffff', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Sunrise Value US($)", field: "SALES_DISC_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return {
                'font-weight': 'bold', 'background-color': '#ccffff', 'text-align': 'center', 'font-weight': '600', 'font-size': '11px'
            };
        }
    },
    {
        headerName: "Supp Base Offer(%)", field: "SUPP_BASE_OFFER_PER", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Supp Base Offer Value", field: "SUPP_BASE_VALUE", width: 80, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Cut", field: "CUT", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.value == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: "Polish", field: "POLISH", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.data.CUT == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: "Symm", field: "SYMM", width: 50, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            if (params.data) {
                if (params.data.CUT == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    { headerName: "Fls", field: "FLS", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    {
        headerName: "Length", field: "LENGTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Width", field: "WIDTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Depth", field: "DEPTH", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Depth(%)", field: "DEPTH_PER", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Table(%)", field: "TABLE_PER", width: 60, sortable: false
        , tooltip: function (params) { return (params.value); }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Key to Symbol", field: "SYMBOL", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    { headerName: "Gia Lab Comment", field: "COMMENTS", tooltip: function (params) { return (params.value); }, width: 200, sortable: false },
    {
        headerName: "Girdle(%)", field: "GIRDLE_PER", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Crown Angle", field: "CROWN_ANGLE", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Crown Height", field: "CROWN_HEIGHT", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Pav Angle", field: "PAV_ANGLE", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    {
        headerName: "Pav Height", field: "PAV_HEIGHT", width: 70, sortable: false
        , tooltip: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellRenderer: function (params) { if (params.value != "") { return formatNumber(params.value); } }
        , cellStyle: function (params) {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
    },
    { headerName: "Table Natts", field: "TABLE_NATTS", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Natts", field: "CROWN_NATTS", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Table Inclusion", field: "TABLE_INCLUSION", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Inclusion", field: "CROWN_INCLUSION", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Culet", field: "CULET", tooltip: function (params) { return (params.value); }, width: 60, sortable: false },
    { headerName: "Table Open", field: "TABLE_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Girdle Open", field: "GIRDLE_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Crown Open", field: "CROWN_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
    { headerName: "Pavilion Open", field: "PAV_OPEN", tooltip: function (params) { return (params.value); }, width: 80, sortable: false },
];
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
var showEntryVar = "";
var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';
new WOW().init();
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    GridpgSize = Number(value);
    GetLabByRequestCart();
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbar = $(".navbar").height(),
        resultHei = $(".order-history-data").height(),
        contentHei = winH - (navbar + tabsmarkerHei + resultHei + 75);
    contentHei = (contentHei <= 100 ? 450 : contentHei);
    $("#myGrid").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetLabByRequestCart() {
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
        onSelectionChanged: onSelectionChanged,
        onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize,
        paginationPageSize: GridpgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);

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
        onSelectionChanged();
    });
    gridOptions.api.setServerSideDatasource(datasource1);
    setTimeout(function () {
        var allColumnIds = [];
        gridOptions.columnApi.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });

        //gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);

    var a = $('.ag-header-select-all')[0];
    $(a).removeClass('ag-hidden');
}
function onBodyScroll(params) {
    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
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
        onSelectionChanged();
    });
}
function onSelectionChanged(event) {
    var TOT_PCS = 0, TOT_CTS = 0, TOT_RAP_AMOUNT = 0, SALES_DISC_VALUE = 0, RAP_VALUE = 0, SALES_DISC_PER = 0
        , OFFER_DISC_VALUE = 0, OFFER_DISC_PER = 0, SUPP_BASE_VALUE = 0, SUPP_BASE_OFFER_PER = 0;

    if (gridOptions.api.getSelectedRows().length > 0) {
        TOT_PCS = gridOptions.api.getSelectedRows().length;
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CTS'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'RAP_VALUE'), function (memo, num) { return memo + num; }, 0);

        SALES_DISC_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SALES_DISC_VALUE'), function (memo, num) { return memo + num; }, 0);
        RAP_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'RAP_VALUE'), function (memo, num) { return memo + num; }, 0);
        SALES_DISC_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(SALES_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        OFFER_DISC_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'OFFER_DISC_VALUE'), function (memo, num) { return memo + num; }, 0);
        OFFER_DISC_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(OFFER_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        SUPP_BASE_VALUE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SUPP_BASE_VALUE'), function (memo, num) { return memo + num; }, 0);
        SUPP_BASE_OFFER_PER = (RAP_VALUE == 0 ? 0 : ((1 - (parseFloat(OFFER_DISC_VALUE) / parseFloat(RAP_VALUE))) * 100));

        $('#tab1Pcs').html(formatIntNumber(TOT_PCS));
        $('#tab1CTS').html(formatNumber(TOT_CTS));
        $('#tab1RapValue').html(formatNumber(TOT_RAP_AMOUNT));
        $('#tab1SunriseDiscPer').html(formatNumber(SALES_DISC_PER));
        $('#tab1SunriseValueUSDoller').html(formatNumber(SALES_DISC_VALUE));
    } else {
        $('#tab1Pcs').html(formatIntNumber(searchSummary.TOT_PCS));
        $('#tab1CTS').html(formatNumber(searchSummary.TOT_CTS));
        $('#tab1RapValue').html(formatNumber(searchSummary.TOT_RAP_AMOUNT));
        $('#tab1SunriseDiscPer').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
        $('#tab1SunriseValueUSDoller').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
        $("#li_Approve").show();
        $("#li_Reject").show();
    }
}
var orderBy = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = '';
        }

        $.ajax({
            url: "/LabStock/LabByRequestCartGet",
            async: false,
            type: "POST",
            data: {
                PgNo: PageNo
                , OrderBy: orderBy
                , PgSize: GridpgSize
                , REF_NO: $('#txtRefNo').val()
                , EntryBy: $('#txtEntryBy').val()
                , FromDate: $('#txtFromDate').val()
                , ToDate: $('#txtToDate').val()
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data && data.Data.length > 0) {
                    searchSummary = data.Data[0].DataSummary;
                    rowData = data.Data[0].DataList;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);
                    $('#tab1TCount').show();
                    $('#tab1Pcs').html(formatIntNumber(searchSummary.TOT_PCS));
                    $('#tab1CTS').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1RapValue').html(formatNumber(searchSummary.TOT_RAP_AMOUNT));
                    $('#tab1SunriseDiscPer').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    $('#tab1SunriseValueUSDoller').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
                } else {
                    if (data.Data.length == 0) {
                        $("#divFilter").show();
                        $("#divGridView").hide();
                        if (data.Message != "No Data Found") {
                            toastr.error(data.Message);
                        }
                    }
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1TCount').hide();
                    $('#tab1Pcs').html('0');
                    $('#tab1CTS').html('0');
                    $('#tab1RapValue').html('0');
                    $('#tab1SunriseDiscPer').html('0');
                    $('#tab1SunriseValueUSDoller').html('0');
                }
                if ($('#myGrid .ag-paging-panel').length > 0) {
                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $('#ddlPagesize').val(GridpgSize);
                    clearInterval(showEntryVar);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                contentHeight();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
function ByRequestModal() {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'REF_NO') != "" && count != 0) {
        $("#ByRequestAreyousure_Modal").modal("show");
    }
    else {
        toastr.warning("No Stone selected for By Request");
    }
}
function ByRequest() {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'REF_NO') != "" && count != 0) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $("#ByRequestAreyousure_Modal").modal("hide");

        setTimeout(function () {
            var REFNO_List = _.pluck(gridOptions.api.getSelectedRows(), 'REF_NO').join(",");
            var obj = {};
            obj.REF_NO = REFNO_List;
            obj.Type = "Insert";

            debugger
            $.ajax({
                url: "/LabStock/ByRequest_CRUD",
                async: false,
                type: "POST",
                dataType: "json",
                data: JSON.stringify({ obj: obj }),
                contentType: "application/json; charset=utf-8",
                success: function (data, textStatus, jqXHR) {
                    debugger
                    if (data.Status == "0") {
                        debugger
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else if (data.Status == "1") {
                        debugger
                        toastr.success(data.Message);
                        setTimeout(function () {
                            GetLabByRequestCart();
                        }, 50);
                    }
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        }, 30);
    }
    else {
        $("#ByRequestAreyousure_Modal").modal("hide");
        toastr.warning("No Stone selected for Buy Request");
    }
}
var ClearRemoveModel = function () {
    $("#ByRequestAreyousure_Modal").modal("hide");
}
function reset() {
    $('#txtRefNo').val("");
    $('#txtEntryBy').val("");
    setTimeout(function () {
        Five_Day_Date_Set();
        GetLabByRequestCart();
    }, 1);
}
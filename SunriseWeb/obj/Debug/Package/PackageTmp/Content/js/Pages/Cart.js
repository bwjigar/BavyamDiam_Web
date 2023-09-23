var ShapeList = [];
var CaratList = [];
var LabList = [];
var ColorList = [];
var PolishList = [];
var FlouList = [];
var ClarityList = [];
var CutList = [];
var SymList = [];
var LocationList = [];
var obj = {};
var obj1;
var IsObj_company = false;
var IsObj_username = false;
var IsObj_customername = false;
var IsObj = false;
var IsObj1 = false;
var AllD = false;
var ToDate = F_date;
var FromDate = F_date;
var currMonthInd = parseInt(mnth) - 1;
var Datalist = [];
var limit = 0;
var renderLimit = 0;
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var SubUser = false;

if (currMonthInd == 0) {
    FromDate = [day, m_names[11], (date.getFullYear() - 1)].join("-");
}
else {
    var d = new Date(moment().add(-30, 'days'))
    var curr_date = ("0" + d.getDate()).slice(-2);
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    FromDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
}

if ($('#hdnisadminflg').val() == 1) {
    IsObj1 = false;
} else {
    IsObj1 = true;
}
if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1) {
    IsObj = false;
    IsObj_company = false;
    IsObj_username = false;
    IsObj_customername = false;
}
else {
    IsObj = true;
    IsObj_company = true;
    IsObj_username = true;
    IsObj_customername = true;
}
if ($("#hdnIsPrimary").val() == "True") {
    IsObj_username = false;
    IsObj_company = true;
    IsObj_customername = false;
}

var rowData = [];
var Filtered_Data = [];
function OpenDownloadCheck() {
    if (gridOptions.api.getSelectedRows().length > 0) {
        $(".download-toggle #liAll").show();
    } else {
        $(".download-toggle #liAll").hide();
    }
}
function GetSearchParameter() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var ParameterList = data.Data;
            ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
            CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
            LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
            ColorList = _.filter(ParameterList, function (e) { return e.ListType == 'COLOR' });
            PolishList = _.filter(ParameterList, function (e) { return e.ListType == 'POLISH' });
            FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });
            ClarityList = _.filter(ParameterList, function (e) { return e.ListType == 'CLARITY' });
            CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
            SymList = _.filter(ParameterList, function (e) { return e.ListType == 'SYMM' });
            LocationList = _.filter(ParameterList, function (e) { return e.ListType == 'LOCATION' });

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
GetSearchParameter();
function getValuesAsync1(field) {
    if (field == "shape" || field == "lab" || field == "pointer" || field == "color" || field == "clarity" || field == "cut" || field == "symm" || field == "fls"
        || field == "polish" || field == "Location") {
        return "agSetColumnFilter";
    }
    else if (field == "cts") {
        return "agNumberColumnFilter";
    }
    else {
        return false;
    }
}
function getValuesAsync(field) {
    if (field == "shape") {
        return _.pluck(ShapeList, 'Value');
    }
    else if (field == "lab") {
        return _.pluck(LabList, 'Value');
    }
    else if (field == "pointer") {
        return _.pluck(CaratList, 'Value');
    }
    else if (field == "color") {
        return _.pluck(ColorList, 'Value');
    }
    else if (field == "clarity") {
        return _.pluck(ClarityList, 'Value');
    }
    else if (field == "cut") {
        return _.pluck(CutList, 'Value');
    }
    else if (field == "symm") {
        return _.pluck(SymList, 'Value');
    }
    else if (field == "fls") {
        return _.pluck(FlouList, 'Value');
    }
    else if (field == "polish") {
        return _.pluck(PolishList, 'Value');
    }
    else if (field == "Location") {
        return _.pluck(LocationList, 'Value');
    }
    else {
        return null;
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
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "Cust Id", field: "cust_id", hide: true, tooltip: function (params) { return (params.value); }, width: 100, filter: false },
    { headerName: $("#hdn_Date").val(), field: "TempOrderDate", tooltip: function (params) { return (params.value); }, width: 100, filter: false, sortable: true },
    { headerName: $("#hdn_Username").val(), field: "UserName", hide: IsObj_username, tooltip: function (params) { return (params.value); }, width: 100, filter: false, sortable: true },
    { headerName: $("#hdn_CompanyName").val(), field: "CompName", hide: IsObj_company, tooltip: function (params) { return (params.value); }, width: 100, filter: false, sortable: true },
    { headerName: $("#hdn_CustomerName").val(), field: "cust_name", hide: IsObj_customername, tooltip: function (params) { return (params.value); }, width: 100, filter: false, sortable: true },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "stone_ref_no", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            return '<div class="stock-font"><a target="_blank" href="/DNA?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
        },
        filter: false,
        sortable: false
    },
    {
        headerName: $("#hdn_Lab").val(), field: "Lab", width: 40, tooltip: function (params) { return (params.value); }, cellRenderer: LotValueGetter,
        filter: getValuesAsync1("lab"),
        filterParams: {
            values: getValuesAsync("lab"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        sortable: false,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Shape").val(), field: "shape", tooltip: function (params) { return (params.value); }, width: 95,
        filter: getValuesAsync1("shape"),
        filterParams: {
            values: getValuesAsync("shape"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_CTS").val(), field: "cts", filter: 'agNumberColumnFilter', tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); },
        filter: getValuesAsync1("cts"),
        filterParams: {
            values: getValuesAsync('cts'),
            resetButton: true,
            applyButton: true,
            filterOptions: ['inRange']
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Color").val(), field: "color", tooltip: function (params) { return (params.value); }, width: 50,
        filter: getValuesAsync1("color"),
        filterParams: {
            values: getValuesAsync("color"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Clarity").val(), field: "clarity", tooltip: function (params) { return (params.value); }, width: 60,
        filter: getValuesAsync1("clarity"),
        filterParams: {
            values: getValuesAsync("clarity"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Cut").val(), field: "cut", width: 50, tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        cellStyle: function (params) {
            if (params.value == '3EX')
                return { 'font-weight': 'bold' };
        },
        filter: getValuesAsync1("cut"),
        filterParams: {
            values: getValuesAsync("cut"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Polish").val(), field: "polish", width: 60, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data == undefined) {
                return '';
            }
            if (params.data.cut == '3EX')
                return { 'font-weight': 'bold' };
        },
        filter: getValuesAsync1("polish"),
        filterParams: {
            values: getValuesAsync("polish"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Symm").val(), field: "symm", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data == undefined) {
                return '';
            }
            if (params.data.cut == '3EX')
                return { 'font-weight': 'bold' };
        },
        filter: getValuesAsync1("symm"),
        filterParams: {
            values: getValuesAsync("symm"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Fls").val(), field: "fls", tooltip: function (params) { return (params.value); }, width: 50,
        filter: getValuesAsync1("fls"),
        filterParams: {
            values: getValuesAsync("fls"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true,
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Rap_Price_Doller").val(), field: "cur_rap_rate", tooltip: function (params) { return formatNumber(params.value); }, width: 80, cellRenderer: function (params) { if (params.value != "0") { return formatNumber(params.value); } }, filter: false, sortable: true,
        cellStyle: function (params) {
            if (params.data) {
                return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
            }
        },
    },
    {
        headerName: $("#hdn_Offer_Disc_Per").val(), field: "sales_disc_per", width: 90, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { if (params.value != "0") { return parseFloat(params.value).toFixed(2); } },
        filter: false, sortable: true,
    },
    {
        headerName: $("#hdn_Price_Cts").val(), field: "price_per_cts", width: 75, cellRenderer: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { return formatNumber(params.value) }, filter: false, sortable: true,
        cellStyle: function (params) {
            if (params.data) {
                return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
            }
        },
    },
    {
        headerName: $("#hdn_Offer_Value_Dollar").val(), field: "net_amount", width: 95, tooltip: function (params) { return formatNumber(params.value); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { return formatNumber(params.value); }, filter: false, sortable: true,
    },
    { headerName: "Measurement", field: "Measurement", tooltip: function (params) { return (params.value); }, width: 90, filter: false, sortable: false },
    {
        headerName: $("#hdn_Table_Per").val(), field: "table_per", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true,
        cellStyle: function (params) {
            if (params.data) {
                return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
            }
        },
    },
    {
        headerName: $("#hdn_Depth_Per").val(), field: "depth_per", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true,
        cellStyle: function (params) {
            if (params.data) {
                return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
            }
        },
    },
    { headerName: $("#hdn_Certi_No").val(), field: "certi_no", tooltip: function (params) { return (params.value); }, rowGroup: false, width: 100, filter: false, sortable: true },
    { headerName: $("#hdn_Key_to_symbol").val(), field: "symbol", tooltip: function (params) { return (params.value); }, width: 350, filter: false, sortable: true },
    { headerName: "GIA COMMENT", field: "sComments", tooltip: function (params) { return (params.value); }, width: 350, filter: false, sortable: true },





    //{
    //    headerName: $("#hdn_Pointer").val(), field: "pointer", tooltip: function (params) { return (params.value); }, width: 60,
    //    filter: getValuesAsync1("pointer"),
    //    filterParams: {
    //        values: getValuesAsync("pointer"),
    //        resetButton: true,
    //        applyButton: true,
    //        comparator: function (a, b) {
    //            return 0;
    //        }
    //    }, sortable: true,
    //    menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    //},


    //{ headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 70, filter: false, sortable: true },



    //{ headerName: $("#hdn_Rap_Amt_Doller").val(), field: "rap_amount", width: 90, tooltip: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { if (params.value != "0") { return formatNumber(params.value); } }, filter: false, sortable: true },




    //{ headerName: $("#hdn_Length").val(), field: "length", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_Width").val(), field: "width", width: 50, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_Depth").val(), field: "depth", width: 50, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },

    //{ headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    ////{ headerName: "Luster /Milky", field: "Luster", tooltip: function (params) { return (params.value); }, width: 90, filter: false },
    //{ headerName: $("#hdn_Table_Black").val(), field: "table_natts", tooltip: function (params) { return (params.value); }, width: 90, filter: false, sortable: true },
    //{ headerName: $("#hdn_Crown_Natts").val(), field: "Crown_Natts", tooltip: function (params) { return (params.value); }, width: 90, filter: false, sortable: true },
    //{ headerName: $("#hdn_Table_White").val(), field: "inclusion", tooltip: function (params) { return (params.value); }, width: 80, filter: false, sortable: true },
    //{ headerName: $("#hdn_Crown_White").val(), field: "Crown_Inclusion", tooltip: function (params) { return (params.value); }, width: 90, filter: false, sortable: true },
    //{ headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_CR_HT").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_height", width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_height", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false, sortable: true },
    //{ headerName: $("#hdn_Table_Open").val(), tooltip: function (params) { return (params.value); }, field: "Table_Open", width: 75, filter: false },
    //{ headerName: $("#hdn_Crown_Open").val(), tooltip: function (params) { return (params.value); }, field: "Crown_Open", width: 80, filter: false },
    //{ headerName: $("#hdn_Pav_Open").val(), tooltip: function (params) { return (params.value); }, field: "Pav_Open", width: 70, filter: false },
    //{ headerName: $("#hdn_Girdle_Open").val(), tooltip: function (params) { return (params.value); }, field: "Girdle_Open", width: 80, filter: false },
    //{ headerName: $("#hdn_girdle").val(), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88, sortable: true },
    //{ headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) { return (params.value); }, field: "girdle_type", width: 90, filter: false, sortable: true },
    //{ headerName: $("#hdn_Laser_in_SC").val(), tooltip: function (params) { return (params.value); }, field: "sInscription", width: 90, filter: false, sortable: true },
    //{ headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
    //{ headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
    //{ headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    //{ headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    //{ headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
];
var gridOptions = {};

/*------------ order-history-dropdown-select ------------*/
$(document).ready(function (e) {
    //$('#gallerypoplia1').on('click', function () {
    //    $('#gallery-popup1').toggleClass('show');
    //    $('#aggrid-section1.gallery-grid').toggleClass('close');
    //});
    //$('#ConfirmOrderModal').on('show.bs.modal', function (event) {
    //    var count = gridOptions.api.getSelectedRows().length;
    //    if (count > 0) {
    //        $('#frmSaveOrder #Selected').show();
    //        $('#frmSaveOrder #NotSelected').hide();
    //        $('.modal-footer #btnsaveOrderstone').show();
    //    } else {
    //        $('#frmSaveOrder #Selected').hide();
    //        $('#frmSaveOrder #NotSelected').show();
    //        $('.modal-footer #btnsaveOrderstone').hide();
    //    }
    //});
    //$('#RemoveCart').on('show.bs.modal', function (event) {
    //    var count = gridOptions.api.getSelectedRows().length;
    //    if (count > 0) {
    //        $('#RemoveCart #Selected').show();
    //        $('#RemoveCart #NotSelected').hide();
    //        $('#RemoveCart .modal-footer #btnSendMail').show();
    //        $('#RemoveCart .modal-footer #btnCancelMail').hide();
    //    } else {
    //        $('#RemoveCart #Selected').hide();
    //        $('#RemoveCart #NotSelected').show();
    //        $('#RemoveCart .modal-footer #btnSendMail').hide();
    //        $('#RemoveCart .modal-footer #btnCancelMail').show();
    //    }
    //});
    //$('#ExcelModalAll').on('show.bs.modal', function (event) {
    //    var count = gridOptions.api.getSelectedRows().length;
    //    if (count > 0) {
    //        $('#customRadio4').prop('checked', true);
    //    } else {
    //        $('#customRadio3').prop('checked', true);
    //    }
    //});
    //$('#EmailModal').on('show.bs.modal', function (event) {
    //    var count = gridOptions.api.getSelectedRows().length;
    //    if (count > 0) {
    //        $('#customRadiomail2').prop('checked', true);
    //    } else {
    //        $('#customRadiomail').prop('checked', true);
    //    }
    //});
    //$('.result-three li a.download-popup').on('click', function (event) {
    //    $('.download-toggle').toggleClass('active');
    //    event.stopPropagation();
    //});
    //$(document).click(function (event) {
    //    if (!$(event.target).hasClass('active')) {
    //        $(".download-toggle").removeClass("active");
    //    }
    //});
});
$(document).ready(function () {
    //$('#txtFromDate').daterangepicker({
    //    singleDatePicker: true,
    //    startDate: FromDate,
    //    showDropdowns: true,
    //    locale: {
    //        separator: "-",
    //        format: 'DD-MMM-YYYY'
    //    },
    //    minYear: 1901,
    //    maxYear: parseInt(moment().format('YYYY'), 10)
    //}).on('change', function (e) {
    //    greaterThanDate(e);
    //});

    //$('#txtToDate').daterangepicker({
    //    singleDatePicker: true,
    //    startDate: ToDate,
    //    showDropdowns: true,
    //    locale: {
    //        separator: "-",
    //        format: 'DD-MMM-YYYY'
    //    },
    //    minYear: 1901,
    //    maxYear: parseInt(moment().format('YYYY'), 10)
    //}).on('change', function (e) {
    //    greaterThanDate(e);
    //});
    //$('[data-toggle="tooltip"]').tooltip();
    ////bindData();
    //GetSearch();
    //$('#ConfirmOrderModal').on('hidden.bs.modal', function () {
    //    $('#Comments').val("");
    //});
});
function contentHeight() {
    var winH = $(window).height(),
        header = $(".result-three").height(),
        navbarHei = $(".viewcart-data").height(),
        contentHei = winH - (header + navbarHei + 120);
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function (e) {
    GET_Scheme_Disc();
    $('#gallerypoplia1').on('click', function () {
        $('#gallery-popup1').toggleClass('show');
        $('#aggrid-section1.gallery-grid').toggleClass('close');
    });
    $('#ConfirmOrderModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#frmSaveOrder #Selected').show();
            $('#frmSaveOrder #NotSelected').hide();
            $('.modal-footer #btnsaveOrderstone').show();
        } else {
            $('#frmSaveOrder #Selected').hide();
            $('#frmSaveOrder #NotSelected').show();
            $('.modal-footer #btnsaveOrderstone').hide();
        }
    });
    $('#RemoveCart').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#RemoveCart #Selected').show();
            $('#RemoveCart #NotSelected').hide();
            $('#RemoveCart .modal-footer #btnSendMail').show();
            $('#RemoveCart .modal-footer #btnCancelMail').hide();
        } else {
            $('#RemoveCart #Selected').hide();
            $('#RemoveCart #NotSelected').show();
            $('#RemoveCart .modal-footer #btnSendMail').hide();
            $('#RemoveCart .modal-footer #btnCancelMail').show();
        }
    });
    $('#ExcelModalAll').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadio4').prop('checked', true);
        } else {
            $('#customRadio3').prop('checked', true);
        }
    });
    $('#EmailModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadiomail2').prop('checked', true);
        } else {
            $('#customRadiomail').prop('checked', true);
        }
    });
    $('.result-three li a.download-popup').on('click', function (event) {
        $('.download-toggle').toggleClass('active');
        event.stopPropagation();
    });
    $(document).click(function (event) {
        if (!$(event.target).hasClass('active')) {
            $(".download-toggle").removeClass("active");
        }
    });
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
    }).on('change', function (e) {
        greaterThanDate(e);
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
    }).on('change', function (e) {
        greaterThanDate(e);
    });
    $('[data-toggle="tooltip"]').tooltip();
    GetSearch();
    $('#ConfirmOrderModal').on('hidden.bs.modal', function () {
        $('#Comments').val("");
    });
    contentHeight();

    //$('#ddlStatus').multiselect();

    $('#btnSearch').click(function () {
        GetSearch();
    });

    $('#btnReset').click(function () {
        $('#txtFromDate').val(FromDate);
        $('#txtToDate').val(ToDate);
        $("#txtStoneId").val("");
        $("#txtCompanyName").val("");
        GetSearch();
    });
    GetCompanyList();
    $("#_txtCompanyName").focusout(function () {
        CmpnynmSelectRequired();
    });
});
$(window).resize(function () {
    contentHeight();
});
function closeOrderConfirmModal() {
    if ($("#hdnIsSubUser").val() == "False" || ($("#hdnIsSubUser").val() == "True" && $("#hdnOrderHisrory").val() == "True")) {
        window.location.href = "/Order/OrderHistory";
    }
    else {
        $('#order-confirm-modal').modal('hide');
    }
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
            if ($scope.Filtered_Data.length > 0) {
                $scope.gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                $scope.gridOptions.api.forEachNode(function (node) {
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
function StatusValueGetter(params) {

    if (params.data.status1 == "N")
        return '<div class="newStatus"><span>N</span></div>';
    else if (params.data.status1 == "AVAILABLE")
        return '<div class="activeStatus"><span>A</span></div>';
    else if (params.data.status1 == "AVAILABLE OFFER")
        return '<div class="offerStatus"><span>O</span></div>';

    else if (params.data.status1 == "BUSS. PROCESS")
        return '<div class="busyStatus"><span>B</span></div>';



}
function LotValueGetter(params) {
    if (params.data != undefined) {
        if (params.data.lab != "" && params.data.Actual_certi_url != "") {
            return '<a href= "' + params.data.Actual_certi_url + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
        }
        else if (params.data.lab != "" && params.data.Actual_certi_url == "") {
            return params.data.lab;
        }
        else if (params.data.lab == "") {
            return '';
        }
    }
    else {
        return '';
    }
}
function ImageValueGetter(params) {
    var image_url = '', movie_url = '', certi_url = '';

    if ((params.data.image_url != null && params.data.image_url != "") ||
        (params.data.image_url1 != null && params.data.image_url1 != "") ||
        (params.data.image_url2 != null && params.data.image_url2 != "") ||
        (params.data.image_url3 != null && params.data.image_url3 != "")) {
        image_url = '<li><a href="https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=I&StoneId=' + params.data.stone_ref_no + '" target="_blank" title="View Diamond Images">' +
            '<img src="../Content/images/frame.svg" class="frame-icon"></a></li>';
    }
    else {
        image_url = '<li><a href="javascript:void(0);" title="View Diamond Images">' +
            '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li>';
    }

    //if (params.data.image_url != null && params.data.image_url != "") {
    //    image_url = '<li><a href="' + params.data.image_url + '" target="_blank" title="View Diamond Images">' +
    //        '<img src="../Content/images/frame.svg" class="frame-icon"></a></li>';
    //}
    //else {
    //    image_url = '<li><a href="javascript:void(0);" title="View Diamond Images">' +
    //        '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li>';
    //}

    if (params.data.movie_url != null && params.data.movie_url != "") {
        movie_url = '<li><a href="https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=V&StoneId=' + params.data.stone_ref_no + '" target="_blank" title="View Diamond Video">' +
            '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
    }
    else {
        movie_url = '<li><a href="javascript:void(0);" title="View Diamond Video">' +
            '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li>';
    }
    //if (params.data.movie_url != null && params.data.movie_url != "") {
    //    movie_url = '<li><a href="' + params.data.movie_url + '" target="_blank" title="View Diamond Video">' +
    //        '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
    //}
    //else {
    //    movie_url = '<li><a href="javascript:void(0);" title="View Diamond Video">' +
    //        '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li>';
    //}

    if (params.data.view_certi_url != null && params.data.view_certi_url != "") {
        certi_url = '<li><a href="https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=C&StoneId=' + params.data.stone_ref_no + '" target="_blank" title="View Diamond Certificate">' +
            '<img src="../Content/images/medal.svg" class="medal-icon"></a></li>';
    }
    else {
        certi_url = '<li><a href="javascript:void(0);" title="View Diamond Certificate">' +
            '<img src="../Content/images/medal-not-available.svg" class="medal-icon"></a></li>';
    }
    //if (params.data.view_certi_url != null && params.data.view_certi_url != "") {
    //    certi_url = '<li><a href="' + params.data.view_certi_url + '" target="_blank" title="View Diamond Certificate">' +
    //        '<img src="../Content/images/medal.svg" class="medal-icon"></a></li>';
    //}
    //else {
    //    certi_url = '<li><a href="javascript:void(0);" title="View Diamond Certificate">' +
    //        '<img src="../Content/images/medal-not-available.svg" class="medal-icon"></a></li>';
    //}

    var data = ('<ul class="flat-icon-ul">' + image_url + movie_url + certi_url + '</ul>');

    return data;
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

var showEntryVar = null;
var pgSize = 50;
function GetSearch() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

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
        onRowSelected: onSelectionChanged,

        onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize, // you can have your custom page size
        paginationPageSize: pgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    var showEntryHtml = '<div class="show_entry"><label>'
        + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
        + '<option value="50">50</option>'
        + '<option value="100">100</option>'
        + '<option value="200">200</option>'
        + '<option value="500">500</option>'
        + '</select> entries'
        + '</label>'
        + '</div>';

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

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
        onSelectionChanged();
    });
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
        //  onSelectionChanged();
    });
}

const datasource1 = {
    getRows(params) {

        obj1 = {};
        obj1.PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            obj1.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }

        if ($("#txtFromDate").length > 0)
            obj1.FromDate = $('#txtFromDate').val();

        if ($("#txtToDate").length > 0)
            obj1.ToDate = $('#txtToDate').val();

        if ($("#txtStoneId").length > 0 && $('#txtStoneId').val() != "")
            obj1.RefNo1 = $('#txtStoneId').val();

        if ($("#txtCompanyName").length > 0 && $('#txtCompanyName').val() != "")
            obj1.CompanyName = $("#txtCompanyName").val();

        obj1.PageSize = pgSize;
        obj1.SubUser = SubUser;

        $.ajax({
            url: "/Cart/GetCartStoneList",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    Datalist = data.Data[0].DataList;
                    rowData = data.Data[0].DataList;

                    var searchSummary = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PAGE);
                    //$('#tab1pcs').html($("#hdn_Cts").val() + ' : ' + formatNumber(searchSummary.TOT_CTS) + '');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
                    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
                    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + searchSummary.TOT_PAGE + '');
                    $('#tab1TCount').show();
                    $('#tab1pcs').html(searchSummary.TOT_PAGE);
                    $('#tab1cts').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1disc').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    $('#tab1ppcts').html(formatNumber(searchSummary.AVG_PRICE_PER_CTS));
                    $('#tab1totAmt').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();

                } else {
                    Datalist = [];
                    rowData = [];

                    params.successCallback([], 0);
                    //$('#tab1cts').html($("#hdn_Cts").val() + ' : 0');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : 0');
                    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab1totAmt').html('Total : 0');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab1TCount').hide();
                    $('#tab1pcs').html('0');
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    $('#tab1ppcts').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                }
                $('#dvGalleryView1').html("");
                renderLimit = 0;
                limit = 0;
                if (data.Data.length > 0) {
                    setTimeout(function () {
                        if (data.Data[0].DataList.length > 12) {
                            renderLimit = renderLimit + 12;
                            $('#btnLoadMore').show();
                        } else {
                            renderLimit = data.Data[0].DataList.length;
                            $('#btnLoadMore').hide();
                        }
                        BindGalleryView();
                    }, 1000);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};

//function bindData() {
//    $('.loading-overlay-image-container').show();
//    $('.loading-overlay').show();
//    $.ajax({
//        url: "/Cart/GetCartStoneList",
//        async: false,
//        type: "POST",
//        data: null,
//        success: function (data, textStatus, jqXHR) {

//            if (data.Data.length > 0) {
//                var searchSummary = data.Data[0].DataSummary;
//                GetSearch(data.Data[0].DataList);
//                //params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);
//                $('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(searchSummary.TOT_CTS) + '');
//                $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
//                $('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
//                $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
//                $('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + searchSummary.TOT_PCS + '');
//            } else {
//                GetSearch([]);
//                $('#tab1cts').html($("#hdn_Cts").val() +' : 0');
//                $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
//                $('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ 0');
//                $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ 0');
//                $('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
//            }
//            $('.loading-overlay-image-container').hide();
//            $('.loading-overlay').hide();
//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//            params.successCallback([], 0);
//            gridOptions.api.showNoRowsOverlay()
//            $('.loading-overlay-image-container').hide();
//            $('.loading-overlay').hide();
//        }
//    });
//}
function onSelectionChanged(event) {
    var Totalpcs = 0;
    var TotalCts = 0.0;
    var TotalNetAmt = 0.0;
    var TotalRapAmt = 0.0;
    var net_amount = 0.0;
    var rap_amount = 0.0;
    var TotalPricePerCts = 0.0;
    var CUR_RAP_RATE = 0;
    var i = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    var SearchResultData = gridOptions.api.getSelectedRows();
    if (SearchResultData.length != 0) {
        for (i = 0; i < SearchResultData.length; i++) {
            Totalpcs = Totalpcs + 1;
            TotalCts += parseFloat(SearchResultData[i].cts);

            net_amount = parseFloat(SearchResultData[i].net_amount);
            rap_amount = parseFloat(SearchResultData[i].rap_amount);
            CUR_RAP_RATE += parseFloat(SearchResultData[i].cur_rap_rate);
            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

            TotalNetAmt += parseFloat(net_amount);
            TotalRapAmt += parseFloat(rap_amount);
            dDisc += parseFloat(SearchResultData[i].sales_disc_per);
        }
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();
    }
    else {
        SearchStoneDataList = Filtered_Data.length > 0 ? Filtered_Data : rowData;

        for (i = 0; i < SearchStoneDataList.length; i++) {
            Totalpcs = Totalpcs + 1;
            TotalCts += parseFloat(SearchStoneDataList[i].cts);

            net_amount = parseFloat(SearchStoneDataList[i].net_amount);
            rap_amount = parseFloat(SearchStoneDataList[i].rap_amount);
            CUR_RAP_RATE += parseFloat(SearchStoneDataList[i].cur_rap_rate);
            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

            TotalNetAmt += parseFloat(net_amount);
            TotalRapAmt += parseFloat(rap_amount);
            dDisc += parseFloat(SearchStoneDataList[i].sales_disc_per);
        }
        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
    }
    TotalPricePerCts = (TotalNetAmt / TotalCts).toFixed(2);
    AvgDis = ((1 - (TotalNetAmt / TotalRapAmt)) * (-100)).toFixed(2);

    TotalPricePerCts = isNaN(TotalPricePerCts) ? 0 : TotalPricePerCts;
    AvgDis = isNaN(AvgDis) ? 0 : AvgDis;

    if (Scheme_Disc_Type == "Discount") {
        Net_Value = 0;
        Final_Disc = 0;
        Web_Benefit = 0;
    }
    else if (Scheme_Disc_Type == "Value") {
        Net_Value = parseFloat(TotalNetAmt) + (parseFloat(TotalNetAmt) * parseFloat(Scheme_Disc) / 100);
        Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(TotalRapAmt)) * 100) * -1;
        Web_Benefit = parseFloat(TotalNetAmt) - parseFloat(Net_Value);
    }
    else {
        Net_Value = parseFloat(TotalNetAmt);
        Final_Disc = parseFloat(AvgDis);
        Web_Benefit = 0;
    }
    if (CUR_RAP_RATE == 0) {
        Final_Disc = 0;
        AvgDis = 0;
    }

    setTimeout(function () {
        //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TotalCts) + '');
        //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AvgDis) + '');
        //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : ' + formatNumber(TotalPricePerCts) + '');
        //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : ' + formatNumber(TotalNetAmt) + '');
        //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + Totalpcs + '');
        $('#tab1TCount').show();
        $('#tab1pcs').html(Totalpcs);
        $('#tab1cts').html(formatNumber(TotalCts));
        $('#tab1disc').html(formatNumber(AvgDis));
        $('#tab1ppcts').html(formatNumber(TotalPricePerCts));
        $('#tab1totAmt').html(formatNumber(TotalNetAmt));

        //$('#tab1Web_Disc').html(formatNumber(Web_Benefit));
        //$('#tab1Net_Value').html(formatNumber(Net_Value));
        //$('#tab1Final_Disc').html(formatNumber(Final_Disc));
    });
}

function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetSearch();
}
/*--------------------------------------------------------REMOVE FROM CART START--------------------------------------------------*/

function RemoveFromCart() {
    $('#RemoveCart').modal('hide');
    var selectedRows = gridOptions.api.getSelectedRows();
    var list = '';
    var i = 0, tot = selectedRows.length;
    for (; i < tot; i++) {
        list += selectedRows[i].cust_id + ":" + selectedRows[i].stone_ref_no + ',';
    }

    if (list != '') {
        list = list.substr(0, (list.length - 1));

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/RemoveToCart",
            type: "POST",
            data: { removeToCart: list },
            success: function (data, textStatus, jqXHR) {
                data.Message = data.Message.replace('Stone(s) removed from cart successfully', $("#hdn_Stones_removed_from_cart_successfully").val());
                data.Message = data.Message.replace('Remove from cart failed', $("#hdn_Remove_from_cart_failed").val());

                if (data.Status == "0") {
                    toastr.error(data.Message);
                } else {
                    GetSearch();
                    toastr.success(data.Message);
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
    else {
        toastr.warning('No stone selected to remove from cart!');
    }
}

/*--------------------------------------------------------REMOVE FROM CART END----------------------------------------------------*/
function SendMail() {

    var isValid = $('#frmSendMail').valid();
    if (!isValid) {
        return false;
    }

    var stoneno = '';
    var count = 0;
    if ($('#customRadiomail').prop('checked')) {
        var stoneno = '';
        var count = rowData.length;
        if ($("#txtStoneId").length > 0 && $('#txtStoneId').val() != "")
            obj1.RefNo1 = $('#txtStoneId').val();

        if ($("#txtCompanyName").length > 0 && $('#txtCompanyName').val() != "")
            obj1.CompanyName = $("#txtCompanyName").val();
    } else {
        //var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;

        var selectedRows = gridOptions.api.getSelectedRows();
        stoneno = '';
        var i = 0, tot = selectedRows.length;
        for (; i < tot; i++) {
            stoneno += selectedRows[i].cust_id + ":" + selectedRows[i].stone_ref_no + ',';
        }

        if (stoneno != '') {
            stoneno = stoneno.substr(0, (stoneno.length - 1));
        }
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    if (count > 0) {
        obj1.RefNo = stoneno;
        obj1.isEmp = !IsObj1;
        if ($("#hdnIsPrimary").val() == "True") {
            obj1.isAdmin = true;
        }
        else {
            obj1.isAdmin = !IsObj;
        }
        obj1.ToAddress = $('#txtemail').val();
        obj1.Comments = $('#txtNotes').val();
        obj1.SubUser = SubUser;

        $.ajax({
            url: "/SearchStock/CartEmailSelectedStone",
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    toastr.error(data.Message);
                } else {
                    toastr.success(data.Message);
                }
                CloseSendMailPopup();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        $('.loading-overlay-image-container').hide();
        $('.loading-overlay').hide();
        toastr.warning('No stone selected to send email!!');
    }
}
function validmail(e) {
    var emailID = $(e).val();
    emailID = emailID.split(',');
    for (var i = 0; i < emailID.length; i++) {
        if (!checkemail(emailID[i])) {
            toastr.error("Invalid Email Formate.");
            $("#txtemail").val('');
            return;
        }
    }
}
function checkemail(valemail) {
    var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$)/;  ///^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (forgetfilter.test(valemail)) {

        return true;
    }
    else {

        return false;
    }
}
function CloseSendMailPopup() {
    $('#EmailModal').modal('hide');
    $('#txtemail').val("");
    $('#txtNotes').val("");
}
function ClearSendMail() {
    $('#txtemail').val("");
    $('#txtNotes').val("");
}
/*--------------------------------------------------------PLACE ORDER START--------------------------------------------------*/

var StoneLstForPlace = '';
function ConfirmOrderModal() {
    StoneLstForPlace = '';
    $('#Comments').val("");

    var stoneList = [];
    stoneList = gridOptions.api.getSelectedRows();

    if (stoneList.length == 0) {
        $('#ConfirmOrderModal').modal('hide');
        toastr.warning($("#hdn_No_Stone_Selected_for_place_order").val());
        return;
    }
    else {
        $('#ConfirmOrderModal').modal('show');
        StoneLstForPlace = _.pluck(_.filter(stoneList), 'stone_ref_no').join(",");
    }

}
function SaveOrder() {
    if ($("#Comments").val().trim() == "") {
        $("#Comments").val("");
        $("#Comments").focus();
        toastr.warning("Enter Comments");
        return;
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    setTimeout(function () {
        if (StoneLstForPlace != "") {
            PlaceOrder();
        }
    }, 30);
}
function PlaceOrder() {
    var _obj = {};
    _obj.StoneID = StoneLstForPlace;
    _obj.Comments = $('#Comments').val();

    $.ajax({
        url: "/SearchStock/Final_PlaceOrder",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ req: _obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            } else {
                var iOrderidsRefNo = data.Error;

                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                isPlaceOrder = true;
                $('#ConfirmOrderModal').modal('hide');
                $('#ConfirmOrderWarningModal').modal('hide');
                var stoneList = [];
                stoneList = gridOptions.api.getSelectedRows();

                data.Message = data.Message.replace('Your Transaction Done Successfully', $("#hdn_Your_Transaction_Done_Successfully").val());
                data.Message = data.Message.replace('This Stone(s) are subject to avaibility', $("#hdn_This_Stones_are_subject_to_availbility").val());
                data.Message = data.Message.replace('Please contact your sales person', $("#hdn_Please_contact_your_sales_person").val());

                $('#lblcheckingavailability').html(data.Message);

                if (iOrderidsRefNo.includes('_') == true && iOrderidsRefNo != "" && data.Status == "SUCCESS") {
                    $.ajax({
                        url: "/ConfirmOrder/AUTO_PlaceConfirmOrder",
                        async: false,
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify({ iOrderid_sRefNo: iOrderidsRefNo }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data, textStatus, jqXHR) {


                        },
                        error: function (jqXHR, textStatus, errorThrown) {

                        }
                    });
                }

                $('#order-confirm-modal').modal('show');
                tabno = CurrentTab;
                GetSearch();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}

/*--------------------------------------------------------PLACE ORDER END----------------------------------------------------*/
/*--------------------------------------------------------DOWNLOAD ALL START----------------------------------------------------*/
function OpenDownloadPopup(downloadType) {
    $('#hdnDownloadType').val(downloadType);
    $('#ExcelModalAll').modal('show');
}
function ALLDownload() {
    AllD = true;
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $("#customRadio4").prop("checked", true);
    $('#hdnDownloadType').val("Image");
    DownloadMedia();
    $('#hdnDownloadType').val("Certificate");
    DownloadMedia();
    $('#hdnDownloadType').val("Video");
    DownloadMedia();
    $('#hdnDownloadType').val("Excel");
    DownloadExcel();
    AllD = false;
}
function DownloadAll() {
    $('#ExcelModalAll').modal('hide');
    if ($('#hdnDownloadType').val() == "Excel") {
        DownloadExcel();
    }
    else if ($('#hdnDownloadType').val() == "Pdf") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Image") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Video") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Certificate") {
        DownloadMedia();
    }
}
function DownloadExcel() {
    var stoneno = '';
    var count = 0;
    if (AllD == false) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }

    if ($('#customRadio3').prop('checked')) {
        count = rowData.length;
        stoneno = '';
        if ($("#txtStoneId").length > 0 && $('#txtStoneId').val() != "")
            obj1.RefNo1 = $('#txtStoneId').val();

        if ($("#txtCompanyName").length > 0 && $('#txtCompanyName').val() != "")
            obj1.CompanyName = $("#txtCompanyName").val();
    }
    else {
        count = gridOptions.api.getSelectedRows().length;
        //stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var selectedRows = gridOptions.api.getSelectedRows();
        stoneno = '';
        var i = 0, tot = selectedRows.length;
        for (; i < tot; i++) {
            stoneno += selectedRows[i].cust_id + ":" + selectedRows[i].stone_ref_no + ',';
        }

        if (stoneno != '') {
            stoneno = stoneno.substr(0, (stoneno.length - 1));
        }
    }

    //var status = "";
    //if ($('#ddlStatus').val() != null && $('#ddlStatus').val() != '')
    //    status = $('#ddlStatus').val().join(",");
    //obj1.Status = status;

    if (count > 0) {
        obj1.RefNo = stoneno;
        obj1.isEmp = !IsObj1;
        if ($("#hdnIsPrimary").val() == "True") {
            obj1.isAdmin = true;
        }
        else {
            obj1.isAdmin = !IsObj;
        }
        obj1.PageNo = "0";
        obj1.FormName = 'My Cart';
        obj1.ActivityType = 'Excel Export';
        obj1.SubUser = SubUser;

        if ($("#txtFromDate").length > 0)
            obj1.FromDate = $('#txtFromDate').val();
        if ($("#txtToDate").length > 0)
            obj1.ToDate = $('#txtToDate').val();

        $.ajax({
            url: "/Common/CartExcelDownloadByStoneId",
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                location.href = data;
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        toastr.warning($("#hdn_No_stone_selected_for_download_as_a").val() + ' ' + $("#hdn_Excel").val() + ' !');
        if (AllD == false) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}
function DownloadMedia() {
    var stoneno = '';
    var count = 0;
    if (AllD == false) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }
    if ($('#customRadio3').prop('checked')) {
        count = rowData.length;
        stoneno = '';
        if ($("#txtStoneId").length > 0 && $('#txtStoneId').val() != "")
            obj1.RefNo1 = $('#txtStoneId').val();

        if ($("#txtCompanyName").length > 0 && $('#txtCompanyName').val() != "")
            obj1.CompanyName = $("#txtCompanyName").val();
    }
    else {
        count = gridOptions.api.getSelectedRows().length;
        //stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var selectedRows = gridOptions.api.getSelectedRows();
        stoneno = '';
        var i = 0, tot = selectedRows.length;
        for (; i < tot; i++) {
            stoneno += selectedRows[i].cust_id + ":" + selectedRows[i].stone_ref_no + ',';
        }

        if (stoneno != '') {
            stoneno = stoneno.substr(0, (stoneno.length - 1));
        }
    }
    //var status = "";
    //if ($('#ddlStatus').val() != null && $('#ddlStatus').val() != '')
    //    status = $('#ddlStatus').val().join(",");
    obj1.Status = status;
    obj1.FormName = 'My Cart';
    obj1.ActivityType = $('#hdnDownloadType').val() + ' Download';
    obj1.SubUser = SubUser;

    if (count > 0) {
        obj1.RefNo = stoneno;
        obj1.DownloadMedia = $('#hdnDownloadType').val();
        if ($("#txtFromDate").length > 0)
            obj1.FromDate = $('#txtFromDate').val();
        if ($("#txtToDate").length > 0)
            obj1.ToDate = $('#txtToDate').val();
        $.ajax({
            url: "/Common/CartMediaDownloadBySearchObject", //StockMediaDownloadByStoneId",
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.search('.zip') == -1 && data.search('.pdf') == -1) {
                    if (data.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    data = data.replace('Error to download video, video is not MP4', $("#hdn_Error_to_download_video_video_is_not_MP4").val());
                    data = data.replace('Image is not available in this stone', $("#hdn_Image_is_not_available_in_this_stone").val());
                    toastr.error(data);
                } else {
                    location.href = data;
                }
                if (AllD == false) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        var type = '';
        if ($('#hdnDownloadType').val() == "Excel")
            type = $("#hdn_Excel").val();
        else if ($('#hdnDownloadType').val() == "Pdf")
            type = $("#hdn_Pdf").val();
        else if ($('#hdnDownloadType').val() == "Image")
            type = $("#hdn_Image").val();
        else if ($('#hdnDownloadType').val() == "Video")
            type = $("#hdn_Video").val();
        else if ($('#hdnDownloadType').val() == "Certificate")
            type = $("#hdn_Certificate").val();

        toastr.warning($("#hdn_No_stone_selected_for_download_as_a").val() + type + ' !');
        if (AllD == false) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}
/*--------------------------------------------------------DOWNLOAD ALL END----------------------------------------------------*/
function StatusWiseData(Sts) {
    var lst = [];
    var Status = Sts;

    if (Status == "N") {
        lst = _.filter(rowData, function (e) { return e.Stock_Staus == Status });
    }
    else {
        lst = _.filter(rowData, function (e) { return e.Stock_Staus == Status });
    }

    gridOptions.api.setRowData(lst);

    Datalist = lst;
    limit = 0;
    renderLimit = 0;
    $('#dvGalleryView1').html("");
    if (Datalist.length > 12) {
        renderLimit = renderLimit + 12;
        $('#btnLoadMore').show();
    } else {
        renderLimit = Datalist.length;
        $('#btnLoadMore').hide();
    }
    BindGalleryView();

    Filtered_Data = lst;
    var Totalpcs = 0;
    var TotalCts = 0.0;
    var TotalNetAmt = 0.0;
    var TotalRapAmt = 0.0;
    var net_amount = 0.0;
    var rap_amount = 0.0;
    var TotalPricePerCts = 0.0;

    for (var i = 0; i < lst.length; i++) {
        Totalpcs = Totalpcs + 1;
        TotalCts += parseFloat(lst[i].cts);

        net_amount = parseFloat(lst[i].net_amount);
        rap_amount = parseFloat(lst[i].rap_amount);
        net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
        rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

        TotalNetAmt += parseFloat(net_amount);
        TotalRapAmt += parseFloat(rap_amount);
    }
    TotalPricePerCts = (TotalNetAmt / TotalCts).toFixed(2);
    AvgDis = ((1 - (TotalNetAmt / TotalRapAmt)) * (-100)).toFixed(2);

    TotalPricePerCts = isNaN(TotalPricePerCts) ? 0 : TotalPricePerCts;
    AvgDis = isNaN(AvgDis) ? 0 : AvgDis;

    setTimeout(function () {
        //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TotalCts) + '');
        //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AvgDis) + '');
        //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(TotalPricePerCts) + '');
        //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ': $ ' + formatNumber(TotalNetAmt) + '');
        //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + Totalpcs + '');

        $('#tab1TCount').show();
        $('#tab1pcs').html(Totalpcs);
        $('#tab1cts').html(formatNumber(TotalCts));
        $('#tab1disc').html(formatNumber(AvgDis));
        $('#tab1ppcts').html(formatNumber(TotalPricePerCts));
        $('#tab1totAmt').html(formatNumber(TotalNetAmt));
    });
}
function LoadMore() {
    renderLimit = renderLimit + 12;
    if (Datalist.length > renderLimit) {
        $('#btnLoadMore').show();
    } else {
        renderLimit = Datalist.length;
        $('#btnLoadMore').hide();
    }
    BindGalleryView();
}
function BindGalleryView() {

    for (var i = limit; i < renderLimit; i++) {
        limit = limit + 1;

        $('#dvGalleryView1').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist[i] != undefined && Datalist[i].bPRimg ? $("#External_ImageURL").val() + (Datalist[i] != undefined ? Datalist[i].certi_no : '') + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + (Datalist[i] != undefined ? Datalist[i].stone_ref_no : '-') + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].shape : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].cts : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].color : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].clarity : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].cut : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].fls : '-') + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].sales_disc_per : '') + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + (Datalist[i] != undefined ? Datalist[i].net_amount : '-') + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + (Datalist[i] != undefined ? Datalist[i].stone_ref_no : '-') + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + (Datalist[i] != undefined ? Datalist[i].stone_ref_no : '-') + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + (Datalist[i] != undefined ? Datalist[i].stone_ref_no : '-') + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + (Datalist[i] != undefined ? Datalist[i].lab : '-') + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + (Datalist[i] != undefined ? Datalist[i].clarity : '-') + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + (Datalist[i] != undefined ? Datalist[i].color : '-') + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }

}
function GET_Scheme_Disc() {
    $.ajax({
        url: "/SearchStock/GET_Scheme_Disc",
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            Scheme_Disc_Type = '';
            Scheme_Disc = "0";
            if (data.Data != null) {
                if (data.Data.length != 0) {
                    if (data.Data[0].Discount != null) {
                        Scheme_Disc_Type = 'Discount';
                        Scheme_Disc = data.Data[0].Discount;
                    }
                    if (data.Data[0].Value != null) {
                        Scheme_Disc_Type = 'Value';
                        Scheme_Disc = data.Data[0].Value;
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        $("#" + id).removeClass("btn-spn-opt-active");
        if (id == "SubUser") {
            SubUser = false;
        }
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "SubUser") {
            SubUser = true;
        }
    }
    GetSearch();
}
var CompanyList = [];
function GetCompanyList() {
    $.ajax({
        url: "/User/GetCompanyForHoldStonePlaceOrder",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Data != null) {
                CompanyList = data.Data;
                for (var i = 0; i < CompanyList.length; i++) {
                    CompanyList[i].iUserid = CompanyList[i].iUserid + "__" + CompanyList[i].FortunePartyCode;
                }

                if ($("#hdnUserType").val() == "1") {
                    $('#_txtCompanyName').ejAutocomplete({
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
                else if ($("#hdnUserType").val() == "2") {
                    $('#_txtCompanyName').ejAutocomplete({
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
function CmpnynmSelectRequired() {
    setTimeout(function () {
        if ($("#_txtCompanyName_hidden").val().split("__").length != 2) {
            $("#_txtCompanyName").val("");
            $("#_txtCompanyName_hidden").val("");
        }
    }, 250);
}
function CmpnynmRst() {
    $("#_txtCompanyName_hidden").val("");
}
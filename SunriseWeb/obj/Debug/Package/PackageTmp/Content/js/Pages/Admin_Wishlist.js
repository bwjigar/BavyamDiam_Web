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
var searchSummary = {};
var ToDate = F_date;
var FromDate = F_date;
var currMonthInd = parseInt(mnth) - 1;
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";

if (currMonthInd == 0)
    FromDate = [day, m_names[11], (date.getFullYear() - 1)].join("-");
else {
    var d = new Date(moment().add(-30, 'days'))
    var curr_date = ("0" + d.getDate()).slice(-2);
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    FromDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
}

var IsObj = false;
var IsObj1 = false;
if ($('#hdnisadminflg').val() == 1) {
    IsObj1 = false;
} else {
    IsObj1 = true;
}
if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1)
    IsObj = false;
else
    IsObj = true;

var showEntryVar = null;
var pgSize = 50;
var gridOptions = {};
var showEntryHtml = '<div class="show_entry"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize">'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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
    if (field == "shape" || field == "lab" || field == "pointer" || field == "color" || field == "clarity" || field == "cut"
        || field == "symm" || field == "fls"
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
    //  { headerName: "SR.NO", field: "Sr", rowGroup: false, width: 100 }, 
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        suppressMovable: false
    },
    { headerName: "Date", field: "Trans_date", tooltip: function (params) { return (params.value); }, width: 80, sortable: true },
    { headerName: "iUserid", field: "iUserid", hide: true },
    { headerName: "User Name", field: "UserName", hide: IsObj, sortable: true, tooltip: function (params) { return (params.value); }, width: 100, filter: false },
    {
        headerName: "Company Name", field: "CompName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 100,
        filter: false, sortable: true
    },
    { headerName: "Assist By", field: "AssistBy1", hide: IsObj1, sortable: true, tooltip: function (params) { return (params.value); }, width: 100, filter: false },
    {
        headerName: "Stock ID / DNA", field: "stone_ref_no", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
        },
        filter: false, sortable: true
    },
    {
        headerName: "Location", field: "Location", sortable: true, tooltip: function (params) { return (params.value); }, width: 70,
        filter: getValuesAsync1("Location"),
        filterParams: {
            values: getValuesAsync("Location"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Status", field: "StoneStatus", width: 50,
        cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            return params.data.StoneStatus;
        },
        cellClass: function (params) {
            if (params.data != undefined) {
                if (params.data.Stock_Staus == 'AVAILABLE OFFER') {
                    return 'offercls';
                }
                if (params.data.Location == 'Upcoming') {
                    return 'upcomingcls';
                }
            }
        },
        filter: false
    },
    {
        headerName: "View Image", tooltip: function (params) { return (params.value); }, field: "Imag_Video", width: 90, cellRenderer: ImageValueGetter, suppressSorting: true,
        suppressMenu: true,
    },
    {
        headerName: "Shape", field: "shape", sortable: true, tooltip: function (params) { return (params.value); }, width: 95,
        filter: getValuesAsync1("shape"),
        filterParams: {
            values: getValuesAsync("shape"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Pointer", field: "pointer", sortable: true, tooltip: function (params) { return (params.value); }, width: 60,
        filter: getValuesAsync1("pointer"),
        filterParams: {
            values: getValuesAsync("pointer"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Lab", field: "Lab", width: 40, tooltip: function (params) { return (params.value); }, cellRenderer: LotValueGetter,
        filter: getValuesAsync1("lab"),
        filterParams: {
            values: getValuesAsync("lab"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: "Certi No.", field: "certi_no", sortable: true, tooltip: function (params) { return (params.value); }, rowGroup: false, width: 80, filter: false },
    { headerName: "BGM", field: "BGM", sortable: true, tooltip: function (params) { return (params.value); }, width: 70, filter: false },
    {
        headerName: "Color", field: "color", sortable: true, tooltip: function (params) { return (params.value); }, width: 50,
        filter: getValuesAsync1("color"),
        filterParams: {
            values: getValuesAsync("color"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Clarity", field: "clarity", sortable: true, tooltip: function (params) { return (params.value); }, width: 60,
        filter: getValuesAsync1("clarity"),
        filterParams: {
            values: getValuesAsync("clarity"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Cts", field: "cts", sortable: true, filter: 'agNumberColumnFilter', tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); },
        filter: getValuesAsync1("cts"),
        filterParams: {
            values: getValuesAsync('cts'),
            resetButton: true,
            applyButton: true,
            filterOptions: ['inRange']
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: "Rap Price($)", field: "cur_rap_rate", sortable: true, tooltip: function (params) { return formatNumber(params.value); }, width: 80, cellRenderer: function (params) { if (params.value != "0") { return formatNumber(params.value); } }, filter: false },
    { headerName: "Rap Amt($)", field: "rap_amount", sortable: true, width: 90, tooltip: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { if (params.value != "0") { return formatNumber(params.value); } }, filter: false },

    { headerName: "Offer Disc.(%)", field: "sales_disc_per", sortable: true, width: 90, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { if (params.value != "0") { return parseFloat(params.value).toFixed(2); } }, filter: false },
    { headerName: "Offer Value($)", field: "net_amount", sortable: true, width: 95, tooltip: function (params) { return formatNumber(params.value); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { return formatNumber(params.value); }, filter: false },
    { headerName: "Price/Cts ", field: "price_per_cts", sortable: true, width: 75, cellRenderer: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { return formatNumber(params.value) }, filter: false },
    {
        headerName: "Cut", field: "cut", width: 50, sortable: true, tooltip: function (params) { return (params.value); },
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Polish", field: "polish", width: 60, sortable: true, tooltip: function (params) { return (params.value); },
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Symm", field: "symm", width: 50, sortable: true, tooltip: function (params) { return (params.value); },
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: "Fls", field: "fls", sortable: true, tooltip: function (params) { return (params.value); }, width: 50,
        filter: getValuesAsync1("fls"),
        filterParams: {
            values: getValuesAsync("fls"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: "Length", field: "length", width: 60, sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Width", field: "width", width: 50, sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Depth", field: "depth", width: 50, sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Depth(%)", field: "depth_per", width: 60, sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Table(%)", field: "table_per", width: 60, sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Key To Symbol", field: "symbol", sortable: true, tooltip: function (params) { return (params.value); }, width: 350, filter: false },
    { headerName: "Culet", field: "sCulet", sortable: true, tooltip: function (params) { return (params.value); }, width: 50 },
    //{ headerName: "Luster /Milky", field: "Luster", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Table Black ", field: "table_natts", sortable: true, tooltip: function (params) { return (params.value); }, width: 90, filter: false },
    { headerName: "Crown Black ", field: "Crown_Natts", sortable: true, tooltip: function (params) { return (params.value); }, width: 90, filter: false },
    { headerName: "Table White", field: "inclusion", sortable: true, tooltip: function (params) { return (params.value); }, width: 80, filter: false },
    { headerName: "Crown White", field: "Crown_Inclusion", sortable: true, tooltip: function (params) { return (params.value); }, width: 90, filter: false },
    { headerName: "Cr Ang", sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Cr Ht", sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_height", width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Pav Ang", sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: "Pav Ht", sortable: true, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_height", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, filter: false },
    { headerName: $("#hdn_Table_Open").val(), tooltip: function (params) { return (params.value); }, field: "Table_Open", width: 75, filter: false },
    { headerName: $("#hdn_Crown_Open").val(), tooltip: function (params) { return (params.value); }, field: "Crown_Open", width: 80, filter: false },
    { headerName: $("#hdn_Pav_Open").val(), tooltip: function (params) { return (params.value); }, field: "Pav_Open", width: 70, filter: false },
    { headerName: $("#hdn_Girdle_Open").val(), tooltip: function (params) { return (params.value); }, field: "Girdle_Open", width: 80, filter: false },
    { headerName: "Girdle(%)", sortable: true, field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88 },
    { headerName: "Girdle Type", sortable: true, tooltip: function (params) { return (params.value); }, field: "girdle_type", width: 90, filter: false },
    { headerName: "Laser Insc", sortable: true, tooltip: function (params) { return (params.value); }, field: "sInscription", width: 90, filter: false },
    { headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Hold_Username", field: "Hold_Username", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
];

function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 103;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    GET_Scheme_Disc();
    //$('#ddlStatus').multiselect();
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

    GetSearch();
    contentHeight();
});
$(window).resize(function () {
    contentHeight();
});
function StatusValueGetter(params) {
    if (params.data == undefined) {
        return '';
    }

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
    $('.offercls').parent().addClass('offerrow');
    $('.upcomingcls').parent().addClass('upcomingrow');
    if (params.data == undefined) {
        return '';
    }

    if (params.data.certi_no != "") {
        if (params.data != undefined) {
            if (params.data.lab == "GIA") {
                return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
            }
            else if (params.data.lab == "HRD") {
                return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
            }
            else if (params.data.lab == "IGI") {
                return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
            }
            else {
                return '';
            }
        }
        else {
            return '';
        }
    }
    else {
        return '<span style="color :blue;">' + params.data.lab + '</span>';
    }
}
function ImageValueGetter(params) {
    if (params.data == undefined) {
        return '';
    }

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
        movie_url = '<li><a href="https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=V&StoneId=' + params.data.stone_ref_no + '" target="_blank" title="View Diamond Videos">' +
            '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
    }
    else {
        movie_url = '<li><a href="javascript:void(0);" title="View Diamond Videos">' +
            '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li>';
    }
    //if (params.data.movie_url != null && params.data.movie_url != "") {
    //    movie_url = '<li><a href="' + params.data.movie_url + '" target="_blank" title="View Diamond Videos">' +
    //        '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
    //}
    //else {
    //    movie_url = '<li><a href="javascript:void(0);" title="View Diamond Videos">' +
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
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetSearch();
}

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
        onRowSelected: onSelectionChanged, onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize,
        paginationPageSize: pgSize,
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
        onSelectionChanged();
    });
}

var pData = {};
const datasource1 = {
    getRows(params) {

        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        var Pointer = "", Shape = "", Lab = "", Color = "", Polish = "", Clarity = "", Fls = "", Cut = "", Symm = "", Location = "", CompName = "", orderBy = "";
        var str = "";
        if (params.request.filterModel.cts) {
            str = "";
            if (params.request.filterModel.cts.operator == "AND" || params.request.filterModel.cts.operator == "OR") {
                if (params.request.filterModel.cts.condition1) {
                    str = params.request.filterModel.cts.condition1.filter + "-";
                    if (params.request.filterModel.cts.condition1.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition1.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition1.filter
                    }
                }
                if (params.request.filterModel.cts.condition2) {
                    if (str != "")
                        str = str + ",";
                    str = params.request.filterModel.cts.condition2.filter + "-";
                    if (params.request.filterModel.cts.condition2.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition2.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition2.filter
                    }
                }
            }
            else {
                str = params.request.filterModel.cts.filter + "-";
                if (params.request.filterModel.cts.filterTo != null) {
                    str = str + params.request.filterModel.cts.filterTo
                } else {
                    str = str + params.request.filterModel.cts.filter
                }
            }
            Pointer = str;
        }
        else {
            Pointer = "";
        }

        if (params.request.filterModel.CompName) {
            str = "";
            if (params.request.filterModel.CompName.operator == "AND" || params.request.filterModel.CompName.operator == "OR") {
                if (params.request.filterModel.CompName.condition1) {
                    str = params.request.filterModel.CompName.condition1.filter;
                }
            }
            else {
                str = params.request.filterModel.CompName.filter
            }
            CompName = str;
        }
        else {
            CompName = "";
        }

        if (params.request.filterModel.shape) {
            Shape = params.request.filterModel.shape.values.join(",");
        }
        else {
            Shape = "";
        }

        if (params.request.filterModel.pointer) {
            Pointer = params.request.filterModel.pointer.values.join(",");
        }
        else {
            if (Pointer == "")
                Pointer = "";
        }

        if (params.request.filterModel.Lab) {
            Lab = params.request.filterModel.Lab.values.join(",");
        }
        else {
            Lab = "";
        }

        if (params.request.filterModel.color) {
            Color = params.request.filterModel.color.values.join(",");
        }
        else {
            Color = "";
        }

        if (params.request.filterModel.polish) {
            Polish = params.request.filterModel.polish.values.join(",");
        }
        else {
            Polish = "";
        }

        if (params.request.filterModel.clarity) {
            Clarity = params.request.filterModel.clarity.values.join(",");
        }
        else {
            Clarity = "";
        }

        if (params.request.filterModel.fls) {
            Fls = params.request.filterModel.fls.values.join(",");
        }
        else {
            Fls = "";
        }

        if (params.request.filterModel.cut) {
            Cut = params.request.filterModel.cut.values.join(",");
        }
        else {
            Cut = "";
        }

        if (params.request.filterModel.symm) {
            Symm = params.request.filterModel.symm.values.join(",");
        }
        else {
            Symm = "";
        }

        if (params.request.filterModel.Location) {
            Location = params.request.filterModel.Location.values.join(",");
        }
        else {
            Location = "";
        }
        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = "";
        }

        var status = "";
        //if ($('#ddlStatus').val() != null && $('#ddlStatus').val() != '')
        //    status = $('#ddlStatus').val().join(",");

        pData = {
            RefNoCerti: $("#txtStoneId").val(),
            CompName: $("#txtCompanyName").val(),
            PageNo: PageNo,
            Location: Location,
            Shape: Shape, Color: Color,
            Polish: Polish, Pointer: Pointer,
            Lab: Lab, Fls: Fls,
            Clarity: Clarity, Cut: Cut,
            Symm: Symm,
            OrderBy: orderBy,
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            status: status,
            PageSize: pgSize
        };
        $.ajax({
            url: "/Wishlist/GetAdminWishListStone",
            async: false,
            type: "POST",
            data: pData,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    searchSummary = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PAGE);

                    //$('#tab1cts').html('Cts : ' + formatNumber(searchSummary.TOT_CTS) + '');
                    //$('#tab1disc').html('Avg.Disc % : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
                    //$('#tab1ppcts').html('Price Per Cts : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
                    //$('#tab1totAmt').html('Total Amount : $ ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
                    //$('#tab1pcs').html('Pcs : ' + searchSummary.TOT_PAGE + '');
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
                    params.successCallback([], 0);
                    //$('#tab1cts').html('Cts : 0');
                    //$('#tab1disc').html('Avg.Disc % : 0');
                    //$('#tab1ppcts').html('Price Per Cts : 0');
                    //$('#tab1totAmt').html('Total : 0');
                    //$('#tab1pcs').html('Pcs : 0');
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

function onSelectionChanged() {

    var Totalpcs = 0;
    var TotalCts = 0.0;
    var TotalNetAmt = 0.0;
    var TotalRapAmt = 0.0;
    var net_amount = 0.0;
    var rap_amount = 0.0;
    var CUR_RAP_RATE = 0;
    var TotalPricePerCts = 0.0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    var SearchResultData = gridOptions.api.getSelectedRows();
    if (SearchResultData.length != 0) {
        for (var i = 0; i < SearchResultData.length; i++) {
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
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();
    }
    else {
        TotalCts = searchSummary.TOT_CTS;
        AvgDis = searchSummary.AVG_SALES_DISC_PER;
        TotalPricePerCts = searchSummary.AVG_PRICE_PER_CTS;
        TotalNetAmt = searchSummary.TOT_NET_AMOUNT;
        Totalpcs = searchSummary.TOT_PAGE;

        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
    }

    setTimeout(function () {
        //$('#tab1cts').html('Cts : ' + formatNumber(TotalCts) + '');
        //$('#tab1disc').html('Avg.Disc % : ' + formatNumber(AvgDis) + '');
        //$('#tab1ppcts').html('Price Per Cts : $ ' + formatNumber(TotalPricePerCts) + '');
        //$('#tab1totAmt').html('Total Amount : $ ' + formatNumber(TotalNetAmt) + '');
        //$('#tab1pcs').html('Pcs : ' + Totalpcs + '');

        $('#tab1TCount').show();
        $('#tab1pcs').html(Totalpcs);
        $('#tab1cts').html(formatNumber(TotalCts));
        $('#tab1disc').html(formatNumber(AvgDis));
        $('#tab1ppcts').html(formatNumber(TotalPricePerCts));
        $('#tab1totAmt').html(formatNumber(TotalNetAmt));

        $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
        $('#tab1Net_Value').html(formatNumber(Net_Value));
        $('#tab1Final_Disc').html(formatNumber(Final_Disc));
    });
}

function Reset() {
    pData = {};
    $('#txtFromDate').val(FromDate);
    $('#txtToDate').val(ToDate);
    $("#txtStoneId").val("");
    $("#txtCompanyName").val("");
    //$("#ddlStatus").multiselect("clearSelection");
    //$("#ddlStatus").multiselect('refresh');
    GetSearch();
}

/*
function GetParameter() {
    $('#ddlValue').html(" <option value=\"\" selected=\"selected\">Select an Option</option>");
    if ($('#ddlSelectUser').val() != "") {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/GetParameter",
            async: false,
            type: "POST",
            data: { ListValue: $('#ddlSelectUser').val()},
            success: function (data, textStatus, jqXHR) {
                ParameterList = data.Data;
                
                _(ParameterList).each(function (obj, i) {
                    if ($('#ddlSelectUser').val() == "CM") {
                        $('#ddlValue').append("<option value=\"" + obj.Value + "\">" + obj.Value + "</option>");
                    } else {
                        $('#ddlValue').append("<option value=\"" + obj.Id + "\">" + obj.Value + "</option>");
                    }
                });
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
} */
function DownloadWishlist() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    var status = "";
    //if ($('#ddlStatus').val() != null && $('#ddlStatus').val() != '')
    //    status = $('#ddlStatus').val().join(",");

    var comma_stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no');
    var iUserid = _.pluck(gridOptions.api.getSelectedRows(), 'iUserid'); //.join(",");
    var certi_no = _.pluck(gridOptions.api.getSelectedRows(), 'certi_no'); //.join(",")
    var iUserid_stoneno = ([iUserid, stoneno].reduce((a, b) => a.map((v, i) => v + b[i]))).join(",");
    $.ajax({
        url: "/Wishlist/DownloadWishlist",
        type: "POST",
        data: {
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            RefNo: comma_stoneno,
            Status: status,
            RefNoCerti: $("#txtStoneId").val(),
            CompName: $("#txtCompanyName").val(),
            isAssistBy: !IsObj1,
            PageNo: 0,
            Location: pData.Location,
            Shape: pData.Shape,
            Color: pData.Color,
            Polish: pData.Polish,
            Pointer: pData.Pointer,
            Lab: pData.Lab,
            Fls: pData.Fls,
            Clarity: pData.Clarity,
            Cut: pData.Cut,
            Symm: pData.Symm,
            OrderBy: pData.OrderBy,
            FormName: 'Wishlist',
            ActivityType: 'Excel Export',
            iUserid_certi_no: iUserid_stoneno
        },
        success: function (data, textStatus, jqXHR) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data found') > -1) {
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
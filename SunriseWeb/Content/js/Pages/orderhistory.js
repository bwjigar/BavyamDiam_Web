var GridpgSize = 50;
var gridOptions = {};
var IsObj = false;
var IsObj1 = false;
var searchSummary = {};
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var OrderHistory_Video_Displayed = true;
var PickUp = false;
var NotPickUp = false;
var Collected = false;
var NotCollected = false;
var Status = [];
var DateStatus = true;
var SubUser = false;
var IsObj_IsPrmry = false;
var rowData = [];

if ($('#hdnisadminflg').val() == 1) {
    IsObj1 = false;
} else {
    IsObj1 = true;
}
if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1) {
    IsObj = false;
    IsObj_IsPrmry = false;
}
else {
    IsObj = true;
    IsObj_IsPrmry = true;
}

if ($("#hdnIsPrimary").val() == "True") {
    IsObj_IsPrmry = false;
}

var today = new Date();
var lastWeekDate = new Date();
if (IsObj) {
    lastWeekDate = new Date(today.setDate(today.getDate() - 60));
} else {
    lastWeekDate = new Date(today.setDate(today.getDate() - 7));
}
lastWeekDate = new Date("2022-11-01");

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
    var FinalDate = (curr_date + "-" + m_names[curr_month]
        + "-" + curr_year);

    return FinalDate;
}
function reset() {
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
        if (SetCurrentDate() == $('#txtToDate').val()) {
            DateStatus = true;
        }
        else {
            DateStatus = false;
        }
    });
    $('#txtStoneId').val("");
    $('#txtCompanyName').val("");
    //$("#ddlStatus").multiselect("clearSelection");
    //$("#ddlStatus").multiselect('refresh');
    Status = [];
    //$("#SubUser").removeClass("btn-spn-opt");
    $("#SubUser").addClass("btn-spn-opt-active");
    $("#PickUp").removeClass("btn-spn-opt-active");
    $("#NotPickUp").removeClass("btn-spn-opt-active");
    $("#Collected").removeClass("btn-spn-opt-active");
    $("#NotCollected").removeClass("btn-spn-opt-active");
    $("#Confirmed").removeClass("btn-spn-opt-active");
    $("#NotAvailable").removeClass("btn-spn-opt-active");
    $("#CheckingAvaibility").removeClass("btn-spn-opt-active");
    SubUser = true;
    PickUp = false;
    NotPickUp = false;
    Collected = false;
    NotCollected = false;

    setTimeout(function () {
        //GetOrderFilterData();
        GetOrderData();
    }, 1);
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
    if ($("#hdnIsPrimary").val() == "True") {
        SubUser = true;
    }
    else {
        SubUser = false;
    }
    //OrderHistory_Video_Status_Get();
    //on();
    //$('#ODVideo').attr('width', $(window).height() + 200);
    //$('#ODVideo').attr('height', $(window).height() - 50);
    //setTimeout(
    //    function () {
    //        document.getElementById("ODVideo").play();
    //    }, 100);
    
    GET_Scheme_Disc();
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
        if (SetCurrentDate() == $('#txtToDate').val()) {
            DateStatus = true;
        }
        else {
            DateStatus = false;
        }
    });


    //$('#ddlStatus').multiselect({
    //    includeSelectAllOption: true,
    //    numberDisplayed: 1
    //});
    GetOrderData();
    contentHeight();
    $('#txtStoneId,#txtCompanyName').on('keypress', function (e) {
        if (e.which == 13) {
            //GetOrderFilterData();
            GetOrderData();
        }
    });
    //$(function () {
    //    //Default Style
    //    $('h1').tooltip({
    //        speed: 'fast',
    //        animation: 'transX'
    //    });

    //    $('h2').tooltip({
    //        animation: 'transX',
    //        speed: 'slow',
    //        background: 'linear-gradient(#772300,orange)',
    //        borderBottom: '2px solid black',
    //        borderRight: '2px solid black',
    //        boxShadow: '2px 2px 7px #555'
    //    });

    //    $('h3').tooltip({
    //        backgroundColor: '#78a',
    //        padding: '15px',
    //        transition: 'all 0.1s ease',
    //    });

    //    $('h4').tooltip({
    //        backgroundColor: '#063758',
    //        padding: '15px',
    //        transition: 'all 0.1s ease',
    //    });
    //    $('.tooltip_1').tooltip({
    //        backgroundColor: 'pink'
    //    });
    //});
    $('.result-three li a.download-popup').on('click', function (event) {
        $('.download-toggle').toggleClass('active');
        down_popup();
        event.stopPropagation();
    });
    $('.wrapper').on('click', function (event) {
        if ($(".tab_1").hasClass("dis_block")) {
            $(".tab_1").removeClass("dis_block");
            $(".tab_1").removeClass("active");
            $(".tab_1").addClass("dis_none");
        }
    });
    
});
var header_template = '<span class="text-danger" style="height:30px;">Arrival in HK </span>';

var gridDiv = document.querySelector('#myGrid');
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
    { headerName: "iOrderid", field: "iOrderid", hide: true },
    { headerName: "iUserid", field: "iUserid", hide: true },
    { headerName: "FullOrderDate", field: "FullOrderDate", hide: true },
    {
        headerName: $("#hdn_Order_Date").val(), field: "OrderDate", tooltip: function (params) { return (params.value); }, width: 80, sortable: true
    },
    {
        headerName: $("#hdn_Order_No").val(), field: "iOrderid", tooltip: function (params) { return (params.value); }, width: 60, sortable: true
    },
   
    
    {
        headerName: $("#hdn_Username").val(), field: "UserName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 150, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: $("#hdn_CompanyName").val(), field: "CompanyName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 150, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: $("#hdn_CustomerName").val(), field: "CustomerName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 180, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: "STOCK ID / DNA", field: "sRefNo", width: 95, tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            //return '<div class="stock-font" style="text-decoration: underline;"><a target="_blank" href="/DNA?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
            return '<div><a style="text-decoration: underline; color :blue;" target="_blank" href="/DNA?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
        }
    },
    { headerName: "LAB", field: "sLab", tooltip: function (params) { return (params.value); }, width: 40, cellRenderer: LotValueGetter, sortable: false },
    { headerName: "SHAPE", field: "sShape", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    
    { headerName: "CTS", field: "dCts", tooltip: function (params) { return (params.value); }, width: 75, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: "COLOR", field: "sColor", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: "CLARITY", field: "sClarity", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    {
        headerName: "CUT", field: "sCut", tooltip: function (params) { return (params.value); }, width: 50,
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        cellStyle: function (params) {
            if (params.data) {
                if (params.value == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: "POLISH", field: "sPolish", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },
    {
        headerName: "SYMM", field: "sSymm", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },

    { headerName: "FLS", field: "sFls", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: "RAP PRICE($)", field: "dRepPrice", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true },
    { headerName: "OFFER DISC.(%)", field: "dDisc", tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 90, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { if (params.value != 0) { return parseFloat(params.value).toFixed(2); } }, sortable: true },
    //{ headerName: $("#hdn_Rap_Amt_Doller").val(), field: "dRapAmount", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true },
    { headerName: "PRICE/CTS", field: "PRICE_PER_CTS", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true },
    { headerName: "OFFER VALUE($)", field: "dNetPrice", tooltip: function (params) { return formatNumber(params.value); }, width: 105, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: "MEASUREMENT", field: "Measurement", tooltip: function (params) { return (params.value); }, width: 100, sortable: true },
    { headerName: "TABLE(%)", field: "dTablePer", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: "DEPTH(%)", field: "dDepthPer", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: "CERTI NO.", field: "sCertiNo", tooltip: function (params) { return (params.value); }, width: 100, sortable: true },
    { headerName: "KEY TO SYMBOL", field: "sSymbol", tooltip: function (params) { return (params.value); }, width: 350 },
    { headerName: "GIA COMMENT", field: "sComments", tooltip: function (params) { return (params.value); }, width: 350 },

    //{ headerName: $("#hdn_Pointer").val(), field: "sPointer", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    //{ headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    //{ headerName: $("#hdn_Web_Disc_Dollar").val(), field: "Web_Benefit", tooltip: function (params) { return formatNumber(params.value); }, width: 80, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color':'#ddeedf' },cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    //{ headerName: $("#hdn_Final_Value").val(), field: "Net_Value", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1'}, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    //{ headerName: $("#hdn_Final_Disc_Per").val(), field: "Final_Disc", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1' }, cellRenderer: function (params) { if (params.value != 0) { return formatNumber(params.value); } }, sortable: true },
    //{ headerName: $("#hdn_Length").val(), field: "dLength", tooltip: function (params) { return (params.value); }, width: 65, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    //{ headerName: $("#hdn_Width").val(), field: "dWidth", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    //{ headerName: $("#hdn_Depth").val(), field: "dDepth", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    //{ headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    //{ headerName: $("#hdn_Table_Black").val(), field: "sTableNatts", tooltip: function (params) { return (params.value); }, width: 90 },
    //{ headerName: $("#hdn_Crown_Natts").val(), field: "sCrownNatts", tooltip: function (params) { return (params.value); }, width: 90 },
    //{ headerName: $("#hdn_Table_White").val(), field: "sInclusion", tooltip: function (params) { return (params.value); }, width: 80 },
    //{ headerName: $("#hdn_Crown_White").val(), field: "sCrownInclusion", tooltip: function (params) { return (params.value); }, width: 90 },
    //{ headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dCrAng", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    //{ headerName: $("#hdn_CR_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dCrHt", width: 50, cellRenderer: function (params) { return formatNumber(params.value); }, },
    //{ headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dPavAng", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    //{ headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dPavHt", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    //{ headerName: $("#hdn_Table_Open").val(), tooltip: function (params) { return (params.value); }, field: "Table_Open", width: 75, filter: false },
    //{ headerName: $("#hdn_Crown_Open").val(), tooltip: function (params) { return (params.value); }, field: "Crown_Open", width: 80, filter: false },
    //{ headerName: $("#hdn_Pav_Open").val(), tooltip: function (params) { return (params.value); }, field: "Pav_Open", width: 70, filter: false },
    //{ headerName: $("#hdn_Girdle_Open").val(), tooltip: function (params) { return (params.value); }, field: "Girdle_Open", width: 80, filter: false },
    //{ headerName: ($("#hdn_girdle").val() + "(%)"), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88, cellRenderer: function (params) { return formatNumber(params.value); }, },
    //{ headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) { return (params.value); }, field: "sGirdleType", width: 90 },
    //{ headerName: $("#hdn_Laser_in_SC").val(), field: "sInscription", width: 110, tooltip: function (params) { return (params.value); }, },
    //{
    //    headerName: $("#hdn_View_Image").val(), field: "Imag_Video", width: 80, tooltip: function (params) { return (params.value); }, cellRenderer: ImageValueGetter, suppressSorting: true,
    //    suppressMenu: true,
    //},
];


function StoneStatus(params) {
    if (capitalizeTheFirstLetterOfEachWord(params.data.sStoneStatus) == "Checking Avaibility") {
        return $("#hdn_Checking_Avaibility").val();
    }
    else if (capitalizeTheFirstLetterOfEachWord(params.data.sStoneStatus) == "Not Available") {
        return $("#hdn_Not_Available").val();
    }
    else if (capitalizeTheFirstLetterOfEachWord(params.data.sStoneStatus) == "Confirmed") {
        return $("#hdn_Confirmed").val();
    }
    else {
        return params.data.sStoneStatus;
    }
}
function Location(params) {
    if (params.data.Location.toUpperCase() == "HONG KONG") {
        return $("#hdn_Hong_Kong").val();
    }
    else if (params.data.Location.toUpperCase() == "INDIA") {
        return $("#hdn_India").val();
    }
    else if (params.data.Location.toUpperCase() == "UPCOMING") {
        return $("#hdn_Upcoming").val();
    }
    else if (params.data.Location.toUpperCase() == "DUBAI") {
        return $("#hdn_Dubai").val();
    }
    else {
        return params.data.Location;
    }
}
function Ready_For_Pick_up(params) {
    if (params.data.PickUp_Status == "Yes") {
        return '<span class="spn-Yes">YES</span>';
    }
    else if (params.data.PickUp_Status == "No") {
        return '<span class="spn-No">NO</span>';
    }
    else {
        return params.data.PickUp_Status;
    }   
}
function Ready_For_Pick_up1(params) {
    if (params.data.PickUp_Status == "Yes") {
        return '<p class="spn-Yes1">YES</p>';
    }
    else if (params.data.PickUp_Status == "No") {
        return '<p class="spn-No1">NO</p>';
    }
    else {
        return params.data.PickUp_Status;
    }
}
function Ready_For_Pick_up2(params) {
    if (params.data.PickUp_Status == "Yes") {
        return '<img src="/Content/images/order_history_yes.png" style="width: 25px;"/>';
    }
    else if (params.data.PickUp_Status == "No") {
        return '<img src="/Content/images/order_history_no.png" style="width: 25px;"/>';
    }
    else {
        return params.data.PickUp_Status;
    }
}
//function CustomTooltip() { }

//CustomTooltip.prototype.init = function (params) {
//    var eGui = (this.eGui = document.createElement('div')),
//        isHeader = params.rowIndex === undefined,
//        isGroupedHeader = isHeader && !!params.colDef.children,
//        str,
//        valueToDisplay;

//    eGui.classList.add('custom-tooltip');

//    if (isHeader) {
//        str = '<p>' + params.value + '</p>';
//        if (isGroupedHeader) {
//            str += '<hr>';
//            params.colDef.children.forEach(function (header, idx) {
//                str += '<p>Child ' + (idx + 1) + ' - ' + header.headerName + '</p>';
//            });
//        }
//        eGui.innerHTML = str;
//    } else {
//        valueToDisplay = params.value.value ? params.value.value : '- Missing -';

//        eGui.innerHTML =
//            '<p><span class"name">' +
//            valueToDisplay +
//            '</span></p>';
//    }
//};

//CustomTooltip.prototype.getGui = function () {
//    return this.eGui;
//};
function capitalizeTheFirstLetterOfEachWord(words) {
    var separateWord = words.toLowerCase().split(' ');
    for (var i = 0; i < separateWord.length; i++) {
        separateWord[i] = separateWord[i].charAt(0).toUpperCase() + separateWord[i].substring(1);
    }
    return separateWord.join(' ');
}
function OrderDetailPage(params) {
    return '<a href=""style="text-decoration: underline; color: #003d66;" ng-click="GoToOrderDetail(data)">Detail</a>';
}
function ImageValueGetter(params) {
    if (params.data != undefined) {
        return params.data.ImagesLink;
    }
    else {
        return '';
    }
}
var ImagesURL = [];
function LotValueGetter(params) {
    $('.offercls').parent().addClass('offerrow');
    $('.upcomingcls').parent().addClass('upcomingrow');
    
    //if (params.data.sCertiNo != "") {
    //    if (params.data != undefined) {
    //        if (params.value == "GIA") {
    //            return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //        }
    //        else if (params.value == "HRD") {
    //            return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //        }
    //        else if (params.value == "IGI") {
    //            return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //        }
    //        else {
    //            return '';
    //        }
    //    }
    //    else {
    //        return '';
    //    }
    //}
    //else {
    //    return '<span style="color :blue;">' + params.value + '</span>';
    //}

    //if (params.data != undefined && params.value != "") {
    //    if (params.value != "") {
    //        return '<a href= "' + params.data.movie_url + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //    }
    //    else {
    //        return '';
    //    }
    //}
    //else {
    //    return '';
    //}

    if (params.data != undefined) {
        debugger
        if (params.value != "" && params.data.view_certi_url != "") {
            return '<a href= "' + params.data.view_certi_url + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
        }
        else if (params.value != "" && params.data.view_certi_url == "") {
            return params.value;
        }
        else if (params.value == "") {
            return '';
        }
    }
    else {
        return '';
    }
}
function GoToOrderDetail(row) {
    window.open('/MyOrder/OrderDetail?OrderId=' + row.order_id);

}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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


/*------------ order-history-dropdown-select ------------*/

function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    //gridOptions.api.paginationSetPageSize(Number(value));
    GridpgSize = Number(value);
    GetOrderData();
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
        
$(document).ready(function () {
    //$('#ddlStatus').multiselect({
    //    includeSelectAllOption: true,
    //    numberDisplayed: 1
    //});
    ////GetOrderFilterData();
    //GetOrderData();
    //contentHeight();
    //$('#txtStoneId,#txtCompanyName').on('keypress', function (e) {
    //    if (e.which == 13) {
    //        GetOrderFilterData();
    //    }
    //});
});
$(window).resize(function () {
    contentHeight();
    //$('#ODVideo').attr('width', $(window).height() + 200);
    //$('#ODVideo').attr('height', $(window).height() - 100);
});
$('input[name="dates"]').daterangepicker();

/*------------ order-history-dropdown-li-select ------------*/

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetOrderFilterData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    //Status: $('#ddlStatus').val().join(","),
    $.ajax({
        url: "/Order/GetOrderHistoryFilterData",
        async: false,
        type: "POST",
        data: {
            FromDate: $('#txtFromDate').val(), ToDate: $('#txtToDate').val(), CommonName: $('#txtCompanyName').val(),
            CompanyName: CompanyName, StoneNoList: $('#txtStoneId').val(), Status: Status.join(","),
            CustomerName: CustomerName, UserName: UserName,
            Location: Location, OrderBy: orderBy, PgSize: GridpgSize,
            PickUp: PickUp, NotPickUp: NotPickUp, Collected: Collected, NotCollected: NotCollected, DateStatus: DateStatus, SubUser: SubUser
        },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Data && data.Data.length > 0) {
                rowData = data.Data[0].DataList;
                if (data.Data[0].Companies != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'CompanyName'; })) {
                        _.findWhere(columnDefs, { field: 'CompanyName' }).filterParams.values = data.Data[0].Companies;
                    }
                }
                if (data.Data[0].Customers != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'CustomerName'; })) {
                        _.findWhere(columnDefs, { field: 'CustomerName' }).filterParams.values = data.Data[0].Customers;
                    }
                }
                if (data.Data[0].Users != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'UserName'; })) {
                        _.findWhere(columnDefs, { field: 'UserName' }).filterParams.values = data.Data[0].Users;
                    }
                }
                //if (data.Data[0].Status != undefined) {
                //    if (_.find(columnDefs, function (num) { return num.field == 'sStoneStatus'; })) {
                //        _.findWhere(columnDefs, { field: 'sStoneStatus' }).filterParams.values = data.Data[0].Status;
                //    }
                //}
                if (data.Data[0].Locations != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'Location'; })) {
                        _.findWhere(columnDefs, { field: 'Location' }).filterParams.values = data.Data[0].Locations;
                    }
                }

            } else {
                if (_.find(columnDefs, function (num) { return num.field == 'CompanyName'; })) {
                    _.findWhere(columnDefs, { field: 'CompanyName' }).filterParams.values = null;
                }
                if (_.find(columnDefs, function (num) { return num.field == 'UserName'; })) {
                    _.findWhere(columnDefs, { field: 'UserName' }).filterParams.values = null;
                }
                //if (_.find(columnDefs, function (num) { return num.field == 'sStoneStatus'; })) {
                //    _.findWhere(columnDefs, { field: 'sStoneStatus' }).filterParams.values = null;
                //}
                if (_.find(columnDefs, function (num) { return num.field == 'Location'; })) {
                    _.findWhere(columnDefs, { field: 'Location' }).filterParams.values = null;
                }
            }
            GetOrderData();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function GetOrderData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        components: {
            //deltaIndicator: deltaIndicator,
            //statusIndicator: statusIndicator,
            ImageValueGetter: ImageValueGetter,
            LotValueGetter: LotValueGetter
        },
        defaultColDef: {
            enableSorting: true,
            sortable: true,
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
        onSelectionChanged: onSelectionChanged, onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize, // you can have your custom page size
        paginationPageSize: GridpgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        },
        //components: {
        //    customTooltip: CustomTooltip,
        //}
    };

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

        gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
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
function onSelectionChanged() {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var CUR_RAP_RATE = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;
    
    if (gridOptions.api.getSelectedRows().length > 0) {
        dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dCts'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dNetPrice'), function (memo, num) { return memo + num; }, 0);
        //TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Net_Value'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dRapAmount'), function (memo, num) { return memo + num; }, 0);
        CUR_RAP_RATE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dRepPrice'), function (memo, num) { return memo + num; }, 0);
        //AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;
        //TOT_NET_AMOUNT = (TOT_NET_AMOUNT).toFixed(2);
        //TOT_RAP_AMOUNT = (TOT_RAP_AMOUNT).toFixed(2);
        
        if (Scheme_Disc_Type == "Discount") {
            Net_Value = 0;
            Final_Disc = 0;
            Web_Benefit = 0;
        }
        else if (Scheme_Disc_Type == "Value") {
            Net_Value = (parseFloat(TOT_NET_AMOUNT) + (parseFloat(TOT_NET_AMOUNT) * parseFloat(Scheme_Disc) / 100)).toFixed(2);
            Final_Disc = (((1 - parseFloat(Net_Value) / parseFloat(TOT_RAP_AMOUNT)) * 100) * -1).toFixed(2);
            Web_Benefit = (parseFloat(TOT_NET_AMOUNT) - parseFloat(Net_Value)).toFixed(2);
        }
        else {
            Net_Value = parseFloat(TOT_NET_AMOUNT);
            Final_Disc = parseFloat(AVG_SALES_DISC_PER);
            Web_Benefit = 0;
        }
        if (CUR_RAP_RATE == 0) {
            Final_Disc = 0;
            AVG_SALES_DISC_PER = 0;
        }
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();
    } else {
        TOT_CTS = searchSummary.TOT_CTS;
        AVG_SALES_DISC_PER = searchSummary.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = searchSummary.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = searchSummary.TOT_NET_AMOUNT;
        TOT_PCS = searchSummary.TOT_PCS;
        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
    }

    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TOT_CTS) + '');
    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : ' + formatNumber(TOT_NET_AMOUNT) + '');
    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');
    $('#tab1TCount').show();
    $('#tab1pcs').html(TOT_PCS);
    $('#tab1cts').html(formatNumber(TOT_CTS));
    $('#tab1disc').html(formatNumber(AVG_SALES_DISC_PER));
    //$('#tab1ppcts').html(formatNumber(AVG_PRICE_PER_CTS));
    $('#tab1totAmt').html(formatNumber(TOT_NET_AMOUNT));

    $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
    $('#tab1Net_Value').html(formatNumber(Net_Value));
    $('#tab1Final_Disc').html(formatNumber(Final_Disc));
}
function round(value, exp) {
    if (typeof exp === 'undefined' || +exp === 0)
        return Math.round(value);

    value = +value;
    exp = +exp;

    if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
        return NaN;

    // Shift
    value = value.toString().split('e');
    value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

    // Shift back
    value = value.toString().split('e');
    return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
}
var UserName = "";
var CustomerName = "";
var CompanyName = "";
var Location = "";
var orderBy = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        var status = '';
        var CommonName = $('#txtCompanyName').val();

        if (params.request.filterModel.CustomerName) {
            CustomerName = params.request.filterModel.CustomerName.values.join(",");
        }
        else {
            CustomerName = '';
        }

        if (params.request.filterModel.UserName) {
            UserName = params.request.filterModel.UserName.values.join(",");
        }
        else {
            UserName = '';
        }

        if (params.request.filterModel.CompanyName) {
            CompanyName = params.request.filterModel.CompanyName.values.join(",");
        }
        else {
            CompanyName = '';
        }

        if (params.request.filterModel.Location) {
            Location = params.request.filterModel.Location.values.join(",");
        }
        else {
            Location = '';
        }

        //if (params.request.filterModel.sStoneStatus) {
        //    status = params.request.filterModel.sStoneStatus.values.join(",");
        //}
        //else {
        //    //status = $('#ddlStatus').val().join(",");
        //    status = Status.join(",");
        //}

        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = '';
        }
        $.ajax({
            url: "/Order/GetOrderHistoryData",
            async: false,
            type: "POST",
            data: {
                FromDate: $('#txtFromDate').val(), ToDate: $('#txtToDate').val(), CommonName: CommonName,
                CompanyName: CompanyName, StoneNoList: $('#txtStoneId').val(), Status: status, PageNo: PageNo,
                CustomerName: CustomerName, UserName: UserName, Location: Location, OrderBy: orderBy, PgSize: GridpgSize,
                PickUp: PickUp, NotPickUp: NotPickUp, Collected: Collected, NotCollected: NotCollected, DateStatus: DateStatus, SubUser: SubUser
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data && data.Data.length > 0) {
                    searchSummary = data.Data[0].DataSummary;
                    rowData = data.Data[0].DataList;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);

                    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(searchSummary.TOT_CTS) + '');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
                    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
                    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + searchSummary.TOT_PCS + '');
                    $('#tab1TCount').show();
                    $('#tab1pcs').html(searchSummary.TOT_PCS);
                    $('#tab1cts').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1disc').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    //$('#tab1ppcts').html(formatNumber(searchSummary.AVG_PRICE_PER_CTS));
                    $('#tab1totAmt').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    //$('#tab1cts').html($("#hdn_Cts").val() + ' : 0');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : 0');
                    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : 0');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab1TCount').hide();
                    $('#tab1pcs').html('0');
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    //$('#tab1ppcts').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                }
                if ($('#myGrid .ag-paging-panel').length > 0) {

                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $('#ddlPagesize').val(GridpgSize);
                    clearInterval(showEntryVar);
                }
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
function DownloadExcel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'sRefNo').join(",");

    var iUserid = _.pluck(gridOptions.api.getSelectedRows(), 'iUserId'); //.join(",");
    var FullOrderDate = _.pluck(gridOptions.api.getSelectedRows(), 'FullOrderDate'); //.join(",")
    var iUserid_FullOrderDate = ([iUserid, FullOrderDate].reduce((a, b) => a.map((v, i) => v + b[i]))).join(",");

    if (stoneno == "") {
        stoneno = $('#txtStoneId').val();
    }

    //var status = $('#ddlStatus').val().join(",");
    var status = Status.join(",");

    $.ajax({
        url: "/Order/DownloadOrderHistory",
        type: "POST",
        data: {
            iUserid_FullOrderDate: iUserid_FullOrderDate,
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            CommonName: $('#txtCompanyName').val(),
            CompanyName: CompanyName,
            StoneNoList: stoneno,
            Status: status,
            UserName: UserName,
            CustomerName: CustomerName,
            Location: Location,
            OrderBy: orderBy,
            PageNo: 0,
            isAdmin: !IsObj,
            isEmp: !IsObj1,
            PgSize: GridpgSize,
            FormName: "Order History",
            ActivityType: "Excel Export",
            PickUp: PickUp, NotPickUp: NotPickUp, Collected: Collected, NotCollected: NotCollected, DateStatus: DateStatus, SubUser: SubUser
        },
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
function on() {
    document.getElementById("overlay").style.display = "block";
}
function off() {
    document.getElementById("overlay").style.display = "none";
}
function OrderHistory_Video_Status_Get() {
    $.ajax({
        url: "/SearchStock/OrderHistory_Video_Status_Get",
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            OrderHistory_Video_Displayed = true;
            if (data.Status == "1") {
                if (data.Data[0].Video_Visible == "Yes") {
                    on();
                    OrderHistory_Video_Displayed = true;
                }
                else if (data.Data[0].Video_Visible == "No") {
                    off()
                    OrderHistory_Video_Displayed = false;
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
            GetOrderData();
        }
        if (id == "PickUp") {
            PickUp = false;
            GetOrderData();
        }
        if (id == "NotPickUp") {
            NotPickUp = false;
            GetOrderData();
        }
        if (id == "Collected") {
            Collected = false;
            GetOrderData();
        }
        if (id == "NotCollected") {
            NotCollected = false;
            GetOrderData();
        }
        
        if (id == "Confirmed" || id == "NotAvailable" || id == "CheckingAvaibility") {
            id = (id == "NotAvailable" ? "Not Available" : id);
            id = (id == "CheckingAvaibility" ? "Checking Avaibility" : id);

            if (Status.includes(id)) {
                const index = Status.indexOf(id);
                if (index > -1) {
                    Status.splice(index, 1);
                }
            }
            GetOrderData();
        }
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "SubUser") {
            SubUser = true;
            GetOrderData();
        }
        if (id == "PickUp") {
            PickUp = true;
            NotPickUp = false;
            $("#NotPickUp").removeClass("btn-spn-opt-active");
            GetOrderData();
        }
        if (id == "NotPickUp") {
            NotPickUp = true;
            PickUp = false;
            $("#PickUp").removeClass("btn-spn-opt-active");
            Collected = false;
            $("#Collected").removeClass("btn-spn-opt-active");
            GetOrderData();
        }
        if (id == "Collected") {
            Collected = true;
            NotCollected = false;
            $("#NotCollected").removeClass("btn-spn-opt-active");
            NotPickUp = false;
            $("#NotPickUp").removeClass("btn-spn-opt-active");
            GetOrderData();
        }
        if (id == "NotCollected") {
            NotCollected = true;
            Collected = false;
            $("#Collected").removeClass("btn-spn-opt-active");
            GetOrderData();
        }
        
        if (id == "Confirmed" || id == "NotAvailable" || id == "CheckingAvaibility") {
            id = (id == "NotAvailable" ? "Not Available" : id);
            id = (id == "CheckingAvaibility" ? "Checking Avaibility" : id);
            
            if (!Status.includes(id)) {
                Status.push(id);
            }
            GetOrderData();
        }
    }
}
function OpenDownloadCheck() {
    if (gridOptions.api.getSelectedRows().length > 0) {
        $(".tab_1 #liAll_1").show();
    }
    else {
        $(".tab_1 #liAll_1").hide();
    }
}
function down_popup() {
    if ($(".tab_1").hasClass("dis_none")) {
        $(".tab_1").addClass("dis_block");
    }
    else {
        $(".tab_1").addClass("dis_none");
    }
}
function DownloadMedia(Type) {
    var count = 0, iorderid, stoneno, iOrderId_sRefNo;
    count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'iOrderid') != "" && count != 0) {
        //if (_.pluck(gridOptions.api.getSelectedRows(), 'sRefNo').join(",") != "") {
            iorderid = _.pluck(gridOptions.api.getSelectedRows(), 'iOrderid');
            stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'sRefNo');
        //}
        //else {
        //    iorderid = _.pluck(rowData, 'iOrderid');
        //    stoneno = _.pluck(rowData, 'sRefNo');
        //}
        iOrderId_sRefNo = ([iorderid, stoneno].reduce((a, b) => a.map((v, i) => v + b[i]))).join(",");

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/OrderHistoryMediaDownload",
            type: "POST",
            data: { SearchName: iOrderId_sRefNo, DownloadMedia: Type },
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                if (data.search('.zip') == -1 && data.search('.pdf') == -1) {
                    if (data.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    data = data.replace('Error to download video, video is not MP4', $("#hdn_Error_to_download_video_video_is_not_MP4").val());
                    data = data.replace('Image is not available in this stone', $("#hdn_Image_is_not_available_in_this_stone").val());
                    toastr.warning(data);
                } else {
                    location.href = data;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
    else {
        toastr.warning($("#hdn_No_stone_selected_for_download_as_a").val() + ' ' + Type + '!');
    }
}
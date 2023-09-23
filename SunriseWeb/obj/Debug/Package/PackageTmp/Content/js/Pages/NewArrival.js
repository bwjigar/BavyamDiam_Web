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
var AllD = false;
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
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";

function OpenDownloadCheck() {
    if (gridOptions.api.getSelectedRows().length > 0) {
        $(".download-toggle #liAll").show();
    } else {
        $(".download-toggle #liAll").hide();
    }
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

var gridDiv = document.querySelector('#Price-Revised-Grid');
var summary1 = [];
var GalleryDatalist = [];
var limit = 0;
var renderLimit = 0;
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
    //  { headerName: "SR.NO", field: "Sr", rowGroup: false, width: 100 }, 
    {
        headerName: $("#hdn_View_Image").val(), field: "ImagesLink", width: 90, cellRenderer: ImageValueGetter, suppressSorting: true,
        suppressMenu: true,
    },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "stone_ref_no", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
        }
    },
    {
        headerName: $("#hdn_Location").val(), field: "Location", tooltip: function (params) { return (params.value); }, width: 70,
        cellClass: function (params) {
            if (params.data != undefined) {
                if (params.data.status == 'AVAILABLE OFFER') {
                    return 'offercls';
                }
                if (params.data.Location == 'Upcoming') {
                    return 'upcomingcls';
                }
            }
        },
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
        headerName: $("#hdn_Status").val(), field: "StoneStatus", width: 50, cellRenderer: function (params) {

            if (params.data == undefined) {
                return '';
            }
            return params.data.StoneStatus;
        }
    },
    {
        headerName: $("#hdn_Shape").val(), field: "shape", tooltip: function (params) { return (params.value); }, width: 60,
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
        headerName: $("#hdn_Pointer").val(), field: "pointer", tooltip: function (params) { return (params.value); }, width: 60,
        filter: getValuesAsync1("pointer"),
        filterParams: {
            values: getValuesAsync("pointer"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
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
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 70 },
    { headerName: $("#hdn_Certi_No").val(), field: "certi_no", tooltip: function (params) { return (params.value); }, rowGroup: false, width: 80 },
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
    },// COL
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
    },//CLAR
    {
        headerName: $("#hdn_CTS").val(), field: "cts", filter: 'agNumberColumnFilter',
        tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 50,
        cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); },
        filter: getValuesAsync1("cts"),
        filterParams: {
            values: getValuesAsync('cts'),
            resetButton: true,
            applyButton: true,
            filterOptions: ['inRange']
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: $("#hdn_Rap_Price_Doller").val(), field: "cur_rap_rate", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Rap_Amt_Doller").val(), field: "rap_amount", width: 90, tooltip: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Offer_Disc_Per").val(), field: "sales_disc_per", width: 90, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Offer_Value_Dollar").val(), field: "net_amount", width: 95, tooltip: function (params) { return formatNumber(params.value); }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Price_Cts").val(), field: "price_per_cts", width: 75, cellRenderer: function (params) { return formatNumber(params.value); }, cellRenderer: function (params) { return formatNumber(params.value) } },

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
        },
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
        },
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
        },
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
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    { headerName: $("#hdn_Length").val(), field: "length", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Width").val(), field: "width", width: 50, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Depth").val(), field: "depth", width: 50, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Depth_Per").val(), field: "depth_per", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Table_Per").val(), field: "table_per", width: 60, tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Key_to_symbol").val(), field: "symbol", tooltip: function (params) { return (params.value); }, width: 350 },
    { headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    //{ headerName: "Luster /Milky", field: "Luster", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Table_Black").val(), field: "table_natts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Natts").val(), field: "Crown_Natts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Table_White").val(), field: "inclusion", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: $("#hdn_Crown_White").val(), field: "Crown_Inclusion", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_CR_HT").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "crown_height", width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_angle", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, field: "pav_height", width: 60, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, },
    { headerName: $("#hdn_Table_Open").val(), tooltip: function (params) { return (params.value); }, field: "Table_Open", width: 75, filter: false },
    { headerName: $("#hdn_Crown_Open").val(), tooltip: function (params) { return (params.value); }, field: "Crown_Open", width: 80, filter: false },
    { headerName: $("#hdn_Pav_Open").val(), tooltip: function (params) { return (params.value); }, field: "Pav_Open", width: 70, filter: false },
    { headerName: $("#hdn_Girdle_Open").val(), tooltip: function (params) { return (params.value); }, field: "Girdle_Open", width: 80, filter: false },
    { headerName: $("#hdn_girdle").val(), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88 },
    { headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) { return (params.value); }, field: "girdle_type", width: 90 },
    { headerName: $("#hdn_Laser_in_SC").val(), tooltip: function (params) { return (params.value); }, field: "sInscription", width: 90 },
    { headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
    { headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
];

function StatusValueGetter(params) {

    if (params.value == "N")
        return '<div class="newStatus"><span>N</span></div>';
    else if (params.value == "AVAILABLE")
        return '<div class="activeStatus"><span>A</span></div>';
    else if (params.value == "AVAILABLE OFFER")
        return '<div class="offerStatus"><span>O</span></div>';

    else if (params.value == "BUSS. PROCESS")
        return '<div class="busyStatus"><span>B</span></div>';
}
function LotValueGetter(params) {
    setTimeout(function () {
        $('.offercls').parent().addClass('offerrow');
        $('.upcomingcls').parent().addClass('upcomingrow');
    }, 0);
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
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function ImageValueGetter(params) {
    //var image_url = (params.data.image_url != null) ? 'frame.svg' : 'image-not-available.svg';
    //var movie_url = (params.data.movie_url != null) ? 'video-recording.svg' : 'video-recording-not-available.svg';
    //var certi_url = (params.data.view_certi_url != null) ? 'medal.svg' : 'medal-not-available.svg';
    //var image_url1 = (params.data.image_url != null) ? params.data.image_url : 'javascript:void(0);';
    //var movie_url1 = (params.data.movie_url != null) ? params.data.movie_url : 'javascript:void(0);';
    //var certi_url1 = (params.data.view_certi_url != null) ? params.data.view_certi_url : 'javascript:void(0);';
    //var data =
    //    '<ul class="flat-icon-ul"><li><a href="' + image_url1 + '" target="_blank" title="View Diamond Images"><img src="../Content/images/' + image_url + '" class="frame-icon"></a></li><li><a href="' + movie_url1 + '" target="_blank" title="View Diamond Images"><img src="../Content/images/' + movie_url + '" class="frame-icon"></a></li><li><a href="' + certi_url1 + '" target="_blank" title="View Diamond Certificate"><img src="../Content/images/' + certi_url + '" class="medal-icon"></a></li></ul>';
    //return data;
    return params.value;
}
function onBodyScroll(params) {
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
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
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetNewArrivalData();
}
//===========================================  Summary Calculation  =============================================//
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
    GET_Scheme_Disc();
    GetDashboardCount();
    $('#gallerypoplia').on('click', function () {
        $('#gallery-popup').toggleClass('show');
        $('.aggrid-section.gallery-grid').toggleClass('close');
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
    GetCompanyList();
    $("#txtCompanyName").focusout(function () {
        CmpnynmSelectRequired();
    });
});

document.addEventListener('DOMContentLoaded', function () {
    agGrid.LicenseManager.setLicenseKey("345b4a029e68391149aa2162aaa0807c");
    GetNewArrivalData();
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
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
});
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 156;
    $("#Price-Revised-Grid").css("height", contentHei);
}

$(document).ready(function () {
    contentHeight();
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
function columnVisible(params) {
    if (params.column.colId == 0 && params.visible) {
        $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
        $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
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
}
function onSelectionChanged(params) {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    if (gridOptions.api.getSelectedRows().length > 0) {
        dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;

        if (Scheme_Disc_Type == "Discount") {
            Net_Value = 0;
            Final_Disc = 0;
            Web_Benefit = 0;
        }
        else if (Scheme_Disc_Type == "Value") {
            Net_Value = parseFloat(TOT_NET_AMOUNT) + (parseFloat(TOT_NET_AMOUNT) * parseFloat(Scheme_Disc) / 100);
            Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(TOT_RAP_AMOUNT)) * 100) * -1;
            Web_Benefit = parseFloat(TOT_NET_AMOUNT) - parseFloat(Net_Value);
        }
        else {
            Net_Value = parseFloat(TOT_NET_AMOUNT);
            Final_Disc = parseFloat(AVG_SALES_DISC_PER);
            Web_Benefit = 0;
        }
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();
    } else {
        TOT_CTS = summary1.TOT_CTS;
        AVG_SALES_DISC_PER = summary1.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = summary1.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = summary1.TOT_NET_AMOUNT;
        TOT_PCS = summary1.TOT_PCS;
        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
    }

    //$('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(TOT_CTS) + '');
    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TOT_NET_AMOUNT) + '');
    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');
    $('#tab1TCount').show();
    $('#tab1pcs').html(TOT_PCS);
    $('#tab1cts').html(formatNumber(TOT_CTS));
    $('#tab1disc').html(formatNumber(AVG_SALES_DISC_PER));
    $('#tab1ppcts').html(formatNumber(AVG_PRICE_PER_CTS));
    $('#tab1totAmt').html(formatNumber(TOT_NET_AMOUNT));

    $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
    $('#tab1Net_Value').html(formatNumber(Net_Value));
    $('#tab1Final_Disc').html(formatNumber(Final_Disc));
}
function GetNewArrivalData() {

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
            statusIndicator: StatusValueGetter,
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
        onColumnVisible: columnVisible,
        onBodyScroll: onBodyScroll,
        onSelectionChanged: onSelectionChanged,
        overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize,
        paginationPageSize: pgSize,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#Price-Revised-Grid .ag-paging-panel').length > 0) {
            var a = $('.ag-header-select-all')[0];
            $(a).removeClass('ag-hidden');

            $(showEntryHtml).appendTo('#Price-Revised-Grid .ag-paging-panel');
            $('#ddlPagesize').val(pgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);
}

const datasource1 = {
    getRows(params) {

        obj.StoneStatus = 'N';
        obj.PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        obj.PgSize = pgSize;
        if (params.request.sortModel.length > 0) {
            obj.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }

        if (params.request.filterModel.cts) {
            var str = "";
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
            obj.Pointer = str;
        }
        else {
            obj.Pointer = "";
        }

        if (params.request.filterModel.shape) {
            obj.Shape = params.request.filterModel.shape.values.join(",");
        }
        else {
            obj.Shape = "";
        }

        if (params.request.filterModel.pointer) {
            obj.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        else {
            if (obj.Pointer == undefined || obj.Pointer == "")
                obj.Pointer = "";
        }

        if (params.request.filterModel.Lab) {
            obj.Lab = params.request.filterModel.Lab.values.join(",");
        }
        else {
            obj.Lab = "";
        }

        if (params.request.filterModel.color) {
            obj.Color = params.request.filterModel.color.values.join(",");
        }
        else {
            obj.Color = "";
        }

        if (params.request.filterModel.polish) {
            obj.Polish = params.request.filterModel.polish.values.join(",");
        }
        else {
            obj.Polish = "";
        }

        if (params.request.filterModel.clarity) {
            obj.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        else {
            obj.Clarity = "";
        }

        if (params.request.filterModel.fls) {
            obj.Fls = params.request.filterModel.fls.values.join(",");
        }
        else {
            obj.Fls = "";
        }

        if (params.request.filterModel.cut) {
            obj.Cut = params.request.filterModel.cut.values.join(",");
        }
        else {
            obj.Cut = "";
        }

        if (params.request.filterModel.symm) {
            obj.Symm = params.request.filterModel.symm.values.join(",");
        }
        else {
            obj.Symm = "";
        }

        if (params.request.filterModel.Location) {
            obj.Location = params.request.filterModel.Location.values.join(",");
        }
        else {
            obj.Location = "";
        }

        GalleryDatalist = [];
        $.ajax({
            url: "/NewArrival/GetNewArrivalStock",
            async: false,
            type: "POST",
            data: obj,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    var DataList = [];

                    $.map(data.Data[0].DataList, function (obj) {
                        GalleryDatalist.push(obj);
                    });
                    DataList = data.Data[0].DataList;
                    summary1 = data.Data[0].DataSummary;
                    if (DataList.length > 0) {
                        DataList.forEach(function (itm) {

                            if (itm.movie_url != null || itm.movie_url != undefined)
                                itm.IsMovieUrl = true;
                            else
                                itm.IsMovieUrl = false;
                            if (itm.ImageUrl1 != null || itm.ImageUrl1 != undefined)
                                itm.IsImageUrl = true;
                            else
                                itm.IsImageUrl = false;
                            if (itm.view_certi_url != null || itm.view_certi_url != undefined)
                                itm.IsCertiUrl = true;
                            else
                                itm.IsCertiUrl = false;


                            itm.ImageUrl1 = itm.ImageUrl1 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl1;
                            itm.ImageUrl2 = itm.ImageUrl2 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl2;
                            itm.ImageUrl3 = itm.ImageUrl3 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl3;
                            itm.ImageUrl4 = itm.ImageUrl4 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl4;
                        });

                        params.successCallback(DataList, summary1.TOT_PCS);

                        //$('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary1.TOT_CTS) + '');
                        //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary1.AVG_SALES_DISC_PER) + '');
                        //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary1.AVG_PRICE_PER_CTS) + '');
                        //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary1.TOT_NET_AMOUNT) + '');
                        //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + summary1.TOT_PCS + '');
                        $('#tab1TCount').show();
                        $('#tab1pcs').html(summary1.TOT_PCS);
                        $('#tab1cts').html(formatNumber(summary1.TOT_CTS));
                        $('#tab1disc').html(formatNumber(summary1.AVG_SALES_DISC_PER));
                        $('#tab1ppcts').html(formatNumber(summary1.AVG_PRICE_PER_CTS));
                        $('#tab1totAmt').html(formatNumber(summary1.TOT_NET_AMOUNT));
                        $('#tab1_WebDisc_t').hide();
                        $('#tab1_FinalValue_t').hide();
                        $('#tab1_FinalDisc_t').hide();
                    }
                    else {
                        params.successCallback([], 0);
                        gridOptions.api.showNoRowsOverlay();
                        //$('#tab1cts').html($("#hdn_Cts").val() +' : 0');
                        //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                        //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ 0');
                        //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ 0');
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
                        //toastr.error("No Data Available ", 2500);
                    }
                }
                else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1TCount').hide();
                    $('#tab1pcs').html('0');
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    $('#tab1ppcts').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                    //toastr.error("No Data Available ", 2500);
                }
                setTimeout(function () {
                    limit = 0;
                    $('#dvGalleryView').html("");
                    if (GalleryDatalist.length > 12) {
                        renderLimit = 12;
                        $('#btnLoadMore').show();
                    } else {
                        renderLimit = GalleryDatalist.length;
                        $('#btnLoadMore').hide();
                    }
                    BindGalleryView();
                }, 1000);
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
function LoadMore() {
    renderLimit = renderLimit + 12;
    if (GalleryDatalist.length > renderLimit) {
        $('#btnLoadMore').show();
    } else {
        renderLimit = GalleryDatalist.length;
        $('#btnLoadMore').hide();
    }
    BindGalleryView();
}
function BindGalleryView() {
    //$('#dvGalleryView1').html("");

    for (var i = limit; i < renderLimit; i++) {
        limit = limit + 1;

        $('#dvGalleryView').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (GalleryDatalist[i].bPRimg ? $("#External_ImageURL").val() + GalleryDatalist[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + GalleryDatalist[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + GalleryDatalist[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + GalleryDatalist[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + GalleryDatalist[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + GalleryDatalist[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Discount").val() + '</span>:<span>"' + GalleryDatalist[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Net_Amt").val() + '</span>:<span>"' + GalleryDatalist[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + GalleryDatalist[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + GalleryDatalist[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
function StatusWiseData(Sts) {
    if (Sts == "NEW") {
        GetNewArrivalData();
    } else {
        gridOptions.api.showNoRowsOverlay();
    }
}
/*--------------------------------------------------------ADD TO CART START--------------------------------------------------*/
function AddToCart() {
    var stoneList = [];
    stoneList = gridOptions.api.getSelectedRows();

    var availabelstonelist = '';
    var offerstonelist = '';
    var availabelstonelist = '';
    var offerstonelist = '';
    if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
        availabelstonelist = _.pluck(stoneList, 'stone_ref_no').join(",");
    } else {
        availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' }), 'stone_ref_no').join(",");
        offerstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' }), 'stone_ref_no').join(",");
    }

    if (availabelstonelist != '') {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/AddToCart",
            type: "POST",
            data: { stoneNo: availabelstonelist, transType: 'A' },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Status == "0") {
                    if (data.Message.indexOf('already added to cart') > -1) {
                        $('#cartresMsg').html('<div>' + data.Message + '</div>');
                        $('#cartModal').modal('show');
                        GetDashboardCount();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                } else {
                    if (offerstonelist != '') {
                        $('#cartresMsg').html(' <div>' + offerstonelist + ' </div>' +
                            '<div>' + $("#hdn_This_Stone_is_not_Available_Stone_You_can_add_only_available_Stone_into_Cart").val() + '...!</div>' +
                            ' <div>' + $("#hdn_Other_Stones_are_added_into_cart_successfully").val() + '...!</div>')
                    } else {
                        $('#cartresMsg').html(data.Message)
                    }
                    $('#cartModal').modal('show');
                    GetDashboardCount();
                }
                if (gridOptions != null) {
                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
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
    else if (offerstonelist != '') {
        toastr.warning($("#hdn_Select_Avail_Stone_for_add_to_cart").val() + '!');
    }
    else {
        toastr.warning($("#hdn_No_Stone_Selected_for_add_to_cart").val() + '!');
    }
}

function GoToCart() {
    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
    if (stoneno == '') {
        window.location = "/Cart/Index";
    }
    else {
        AddToCart();
    }
}
/*--------------------------------------------------------ADD TO CART END----------------------------------------------------*/
/*--------------------------------------------------------ADD TO WISHLIST START----------------------------------------------*/

function AddToWishlist() {
    var stoneno = '';
    var count = 0;
    count = gridOptions.api.getSelectedRows().length;
    stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (count > 0) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/AddToWishlist",
            type: "POST",
            data: { stoneNo: stoneno, transType: 'A' },
            success: function (data, textStatus, jqXHR) {
                data.Message = data.Message.replace('Stone(s) added in wishList successfully', $("#hdn_Stones_added_in_wishlist_successfully").val());
                data.Message = data.Message.replace('Stone(s) removed from wishList successfully', $("#hdn_Stones_removed_from_wishList_successfully").val());
                data.Message = data.Message.replace('Add in to wishList failed', $("#hdn_Add_in_to_wishList_failed").val());

                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Status == "0") {
                    if (data.Message.indexOf('already added to wishlist') > -1) {
                        $('#wishlistresMsg').html(data.Message);
                        $('#WishlistModal').modal('show');
                        GetDashboardCount();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                } else {
                    $('#wishlistresMsg').html(data.Message)
                    $('#WishlistModal').modal('show');
                    GetDashboardCount();
                }
                if (gridOptions != null) {
                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        toastr.warning($("#hdn_No_stone_selected_for_add_to_wishlist").val() + '!');
    }
}

function GoToWishlist() {
    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (stoneno == '') {
        window.location = "/Wishlist/Index";
    }
    else {
        AddToWishlist(TN);
    }
}

function GetDashboardCount() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/Dashboard/GetDashboardCount",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            $.each(data.Data, function (key, obj) {
                if (obj.Type == "MyCart") {
                    $('.cntCart').html(obj.sCnt);
                }
                else if (obj.Type == "WishList") {
                    $('.cntwishlist').html(obj.sCnt);
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
/*--------------------------------------------------------ADD TO WISHLIST END------------------------------------------------*/
function SendMail() {

    var isValid = $('#frmSendMail').valid();
    if (!isValid) {
        return false;
    }

    if ($('#customRadiomail').prop('checked')) {
        var sobj = {
            StoneStatus: 'N',
            FormName: 'New Arrival',
            ActivityType: 'Excel Email'
        };
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/EmailAllStone",
            type: "POST",
            data: {
                SearchCriteria: sobj, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val()
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {
                    data.Message = data.Message.replace('Mail sent successfully', $("#hdn_Mail_sent_successfully").val());
                    toastr.success(data.Message);
                }
                $('#EmailModal').modal('hide');
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    } else {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        if (count > 0) {
            $.ajax({
                url: "/SearchStock/EmailSelectedStone",
                type: "POST",
                data: {
                    StoneID: stoneno, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val(),
                    FormName: 'New Arrival', ActivityType: 'Excel Email'
                },
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else {
                        data.Message = data.Message.replace('Mail sent successfully', $("#hdn_Mail_sent_successfully").val());
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
            toastr.warning($("#hdn_No_stones_selected_to_send_email").val() + '!');
        }
    }
}
function validmail(e) {
    var emailID = $(e).val();
    emailID = emailID.split(',');
    for (var i = 0; i < emailID.length; i++) {
        if (!checkemail(emailID[i])) {
            toastr.error($("#hdn_Invalid_email_format").val());
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
function OpenComparStoneModel() {
    var stoneno = '';
    var count = 0;
    count = gridOptions.api.getSelectedRows().length;
    stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (count <= 1) {
        return toastr.warning($("#hdn_Please_select_at_least_2_item_for_compare").val());
    }
    else if (count >= 5) {
        return toastr.warning($("#hdn_You_can_compare_maximum_4_item").val());
    }
    else {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/CompareStones",
            type: "POST",
            data: { stoneNo: stoneno },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {

                    var str = '';
                    if (data.Data.length > 0) {
                        var ComparStoneList = data.Data[0];
                        str += '<tbody><tr>';
                        str += '<th><span>' + $("#hdn_Stock_Id").val() + ' :</span></th>';
                        ComparStoneList.ReferenceNo.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Photo_Real").val() + ' :</span></th>';
                        ComparStoneList.Imge1.forEach(function (item) {
                            str += '<td>';
                            str += '<span>';
                            if (item != "") {
                                str += '<img src="' + item + '" /></span>';
                            }
                            else {
                                str += '<img src="/Content/images/no-img1.jpg" /></span>';
                            }
                            str += '</td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Status").val() + ' :</span></th>';
                        ComparStoneList.Status.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Shape").val() + ' :</span></th>';
                        ComparStoneList.Shape.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Certi_No").val() + ' :</span></th>';
                        ComparStoneList.Lab.forEach(function (item, i) {
                            str += '<td><span>' + ComparStoneList.Lab[i] + '&nbsp;' + ComparStoneList.CertiNo[i] + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_BGM").val() + ' :</span></th>';
                        ComparStoneList.Shade.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Color").val() + ' :</span></th>';
                        ComparStoneList.Color.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Clarity").val() + ' :</span></th>';
                        ComparStoneList.Clarity.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Carat_Weight").val() + ' :</span></th>';
                        ComparStoneList.CaratWeight.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Rap_Price_Doller").val() + ' :</span></th>';
                        ComparStoneList.RapPrice.forEach(function (item) {
                            str += '<td><span>' + formatNumber(item) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Rap_Amt_Doller").val() + ' :</span></th>';
                        ComparStoneList.RapAmt.forEach(function (item) {
                            str += '<td><span>' + formatNumber(item) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Disc_Per").val() + ' : </span></th>';
                        ComparStoneList.Disc.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.Disc[i]) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Net_Amt").val() + ' :</span></th>';
                        ComparStoneList.net_amount.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.net_amount[i]) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Cut").val() + ' :</span></th>';
                        ComparStoneList.Cut.forEach(function (item, i) {
                            if (item == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Polish").val() + ' :</span></th>';
                        ComparStoneList.Polish.forEach(function (item, i) {
                            if (ComparStoneList.Cut[i] == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Symm").val() + ' :</span></th>';
                        ComparStoneList.Symmetry.forEach(function (item, i) {
                            if (ComparStoneList.Cut[i] == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Fls").val() + ' :</span></th>';
                        ComparStoneList.Flurescence.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Length").val() + ' :</span></th>';
                        ComparStoneList.Length.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Width").val() + ' :</span></th>';
                        ComparStoneList.Width.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Depth").val() + ' :</span></th>';
                        ComparStoneList.Depth.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Depth_Per").val() + ' :</span></th>';
                        ComparStoneList.TotalDepth.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_Per").val() + ' :</span></th>';
                        ComparStoneList.Table.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Key_to_symbol").val() + ' :</span></th>';
                        ComparStoneList.KeytoSymbol.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_White").val() + ' :</span></th>';
                        ComparStoneList.table_natts.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_White").val() + ' :</span></th>';
                        ComparStoneList.Crown_Natts.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_Black").val() + ' :</span></th>';
                        ComparStoneList.inclusion.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_Natts").val() + ' :</span></th>';
                        ComparStoneList.Crown_Inclusion.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_Angle").val() + ' : </span></th>';
                        ComparStoneList.CrAng.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_CR_HT").val() + ' : </span></th>';
                        ComparStoneList.CrHt.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Pav_Ang").val() + ' :</span></th>';
                        ComparStoneList.PavAng.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Pav_HT").val() + ' :</span></th>';
                        ComparStoneList.PavHt.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Girdle_Type").val() + ' :</span></th>';
                        ComparStoneList.GirdleType.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '</tbody>';
                        $("#tblCompare").empty().append(str);
                    }

                    $('#CompareStone').modal('show');
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
var availabelstonelist = '', availabelstonewithoutbusyary = '', availabelstonewithoutbusylist = '', offerstonelist = '',
    HoldStone_Lst = '', HoldStone_ary = [], HoldStone_tblbody = '', StoneLstForPlace = '', Hold_Stone_FortuneCode_Lst = '',
    not_authorize = '', Employee_Hold_Stone_FortuneCode_Lst = '', company_Userid = '', company_Fortunecode = '',
    Hold_List = [], UnHold_List = [], UnHold_Lst = '';
function ConfirmOrderModal() {
    var stoneList = [];
    availabelstonelist = '', availabelstonewithoutbusyary = '', availabelstonewithoutbusylist = '', offerstonelist = '',
        HoldStone_Lst = '', HoldStone_ary = [], HoldStone_tblbody = '', StoneLstForPlace = '', Hold_Stone_FortuneCode_Lst = '',
        not_authorize = '', Employee_Hold_Stone_FortuneCode_Lst = '', company_Userid = '', company_Fortunecode = '',
        Hold_List = [], UnHold_List = [], UnHold_Lst = '';
    $('#Comments').val("");
    $("#divPlaceOrderHoldCompany").hide();

    var stoneList = gridOptions.api.getSelectedRows();
    if ($('#hdnisadminflg').val() == '1') {
        debugger
        availabelstonelist = _.pluck(_.filter(stoneList), 'stone_ref_no').join(",");
        availabelstonewithoutbusyary = _.filter(stoneList, function (e) { return e.ForAdmin_Hold == 0 });
        availabelstonewithoutbusylist = _.pluck(_.filter(availabelstonewithoutbusyary), 'stone_ref_no').join(",");
        HoldStone_ary = _.filter(stoneList, function (e) { return (e.ForAdmin_Hold == 1) });
        HoldStone_Lst = _.pluck(_.filter(HoldStone_ary), 'stone_ref_no').join(",");

        for (var i = 0; i < availabelstonewithoutbusyary.length; i++) {
            UnHold_List.push({
                sRefNo: availabelstonewithoutbusyary[i].stone_ref_no
            });
        }
        UnHold_Lst = _.pluck(_.filter(UnHold_List), 'sRefNo').join(",");
    }
    else if ($('#hdnisempflg').val() == '1') {
        debugger
        availabelstonewithoutbusyary = _.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' || e.status == 'AVAILABLE OFFER' && e.ForAssist_Hold == 0 });
        availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' || e.status == 'AVAILABLE OFFER' && e.ForAssist_Hold == 0 }), 'stone_ref_no').join(",");
        not_authorize = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' && e.status != 'AVAILABLE OFFER' && e.ForAssist_Hold == 0 }), 'stone_ref_no').join(", ");
        HoldStone_ary = _.filter(stoneList, function (e) { return (e.ForAssist_Hold == 1) });
        HoldStone_Lst = _.pluck(_.filter(HoldStone_ary), 'stone_ref_no').join(",");

        for (var i = 0; i < availabelstonewithoutbusyary.length; i++) {
            UnHold_List.push({
                sRefNo: availabelstonewithoutbusyary[i].stone_ref_no
            });
        }
        UnHold_Lst = _.pluck(_.filter(UnHold_List), 'sRefNo').join(",");
    }
    else {
        debugger
        availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' && (e.ForCust_Hold == 0) }), 'stone_ref_no').join(",");
        offerstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' && (e.ForCust_Hold == 0) }), 'stone_ref_no').join(", ");

        HoldStone_ary = _.filter(stoneList, function (e) { return (e.ForCust_Hold == 1) });
        HoldStone_Lst = _.pluck(_.filter(HoldStone_ary), 'stone_ref_no').join(",");

        for (var i = 0; i < HoldStone_ary.length; i++) {
            Hold_List.push({
                sRefNo: HoldStone_ary[i].stone_ref_no,
                Hold_Party_Code: HoldStone_ary[i].Hold_Party_Code,
                Hold_CompName: HoldStone_ary[i].Hold_CompName
            });
        }
    }

    if ($('#hdnisadminflg').val() == '1') {
        debugger
        $('#ppPlaceOrderMsg').html('');
        if (availabelstonewithoutbusylist != "" && HoldStone_Lst != "") {
            $('#ppPlaceOrderMsg').append('<div>' + $("#hdn_PlaceOrderMsg_4").val() + '...!</div>');
        }
        if ($('#ppPlaceOrderMsg').html() != "") {
            $('#ConfirmOrderWarningModal').modal('show');
            $('#ConfirmOrderModal').modal('hide');
            return;
        }
    }
    if ($('#hdnisempflg').val() == '1') {
        debugger
        $('#ppPlaceOrderMsg').html('');
        if (availabelstonelist != "" && HoldStone_Lst != "") {
            $('#ppPlaceOrderMsg').append('<div>' + $("#hdn_PlaceOrderMsg_4").val() + '...!</div>');
        }
        if (not_authorize != "") {
            $('#ppPlaceOrderMsg').append('<div>' + $("#hdn_PlaceOrderMsg_5").val() + ' <b style="font-weight: 700;">' + not_authorize + '</b>...!</div>');
        }
        if ($('#ppPlaceOrderMsg').html() != "") {
            $('#ConfirmOrderWarningModal').modal('show');
            $('#ConfirmOrderModal').modal('hide');
            return;
        }
    }

    $("#divHoldList").html("");
    $('#pPlaceOrderMsg').html("");
    if (HoldStone_Lst != '') {
        debugger
        if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
            debugger
            var flag = 0;
            HoldStone_tblbody = "<center><table id='tblHold' border='1' style='font-size:12px; width:95%; margin-top:5px; display:block; max-height:154px; overflow-y:auto;'>";
            HoldStone_tblbody += "<thead>";
            HoldStone_tblbody += "<tr>";
            HoldStone_tblbody += "<td style='display:none;background-color: #003d66;color: white;padding: 3px;width: 5%;'><center><b>No.</b></center></td>";
            HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 15%;'><center><b>Stock ID</b></center></td>";
            HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 15%;'><center><b>Party Code</b></center></td>";
            HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 65%;'><b>Already Holded Company Name</b></td>";
            HoldStone_tblbody += "</tr>";
            HoldStone_tblbody += "</thead>";
            HoldStone_tblbody += "<tbody>";
            for (var i = 0; i < HoldStone_ary.length; i++) {
                var id = parseInt(i) + 1;
                HoldStone_tblbody += "<tr>";
                HoldStone_tblbody += "<td style='display:none;'><center><b>" + id + "</b></center></td>";
                HoldStone_tblbody += "<td><center><b>" + HoldStone_ary[i].stone_ref_no + "</b></center></td>";
                HoldStone_tblbody += "<td><center>" + (HoldStone_ary[i].Hold_Party_Code == 0 ? '' : HoldStone_ary[i].Hold_Party_Code) + "</center></td>";
                HoldStone_tblbody += "<td>" + HoldStone_ary[i].Hold_CompName + "</td>";
                HoldStone_tblbody += "</tr>";
                flag = 1;
            }
            HoldStone_tblbody += "</tbody>";
            HoldStone_tblbody += "</table></center>";
            if (flag == 0) {
                HoldStone_tblbody = "";
            }
            else {
                $("#divHoldList").html(HoldStone_tblbody);
            }

            $("#divPlaceOrderHoldCompany").show();
            $("#txtCompanyName_hidden").val("");
            $("#txtCompanyName").val("");

            if (HoldStone_Lst != '') {
                var HoldStone_Lst_space = _.pluck(_.filter(HoldStone_ary), 'stone_ref_no').join(", ");
                $('#pPlaceOrderMsg').html(
                    '<div>' + $("#hdn_PlaceOrderMsg_3").val() + ' <b>' + HoldStone_Lst_space + '</b>...!</div>');
            }
        }
        $('#ConfirmOrderModal').modal('show');
    }

    if (UnHold_Lst != "") {
        debugger
        if (_.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' }), 'stone_ref_no').join(",") == "") {
            debugger
            $("#divPlaceOrderHoldCompany").show();
            $("#txtCompanyName_hidden").val("");
            $("#txtCompanyName").val("");
            $('#ConfirmOrderModal').modal('show');
        }
        else {
            debugger
            $("#divPlaceOrderHoldCompany").hide();
            $("#txtCompanyName_hidden").val("");
            $("#txtCompanyName").val("");
        }
    }

    if (HoldStone_Lst != '') {
        debugger
        if ($('#hdnisadminflg').val() != '1') {
            StoneLstForPlace += "," + HoldStone_Lst;
        }
        if (availabelstonelist != '') {
            StoneLstForPlace += "," + availabelstonelist;
        }
        if (offerstonelist != '') {
            $('#pPlaceOrderMsg').html(
                '<div><b>' + offerstonelist + '</b> ' + $("#hdn_PlaceOrderMsg_1").val() + '...!</div>' +
                ' <div>' + $("#hdn_PlaceOrderMsg_2").val() + ' ? </div>');
        }
    }
    else if (availabelstonelist != '') {
        debugger
        $('#ConfirmOrderModal').modal('show');
        StoneLstForPlace += "," + availabelstonelist;
        if (offerstonelist != '') {
            $('#pPlaceOrderMsg').html(
                '<div><b>' + offerstonelist + '</b> ' + $("#hdn_PlaceOrderMsg_1").val() + ' ...!</div>' +
                ' <div>' + $("#hdn_PlaceOrderMsg_2").val() + ' ? </div>');
        }
    }
    else if (offerstonelist != '') {
        debugger
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        $.ajax({
            url: "/Order/GetAssistPersonDetail",
            type: "POST",
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {
                    $('#PlaceOrderMsg').html('<div>' + $("#hdn_Select_Avail_Stone_for_place_order").val() + ' !<br>' + $("#hdn_PleaseContact").val() + data.Message + '</div>');
                    $('#ConfirmOrderModal').modal('hide');
                    $('#ConfirmOrderWarningModal').modal('show');
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
        toastr.warning($("#hdn_No_Stone_Selected_for_place_order").val() + ' !');
    }
    StoneLstForPlace = StoneLstForPlace.substring(1);
}
function SaveOrder() {
    debugger
    company_Userid = '', company_Fortunecode = '';

    if ($("#Comments").val().trim() == "") {
        $("#Comments").val("");
        $("#Comments").focus();
        toastr.warning("Enter Comments");
        return;
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    setTimeout(function () {
        if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
            debugger
            if (HoldStone_Lst != '') {
                debugger
                if ($("#txtCompanyName_hidden").val().split("__").length != 2) {
                    debugger
                    Hold_List = [];
                    for (var i = 0; i < HoldStone_ary.length; i++) {
                        if (HoldStone_ary[i].Hold_Party_Code != 0) {
                            Hold_List.push({
                                sRefNo: HoldStone_ary[i].stone_ref_no,
                                Hold_Party_Code: HoldStone_ary[i].Hold_Party_Code,
                                Hold_CompName: HoldStone_ary[i].Hold_CompName
                            });
                        }
                        else if (HoldStone_ary[i].Hold_Party_Code == 0) {
                            Hold_List.push({
                                sRefNo: HoldStone_ary[i].stone_ref_no,
                                Hold_Party_Code: "0",
                                Hold_CompName: HoldStone_ary[i].Hold_CompName
                            });
                        }
                    }
                    if (StoneLstForPlace != "") {
                        PlaceOrder();
                    }
                }
                else {
                    debugger
                    company_Userid = $("#txtCompanyName_hidden").val().split("__")[0];
                    company_Fortunecode = $("#txtCompanyName_hidden").val().split("__")[1];

                    var Hold_obj = {};
                    Hold_obj.UserID = company_Userid;
                    Hold_obj.StoneID = HoldStone_Lst;
                    $.ajax({
                        url: "/SearchStock/Hold_Stone_Avail_Customers",
                        type: "POST",
                        data: { req: Hold_obj },
                        success: function (data, textStatus, jqXHR) {
                            if (data.Status != "1" && data.Message != "Success") {
                                toastr.error(data.Message);
                                $('.loading-overlay-image-container').hide();
                                $('.loading-overlay').hide();
                                return;
                            }
                            else {
                                Hold_List = [];
                                for (var i = 0; i < HoldStone_ary.length; i++) {
                                    if (HoldStone_ary[i].Hold_Party_Code != 0) {
                                        Hold_List.push({
                                            sRefNo: HoldStone_ary[i].stone_ref_no,
                                            Hold_Party_Code: HoldStone_ary[i].Hold_Party_Code,
                                            Hold_CompName: HoldStone_ary[i].Hold_CompName
                                        });
                                    }
                                    else if (HoldStone_ary[i].Hold_Party_Code == 0) {
                                        Hold_List.push({
                                            sRefNo: HoldStone_ary[i].stone_ref_no,
                                            Hold_Party_Code: "0",
                                            Hold_CompName: HoldStone_ary[i].Hold_CompName
                                        });
                                    }
                                }
                                if (StoneLstForPlace != "") {
                                    PlaceOrder();
                                }
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
                debugger
                if (UnHold_Lst != "") {
                    debugger
                    if ($("#txtCompanyName_hidden").val().split("__").length != 2) {
                        if (StoneLstForPlace != "") {
                            PlaceOrder();
                        }
                    }
                    else {
                        debugger
                        company_Userid = $("#txtCompanyName_hidden").val().split("__")[0];
                        company_Fortunecode = $("#txtCompanyName_hidden").val().split("__")[1];

                        var Hold_obj = {};
                        Hold_obj.UserID = company_Userid;
                        Hold_obj.StoneID = UnHold_Lst;
                        $.ajax({
                            url: "/SearchStock/Hold_Stone_Avail_Customers",
                            type: "POST",
                            data: { req: Hold_obj },
                            success: function (data, textStatus, jqXHR) {
                                debugger
                                if (data.Status != "1" && data.Message != "Success") {
                                    toastr.error(data.Message);
                                    $('.loading-overlay-image-container').hide();
                                    $('.loading-overlay').hide();
                                    return;
                                }
                                else {
                                    if (StoneLstForPlace != "") {
                                        PlaceOrder();
                                    }
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                $('.loading-overlay-image-container').hide();
                                $('.loading-overlay').hide();
                            }
                        });
                    }
                }
            }
        }
        else {
            if (StoneLstForPlace != "") {
                PlaceOrder();
            }
        }
    }, 30);
}

function PlaceOrder() {
    debugger
    var _obj = {};
    _obj.StoneID = StoneLstForPlace;
    _obj.Comments = $('#Comments').val();
    _obj.Userid = (company_Userid == '' ? 0 : company_Userid);
    _obj.IsAdminEmp_Hold = ((company_Userid == '' ? '0' : company_Userid) == '0' ? false : true);
    _obj.Hold_Stone_List = Hold_List;
    _obj.UnHold_Stone_List = UnHold_List;

    $.ajax({
        url: "/SearchStock/PlaceOrder_Web_1",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ req: _obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            debugger
            if (data.Status == "0") {
                debugger
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            } else {
                debugger
                var iOrderidsRefNo = data.Error;

                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                $('#ConfirmOrderModal').modal('hide');
                $('#ConfirmOrderWarningModal').modal('hide');
                //if (data.Message == 'Your Transaction Done Successfully') {
                //    $('#lblcheckingavailability').html($("#hdn_order_placed_success").val());
                //} else {
                //    $('#lblcheckingavailability').html($("#hdn_Transaction_Done_Success").val());
                //}

                data.Message = data.Message.replace('Your Transaction Done Successfully', $("#hdn_Your_Transaction_Done_Successfully").val());
                data.Message = data.Message.replace('This Stone(s) are subject to avaibility', $("#hdn_This_Stones_are_subject_to_availbility").val());
                data.Message = data.Message.replace('Please contact your sales person', $("#hdn_Please_contact_your_sales_person").val());

                $('#lblcheckingavailability').html(data.Message);

                debugger
                if (iOrderidsRefNo.includes('_') == true && iOrderidsRefNo != "" && data.Status == "SUCCESS") {
                    debugger
                    $.ajax({
                        url: "/ConfirmOrder/AUTO_PlaceConfirmOrder",
                        async: false,
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify({ iOrderid_sRefNo: iOrderidsRefNo }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data, textStatus, jqXHR) {
                            debugger

                        },
                        error: function (jqXHR, textStatus, errorThrown) {

                        }
                    });
                }

                $('#order-confirm-modal').modal('show');
                GetNewArrivalData();
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
    if ($('#customRadio3').prop('checked')) {
        var sobj = {
            StoneStatus: 'N',
            PageNo: 0,
            Location: obj.Location,
            Shape: obj.Shape,
            Color: obj.Color,
            Polish: obj.Polish,
            Pointer: obj.Pointer,
            Lab: obj.Lab,
            Fls: obj.Fls,
            Clarity: obj.Clarity,
            Cut: obj.Cut,
            Symm: obj.Symm,
            FormName: 'New Arrival',
            ActivityType: 'Excel Export',
        };
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/StockExcelDownloadBySearchObject",
            type: "POST",
            data: sobj,
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                location.href = data;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (count > 0) {
            $.ajax({
                url: "/Common/StockExcelDownloadByStoneId",
                type: "POST",
                data: { StoneID: stoneno, FormName: 'New Arrival', ActivityType: 'Excel Export' },
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
            toastr.warning($("#hdn_No_stone_selected_for_download_as_a_excel").val() + '!');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }

}
function DownloadMedia() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {
            StoneStatus: 'N',
            PageNo: 0,
            Location: obj.Location,
            Shape: obj.Shape,
            Color: obj.Color,
            Polish: obj.Polish,
            Pointer: obj.Pointer,
            Lab: obj.Lab,
            Fls: obj.Fls,
            Clarity: obj.Clarity,
            Cut: obj.Cut,
            Symm: obj.Symm,
            FormName: 'New Arrival',
            ActivityType: $('#hdnDownloadType').val() + ' Download',
        };
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/StockMediaDownloadBySearchObject",
            type: "POST",
            data: { obj: sobj, MediaType: $('#hdnDownloadType').val() },
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
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
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
    else {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (count > 0) {

            $.ajax({
                url: "/Common/StockMediaDownloadByStoneId",
                type: "POST",
                data: { StoneID: stoneno, MediaType: $('#hdnDownloadType').val(), FormName: 'Search Stock', ActivityType: $('#hdnDownloadType').val() + ' Download' },
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
            toastr.warning($("#hdn_No_stone_selected_for_download_as_a").val() + ' ' + $('#hdnDownloadType').val() + '!');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}
/*--------------------------------------------------------DOWNLOAD ALL END----------------------------------------------------*/
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
                    $('#txtCompanyName').ejAutocomplete({
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
                    $('#txtCompanyName').ejAutocomplete({
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
        if ($("#txtCompanyName_hidden").val().split("__").length != 2) {
            $("#txtCompanyName").val("");
            $("#txtCompanyName_hidden").val("");
        }
    }, 250);
}
function CmpnynmRst() {
    $("#txtCompanyName_hidden").val("");
}

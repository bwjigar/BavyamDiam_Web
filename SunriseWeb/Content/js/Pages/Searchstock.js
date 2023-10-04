var _Firstload = true;
var ParameterList;
var isModify = 0;
var gridOptions1 = {};
var gridOptions2 = {};
var gridOptions3 = {};
var gridOptions4 = {};
var gridOptions5 = {};
var gridOptions1_SelectedRows = {};
var gridOptions2_SelectedRows = {};
var gridOptions3_SelectedRows = {};
var gridOptions4_SelectedRows = {};
var gridOptions5_SelectedRows = {};
var rowData = [];
var columnDefs = [];
var columnConfigDefs = [];
var SizeGroupList = [];
var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];
var ShapeList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var LabList = [];
var PolishList = [];
var SymList = [];
var FlouList = [];
var CaratList = [];
var BgmList = [];
var BlackList = [];
var TblInclList = [];
var TblNattsList = [];
var CrwnInclList = [];
var CrwnNattsList = [];
var TableOpenList = [];
var CrownOpenList = [];
var PavOpenList = [];
var GirdleOpenList = [];
var LusterList = [];
var LocationList = [];
var CurencyList = [];
var tabno = 1;
var CurrentTab = 1;
var obj1 = {};
var obj2 = {};
var obj3 = {};
var obj4 = {};
var obj5 = {};
var summary1 = [];
var summary2 = [];
var summary3 = [];
var summary4 = [];
var summary5 = [];
var Datalist1 = [];
var Datalist2 = [];
var Datalist3 = [];
var Datalist4 = [];
var Datalist5 = [];
var limit1 = 0;
var renderLimit1 = 0;
var limit2 = 0;
var renderLimit2 = 0;
var limit3 = 0;
var renderLimit3 = 0;
var limit4 = 0;
var renderLimit4 = 0;
var limit5 = 0;
var pgSize1 = 50;
var pgSize2 = 50;
var pgSize3 = 50;
var pgSize4 = 50;
var pgSize5 = 50;
var renderLimit5 = 0;
var SmartSearchdata = "";
var isPlaceOrder = false;
var AllD = false;
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var d = new Date();
var curr_date = d.getDate();
var curr_month = d.getMonth();
var curr_year = d.getFullYear();
var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
var objSearch = {};
var StockFound = [];
var IsSearchFound = 0;
var gridOptions1Data = [];
var CurrencyTabName1 = '';
var CurrencyTabName2 = '';
var CurrencyTabName3 = '';
var CurrencyTabName4 = '';
var CurrencyTabName5 = '';
var Check_Color_1 = [];
var Check_Color_2 = [];
var Check_Color_3 = [];
var KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var Color_Type = 'Regular';
var StoneIDList = '';
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";
var FancyColor_Image_Displayed = true;
var IsFiltered = true;
var ActivityType = "";
function FancyDDLHide() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
}

function INTENSITYShow() {
    setTimeout(function () {
        if (C1 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").show();
            C1 = 1;
            KTS = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec1 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function OVERTONEShow() {
    setTimeout(function () {
        if (C2 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").show();
            C2 = 1;
            C1 = 0, KTS = 0, C3 = 0;
        }
        else {
            $("#sym-sec2 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FANCY_COLORShow() {
    setTimeout(function () {
        if (C3 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").show();
            C3 = 1;
            C1 = 0, KTS = 0, C2 = 0;
        }
        else {
            $("#sym-sec3 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FcolorBind() {
    //INTENSITY = ['W-X', 'Y-Z', 'Faint', 'Very Light', 'Light', 'Fancy Light', 'Fancy', 'Fancy Intense', 'Fancy Green', 'Fancy Dark',
    //    'Fancy Deep', 'Fancy Vivid', 'Fancy Faint', 'Dark'];
    //OVERTONE = ['None', 'Brownish', 'Grayish', 'Greenish', 'Yellowish', 'Pinkish', 'Orangey', 'Bluish', 'Reddish', 'Purplish'];
    //FANCY_COLOR = ['Yellow', 'Pink', 'Red', 'Green', 'Orange', 'Violet', 'Brown', 'Gray', 'Blue', 'Purple'];

    INTENSITY = ['W-X', 'Y-Z', 'FAINT', 'VERY LIGHT', 'LIGHT', 'FANCY LIGHT', 'FANCY', 'FANCY INTENSE', 'FANCY GREEN', 'FANCY DARK',
        'FANCY DEEP', 'FANCY VIVID', 'FANCY FAINT', 'DARK'];
    OVERTONE = ['NONE', 'BROWNISH', 'GRAYISH', 'GREENISH', 'YELLOWISH', 'PINKISH', 'ORANGEY', 'BLUISH', 'REDDISH', 'PURPLISH'];
    FANCY_COLOR = ['YELLOW', 'PINK', 'RED', 'GREEN', 'ORANGE', 'VIOLET', 'BROWN', 'GRAY', 'BLUE', 'PURPLE'];

    INTENSITY.sort();
    OVERTONE.sort();
    FANCY_COLOR.sort();
    INTENSITY.unshift("ALL SELECTED");
    OVERTONE.unshift("ALL SELECTED");
    FANCY_COLOR.unshift("ALL SELECTED");

    for (var i = 0; i <= INTENSITY.length - 1; i++) {
        $('#ddl_INTENSITY').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_I_' + i + '" name="CHK_I_' + i + '" onclick="GetCheck_INTENSITY_List(\'' + INTENSITY[i] + '\',' + i + ');">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + INTENSITY[i] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_INTENSITY').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var j = 0; j <= OVERTONE.length - 1; j++) {
        $('#ddl_OVERTONE').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_O_' + j + '" name="CHK_O_' + j + '" onclick="GetCheck_OVERTONE_List(\'' + OVERTONE[j] + '\',' + j + ');">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + OVERTONE[j] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_OVERTONE').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var k = 0; k <= FANCY_COLOR.length - 1; k++) {
        $('#ddl_FANCY_COLOR').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_F_' + k + '" name="CHK_F_' + k + '" onclick="GetCheck_FANCY_COLOR_List(\'' + FANCY_COLOR[k] + '\',' + k + ');" style="cursor:pointer;">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + FANCY_COLOR[k] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_FANCY_COLOR').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
}
function GetCheck_INTENSITY_List(item, id) {
    var res = _.filter(Check_Color_1, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_1 = [];
        if ($("#CHK_I_0").prop("checked") == true) {
            for (var i = 1; i <= INTENSITY.length - 1; i++) {
                Check_Color_1.push({
                    "NewID": Check_Color_1.length + 1,
                    "Symbol": INTENSITY[i],
                });
                $("#CHK_I_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= INTENSITY.length - 1; i++) {
                $("#CHK_I_" + i).prop('checked', false);
            }
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }
    else {
        $("#CHK_I_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_1.push({
                "NewID": Check_Color_1.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_1.length - 1; i++) {
                if (Check_Color_1[i].Symbol == item) {
                    $("#CHK_I_" + id).prop('checked', false);
                    Check_Color_1.splice(i, 1);
                }
            }
        }
        if (INTENSITY.length - 1 == Check_Color_1.length) {
            $("#CHK_I_0").prop('checked', true);
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }

    setTimeout(function () {
        $("#sym-sec1 .carat-dropdown-main").show();
    }, 2);
}
function GetCheck_OVERTONE_List(item, id) {
    var res = _.filter(Check_Color_2, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_2 = [];
        if ($("#CHK_O_0").prop("checked") == true) {
            for (var i = 1; i <= OVERTONE.length - 1; i++) {
                Check_Color_2.push({
                    "NewID": Check_Color_2.length + 1,
                    "Symbol": OVERTONE[i],
                });
                $("#CHK_O_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= OVERTONE.length - 1; i++) {
                $("#CHK_O_" + i).prop('checked', false);
            }
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    else {
        $("#CHK_O_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_2.push({
                "NewID": Check_Color_2.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_2.length - 1; i++) {
                if (Check_Color_2[i].Symbol == item) {
                    $("#CHK_O_" + id).prop('checked', false);
                    Check_Color_2.splice(i, 1);
                }
            }
        }
        if (OVERTONE.length - 1 == Check_Color_2.length) {
            $("#CHK_O_0").prop('checked', true);
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec2 .carat-dropdown-main").show();
    }, 2);
}
function GetCheck_FANCY_COLOR_List(item, id) {
    var res = _.filter(Check_Color_3, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_3 = [];
        if ($("#CHK_F_0").prop("checked") == true) {
            for (var i = 1; i <= FANCY_COLOR.length - 1; i++) {
                Check_Color_3.push({
                    "NewID": Check_Color_3.length + 1,
                    "Symbol": FANCY_COLOR[i],
                });
                $("#CHK_F_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= FANCY_COLOR.length - 1; i++) {
                $("#CHK_F_" + i).prop('checked', false);
            }
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    else {
        $("#CHK_F_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_3.push({
                "NewID": Check_Color_3.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_3.length - 1; i++) {
                if (Check_Color_3[i].Symbol == item) {
                    $("#CHK_F_" + id).prop('checked', false);
                    Check_Color_3.splice(i, 1);
                }
            }
        }
        if (FANCY_COLOR.length - 1 == Check_Color_3.length) {
            $("#CHK_F_0").prop('checked', true);
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec3 .carat-dropdown-main").show();
    }, 2);
}
function resetINTENSITY() {
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
    C1 = 1;
    INTENSITYShow();
}
function resetOVERTONE() {
    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
    C2 = 1;
    OVERTONEShow();
}
function resetFANCY_COLOR() {
    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    C3 = 1;
    FANCY_COLORShow();
}
function Color_Hide_Show(type) {
    if (type == '1' || type == '3') {
        $("#div_Color").show();
        $("#div_Fancy_Color").hide();
        $("#Color_Hide_Show_1").addClass("active");
        $("#Color_Hide_Show_2").removeClass("active");
        Color_Type = "Regular";
    }
    else if (type == '2' || type == '4') {
        $("#div_Color").hide();
        $("#div_Fancy_Color").show();
        $("#Color_Hide_Show_3").removeClass("active");
        $("#Color_Hide_Show_3").removeClass("active_btn_active");
        $("#Color_Hide_Show_4").addClass("active");
        $("#Color_Hide_Show_4").addClass("active_btn_active");
        Color_Type = "Fancy";
        $(".carat-dropdown-main").hide();
        C1 = 0, C2 = 0, C3 = 0;
    }
}

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function CurrencyCountryListGet(flag, id) {
    var CountryId = 0;
    if (flag == "Select") {
        if ($("#ddlCountryId_" + id).val() == "") {
            $("#txtdExRate_" + id).val(0);
            document.getElementById("txtdExRate_" + id).disabled = true;
            document.getElementById("btnSaveCurrency_" + id).disabled = true;

            return;
        }
        else {
            CountryId = $("#ddlCountryId_" + id).val();
            document.getElementById("txtdExRate_" + id).disabled = false;
            document.getElementById("btnSaveCurrency_" + id).disabled = false;
        }
    }
    $.ajax({
        type: 'POST',
        url: '/SearchStock/CurrencyCountryListGet',
        data: { iCountryId: CountryId },
        dataType: "json",
        success: function (data) {
            if (flag == "SelectList") {
                var ddl = "<option value=''>Select</option>";
                for (i = 0; i <= data.Data.length - 1; i++) {
                    ddl += "<option value=" + data.Data[i].iCountryId + ">" + data.Data[i].sCountryName + "</option>"
                }
                $("#ddlCountryId_" + id).html(ddl);
                $("#txtdExRate_" + id).val("0");
                document.getElementById("txtdExRate_" + id).disabled = true;
                document.getElementById("btnSaveCurrency_" + id).disabled = true;
            }
            else if (flag == "Select") {
                $("#txtdExRate_" + id).val(data.Data[0].dExRate);
                if (id == 1) {
                    CurrencyTabName1 = data.Data[0].sSymbol;
                }
                else if (id == 2) {
                    CurrencyTabName2 = data.Data[0].sSymbol;
                }
                else if (id == 3) {
                    CurrencyTabName3 = data.Data[0].sSymbol;
                }
                else if (id == 4) {
                    CurrencyTabName4 = data.Data[0].sSymbol;
                }
                else if (id == 5) {
                    CurrencyTabName5 = data.Data[0].sSymbol;
                }
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function SaveCurrencyModal() {
    var tbnm = "", disornot = false;;
    if (CurrentTab == 1) {
        tbnm = CurrencyTabName1;
        gridOptions1 = {};
        tabno = 1;
        $("#myGrid1").html("");
    }
    else if (CurrentTab == 2) {
        tbnm = CurrencyTabName2;
        gridOptions2 = {};
        tabno = 2;
        $("#myGrid2").html("");
    }
    else if (CurrentTab == 3) {
        tbnm = CurrencyTabName3;
        gridOptions3 = {};
        tabno = 3;
        $("#myGrid3").html("");
    }
    else if (CurrentTab == 4) {
        tbnm = CurrencyTabName4;
        gridOptions4 = {};
        tabno = 4;
        $("#myGrid4").html("");
    }
    else if (CurrentTab == 5) {
        tbnm = CurrencyTabName5;
        gridOptions5 = {};
        tabno = 5;
        $("#myGrid5").html("");
    }

    if (tbnm != "") {
        disornot = false;
    }
    else {
        disornot = true;
    }
    if (columnDefs.length == 0) {
        BindColumnsSettings();
    }
    column = {
        headerName: $("#hdn_Amount_in").val() + " (" + tbnm + ")",
        Priority: 16,
        ColumnName: "net_amount_CurrencyAmt",
        iColumnId: 500,
        field: "net_amount_CurrencyAmt",
        tooltip: function (params) { return formatNumber(params.value); },
        width: 100,
        cellStyle: { color: 'red', 'font-weight': 'bold' },
        cellRenderer: function (params) { return formatNumber(params.value); },
        sortable: false,
        hide: disornot
    };

    for (var i = 0; i <= columnDefs.length - 1; i++) {
        if (columnDefs[i].ColumnName == "net_amount_CurrencyAmt") {
            columnDefs.splice(i, 1);
        }
    }
    columnDefs.push(column);
    columnDefs.sort(function (obj1, obj2) {
        // Ascending: first age less than the previous
        return obj1.Priority - obj2.Priority;
    });

    GetSearch();
    //gridOptions1.api.setServerSideDatasource(datasource1);
}
function AutoEmailModal1() {
    if (gridOptions1Data.Data.length == 0) {
        $.ajax({
            type: 'POST',
            url: '/SearchStock/SmartSearchURLGet',
            dataType: "json",
            success: function (data) {
                window.location = data.URL;
            }
        });
    }
}
function greaterThanDate(evt) {
    if ($.trim($('#txtValidityDate').val()) != "") {
        var fDate = FinalDate;
        var tDate = $.trim($('#txtValidityDate').val());
        if (fDate != "" && tDate != "") {
            if (new Date(tDate) >= new Date(fDate)) {
                return true;
            }
            else {
                evt.currentTarget.value = "";
                toastr.warning($("#hdn_Validity_date_must_be_greater_than_Today_date").val());
                ValidityDate();
                return false;
            }
        }
        else {
            return true;
        }
    }
}
function ValidityDate() {
    $('#txtValidityDate').daterangepicker({
        singleDatePicker: true,
        startDate: SetCurrentDate(),
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
}

function GetStockAvail() {
    if ($('#hdnType').val() != "QuickSearch" && $('#hdnType').val() != "Recent" && $('#hdnType').val() != "SaveSearch" && $('#hdnType').val() != "SmartSearch") {
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        $.ajax({
            url: "/SearchStock/GetStockAvail",
            async: false,
            type: "POST",
            data: { obj: objSearch, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Data != null) {
                    if (data.Data.length > 0) {
                        StockFound = data;
                        $('#ModalStockAvail').modal('show');
                    }
                    if (_Firstload == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
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
function GotoAvailStone() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    IsSearchFound = 1;

    SetSearchParameter1();

    obj1.Shape = StockFound.Data[0].Shape;
    obj1.Pointer = StockFound.Data[0].Pointer;
    obj1.Color = StockFound.Data[0].Color;
    obj1.Clarity = StockFound.Data[0].Clarity;
    obj1.Cut = StockFound.Data[0].Cut;
    obj1.Polish = StockFound.Data[0].Polish;
    obj1.Symm = StockFound.Data[0].Symm;
    obj1.Fls = StockFound.Data[0].Fls;
    obj1.Lab = StockFound.Data[0].Lab;
    obj1.Inclusion = StockFound.Data[0].Inclusion;
    obj1.Natts = StockFound.Data[0].Natts;
    obj1.Shade = StockFound.Data[0].Shade;
    obj1.FromCts = StockFound.Data[0].FromCts;
    obj1.ToCts = StockFound.Data[0].ToCts;
    obj1.FormDisc = StockFound.Data[0].FormDisc;
    obj1.ToDisc = StockFound.Data[0].ToDisc;
    obj1.FormPricePerCts = StockFound.Data[0].FormPricePerCts;
    obj1.ToPricePerCts = StockFound.Data[0].ToPricePerCts;
    obj1.FormNetAmt = StockFound.Data[0].FormNetAmt
    obj1.ToNetAmt = StockFound.Data[0].ToNetAmt;
    obj1.FormDepth = StockFound.Data[0].FormDepth;
    obj1.ToDepth = StockFound.Data[0].ToDepth;
    obj1.FormLength = StockFound.Data[0].FormLength;
    obj1.ToLength = StockFound.Data[0].ToLength;
    obj1.FormWidth = StockFound.Data[0].FormWidth;
    obj1.ToWidth = StockFound.Data[0].ToWidth;
    obj1.FormDepthPer = StockFound.Data[0].FormDepthPer;
    obj1.ToDepthPer = StockFound.Data[0].ToDepthPer;
    obj1.FormTablePer = StockFound.Data[0].FormTablePer;
    obj1.ToTablePer = StockFound.Data[0].ToTablePer;
    obj1.HasImage = StockFound.Data[0].HasImage;
    obj1.HasHDMovie = StockFound.Data[0].HasHDMovie;
    obj1.NewArrival = StockFound.Data[0].NewArrival;
    obj1.IsPromotion = StockFound.Data[0].IsPromotion;
    obj1.CrownInclusion = StockFound.Data[0].CrownInclusion;
    obj1.ShapeColorPurity = StockFound.Data[0].ShapeColorPurity;
    obj1.ReviseStockFlag = StockFound.Data[0].ReviseStockFlag;
    obj1.CrownNatts = StockFound.Data[0].CrownNatts;
    obj1.Luster = StockFound.Data[0].Luster;
    obj1.Location = StockFound.Data[0].Location;
    obj1.StoneStatus = StockFound.Data[0].StoneStatus;
    obj1.FromCrownAngle = StockFound.Data[0].FromCrownAngle;
    obj1.ToCrownAngle = StockFound.Data[0].ToCrownAngle;
    obj1.FromCrownHeight = StockFound.Data[0].FromCrownHeight;
    obj1.ToCrownHeight = StockFound.Data[0].ToCrownHeight;
    obj1.FromPavAngle = StockFound.Data[0].FromPavAngle;
    obj1.ToPavAngle = StockFound.Data[0].ToPavAngle;
    obj1.FromPavHeight = StockFound.Data[0].FromPavHeight;
    obj1.ToPavHeight = StockFound.Data[0].ToPavHeight;
    obj1.BGM = StockFound.Data[0].BGM;
    obj1.Black = StockFound.Data[0].Black;
    obj1.KeyToSymbol = StockFound.Data[0].KeyToSymbol;
    obj1.ColorType = StockFound.Data[0].ColorType;
    obj1.Intensity = StockFound.Data[0].Intensity;
    obj1.Overtone = StockFound.Data[0].Overtone;
    obj1.Fancy_Color = StockFound.Data[0].Fancy_Color;
    obj1.Table_Open = StockFound.Data[0].Table_Open;
    obj1.Crown_Open = StockFound.Data[0].Crown_Open;
    obj1.Pav_Open = StockFound.Data[0].Pav_Open;
    obj1.Girdle_Open = StockFound.Data[0].Girdle_Open;

    for (var c = 0; c <= columnDefs.length - 1; c++) {
        if (columnDefs[c].ColumnName == "sColor") {
            if (StockFound.Data[0].ColorType == "Fancy") {
                columnDefs[c].width = 175;
            }
            else if (StockFound.Data[0].ColorType == "Regular") {
                columnDefs[c].width = 50;
            }
        }
        //if (columnDefs[c].ColumnName == "dRepPrice") {
        //    if (StockFound.Data[0].ColorType == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (StockFound.Data[0].ColorType == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
        //if (columnDefs[c].ColumnName == "dRapAmount") {
        //    if (StockFound.Data[0].ColorType == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (StockFound.Data[0].ColorType == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
        //if (columnDefs[c].ColumnName == "dDisc") {
        //    if (StockFound.Data[0].ColorType == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (StockFound.Data[0].ColorType == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
    }
    GetSearch();

    if (StockFound.Status == 1) {
        $.ajax({
            url: "/SearchStock/ExpireAvailStock",
            async: false,
            type: "POST",
            data: { iId: StockFound.Data[0].iId },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == 0) {
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
    else if (StockFound.Status == 0) {
        toastr.error(StockFound.Message);
    }


    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();

    IsSearchFound = 0;
}
/*------------ order-history-dropdown-select ------------*/
$(document).ready(function (e) {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $('#tabhome').click(function () {
        IsFiltered = true;
    });
    $('#ddl_INTENSITY').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec1 .carat-dropdown-main").show();
            }
        }, 0.1);
    });
    $('#ddl_OVERTONE').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec2 .carat-dropdown-main").show();
            }
        }, 0.1);
    });
    $('#ddl_FANCY_COLOR').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec3 .carat-dropdown-main").show();
            }
        }, 0.1);
    });

    if ($('#hdnType').val() == "QuickSearch") {
        ActivityType = 'Quick Search';
    }
    else if ($('#hdnType').val() == "Recent") {
        ActivityType = 'Recent Search';
    }
    else if ($('#hdnType').val() == "SaveSearch") {
        ActivityType = 'Save Search';
    }
    else {
        ActivityType = 'Search Stock';
    }

    //if ($('#hdnType').val() != "SmartSearch" && $('#hdnType').val() != "Recent" && $('#hdnType').val() != "QuickSearch" && $('#hdnType').val() != "SaveSearch" && $('#hdnType').val() != "Modify") {
    //    FancyColor_Image_Status_Get();
    //}
    GET_Scheme_Disc();
    setTimeout(function () {
        FcolorBind();
        $(".aEmailStone").tooltip({ placement: "bottom" });
        ValidityDate();
        GetDashboardCount();
        CallingMarquee();

        $(".numeric").numeric({ decimal: ".", negative: true, decimalPlaces: 2 });

        $('#CurrencyModal').on('show.bs.modal', function (event) {
            $("#CurrencyModal .CurControl").hide();
            if (CurrentTab == 1) {
                $("#CurrencyModal #ddlCountryId_1").show();
                $("#CurrencyModal #txtdExRate_1").show();
                $("#CurrencyModal #btnSaveCurrency_1").show();
            }
            else if (CurrentTab == 2) {
                $("#CurrencyModal #ddlCountryId_2").show();
                $("#CurrencyModal #txtdExRate_2").show();
                $("#CurrencyModal #btnSaveCurrency_2").show();
            }
            else if (CurrentTab == 3) {
                $("#CurrencyModal #ddlCountryId_3").show();
                $("#CurrencyModal #txtdExRate_3").show();
                $("#CurrencyModal #btnSaveCurrency_3").show();
            }
            else if (CurrentTab == 4) {
                $("#CurrencyModal #ddlCountryId_4").show();
                $("#CurrencyModal #txtdExRate_4").show();
                $("#CurrencyModal #btnSaveCurrency_4").show();
            }
            else if (CurrentTab == 5) {
                $("#CurrencyModal #ddlCountryId_5").show();
                $("#CurrencyModal #txtdExRate_5").show();
                $("#CurrencyModal #btnSaveCurrency_5").show();
            }
        });

        $('#ConfirmOrderModal').on('show.bs.modal', function (event) {
            var count = 0;
            if (CurrentTab == 1) {
                count = gridOptions1.api.getSelectedRows().length;
            }
            else if (CurrentTab == 2) {
                count = gridOptions2.api.getSelectedRows().length;
            }
            else if (CurrentTab == 3) {
                count = gridOptions3.api.getSelectedRows().length;
            }
            else if (CurrentTab == 4) {
                count = gridOptions4.api.getSelectedRows().length;
            }
            else if (CurrentTab == 5) {
                count = gridOptions5.api.getSelectedRows().length;
            }
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

        $('#HoldStoneModal').on('show.bs.modal', function (event) {
            var count = 0;
            if (CurrentTab == 1) {
                count = gridOptions1.api.getSelectedRows().length;
            }
            else if (CurrentTab == 2) {
                count = gridOptions2.api.getSelectedRows().length;
            }
            else if (CurrentTab == 3) {
                count = gridOptions3.api.getSelectedRows().length;
            }
            else if (CurrentTab == 4) {
                count = gridOptions4.api.getSelectedRows().length;
            }
            else if (CurrentTab == 5) {
                count = gridOptions5.api.getSelectedRows().length;
            }
            if (count > 0) {
                $('#___frmSaveOrder #___Selected').show();
                $('#___frmSaveOrder #___NotSelected').hide();
                $('.modal-footer #___btnsaveOrderstone').show();
            } else {
                $('#___frmSaveOrder #___Selected').hide();
                $('#___frmSaveOrder #___NotSelected').show();
                $('.modal-footer #___btnsaveOrderstone').hide();
            }
        });

        $('#ExcelModalAll').on('show.bs.modal', function (event) {
            var count = 0;
            if (CurrentTab == 1) {
                count = gridOptions1.api.getSelectedRows().length;
            }
            else if (CurrentTab == 2) {
                count = gridOptions2.api.getSelectedRows().length;
            }
            else if (CurrentTab == 3) {
                count = gridOptions3.api.getSelectedRows().length;
            }
            else if (CurrentTab == 4) {
                count = gridOptions4.api.getSelectedRows().length;
            }
            else if (CurrentTab == 5) {
                count = gridOptions5.api.getSelectedRows().length;
            }
            if (count > 0) {
                $('#customRadio4').prop('checked', true);
            } else {
                $('#customRadio3').prop('checked', true);
            }
        });
        $('#EmailModal').on('show.bs.modal', function (event) {
            var count = 0;
            if (CurrentTab == 1) {
                count = gridOptions1.api.getSelectedRows().length;
            }
            else if (CurrentTab == 2) {
                count = gridOptions2.api.getSelectedRows().length;
            }
            else if (CurrentTab == 3) {
                count = gridOptions3.api.getSelectedRows().length;
            }
            else if (CurrentTab == 4) {
                count = gridOptions4.api.getSelectedRows().length;
            }
            else if (CurrentTab == 5) {
                count = gridOptions5.api.getSelectedRows().length;
            }
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

        $(document).mouseup(function (e) {
            if ($(e.target).closest(".sym-sec").length === 0) {
                $('.sym-sec').removeClass('active');
            }
        });
        $(".card").click(function () {
            $(".card").removeClass("active");
            $(this).addClass("active");
        });
        var li_selected = new Array();
        $('.carat2.common-li').on('click', "li", function () {

            var ab = $(this)
            if (!ab.is('.active')) {
                ab.addClass('active');

                var l_id = this.id;
                li_selected.push(l_id);

            } else {
                ab.removeClass('active');
                var l_id = this.id;
                var index = li_selected.indexOf(l_id);
                if (index > -1) {
                    li_selected.splice(index, 1);
                }
            }
        });
        var li_selected = new Array();
        $('ul.common-li').on('click', "li", function () {

            var ab = $(this)
            if (!ab.hasClass('active')) {
                ab.addClass('active');

                var l_id = this.id;
                li_selected.push(l_id);

            } else {
                ab.removeClass('active');
                var l_id = this.id;
                var index = li_selected.indexOf(l_id);
                if (index > -1) {
                    li_selected.splice(index, 1);
                }
            }
        });
        $('#gallerypoplia1').on('click', function () {
            $('#gallery-popup1').toggleClass('show');
            $('#aggrid-section1.gallery-grid').toggleClass('close');
        });
        $('#gallerypoplia2').on('click', function () {
            $('#gallery-popup2').toggleClass('show');
            $('#aggrid-section2.gallery-grid').toggleClass('close');
        });
        $('#gallerypoplia3').on('click', function () {
            $('#gallery-popup3').toggleClass('show');
            $('#aggrid-section3.gallery-grid').toggleClass('close');
        });
        $('#gallerypoplia4').on('click', function () {
            $('#gallery-popup4').toggleClass('show');
            $('#aggrid-section4.gallery-grid').toggleClass('close');
        });
        $('#gallerypoplia5').on('click', function () {
            $('#gallery-popup5').toggleClass('show');
            $('#aggrid-section5.gallery-grid').toggleClass('close');
        });
        $('#ic_Amount_in_Currency_1, #ic_Amount_in_Currency_2, #ic_Amount_in_Currency_3, #ic_Amount_in_Currency_4, #ic_Amount_in_Currency_5').on('click', function () {
            $('#CurrencyModal').modal('show');
        });
        var icon_selected = new Array();
        $('ul.search').on('click', ".common-ico", function () {

            var aa = $(this)
            if (!aa.is('.active')) {
                aa.addClass('active');

                var my_id = this.id;
                icon_selected.push(my_id);

            } else {
                aa.removeClass('active');
                var my_id = this.id;
                var index = icon_selected.indexOf(my_id);
                if (index > -1) {
                    icon_selected.splice(index, 1);
                }
            }
        });
        //$('#frmSaveOrder').validate({
        //    rules: {
        //        field: {
        //            required: true
        //        }
        //    },
        //    messages: {
        //        comments: "Enter Comment"
        //    }
        //});
        $('#frmSendMail').validate({
            rules: {
                field: {
                    required: true
                }
            },
            messages: {
                email: "Enter Email"
            }
        });
        $('#ConfirmOrderModal').on('hidden.bs.modal', function () {
            $('#Comments').val("");
        });
        $('#HoldStoneModal').on('hidden.bs.modal', function () {
            $('#___Comments').val("");
        });
        $("#myTab").on("click", "a", function (e) {

            CurrentTab = $(this).attr('tabindex');
            if (CurrentTab > 0) {
                ResetActive();
                $('#iresult' + CurrentTab + '').show();
                if ($('#result1').css('display') == 'none') {
                    tabno = 1;
                }
                else if ($('#result2').css('display') == 'none') {
                    tabno = 2;
                }
                else if ($('#result3').css('display') == 'none') {
                    tabno = 3;
                }
                else if ($('#result4').css('display') == 'none') {
                    tabno = 4;
                }
                else if ($('#result5').css('display') == 'none') {
                    tabno = 5;
                }

                if ($('#result1').css('display') == 'block') {
                    $('#iresult1').show();
                }
                if ($('#result2').css('display') == 'block') {
                    $('#iresult2').show();
                }
                if ($('#result3').css('display') == 'block') {
                    $('#iresult3').show();
                }
                if ($('#result4').css('display') == 'block') {
                    $('#iresult4').show();
                }
                if ($('#result5').css('display') == 'block') {
                    $('#iresult5').show();
                }
            }
        }).on("click", "i", function (e) {
            e.preventDefault();
            tabno = $(this).parent().attr('tabindex');
            CurrentTab = $(this).parent().attr('tabindex');
            $(this).parent().hide();
            $(this).hide();
            $('.result-page-content').removeClass('active show');
            $('.result-page-content').addClass('hide');
            $("#home-tab2").removeClass('hide');
            $(".nav-link").removeClass('active');
            $("#tabhome").addClass('active');
            $("#home-tab2").addClass('active show');
            e.stopImmediatePropagation();
        });
        contentHeight();
        //$('#aPlaceOrder1').tooltip({ placement: 'top' });
        GetSearchParameter();
        BindColumnsSettings();
        BindKeyToSymbolList();
        SetRecentSearchParameter();
        SetSavedSearchParameter();
        SetChartSearchParameter();
        SetQuickSearchParameter();
        SetSmartSearchParameter();
        SetModifySavedSearchParameter();
        $('.sym-sec').on('click', function () {
            $('.sym-sec').toggleClass('active');
        });
        //setTimeout(function () {
        if ($('#hdnType').val() == "") {
            $("#tabhome").parent().show();
            $("#home-tab2").show();
        }
        //}, 2000);
        GetStockAvail();
        CurrencyCountryListGet("SelectList", 1);
        CurrencyCountryListGet("SelectList", 2);
        CurrencyCountryListGet("SelectList", 3);
        CurrencyCountryListGet("SelectList", 4);
        CurrencyCountryListGet("SelectList", 5);

        if ($('#hdnType').val() == "NewArrival") {
            newarriv(1);
        }
    }, 1);
    GetCompanyList();
    $("#txtCompanyName").focusout(function () {
        CmpnynmSelectRequired();
    });

});
var GridRowClr = null;
GridRowClr = setInterval(function () {
    $('.offercls').parent().addClass('offerrow');
    $('.upcomingcls').parent().addClass('upcomingrow');
    //$('.tcpgopencls').parent().addClass('tcpgopen_row');
}, 100);

function OpenDownloadCheck() {
    if (CurrentTab == 1) {
        if (gridOptions1.api.getSelectedRows().length > 0) {
            $(".tab_1 #liAll_1").show();
        }
        else {
            $(".tab_1 #liAll_1").hide();
        }
    }
    if (CurrentTab == 2) {
        if (gridOptions2.api.getSelectedRows().length > 0) {
            $(".tab_2 #liAll_2").show();
        }
        else {
            $(".tab_2 #liAll_2").hide();
        }
    }
    if (CurrentTab == 3) {
        if (gridOptions3.api.getSelectedRows().length > 0) {
            $(".tab_3 #liAll_3").show();
        }
        else {
            $(".tab_3 #liAll_3").hide();
        }
    }
    if (CurrentTab == 4) {
        if (gridOptions4.api.getSelectedRows().length > 0) {
            $(".tab_4 #liAll_4").show();
        }
        else {
            $(".tab_4 #liAll_4").hide();
        }
    }
    if (CurrentTab == 5) {
        if (gridOptions5.api.getSelectedRows().length > 0) {
            $(".tab_5 #liAll_5").show();
        }
        else {
            $(".tab_5 #liAll_5").hide();
        }
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
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".tabs-marker").height(),
        resultHei = 0,
        contentHei = winH - (tabsmarkerHei + resultHei + 105);
    if (CurrentTab == "1") {
        resultHei = $("#profile-tab1 .result-nav").height();
        contentHei -= resultHei;
        $("#myGrid1").css("height", contentHei);
    }
    else if (CurrentTab == "2") {
        resultHei = $("#profile-tab2 .result-nav").height();
        contentHei -= resultHei;
        $("#myGrid2").css("height", contentHei);
    }
    else if (CurrentTab == "3") {
        resultHei = $("#profile-tab3 .result-nav").height();
        contentHei -= resultHei;
        $("#myGrid3").css("height", contentHei);
    }
    else if (CurrentTab == "4") {
        resultHei = $("#profile-tab4 .result-nav").height();
        contentHei -= resultHei;
        $("#myGrid4").css("height", contentHei);
    }
    else if (CurrentTab == "5") {
        resultHei = $("#profile-tab5 .result-nav").height();
        contentHei -= resultHei;
        $("#myGrid5").css("height", contentHei);
    }
}

$(window).resize(function () {
    contentHeight();
});

new WOW().init();

function closeOrderConfirmModal() {
    if ($("#hdnIsSubUser").val() == "False" || ($("#hdnIsSubUser").val() == "True" && $("#hdnOrderHisrory").val() == "True")) {
        window.location.href = "/Order/OrderHistory";
    }
    else {
        $('#order-confirm-modal').modal('hide');
    }
}
function closeHoldModal() {
    $('#Hold-confirm-modal').modal('hide');
    window.location.href = "/Order/HoldStoneReport";
}
function SetModifyParameter(tabNo) {
    if (tabNo == "1") {
        if ($('#hdnType').val() == "SmartSearch" || $('#hdnType').val() == "Recent" || $('#hdnType').val() == "QuickSearch" || $('#hdnType').val() == "NewArrival") {
            window.location.href = "/SearchStock/Search";
        }
        else if ($('#hdnType').val() == "SaveSearch") {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
            $.ajax({
                url: "/SearchStock/ModifySavedSearchDataSessionStore",
                async: false,
                type: "POST",
                data: { obj: obj1 },
                success: function (data, textStatus, jqXHR) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                    window.location.href = '/SearchStock/Search?type=Modify';
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        }
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    tabno = tabNo;
    CurrentTab = tabNo;
    $('#iresult' + tabNo + '').hide();
    $('.result-page-content').removeClass('active show');
    $('.result-page-content').addClass('hide');
    $("#home-tab2").removeClass('hide');
    $(".nav-link").removeClass('active');
    $("#tabhome").addClass('active');
    $("#home-tab2").addClass('active show');
    $.ajax({
        url: "/SearchStock/GetModifyStockParameter",
        async: false,
        type: "POST",
        data: { tabNo: tabNo },
        success: function (data, textStatus, jqXHR) {
            //$('#searchcaratspecific').
            //CheckKTS,UNCheckKTS,KeyToSymbol
            if (data.IsTripalEx) {
                $('#li3ex').addClass('active');
            } else {
                $('#li3ex').removeClass('active');
            }
            if (data.IsTripalVg) {
                $('#li3vg').addClass('active');
            } else {
                $('#li3vg').removeClass('active');
            }
            var checkkts = data.CheckKTS;
            if (checkkts != null) {
                checkkts = checkkts.split(',');
                $(checkkts).each(function (i, res) {

                    CheckKeyToSymbolList.push({
                        "NewID": KeyToSymbolList.length + 1,
                        "Symbol": res,
                    });
                    $('#searchkeytosymbol input[onclick="GetCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                });
                $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
            }
            var uncheckkts = data.UNCheckKTS;
            if (uncheckkts != null) {
                uncheckkts = uncheckkts.split(',');
                $(uncheckkts).each(function (i, res) {
                    UnCheckKeyToSymbolList.push({
                        "NewID": UnCheckKeyToSymbolList.length + 1,
                        "Symbol": res,
                    });
                    $('#searchkeytosymbol input[onclick="GetUnCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                });
                $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
            }

            var carattype = data.CaratType;

            var pointer = data.Pointer;
            if (pointer != null) {
                pointer = pointer.split(',');
                if (carattype == 'Specific') {
                    $(pointer).each(function (i, res) {
                        var res1 = res.split('-')
                        var NewID = SizeGroupList.length + 1;
                        SizeGroupList.push({
                            "NewID": NewID,
                            "FromCarat": res1[0],
                            "ToCarat": res1[1],
                            "Size": res,
                        });
                        //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                        $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                    });
                    $('a[href="#carat1"]').click();
                }
                else {
                    $(pointer).each(function (i, res) {
                        if (_.find(CaratList, function (num) { return num.Value == res; })) {
                            _.findWhere(CaratList, { Value: res }).ACTIVE = true;
                            $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                    $('a[href="#carat2"]').click()
                }
            }
            var shape = data.Shape;
            if (shape != null) {
                shape = shape.split(',');
                $(shape).each(function (i, res) {

                    if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                        _.findWhere(ShapeList, { Value: res }).ACTIVE = true;
                        $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var clarity = data.Clarity;
            if (clarity != null) {
                clarity = clarity.split(',');
                $(clarity).each(function (i, res) {

                    if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                        _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                        $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var cut = data.Cut;
            if (cut != null) {
                cut = cut.split(',');
                $(cut).each(function (i, res) {

                    if (_.find(CutList, function (num) { return num.Value == res; })) {
                        _.findWhere(CutList, { Value: res }).ACTIVE = true;
                        $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var polish = data.Polish;
            if (polish != null) {
                polish = polish.split(',');
                $(polish).each(function (i, res) {

                    if (_.find(PolishList, function (num) { return num.Value == res; })) {
                        _.findWhere(PolishList, { Value: res }).ACTIVE = true;
                        $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var symm = data.Symm;
            if (symm != null) {
                symm = symm.split(',');
                $(symm).each(function (i, res) {

                    if (_.find(SymList, function (num) { return num.Value == res; })) {
                        _.findWhere(SymList, { Value: res }).ACTIVE = true;
                        $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var fls = data.Fls;
            if (fls != null) {
                fls = fls.split(',');
                $(fls).each(function (i, res) {

                    if (_.find(FlouList, function (num) { return num.Value == res; })) {
                        _.findWhere(FlouList, { Value: res }).ACTIVE = true;
                        $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var lab = data.Lab;
            if (lab != null) {
                lab = lab.split(',');
                $(lab).each(function (i, res) {

                    if (_.find(LabList, function (num) { return num.Value == res; })) {
                        _.findWhere(LabList, { Value: res }).ACTIVE = true;
                        $('#searchlab li[onclick="SetActive(\'LAB\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var inclusion = data.Inclusion;
            if (inclusion != null) {
                inclusion = inclusion.split(',');
                $(inclusion).each(function (i, res) {

                    if (_.find(TblInclList, function (num) { return num.Value == res; })) {
                        _.findWhere(TblInclList, { Value: res }).ACTIVE = true;
                        $('#searchtableincl li[onclick="SetActive(\'TABLE_INCL\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var natts = data.Natts;
            if (natts != null) {
                natts = natts.split(',');
                $(natts).each(function (i, res) {

                    if (_.find(TblNattsList, function (num) { return num.Value == res; })) {
                        _.findWhere(TblNattsList, { Value: res }).ACTIVE = true;
                        $('#searchtablenatts li[onclick="SetActive(\'TABLE_NATTS\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var crowninclusion = data.CrownInclusion;
            if (crowninclusion != null) {
                crowninclusion = crowninclusion.split(',');
                $(crowninclusion).each(function (i, res) {

                    if (_.find(CrwnInclList, function (num) { return num.Value == res; })) {
                        _.findWhere(CrwnInclList, { Value: res }).ACTIVE = true;
                        $('#searchcrownincl li[onclick="SetActive(\'CROWN_INCL\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var crownnatts = data.CrownNatts;
            if (crownnatts != null) {
                crownnatts = crownnatts.split(',');
                $(crownnatts).each(function (i, res) {

                    if (_.find(CrwnNattsList, function (num) { return num.Value == res; })) {
                        _.findWhere(CrwnNattsList, { Value: res }).ACTIVE = true;
                        $('#searchcrownnatts li[onclick="SetActive(\'CROWN_NATTS\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var location = data.Location;
            if (location != null) {
                location = location.split(',');
                $(location).each(function (i, res) {

                    if (_.find(LocationList, function (num) { return num.Value == res; })) {
                        _.findWhere(LocationList, { Value: res }).ACTIVE = true;
                        $('#searchlocation li[onclick="SetActive(\'Location\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }
            var bgm = data.BGM;
            if (bgm != null) {
                bgm = bgm.split(',');
                $(bgm).each(function (i, res) {

                    if (_.find(BgmList, function (num) { return num.Value == res; })) {
                        _.findWhere(BgmList, { Value: res }).ACTIVE = true;
                        $('#searchbgm li[onclick="SetActive(\'BGM\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }

            var _tableopenlist = data.Table_Open;
            if (_tableopenlist != null) {
                _tableopenlist = _tableopenlist.split(',');
                $(_tableopenlist).each(function (i, res) {
                    if (_.find(TableOpenList, function (num) { return num.Value == res; })) {
                        _.findWhere(TableOpenList, { Value: res }).ACTIVE = true;
                        $('#searchtableopen li[onclick="SetActive(\'TABLEOPEN\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }

            var _crownopenlist = data.Crown_Open;
            if (_crownopenlist != null) {
                _crownopenlist = _crownopenlist.split(',');
                $(_crownopenlist).each(function (i, res) {
                    if (_.find(CrownOpenList, function (num) { return num.Value == res; })) {
                        _.findWhere(CrownOpenList, { Value: res }).ACTIVE = true;
                        $('#searchcrownopen li[onclick="SetActive(\'CROWNOPEN\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }

            var _pavopenlist = data.Pav_Open;
            if (_pavopenlist != null) {
                _pavopenlist = _pavopenlist.split(',');
                $(_pavopenlist).each(function (i, res) {
                    if (_.find(PavOpenList, function (num) { return num.Value == res; })) {
                        _.findWhere(PavOpenList, { Value: res }).ACTIVE = true;
                        $('#searchpavopen li[onclick="SetActive(\'PAVILIONOPEN\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }

            var _girdleopenlist = data.Girdle_Open;
            if (_girdleopenlist != null) {
                _girdleopenlist = _girdleopenlist.split(',');
                $(_girdleopenlist).each(function (i, res) {
                    if (_.find(GirdleOpenList, function (num) { return num.Value == res; })) {
                        _.findWhere(GirdleOpenList, { Value: res }).ACTIVE = true;
                        $('#searchgirdleopen li[onclick="SetActive(\'GIRDLEOPEN\',\'' + res + '\')"]').addClass('active');
                    }
                });
            }

            $('#txtDisFrom').val(data.FormDisc);
            $('#txtDisTo').val(data.ToDisc);
            $('#txtPrCtsFrom').val(data.FormPricePerCts);
            $('#txtPrCtsTo').val(data.ToPricePerCts);
            $('#TotalAmtFrom').val(data.FormNetAmt);
            $('#TotalAmtTo').val(data.ToNetAmt);
            $('#txtLengthFrom').val(data.FormLength);
            $('#txtLengthTo').val(data.ToLength);
            $('#txtWidthFrom').val(data.FormWidth);
            $('#txtWidthTo').val(data.ToWidth);
            $('#txtDepthPerFrom').val(data.FormDepthPer);
            $('#txtDepthPerTo').val(data.ToDepthPer);
            $('#txtTablePerFrom').val(data.FormTablePer);
            $('#txtTablePerTo').val(data.ToTablePer);
            $('#txtCrAngFrom').val(data.FromCrownAngle);
            $('#txtCrAngTo').val(data.ToCrownAngle);
            $('#txtCrHtFrom').val(data.FromCrownHeight);
            $('#txtCrHtTo').val(data.ToCrownHeight);
            $('#txtPavAngFrom').val(data.FromPavAngle);
            $('#txtPavAngTo').val(data.ToPavAngle);
            $('#txtPavHtFrom').val(data.FromPavHeight);
            $('#txtPavHtTo').val(data.ToPavHeight);
            if (data.HasImage) {
                $('#SearchImage').addClass('active');
            } else {
                $('#SearchImage').removeClass('active');
            }
            if (data.HasHDMovie) {
                $('#SearchVideo').addClass('active');
            } else {
                $('#SearchVideo').removeClass('active');
            }
            if (data.NewArrival) {
                $('#SearchNewArrival').addClass('active');
            } else {
                $('#SearchNewArrival').removeClass('active');
            }
            if (data.ColorType == "Regular") {
                Color_Hide_Show('1');
                var color = data.Color;
                if (color != null) {
                    color = color.split(',');
                    $(color).each(function (i, res) {

                        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
            }
            else if (data.ColorType == "Fancy") {
                Color_Hide_Show('2');
                $(".carat-dropdown-main").hide();
                C1 = 0, C2 = 0, C3 = 0;
                if (data.Intensity != null) {
                    var i = data.Intensity;
                    var _INTENSITY = i.split(',');
                    if (_INTENSITY.length == INTENSITY.length - 1) {
                        $("#CHK_I_0").prop('checked', true);
                        GetCheck_INTENSITY_List('ALL SELECTED', 0);
                    }
                    else {
                        for (var _j = 0; _j <= _INTENSITY.length - 1; _j++) {
                            for (var j = 0; j <= INTENSITY.length - 1; j++) {
                                if (_INTENSITY[_j] == INTENSITY[j]) {
                                    $("#CHK_I_" + j).prop('checked', true);
                                    GetCheck_INTENSITY_List(INTENSITY[j], j);
                                }
                            }
                        }
                    }
                }
                if (data.Overtone != null) {
                    var o = data.Overtone;
                    var _OVERTONE = o.split(',');
                    if (_OVERTONE.length == OVERTONE.length - 1) {
                        $("#CHK_O_0").prop('checked', true);
                        GetCheck_OVERTONE_List('ALL SELECTED', 0);
                    }
                    else {
                        for (var _j = 0; _j <= _OVERTONE.length - 1; _j++) {
                            for (var j = 0; j <= OVERTONE.length - 1; j++) {
                                if (_OVERTONE[_j] == OVERTONE[j]) {
                                    $("#CHK_O_" + j).prop('checked', true);
                                    GetCheck_OVERTONE_List(OVERTONE[j], j);
                                }
                            }
                        }
                    }
                }
                if (data.Fancy_Color != null) {
                    var fc = data.Fancy_Color;
                    var _FANCY_COLOR = fc.split(',');
                    if (_FANCY_COLOR.length == FANCY_COLOR.length - 1) {
                        $("#CHK_F_0").prop('checked', true);
                        GetCheck_FANCY_COLOR_List('ALL SELECTED', 0);
                    }
                    else {
                        for (var _j = 0; _j <= _FANCY_COLOR.length - 1; _j++) {
                            for (var j = 0; j <= FANCY_COLOR.length - 1; j++) {
                                if (_FANCY_COLOR[_j] == FANCY_COLOR[j]) {
                                    $("#CHK_F_" + j).prop('checked', true);
                                    GetCheck_FANCY_COLOR_List(FANCY_COLOR[j], j);
                                }
                            }
                        }
                    }
                }
            }
            IsFiltered = true;
            setTimeout(function () {
                FancyDDLHide();
            }, 2);
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function GetSearchParameter() {
    if (_Firstload == false) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }
    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            ParameterList = data.Data;
            if (ParameterList != null) {
                if (ParameterList.length > 0) {
                    _.each(ParameterList, function (itm) {
                        itm.ACTIVE = false;
                    });
                    ParameterList.push({
                        Id: 12, Value: "OTHERS", ListType: "SHAPE",
                        UrlValue: "https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg",
                        UrlValueHov: "https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png"
                    })

                    ParameterList.push({
                        Id: 13, Value: "ALL", ListType: "SHAPE",
                        UrlValue: "",
                        UrlValueHov: ""
                    })
                }
            }

            $('#searchcaratgen').html("");
            CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
            _(CaratList).each(function (carat, i) {
                $('#searchcaratgen').append('<li onclick="SetActive(\'carat\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
            });

            $('#searchshape').html("");
            $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div><span>ALL</span></a></li>');

            ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
            _(ShapeList).each(function (shape, i) {
                if (shape.Value != 'ALL') {
                    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
                }
            });

            $('#searchcolor').html("");
            ColorList = _.filter(ParameterList, function (e) { return e.ListType == 'COLOR' });
            _(ColorList).each(function (color, i) {
                $('#searchcolor').append('<li onclick="SetActive(\'COLOR\',\'' + color.Value + '\')">' + color.Value + '</li>');
            });

            $('#searchclarity').html("");
            ClarityList = _.filter(ParameterList, function (e) { return e.ListType == 'CLARITY' });
            _(ClarityList).each(function (clarity, i) {
                $('#searchclarity').append('<li onclick="SetActive(\'CLARITY\',\'' + clarity.Value + '\')">' + clarity.Value + '</li>');
            });

            $('#searchcut').html("");
            CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
            _(CutList).each(function (cut, i) {
                $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + cut.Value + '</li>');
            });

            $('#searchlab').html("");
            LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
            _(LabList).each(function (lab, i) {
                $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
            });

            $('#searchpolish').html("");
            PolishList = _.filter(ParameterList, function (e) { return e.ListType == 'POLISH' });
            _(PolishList).each(function (polish, i) {
                $('#searchpolish').append('<li onclick="SetActive(\'POLISH\',\'' + polish.Value + '\')">' + polish.Value + '</li>');
            });

            $('#searchsymm').html("");
            SymList = _.filter(ParameterList, function (e) { return e.ListType == 'SYMM' });
            _(SymList).each(function (sym, i) {
                $('#searchsymm').append('<li onclick="SetActive(\'SYMM\',\'' + sym.Value + '\')">' + sym.Value + '</li>');
            });

            $('#searchfls').html("");
            FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });
            _(FlouList).each(function (fls, i) {
                $('#searchfls').append('<li onclick="SetActive(\'FLS\',\'' + fls.Value + '\')">' + fls.Value + '</li>');
            });

            $('#searchbgm').html("");
            BgmList = _.filter(ParameterList, function (e) { return e.ListType == 'BGM' });
            _(BgmList).each(function (bgm, i) {
                $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
            });

            BlackList = _.filter(ParameterList, function (e) { return e.ListType == 'BLACK' });

            $('#searchtableincl').html("");
            TblInclList = _.filter(ParameterList, function (e) { return e.ListType == 'TABLE_INCL' });
            _(TblInclList).each(function (tblincl, i) {
                $('#searchtableincl').append('<li onclick="SetActive(\'TABLE_INCL\',\'' + tblincl.Value + '\')">' + tblincl.Value + '</li>');
            });

            $('#searchtablenatts').html("");
            TblNattsList = _.filter(ParameterList, function (e) { return e.ListType == 'TABLE_NATTS' });
            _(TblNattsList).each(function (tblnatts, i) {
                $('#searchtablenatts').append('<li onclick="SetActive(\'TABLE_NATTS\',\'' + tblnatts.Value + '\')">' + tblnatts.Value + '</li>');
            });

            $('#searchcrownincl').html("");
            CrwnInclList = _.filter(ParameterList, function (e) { return e.ListType == 'CROWN_INCL' });
            _(CrwnInclList).each(function (crwnincl, i) {
                $('#searchcrownincl').append('<li onclick="SetActive(\'CROWN_INCL\',\'' + crwnincl.Value + '\')">' + crwnincl.Value + '</li>');
            });

            $('#searchcrownnatts').html("");
            CrwnNattsList = _.filter(ParameterList, function (e) { return e.ListType == 'CROWN_NATTS' });
            _(CrwnNattsList).each(function (crwnnatt, i) {
                $('#searchcrownnatts').append('<li onclick="SetActive(\'CROWN_NATTS\',\'' + crwnnatt.Value + '\')">' + crwnnatt.Value + '</li>');
            });

            LusterList = _.filter(ParameterList, function (e) { return e.ListType == 'LUSTER' });

            $('#searchlocation').html("");
            LocationList = _.filter(ParameterList, function (e) { return e.ListType == 'LOCATION' });
            _(LocationList).each(function (loc, i) {
                $('#searchlocation').append('<li onclick="SetActive(\'Location\',\'' + loc.Value + '\')">' + loc.Value + '</li>');
            });

            CurencyList = _.filter(ParameterList, function (e) { return e.ListType == 'CURRENCY' });

            $('#searchtableopen').html("");
            TableOpenList = _.filter(ParameterList, function (e) { return e.ListType == 'TABLEOPEN' });
            _(TableOpenList).each(function (tableopen, i) {
                $('#searchtableopen').append('<li onclick="SetActive(\'TABLEOPEN\',\'' + tableopen.Value + '\')">' + tableopen.Value + '</li>');
            });

            $('#searchcrownopen').html("");
            CrownOpenList = _.filter(ParameterList, function (e) { return e.ListType == 'CROWNOPEN' });
            _(CrownOpenList).each(function (crownopen, i) {
                $('#searchcrownopen').append('<li onclick="SetActive(\'CROWNOPEN\',\'' + crownopen.Value + '\')">' + crownopen.Value + '</li>');
            });

            $('#searchpavopen').html("");
            PavOpenList = _.filter(ParameterList, function (e) { return e.ListType == 'PAVILIONOPEN' });
            _(PavOpenList).each(function (pavopen, i) {
                $('#searchpavopen').append('<li onclick="SetActive(\'PAVILIONOPEN\',\'' + pavopen.Value + '\')">' + pavopen.Value + '</li>');
            });

            $('#searchgirdleopen').html("");
            GirdleOpenList = _.filter(ParameterList, function (e) { return e.ListType == 'GIRDLEOPEN' });
            _(GirdleOpenList).each(function (girdleopen, i) {
                $('#searchgirdleopen').append('<li onclick="SetActive(\'GIRDLEOPEN\',\'' + girdleopen.Value + '\')">' + girdleopen.Value + '</li>');
            });

            if (_Firstload == false) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function BindKeyToSymbolList() {
    if (_Firstload == false) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }

    $.ajax({
        url: "/SearchStock/GetKeyToSymbolList",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var KeytoSymbolList = data.Data;
            $('#searchkeytosymbol').html("");
            if (KeytoSymbolList != null) {
                if (KeytoSymbolList.length > 0) {
                    $.each(KeytoSymbolList, function (i, itm) {
                        $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                            + '<ul class="row m-0">'
                            + '<li class="carat-dropdown-chkbox">'
                            + '<div class="main-cust-check">'
                            + '<label class="cust-rdi-bx mn-check">'
                            + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + (i + 1) + '" name="radio' + (i + 1) + '" onclick="GetCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                            + '<span class="cust-rdi-check">'
                            + '<i class="fa fa-check"></i>'
                            + '</span>'
                            + '</label>'
                            + '<label class="cust-rdi-bx mn-time">'
                            + '<input type="radio" id="UNCHK_KTS_Radio_' + (i + 1) + '" class="checkradio" name="radio' + (i + 1) + '" onclick="GetUnCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                            + '<span class="cust-rdi-check">'
                            + '<i class="fa fa-times"></i>'
                            + '</span>'
                            + '</label>'
                            + '</div>'
                            + '</li>'
                            + '<li class="col" style="text-align: left;margin-left: -15px;">'
                            + '<span>' + itm.sSymbol + '</span>'
                            + '</li>'
                            + '</ul>'
                            + '</div>')
                        //itm.ACTIVE = false;
                        //itm.INACTIVE = false;
                    });
                    $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
                }
            }
            _Firstload = false;
            if (_Firstload == false) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}


function GetCheck_KTS_List(item) {
    var SList = _.reject(UnCheckKeyToSymbolList, function (e) { return e.Symbol == item });
    UnCheckKeyToSymbolList = SList;

    var res = _.filter(CheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        CheckKeyToSymbolList.push({
            "NewID": CheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
function GetUnCheck_KTS_List(item) {
    var SList = _.reject(CheckKeyToSymbolList, function (e) { return e.Symbol == item });
    CheckKeyToSymbolList = SList

    var res = _.filter(UnCheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        UnCheckKeyToSymbolList.push({
            "NewID": UnCheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
function CloseSendMailPopup() {
    $('#EmailModal').modal('hide');
    $('#txtemail').val("");
    $('#txtNotes').val("");
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
function ClearSendMail() {
    $('#txtemail').val("");
    $('#txtNotes').val("");
}
function NewSizeGroup() {

    fcarat = $('#txtfromcarat').val();
    tcarat = $('#txttocarat').val();

    if (fcarat == "" && tcarat == "" || fcarat == 0 && tcarat == 0) {
        toastr.warning($("#hdn_Please_Enter_Carat").val());
        return false;
    }
    if (fcarat == "") {
        fcarat = "0";
    }
    var SizeGroupList_ = [];
    SizeGroupList_.push({
        "NewID": SizeGroupList.length + 1,
        "FromCarat": fcarat,
        "ToCarat": tcarat,
        "Size": fcarat + '-' + tcarat,

    });
    var lst = _.filter(SizeGroupList, function (e) { return (e.Size == SizeGroupList_[0].Size) });
    if (lst.length == 0) {
        SizeGroupList.push({
            "NewID": SizeGroupList_[0].NewID,
            "FromCarat": SizeGroupList_[0].FromCarat,
            "ToCarat": SizeGroupList_[0].ToCarat,
            "Size": SizeGroupList_[0].Size,
        });
        //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
        $('#searchcaratspecific').append('<li id="' + SizeGroupList_[0].NewID + '" class="carat-li-top">' + SizeGroupList_[0].Size + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + SizeGroupList_[0].NewID + ');"></i></li>');

        $('#txtfromcarat').val("0");
        $('#txttocarat').val("0");
    }
    else {
        $('#txtfromcarat').val("0");
        $('#txttocarat').val("0");
        toastr.warning($("#hdn_Carat_is_already_exist").val());
    }
    //SetSearchParameter();
}
function autoEmailModalHide() {
    $('#AutoEmailModal').modal('hide');
    $('#result1,#result2,#result3,#result4,#result5').removeClass('active');
    $('#profile-tab1,#profile-tab2,#profile-tab3,#profile-tab4,#profile-tab5').removeClass('active show');
    $('#tabhome').addClass('active');
    $('#home-tab2').addClass('active show');
    //document.getElementById("customRadio11").checked = true;
    ValidityDate();
}
function SaveGetSearchStock() {
    $("#hdnIsSearchNoFound").val("1");
    SetSearchParameter1();
}

function NewSizeGroupRemove(id) {
    $('#' + id).remove();
    var SList = _.reject(SizeGroupList, function (e) { return e.NewID == id });
    SizeGroupList = SList;
    //SetSearchParameter();
}
function SetSearchParameter(flag) {
    if (flag == 'savesearch') {
        if ($('#txtSearchName').val() == "") {
            return toastr.warning($("#hdn_Please_Fill_SearchName").val() + '!')
        }
    }
    if (columnDefs.length == 0) {
        BindColumnsSettings();
    }
    var SizeLst = "";
    var CaratType = "";
    SaveSearchList = [];
    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true });
    if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
        NewSizeGroup();
    }
    if (SizeGroupList.length != 0) {
        SizeLst = _.pluck(SizeGroupList, 'Size').join(",");
        CaratType = 'Specific';
    }
    if (lst.length != 0) {
        SizeLst = _.pluck(_.filter(CaratList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        CaratType = 'General';
    }
    var TripalEX = false;
    var TripalVG = false;
    var cut_Lst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polish_Lst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var sym_Lst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    if (cut_Lst == "EX" && polish_Lst == "EX" && sym_Lst == "EX") {
        $('#li3ex').addClass('active');
        TripalEX = true;
    }
    else {
        $('#li3ex').removeClass('active');
        TripalEX = false;
    }
    if (cut_Lst == "EX,VG" && polish_Lst == "EX,VG" && sym_Lst == "EX,VG") {
        $('#li3vg').addClass('active');
        TripalVG = true;
    }
    else {
        $('#li3vg').removeClass('active');
        TripalVG = false;
    }

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BGMLst = _.pluck(_.filter(BgmList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BLACKLst = _.pluck(_.filter(BlackList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var milkyLst = _.pluck(_.filter(LusterList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tableopenlst = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crownopenlst = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var pavopenlst = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var girdleopenlst = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    //var KeyToSymLst = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;

    //StockSearch.FromCarat = StockSearch.FromCarat == "" ? 0 : StockSearch.FromCarat == "NaN" ? 0 : StockSearch.FromCarat;
    //StockSearch.ToCarat = StockSearch.ToCarat == "" ? 0 : StockSearch.ToCarat == "NaN" ? 0 : StockSearch.ToCarat;

    var obj = {
        "Pointer": SizeLst,
        "CaratType": CaratType,
        "Shape": shapeLst,
        //"Color": colorLst,
        "Clarity": clarityLst,
        "Cut": cut_Lst,
        "Lab": labLst,
        "Polish": polish_Lst,
        "Location": locationLst,
        "Symm": sym_Lst,
        "BGM": BGMLst,
        "Fls": fluoLst,
        "BLACK": BLACKLst,
        "Inclusion": tblincLst,
        "Natts": tblnattsLst,
        "CrownInclusion": crwincLst,
        "CrownNatts": crwnattsLst,
        "Luster": milkyLst,

        "FromCts": "",
        "ToCts": "",
        "FormPricePerCts": $('#txtPrCtsFrom').val(),
        "ToPricePerCts": $('#txtPrCtsTo').val(),
        "FormDisc": $('#txtDisFrom').val(),
        "ToDisc": $('#txtDisTo').val(),

        "FormRapAmt": "",
        "ToRapAmt": "",

        "FormNetAmt": $('#TotalAmtFrom').val(),
        "ToNetAmt": $('#TotalAmtTo').val(),

        "FromCrownHeight": $('#txtCrHtFrom').val(),
        "ToCrownHeight": $('#txtCrHtTo').val(),

        "FromCrownAngle": $('#txtCrAngFrom').val(),
        "ToCrownAngle": $('#txtCrAngTo').val(),

        "FromPavHeight": $('#txtPavHtFrom').val(),
        "ToPavHeight": $('#txtPavHtTo').val(),

        "FromPavAngle": $('#txtPavAngFrom').val(),
        "ToPavAngle": $('#txtPavAngTo').val(),

        "FormTablePer": $('#txtTablePerFrom').val(),
        "ToTablePer": $('#txtTablePerTo').val(),

        "FormDepthPer": $('#txtDepthPerFrom').val(),
        "ToDepthPer": $('#txtDepthPerTo').val(),

        "FormLength": $('#txtLengthFrom').val(),
        "ToLength": $('#txtLengthTo').val(),

        "FormWidth": $('#txtWidthFrom').val(),
        "ToWidth": $('#txtWidthTo').val(),

        "FormDepth": "",
        "ToDepth": "",

        "StoneID": StoneIDList,

        "CertiNo": "",
        "ShapeColorPurity": $('#hdnShapeColorPurity').val(),

        "KeyToSymbol": KeyToSymLst_Check + '-' + KeyToSymLst_uncheck,//FinalList_KTS, //KeyToSymLst, //,StockSearch.KeyToSymbol == undefined ? "" : StockSearch.KeyToSymbol,
        "CheckKTS": KeyToSymLst_Check,
        "UNCheckKTS": KeyToSymLst_uncheck,
        "HasImage": $('#SearchImage').hasClass('active') ? true : "",
        "HasHDMovie": $('#SearchVideo').hasClass('active') ? true : "",
        "NewArrival": $('#SearchNewArrival').hasClass('active') ? true : "",
        "Shade": "",
        "ReviseStockFlag": "",
        "IsPromotion": "",
        "PageNo": 1,
        "TokenNo": "",
        "StoneStatus": "",
        "IsTripalEx": TripalEX,
        "IsTripalVg": TripalVG,
        "SmartSearch": SmartSearchdata,
        "ColorType": Color_Type,
        "Table_Open": tableopenlst,
        "Crown_Open": crownopenlst,
        "Pav_Open": pavopenlst,
        "Girdle_Open": girdleopenlst,
    };

    if (Color_Type == "Fancy") {
        obj.Intensity = _.pluck(Check_Color_1, 'Symbol').join(",");
        obj.Overtone = _.pluck(Check_Color_2, 'Symbol').join(",");
        obj.Fancy_Color = _.pluck(Check_Color_3, 'Symbol').join(",");
        obj.Color = "";
    }
    else if (Color_Type == "Regular") {
        obj.Color = colorLst;
        obj.Intensity = "";
        obj.Overtone = "";
        obj.Fancy_Color = "";
    }

    for (var c = 0; c <= columnDefs.length - 1; c++) {
        if (columnDefs[c].ColumnName == "sColor") {
            if (Color_Type == "Fancy") {
                columnDefs[c].width = 175;
            }
            else if (Color_Type == "Regular") {
                columnDefs[c].width = 50;

            }
        }
        //if (columnDefs[c].ColumnName == "dRepPrice") {
        //    if (Color_Type == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (Color_Type == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
        //if (columnDefs[c].ColumnName == "dRapAmount") {
        //    if (Color_Type == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (Color_Type == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
        //if (columnDefs[c].ColumnName == "dDisc") {
        //    if (Color_Type == "Fancy") {
        //        columnDefs[c].hide = true;
        //    }
        //    else if (Color_Typee == "Regular") {
        //        columnDefs[c].hide = false;
        //    }
        //}
    }

    if (flag == 'savesearch') {
        var Searchname = $('#txtSearchName').val();
        $("#save-src-modl").modal("hide");
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/SaveSearchData",
            type: "POST",
            async: false,
            data: { SearchId: $('#hdnSavedSearchId').val(), Searchname: Searchname, model: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (tabno <= 5) {
                    if (tabno == 1) {
                        Datalist1 = [];
                        limit1 = 0;
                        renderLimit1 = 0;
                        obj1 = obj;
                    }
                    else if (tabno == 2) {
                        Datalist2 = [];
                        limit2 = 0;
                        renderLimit2 = 0;
                        obj2 = obj;
                    }
                    else if (tabno == 3) {
                        Datalist3 = [];
                        limit3 = 0;
                        renderLimit3 = 0;
                        obj3 = obj;
                    }
                    else if (tabno == 4) {
                        obj4 = obj;
                        Datalist4 = [];
                        limit4 = 0;
                        renderLimit4 = 0;
                    }
                    else if (tabno == 5) {
                        obj5 = obj;
                        Datalist5 = [];
                        limit5 = 0;
                        renderLimit5 = 0;
                    }
                    GetSearch();
                } else {
                    toastr.warning($("#hdn_You_Can_Search_Maximum_5_Result").val());
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
        if (tabno <= 5) {
            if (tabno == 1) {
                Datalist1 = [];
                limit1 = 0;
                renderLimit1 = 0;
                if (IsSearchFound == 0) {
                    obj1 = obj;
                }
            }
            else if (tabno == 2) {
                obj2 = obj;
                Datalist2 = [];
                limit2 = 0;
                renderLimit2 = 0;
            }
            else if (tabno == 3) {
                obj3 = obj;
                Datalist3 = [];
                limit3 = 0;
                renderLimit3 = 0;
            }
            else if (tabno == 4) {
                obj4 = obj;
                Datalist4 = [];
                limit4 = 0;
                renderLimit4 = 0;
            }
            else if (tabno == 5) {
                obj5 = obj;
                Datalist5 = [];
                limit5 = 0;
                renderLimit5 = 0;
            }
            if (IsSearchFound == 0) {
                if ($("#hdnIsSearchNoFound").val() == "0") {
                    GetSearch();
                }
                else if ($("#hdnIsSearchNoFound").val() == "1") {
                    GetSearchSave();
                }
            }
        } else {
            toastr.warning($("#hdn_You_Can_Search_Maximum_5_Result").val());
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}

function SetSearchParameter1() {
    FancyDDLHide();
    IsFiltered = false;

    if (tabno > 1) {
        for (var i = 0; i <= columnDefs.length - 1; i++) {
            if (columnDefs[i].ColumnName == "net_amount_CurrencyAmt") {
                columnDefs.splice(i, 1);
            }
        }
    }

    if (tabno == 1)
        pgSize1 = 50;
    else if (tabno == 2)
        pgSize2 = 50;
    else if (tabno == 3)
        pgSize3 = 50;
    else if (tabno == 4)
        pgSize4 = 50;
    else if (tabno == 5)
        pgSize5 = 50;
    SmartSearchdata = "";
    SetSearchParameter();
    if ($("#hdnIsSearchNoFound").val() == "0") {
        contentHeight();
    }
}

function DownloadBySearchPara() {

    if (columnDefs.length == 0) {
        BindColumnsSettings();
    }

    if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
        NewSizeGroup();
    }

    var SizeLst = "";
    var CaratType = "";
    SaveSearchList = [];
    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true });
    if (SizeGroupList.length != 0) {
        SizeLst = _.pluck(SizeGroupList, 'Size').join(",");
        CaratType = 'Specific';
    }
    if (lst.length != 0) {
        SizeLst = _.pluck(_.filter(CaratList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        CaratType = 'General';
    }
    var TripalEX = false;
    var TripalVG = false;
    var cut_Lst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polish_Lst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var sym_Lst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    if (cut_Lst == "EX" && polish_Lst == "EX" && sym_Lst == "EX") {
        $('#li3ex').addClass('active');
        TripalEX = true;
    }
    else {
        $('#li3ex').removeClass('active');
        TripalEX = false;
    }
    if (cut_Lst == "EX,VG" && polish_Lst == "EX,VG" && sym_Lst == "EX,VG") {
        $('#li3vg').addClass('active');
        TripalVG = true;
    }
    else {
        $('#li3vg').removeClass('active');
        TripalVG = false;
    }

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BGMLst = _.pluck(_.filter(BgmList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BLACKLst = _.pluck(_.filter(BlackList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var milkyLst = _.pluck(_.filter(LusterList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    var tableopenlst = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crownopenlst = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var pavopenlst = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var girdleopenlst = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    //var KeyToSymLst = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;

    //StockSearch.FromCarat = StockSearch.FromCarat == "" ? 0 : StockSearch.FromCarat == "NaN" ? 0 : StockSearch.FromCarat;
    //StockSearch.ToCarat = StockSearch.ToCarat == "" ? 0 : StockSearch.ToCarat == "NaN" ? 0 : StockSearch.ToCarat;


    var obj = {
        "Pointer": SizeLst,
        "CaratType": CaratType,
        "Shape": shapeLst,
        //"Color": colorLst,
        "Clarity": clarityLst,
        "Cut": cut_Lst,
        "Lab": labLst,
        "Polish": polish_Lst,
        "Location": locationLst,
        "Symm": sym_Lst,
        "BGM": BGMLst,
        "Fls": fluoLst,
        "BLACK": BLACKLst,
        "Inclusion": tblincLst,
        "Natts": tblnattsLst,
        "CrownInclusion": crwincLst,
        "CrownNatts": crwnattsLst,
        "Luster": milkyLst,

        "FromCts": "",
        "ToCts": "",
        "FormPricePerCts": $('#txtPrCtsFrom').val(),
        "ToPricePerCts": $('#txtPrCtsTo').val(),
        "FormDisc": $('#txtDisFrom').val(),
        "ToDisc": $('#txtDisTo').val(),

        "FormRapAmt": "",
        "ToRapAmt": "",

        "FormNetAmt": $('#TotalAmtFrom').val(),
        "ToNetAmt": $('#TotalAmtTo').val(),

        "FromCrownHeight": $('#txtCrHtFrom').val(),
        "ToCrownHeight": $('#txtCrHtTo').val(),

        "FromCrownAngle": $('#txtCrAngFrom').val(),
        "ToCrownAngle": $('#txtCrAngTo').val(),

        "FromPavHeight": $('#txtPavHtFrom').val(),
        "ToPavHeight": $('#txtPavHtTo').val(),

        "FromPavAngle": $('#txtPavAngFrom').val(),
        "ToPavAngle": $('#txtPavAngTo').val(),

        "FormTablePer": $('#txtTablePerFrom').val(),
        "ToTablePer": $('#txtTablePerTo').val(),

        "FormDepthPer": $('#txtDepthPerFrom').val(),
        "ToDepthPer": $('#txtDepthPerTo').val(),

        "FormLength": $('#txtLengthFrom').val(),
        "ToLength": $('#txtLengthTo').val(),

        "FormWidth": $('#txtWidthFrom').val(),
        "ToWidth": $('#txtWidthTo').val(),

        "FormDepth": "",
        "ToDepth": "",

        "StoneID": "",

        "CertiNo": "",
        "ShapeColorPurity": $('#hdnShapeColorPurity').val(),

        "KeyToSymbol": KeyToSymLst_Check + '-' + KeyToSymLst_uncheck,//FinalList_KTS, //KeyToSymLst, //,StockSearch.KeyToSymbol == undefined ? "" : StockSearch.KeyToSymbol,
        "CheckKTS": KeyToSymLst_Check,
        "UNCheckKTS": KeyToSymLst_uncheck,
        "HasImage": $('#SearchImage').hasClass('active') ? true : "",
        "HasHDMovie": $('#SearchVideo').hasClass('active') ? true : "",
        "NewArrival": $('#SearchNewArrival').hasClass('active') ? true : "",
        "Shade": "",
        "ReviseStockFlag": "",
        "IsPromotion": "",
        "PageNo": 1,
        "TokenNo": "",
        "StoneStatus": "",
        "IsTripalEx": TripalEX,
        "IsTripalVg": TripalVG,
        "SmartSearch": SmartSearchdata,
        "FormName": "Stock",
        "ActivityType": "Export Excel",
        "ColorType": Color_Type,
        "Table_Open": tableopenlst,
        "Crown_Open": crownopenlst,
        "Pav_Open": pavopenlst,
        "Girdle_Open": girdleopenlst,
        "UsedFor": "Download"
    };
    if (Color_Type == "Fancy") {
        obj.Intensity = _.pluck(Check_Color_1, 'Symbol').join(",");
        obj.Overtone = _.pluck(Check_Color_2, 'Symbol').join(",");
        obj.Fancy_Color = _.pluck(Check_Color_3, 'Symbol').join(",");
        obj.Color = "";
    }
    else if (Color_Type == "Regular") {
        obj.Color = colorLst;
        obj.Intensity = "";
        obj.Overtone = "";
        obj.Fancy_Color = "";
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/Common/StockExcelDownloadBySearchObject",
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.search('.xlsx') == -1) {
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
function CloseTab(tabid) {

    $('#iresult' + tabid + '').css('display', 'none');
    $('#result' + tabid + '').css('display', 'none');
    $('#result' + tabid + '').removeClass('active');
    $('#profile-tab' + tabid + '').removeClass("active").removeClass("show");
    //$('#tabhome').addClass('active');
    //$('#home-tab2').addClass("active").addClass("show");
    tabno = 0;
    CurrentTab = 0;
    $('#tabhome').tab("show");
}
function BindColumnsSettings() {
    if (_Firstload == false) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
    }
    $.ajax({
        url: "/SearchStock/GetColumnsConfigForSearch",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $.each(data.Data, function (i, item) {
                var headerName = item.Caption;
                var field = item.SPSearchColumn;
                var IsVisible = false;
                var column = {};
                var width = 0;
                if (headerName.length < 6) {
                    width = 50;
                } else if (headerName.length < 10) {
                    width = 60;
                } else if (headerName.length < 13) {
                    width = 90;
                } else {
                    width = 350;
                }

                if (field == 'chk') {
                    column = {
                        headerName: "", field: "",
                        headerCheckboxSelection: true,
                        checkboxSelection: true, width: 28,
                        suppressSorting: true,
                        suppressMenu: true,
                        headerCheckboxSelectionFilteredOnly: true,
                        //headerCellRenderer: selectAllRendererDetail,
                        suppressMovable: false
                    };
                }
                else if (field == 'stone_ref_no') {
                    column = {
                        headerName: $("#hdn_Stock_Id_DNA").val(),
                        field: field,
                        hide: IsVisible,
                        width: 95,
                        cellRenderer: 'deltaIndicator',
                        sortable: false,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false
                    };
                }
                else if (field == 'lab') {
                    column = {
                        headerName: $("#hdn_Lab").val(),
                        field: field,
                        hide: IsVisible,
                        width: 40,
                        cellRenderer: 'labIndicator',
                        sortable: false,
                        filter: getValuesAsync1(field),
                        filterParams: {
                            values: getValuesAsync(field),
                            resetButton: true,
                            applyButton: true,
                            comparator: function (a, b) {
                                return 0;
                            }
                        },
                        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId
                    };
                }
                else if (field == 'StoneStatus') {
                    column = {
                        headerName: $("#hdn_Status").val(), //headerName,
                        field: field,
                        hide: IsVisible,
                        width: 50,
                        cellRenderer: 'viewIndicator',
                        sortable: false,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
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
                        filter: false
                    };
                }
                else if (field == 'certi_no') {
                    column = {
                        headerName: $("#hdn_Certi_No").val(),
                        field: field,
                        hide: IsVisible,
                        width: 100,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        //cellClass: function (params) {
                        //    if (params.data != undefined) {
                        //        if ((params.data.Table_Open != null && params.data.Table_Open != 'NN') ||
                        //            (params.data.Crown_Open != null && params.data.Crown_Open != 'NN') ||
                        //            (params.data.Pav_Open != null && params.data.Pav_Open != 'NN') ||
                        //            (params.data.Girdle_Open != null && params.data.Girdle_Open != 'NN')) {
                        //            return 'tcpgopen_bg';
                        //        }
                        //    }
                        //},
                        filter: false
                    };
                }
                else if (field == 'ImagesLink') {
                    column = {
                        headerName: $("#hdn_View_Image").val(),
                        field: field,
                        hide: IsVisible,
                        width: 80,
                        cellRenderer: 'viewIndicator',
                        sortable: false,
                        suppressSorting: true,
                        suppressMenu: true,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId
                    };
                }
                else if (field == 'Table_Open') {
                    column = {
                        headerName: $("#hdn_Table_Open").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 85,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "TO1")
                                        Tooltip = "Minor";
                                    else if (params.value == "TO2")
                                        Tooltip = "Medium";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'Crown_Open') {
                    column = {
                        headerName: $("#hdn_Crown_Open").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 85,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "CO1")
                                        Tooltip = "Minor";
                                    else if (params.value == "CO2")
                                        Tooltip = "Medium";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'Pav_Open') {
                    column = {
                        headerName: $("#hdn_Pav_Open").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 75,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "PO1")
                                        Tooltip = "Minor";
                                    else if (params.value == "PO1")
                                        Tooltip = "Medium";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'Girdle_Open') {
                    column = {
                        headerName: $("#hdn_Girdle_Open").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 85,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "GO1")
                                        Tooltip = "Minor";
                                    else if (params.value == "GO1")
                                        Tooltip = "Medium";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'inclusion') {
                    column = {
                        headerName: $("#hdn_Table_White").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 85,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "T1")
                                        Tooltip = "Minor";
                                    else if (params.value == "T2")
                                        Tooltip = "Medium";
                                    else if (params.value == "T3")
                                        Tooltip = "Heavy";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'table_natts') {
                    column = {
                        headerName: $("#hdn_Table_Black").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 85,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "BT1")
                                        Tooltip = "Minor";
                                    else if (params.value == "BT2")
                                        Tooltip = "Medium";
                                    else if (params.value == "BT3")
                                        Tooltip = "Heavy";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'Crown_Natts') {
                    column = {
                        headerName: $("#hdn_Crown_Natts").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 90,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "BC1")
                                        Tooltip = "Minor";
                                    else if (params.value == "BC2")
                                        Tooltip = "Medium";
                                    else if (params.value == "BC3")
                                        Tooltip = "Heavy";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else if (field == 'Crown_Inclusion') {
                    column = {
                        headerName: $("#hdn_Crown_White").val(),
                        field: field,
                        sortable: true,
                        hide: IsVisible,
                        width: 90,
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
                        filter: false,
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (params.value != null) {
                                    var Tooltip = "";
                                    if (params.value == "-")
                                        Tooltip = "-";
                                    else if (params.value == "NN")
                                        Tooltip = "None";
                                    else if (params.value == "C1")
                                        Tooltip = "Minor";
                                    else if (params.value == "C2")
                                        Tooltip = "Medium";
                                    else if (params.value == "C3")
                                        Tooltip = "Heavy";
                                    else if (params.value == "-")
                                        Tooltip = "Blank";

                                    if (params.value != "")
                                        return "<span class='tags' gloss='" + params.value + ' = ' + Tooltip + "'>" + params.value + "</span>";
                                }
                            }
                        },
                        cellClass: function (params) {
                            return 'tool_ag_cell';
                        },
                    };
                }
                else {
                    if (field == 'shape') {
                        width = 95;
                        headerName = $("#hdn_Shape").val();
                    }
                    //else if (field == 'certi_no') {
                    //    width = 80;
                    //    headerName = $("#hdn_Certi_No").val();
                    //}
                    else if (field == 'BGM') {
                        width = 70;
                        headerName = $("#hdn_BGM").val();
                    }
                    else if (field == 'price_per_cts') {
                        headerName = $("#hdn_Price_Cts").val();
                        width = 75;
                    }
                    else if (field == 'symbol') {
                        headerName = "Key To Symbol";
                    }
                    else if (field == 'sComments') {
                        width = 350;
                        //headerName = "Gia Comment";
                        headerName = "Member Comments";
                    }
                    else if (field == 'Measurement') {
                        headerName = "Measurement";
                    }
                    else if (field == 'Location') {
                        width = 75;
                        headerName = $("#hdn_Location").val();
                    }
                    else if (field == 'pointer') {
                        headerName = $("#hdn_Pointer").val();
                    }
                    else if (field == 'color') {
                        headerName = $("#hdn_Color").val();
                    }
                    else if (field == 'polish') {
                        headerName = $("#hdn_Polish").val();
                    }
                    else if (field == 'fls') {
                        headerName = $("#hdn_Fls").val();
                    }
                    else if (field == 'fls') {
                        headerName = $("#hdn_Fls").val();
                    }
                    else if (field == 'clarity') {
                        headerName = $("#hdn_Clarity").val();
                    }
                    else if (field == 'cts') {
                        headerName = $("#hdn_CTS").val();
                    }
                    else if (field == 'cur_rap_rate') {
                        headerName = $("#hdn_Rap_Price_Doller").val();
                    }
                    else if (field == 'rap_amount') {
                        headerName = $("#hdn_Rap_Amt_Doller").val();
                    }
                    else if (field == 'sales_disc_per') {
                        //headerName = $("#hdn_Disc_Per").val();
                        headerName = $("#hdn_Offer_Disc_Per").val();
                        width = 90;
                    }
                    else if (field == 'net_amount') {
                        //headerName = $("#hdn_Net_Amt").val();
                        headerName = $("#hdn_Offer_Value_Dollar").val();
                        width = 95;
                    }
                    else if (field == 'Final_Disc') {
                        headerName = $("#hdn_Final_Disc_Per").val();
                        width = 95;
                    }
                    else if (field == 'Final_Value') {
                        headerName = $("#hdn_Final_Value").val();
                        width = 95;
                    }
                    else if (field == 'cut') {
                        headerName = $("#hdn_Cut").val();
                    }
                    else if (field == 'symm') {
                        headerName = $("#hdn_Symm").val();
                    }
                    else if (field == 'length') {
                        headerName = $("#hdn_Length").val();
                    }
                    else if (field == 'width') {
                        headerName = $("#hdn_Width").val();
                    }
                    else if (field == 'depth') {
                        headerName = $("#hdn_Depth").val();
                    }
                    else if (field == 'depth_per') {
                        headerName = $("#hdn_Depth_Per").val();
                    }
                    else if (field == 'table_per') {
                        headerName = $("#hdn_Table_Per").val();
                    }
                    else if (field == 'sCulet') {
                        headerName = $("#hdn_Culet").val();
                    }
                    //else if (field == 'table_natts') {
                    //    headerName = $("#hdn_Table_Black").val();
                    //}
                    //else if (field == 'Crown_Natts') {
                    //    headerName = $("#hdn_Crown_Natts").val();
                    //}
                    //else if (field == 'inclusion') {
                    //    headerName = $("#hdn_Table_White").val();
                    //}
                    //else if (field == 'Crown_Inclusion') {
                    //    headerName = $("#hdn_Crown_White").val();
                    //}
                    else if (field == 'crown_angle') {
                        headerName = $("#hdn_Crown_Angle").val();
                    }
                    else if (field == 'crown_height') {
                        headerName = $("#hdn_CR_HT").val();
                    }
                    else if (field == 'pav_angle') {
                        headerName = $("#hdn_Pav_Ang").val();
                    }
                    else if (field == 'pav_height') {
                        headerName = $("#hdn_Pav_HT").val();
                    }
                    else if (field == 'girdle_per') {
                        headerName = $("#hdn_girdle").val() + "(%)";
                        width = 80;
                    }
                    else if (field == 'girdle_type') {
                        headerName = $("#hdn_Girdle_Type").val();
                    }
                    else if (field == 'sInscription') {
                        headerName = $("#hdn_Laser_in_SC").val();
                        width = 110;
                    }
                    //else if (field == 'Table_Open') {
                    //    headerName = $("#hdn_Table_Open").val();
                    //    width = 75;
                    //}
                    //else if (field == 'Crown_Open') {
                    //    headerName = $("#hdn_Crown_Open").val();
                    //    width = 80;
                    //}
                    //else if (field == 'Pav_Open') {
                    //    headerName = $("#hdn_Pav_Open").val();
                    //    width = 70;
                    //}
                    //else if (field == 'Girdle_Open') {
                    //    headerName = $("#hdn_Girdle_Open").val();
                    //    width = 80;
                    //}
                    column = {
                        headerName: headerName,
                        field: field,
                        hide: IsVisible,
                        width: width,
                        sortable: true,
                        tooltip: function (params) {
                            if (field == 'cts') {
                                return parseFloat(params.value).toFixed(2);
                            }
                            else if (field == 'sales_disc_per' || field == 'net_amount' || field == 'Final_Disc' || field == 'Final_Value' || field == 'price_per_cts' || field == 'cur_rap_rate' || field == 'rap_amount' || field == 'length' || field == 'width' || field == 'depth' || field == 'depth_per' || field == 'table_per' || field == 'crown_angle' || field == 'crown_height' || field == 'pav_angle' || field == 'pav_height' || field == 'girdle_per') {
                                return formatNumber(params.value);
                            }
                            else {
                                return params.value;
                            }
                        },
                        cellRenderer: function (params) {
                            if (params.data) {
                                if (field == 'cts') {
                                    return parseFloat(params.value).toFixed(2);
                                }
                                else if (field == 'price_per_cts' || field == 'length' || field == 'width' || field == 'depth' || field == 'depth_per' || field == 'table_per' || field == 'crown_angle' || field == 'crown_height' || field == 'pav_angle' || field == 'pav_height' || field == 'girdle_per') {
                                    return formatNumber(params.value);
                                }
                                else if (field == 'sales_disc_per' || field == 'net_amount' || field == 'Final_Disc' || field == 'Final_Value' || field == 'cur_rap_rate' || field == 'rap_amount') {
                                    //if (params.value != "") {
                                    return formatNumber(params.value);
                                    //}
                                }
                                else if (field == 'cut') {
                                    return (params.value == 'FR' ? 'F' : params.value);
                                }
                                else {
                                    return params.value;
                                }
                            }
                        },
                        cellStyle: function (params) {
                            if (params.data) {
                                if (field == 'sales_disc_per' || field == 'net_amount' || field == 'Final_Disc' || field == 'Final_Value') {
                                    return { 'color': 'red', 'font-weight': 'bold', 'font-size': '11px', 'text-align': 'center' };
                                }
                                else if (field == 'cur_rap_rate' || field == 'cts' || field == 'price_per_cts' || field == 'rap_amount' || field == 'length' || field == 'width' || field == 'depth' || field == 'depth_per' || field == 'table_per' || field == 'crown_angle' || field == 'crown_height' || field == 'pav_angle' || field == 'pav_height' || field == 'girdle_per') {
                                    return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
                                }
                                else if (params.data.cut == '3EX' && (field == 'cut' || field == 'polish' || field == 'symm')) {
                                    return { 'font-weight': 'bold' };
                                }
                                else {
                                    return { 'font-size': '11px', 'text-align': 'center' };
                                }
                            }
                        },
                        filter: getValuesAsync1(field),
                        filterParams: {
                            //values: getCountryValuesAsync(field) _.pluck(ShapeList, 'Value');
                            values: getValuesAsync(field),
                            resetButton: true,
                            applyButton: true,
                            filterOptions: ['inRange'],
                            comparator: function (a, b) {
                                return 0;
                            }
                        },
                        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                        Priority: item.Priority,
                        ColumnName: item.ColumnName,
                        iColumnId: item.iColumnId,
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
                    };


                }
                columnDefs.push(column);
            });
            var Extra_columns = [
                { headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "Cur_Status", field: "Cur_Status", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "Party_Name", field: "Party_Name", cellRenderer: function (params) { return params.value; }, hide: true },
            ];
            for (var i = 0; i < Extra_columns.length; i++) {
                columnDefs.push(Extra_columns[i]);
            }

            if (_Firstload == false) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
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
function deltaIndicator(params) {
    //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.value + '">' + params.value + '</a></div>';
    //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.value + '">' + params.value + '</a></div>';
    //return '<div class="stock-font" style="text-decoration: underline;"><a target="_blank" href="/DNA?StoneNo=' + params.value + '">' + params.value + '</a></div>';
    return '<div><a style="text-decoration: underline; color :blue;" target="_blank" href="/DNA?StoneNo=' + params.value + '">' + params.value + '</a></div>';
}
function viewIndicator(params) {

    return params.value;

}

function statusIndicator(params) {
    var value = '';
    if (params.value == 'AVAILABLE') {
        value = 'A';
    } else if (params.value == 'AVAILABLE OFFER') {
        value = 'O';
    }
    var element = document.createElement("span");
    element.appendChild(document.createTextNode(value));
    return element;
}

function labIndicator(params) {
    //if (params.data.certi_no != "") {
    //    if (params.data != undefined) {
    //        if (params.value == "GIA") {
    //            return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //        }
    //        else if (params.value == "HRD") {
    //            return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
    //        }
    //        else if (params.value == "IGI") {
    //            return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
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

    if (params.data != undefined) {
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
function isFirstColumn(params) {
    var displayedColumns = params.columnApi.getAllDisplayedColumns();
    var thisIsFirstColumn = displayedColumns[1] === params.column;
    return thisIsFirstColumn;
}
function StatusFilter(status, tn) {
    if (tn == 1) {
        obj1.StoneStatus = status;
        gridOptions1.api.setServerSideDatasource(datasource1);
    }
    else if (tn == 2) {
        obj2.StoneStatus = status;
        gridOptions2.api.setServerSideDatasource(datasource2);
    }
    else if (tn == 3) {
        obj3.StoneStatus = status;
        gridOptions3.api.setServerSideDatasource(datasource3);
    }
    else if (tn == 4) {
        obj4.StoneStatus = status;
        gridOptions4.api.setServerSideDatasource(datasource4);
    }
    else if (tn == 5) {
        obj5.StoneStatus = status;
        gridOptions5.api.setServerSideDatasource(datasource5);
    }
}
function setFromCarat() {
    if ($('#txtfromcarat').val() != "") {
        $('#txtfromcarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
        //$('#txttocarat').val((parseFloat($('#txtfromcarat').val()) + 0.10).toFixed(2));
    } else {
        $('#txtfromcarat').val("0");
    }
    if ($('#txttocarat').val() == "") {
        //$('#txttocarat').val("0");
    }
}
function setToCarat() {
    if ($('#txttocarat').val() != "") {
        $('#txttocarat').val(parseFloat($('#txttocarat').val()).toFixed(2));
    } else {
        $('#txttocarat').val("0");
    }
    if ($('#txtfromcarat').val() == "") {
        $('#txtfromcarat').val("0");
    }
}
function resetKeytoSymbol() {
    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
    KTS = 1;
    Key_to_symbolShow();
}
function SetRecentSearchParameter() {
    if ($('#hdnType').val() == 'Recent') {
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        tabno = 1;
        CurrentTab = 1;

        $.ajax({
            url: "/SearchStock/RecentSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                if (data.KeyToSymbol != null && data.KeyToSymbol != '-') {
                    var KeyToSymbol = data.KeyToSymbol;
                    KeyToSymbol = KeyToSymbol.split(',');
                    $(KeyToSymbol).each(function (i, res) {

                        CheckKeyToSymbolList.push({
                            "NewID": KeyToSymbolList.length + 1,
                            "Symbol": res,
                        });
                        $('#searchkeytosymbol input[onclick="GetCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                    });
                    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');

                }
                SmartSearchdata = data.StoneID;
                //var carattype = data.CaratType;
                if (data.Pointer != null) {

                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        _.each(_.filter(CaratList, function (e) { return e.value == item }), function (itm) {
                            itm.ACTIVE = true;
                        });
                    });
                    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true })
                    if (lst.length == 0) {
                        $(pointer).each(function (i, res) {
                            var res1 = res.split('-')
                            var NewID = SizeGroupList.length + 1;
                            SizeGroupList.push({
                                "NewID": NewID,
                                "FromCarat": res1[0],
                                "ToCarat": res1[1],
                                "Size": res,
                            });
                            //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                            $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                        });
                        $('a[href="#carat1"]').click();
                    }
                    else {

                        $(pointer).each(function (i, res) {
                            if (_.find(CaratList, function (num) { return num.Value == res; })) {
                                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                        $('a[href="#carat2"]').click()
                    }

                }
                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    $(shape).each(function (i, res) {

                        if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                            _.findWhere(ShapeList, { Value: res }).ACTIVE = true;
                            $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                //var color = data.Color;
                //if (color != null) {
                //    color = color.split(',');
                //    $(color).each(function (i, res) {

                //        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                //            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                //            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                //        }
                //    });
                //}
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var cut = data.Cut;
                var polish = data.Polish;
                var symm = data.Symm;

                if (cut == "EX" && polish == "EX" && symm == "EX") {
                    $('#li3ex').addClass('active');
                }
                else {
                    $('#li3ex').removeClass('active');
                }
                if (cut == "EX,VG" && polish == "EX,VG" && symm == "EX,VG") {
                    $('#li3vg').addClass('active');
                }
                else {
                    $('#li3vg').removeClass('active');
                }


                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).ACTIVE = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (polish != null) {
                    polish = polish.split(',');
                    $(polish).each(function (i, res) {

                        if (_.find(PolishList, function (num) { return num.Value == res; })) {
                            _.findWhere(PolishList, { Value: res }).ACTIVE = true;
                            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (symm != null) {
                    symm = symm.split(',');
                    $(symm).each(function (i, res) {

                        if (_.find(SymList, function (num) { return num.Value == res; })) {
                            _.findWhere(SymList, { Value: res }).ACTIVE = true;
                            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var fls = data.Fls;
                if (fls != null) {
                    fls = fls.split(',');
                    $(fls).each(function (i, res) {

                        if (_.find(FlouList, function (num) { return num.Value == res; })) {
                            _.findWhere(FlouList, { Value: res }).ACTIVE = true;
                            $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var lab = data.Lab;
                if (lab != null) {
                    lab = lab.split(',');
                    $(lab).each(function (i, res) {

                        if (_.find(LabList, function (num) { return num.Value == res; })) {
                            _.findWhere(LabList, { Value: res }).ACTIVE = true;
                            $('#searchlab li[onclick="SetActive(\'LAB\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var inclusion = data.Inclusion;
                if (inclusion != null) {
                    inclusion = inclusion.split(',');
                    $(inclusion).each(function (i, res) {

                        if (_.find(TblInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblInclList, { Value: res }).ACTIVE = true;
                            $('#searchtableincl li[onclick="SetActive(\'TABLE_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var natts = data.Natts;
                if (natts != null) {
                    natts = natts.split(',');
                    $(natts).each(function (i, res) {

                        if (_.find(TblNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblNattsList, { Value: res }).ACTIVE = true;
                            $('#searchtablenatts li[onclick="SetActive(\'TABLE_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crowninclusion = data.CrownInclusion;
                if (crowninclusion != null) {
                    crowninclusion = crowninclusion.split(',');
                    $(crowninclusion).each(function (i, res) {

                        if (_.find(CrwnInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnInclList, { Value: res }).ACTIVE = true;
                            $('#searchcrownincl li[onclick="SetActive(\'CROWN_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crownnatts = data.CrownNatts;
                if (crownnatts != null) {
                    crownnatts = crownnatts.split(',');
                    $(crownnatts).each(function (i, res) {

                        if (_.find(CrwnNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnNattsList, { Value: res }).ACTIVE = true;
                            $('#searchcrownnatts li[onclick="SetActive(\'CROWN_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var location = data.Location;
                if (location != null) {
                    location = location.split(',');
                    $(location).each(function (i, res) {

                        if (_.find(LocationList, function (num) { return num.Value == res; })) {
                            _.findWhere(LocationList, { Value: res }).ACTIVE = true;
                            $('#searchlocation li[onclick="SetActive(\'Location\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var bgm = data.BGM;
                if (bgm != null) {
                    bgm = bgm.split(',');
                    $(bgm).each(function (i, res) {

                        if (_.find(BgmList, function (num) { return num.Value == res; })) {
                            _.findWhere(BgmList, { Value: res }).ACTIVE = true;
                            $('#searchbgm li[onclick="SetActive(\'BGM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _tableopenlist = data.Table_Open;
                if (_tableopenlist != null) {
                    _tableopenlist = _tableopenlist.split(',');
                    $(_tableopenlist).each(function (i, res) {
                        if (_.find(TableOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(TableOpenList, { Value: res }).ACTIVE = true;
                            $('#searchtableopen li[onclick="SetActive(\'TABLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _crownopenlist = data.Crown_Open;
                if (_crownopenlist != null) {
                    _crownopenlist = _crownopenlist.split(',');
                    $(_crownopenlist).each(function (i, res) {
                        if (_.find(CrownOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrownOpenList, { Value: res }).ACTIVE = true;
                            $('#searchcrownopen li[onclick="SetActive(\'CROWNOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _pavopenlist = data.Pav_Open;
                if (_pavopenlist != null) {
                    _pavopenlist = _pavopenlist.split(',');
                    $(_pavopenlist).each(function (i, res) {
                        if (_.find(PavOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(PavOpenList, { Value: res }).ACTIVE = true;
                            $('#searchpavopen li[onclick="SetActive(\'PAVILIONOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _girdleopenlist = data.Girdle_Open;
                if (_girdleopenlist != null) {
                    _girdleopenlist = _girdleopenlist.split(',');
                    $(_girdleopenlist).each(function (i, res) {
                        if (_.find(GirdleOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(GirdleOpenList, { Value: res }).ACTIVE = true;
                            $('#searchgirdleopen li[onclick="SetActive(\'GIRDLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                $('#txtDisFrom').val(data.FormDisc);
                $('#txtDisTo').val(data.ToDisc);
                $('#txtPrCtsFrom').val(data.FormPricePerCts);
                $('#txtPrCtsTo').val(data.ToPricePerCts);
                $('#TotalAmtFrom').val(data.FormNetAmt);
                $('#TotalAmtTo').val(data.ToNetAmt);
                $('#txtLengthFrom').val(data.FormLength);
                $('#txtLengthTo').val(data.ToLength);
                $('#txtWidthFrom').val(data.FormWidth);
                $('#txtWidthTo').val(data.ToWidth);
                $('#txtDepthPerFrom').val(data.FormDepthPer);
                $('#txtDepthPerTo').val(data.ToDepthPer);
                $('#txtTablePerFrom').val(data.FormTablePer);
                $('#txtTablePerTo').val(data.ToTablePer);
                $('#txtCrAngFrom').val(data.FromCrownAngle);
                $('#txtCrAngTo').val(data.ToCrownAngle);
                $('#txtCrHtFrom').val(data.FromCrownHeight);
                $('#txtCrHtTo').val(data.ToCrownHeight);
                $('#txtPavAngFrom').val(data.FromPavAngle);
                $('#txtPavAngTo').val(data.ToPavAngle);
                $('#txtPavHtFrom').val(data.FromPavHeight);
                $('#txtPavHtTo').val(data.ToPavHeight);

                if (data.ColorType == "Regular") {
                    Color_Hide_Show('1');
                    var color = data.Color;
                    if (color != null) {
                        color = color.split(',');
                        $(color).each(function (i, res) {

                            if (_.find(ColorList, function (num) { return num.Value == res; })) {
                                _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                                $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                    }
                }
                else if (data.ColorType == "Fancy") {
                    Color_Hide_Show('2');
                    $(".carat-dropdown-main").hide();
                    C1 = 0, C2 = 0, C3 = 0;
                    if (data.Intensity != null) {
                        var i = data.Intensity;
                        var _INTENSITY = i.split(',');
                        if (_INTENSITY.length == INTENSITY.length - 1) {
                            $("#CHK_I_0").prop('checked', true);
                            GetCheck_INTENSITY_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _INTENSITY.length - 1; _j++) {
                                for (var j = 0; j <= INTENSITY.length - 1; j++) {
                                    if (_INTENSITY[_j] == INTENSITY[j]) {
                                        $("#CHK_I_" + j).prop('checked', true);
                                        GetCheck_INTENSITY_List(INTENSITY[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Overtone != null) {
                        var o = data.Overtone;
                        var _OVERTONE = o.split(',');
                        if (_OVERTONE.length == OVERTONE.length - 1) {
                            $("#CHK_O_0").prop('checked', true);
                            GetCheck_OVERTONE_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _OVERTONE.length - 1; _j++) {
                                for (var j = 0; j <= OVERTONE.length - 1; j++) {
                                    if (_OVERTONE[_j] == OVERTONE[j]) {
                                        $("#CHK_O_" + j).prop('checked', true);
                                        GetCheck_OVERTONE_List(OVERTONE[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Fancy_Color != null) {
                        var fc = data.Fancy_Color;
                        var _FANCY_COLOR = fc.split(',');
                        if (_FANCY_COLOR.length == FANCY_COLOR.length - 1) {
                            $("#CHK_F_0").prop('checked', true);
                            GetCheck_FANCY_COLOR_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _FANCY_COLOR.length - 1; _j++) {
                                for (var j = 0; j <= FANCY_COLOR.length - 1; j++) {
                                    if (_FANCY_COLOR[_j] == FANCY_COLOR[j]) {
                                        $("#CHK_F_" + j).prop('checked', true);
                                        GetCheck_FANCY_COLOR_List(FANCY_COLOR[j], j);
                                    }
                                }
                            }
                        }
                    }
                }
                if ($('#hdnSet').val() == '') {
                    SetSearchParameter();
                }
                else {
                    if (_Firstload == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
        contentHeight();
    }
}
function SetSavedSearchParameter() {
    if ($('#hdnType').val() == 'SaveSearch') {
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        tabno = 1;
        CurrentTab = 1;
        $.ajax({
            url: "/SearchStock/SavedSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                $('#hdnSavedSearchId').val(data.SearchID);
                $('#txtSearchName').val(data.SearchName);
                if (data.KeyToSymbol != null && data.KeyToSymbol != '-') {
                    var KeyToSymbol = data.KeyToSymbol;
                    KeyToSymbol = KeyToSymbol.split(',');
                    $(KeyToSymbol).each(function (i, res) {

                        CheckKeyToSymbolList.push({
                            "NewID": KeyToSymbolList.length + 1,
                            "Symbol": res,
                        });
                        $('#searchkeytosymbol input[onclick="GetCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                    });
                    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');

                }
                SmartSearchdata = data.StoneID;
                //var carattype = data.CaratType;
                if (data.Pointer != null) {

                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        _.each(_.filter(CaratList, function (e) { return e.value == item }), function (itm) {
                            itm.ACTIVE = true;
                        });
                    });
                    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true })
                    if (lst.length == 0) {
                        $(pointer).each(function (i, res) {
                            var res1 = res.split('-')
                            var NewID = SizeGroupList.length + 1;
                            SizeGroupList.push({
                                "NewID": NewID,
                                "FromCarat": res1[0],
                                "ToCarat": res1[1],
                                "Size": res,
                            });
                            //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                            $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                        });
                        $('a[href="#carat1"]').click();
                    }
                    else {

                        $(pointer).each(function (i, res) {
                            if (_.find(CaratList, function (num) { return num.Value == res; })) {
                                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                        $('a[href="#carat2"]').click()
                    }

                }
                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    $(shape).each(function (i, res) {

                        if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                            _.findWhere(ShapeList, { Value: res }).ACTIVE = true;
                            $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                //var color = data.Color;
                //if (color != null) {
                //    color = color.split(',');
                //    $(color).each(function (i, res) {

                //        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                //            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                //            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                //        }
                //    });
                //}
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var cut = data.Cut;
                var polish = data.Polish;
                var symm = data.Symm;

                if (cut == "EX" && polish == "EX" && symm == "EX") {
                    $('#li3ex').addClass('active');
                }
                else {
                    $('#li3ex').removeClass('active');
                }
                if (cut == "EX,VG" && polish == "EX,VG" && symm == "EX,VG") {
                    $('#li3vg').addClass('active');
                }
                else {
                    $('#li3vg').removeClass('active');
                }

                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).ACTIVE = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (polish != null) {
                    polish = polish.split(',');
                    $(polish).each(function (i, res) {

                        if (_.find(PolishList, function (num) { return num.Value == res; })) {
                            _.findWhere(PolishList, { Value: res }).ACTIVE = true;
                            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (symm != null) {
                    symm = symm.split(',');
                    $(symm).each(function (i, res) {

                        if (_.find(SymList, function (num) { return num.Value == res; })) {
                            _.findWhere(SymList, { Value: res }).ACTIVE = true;
                            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var fls = data.Fls;
                if (fls != null) {
                    fls = fls.split(',');
                    $(fls).each(function (i, res) {

                        if (_.find(FlouList, function (num) { return num.Value == res; })) {
                            _.findWhere(FlouList, { Value: res }).ACTIVE = true;
                            $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var lab = data.Lab;
                if (lab != null) {
                    lab = lab.split(',');
                    $(lab).each(function (i, res) {

                        if (_.find(LabList, function (num) { return num.Value == res; })) {
                            _.findWhere(LabList, { Value: res }).ACTIVE = true;
                            $('#searchlab li[onclick="SetActive(\'LAB\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var inclusion = data.Inclusion;
                if (inclusion != null) {
                    inclusion = inclusion.split(',');
                    $(inclusion).each(function (i, res) {

                        if (_.find(TblInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblInclList, { Value: res }).ACTIVE = true;
                            $('#searchtableincl li[onclick="SetActive(\'TABLE_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var natts = data.Natts;
                if (natts != null) {
                    natts = natts.split(',');
                    $(natts).each(function (i, res) {

                        if (_.find(TblNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblNattsList, { Value: res }).ACTIVE = true;
                            $('#searchtablenatts li[onclick="SetActive(\'TABLE_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crowninclusion = data.CrownInclusion;
                if (crowninclusion != null) {
                    crowninclusion = crowninclusion.split(',');
                    $(crowninclusion).each(function (i, res) {

                        if (_.find(CrwnInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnInclList, { Value: res }).ACTIVE = true;
                            $('#searchcrownincl li[onclick="SetActive(\'CROWN_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crownnatts = data.CrownNatts;
                if (crownnatts != null) {
                    crownnatts = crownnatts.split(',');
                    $(crownnatts).each(function (i, res) {

                        if (_.find(CrwnNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnNattsList, { Value: res }).ACTIVE = true;
                            $('#searchcrownnatts li[onclick="SetActive(\'CROWN_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var location = data.Location;
                if (location != null) {
                    location = location.split(',');
                    $(location).each(function (i, res) {

                        if (_.find(LocationList, function (num) { return num.Value == res; })) {
                            _.findWhere(LocationList, { Value: res }).ACTIVE = true;
                            $('#searchlocation li[onclick="SetActive(\'Location\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var bgm = data.BGM;
                if (bgm != null) {
                    bgm = bgm.split(',');
                    $(bgm).each(function (i, res) {

                        if (_.find(BgmList, function (num) { return num.Value == res; })) {
                            _.findWhere(BgmList, { Value: res }).ACTIVE = true;
                            $('#searchbgm li[onclick="SetActive(\'BGM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _tableopenlist = data.Table_Open;
                if (_tableopenlist != null) {
                    _tableopenlist = _tableopenlist.split(',');
                    $(_tableopenlist).each(function (i, res) {
                        if (_.find(TableOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(TableOpenList, { Value: res }).ACTIVE = true;
                            $('#searchtableopen li[onclick="SetActive(\'TABLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _crownopenlist = data.Crown_Open;
                if (_crownopenlist != null) {
                    _crownopenlist = _crownopenlist.split(',');
                    $(_crownopenlist).each(function (i, res) {
                        if (_.find(CrownOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrownOpenList, { Value: res }).ACTIVE = true;
                            $('#searchcrownopen li[onclick="SetActive(\'CROWNOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _pavopenlist = data.Pav_Open;
                if (_pavopenlist != null) {
                    _pavopenlist = _pavopenlist.split(',');
                    $(_pavopenlist).each(function (i, res) {
                        if (_.find(PavOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(PavOpenList, { Value: res }).ACTIVE = true;
                            $('#searchpavopen li[onclick="SetActive(\'PAVILIONOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _girdleopenlist = data.Girdle_Open;
                if (_girdleopenlist != null) {
                    _girdleopenlist = _girdleopenlist.split(',');
                    $(_girdleopenlist).each(function (i, res) {
                        if (_.find(GirdleOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(GirdleOpenList, { Value: res }).ACTIVE = true;
                            $('#searchgirdleopen li[onclick="SetActive(\'GIRDLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                $('#txtDisFrom').val(data.FormDisc);
                $('#txtDisTo').val(data.ToDisc);
                $('#txtPrCtsFrom').val(data.FormPricePerCts);
                $('#txtPrCtsTo').val(data.ToPricePerCts);
                $('#TotalAmtFrom').val(data.FormNetAmt);
                $('#TotalAmtTo').val(data.ToNetAmt);
                $('#txtLengthFrom').val(data.FormLength);
                $('#txtLengthTo').val(data.ToLength);
                $('#txtWidthFrom').val(data.FormWidth);
                $('#txtWidthTo').val(data.ToWidth);
                $('#txtDepthPerFrom').val(data.FormDepthPer);
                $('#txtDepthPerTo').val(data.ToDepthPer);
                $('#txtTablePerFrom').val(data.FormTablePer);
                $('#txtTablePerTo').val(data.ToTablePer);
                $('#txtCrAngFrom').val(data.FromCrownAngle);
                $('#txtCrAngTo').val(data.ToCrownAngle);
                $('#txtCrHtFrom').val(data.FromCrownHeight);
                $('#txtCrHtTo').val(data.ToCrownHeight);
                $('#txtPavAngFrom').val(data.FromPavAngle);
                $('#txtPavAngTo').val(data.ToPavAngle);
                $('#txtPavHtFrom').val(data.FromPavHeight);
                $('#txtPavHtTo').val(data.ToPavHeight);

                if (data.ColorType == "Regular") {
                    Color_Hide_Show('1');
                    var color = data.Color;
                    if (color != null) {
                        color = color.split(',');
                        $(color).each(function (i, res) {

                            if (_.find(ColorList, function (num) { return num.Value == res; })) {
                                _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                                $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                    }
                }
                else if (data.ColorType == "Fancy") {
                    Color_Hide_Show('2');
                    $(".carat-dropdown-main").hide();
                    C1 = 0, C2 = 0, C3 = 0;
                    if (data.Intensity != null) {
                        var i = data.Intensity;
                        var _INTENSITY = i.split(',');
                        if (_INTENSITY.length == INTENSITY.length - 1) {
                            $("#CHK_I_0").prop('checked', true);
                            GetCheck_INTENSITY_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _INTENSITY.length - 1; _j++) {
                                for (var j = 0; j <= INTENSITY.length - 1; j++) {
                                    if (_INTENSITY[_j] == INTENSITY[j]) {
                                        $("#CHK_I_" + j).prop('checked', true);
                                        GetCheck_INTENSITY_List(INTENSITY[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Overtone != null) {
                        var o = data.Overtone;
                        var _OVERTONE = o.split(',');
                        if (_OVERTONE.length == OVERTONE.length - 1) {
                            $("#CHK_O_0").prop('checked', true);
                            GetCheck_OVERTONE_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _OVERTONE.length - 1; _j++) {
                                for (var j = 0; j <= OVERTONE.length - 1; j++) {
                                    if (_OVERTONE[_j] == OVERTONE[j]) {
                                        $("#CHK_O_" + j).prop('checked', true);
                                        GetCheck_OVERTONE_List(OVERTONE[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Fancy_Color != null) {
                        var fc = data.Fancy_Color;
                        var _FANCY_COLOR = fc.split(',');
                        if (_FANCY_COLOR.length == FANCY_COLOR.length - 1) {
                            $("#CHK_F_0").prop('checked', true);
                            GetCheck_FANCY_COLOR_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _FANCY_COLOR.length - 1; _j++) {
                                for (var j = 0; j <= FANCY_COLOR.length - 1; j++) {
                                    if (_FANCY_COLOR[_j] == FANCY_COLOR[j]) {
                                        $("#CHK_F_" + j).prop('checked', true);
                                        GetCheck_FANCY_COLOR_List(FANCY_COLOR[j], j);
                                    }
                                }
                            }
                        }
                    }
                }

                if ($('#hdnSet').val() == '') {
                    SetSearchParameter();
                }
                else {
                    if (_Firstload == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
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
function SetChartSearchParameter() {debugger
    if ($('#hdnType').val() == 'ChartSearch') {
        debugger
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        tabno = 1;
        CurrentTab = 1;
        $.ajax({
            url: "/SearchStock/ChartSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                debugger
                
                if (data.Pointer != null) {
                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        _.each(_.filter(CaratList, function (e) { return e.value == item }), function (itm) {
                            itm.ACTIVE = true;
                        });
                    });
                    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true })
                    if (lst.length == 0) {
                        $(pointer).each(function (i, res) {
                            var res1 = res.split('-')
                            var NewID = SizeGroupList.length + 1;
                            SizeGroupList.push({
                                "NewID": NewID,
                                "FromCarat": res1[0],
                                "ToCarat": res1[1],
                                "Size": res,
                            });
                            //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                            $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                        });
                        $('a[href="#carat1"]').click();
                    }
                    else {

                        $(pointer).each(function (i, res) {
                            if (_.find(CaratList, function (num) { return num.Value == res; })) {
                                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                        $('a[href="#carat2"]').click()
                    }

                }

                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    $(shape).each(function (i, res) {

                        if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                            _.findWhere(ShapeList, { Value: res }).ACTIVE = true;
                            $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                Color_Hide_Show('1');
                var color = data.Color;
                if (color != null) {
                    color = color.split(',');
                    $(color).each(function (i, res) {

                        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
               
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var cut = data.Cut;
                var polish = data.Polish;
                var symm = data.Symm;

                if (cut == "EX" && polish == "EX" && symm == "EX") {
                    $('#li3ex').addClass('active');
                }
                else {
                    $('#li3ex').removeClass('active');
                }
                if (cut == "EX,VG" && polish == "EX,VG" && symm == "EX,VG") {
                    $('#li3vg').addClass('active');
                }
                else {
                    $('#li3vg').removeClass('active');
                }

                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).ACTIVE = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (polish != null) {
                    polish = polish.split(',');
                    $(polish).each(function (i, res) {

                        if (_.find(PolishList, function (num) { return num.Value == res; })) {
                            _.findWhere(PolishList, { Value: res }).ACTIVE = true;
                            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (symm != null) {
                    symm = symm.split(',');
                    $(symm).each(function (i, res) {

                        if (_.find(SymList, function (num) { return num.Value == res; })) {
                            _.findWhere(SymList, { Value: res }).ACTIVE = true;
                            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                

                if ($('#hdnSet').val() == '') {
                    SetSearchParameter();
                }
                else {
                    if (_Firstload == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
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

function SetQuickSearchParameter() {
    if ($('#hdnType').val() == 'QuickSearch') {
        $("#tabhome").hide();
        $("#home-tab2").hide();
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        tabno = 1;
        CurrentTab = 1;
        $.ajax({
            url: "/SearchStock/QuickSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                $('#hdnShapeColorPurity').val(data.ShapeColorPurity);
                if (data.Pointer != null) {
                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        if (_.find(CaratList, function (num) { return num.Value == item; })) {
                            _.findWhere(CaratList, { Value: item }).ACTIVE = true;
                            $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + item + '\')"]').addClass('active');
                        }
                    });
                    $('a[href="#carat2"]').click();
                }
                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    if (shape.length == 1) {
                        $(shape).each(function (i, res) {
                            if (res == 0) {
                                if (_.find(ShapeList, function (num) { return num.Value == 'ROUND'; })) {
                                    _.findWhere(ShapeList, { Value: 'ROUND' }).ACTIVE = true;
                                    $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                                }
                            } else {
                                $.map(ShapeList, function (obj) {
                                    if (obj.Value != 'ROUND') {
                                        obj.ACTIVE = true;
                                        $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + obj.Value + '\')"]').addClass('active');
                                    }
                                });
                            }
                        });
                    }
                }
                var color = data.Color;
                if (color != null) {
                    color = color.split(',');
                    $(color).each(function (i, res) {

                        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var fls = data.Fls;
                if (fls != null) {
                    fls = fls.split(',');
                    $(fls).each(function (i, res) {

                        if (_.find(FlouList, function (num) { return num.Value == res; })) {
                            _.findWhere(FlouList, { Value: res }).ACTIVE = true;
                            $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var cut = data.Cut;

                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).ACTIVE = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                SetSearchParameter();

                //$('.loading-overlay-image-container').hide();
                //$('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
}
function SetSmartSearchParameter() {
    if ($('#hdnType').val() == 'SmartSearch') {
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        $("#tabhome").parent().hide();
        $("#home-tab2").hide();

        tabno = 1;
        CurrentTab = 1;
        //setTimeout(function () {
        $.ajax({
            url: "/SearchStock/StoreSmartSearchGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                SmartSearchdata = data.SmartSearch;
                SetSearchParameter();
                //$('.loading-overlay-image-container').hide();
                //$('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
        //}, 2000);
        contentHeight();
    }
}
function SetModifySavedSearchParameter() {
    if ($('#hdnType').val() == 'Modify') {
        if (_Firstload == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        tabno = 1;
        CurrentTab = 1;
        $.ajax({
            url: "/SearchStock/ModifySavedSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                $('#hdnSavedSearchId').val(data.SearchID);
                $('#txtSearchName').val(data.SearchName);
                if (data.KeyToSymbol != null && data.KeyToSymbol != '-') {
                    var KeyToSymbol = data.KeyToSymbol;
                    KeyToSymbol = KeyToSymbol.split(',');
                    $(KeyToSymbol).each(function (i, res) {

                        CheckKeyToSymbolList.push({
                            "NewID": KeyToSymbolList.length + 1,
                            "Symbol": res,
                        });
                        $('#searchkeytosymbol input[onclick="GetCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                    });
                    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');

                }
                SmartSearchdata = data.StoneID;
                //var carattype = data.CaratType;
                if (data.Pointer != null) {

                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        _.each(_.filter(CaratList, function (e) { return e.value == item }), function (itm) {
                            itm.ACTIVE = true;
                        });
                    });
                    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true })
                    if (lst.length == 0) {
                        $(pointer).each(function (i, res) {
                            var res1 = res.split('-')
                            var NewID = SizeGroupList.length + 1;
                            SizeGroupList.push({
                                "NewID": NewID,
                                "FromCarat": res1[0],
                                "ToCarat": res1[1],
                                "Size": res,
                            });
                            //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                            $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                        });
                        $('a[href="#carat1"]').click();
                    }
                    else {

                        $(pointer).each(function (i, res) {
                            if (_.find(CaratList, function (num) { return num.Value == res; })) {
                                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                        $('a[href="#carat2"]').click()
                    }

                }
                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    $(shape).each(function (i, res) {

                        if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                            _.findWhere(ShapeList, { Value: res }).ACTIVE = true;
                            $('#searchshape li a[onclick="SetActive(\'Shape\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                //var color = data.Color;
                //if (color != null) {
                //    color = color.split(',');
                //    $(color).each(function (i, res) {

                //        if (_.find(ColorList, function (num) { return num.Value == res; })) {
                //            _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                //            $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                //        }
                //    });
                //}
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).ACTIVE = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var cut = data.Cut;
                var polish = data.Polish;
                var symm = data.Symm;

                if (cut == "EX" && polish == "EX" && symm == "EX") {
                    $('#li3ex').addClass('active');
                }
                else {
                    $('#li3ex').removeClass('active');
                }
                if (cut == "EX,VG" && polish == "EX,VG" && symm == "EX,VG") {
                    $('#li3vg').addClass('active');
                }
                else {
                    $('#li3vg').removeClass('active');
                }

                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).ACTIVE = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (polish != null) {
                    polish = polish.split(',');
                    $(polish).each(function (i, res) {

                        if (_.find(PolishList, function (num) { return num.Value == res; })) {
                            _.findWhere(PolishList, { Value: res }).ACTIVE = true;
                            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                if (symm != null) {
                    symm = symm.split(',');
                    $(symm).each(function (i, res) {

                        if (_.find(SymList, function (num) { return num.Value == res; })) {
                            _.findWhere(SymList, { Value: res }).ACTIVE = true;
                            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var fls = data.Fls;
                if (fls != null) {
                    fls = fls.split(',');
                    $(fls).each(function (i, res) {

                        if (_.find(FlouList, function (num) { return num.Value == res; })) {
                            _.findWhere(FlouList, { Value: res }).ACTIVE = true;
                            $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var lab = data.Lab;
                if (lab != null) {
                    lab = lab.split(',');
                    $(lab).each(function (i, res) {

                        if (_.find(LabList, function (num) { return num.Value == res; })) {
                            _.findWhere(LabList, { Value: res }).ACTIVE = true;
                            $('#searchlab li[onclick="SetActive(\'LAB\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var inclusion = data.Inclusion;
                if (inclusion != null) {
                    inclusion = inclusion.split(',');
                    $(inclusion).each(function (i, res) {

                        if (_.find(TblInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblInclList, { Value: res }).ACTIVE = true;
                            $('#searchtableincl li[onclick="SetActive(\'TABLE_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var natts = data.Natts;
                if (natts != null) {
                    natts = natts.split(',');
                    $(natts).each(function (i, res) {

                        if (_.find(TblNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblNattsList, { Value: res }).ACTIVE = true;
                            $('#searchtablenatts li[onclick="SetActive(\'TABLE_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crowninclusion = data.CrownInclusion;
                if (crowninclusion != null) {
                    crowninclusion = crowninclusion.split(',');
                    $(crowninclusion).each(function (i, res) {

                        if (_.find(CrwnInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnInclList, { Value: res }).ACTIVE = true;
                            $('#searchcrownincl li[onclick="SetActive(\'CROWN_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crownnatts = data.CrownNatts;
                if (crownnatts != null) {
                    crownnatts = crownnatts.split(',');
                    $(crownnatts).each(function (i, res) {

                        if (_.find(CrwnNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnNattsList, { Value: res }).ACTIVE = true;
                            $('#searchcrownnatts li[onclick="SetActive(\'CROWN_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var location = data.Location;
                if (location != null) {
                    location = location.split(',');
                    $(location).each(function (i, res) {

                        if (_.find(LocationList, function (num) { return num.Value == res; })) {
                            _.findWhere(LocationList, { Value: res }).ACTIVE = true;
                            $('#searchlocation li[onclick="SetActive(\'Location\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var bgm = data.BGM;
                if (bgm != null) {
                    bgm = bgm.split(',');
                    $(bgm).each(function (i, res) {

                        if (_.find(BgmList, function (num) { return num.Value == res; })) {
                            _.findWhere(BgmList, { Value: res }).ACTIVE = true;
                            $('#searchbgm li[onclick="SetActive(\'BGM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _tableopenlist = data.Table_Open;
                if (_tableopenlist != null) {
                    _tableopenlist = _tableopenlist.split(',');
                    $(_tableopenlist).each(function (i, res) {
                        if (_.find(TableOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(TableOpenList, { Value: res }).ACTIVE = true;
                            $('#searchtableopen li[onclick="SetActive(\'TABLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _crownopenlist = data.Crown_Open;
                if (_crownopenlist != null) {
                    _crownopenlist = _crownopenlist.split(',');
                    $(_crownopenlist).each(function (i, res) {
                        if (_.find(CrownOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrownOpenList, { Value: res }).ACTIVE = true;
                            $('#searchcrownopen li[onclick="SetActive(\'CROWNOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _pavopenlist = data.Pav_Open;
                if (_pavopenlist != null) {
                    _pavopenlist = _pavopenlist.split(',');
                    $(_pavopenlist).each(function (i, res) {
                        if (_.find(PavOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(PavOpenList, { Value: res }).ACTIVE = true;
                            $('#searchpavopen li[onclick="SetActive(\'PAVILIONOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _girdleopenlist = data.Girdle_Open;
                if (_girdleopenlist != null) {
                    _girdleopenlist = _girdleopenlist.split(',');
                    $(_girdleopenlist).each(function (i, res) {
                        if (_.find(GirdleOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(GirdleOpenList, { Value: res }).ACTIVE = true;
                            $('#searchgirdleopen li[onclick="SetActive(\'GIRDLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                $('#txtDisFrom').val(data.FormDisc);
                $('#txtDisTo').val(data.ToDisc);
                $('#txtPrCtsFrom').val(data.FormPricePerCts);
                $('#txtPrCtsTo').val(data.ToPricePerCts);
                $('#TotalAmtFrom').val(data.FormNetAmt);
                $('#TotalAmtTo').val(data.ToNetAmt);
                $('#txtLengthFrom').val(data.FormLength);
                $('#txtLengthTo').val(data.ToLength);
                $('#txtWidthFrom').val(data.FormWidth);
                $('#txtWidthTo').val(data.ToWidth);
                $('#txtDepthPerFrom').val(data.FormDepthPer);
                $('#txtDepthPerTo').val(data.ToDepthPer);
                $('#txtTablePerFrom').val(data.FormTablePer);
                $('#txtTablePerTo').val(data.ToTablePer);
                $('#txtCrAngFrom').val(data.FromCrownAngle);
                $('#txtCrAngTo').val(data.ToCrownAngle);
                $('#txtCrHtFrom').val(data.FromCrownHeight);
                $('#txtCrHtTo').val(data.ToCrownHeight);
                $('#txtPavAngFrom').val(data.FromPavAngle);
                $('#txtPavAngTo').val(data.ToPavAngle);
                $('#txtPavHtFrom').val(data.FromPavHeight);
                $('#txtPavHtTo').val(data.ToPavHeight);

                if (data.ColorType == "Regular") {
                    Color_Hide_Show('1');
                    var color = data.Color;
                    if (color != null) {
                        color = color.split(',');
                        $(color).each(function (i, res) {

                            if (_.find(ColorList, function (num) { return num.Value == res; })) {
                                _.findWhere(ColorList, { Value: res }).ACTIVE = true;
                                $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                    }
                }
                else if (data.ColorType == "Fancy") {
                    Color_Hide_Show('2');
                    $(".carat-dropdown-main").hide();
                    C1 = 0, C2 = 0, C3 = 0;
                    if (data.Intensity != null) {
                        var i = data.Intensity;
                        var _INTENSITY = i.split(',');
                        if (_INTENSITY.length == INTENSITY.length - 1) {
                            $("#CHK_I_0").prop('checked', true);
                            GetCheck_INTENSITY_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _INTENSITY.length - 1; _j++) {
                                for (var j = 0; j <= INTENSITY.length - 1; j++) {
                                    if (_INTENSITY[_j] == INTENSITY[j]) {
                                        $("#CHK_I_" + j).prop('checked', true);
                                        GetCheck_INTENSITY_List(INTENSITY[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Overtone != null) {
                        var o = data.Overtone;
                        var _OVERTONE = o.split(',');
                        if (_OVERTONE.length == OVERTONE.length - 1) {
                            $("#CHK_O_0").prop('checked', true);
                            GetCheck_OVERTONE_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _OVERTONE.length - 1; _j++) {
                                for (var j = 0; j <= OVERTONE.length - 1; j++) {
                                    if (_OVERTONE[_j] == OVERTONE[j]) {
                                        $("#CHK_O_" + j).prop('checked', true);
                                        GetCheck_OVERTONE_List(OVERTONE[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.Fancy_Color != null) {
                        var fc = data.Fancy_Color;
                        var _FANCY_COLOR = fc.split(',');
                        if (_FANCY_COLOR.length == FANCY_COLOR.length - 1) {
                            $("#CHK_F_0").prop('checked', true);
                            GetCheck_FANCY_COLOR_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _FANCY_COLOR.length - 1; _j++) {
                                for (var j = 0; j <= FANCY_COLOR.length - 1; j++) {
                                    if (_FANCY_COLOR[_j] == FANCY_COLOR[j]) {
                                        $("#CHK_F_" + j).prop('checked', true);
                                        GetCheck_FANCY_COLOR_List(FANCY_COLOR[j], j);
                                    }
                                }
                            }
                        }
                    }
                }
                $("#liNew_Search").show();
                $("#home-tab2").show();

                if (_Firstload == false) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
}
/*-------------------------------------------------------- GET SEARCH AND AGGRID BINDING START------------------------------------------------*/
function onSelectionChanged(params) {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var CUR_RAP_RATE = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;
    if (CurrentTab == 1) {
        if (gridOptions1.api.getSelectedRows().length > 0) {
            //gridOptions1_SelectedRows = gridOptions1.api.getSelectedRows();
            dDisc = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = gridOptions1.api.getSelectedRows().length;

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
            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }

            //for (var i = 0; i < gridOptions1_SelectedRows.length; i++) {
            //    dDisc = gridOptions1_SelectedRows[0].sales_disc_per;
            //    dRepPrice = gridOptions1_SelectedRows[0].cur_rap_rate;
            //    DCTS = gridOptions1_SelectedRows[0].cts;
            //    dNetPrice = gridOptions1_SelectedRows[0].net_amount;
            //    dRepPrice = parseFloat(dRepPrice) * parseFloat(DCTS);

            //    if (Scheme_Disc_Type == "Discount") {
            //        Final_Disc = parseFloat(dDisc) + parseFloat(Scheme_Disc);
            //        Net_Value = parseFloat(dRepPrice) - (parseFloat(dRepPrice) * parseFloat(Final_Disc) / 100);
            //        Web_Benefit = parseFloat(dNetPrice) - parseFloat(Net_Value);
            //    }
            //    else if (Scheme_Disc_Type == "Value") {
            //        Net_Value = parseFloat(dNetPrice) + (parseFloat(dNetPrice) * parseFloat(Scheme_Disc) / 100);
            //        Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(dRepPrice)) * 100) * -1;
            //        Web_Benefit = parseFloat(dNetPrice) - parseFloat(Net_Value);
            //    }
            //}

            //dDisc = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            //dRepPrice = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            //DCTS = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            //dNetPrice = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            //dRepPrice = parseFloat(dRepPrice) * parseFloat(DCTS);
            //if (Scheme_Disc_Type == "Discount") {
            //    Final_Disc = parseFloat(dDisc) + parseFloat(Scheme_Disc);
            //    Net_Value = parseFloat(dRepPrice) - (parseFloat(dRepPrice) * parseFloat(Final_Disc) / 100);
            //    Web_Benefit = parseFloat(dNetPrice) - parseFloat(Net_Value);
            //}
            //else if (Scheme_Disc_Type == "Value") {
            //    Net_Value = parseFloat(dNetPrice) + (parseFloat(dNetPrice) * parseFloat(Scheme_Disc) / 100);
            //    Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(dRepPrice)) * 100) * -1;
            //    Web_Benefit = parseFloat(dNetPrice) - parseFloat(Net_Value);
            //}

            $('#tab' + CurrentTab + '_WebDisc_t').show();
            $('#tab' + CurrentTab + '_FinalValue_t').show();
            $('#tab' + CurrentTab + '_FinalDisc_t').show();
        } else {
            TOT_CTS = summary1.TOT_CTS;
            AVG_SALES_DISC_PER = summary1.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary1.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary1.TOT_NET_AMOUNT;
            TOT_PCS = summary1.TOT_PCS;
            $('#tab' + CurrentTab + '_WebDisc_t').hide();
            $('#tab' + CurrentTab + '_FinalValue_t').hide();
            $('#tab' + CurrentTab + '_FinalDisc_t').hide();
        }
    }
    else if (CurrentTab == 2) {
        if (gridOptions2.api.getSelectedRows().length > 0) {
            dDisc = _.reduce(_.pluck(gridOptions2.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions2.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions2.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions2.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(gridOptions2.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = gridOptions2.api.getSelectedRows().length;

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
            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }
            $('#tab' + CurrentTab + '_WebDisc_t').show();
            $('#tab' + CurrentTab + '_FinalValue_t').show();
            $('#tab' + CurrentTab + '_FinalDisc_t').show();
        } else {
            TOT_CTS = summary2.TOT_CTS;
            AVG_SALES_DISC_PER = summary2.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary2.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary2.TOT_NET_AMOUNT;
            TOT_PCS = summary2.TOT_PCS;
            $('#tab' + CurrentTab + '_WebDisc_t').hide();
            $('#tab' + CurrentTab + '_FinalValue_t').hide();
            $('#tab' + CurrentTab + '_FinalDisc_t').hide();
        }
    }
    else if (CurrentTab == 3) {
        if (gridOptions3.api.getSelectedRows().length > 0) {
            dDisc = _.reduce(_.pluck(gridOptions3.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions3.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions3.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions3.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(gridOptions3.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = gridOptions3.api.getSelectedRows().length;

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
            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }
            $('#tab' + CurrentTab + '_WebDisc_t').show();
            $('#tab' + CurrentTab + '_FinalValue_t').show();
            $('#tab' + CurrentTab + '_FinalDisc_t').show();
        } else {
            TOT_CTS = summary3.TOT_CTS;
            AVG_SALES_DISC_PER = summary3.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary3.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary3.TOT_NET_AMOUNT;
            TOT_PCS = summary3.TOT_PCS;
            $('#tab' + CurrentTab + '_WebDisc_t').hide();
            $('#tab' + CurrentTab + '_FinalValue_t').hide();
            $('#tab' + CurrentTab + '_FinalDisc_t').hide();
        }
    }
    else if (CurrentTab == 4) {
        if (gridOptions4.api.getSelectedRows().length > 0) {
            dDisc = _.reduce(_.pluck(gridOptions4.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions4.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions4.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions4.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(gridOptions4.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = gridOptions4.api.getSelectedRows().length;

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
            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }
            $('#tab' + CurrentTab + '_WebDisc_t').show();
            $('#tab' + CurrentTab + '_FinalValue_t').show();
            $('#tab' + CurrentTab + '_FinalDisc_t').show();
        } else {
            TOT_CTS = summary4.TOT_CTS;
            AVG_SALES_DISC_PER = summary4.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary4.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary4.TOT_NET_AMOUNT;
            TOT_PCS = summary4.TOT_PCS;
            $('#tab' + CurrentTab + '_WebDisc_t').hide();
            $('#tab' + CurrentTab + '_FinalValue_t').hide();
            $('#tab' + CurrentTab + '_FinalDisc_t').hide();
        }
    }
    else if (CurrentTab == 5) {
        if (gridOptions5.api.getSelectedRows().length > 0) {
            dDisc = _.reduce(_.pluck(gridOptions5.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions5.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions5.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions5.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(gridOptions5.api.getSelectedRows(), 'cur_rap_rate'), function (memo, num) { return memo + num; }, 0);
            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = gridOptions5.api.getSelectedRows().length;

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
            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }
            $('#tab' + CurrentTab + '_WebDisc_t').show();
            $('#tab' + CurrentTab + '_FinalValue_t').show();
            $('#tab' + CurrentTab + '_FinalDisc_t').show();
        } else {
            TOT_CTS = summary5.TOT_CTS;
            AVG_SALES_DISC_PER = summary5.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary5.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary5.TOT_NET_AMOUNT;
            TOT_PCS = summary5.TOT_PCS;
            $('#tab' + CurrentTab + '_WebDisc_t').hide();
            $('#tab' + CurrentTab + '_FinalValue_t').hide();
            $('#tab' + CurrentTab + '_FinalDisc_t').hide();
        }
    }

    $('#tab' + CurrentTab + 'cts').html(formatNumber(TOT_CTS));
    $('#tab' + CurrentTab + 'disc').html(formatNumber(AVG_SALES_DISC_PER));
    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(AVG_PRICE_PER_CTS));
    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(TOT_NET_AMOUNT));
    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(TOT_PCS));
    $('#tab' + CurrentTab + 'Web_Disc').html(formatNumber(Web_Benefit));
    $('#tab' + CurrentTab + 'Net_Value').html(formatNumber(Net_Value));
    $('#tab' + CurrentTab + 'Final_Disc').html(formatNumber(Final_Disc));
}
function columnVisible(params) {
    if (params.column.colId == 0 && params.visible) {
        $('#myGrid' + CurrentTab + ' .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
        if (CurrentTab == 1) {
            $($('#myGrid1 .ag-header-select-all')[1]).click(function () {
                if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                    gridOptions1.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                } else {
                    gridOptions1.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                onSelectionChanged();
            });
        }
        else if (CurrentTab == 2) {
            $($('#myGrid2 .ag-header-select-all')[1]).click(function () {
                if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                    gridOptions2.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                } else {
                    gridOptions2.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                onSelectionChanged();
            });
        }
        else if (CurrentTab == 3) {
            $($('#myGrid3 .ag-header-select-all')[1]).click(function () {
                if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                    gridOptions3.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                } else {
                    gridOptions3.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                onSelectionChanged();
            });
        }
        else if (CurrentTab == 4) {
            $($('#myGrid4 .ag-header-select-all')[1]).click(function () {
                if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                    gridOptions4.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                } else {
                    gridOptions4.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                onSelectionChanged();
            });
        }
        else if (CurrentTab == 5) {
            $($('#myGrid5 .ag-header-select-all')[1]).click(function () {
                if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                    gridOptions5.api.forEachNode(function (node) {
                        node.setSelected(false);
                    });
                } else {
                    gridOptions5.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                onSelectionChanged();
            });
        }
    }
}
function onBodyScroll(params) {
    $('#myGrid' + CurrentTab + ' .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
    if (CurrentTab == 1) {
        $('#myGrid1 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions1.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions1.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();


        });
    }
    else if (CurrentTab == 2) {
        $('#myGrid2 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions2.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions2.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
    }
    else if (CurrentTab == 3) {
        $('#myGrid3 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions3.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions3.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
    }
    else if (CurrentTab == 4) {
        $('#myGrid4 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions4.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions4.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
    }
    else if (CurrentTab == 5) {
        $('#myGrid5 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions5.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions5.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
    }
}
function onGridReady(params) {
    //var allColumnIds = [];
    //params.columnApi.getAllColumns().forEach(function (column) {
    //    allColumnIds.push(column.colId);
    //});

    //params.columnApi.autoSizeColumns(allColumnIds, false);

}

function onDragStopped1(params) {
    var Arr2 = [];
    var cnt = 1;
    var columnState = params.columnApi.getColumnState();
    for (var i = 1; i < columnState.length; i++) {
        if (columnState[i].colId != "net_amount_CurrencyAmt" && columnState[i].colId != "Hold_Party_Code"
            && columnState[i].colId != "Hold_CompName" && columnState[i].colId != "ForCust_Hold"
            && columnState[i].colId != "ForAssist_Hold" && columnState[i].colId != "ForAdmin_Hold"
            && columnState[i].colId != "Cur_Status") {
            var data = _.find(columnDefs, function (num) { return num.field == columnState[i].colId; })
            Arr2.push({ iColumnId: data.iColumnId, iPriority: cnt, sColumnName: data.ColumnName, sCaption: data.headerName, IsActive: true });
            cnt++;
        }
    }

    if (Arr2.length != 0) {
        $.ajax({
            url: "/Settings/SaveColumnsSettings",
            type: "POST",
            data: { obj: Arr2 },
            success: function (data, textStatus, jqXHR) {
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

var showEntryVar = null;
function GetSearch() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $('#result' + tabno + '').css('display', 'block');
    $('#result' + tabno + '').addClass('active');
    $('#profile-tab' + tabno + '').addClass('active show');
    $('#tabhome').removeClass('active');
    $('#home-tab2').removeClass('active show');
    $('#iresult' + tabno + '').show();
    CurrentTab = tabno;
    var gridDiv = document.querySelector('#myGrid' + tabno + '');
    
    if (tabno == 1) {
        if ($('#hdnType').val() == 'SmartSearch') {
            pgSize1 = 500;
        }
        else if (obj1.SmartSearch != "") {
            pgSize1 = 50;
        }

        if (gridOptions1.api != undefined) {
            gridOptions1.api.destroy();
        }
        gridOptions1 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 2,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                labIndicator: labIndicator,
                viewIndicator: viewIndicator,
                //countryIndicator: countryIndicator,
                //agColumnHeader: CustomHeader,
                myDetailCellRenderer: DetailCellRenderer
            },
            defaultColDef: {
                enableValue: false,
                enableRowGroup: false,
                enableSorting: false,
                sortable: false,
                resizable: true,
                enablePivot: false,
                filter: true,
                //tooltipComponent: CustomTooltip,
            },
            //tooltipShowDelay: 0,
            //tooltipHideDelay: 2000,

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
            onColumnVisible: columnVisible,
            onGridReady: onGridReady,
            //onColumnMoved: onColumnMoved1,
            onDragStopped: onDragStopped1,
            getMainMenuItems: getMainMenuItems,
            onSelectionChanged: onSelectionChanged,
            onBodyScroll: onBodyScroll,
            cacheBlockSize: pgSize1, // you can have your custom page size
            paginationPageSize: pgSize1, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };
        new agGrid.Grid(gridDiv, gridOptions1);
        $('#myGrid1 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions1.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions1.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
        if ($('#gallery-popup1').hasClass('show')) {
            $('#gallery-popup1').toggleClass('show');
            $('#aggrid-section1.gallery-grid').toggleClass('close');
        }
        $('#dvGalleryView1').html("");
        limit1 = 0;
        renderLimit1 = 0;
        gridOptions1.api.setServerSideDatasource(datasource1);
    }
    else if (tabno == 2) {
        if (gridOptions2.api != undefined) {
            gridOptions2.api.destroy();
        }
        gridOptions2 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 1,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                labIndicator: labIndicator,
                //countryIndicator: countryIndicator,
                viewIndicator: viewIndicator,
                //agColumnHeader: CustomHeader,
                myDetailCellRenderer: DetailCellRenderer
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
            onColumnVisible: columnVisible,
            onSelectionChanged: onSelectionChanged,
            onBodyScroll: onBodyScroll,
            //onColumnMoved: onColumnMoved1,
            onDragStopped: onDragStopped1,
            getMainMenuItems: getMainMenuItems,
            rowModelType: 'serverSide',
            cacheBlockSize: pgSize2, // you can have your custom page size
            paginationPageSize: pgSize2, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };
        new agGrid.Grid(gridDiv, gridOptions2);
        $('#myGrid2 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions2.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions2.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
        if ($('#gallery-popup2').hasClass('show')) {
            $('#gallery-popup2').toggleClass('show');
            $('#aggrid-section2.gallery-grid').toggleClass('close');
        }
        $('#dvGalleryView2').html("");
        limit2 = 0;
        renderLimit2 = 0;
        gridOptions2.api.setServerSideDatasource(datasource2);

    }
    else if (tabno == 3) {
        if (gridOptions3.api != undefined) {
            gridOptions3.api.destroy();
        }
        gridOptions3 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 1,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                labIndicator: labIndicator,
                //countryIndicator: countryIndicator,
                viewIndicator: viewIndicator,
                //agColumnHeader: CustomHeader,
                myDetailCellRenderer: DetailCellRenderer
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
            onColumnVisible: columnVisible,
            onSelectionChanged: onSelectionChanged,
            onBodyScroll: onBodyScroll,
            //onColumnMoved: onColumnMoved1,
            onDragStopped: onDragStopped1,
            getMainMenuItems: getMainMenuItems,
            rowModelType: 'serverSide',
            cacheBlockSize: pgSize3, // you can have your custom page size
            paginationPageSize: pgSize3, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };
        new agGrid.Grid(gridDiv, gridOptions3);
        $('#myGrid3 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions3.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions3.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
        if ($('#gallery-popup3').hasClass('show')) {
            $('#gallery-popup3').toggleClass('show');
            $('#aggrid-section3.gallery-grid').toggleClass('close');
        }
        $('#dvGalleryView3').html("");
        limit3 = 0;
        renderLimit3 = 0;
        gridOptions3.api.setServerSideDatasource(datasource3);

    }
    else if (tabno == 4) {
        if (gridOptions4.api != undefined) {
            gridOptions4.api.destroy();
        }
        gridOptions4 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 1,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                //countryIndicator: countryIndicator,
                labIndicator: labIndicator,
                viewIndicator: viewIndicator,
                //agColumnHeader: CustomHeader,
                myDetailCellRenderer: DetailCellRenderer
            },
            defaultColDef: {
                enableSorting: true,
                sortable: true,
                resizable: true
            },
            pagination: true,
            overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
            icons: {
                groupExpanded:
                    '<i class="fa fa-minus-circle"/>',
                groupContracted:
                    '<i class="fa fa-plus-circle"/>'
            },
            rowSelection: 'multiple',
            suppressRowClickSelection: true,
            columnDefs: columnDefs,
            onSelectionChanged: onSelectionChanged,
            onBodyScroll: onBodyScroll,
            rowModelType: 'serverSide',
            //onColumnMoved: onColumnMoved1,
            onDragStopped: onDragStopped1,
            getMainMenuItems: getMainMenuItems,
            onColumnVisible: columnVisible,
            cacheBlockSize: pgSize4, // you can have your custom page size
            paginationPageSize: pgSize4, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };
        new agGrid.Grid(gridDiv, gridOptions4);
        $('#myGrid4 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions4.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions4.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
        if ($('#gallery-popup4').hasClass('show')) {
            $('#gallery-popup4').toggleClass('show');
            $('#aggrid-section4.gallery-grid').toggleClass('close');
        }
        $('#dvGalleryView4').html("");
        limit4 = 0;
        renderLimit4 = 0;
        gridOptions4.api.setServerSideDatasource(datasource4);

    }
    else if (tabno == 5) {
        if (gridOptions5.api != undefined) {
            gridOptions5.api.destroy();
        }
        gridOptions5 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 1,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                //countryIndicator: countryIndicator,
                labIndicator: labIndicator,
                viewIndicator: viewIndicator,
                //agColumnHeader: CustomHeader,
                myDetailCellRenderer: DetailCellRenderer
            },
            defaultColDef: {
                enableSorting: true,
                sortable: true,
                resizable: true
            },
            pagination: true,
            overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
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
            onColumnVisible: columnVisible,
            //onColumnMoved: onColumnMoved1,
            onDragStopped: onDragStopped1,
            getMainMenuItems: getMainMenuItems,
            onSelectionChanged: onSelectionChanged,
            onBodyScroll: onBodyScroll,
            cacheBlockSize: pgSize5, // you can have your custom page size
            paginationPageSize: pgSize5, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };
        new agGrid.Grid(gridDiv, gridOptions5);
        $('#myGrid5 .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions5.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions5.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
        if ($('#gallery-popup5').hasClass('show')) {
            $('#gallery-popup5').toggleClass('show');
            $('#aggrid-section5.gallery-grid').toggleClass('close');
        }
        $('#dvGalleryView5').html("");
        limit5 = 0;
        renderLimit5 = 0;
        gridOptions5.api.setServerSideDatasource(datasource5);

    }

    var showEntryHtml = '<div class="show_entry show_entry' + CurrentTab + '"><label>'
        + 'Show <select onchange = "onPageSizeChanged' + CurrentTab + '(\'#ddltab' + CurrentTab + 'Pagesize\')" id = "ddltab' + CurrentTab + 'Pagesize" class="" >'
        + '<option value="50">50</option>'
        + '<option value="100">100</option>'
        + '<option value="200">200</option>'
        + '<option value="500">500</option>'
        + '</select> entries'
        + '</label>'
        + '</div>';

    showEntryVar = setInterval(function () {
        if ($('#myGrid' + CurrentTab + ' .ag-paging-panel').length > 0) {
            $('#myGrid' + CurrentTab + ' .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
            $(showEntryHtml).appendTo('#myGrid' + CurrentTab + ' .ag-paging-panel');
            if (CurrentTab == 1)
                $('#ddltab' + CurrentTab + 'Pagesize').val(pgSize1);
            else if (CurrentTab == 2)
                $('#ddltab' + CurrentTab + 'Pagesize').val(pgSize2);
            else if (CurrentTab == 3)
                $('#ddltab' + CurrentTab + 'Pagesize').val(pgSize3);
            else if (CurrentTab == 4)
                $('#ddltab' + CurrentTab + 'Pagesize').val(pgSize4);
            else if (CurrentTab == 5)
                $('#ddltab' + CurrentTab + 'Pagesize').val(pgSize5);
            clearInterval(showEntryVar);
        }
    }, 1000);

    $('#myGrid' + tabno + ' .gridpage_info').appendTo('#myGrid' + tabno + ' .ag-paging-panel');
    if ($('#result1').css('display') == 'none') {
        tabno = 1;
    }
    else if ($('#result2').css('display') == 'none') {
        tabno = 2;
    }
    else if ($('#result3').css('display') == 'none') {
        tabno = 3;
    }
    else if ($('#result4').css('display') == 'none') {
        tabno = 4;
    }
    else if ($('#result5').css('display') == 'none') {
        tabno = 5;
    }
    ResetActive();

}

/*-------------------------------------------------------- GET SEARCH AND AGGRID BINDING END--------------------------------------------------*/

/*--------------------------------------------------------SET RESET ACTIVE END--------------------------------------------------*/

function selectAll(e, tn) {
    if (e) {
        $('#select').addClass('ag-hidden');
        $('#unselect').removeClass('ag-hidden');
    }
    else {
        $('#unselect').addClass('ag-hidden');
        $('#select').removeClass('ag-hidden');
    }
    if (tn == 1) {
        gridOptions1.api.forEachNode(function (node) {
            node.setSelected(!e);
        });
    }
    else if (tn == 2) {
        gridOptions2.api.forEachNode(function (node) {
            node.setSelected(!e);
        });
    }
    else if (tn == 3) {
        gridOptions3.api.forEachNode(function (node) {
            node.setSelected(!e);
        });
    }
    else if (tn == 4) {
        gridOptions4.api.forEachNode(function (node) {
            node.setSelected(!e);
        });
    }
    else if (tn == 5) {
        gridOptions5.api.forEachNode(function (node) {
            node.setSelected(!e);
        });
    }
}

/*--------------------------------------------------------SET RESET ACTIVE START------------------------------------------------*/
function ResetActive() {
    _.map(ShapeList, function (data) { return data.ACTIVE = false; });
    _.map(ColorList, function (data) { return data.ACTIVE = false; });
    _.map(ClarityList, function (data) { return data.ACTIVE = false; });
    _.map(CutList, function (data) { return data.ACTIVE = false; });
    _.map(LabList, function (data) { return data.ACTIVE = false; });
    _.map(PolishList, function (data) { return data.ACTIVE = false; });
    _.map(SymList, function (data) { return data.ACTIVE = false; });
    _.map(FlouList, function (data) { return data.ACTIVE = false; });
    _.map(BgmList, function (data) { return data.ACTIVE = false; });
    _.map(TblInclList, function (data) { return data.ACTIVE = false; });
    _.map(TblNattsList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnInclList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnNattsList, function (data) { return data.ACTIVE = false; });
    _.map(LocationList, function (data) { return data.ACTIVE = false; });
    _.map(CaratList, function (data) { return data.ACTIVE = false; });
    _.map(TableOpenList, function (data) { return data.ACTIVE = false; });
    _.map(CrownOpenList, function (data) { return data.ACTIVE = false; });
    _.map(PavOpenList, function (data) { return data.ACTIVE = false; });
    _.map(GirdleOpenList, function (data) { return data.ACTIVE = false; });

    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('a[href="#carat1"]').click();
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
    $('#SearchImage').removeClass('active');
    $('#SearchVideo').removeClass('active');
    $('#li3ex').removeClass('active');
    $('#li3vg').removeClass('active');
    SizeGroupList = [];
    $('#searchcaratspecific').html("");
    $('#hdnShapeColorPurity').val("");

    $('#searchcaratgen').html("");
    _(CaratList).each(function (carat, i) {
        $('#searchcaratgen').append('<li onclick="SetActive(\'carat\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
    });

    $('#searchshape').html("");
    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div><span>ALL</span></a></li>');

    _(ShapeList).each(function (shape, i) {
        if (shape.Value != 'ALL') {
            $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
        }
    });

    $('#searchcolor').html("");
    _(ColorList).each(function (color, i) {
        $('#searchcolor').append('<li onclick="SetActive(\'COLOR\',\'' + color.Value + '\')">' + color.Value + '</li>');
    });

    $('#searchclarity').html("");
    _(ClarityList).each(function (clarity, i) {
        $('#searchclarity').append('<li onclick="SetActive(\'CLARITY\',\'' + clarity.Value + '\')">' + clarity.Value + '</li>');
    });

    $('#searchcut').html("");
    _(CutList).each(function (cut, i) {
        $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + cut.Value + '</li>');
    });

    $('#searchlab').html("");
    _(LabList).each(function (lab, i) {
        $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
    });

    $('#searchpolish').html("");
    _(PolishList).each(function (polish, i) {
        $('#searchpolish').append('<li onclick="SetActive(\'POLISH\',\'' + polish.Value + '\')">' + polish.Value + '</li>');
    });

    $('#searchsymm').html("");
    _(SymList).each(function (sym, i) {
        $('#searchsymm').append('<li onclick="SetActive(\'SYMM\',\'' + sym.Value + '\')">' + sym.Value + '</li>');
    });

    $('#searchfls').html("");
    _(FlouList).each(function (fls, i) {
        $('#searchfls').append('<li onclick="SetActive(\'FLS\',\'' + fls.Value + '\')">' + fls.Value + '</li>');
    });

    $('#searchbgm').html("");
    _(BgmList).each(function (bgm, i) {
        $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
    });

    BlackList = _.filter(ParameterList, function (e) { return e.ListType == 'BLACK' });

    $('#searchtableincl').html("");
    _(TblInclList).each(function (tblincl, i) {
        $('#searchtableincl').append('<li onclick="SetActive(\'TABLE_INCL\',\'' + tblincl.Value + '\')">' + tblincl.Value + '</li>');
    });

    $('#searchtablenatts').html("");
    _(TblNattsList).each(function (tblnatts, i) {
        $('#searchtablenatts').append('<li onclick="SetActive(\'TABLE_NATTS\',\'' + tblnatts.Value + '\')">' + tblnatts.Value + '</li>');
    });

    $('#searchcrownincl').html("");
    _(CrwnInclList).each(function (crwnincl, i) {
        $('#searchcrownincl').append('<li onclick="SetActive(\'CROWN_INCL\',\'' + crwnincl.Value + '\')">' + crwnincl.Value + '</li>');
    });

    $('#searchcrownnatts').html("");
    _(CrwnNattsList).each(function (crwnnatt, i) {
        $('#searchcrownnatts').append('<li onclick="SetActive(\'CROWN_NATTS\',\'' + crwnnatt.Value + '\')">' + crwnnatt.Value + '</li>');
    });

    $('#searchlocation').html("");
    _(LocationList).each(function (loc, i) {
        $('#searchlocation').append('<li onclick="SetActive(\'Location\',\'' + loc.Value + '\')">' + loc.Value + '</li>');
    });

    $('#searchtableopen').html("");
    _(TableOpenList).each(function (tableopen, i) {
        $('#searchtableopen').append('<li onclick="SetActive(\'TABLEOPEN\',\'' + tableopen.Value + '\')">' + tableopen.Value + '</li>');
    });

    $('#searchcrownopen').html("");
    _(CrownOpenList).each(function (crownopen, i) {
        $('#searchcrownopen').append('<li onclick="SetActive(\'CROWNOPEN\',\'' + crownopen.Value + '\')">' + crownopen.Value + '</li>');
    });

    $('#searchpavopen').html("");
    _(PavOpenList).each(function (pavopen, i) {
        $('#searchpavopen').append('<li onclick="SetActive(\'PAVILIONOPEN\',\'' + pavopen.Value + '\')">' + pavopen.Value + '</li>');
    });

    $('#searchgirdleopen').html("");
    _(GirdleOpenList).each(function (girdleopen, i) {
        $('#searchgirdleopen').append('<li onclick="SetActive(\'GIRDLEOPEN\',\'' + girdleopen.Value + '\')">' + girdleopen.Value + '</li>');
    });

    $('#hdnSavedSearchId').val("0");
    $('#txtSearchName').val("");
    $('#txtfromcarat').val("");
    $('#txttocarat').val("");
    $('#txtDisFrom').val("");
    $('#txtDisTo').val("");
    $('#txtPrCtsFrom').val("");
    $('#txtPrCtsTo').val("");
    $('#TotalAmtFrom').val("");
    $('#TotalAmtTo').val("");
    $('#txtLengthFrom').val("");
    $('#txtLengthTo').val("");
    $('#txtWidthFrom').val("");
    $('#txtWidthTo').val("");
    $('#txtDepthPerFrom').val("");
    $('#txtDepthPerTo').val("");
    $('#txtTablePerFrom').val("");
    $('#txtTablePerTo').val("");
    $('#txtCrAngFrom').val("");
    $('#txtCrAngTo').val("");
    $('#txtCrHtFrom').val("");
    $('#txtCrHtTo').val("");
    $('#txtPavAngFrom').val("");
    $('#txtPavAngTo').val("");
    $('#txtPavHtFrom').val("");
    $('#txtPavHtTo').val("");
    $('#SearchNewArrival').removeClass('active');
    Color_Hide_Show('3');
    resetINTENSITY();
    resetOVERTONE();
    resetFANCY_COLOR();
    $(".carat-dropdown-main").hide();
    FancyDDLHide();
}
function SetActive(flag, value) {
    if (flag == "Shape") {
        if (value == "ALL") {
            if (_.find(ShapeList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
                _(ShapeList).each(function (shape, i) {
                    shape.ACTIVE = false;
                });

                $("#searchshape").find('.common-ico').each(function (i, shape) {
                    if ($($(shape).find('span')[0]).text() != 'ALL') {
                        $(shape).removeClass('active');
                    }
                });
            } else {
                _(ShapeList).each(function (shape, i) {
                    shape.ACTIVE = true;
                });
                //$("#searchshape").find('.common-ico').addClass('active');
                $("#searchshape").find('.common-ico').each(function (i, shape) {

                    if ($($(shape).find('span')[0]).text() != 'ALL') {
                        $(shape).addClass('active');
                    }
                });
            }
        }
        else {
            if (_.find(ShapeList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
                _.findWhere(ShapeList, { Value: value }).ACTIVE = false;

                if (_.find(ShapeList, function (num) { return num.ACTIVE == true && num.Value == "ALL"; })) {
                    _.findWhere(ShapeList, { Value: "ALL" }).ACTIVE = false;

                    $("#searchshape").find('.common-ico').each(function (i, shape) {
                        if ($($(shape).find('span')[0]).text() == 'ALL') {
                            $(shape).removeClass('active');
                        }
                    });
                }
            } else {
                _.findWhere(ShapeList, { Value: value }).ACTIVE = true;
                var isAllActive = true;
                _(ShapeList).each(function (shape, i) {
                    if (shape.Value != "ALL" && shape.ACTIVE != true) {
                        isAllActive = false;
                    }
                });

                if (isAllActive) {
                    if (_.find(ShapeList, function (num) { return num.ACTIVE == false && num.Value == "ALL"; })) {
                        _.findWhere(ShapeList, { Value: "ALL" }).ACTIVE = true;

                        $("#searchshape").find('.common-ico').each(function (i, shape) {
                            if ($($(shape).find('span')[0]).text() == 'ALL') {
                                $(shape).addClass('active');
                            }
                        });
                    }
                }
            }
        }
    }
    else if (flag == "COLOR") {
        if (_.find(ColorList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(ColorList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(ColorList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "CLARITY") {
        if (_.find(ClarityList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(ClarityList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(ClarityList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "CUT") {
        if (_.find(CutList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CutList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CutList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "LAB") {
        if (_.find(LabList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(LabList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(LabList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "POLISH") {
        if (_.find(PolishList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(PolishList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(PolishList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "SYMM") {
        if (_.find(SymList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(SymList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(SymList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "FLS") {
        if (_.find(FlouList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(FlouList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(FlouList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "BGM") {
        if (_.find(BgmList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(BgmList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(BgmList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "TABLE_INCL") {
        if (_.find(TblInclList, function (num) { return num.ACTIVE == true; })) {
            _.findWhere(TblInclList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(TblInclList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "TABLE_NATTS") {
        if (_.find(TblNattsList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(TblNattsList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(TblNattsList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "CROWN_INCL") {
        if (_.find(CrwnInclList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CrwnInclList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CrwnInclList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "CROWN_NATTS") {
        if (_.find(CrwnNattsList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CrwnNattsList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CrwnNattsList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "Location") {
        if (_.find(LocationList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(LocationList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(LocationList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "carat") {
        if (_.find(CaratList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CaratList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CaratList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "TABLEOPEN") {
        if (_.find(TableOpenList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(TableOpenList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(TableOpenList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "CROWNOPEN") {
        if (_.find(CrownOpenList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CrownOpenList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CrownOpenList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "PAVILIONOPEN") {
        if (_.find(PavOpenList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(PavOpenList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(PavOpenList, { Value: value }).ACTIVE = true;
        }
    }
    else if (flag == "GIRDLEOPEN") {
        if (_.find(GirdleOpenList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(GirdleOpenList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(GirdleOpenList, { Value: value }).ACTIVE = true;
        }
    }
}

SetCutMaster = function (item) {
    _.each(CutList, function (itm) {
        $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    _.each(PolishList, function (itm) {
        $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    _.each(SymList, function (itm) {
        $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    if (item == '3EX' && !$('#li3ex').hasClass('active')) {
        $('#li3vg').removeClass('active');

        _.each(_.filter(CutList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'EX\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'EX\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'EX\')"]').addClass('active');
            itm.ACTIVE = true;
        });
    }
    else if (item == '3VG' && !$('#li3vg').hasClass('active')) {

        $('#li3ex').removeClass('active');
        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
    }
}
/*--------------------------------------------------------SET RESET ACTIVE END-----------------------------------------------*/

/*--------------------------------------------------------DATA SOURCE START----------------------------------------------*/
function onPageSizeChanged1(id) {
    var value = $(id).val();
    //gridOptions1.api.paginationSetPageSize(Number(value));
    pgSize1 = Number(value);
    tabno = 1;
    CurrentTab = 1;
    GetSearch();
}
function onPageSizeChanged2(id) {
    var value = $(id).val();
    //gridOptions2.api.paginationSetPageSize(Number(value));
    pgSize2 = Number(value);
    tabno = 2;
    CurrentTab = 2;
    GetSearch();
}
function onPageSizeChanged3(id) {
    var value = $(id).val();
    //gridOptions3.api.paginationSetPageSize(Number(value));
    pgSize3 = Number(value);
    tabno = 3;
    CurrentTab = 3;
    GetSearch();
}
function onPageSizeChanged4(id) {
    var value = $(id).val();
    //gridOptions4.api.paginationSetPageSize(Number(value));
    pgSize4 = Number(value);
    tabno = 4;
    CurrentTab = 4;
    GetSearch();
}
function onPageSizeChanged5(id) {
    var value = $(id).val();
    //gridOptions5.api.paginationSetPageSize(Number(value));
    pgSize5 = Number(value);
    tabno = 5;
    CurrentTab = 5;
    GetSearch();
}
/*--------------------------------------------------------PAGE SIZE END--------------------------------------------------*/

/*--------------------------------------------------------DATA SOURCE START------------------------------------------------*/
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetSearchSave() {
    var msg = "";
    //if ($('#customRadio11').prop('checked')) {
    //    objSearch.Action = "Notify Me";
    //    msg = $("#hdn_We_will_update_soon_in_Search_Stock_Notification").val() + " !!";
    //}
    //else
    if ($('#customRadio12').prop('checked')) {
        objSearch.Action = "Email Me";
        msg = $("#hdn_We_will_update_soon_in_Email").val() + " !!";
    }
    objSearch.dTransDate = $("#txtValidityDate").val();

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/SaveGetSearchStock",
        async: false,
        type: "POST",
        data: { obj: objSearch, tabNo: CurrentTab },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == 1) {
                ValidityDate();
                $("#hdnIsSearchNoFound").val("0");
                autoEmailModalHide();
                toastr.success(msg);
            }
            else if (data.Status == 0) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
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

const datasource1 = {
    getRows(params) {
        if (IsSearchFound == 0) {
            if ($('#hdnType').val() == "QuickSearch") {
                obj1.FormName = 'Quick Search';
            }
            else if ($('#hdnType').val() == "Recent") {
                obj1.FormName = 'Recent Search';
            }
            else if ($('#hdnType').val() == "SaveSearch") {
                obj1.FormName = 'Save Search';
            }
            else {
                obj1.FormName = 'Search Stock';
            }
        }
        else {
            obj1.FormName = 'Search Stock';
        }
        obj1.CurrencyAmt = $("#txtdExRate_1").val()
        obj1.ActivityType = 'Get';
        obj1.UsedFor = 'View';

        if ($('#hdnType').val() == 'SmartSearch') {
            obj1.PgSize = 500;
        }
        else if (obj1.SmartSearch != "") {
            obj1.PgSize = 50;
        }
        else {
            obj1.PgSize = pgSize1;
        }
        obj1.PageNo = gridOptions1.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj1.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }

        var filPointer = obj1.Pointer;
        var filShape = obj1.Shape;
        var filLab = obj1.Lab;
        var filColor = obj1.Color;
        var filPolish = obj1.Polish;
        var filClarity = obj1.Clarity;
        var filCut = obj1.Cut;
        var filSymm = obj1.Symm;
        var filFls = obj1.Fls;
        var filLocation = obj1.Location;

        if (IsSearchFound == 0) {
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
                obj1.Pointer = str;
            }

            if (params.request.filterModel.shape) {
                obj1.Shape = params.request.filterModel.shape.values.join(",");
            }

            if (params.request.filterModel.pointer) {
                obj1.Pointer = params.request.filterModel.pointer.values.join(",");
            }

            if (params.request.filterModel.lab) {
                obj1.Lab = params.request.filterModel.lab.values.join(",");
            }

            if (params.request.filterModel.color) {
                obj1.Color = params.request.filterModel.color.values.join(",");
            }

            if (params.request.filterModel.polish) {
                obj1.Polish = params.request.filterModel.polish.values.join(",");
            }

            if (params.request.filterModel.clarity) {
                obj1.Clarity = params.request.filterModel.clarity.values.join(",");
            }

            if (params.request.filterModel.cut) {
                obj1.Cut = params.request.filterModel.cut.values.join(",");
            }

            if (params.request.filterModel.symm) {
                obj1.Symm = params.request.filterModel.symm.values.join(",");
            }

            if (params.request.filterModel.fls) {
                obj1.Fls = params.request.filterModel.fls.values.join(",");
            }

            if (params.request.filterModel.Location) {
                obj1.Location = params.request.filterModel.Location.values.join(",");
            }
        }
        objSearch = obj1;
        
        if (obj1.NewArrival) {
            $(".tab1newarriv").removeClass("newarriv");
            $(".tab1newarriv").addClass("newarriv_click");
        }
        else {
            $(".tab1newarriv").removeClass("newarriv_click");
            $(".tab1newarriv").addClass("newarriv");
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj1, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                gridOptions1Data = data;

                obj1.Pointer = filPointer;
                obj1.Shape = filShape;
                obj1.Lab = filLab;
                obj1.Color = filColor;
                obj1.Polish = filPolish;
                obj1.Clarity = filClarity;
                obj1.Cut = filCut;
                obj1.Symm = filSymm;
                obj1.Fls = filFls;
                obj1.Location = filLocation;

                if (data.Data.length > 0) {
                    summary1 = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist1.push(obj);
                    });

                    //setTimeout(function () {
                    params.successCallback(data.Data[0].DataList, summary1.TOT_PCS);
                    //}, 1000);

                    //$('#tab' + CurrentTab + 'disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(summary1.AVG_SALES_DISC_PER) + '');
                    //$('#tab' + CurrentTab + 'totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary1.TOT_NET_AMOUNT) + '');

                    $('#tab' + CurrentTab + 'TCount').show();
                    $('#tab' + CurrentTab + 'cts').html(formatNumber(summary1.TOT_CTS));
                    $('#tab' + CurrentTab + 'disc').html(formatNumber(summary1.AVG_SALES_DISC_PER));
                    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(summary1.AVG_PRICE_PER_CTS));
                    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(summary1.TOT_NET_AMOUNT));
                    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(summary1.TOT_PCS));
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();

                    var SmartSearchstatus = false;
                    $.ajax({
                        url: "/SearchStock/SmartSearchDisplayedGet",
                        async: false,
                        type: "POST",
                        data: null,
                        success: function (data) {
                            SmartSearchstatus = data;
                        }
                    });
                    if ($('#hdnType').val() == 'SmartSearch' && SmartSearchstatus == false) {
                        $.ajax({
                            type: 'POST',
                            url: '/SearchStock/SmartSearchDisplayedSet',
                            dataType: "json",
                            data: { status: true },
                            success: function (data) { }
                        });

                        if (obj1.SmartSearch != "" && obj1.SmartSearch != undefined) {
                            var _stone_ref_no_certi_no = '', _SmartSearch_str = obj1.SmartSearch.toUpperCase().split(',');

                            var _SmartSearch_str = _SmartSearch_str.filter(function (el) {
                                return el != 'NULL' && el != '' && el != 'UNDEFINED';
                            });

                            for (var j = 0; j < data.Data[0].DataList.length; j++) {
                                _stone_ref_no_certi_no = commaset(_stone_ref_no_certi_no) + data.Data[0].DataList[j].stone_ref_no;
                                _stone_ref_no_certi_no = commaset(_stone_ref_no_certi_no) + data.Data[0].DataList[j].certi_no;
                            }
                            _stone_ref_no_certi_no = _stone_ref_no_certi_no.toUpperCase().split(',');

                            var elmts = _SmartSearch_str.filter(
                                function (i) {
                                    return this.indexOf(i) < 0;
                                },
                                _stone_ref_no_certi_no
                            );
                            if (elmts.length > 0) {
                                $('#AutoEmailModal1').modal('show');
                                $("#AutoEmailModal1 #idEmailModal_1").hide();
                                $("#AutoEmailModal1 #idEmailModal_2").show();
                                $("#AutoEmailModal1 #idEmailModal_2").html(elmts.join(", ") + ' ' + $("#hdn_This_stone_is_not_available").val());
                            }
                        }
                    }
                    //BindGalleryView1();
                } else {
                    gridOptions1.api.showNoRowsOverlay();
                    Datalist1 = [];
                    //$('#tab' + CurrentTab + 'TCount').hide();
                    $('#tab' + CurrentTab + 'cts').html('0');
                    $('#tab' + CurrentTab + 'disc').html('0');
                    $('#tab' + CurrentTab + 'ppcts').html('0');
                    $('#tab' + CurrentTab + 'totAmt').html('0');
                    $('#tab' + CurrentTab + 'pcs').html('0');
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();

                    if (!isPlaceOrder) {
                        if (obj1.SmartSearch != "" && obj1.SmartSearch != undefined) {
                            var _GridId = '', _SmartSearch_str = obj1.SmartSearch.toUpperCase().split(',');
                            _GridId = _GridId.toUpperCase().split(',');
                            var _SmartSearch_str = _SmartSearch_str.filter(function (el) {
                                return el != 'NULL' && el != '' && el != 'UNDEFINED';
                            });
                            var elmts = _SmartSearch_str.filter(
                                function (i) {
                                    return this.indexOf(i) < 0;
                                },
                                _GridId
                            );

                            if (elmts.length > 0) {
                                $('#AutoEmailModal1').modal('show');
                                $("#AutoEmailModal1 #idEmailModal_1").hide();
                                $("#AutoEmailModal1 #idEmailModal_2").show();
                                $("#AutoEmailModal1 #idEmailModal_2").html(elmts.join(", ") + ' ' + $("#hdn_This_stone_is_not_available").val());
                            }
                        }
                        else {
                            $('#AutoEmailModal').modal('show');
                            $("#AutoEmailModal #idEmailModal_1").show();
                            $("#AutoEmailModal #idEmailModal_2").hide();
                        }
                    }
                    else {
                        isPlaceOrder = false;
                    }
                }

                var instance = gridOptions1.api.getFilterInstance('shape');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.shape) {
                    $.each(params.request.filterModel.shape.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }

                instance = gridOptions1.api.getFilterInstance('lab');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.lab) {
                    $.each(params.request.filterModel.lab.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('pointer');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.pointer) {
                    $.each(params.request.filterModel.pointer.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('color');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.color) {
                    $.each(params.request.filterModel.color.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('clarity');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.clarity) {
                    $.each(params.request.filterModel.clarity.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('cut');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.cut) {
                    $.each(params.request.filterModel.cut.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('symm');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.symm) {
                    $.each(params.request.filterModel.symm.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('fls');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.fls) {
                    $.each(params.request.filterModel.fls.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                setTimeout(function () {
                    if (Datalist1.length > 12) {
                        renderLimit1 = renderLimit1 + 12;
                        $('#btnLoadMore1').show();
                    } else {
                        renderLimit1 = Datalist1.length;
                        $('#btnLoadMore1').hide();
                    }
                    BindGalleryView1();
                }, 1000);
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions1.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    }
};
const datasource2 = {
    getRows(params) {
        obj2.CurrencyAmt = $("#txtdExRate_2").val()
        obj2.FormName = 'Search Stock';
        obj2.ActivityType = 'Get';
        obj2.UsedFor = 'View';
        obj2.PgSize = pgSize2;
        obj2.PageNo = gridOptions2.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj2.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var filPointer = obj2.Pointer;
        var filShape = obj2.Shape;
        var filLab = obj2.Lab;
        var filColor = obj2.Color;
        var filPolish = obj2.Polish;
        var filClarity = obj2.Clarity;
        var filCut = obj2.Cut;
        var filSymm = obj2.Symm;
        var filFls = obj2.Fls;
        var filLocation = obj2.Location;

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
            obj2.Pointer = str;
        }
        if (params.request.filterModel.shape) {
            obj2.Shape = params.request.filterModel.shape.values.join(",");
        }
        if (params.request.filterModel.pointer) {
            obj2.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        if (params.request.filterModel.lab) {
            obj2.Lab = params.request.filterModel.lab.values.join(",");
        }
        if (params.request.filterModel.color) {
            obj2.Color = params.request.filterModel.color.values.join(",");
        }

        if (params.request.filterModel.polish) {
            obj2.Polish = params.request.filterModel.polish.values.join(",");
        }

        if (params.request.filterModel.clarity) {
            obj2.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        if (params.request.filterModel.cut) {
            obj2.Cut = params.request.filterModel.cut.values.join(",");
        }
        if (params.request.filterModel.symm) {
            obj2.Symm = params.request.filterModel.symm.values.join(",");
        }
        if (params.request.filterModel.fls) {
            obj2.Fls = params.request.filterModel.fls.values.join(",");
        }

        if (params.request.filterModel.Location) {
            obj2.Location = params.request.filterModel.Location.values.join(",");
        }
        objSearch = obj2;
        
        if (obj2.NewArrival) {
            $(".tab2newarriv").removeClass("newarriv");
            $(".tab2newarriv").addClass("newarriv_click");
        }
        else {
            $(".tab2newarriv").removeClass("newarriv_click");
            $(".tab2newarriv").addClass("newarriv");
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj2, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                obj2.Pointer = filPointer;
                obj2.Shape = filShape;
                obj2.Lab = filLab;
                obj2.Color = filColor;
                obj2.Polish = filPolish;
                obj2.Clarity = filClarity;
                obj2.Cut = filCut;
                obj2.Symm = filSymm;
                obj2.Fls = filFls;
                obj2.Location = filLocation;

                if (data.Data.length > 0) {
                    summary2 = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist2.push(obj);
                    });

                    //setTimeout(function () {
                    params.successCallback(data.Data[0].DataList, summary2.TOT_PCS);
                    //}, 500);

                    $('#tab' + CurrentTab + 'TCount').show();
                    $('#tab' + CurrentTab + 'cts').html(formatNumber(summary2.TOT_CTS));
                    $('#tab' + CurrentTab + 'disc').html(formatNumber(summary2.AVG_SALES_DISC_PER));
                    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(summary2.AVG_PRICE_PER_CTS));
                    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(summary2.TOT_NET_AMOUNT));
                    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(summary2.TOT_PCS));
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();
                    //BindGalleryView2();
                } else {
                    params.successCallback([], 0);
                    gridOptions2.api.showNoRowsOverlay();
                    Datalist2 = [];
                    //$('#tab' + CurrentTab + 'TCount').hide();
                    $('#tab' + CurrentTab + 'cts').html('0');
                    $('#tab' + CurrentTab + 'disc').html('0');
                    $('#tab' + CurrentTab + 'ppcts').html('0');
                    $('#tab' + CurrentTab + 'totAmt').html('0');
                    $('#tab' + CurrentTab + 'pcs').html('0');
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();
                    if (!isPlaceOrder) {
                        $('#AutoEmailModal').modal('show');
                        $("#AutoEmailModal #idEmailModal_1").show();
                        $("#AutoEmailModal #idEmailModal_2").hide();
                    }
                    else
                        isPlaceOrder = false;
                }
                var instance = gridOptions2.api.getFilterInstance('shape');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.shape) {
                    $.each(params.request.filterModel.shape.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }

                instance = gridOptions2.api.getFilterInstance('lab');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.lab) {
                    $.each(params.request.filterModel.lab.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions2.api.getFilterInstance('pointer');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.pointer) {
                    $.each(params.request.filterModel.pointer.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions2.api.getFilterInstance('color');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.color) {
                    $.each(params.request.filterModel.color.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions1.api.getFilterInstance('clarity');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.clarity) {
                    $.each(params.request.filterModel.clarity.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions2.api.getFilterInstance('cut');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.cut) {
                    $.each(params.request.filterModel.cut.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions2.api.getFilterInstance('symm');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.symm) {
                    $.each(params.request.filterModel.symm.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions2.api.getFilterInstance('fls');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.fls) {
                    $.each(params.request.filterModel.fls.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                setTimeout(function () {
                    if (Datalist2.length > 12) {
                        renderLimit2 = renderLimit2 + 12;
                        $('#btnLoadMore2').show();
                    } else {
                        renderLimit2 = Datalist2.length;
                        $('#btnLoadMore2').hide();
                    }
                    BindGalleryView2();
                }, 1000);
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions2.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
const datasource3 = {
    getRows(params) {
        //console.log(JSON.stringify(params.request, null, 1));
        obj3.CurrencyAmt = $("#txtdExRate_3").val()
        obj3.FormName = 'Search Stock';
        obj3.ActivityType = 'Get';
        obj3.UsedFor = 'View';
        obj3.PgSize = pgSize3;
        obj3.PageNo = gridOptions3.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj3.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var filPointer = obj3.Pointer;
        var filShape = obj3.Shape;
        var filLab = obj3.Lab;
        var filColor = obj3.Color;
        var filPolish = obj3.Polish;
        var filClarity = obj3.Clarity;
        var filCut = obj3.Cut;
        var filSymm = obj3.Symm;
        var filFls = obj3.Fls;
        var filLocation = obj3.Location;

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
            obj3.Pointer = str;
        }
        if (params.request.filterModel.shape) {
            obj3.Shape = params.request.filterModel.shape.values.join(",");
        }
        if (params.request.filterModel.pointer) {
            obj3.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        if (params.request.filterModel.lab) {
            obj3.Lab = params.request.filterModel.lab.values.join(",");
        }
        if (params.request.filterModel.color) {
            obj3.Color = params.request.filterModel.color.values.join(",");
        }

        if (params.request.filterModel.polish) {
            obj3.Polish = params.request.filterModel.polish.values.join(",");
        }
        if (params.request.filterModel.clarity) {
            obj3.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        if (params.request.filterModel.cut) {
            obj3.Cut = params.request.filterModel.cut.values.join(",");
        }
        if (params.request.filterModel.symm) {
            obj3.Symm = params.request.filterModel.symm.values.join(",");
        }
        if (params.request.filterModel.fls) {
            obj3.Fls = params.request.filterModel.fls.values.join(",");
        }
        if (params.request.filterModel.Location) {
            obj3.Location = params.request.filterModel.Location.values.join(",");
        }
        objSearch = obj3;
        
        if (obj3.NewArrival) {
            $(".tab3newarriv").removeClass("newarriv");
            $(".tab3newarriv").addClass("newarriv_click");
        }
        else {
            $(".tab3newarriv").removeClass("newarriv_click");
            $(".tab3newarriv").addClass("newarriv");
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj3, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                obj3.Pointer = filPointer;
                obj3.Shape = filShape;
                obj3.Lab = filLab;
                obj3.Color = filColor;
                obj3.Polish = filPolish;
                obj3.Clarity = filClarity;
                obj3.Cut = filCut;
                obj3.Symm = filSymm;
                obj3.Fls = filFls;
                obj3.Location = filLocation;

                if (data.Data.length > 0) {
                    summary3 = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist3.push(obj);
                    });

                    //setTimeout(function () {
                    params.successCallback(data.Data[0].DataList, summary3.TOT_PCS);
                    //}, 500);

                    $('#tab' + CurrentTab + 'TCount').show();
                    $('#tab' + CurrentTab + 'cts').html(formatNumber(summary3.TOT_CTS));
                    $('#tab' + CurrentTab + 'disc').html(formatNumber(summary3.AVG_SALES_DISC_PER));
                    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(summary3.AVG_PRICE_PER_CTS));
                    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(summary3.TOT_NET_AMOUNT));
                    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(summary3.TOT_PCS));
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();

                    //BindGalleryView3();
                } else {
                    params.successCallback([], 0);
                    gridOptions3.api.showNoRowsOverlay();
                    Datalist3 = [];
                    //$('#tab' + CurrentTab + 'TCount').hide();
                    $('#tab' + CurrentTab + 'cts').html('0');
                    $('#tab' + CurrentTab + 'disc').html('0');
                    $('#tab' + CurrentTab + 'ppcts').html('0');
                    $('#tab' + CurrentTab + 'totAmt').html('0');
                    $('#tab' + CurrentTab + 'pcs').html('0');
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();
                    if (!isPlaceOrder) {
                        $('#AutoEmailModal').modal('show');
                        $("#AutoEmailModal #idEmailModal_1").show();
                        $("#AutoEmailModal #idEmailModal_2").hide();
                    }
                    else
                        isPlaceOrder = false;
                }
                var instance = gridOptions3.api.getFilterInstance('shape');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.shape) {
                    $.each(params.request.filterModel.shape.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }

                instance = gridOptions3.api.getFilterInstance('lab');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.lab) {
                    $.each(params.request.filterModel.lab.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('pointer');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.pointer) {
                    $.each(params.request.filterModel.pointer.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('color');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.color) {
                    $.each(params.request.filterModel.color.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('clarity');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.clarity) {
                    $.each(params.request.filterModel.clarity.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('cut');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.cut) {
                    $.each(params.request.filterModel.cut.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('symm');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.symm) {
                    $.each(params.request.filterModel.symm.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions3.api.getFilterInstance('fls');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.fls) {
                    $.each(params.request.filterModel.fls.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                setTimeout(function () {
                    if (Datalist3.length > 12) {
                        renderLimit3 = renderLimit3 + 12;
                        $('#btnLoadMore3').show();
                    } else {
                        renderLimit3 = Datalist3.length;
                        $('#btnLoadMore3').hide();
                    }
                    BindGalleryView3();
                }, 1000);
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions3.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
const datasource4 = {
    getRows(params) {
        //console.log(JSON.stringify(params.request, null, 1));
        obj4.CurrencyAmt = $("#txtdExRate_4").val()
        obj4.FormName = 'Search Stock';
        obj4.ActivityType = 'Get';
        obj4.UsedFor = 'View';
        obj4.PageNo = gridOptions4.api.paginationGetCurrentPage() + 1;
        obj4.PgSize = pgSize4;
        if (params.request.sortModel.length > 0) {
            obj4.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var filPointer = obj4.Pointer;
        var filShape = obj4.Shape;
        var filLab = obj4.Lab;
        var filColor = obj4.Color;
        var filPolish = obj4.Polish;
        var filClarity = obj4.Clarity;
        var filCut = obj4.Cut;
        var filSymm = obj4.Symm;
        var filFls = obj4.Fls;
        var filLocation = obj4.Location;

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
            obj4.Pointer = str;
        }
        if (params.request.filterModel.shape) {
            obj4.Shape = params.request.filterModel.shape.values.join(",");
        }
        if (params.request.filterModel.pointer) {
            obj4.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        if (params.request.filterModel.lab) {
            obj4.Lab = params.request.filterModel.lab.values.join(",");
        }
        if (params.request.filterModel.color) {
            obj4.Color = params.request.filterModel.color.values.join(",");
        }

        if (params.request.filterModel.polish) {
            obj4.Polish = params.request.filterModel.polish.values.join(",");
        }
        if (params.request.filterModel.clarity) {
            obj4.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        if (params.request.filterModel.cut) {
            obj4.Cut = params.request.filterModel.cut.values.join(",");
        }
        if (params.request.filterModel.symm) {
            obj4.Symm = params.request.filterModel.symm.values.join(",");
        }
        if (params.request.filterModel.fls) {
            obj4.Fls = params.request.filterModel.fls.values.join(",");
        }
        if (params.request.filterModel.Location) {
            obj4.Location = params.request.filterModel.Location.values.join(",");
        }
        objSearch = obj4;
        
        if (obj4.NewArrival) {
            $(".tab4newarriv").removeClass("newarriv");
            $(".tab4newarriv").addClass("newarriv_click");
        }
        else {
            $(".tab4newarriv").removeClass("newarriv_click");
            $(".tab4newarriv").addClass("newarriv");
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj4, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                obj4.Pointer = filPointer;
                obj4.Shape = filShape;
                obj4.Lab = filLab;
                obj4.Color = filColor;
                obj4.Polish = filPolish;
                obj4.Clarity = filClarity;
                obj4.Cut = filCut;
                obj4.Symm = filSymm;
                obj4.Fls = filFls;
                obj4.Location = filLocation;

                if (data.Data.length > 0) {
                    summary4 = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist4.push(obj);
                    });

                    //setTimeout(function () {
                    params.successCallback(data.Data[0].DataList, summary4.TOT_PCS);
                    //}, 500);

                    $('#tab' + CurrentTab + 'TCount').show();
                    $('#tab' + CurrentTab + 'cts').html(formatNumber(summary4.TOT_CTS));
                    $('#tab' + CurrentTab + 'disc').html(formatNumber(summary4.AVG_SALES_DISC_PER));
                    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(summary4.AVG_PRICE_PER_CTS));
                    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(summary4.TOT_NET_AMOUNT));
                    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(summary4.TOT_PCS));
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();

                    //BindGalleryView4();
                } else {
                    params.successCallback([], 0);
                    gridOptions4.api.showNoRowsOverlay();
                    Datalist4 = [];
                    $('#btnLoadMore4').hide();
                    //$('#tab' + CurrentTab + 'TCount').hide();
                    $('#tab' + CurrentTab + 'cts').html('0');
                    $('#tab' + CurrentTab + 'disc').html('0');
                    $('#tab' + CurrentTab + 'ppcts').html('0');
                    $('#tab' + CurrentTab + 'totAmt').html('0');
                    $('#tab' + CurrentTab + 'pcs').html('0');
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();
                    if (!isPlaceOrder) {
                        $('#AutoEmailModal').modal('show');
                        $("#AutoEmailModal #idEmailModal_1").show();
                        $("#AutoEmailModal #idEmailModal_2").hide();
                    }
                    else
                        isPlaceOrder = false;
                }
                var instance = gridOptions4.api.getFilterInstance('shape');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.shape) {
                    $.each(params.request.filterModel.shape.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }

                instance = gridOptions4.api.getFilterInstance('lab');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.lab) {
                    $.each(params.request.filterModel.lab.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('pointer');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.pointer) {
                    $.each(params.request.filterModel.pointer.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('color');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.color) {
                    $.each(params.request.filterModel.color.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('clarity');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.clarity) {
                    $.each(params.request.filterModel.clarity.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('cut');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.cut) {
                    $.each(params.request.filterModel.cut.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('symm');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.symm) {
                    $.each(params.request.filterModel.symm.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions4.api.getFilterInstance('fls');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.fls) {
                    $.each(params.request.filterModel.fls.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                setTimeout(function () {
                    if (Datalist4.length > 12) {
                        renderLimit4 = renderLimit4 + 12;
                        $('#btnLoadMore4').show();
                    } else {
                        renderLimit4 = Datalist4.length;
                        $('#btnLoadMore4').hide();
                    }
                    BindGalleryView4();
                }, 1000);
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions4.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
const datasource5 = {
    getRows(params) {
        //console.log(JSON.stringify(params.request, null, 1));
        obj5.CurrencyAmt = $("#txtdExRate_5").val()
        obj5.FormName = 'Search Stock';
        obj5.ActivityType = 'Get';
        obj5.UsedFor = 'View';
        obj5.PageNo = gridOptions5.api.paginationGetCurrentPage() + 1;
        obj5.PgSize = pgSize5;
        if (params.request.sortModel.length > 0) {
            obj5.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var filPointer = obj5.Pointer;
        var filShape = obj5.Shape;
        var filLab = obj5.Lab;
        var filColor = obj5.Color;
        var filPolish = obj5.Polish;
        var filClarity = obj5.Clarity;
        var filCut = obj5.Cut;
        var filSymm = obj5.Symm;
        var filFls = obj5.Fls;
        var filLocation = obj5.Location;

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
            obj5.Pointer = str;
        }
        if (params.request.filterModel.shape) {
            obj5.Shape = params.request.filterModel.shape.values.join(",");
        }
        if (params.request.filterModel.pointer) {
            obj5.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        if (params.request.filterModel.lab) {
            obj5.Lab = params.request.filterModel.lab.values.join(",");
        }
        if (params.request.filterModel.color) {
            obj5.Color = params.request.filterModel.color.values.join(",");
        }
        if (params.request.filterModel.polish) {
            obj5.Polish = params.request.filterModel.polish.values.join(",");
        }
        if (params.request.filterModel.clarity) {
            obj5.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        if (params.request.filterModel.cut) {
            obj5.Cut = params.request.filterModel.cut.values.join(",");
        }
        if (params.request.filterModel.symm) {
            obj5.Symm = params.request.filterModel.symm.values.join(",");
        }
        if (params.request.filterModel.fls) {
            obj5.Fls = params.request.filterModel.fls.values.join(",");
        }
        if (params.request.filterModel.Location) {
            obj5.Location = params.request.filterModel.Location.values.join(",");
        }
        objSearch = obj5;
        
        if (obj5.NewArrival) {
            $(".tab5newarriv").removeClass("newarriv");
            $(".tab5newarriv").addClass("newarriv_click");
        }
        else {
            $(".tab5newarriv").removeClass("newarriv_click");
            $(".tab5newarriv").addClass("newarriv");
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj5, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                obj5.Pointer = filPointer;
                obj5.Shape = filShape;
                obj5.Lab = filLab;
                obj5.Color = filColor;
                obj5.Polish = filPolish;
                obj5.Clarity = filClarity;
                obj5.Cut = filCut;
                obj5.Symm = filSymm;
                obj5.Fls = filFls;
                obj5.Location = filLocation;

                if (data.Data.length > 0) {
                    summary5 = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist5.push(obj);
                    });

                    //setTimeout(function () {
                    params.successCallback(data.Data[0].DataList, summary5.TOT_PCS);
                    //}, 500);

                    $('#tab' + CurrentTab + 'TCount').show();
                    $('#tab' + CurrentTab + 'cts').html(formatNumber(summary5.TOT_CTS));
                    $('#tab' + CurrentTab + 'disc').html(formatNumber(summary5.AVG_SALES_DISC_PER));
                    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(summary5.AVG_PRICE_PER_CTS));
                    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(summary5.TOT_NET_AMOUNT));
                    $('#tab' + CurrentTab + 'pcs').html(formatIntNumber(summary5.TOT_PCS));
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();

                    //BindGalleryView5();
                } else {
                    params.successCallback([], 0);
                    Datalist5 = [];
                    gridOptions5.api.showNoRowsOverlay();
                    //$('#tab' + CurrentTab + 'TCount').hide();
                    $('#tab' + CurrentTab + 'cts').html('0');
                    $('#tab' + CurrentTab + 'disc').html('0');
                    $('#tab' + CurrentTab + 'ppcts').html('0');
                    $('#tab' + CurrentTab + 'totAmt').html('0');
                    $('#tab' + CurrentTab + 'pcs').html('0');
                    $('#tab' + CurrentTab + '_WebDisc_t').hide();
                    $('#tab' + CurrentTab + '_FinalValue_t').hide();
                    $('#tab' + CurrentTab + '_FinalDisc_t').hide();
                    if (!isPlaceOrder) {
                        $('#AutoEmailModal').modal('show');
                        $("#AutoEmailModal #idEmailModal_1").show();
                        $("#AutoEmailModal #idEmailModal_2").hide();
                    }
                    else
                        isPlaceOrder = false;
                }
                var instance = gridOptions5.api.getFilterInstance('shape');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.shape) {
                    $.each(params.request.filterModel.shape.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }

                instance = gridOptions5.api.getFilterInstance('lab');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.lab) {
                    $.each(params.request.filterModel.lab.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('pointer');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.pointer) {
                    $.each(params.request.filterModel.pointer.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('color');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.color) {
                    $.each(params.request.filterModel.color.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('clarity');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.clarity) {
                    $.each(params.request.filterModel.clarity.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('cut');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.cut) {
                    $.each(params.request.filterModel.cut.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('symm');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.symm) {
                    $.each(params.request.filterModel.symm.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                instance = gridOptions5.api.getFilterInstance('fls');
                if (instance != undefined)
                    instance.selectNothing();
                if (params.request.filterModel.fls) {
                    $.each(params.request.filterModel.fls.values, function (index, value) {
                        instance.selectValue(value);
                    });
                }
                setTimeout(function () {
                    if (Datalist5.length > 12) {
                        renderLimit5 = renderLimit5 + 12;
                        $('#btnLoadMore5').show();
                    } else {
                        renderLimit5 = Datalist5.length;
                        $('#btnLoadMore5').hide();
                    }
                    BindGalleryView5();
                }, 1000);
                contentHeight();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions5.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
function commaset(value) {
    if (value != '') {
        return value = value + ',';
    }
    else {
        return value;
    }
}
function LoadMore(TN) {
    if (TN == 1) {
        renderLimit1 = renderLimit1 + 12;
        if (Datalist1.length > renderLimit1) {
            $('#btnLoadMore1').show();
        } else {
            renderLimit1 = Datalist1.length;
            $('#btnLoadMore1').hide();
        }
        BindGalleryView1();
    }
    if (TN == 2) {
        renderLimit2 = renderLimit2 + 12;
        if (Datalist2.length > renderLimit2) {
            $('#btnLoadMore2').show();
        } else {
            renderLimit2 = Datalist2.length;
            $('#btnLoadMore2').hide();
        }
        BindGalleryView2();
    }
    if (TN == 3) {
        renderLimit3 = renderLimit3 + 12;
        if (Datalist3.length > renderLimit3) {
            $('#btnLoadMore3').show();
        } else {
            renderLimit3 = Datalist3.length;
            $('#btnLoadMore3').hide();
        }
        BindGalleryView3();
    }
    if (TN == 4) {
        renderLimit4 = renderLimit4 + 12;
        if (Datalist4.length > renderLimit4) {
            $('#btnLoadMore4').show();
        } else {
            renderLimit4 = Datalist4.length;
            $('#btnLoadMore4').hide();
        }
        BindGalleryView4();
    }
    if (TN == 5) {
        renderLimit5 = renderLimit5 + 12;
        if (Datalist5.length > renderLimit5) {
            $('#btnLoadMore5').show();
        } else {
            renderLimit5 = Datalist5.length;
            $('#btnLoadMore5').hide();
        }
        BindGalleryView5();
    }
}
function BindGalleryView1() {
    //$('#dvGalleryView1').html("");
    for (var i = limit1; i < renderLimit1; i++) {
        limit1 = limit1 + 1;
        var imageURL = "";
        if (Datalist1[i].IsOverseas == "1") {
            if ((Datalist1[i].Overseas_Image_Download_Link != "" || Datalist1[i].Overseas_Image_Download_Link != null ? Datalist1[i].Overseas_Image_Download_Link : "") == "") {
                imageURL = "/Content/images/no-img1.jpg";
            }
            else {
                imageURL = Datalist1[i].Overseas_Image_Download_Link;
            }
        }
        else if (Datalist1[i].IsOverseas == "0") {
            if ((Datalist1[i].image_url != "" || Datalist1[i].image_url != null ? Datalist1[i].image_url : "") == "") {
                imageURL = "/Content/images/no-img1.jpg";
            }
            else {
                imageURL = Datalist1[i].image_url;
            }
        }
        $('#dvGalleryView1').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            //'            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + imageURL + '">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist1[i].bPRimg ? $("#External_ImageURL").val() + Datalist1[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist1[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + Datalist1[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + Datalist1[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + Datalist1[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + Datalist1[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + Datalist1[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + Datalist1[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + Datalist1[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + Datalist1[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + Datalist1[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + Datalist1[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist1[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + Datalist1[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + Datalist1[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + Datalist1[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
function BindGalleryView2() {
    //Datalist2
    //$('#dvGalleryView2').html("");
    for (var i = limit2; i < renderLimit2; i++) {
        limit2 = limit2 + 1
        $('#dvGalleryView2').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist2[i].bPRimg ? $("#External_ImageURL").val() + Datalist2[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist2[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + Datalist2[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + Datalist2[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + Datalist2[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + Datalist2[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + Datalist2[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + Datalist2[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + Datalist2[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + Datalist2[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + Datalist2[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + Datalist2[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist2[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + Datalist2[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + Datalist2[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + Datalist2[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
function BindGalleryView3() {
    //Datalist3
    //$('#dvGalleryView3').html("");
    for (var i = limit3; i < renderLimit3; i++) {
        limit3 = limit3 + 1
        $('#dvGalleryView3').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist3[i].bPRimg ? $("#External_ImageURL").val() + Datalist3[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist3[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + Datalist3[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + Datalist3[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + Datalist3[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + Datalist3[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + Datalist3[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + Datalist3[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + Datalist3[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + Datalist3[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + Datalist3[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + Datalist3[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist3[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + Datalist3[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + Datalist3[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + Datalist3[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
function BindGalleryView4() {
    //Datalist4
    //$('#dvGalleryView4').html("");
    for (var i = limit4; i < renderLimit4; i++) {
        limit4 = limit4 + 1
        $('#dvGalleryView4').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist4[i].bPRimg ? $("#External_ImageURL").val() + Datalist4[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist4[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + Datalist4[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + Datalist4[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + Datalist4[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + Datalist4[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + Datalist4[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + Datalist4[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + Datalist4[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + Datalist4[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + Datalist4[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + Datalist4[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist4[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + Datalist4[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + Datalist4[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + Datalist4[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
function BindGalleryView5() {
    //Datalist5
    // $('#dvGalleryView5').html("");
    for (var i = limit5; i < renderLimit5; i++) {
        limit5 = limit5 + 1
        $('#dvGalleryView5').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (Datalist5[i].bPRimg ? $("#External_ImageURL").val() + Datalist5[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist5[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + Datalist5[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + Datalist5[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + Datalist5[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + Datalist5[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + Datalist5[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + Datalist5[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + Datalist5[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + Datalist5[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + Datalist5[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + Datalist5[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + Datalist5[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + Datalist5[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + Datalist5[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + Datalist5[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}
/*--------------------------------------------------------DATA SOURCE END--------------------------------------------------*/

/*--------------------------------------------------------CUSTOM HEADER START--------------------------------------------------*/

function CustomHeader() { }

CustomHeader.prototype.init = function (agParams) {
    this.agParams = agParams;
    this.eGui = document.createElement('div');
    if (agParams.displayName != "") {
        this.eGui.innerHTML =
            '' +
            //'<div class="customHeaderMenuButton"><i class="fa ' +
            //this.agParams.menuIcon +
            //'"></i></div>' +
            '<div class="customHeaderLabel">' +
            this.agParams.displayName +
            '</div>' +
            '<div class="customSortDownLabel"><i class="fa fa-long-arrow-up"></i></div>' +
            '<div class="customSortUpLabel"><i class="fa fa-long-arrow-down"></i></div>';
    } else {
        this.eGui.innerHTML =
            '' +
            //'<div class="customHeaderMenuButton"><i class="fa ' +
            //this.agParams.menuIcon +
            //'"></i></div>' +
            '<div ref="eCellWrapper" class="ag-cell-wrapper" role="presentation">' +
            '<span class="ag-selection-checkbox"><span id="select" class="ag-icon ag-icon-checkbox-checked ag-hidden" unselectable="on" onclick="selectAll(true,\'' + CurrentTab + '\')" ></span>' +
            '<span id="unselect" class="ag-icon ag-icon-checkbox-unchecked" unselectable="on" onclick="selectAll(false,\'' + CurrentTab + '\')" ></span> ' +
            '<span class="ag-icon ag-icon-checkbox-indeterminate ag-hidden" unselectable="on"></span></span >' +
            '<span ref="eCellValue" role="gridcell" aria-colindex="2" class="ag-cell-value" unselectable="on"></span></div> ' +

            '<div class="customSortDownLabel inactive"><i class="fa fa-long-arrow-up"></i></div>' +
            '<div class="customSortUpLabel inactive"><i class="fa fa-long-arrow-down"></i></div>';
    }
    //this.eMenuButton = this.eGui.querySelector('.customHeaderMenuButton');
    this.eSortDownButton = this.eGui.querySelector('.customSortDownLabel');
    this.eSortUpButton = this.eGui.querySelector('.customSortUpLabel');

    //if (this.agParams.enableMenu) {
    //    this.onMenuClickListener = this.onMenuClick.bind(this);
    //    this.eMenuButton.addEventListener('click', this.onMenuClickListener);
    //} else {
    //    this.eGui.removeChild(this.eMenuButton);
    //}

    if (this.agParams.enableSorting) {
        this.onSortAscRequestedListener = this.onSortRequested.bind(this, 'asc');
        this.eSortDownButton.addEventListener(
            'click',
            this.onSortAscRequestedListener
        );
        this.onSortDescRequestedListener = this.onSortRequested.bind(this, 'desc');
        this.eSortUpButton.addEventListener(
            'click',
            this.onSortDescRequestedListener
        );

        this.onSortChangedListener = this.onSortChanged.bind(this);
        this.agParams.column.addEventListener(
            'sortChanged',
            this.onSortChangedListener
        );
        this.onSortChanged();
    } else {
        this.eGui.removeChild(this.eSortDownButton);
        this.eGui.removeChild(this.eSortUpButton);
    }
};

CustomHeader.prototype.onSortChanged = function () {
    function deactivate(toDeactivateItems) {
        toDeactivateItems.forEach(function (toDeactivate) {
            toDeactivate.className = toDeactivate.className.split(' ')[0];
        });
    }

    function activate(toActivate) {
        toActivate.className = toActivate.className + ' sortactive';
    }

    if (this.agParams.column.isSortAscending()) {
        activate(this.eSortDownButton);
        deactivate([this.eSortUpButton]);
    } else if (this.agParams.column.isSortDescending()) {
        activate(this.eSortUpButton);
        deactivate([this.eSortDownButton]);
    } else {
        deactivate([this.eSortUpButton, this.eSortDownButton]);
    }
};

CustomHeader.prototype.getGui = function () {
    return this.eGui;
};

CustomHeader.prototype.onSortRequested = function (order, event) {
    this.agParams.setSort(order, event.shiftKey);
};

CustomHeader.prototype.destroy = function () {
    //if (this.onMenuClickListener) {
    //    this.eMenuButton.removeEventListener('click', this.onMenuClickListener);
    //}
    this.eSortDownButton.removeEventListener(
        'click',
        this.onSortRequestedListener
    );
    this.eSortUpButton.removeEventListener('click', this.onSortRequestedListener);

    this.agParams.column.removeEventListener(
        'sortChanged',
        this.onSortChangedListener
    );
};

/*--------------------------------------------------------CUSTOM HEADER END--------------------------------------------------*/

/*--------------------------------------------------------ADD TO CART START--------------------------------------------------*/

function AddToCart(TN) {
    var stoneList = [];
    if (TN == 1) {
        stoneList = gridOptions1.api.getSelectedRows();
    }
    else if (TN == 2) {
        stoneList = gridOptions2.api.getSelectedRows();
    }
    else if (TN == 3) {
        stoneList = gridOptions3.api.getSelectedRows();
    }
    else if (TN == 4) {
        stoneList = gridOptions4.api.getSelectedRows();
    }
    else if (TN == 5) {
        stoneList = gridOptions5.api.getSelectedRows();
    }
    debugger
    var availabelstonelist = '';
    var offerstonelist = '';
    if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
        debugger
        availabelstonelist = _.pluck(stoneList, 'stone_ref_no').join(",");
    } else {
        debugger
        availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' }), 'stone_ref_no').join(",");
        offerstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' }), 'stone_ref_no').join(",");
    }
    if (availabelstonelist != '') {
        debugger
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/AddToCart",
            type: "POST",
            data: { stoneNo: availabelstonelist, transType: 'A' },
            success: function (data, textStatus, jqXHR) {
                debugger
                data.Message = data.Message.replace('Stone(s) added in cart successfully', $("#hdn_Stones_added_in_cart_successfully").val());
                data.Message = data.Message.replace('Stone(s) removed from cart successfully', $("#hdn_Stones_removed_from_cart_successfully").val());
                data.Message = data.Message.replace('This Stones(s) are already added to cart', $("#hdn_This_Stones_are_already_added_to_cart").val());

                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
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

                var gridOptions = null;
                if (TN == 1) {
                    gridOptions = gridOptions1;
                }
                else if (TN == 2) {
                    gridOptions = gridOptions2;
                }
                else if (TN == 3) {
                    gridOptions = gridOptions3;
                }
                else if (TN == 4) {
                    gridOptions = gridOptions4;
                }
                else if (TN == 5) {
                    gridOptions = gridOptions5;
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

function GoToCart(TN) {
    var stoneno = '';

    if (TN == 1) {
        stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 2) {
        stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 3) {
        stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 4) {
        stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 5) {
        stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    if (stoneno == '') {
        window.location = "/Cart/Index";
    }
    else {
        AddToCart(TN);
    }
}

/*--------------------------------------------------------ADD TO CART END----------------------------------------------------*/

/*--------------------------------------------------------ADD TO WISHLIST START----------------------------------------------*/

function AddToWishlist(TN) {
    var stoneno = '';
    var count = 0;

    if (TN == 1) {
        count = gridOptions1.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 2) {
        count = gridOptions2.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 3) {
        count = gridOptions3.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 4) {
        count = gridOptions4.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 5) {
        count = gridOptions5.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
    }

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

                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
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

                var gridOptions = null;
                if (TN == 1) {
                    gridOptions = gridOptions1;
                }
                else if (TN == 2) {
                    gridOptions = gridOptions2;
                }
                else if (TN == 3) {
                    gridOptions = gridOptions3;
                }
                else if (TN == 4) {
                    gridOptions = gridOptions4;
                }
                else if (TN == 5) {
                    gridOptions = gridOptions5;
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

function GoToWishlist(TN) {
    var stoneno = '';

    if (TN == 1) {
        stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 2) {
        stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 3) {
        stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 4) {
        stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 5) {
        stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    if (stoneno == '') {
        window.location = "/Wishlist/Index";
    }
    else {
        AddToWishlist(TN);
    }
}
/*--------------------------------------------------------ADD TO WISHLIST END------------------------------------------------*/

/*--------------------------------------------------------COMPARE STONE START----------------------------------------------*/

function OpenComparStoneModel(TN) {
    var stoneno = '';
    var count = 0;

    if (TN == 1) {
        count = gridOptions1.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 2) {
        count = gridOptions2.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 3) {
        count = gridOptions3.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 4) {
        count = gridOptions4.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
    }
    else if (TN == 5) {
        count = gridOptions5.api.getSelectedRows().length;
        stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
    }

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
                        //str += '<th><span>' + $("#hdn_Disc_Per").val() + ' : </span></th>';
                        str += '<th><span>' + $("#hdn_Offer_Disc_Per").val() + ' : </span></th>';
                        ComparStoneList.Disc.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.Disc[i]) + '</span></td>';
                        });
                        str += '</tr>';
                        str += '<tr>'
                        //str += '<th><span>' + $("#hdn_Net_Amt").val() + ' :</span></th>';
                        str += '<th><span>' + $("#hdn_Offer_Value_Dollar").val() + ' :</span></th>';
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

/*--------------------------------------------------------COMPARE STONE END------------------------------------------------*/

/*--------------------------------------------------------PLACE ORDER START--------------------------------------------------*/
var StoneLstForPlace = '';
function ConfirmOrderModal() {
    StoneLstForPlace = '';

    $('#Comments').val("");

    if (CurrentTab == 1) {
        stoneList = gridOptions1.api.getSelectedRows();
    }
    else if (CurrentTab == 2) {
        stoneList = gridOptions2.api.getSelectedRows();
    }
    else if (CurrentTab == 3) {
        stoneList = gridOptions3.api.getSelectedRows();
    }
    else if (CurrentTab == 4) {
        stoneList = gridOptions4.api.getSelectedRows();
    }
    else if (CurrentTab == 5) {
        stoneList = gridOptions5.api.getSelectedRows();
    }

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
                if (CurrentTab == 1) {
                    stoneList = gridOptions1.api.getSelectedRows();
                }
                else if (CurrentTab == 2) {
                    stoneList = gridOptions2.api.getSelectedRows();
                }
                else if (CurrentTab == 3) {
                    stoneList = gridOptions3.api.getSelectedRows();
                }
                else if (CurrentTab == 4) {
                    stoneList = gridOptions4.api.getSelectedRows();
                }
                else if (CurrentTab == 5) {
                    stoneList = gridOptions5.api.getSelectedRows();
                }

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

/*--------------------------------------------------------HOLD STONE START--------------------------------------------------*/
var ___availabelstonelist = '', ___availabelstonewithoutbusyary = '', ___availabelstonewithoutbusylist = '', ___offerstonelist = '',
    ___HoldStone_Lst = '', ___HoldStone_ary = [], ___HoldStone_tblbody = '', ___StoneLstForPlace = '', ___Hold_Stone_FortuneCode_Lst = '',
    ___not_authorize = '', ___Employee_Hold_Stone_FortuneCode_Lst = '', ___company_Userid = '', ___company_Fortunecode = '',
    ___Hold_List = [], ___UnHold_List = [], ___UnHold_Lst = '', ___ValidSelection = false;
function HoldStoneModal() {
    var ___stoneList = [];
    ___availabelstonelist = '', ___availabelstonewithoutbusyary = '', ___availabelstonewithoutbusylist = '', ___offerstonelist = '',
        ___HoldStone_Lst = '', ___HoldStone_ary = [], ___HoldStone_tblbody = '', ___StoneLstForPlace = '', ___Hold_Stone_FortuneCode_Lst = '',
        ___not_authorize = '', ___Employee_Hold_Stone_FortuneCode_Lst = '', ___company_Userid = '', ___company_Fortunecode = '',
        ___Hold_List = [], ___UnHold_List = [], ___UnHold_Lst = '', ___ValidSelection = false;

    $('#___Comments').val("");
    $("#___divPlaceOrderHoldCompany").hide();

    if (CurrentTab == 1) {
        ___stoneList = gridOptions1.api.getSelectedRows();
    }
    else if (CurrentTab == 2) {
        ___stoneList = gridOptions2.api.getSelectedRows();
    }
    else if (CurrentTab == 3) {
        ___stoneList = gridOptions3.api.getSelectedRows();
    }
    else if (CurrentTab == 4) {
        ___stoneList = gridOptions4.api.getSelectedRows();
    }
    else if (CurrentTab == 5) {
        ___stoneList = gridOptions5.api.getSelectedRows();
    }

    if (___stoneList.length == 0) {
        toastr.warning($("#hdn_No_Stone_Selected_for_hold_stone").val());
        return;
    }

    if (___stoneList.length == (_.filter(___stoneList, function (e) { return e.Location.toUpperCase() == "HONG KONG" }).length)) {
        if (___stoneList.length == (_.filter(___stoneList, function (e) { return e.Cur_Status.toUpperCase() == "A" }).length)) {
            ___ValidSelection = true;
        }
        else if (___stoneList.length == (_.filter(___stoneList, function (e) { return e.Cur_Status.toUpperCase() == "H" }).length)) {
            ___ValidSelection = true;
        }
        else if (___stoneList.length == (_.filter(___stoneList, function (e) { return e.Cur_Status.toUpperCase() == "C" }).length)) {
            ___ValidSelection = true;
        }
        else if (___stoneList.length == (_.filter(___stoneList, function (e) { return e.Cur_Status.toUpperCase() == "I" }).length)) {
            ___ValidSelection = true;
        }
        else {
            ___ValidSelection = false;

            $('#___ppPlaceOrderMsg').html('');
            $('#___ppPlaceOrderMsg').append('<div>' + $("#hdn_HoldStoneMsg_7").val() + '...!</div>');
            if ($('#___ppPlaceOrderMsg').html() != "") {
                $('#HoldStoneWarningModal').modal('show');
                $('#HoldStoneModal').modal('hide');
                return;
            }
        }
    }
    else {
        ___ValidSelection = false;

        $('#___ppPlaceOrderMsg').html('');
        $('#___ppPlaceOrderMsg').append('<div><b>' + _.pluck(_.filter(___stoneList, function (e) { return e.Location.toUpperCase() != "HONG KONG" }), 'stone_ref_no').join(", ") + '</b> ' + $("#hdn_HoldStoneMsg_6").val() + '...!</div>');
        if ($('#___ppPlaceOrderMsg').html() != "") {
            $('#HoldStoneWarningModal').modal('show');
            $('#HoldStoneModal').modal('hide');
            return;
        }
    }

    if (___ValidSelection == true) {
        if ($('#hdnisadminflg').val() == '1') {
            ___availabelstonelist = _.pluck(_.filter(___stoneList), 'stone_ref_no').join(",");
            ___availabelstonewithoutbusyary = _.filter(___stoneList, function (e) { return e.Cur_Status == 'A' && e.ForAdmin_Hold == 0 });
            ___availabelstonewithoutbusylist = _.pluck(_.filter(___availabelstonewithoutbusyary), 'stone_ref_no').join(",");

            ___HoldStone_ary = _.filter(___stoneList, function (e) { return (e.Cur_Status == 'H' || e.Cur_Status == 'C' || e.Cur_Status == 'I') });
            ___HoldStone_Lst = _.pluck(_.filter(___HoldStone_ary), 'stone_ref_no').join(",");

            ___offerstonelist = _.pluck(_.filter(___stoneList, function (e) { return e.status == 'AVAILABLE OFFER' }), 'stone_ref_no').join(", ");

            for (var i = 0; i < ___availabelstonewithoutbusyary.length; i++) {
                ___UnHold_List.push({
                    sRefNo: ___availabelstonewithoutbusyary[i].stone_ref_no
                });
            }
            ___UnHold_Lst = _.pluck(_.filter(___UnHold_List), 'sRefNo').join(",");
        }
        else if ($('#hdnisempflg').val() == '1') {
            ___availabelstonewithoutbusyary = _.filter(___stoneList, function (e) { return e.Cur_Status == 'A' && e.ForAssist_Hold == 0 });
            ___availabelstonelist = _.pluck(_.filter(___availabelstonewithoutbusyary), 'stone_ref_no').join(",");

            ___not_authorize = _.pluck(_.filter(___stoneList, function (e) { return e.Cur_Status != 'A' && e.ForAssist_Hold == 0 }), 'stone_ref_no').join(", ");

            ___HoldStone_ary = _.filter(___stoneList, function (e) { return ((e.Cur_Status == 'H' || e.Cur_Status == 'C' || e.Cur_Status == 'I') && (e.ForAssist_Hold == 1)) });
            ___HoldStone_Lst = _.pluck(_.filter(___HoldStone_ary), 'stone_ref_no').join(",");

            ___offerstonelist = _.pluck(_.filter(___stoneList, function (e) { return e.status == 'AVAILABLE OFFER' }), 'stone_ref_no').join(", ");

            for (var i = 0; i < ___availabelstonewithoutbusyary.length; i++) {
                ___UnHold_List.push({
                    sRefNo: ___availabelstonewithoutbusyary[i].stone_ref_no
                });
            }
            ___UnHold_Lst = _.pluck(_.filter(___UnHold_List), 'sRefNo').join(",");
        }
        else {
            ___availabelstonelist = _.pluck(_.filter(___stoneList, function (e) { return (e.Cur_Status == 'A') && (e.ForCust_Hold == 0) }), 'stone_ref_no').join(",");
            ___offerstonelist = _.pluck(_.filter(___stoneList, function (e) { return e.status == 'AVAILABLE OFFER' && (e.ForCust_Hold == 0) }), 'stone_ref_no').join(", ");

            ___HoldStone_ary = _.filter(___stoneList, function (e) { return ((e.Cur_Status == 'H' || e.Cur_Status == 'C' || e.Cur_Status == 'I') && (e.ForCust_Hold == 1)) });
            ___HoldStone_Lst = _.pluck(_.filter(___HoldStone_ary), 'stone_ref_no').join(",");

            for (var i = 0; i < ___HoldStone_ary.length; i++) {
                ___Hold_List.push({
                    sRefNo: ___HoldStone_ary[i].stone_ref_no,
                    Hold_Party_Code: ___HoldStone_ary[i].Hold_Party_Code,
                    Hold_CompName: ___HoldStone_ary[i].Party_Name,
                    Status: ___HoldStone_ary[i].Cur_Status
                });
            }
        }

        if (___offerstonelist != "") {
            $('#___ppPlaceOrderMsg').html('');
            $('#___ppPlaceOrderMsg').append('<div><b>' + ___offerstonelist + '</b> ' + $("#hdn_HoldStoneMsg_8").val() + '...!</div>');
            if ($('#___ppPlaceOrderMsg').html() != "") {
                $('#HoldStoneWarningModal').modal('show');
                $('#HoldStoneModal').modal('hide');
                return;
            }
        }

        if ($('#hdnisadminflg').val() == '1') {
            $('#___ppPlaceOrderMsg').html('');
            if (___availabelstonewithoutbusylist != "" && ___HoldStone_Lst != "") {
                $('#___ppPlaceOrderMsg').append('<div>' + $("#hdn_HoldStoneMsg_4").val() + '...!</div>');
            }
            if ($('#___ppPlaceOrderMsg').html() != "") {
                $('#HoldStoneWarningModal').modal('show');
                $('#HoldStoneModal').modal('hide');
                return;
            }
        }
        if ($('#hdnisempflg').val() == '1') {
            $('#___ppPlaceOrderMsg').html('');
            if (___availabelstonelist != "" && ___HoldStone_Lst != "") {
                $('#___ppPlaceOrderMsg').append('<div>' + $("#hdn_HoldStoneMsg_4").val() + '...!</div>');
            }
            if (___not_authorize != "") {
                $('#___ppPlaceOrderMsg').append('<div>' + $("#hdn_HoldStoneMsg_5").val() + ' <b style="font-weight: 700;">' + ___not_authorize + '</b>...!</div>');
            }
            if ($('#___ppPlaceOrderMsg').html() != "") {
                $('#HoldStoneWarningModal').modal('show');
                $('#HoldStoneModal').modal('hide');
                return;
            }
        }

        $("#___divHoldList").html("");
        $('#___pPlaceOrderMsg').html("");

        if (___HoldStone_Lst != '') {
            if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
                var ___flag = 0;
                ___HoldStone_tblbody = "<center><table id='tblHold' border='1' style='font-size:12px; width:95%; margin-top:5px; display:block; max-height:154px; overflow-y:auto;'>";
                ___HoldStone_tblbody += "<thead>";
                ___HoldStone_tblbody += "<tr>";
                ___HoldStone_tblbody += "<td style='display:none;background-color: #003d66;color: white;padding: 3px;width: 5%;'><center><b>No.</b></center></td>";
                ___HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 15%;'><center><b>Stock ID</b></center></td>";
                ___HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 15%;'><center><b>Party Code</b></center></td>";
                ___HoldStone_tblbody += "<td style='background-color: #003d66;color: white;padding: 3px;width: 65%;'><b>Already Holded Party Name</b></td>";
                ___HoldStone_tblbody += "</tr>";
                ___HoldStone_tblbody += "</thead>";
                ___HoldStone_tblbody += "<tbody>";
                for (var i = 0; i < ___HoldStone_ary.length; i++) {
                    var id = parseInt(i) + 1;
                    ___HoldStone_tblbody += "<tr>";
                    ___HoldStone_tblbody += "<td style='display:none;'><center><b>" + id + "</b></center></td>";
                    ___HoldStone_tblbody += "<td><center><b>" + ___HoldStone_ary[i].stone_ref_no + "</b></center></td>";
                    ___HoldStone_tblbody += "<td><center>" + (___HoldStone_ary[i].Hold_Party_Code == 0 ? '' : ___HoldStone_ary[i].Hold_Party_Code) + "</center></td>";
                    ___HoldStone_tblbody += "<td>" + ___HoldStone_ary[i].Party_Name + "</td>";
                    ___HoldStone_tblbody += "</tr>";
                    ___flag = 1;
                }
                ___HoldStone_tblbody += "</tbody>";
                ___HoldStone_tblbody += "</table></center>";
                if (___flag == 0) {
                    ___HoldStone_tblbody = "";
                }
                else {
                    $("#___divHoldList").html(___HoldStone_tblbody);
                }

                $("#___divPlaceOrderHoldCompany").show();
                $("#___txtCompanyName_hidden").val("");
                $("#___txtCompanyName").val("");

                if (___HoldStone_Lst != '') {
                    var ___HoldStone_Lst_space = _.pluck(_.filter(___HoldStone_ary), 'stone_ref_no').join(", ");
                    $('#___pPlaceOrderMsg').html(
                        '<div>' + $("#hdn_HoldStoneMsg_3").val() + ' <b>' + ___HoldStone_Lst_space + '</b>...!</div>');
                }
            }
            $('#HoldStoneModal').modal('show');
        }

        if (___UnHold_Lst != "") {
            if (_.pluck(_.filter(___stoneList, function (e) { return (e.Cur_Status == 'A' || e.Cur_Status == 'H' || e.Cur_Status == 'C' || e.Cur_Status == 'I') }), 'stone_ref_no').join(",") != "") {
                $("#___divPlaceOrderHoldCompany").show();
                $("#___txtCompanyName_hidden").val("");
                $("#___txtCompanyName").val("");
                $('#HoldStoneModal').modal('show');
            }
            else {
                $("#___divPlaceOrderHoldCompany").hide();
                $("#___txtCompanyName_hidden").val("");
                $("#___txtCompanyName").val("");
            }
        }

        if (___HoldStone_Lst != '') {
            if ($('#hdnisadminflg').val() != '1') {
                ___StoneLstForPlace += "," + ___HoldStone_Lst;
            }
            if (___availabelstonelist != '') {
                ___StoneLstForPlace += "," + ___availabelstonelist;
            }
            if (___offerstonelist != '') {
                $('#___pPlaceOrderMsg').html(
                    '<div><b>' + ___offerstonelist + '</b> ' + $("#hdn_HoldStoneMsg_1").val() + '...!</div>' +
                    ' <div>' + $("#hdn_HoldStoneMsg_2").val() + ' ? </div>');
            }
        }
        else if (___availabelstonelist != '') {
            $('#HoldStoneModal').modal('show');
            ___StoneLstForPlace += "," + ___availabelstonelist;
            if (___offerstonelist != '') {
                $('#___pPlaceOrderMsg').html(
                    '<div><b>' + ___offerstonelist + '</b> ' + $("#hdn_HoldStoneMsg_1").val() + ' ...!</div>' +
                    ' <div>' + $("#hdn_HoldStoneMsg_2").val() + ' ? </div>');
            }
        }
        else if (___offerstonelist != '') {
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
                        $('#___PlaceOrderMsg').html('<div>' + $("#hdn_Select_Avail_Stone_for_place_order").val() + ' !<br>' + $("#hdn_PleaseContact").val() + data.Message + '</div>');
                        $('#HoldStoneModal').modal('hide');
                        $('#HoldStoneWarningModal').modal('show');
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
            toastr.warning($("#hdn_No_Stone_Selected_for_hold_stone").val());
        }
        ___StoneLstForPlace = ___StoneLstForPlace.substring(1);
    }
}
function SaveHold() {
    ___company_Userid = '', ___company_Fortunecode = '';
    if ($("#___txtCompanyName_hidden").val().split("__").length != 2) {
        $("#___txtCompanyName_hidden").val("");
        $("#___txtCompanyName").val("");
        $("#___txtCompanyName").focus();
        toastr.warning("Select Company Name");
        return;
    }
    if ($("#___Comments").val().trim() == "") {
        $("#___Comments").val("");
        $("#___Comments").focus();
        toastr.warning("Enter Comments");
        return;
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    setTimeout(function () {

        if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
            if ($("#___txtCompanyName_hidden").val().split("__").length == 2) {
                ___company_Userid = $("#___txtCompanyName_hidden").val().split("__")[0];
                ___company_Fortunecode = $("#___txtCompanyName_hidden").val().split("__")[1];

                var Hold_obj = {};
                Hold_obj.UserID = ___company_Userid;
                Hold_obj.StoneID = ___StoneLstForPlace;
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
                            ___Hold_List = [];
                            for (var i = 0; i < ___HoldStone_ary.length; i++) {
                                ___Hold_List.push({
                                    sRefNo: ___HoldStone_ary[i].stone_ref_no,
                                    Hold_Party_Code: (___HoldStone_ary[i].Hold_Party_Code == 0 ? "0" : ___HoldStone_ary[i].Hold_Party_Code),
                                    Hold_CompName: ___HoldStone_ary[i].Party_Name,
                                    Status: ___HoldStone_ary[i].Cur_Status
                                });
                            }
                            if (___StoneLstForPlace != "") {
                                HoldStone();
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
        //else {
        //    if (___StoneLstForPlace != "") {
        //        HoldStone();
        //    }
        //}
    }, 30);
}
function HoldStone() {
    var _obj = {};
    _obj.StoneID = ___StoneLstForPlace;
    _obj.Comments = $('#___Comments').val();
    _obj.Userid = (___company_Userid == '' ? 0 : ___company_Userid);
    _obj.IsAdminEmp_Hold = ((___company_Userid == '' ? '0' : ___company_Userid) == '0' ? false : true);
    _obj.Hold_Stone_List = ___Hold_List;
    _obj.UnHold_Stone_List = ___UnHold_List;

    $.ajax({
        url: "/SearchStock/HoldStone_1",
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
            } else {
                $('#HoldStoneModal').modal('hide');
                $('#HoldStoneWarningModal').modal('hide');
                var ___stoneList = [];
                if (CurrentTab == 1) {
                    ___stoneList = gridOptions1.api.getSelectedRows();
                }
                else if (CurrentTab == 2) {
                    ___stoneList = gridOptions2.api.getSelectedRows();
                }
                else if (CurrentTab == 3) {
                    ___stoneList = gridOptions3.api.getSelectedRows();
                }
                else if (CurrentTab == 4) {
                    ___stoneList = gridOptions4.api.getSelectedRows();
                }
                else if (CurrentTab == 5) {
                    ___stoneList = gridOptions5.api.getSelectedRows();
                }
                if (data.Status == "SUCCESS") {
                    $('#lblfinalmessage').html(data.Message);
                    $('#Holdconfirmmodal').modal('show');
                    tabno = CurrentTab;
                    GetSearch();
                }
                else if (data.Status == "FAIL") {
                    toastr.warning(data.Message.toString());
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
/*--------------------------------------------------------HOLD STONE END----------------------------------------------------*/

/*--------------------------------------------------------SEND EMAIL START----------------------------------------------------*/
function SendMail() {
    var isValid = $('#frmSendMail').valid();
    if (!isValid) {
        return false;
    }
    if ($('#customRadiomail').prop('checked')) {
        var sobj = {};
        if (CurrentTab == 1) {
            obj1.UsedFor = "View";
            sobj = obj1;
        }
        else if (CurrentTab == 2) {
            obj2.UsedFor = "View";
            sobj = obj2;
        }
        else if (CurrentTab == 3) {
            obj3.UsedFor = "View";
            sobj = obj3;
        }
        else if (CurrentTab == 4) {
            obj4.UsedFor = "View";
            sobj = obj4;
        }
        else if (CurrentTab == 5) {
            obj5.UsedFor = "View";
            sobj = obj5;
        }
        sobj.FormName = ActivityType;
        sobj.ActivityType = 'Excel Email';

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/SearchStockEmailStone",//"/SearchStock/EmailAllStone",
            type: "POST",
            data: { SearchCriteria: sobj, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val(), tabNo: CurrentTab },
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
        var stoneno = '';
        var count = 0;
        var colorType = '';
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        if (CurrentTab == 1) {
            count = gridOptions1.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj1.ColorType;
        }
        else if (CurrentTab == 2) {
            count = gridOptions2.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj2.ColorType;
        }
        else if (CurrentTab == 3) {
            count = gridOptions3.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj3.ColorType;
        }
        else if (CurrentTab == 4) {
            count = gridOptions4.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj4.ColorType;
        }
        else if (CurrentTab == 5) {
            count = gridOptions5.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj5.ColorType;
        }

        if (count > 0) {
            $.ajax({
                url: "/SearchStock/EmailSelectedStone",
                type: "POST",
                data: {
                    StoneID: stoneno, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val(),
                    FormName: ActivityType, ActivityType: 'Excel Email', ColorType: colorType
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
            toastr.warning($("#hdn_No_stones_selected_to_send_email").val() + '!');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}
/*--------------------------------------------------------SEND EMAIL END----------------------------------------------------*/
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
    $('#customRadio3').prop('checked', true);
}
function DownloadExcel() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {};
        if (CurrentTab == 1) {
            obj1.FormName = ActivityType;
            obj1.ActivityType = 'Excel Export';
            obj1.UsedFor = "Download";
            sobj = obj1;
        }
        else if (CurrentTab == 2) {
            obj2.FormName = ActivityType;
            obj2.ActivityType = 'Excel Export';
            obj2.UsedFor = "Download";
            sobj = obj2;
        }
        else if (CurrentTab == 3) {
            obj3.FormName = ActivityType;
            obj3.ActivityType = 'Excel Export';
            obj3.UsedFor = "Download";
            sobj = obj3;
        }
        else if (CurrentTab == 4) {
            obj4.FormName = ActivityType;
            obj4.ActivityType = 'Excel Export';
            obj4.UsedFor = "Download";
            sobj = obj4;
        }
        else if (CurrentTab == 5) {
            obj5.FormName = ActivityType;
            obj5.ActivityType = 'Excel Export';
            obj5.UsedFor = "Download";
            sobj = obj5;
        }

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/SearchStockExcelDownload",
            type: "POST",
            data: { obj: sobj, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();

                if (data.search('.xlsx') == -1) {
                    if (data.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
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
    } else {
        var stoneno = '';
        var count = 0;
        var colorType = 0;
        var UsedFor = "Download";
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (CurrentTab == 1) {
            count = gridOptions1.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj1.ColorType;
        }
        else if (CurrentTab == 2) {
            count = gridOptions2.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj2.ColorType;
        }
        else if (CurrentTab == 3) {
            count = gridOptions3.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj3.ColorType;
        }
        else if (CurrentTab == 4) {
            count = gridOptions4.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj4.ColorType;
        }
        else if (CurrentTab == 5) {
            count = gridOptions5.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
            colorType = obj5.ColorType;
        }
        if (count > 0) {
            $.ajax({
                url: "/Common/StockExcelDownloadByStoneId",
                type: "POST",
                data: {
                    StoneID: stoneno, FormName: ActivityType, ActivityType: 'Excel Export', ColorType: colorType, UsedFor: UsedFor
                },
                success: function (data, textStatus, jqXHR) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();

                    if (data.search('.xlsx') == -1) {
                        if (data.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
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
        } else {
            toastr.warning($("#hdn_No_stone_selected_for_download_as_a_excel").val() + '!');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }
}

function DownloadMedia() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {};
        if (CurrentTab == 1) {
            obj1.FormName = ActivityType;
            obj1.ActivityType = $('#hdnDownloadType').val() + ' Download';
            obj1.UsedFor = "";
            sobj = obj1;
        }
        else if (CurrentTab == 2) {
            obj2.FormName = ActivityType;
            obj2.ActivityType = $('#hdnDownloadType').val() + ' Download';
            obj2.UsedFor = "";
            sobj = obj2;
        }
        else if (CurrentTab == 3) {
            obj3.FormName = ActivityType;
            obj3.ActivityType = $('#hdnDownloadType').val() + ' Download';
            obj3.UsedFor = "";
            sobj = obj3;
        }
        else if (CurrentTab == 4) {
            obj4.FormName = ActivityType;
            obj4.ActivityType = $('#hdnDownloadType').val() + ' Download';
            obj4.UsedFor = "";
            sobj = obj4;
        }
        else if (CurrentTab == 5) {
            obj5.FormName = ActivityType;
            obj5.ActivityType = $('#hdnDownloadType').val() + ' Download';
            obj5.UsedFor = "";
            sobj = obj5;
        }
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/SearchStockMediaDownload", //"/Common/StockMediaDownloadBySearchObject",
            type: "POST",
            data: { obj: sobj, MediaType: $('#hdnDownloadType').val(), tabNo: CurrentTab },
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
    } else {
        var stoneno = '';
        var count = 0;
        var UsedFor = "";
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (CurrentTab == 1) {
            count = gridOptions1.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 2) {
            count = gridOptions2.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 3) {
            count = gridOptions3.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 4) {
            count = gridOptions4.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 5) {
            count = gridOptions5.api.getSelectedRows().length;
            stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        if (count > 0) {
            $.ajax({
                url: "/Common/StockMediaDownloadByStoneId",
                type: "POST",
                data: { StoneID: stoneno, MediaType: $('#hdnDownloadType').val(), FormName: ActivityType, ActivityType: $('#hdnDownloadType').val() + ' Download', UsedFor: UsedFor },
                success: function (data, textStatus, jqXHR) {
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
                    if (AllD == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (AllD == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
                    }
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
function SaveSearch() {

    if ($('#txtSearchName').val() == "") {
        return toastr.warning($("#hdn_Please_Fill_SearchName").val() + '!')
    }
    if ($('#hdnSavedSearchId').val() == "0" || $('#hdnSavedSearchId').val() == "") {
        var SavedSearchID = parseFloat(0);
    }
    else {
        var SavedSearchID = $('#hdnSavedSearchId').val();
    }
    if ($('#hdnSavedSearchId').val() == "0" || $('#hdnSavedSearchId').val() == "") {
        var SavedSearchID = parseFloat(0);

        if (SaveSearchList.length == 0) {
            toastr.warning($("#hdn_Please_select_atlist_one_criteria").val());
            $("#save-src-modl").modal("hide");
            return;
        }
    }
    var Searchname = $('#txtSearchName').val();

    $('#ConfirmOrderModal').modal('show');
    $('#ConfirmOrderWarningModal').modal('show');
    $http({
        method: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/SearchStock/SaveSearch',
        data: JSON.stringify({ SearchId: SavedSearchID, Searchname: Searchname, model: SaveSearchList }),
    }).then(function (data) {
        $('#ConfirmOrderModal').modal('hide');
        $('#ConfirmOrderWarningModal').modal('hide');
        if (data.data == "Search Save data successfully.") {
            $("#save-src-modl").modal("hide");
            toastr.success($("#hdn_Record_Save_Successfully").val());
            SearchStone();
        }
        else {
            toastr.warning(data.data, 5000);
        }
        $('#txtSearchName').val("");
    });

}
function isNumberKey_ISD(evt) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode == 45) {
            return true;
        }
        return false;
    }

    return true;
}
function setToFixed(obj) {
    if ($(obj).val() != "") {
        $(obj).val(parseFloat($(obj).val()).toFixed(2));
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
            if (_Firstload == false) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
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
function CallingMarquee() {
    $.ajax({
        url: "/User/NewsMaster",
        type: "POST",
        data: { Flag: "SelectId", FromDate: "", ToDate: "", Description: "", FontColor: "", iID: 0 },
        success: function (data, textStatus, jqXHR) {
            if (data.Data != null) {
                if (data.Data.length != 0) {
                    var _d = "";
                    _d += '<marquee width="100%" direction="left" height="100%">';
                    for (var i = 0; i < data.Data.length; i++) {
                        _d += "<span style=color:" + data.Data[i].FontColor + ">" + data.Data[i].Description.replace(/'/g, ' &_ ') + "</span>";
                        _d += data.Data.length != i + 1 ? "<span style=color:" + data.Data[i].FontColor + ">,&nbsp;</span>" : "";
                    }
                    _d += '</marquee>';

                    $("#divMarquee").html(_d.replace(/ &_ /g, "'"));
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

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
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
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
                    $('#txtCompanyName,#___txtCompanyName').ejAutocomplete({
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
                    $('#txtCompanyName,#___txtCompanyName').ejAutocomplete({
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
function HelpField(field) {
    $("#htitle").html(field);
    $('#AbbreviationModal').modal('show');

    $("#tblAbbBdy").html("");
    if (field == "Table Open") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>TO1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>TO2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Crown Open") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>CO1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>CO2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Pav Open") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>PO1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>PO2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Girdle Open") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>GO1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>GO2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Table White") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>T1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>T2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>T3</b></center></td><td><center>Heavy</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Crown White") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>C1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>C2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>C3</b></center></td><td><center>Heavy</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Table Black") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BT1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BT2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BT3</b></center></td><td><center>Heavy</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
    else if (field == "Crown Black") {
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>-</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>NN</b></center></td><td><center>None</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BC1</b></center></td><td><center>Minor</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BC2</b></center></td><td><center>Medium</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>BC3</b></center></td><td><center>Heavy</center></td></tr>");
        $("#tblAbbBdy").append("<tr><td><center><b>-</b></center></td><td><center>Blank</center></td></tr>");
    }
}
function HelpFieldModalHide() {
    $('#AbbreviationModal').modal('hide');
}
function getMainMenuItems(params) {
    switch (params.column.getId()) {
        case 'Table_Open':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Table Open");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'Crown_Open':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Crown Open");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'Pav_Open':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Pav Open");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'Girdle_Open':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Girdle Open");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'inclusion':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Table White");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'table_natts':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Table Black");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'Crown_Natts':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Crown Black");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        case 'Crown_Inclusion':
            return [
                'pinSubMenu',
                'separator',
                {
                    name: 'Sunrise Grading',
                    action: () => {
                        HelpField("Crown White");
                    },
                    icon:
                        '<img src="/Content/images/nav-logo.png" style="width: 17px;" />',
                },
                'autoSizeThis',
                'autoSizeAll',
                'separator',
                'resetColumns', // a built in item
            ];
        default:
            // make no changes, just accept the defaults
            return params.defaultItems;
    }
}
function newarriv(tab) {
    if ($(".tab" + tab + "newarriv").hasClass("newarriv")) {
        $(".tab" + tab + "newarriv").removeClass("newarriv");
        $(".tab" + tab + "newarriv").addClass("newarriv_click");
        $('#SearchNewArrival').addClass('active');
    }
    else {
        $(".tab" + tab + "newarriv").removeClass("newarriv_click");
        $(".tab" + tab + "newarriv").addClass("newarriv");
        $('#SearchNewArrival').removeClass('active');
    } 

    tabno = CurrentTab;
    if (tabno == 1) {
        obj1.NewArrival = $('#SearchNewArrival').hasClass('active') ? true : "";
    }
    else if (tabno == 2) {
        obj2.NewArrival = $('#SearchNewArrival').hasClass('active') ? true : "";
    }
    else if (tabno == 3) {
        obj3.NewArrival = $('#SearchNewArrival').hasClass('active') ? true : "";
    }
    else if (tabno == 4) {
        obj4.NewArrival = $('#SearchNewArrival').hasClass('active') ? true : "";
    }
    else if (tabno == 5) {
        obj5.NewArrival = $('#SearchNewArrival').hasClass('active') ? true : "";
    }
    GetSearch();
}
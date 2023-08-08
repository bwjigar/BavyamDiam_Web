var FROMDATE = '', TODATE = '';
var isCustomer = false;
var ParameterList;
var ShapeList = [];
var CaratList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var PolishList = [];
var SymList = [];
var FlouList = [];
var BGMList = [];
var LabList = [];
var SizeGroupList = [];
var TblInclList = [];
var TblNattsList = [];
var CrwnInclList = [];
var CrwnNattsList = [];

var KTSBlank = false;
var LengthBlank = false;
var WidthBlank = false;
var DepthBlank = false;
var DepthPerBlank = false;
var TablePerBlank = false;
var CrAngBlank = false;
var CrHtBlank = false;
var PavAngBlank = false;
var PavHtBlank = false;

var TableOpenList = [];
var CrownOpenList = [];
var PavOpenList = [];
var GirdleOpenList = [];

var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];

var Check_Color_1 = [];
var Check_Color_2 = [];
var Check_Color_3 = [];
var KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var Color_Type = 'Regular';
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

function greaterThanDateLab(evt) {
    var fDate = $.trim($('#txtFromDate').val());
    var tDate = $.trim($('#txtToDate').val());
    if (fDate != "" && tDate != "") {
        if (new Date(tDate) >= new Date(fDate)) {
            FROMDATE = fDate;
            TODATE = tDate;
            GetTransId();
            return true;
        }
        else {
            evt.currentTarget.value = "";
            toastr.warning($("#hdn_To_date_must_be_greater_than_From_date").val() + " !");
            FromTo_Date_PrevSet(FROMDATE, TODATE);
            return false;
        }
    }
    else {
        return true;
    }
}
function FromTo_Date_PrevSet(_FROM_, _TO_) {
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: _FROM_,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDateLab(e);
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: _TO_,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10),

    }).on('change', function (e) {
        greaterThanDateLab(e);
    });
}
new WOW().init();
$(document).ready(function () {
    FROMDATE = F_date;
    TODATE = F_date;
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
        greaterThanDateLab(e);
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
        maxYear: parseInt(moment().format('YYYY'), 10),

    }).on('change', function (e) {
        greaterThanDateLab(e);
    });

    // For Shape selection
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

    $(".numeric").numeric({ decimal: ".", negative: true, decimalPlaces: 2 });

    GetSearchParameter();

    GetTransId();

    FcolorBind();
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
    BindKeyToSymbolList();
    $('#divGridView li a.download-popup').on('click', function (event) {
        $('.download-toggle').toggleClass('active');
        event.stopPropagation();
    });

    $('#ExcelModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadio4').prop('checked', true);
        } else {
            $('#customRadio3').prop('checked', true);
        }
    });
});

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

        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "3EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
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
        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "VG" || e.Value == "3EX" }), function (itm) {
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
function NewSizeGroup() {

    fcarat = $('#txtfromcarat').val();
    tcarat = $('#txttocarat').val();

    if (fcarat == "" && tcarat == "" || fcarat == 0 && tcarat == 0) {
        toastr.warning("Please Enter Carat.");
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

        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
    }
    else {
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
        toastr.warning("Carat is already exist.");
    }
    //SetSearchParameter();
}
function NewSizeGroupRemove(id) {
    $('#' + id).remove();
    var SList = _.reject(SizeGroupList, function (e) { return e.NewID == id });
    SizeGroupList = SList;
    //SetSearchParameter();
}

function setFromCarat() {
    if ($('#txtfromcarat').val() != "") {
        $('#txtfromcarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
        $('#txttocarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
    } else {
        $('#txtfromcarat').val("0");
    }
    if ($('#txttocarat').val() == "") {
        $('#txttocarat').val("0");
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

function GetSearchParameter() {
    loaderShow();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Status != undefined) {
                ParameterList = data.Data;
                if (ParameterList.length > 0) {
                    _.each(ParameterList, function (itm) {
                        itm.ACTIVE = false;
                    });
                }
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

                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "BGM", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLE_INCL", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLE_NATTS", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWN_INCL", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWN_NATTS", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLEOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWNOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "PAVILIONOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "GIRDLEOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                }

                $('#searchcaratgen').html("");
                CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
                _(CaratList).each(function (carat, i) {
                    $('#searchcaratgen').append('<li onclick="SetActive(\'carat\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
                });

                //$('#searchshape').html("");
                //ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
                //_(ShapeList).each(function (shape, i) {
                //    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
                //});

                $('#searchshape').html("");
                $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div></a></li>');
                //$('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div><span>ALL</span></a></li>');

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
                    $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
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
                BGMList = _.filter(ParameterList, function (e) { return e.ListType == 'BGM' });
                _(BGMList).each(function (bgm, i) {
                    $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
                });

                $('#searchlab').html("");
                LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
                _(LabList).each(function (lab, i) {
                    $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
                });

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

                loaderHide();
            }
            else {
                window.location = GetLoginUrl();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

var ddlTransObj = null;
function GetTransId() {
    if (ddlTransObj != null) {
        $("#ddlTransId").multiselect('destroy');
    }
    $("#ddlTransId").html("");

    var fDate = $.trim($('#txtFromDate').val());
    var tDate = $.trim($('#txtToDate').val());
    if (fDate != "" && tDate != "") {
        if (new Date(tDate) >= new Date(fDate)) {
            loaderShow();

            var obj = {};
            obj.FromDate = $('#txtFromDate').val();
            obj.ToDate = $('#txtToDate').val();

            $.ajax({
                //url: "/LabStock/GetTransId",
                url: "/LabStock/Lab_GetTransId",
                type: "POST",
                data: obj,
                success: function (data, textStatus, jqXHR) {
                    if (data.Data != null) {
                        if (data.Data.length != 0) {
                            var list = data.Data, tot = list.length, i = 0;
                            for (; i < tot; i++) {
                                //$("#ddlTransId").append("<option value='" + list[i].TransId + "'>" + list[i].TransId + "-" + list[i].OfferName + "</option>");
                                $("#ddlTransId").append("<option value='" + list[i].TransId + "'>" + list[i].TransId + "-" + list[i].OfferName + "</option>");
                            }
                        }
                    }
                    ddlTransObj = $('#ddlTransId').multiselect({
                        includeSelectAllOption: true
                    });
                    loaderHide();
                    //if (data.Msg != undefined) {
                    //    if (data.Msg == 'success') {
                    //        var list = data.Data, tot = list.length, i = 0;

                    //        for (; i < tot; i++) {
                    //            $("#ddlTransId").append("<option value='" + list[i].TransId + "'>" + list[i].TransId + "-" + list[i].OfferName + "</option>");
                    //        }
                    //        ddlTransObj = $('#ddlTransId').multiselect({
                    //            includeSelectAllOption: true
                    //        });
                    //    }
                    //    else {
                    //        MoveToErrorPage(0);
                    //    }
                    //    loaderHide();
                    //}
                    //else {
                    //    window.location = GetLoginUrl();
                    //}
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    loaderHide();
                }
            });
        }
        else {
            toastr.error('To date must be greater than From date');
            ddlTransObj = $('#ddlTransId').multiselect({
                includeSelectAllOption: true
            });
        }
    }
}

function CustomerExcel(isCustomer) {
    var SizeLst = "";
    var CaratType = "";

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

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var cutLst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var symLst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var bgmLst = _.pluck(_.filter(BGMList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tableopen = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crownopen = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var pavopen = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var girdleopen = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    var obj = {};
    obj.FromDate = $("#txtFromDate").val();
    obj.ToDate = $("#txtToDate").val();
    obj.RefNo = $("#txtRefNo").val().replace(/ /g, ',');
    obj.iVendor = $('#ddlTransId').val().join(",");
    obj.sShape = shapeLst;
    obj.sPointer = SizeLst;
    obj.sColorType = Color_Type;

    if (Color_Type == "Fancy") {
        obj.sColor = "";
        obj.sINTENSITY = _.pluck(Check_Color_1, 'Symbol').join(",");
        obj.sOVERTONE = _.pluck(Check_Color_2, 'Symbol').join(",");
        obj.sFANCY_COLOR = _.pluck(Check_Color_3, 'Symbol').join(",");
    }
    else if (Color_Type == "Regular") {
        obj.sColor = colorLst;
        obj.sINTENSITY = "";
        obj.sOVERTONE = "";
        obj.sFANCY_COLOR = "";
    }

    obj.sClarity = clarityLst;
    obj.sCut = cutLst;
    obj.sPolish = polishLst;
    obj.sSymm = symLst;
    obj.sFls = fluoLst;
    obj.sLab = labLst;

    obj.dFromDisc = $('#FromDiscount').val();
    obj.dToDisc = $('#ToDiscount').val();
    obj.dFromTotAmt = $('#FromTotalAmt').val();
    obj.dToTotAmt = $('#ToTotalAmt').val();

    obj.dFromLength = $('#FromLength').val();
    obj.dToLength = $('#ToLength').val();
    obj.dFromWidth = $('#FromWidth').val();
    obj.dToWidth = $('#ToWidth').val();
    obj.dFromDepth = $('#FromDepth').val();
    obj.dToDepth = $('#ToDepth').val();
    obj.dFromDepthPer = $('#FromDepthPer').val();
    obj.dToDepthPer = $('#ToDepthPer').val();
    obj.dFromTablePer = $('#FromTablePer').val();
    obj.dToTablePer = $('#ToTablePer').val();
    obj.dFromCrAng = $('#FromCrAng').val();
    obj.dToCrAng = $('#ToCrAng').val();
    obj.dFromCrHt = $('#FromCrHt').val();
    obj.dToCrHt = $('#ToCrHt').val();
    obj.dFromPavAng = $('#FromPavAng').val();
    obj.dToPavAng = $('#ToPavAng').val();
    obj.dFromPavHt = $('#FromPavHt').val();
    obj.dToPavHt = $('#ToPavHt').val();
    obj.dKeytosymbol = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;
    obj.dCheckKTS = KeyToSymLst_Check;
    obj.dUNCheckKTS = KeyToSymLst_uncheck;
    obj.sBGM = bgmLst;
    obj.sCrownBlack = crwnattsLst;
    obj.sTableBlack = tblnattsLst;
    obj.sCrownWhite = crwincLst;
    obj.sTableWhite = tblincLst;
    obj.sTableOpen = tableopen;
    obj.sCrownOpen = crownopen;
    obj.sPavOpen = pavopen;
    obj.sGirdleOpen = girdleopen;
    obj.Img = $('#SearchImage').hasClass('active') ? "Yes" : "";
    obj.Vdo = $('#SearchVideo').hasClass('active') ? "Yes" : "";
    obj.ExcelType = isCustomer;
    obj.KTSBlank = (KTSBlank == true ? true : "");
    obj.LengthBlank = (LengthBlank == true ? true : "");
    obj.WidthBlank = (WidthBlank == true ? true : "");
    obj.DepthBlank = (DepthBlank == true ? true : "");
    obj.DepthPerBlank = (DepthPerBlank == true ? true : "");
    obj.TablePerBlank = (TablePerBlank == true ? true : "");
    obj.CrAngBlank = (CrAngBlank == true ? true : "");
    obj.CrHtBlank = (CrHtBlank == true ? true : "");
    obj.PavAngBlank = (PavAngBlank == true ? true : "");
    obj.PavHtBlank = (PavHtBlank == true ? true : "");

    loaderShow();
    $.ajax({
        url: "/LabStock/LabSearchExcel",
        type: "POST",
        //async: false,
        data: obj,
        success: function (data, textStatus, jqXHR) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data found.') > -1) {
                toastr.error(data);
            }
            else if (data.indexOf('ExcelFile') > -1) {
                window.location = data;
                //Reset();
            }
            else if (data.indexOf('<!DOCTYPE html>') > -1) {
                window.location = GetLoginUrl();
            }
            else {
                toastr.error(data);
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
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
    //if (flag == "Shape") {
    //    if (_.find(ShapeList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
    //        _.findWhere(ShapeList, { Value: value }).ACTIVE = false;
    //    } else {
    //        _.findWhere(ShapeList, { Value: value }).ACTIVE = true;
    //    }
    //}
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
        if (_.find(BGMList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(BGMList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(BGMList, { Value: value }).ACTIVE = true;
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
    else if (flag == "carat") {
        if (_.find(CaratList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
            _.findWhere(CaratList, { Value: value }).ACTIVE = false;
        } else {
            _.findWhere(CaratList, { Value: value }).ACTIVE = true;
        }

        $(CaratList).each(function (i, res) {
            if (_.find(CaratList, function (num) { return num.Value == res.Value && num.ACTIVE == true; })) {
                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res.Value + '\')"]').addClass('active');
            }
            else {
                $('#searchcaratgen li[onclick="SetActive(\'carat\',\'' + res.Value + '\')"]').removeClass('active');
            }
        });
        $('a[href="#carat2"]').click()
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
    else if (flag == "KTSBlank") {
        if (KTSBlank) {
            KTSBlank = false;
            $("#KTSBlank").removeClass("active");
        } else {
            KTSBlank = true;
            $("#KTSBlank").addClass("active");
        }
    }
    else if (flag == "LengthBlank") {
        if (LengthBlank) {
            LengthBlank = false;
            $("#LengthBlank").removeClass("active");
        } else {
            LengthBlank = true;
            $("#LengthBlank").addClass("active");
        }
    }
    else if (flag == "WidthBlank") {
        if (WidthBlank) {
            WidthBlank = false;
            $("#WidthBlank").removeClass("active");
        } else {
            WidthBlank = true;
            $("#WidthBlank").addClass("active");
        }
    }
    else if (flag == "DepthBlank") {
        if (DepthBlank) {
            DepthBlank = false;
            $("#DepthBlank").removeClass("active");
        } else {
            DepthBlank = true;
            $("#DepthBlank").addClass("active");
        }
    }
    else if (flag == "DepthPerBlank") {
        if (DepthPerBlank) {
            DepthPerBlank = false;
            $("#DepthPerBlank").removeClass("active");
        } else {
            DepthPerBlank = true;
            $("#DepthPerBlank").addClass("active");
        }
    }
    else if (flag == "TablePerBlank") {
        if (TablePerBlank) {
            TablePerBlank = false;
            $("#TablePerBlank").removeClass("active");
        } else {
            TablePerBlank = true;
            $("#TablePerBlank").addClass("active");
        }
    }
    else if (flag == "CrAngBlank") {
        if (CrAngBlank) {
            CrAngBlank = false;
            $("#CrAngBlank").removeClass("active");
        } else {
            CrAngBlank = true;
            $("#CrAngBlank").addClass("active");
        }
    }
    else if (flag == "CrHtBlank") {
        if (CrHtBlank) {
            CrHtBlank = false;
            $("#CrHtBlank").removeClass("active");
        } else {
            CrHtBlank = true;
            $("#CrHtBlank").addClass("active");
        }
    }
    else if (flag == "PavAngBlank") {
        if (PavAngBlank) {
            PavAngBlank = false;
            $("#PavAngBlank").removeClass("active");
        } else {
            PavAngBlank = true;
            $("#PavAngBlank").addClass("active");
        }
    }
    else if (flag == "PavHtBlank") {
        if (PavHtBlank) {
            PavHtBlank = false;
            $("#PavHtBlank").removeClass("active");
        } else {
            PavHtBlank = true;
            $("#PavHtBlank").addClass("active");
        }
    }
}

function Reset() {
    _.map(ShapeList, function (data) { return data.ACTIVE = false; });
    _.map(ColorList, function (data) { return data.ACTIVE = false; });
    _.map(ClarityList, function (data) { return data.ACTIVE = false; });
    _.map(CutList, function (data) { return data.ACTIVE = false; });
    _.map(PolishList, function (data) { return data.ACTIVE = false; });
    _.map(SymList, function (data) { return data.ACTIVE = false; });
    _.map(FlouList, function (data) { return data.ACTIVE = false; });
    _.map(BGMList, function (data) { return data.ACTIVE = false; });

    _.map(LabList, function (data) { return data.ACTIVE = false; });
    _.map(TblInclList, function (data) { return data.ACTIVE = false; });
    _.map(TblNattsList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnInclList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnNattsList, function (data) { return data.ACTIVE = false; });
    _.map(CaratList, function (data) { return data.ACTIVE = false; });

    $('#SearchImage').removeClass('active');
    $('#SearchVideo').removeClass('active');

    $('#li3ex').removeClass('active');
    $('#li3vg').removeClass('active');

    SizeGroupList = [];
    $('#searchcaratspecific').html("");
    $('a[href="#carat1"]').click();
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

    //$('#searchshape').html("");
    //_(ShapeList).each(function (shape, i) {
    //    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
    //});

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
        $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
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
    _(BGMList).each(function (bgm, i) {
        $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
    });

    $('#searchlab').html("");
    _(LabList).each(function (lab, i) {
        $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
    });

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
    $("#txtRefNo").val("");
    $('#txtfromcarat').val("");
    $('#txttocarat').val("");

    $('#FromDiscount').val("");
    $('#ToDiscount').val("");
    $('#txtPrCtsFrom').val("");
    $('#txtPrCtsTo').val("");
    $('#FromTotalAmt').val("");
    $('#ToTotalAmt').val("");
    $('#FromLength').val("");
    $('#ToLength').val("");
    $('#FromWidth').val("");
    $('#ToWidth').val("");
    $('#FromDepth').val("");
    $('#ToDepth').val("");
    $('#FromDepthPer').val("");
    $('#ToDepthPer').val("");
    $('#FromTablePer').val("");
    $('#ToTablePer').val("");
    $('#FromCrAng').val("");
    $('#ToCrAng').val("");
    $('#FromCrHt').val("");
    $('#ToCrHt').val("");
    $('#FromPavAng').val("");
    $('#ToPavAng').val("");
    $('#FromPavHt').val("");
    $('#ToPavHt').val("");

    resetKeytoSymbol();

    KTSBlank = false;
    $("#KTSBlank").removeClass("active");
    LengthBlank = false;
    $("#LengthBlank").removeClass("active");
    WidthBlank = false;
    $("#WidthBlank").removeClass("active");
    DepthBlank = false;
    $("#DepthBlank").removeClass("active");
    DepthPerBlank = false;
    $("#DepthPerBlank").removeClass("active");
    TablePerBlank = false;
    $("#TablePerBlank").removeClass("active");
    CrAngBlank = false;
    $("#CrAngBlank").removeClass("active");
    CrHtBlank = false;
    $("#CrHtBlank").removeClass("active");
    PavAngBlank = false;
    $("#PavAngBlank").removeClass("active");
    PavHtBlank = false;
    $("#PavHtBlank").removeClass("active");

    $('#txtFromDate').val("");
    $('#txtToDate').val("");
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
    }).on('change', function () {
        GetTransId();
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
        maxYear: parseInt(moment().format('YYYY'), 10),

    }).on('change', function () {
        GetTransId();
    });
    Color_Hide_Show('3');
    resetINTENSITY();
    resetOVERTONE();
    resetFANCY_COLOR();
    $(".carat-dropdown-main").hide();
    FancyDDLHide();
}

function setToFixed(obj) {
    if ($(obj).val() != "") {
        $(obj).val(parseFloat($(obj).val()).toFixed(2));
    }
}
function numvalid(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function ddl_close() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
    $("#sym-sec0 .carat-dropdown-main").hide();
    C1 = 0, KTS = 0, C2 = 0, C3 = 0;
    return;
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
function BindKeyToSymbolList() {
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
                    });
                    $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
                }
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
    setTimeout(function () {
        $("#sym-sec0 .carat-dropdown-main").show();
        KTS = 0;
        return;
    }, 2);
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
    setTimeout(function () {
        $("#sym-sec0 .carat-dropdown-main").show();
        KTS = 0;
        return;
    }, 2);
}
function CommaSeperated_list(e) {
    var data = document.getElementById("txtRefNo").value;
    var lines = data.split(' ');
    document.getElementById("txtRefNo").value = lines.join(',');
    Stone_List = document.getElementById("txtRefNo").value;
    //var key = e.which;
    //if (key == 13)  // the enter key code
    //{
    //    alert("enter")
    //} else {
    //    return false;
    //}
}
var LeaveTextBox = function (ele, fromid, toid) {
    $("#" + fromid).val($("#" + fromid).val() == "" ? "0.00" : $("#" + fromid).val() == undefined ? "0.00" : parseFloat($("#" + fromid).val()).toFixed(2));
    $("#" + toid).val($("#" + toid).val() == "" ? "0.00" : $("#" + toid).val() == undefined ? "0.00" : parseFloat($("#" + toid).val()).toFixed(2));

    var fromvalue = parseFloat($("#" + fromid).val()).toFixed(2) == "" ? 0 : parseFloat($("#" + fromid).val()).toFixed(2);
    var tovalue = parseFloat($("#" + toid).val()).toFixed(2) == "" ? 0 : parseFloat($("#" + toid).val()).toFixed(2);
    if (ele == "FROM") {
        if (parseFloat(parseFloat(fromvalue).toFixed(2)) > parseFloat(parseFloat(tovalue).toFixed(2))) {
            $("#" + toid).val(fromvalue);
            if (fromvalue == 0) {
                $("#" + fromid).val("");
                $("#" + toid).val("");
            }
        }
    }
    else if (ele == "TO") {
        if (parseFloat(parseFloat(tovalue).toFixed(2)) < parseFloat(parseFloat(fromvalue).toFixed(2))) {
            $("#" + fromid).val($("#" + toid).val());
            if (tovalue == 0) {
                $("#" + fromid).val("");
                $("#" + toid).val("");
            }
        }
    }
    if (parseFloat(parseFloat($("#" + fromid).val())) == "0" && parseFloat(parseFloat($("#" + toid).val())) == "0") {
        $("#" + fromid).val("");
        $("#" + toid).val("");
    }
}
function SetModifyParameter() {
    $("#divFilter").show();
    $("#divGridView").hide();
}
function Search(type) {
    $("#divFilter").hide();
    $("#divGridView").show();
    if (type == "Customer") {
        //$(".sup").hide();
        isCustomer = true;
        CustomerColumn();
    }
    else if (type == "Supplier") {
        //$(".sup").show();
        isCustomer = false;
        SupplierColumn();
    }
    GetHoldDataGrid();
}
function CustomerColumn() {
    columnDefs = [
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
}
function SupplierColumn() {
    columnDefs = [
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
    ]
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
var GridpgSize = 800;
var gridOptions = {};
var gridDiv = document.querySelector('#myGrid');
var columnDefs = [];
var rowData = [];
var searchSummary = [];
var showEntryVar = "";
//var showEntryHtml = '<div class="show_entry show_entry1"><label>'
//    + 'Show <select id="ddlPagesize" name="ddlPagesize">'
//    + '<option value="100">100</option>'
//    + '<option value="500">500</option>'
//    + '<option value="800">800</option>'
//    + '</select> entries'
//    + '</label>'
//    + '</div>';
var showEntryHtml = '';
new WOW().init();
function onPageSizeChanged(value) {
    setTimeout(function () {
        //var value = $("#ddlPagesize").val();
        GridpgSize = Number(value);
        GetHoldDataGrid();
    }, 500);
}
function contentHeight() {
    var winH = $(window).height(),
        contentHei = winH - 170;
    contentHei = (contentHei <= 100 ? 450 : contentHei);
    $("#myGrid").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function GetHoldDataGrid() {
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
    }
}
var orderBy = "";
const datasource1 = {
    getRows(params) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = '';
        }

        var PointerSizeLst = "";
        var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true });
        if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
            NewSizeGroup();
        }
        if (SizeGroupList.length != 0) {
            PointerSizeLst = _.pluck(SizeGroupList, 'Size').join(",");
        }
        if (lst.length != 0) {
            PointerSizeLst = _.pluck(_.filter(CaratList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        }

        var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var cutLst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var symLst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var bgmLst = _.pluck(_.filter(BGMList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

        var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

        var tableopen = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var crownopen = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var pavopen = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
        var girdleopen = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

        var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
        var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

        var obj = {};

        obj.PgNo = PageNo;
        obj.PgSize = GridpgSize;
        obj.OrderBy = orderBy;

        obj.FromDate = $("#txtFromDate").val();
        obj.ToDate = $("#txtToDate").val();
        obj.RefNo = $("#txtRefNo").val().replace(/ /g, ',');
        obj.iVendor = $('#ddlTransId').val().join(",");
        obj.sShape = shapeLst;
        obj.sPointer = PointerSizeLst;
        obj.sColorType = Color_Type;

        if (Color_Type == "Fancy") {
            obj.sColor = "";
            obj.sINTENSITY = _.pluck(Check_Color_1, 'Symbol').join(",");
            obj.sOVERTONE = _.pluck(Check_Color_2, 'Symbol').join(",");
            obj.sFANCY_COLOR = _.pluck(Check_Color_3, 'Symbol').join(",");
        }
        else if (Color_Type == "Regular") {
            obj.sColor = colorLst;
            obj.sINTENSITY = "";
            obj.sOVERTONE = "";
            obj.sFANCY_COLOR = "";
        }

        obj.sClarity = clarityLst;
        obj.sCut = cutLst;
        obj.sPolish = polishLst;
        obj.sSymm = symLst;
        obj.sFls = fluoLst;
        obj.sLab = labLst;

        obj.dFromDisc = $('#FromDiscount').val();
        obj.dToDisc = $('#ToDiscount').val();
        obj.dFromTotAmt = $('#FromTotalAmt').val();
        obj.dToTotAmt = $('#ToTotalAmt').val();

        obj.dFromLength = $('#FromLength').val();
        obj.dToLength = $('#ToLength').val();
        obj.dFromWidth = $('#FromWidth').val();
        obj.dToWidth = $('#ToWidth').val();
        obj.dFromDepth = $('#FromDepth').val();
        obj.dToDepth = $('#ToDepth').val();
        obj.dFromDepthPer = $('#FromDepthPer').val();
        obj.dToDepthPer = $('#ToDepthPer').val();
        obj.dFromTablePer = $('#FromTablePer').val();
        obj.dToTablePer = $('#ToTablePer').val();
        obj.dFromCrAng = $('#FromCrAng').val();
        obj.dToCrAng = $('#ToCrAng').val();
        obj.dFromCrHt = $('#FromCrHt').val();
        obj.dToCrHt = $('#ToCrHt').val();
        obj.dFromPavAng = $('#FromPavAng').val();
        obj.dToPavAng = $('#ToPavAng').val();
        obj.dFromPavHt = $('#FromPavHt').val();
        obj.dToPavHt = $('#ToPavHt').val();
        obj.dKeytosymbol = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;
        obj.dCheckKTS = KeyToSymLst_Check;
        obj.dUNCheckKTS = KeyToSymLst_uncheck;
        obj.sBGM = bgmLst;
        obj.sCrownBlack = crwnattsLst;
        obj.sTableBlack = tblnattsLst;
        obj.sCrownWhite = crwincLst;
        obj.sTableWhite = tblincLst;
        obj.sTableOpen = tableopen;
        obj.sCrownOpen = crownopen;
        obj.sPavOpen = pavopen;
        obj.sGirdleOpen = girdleopen;
        obj.Img = $('#SearchImage').hasClass('active') ? "Yes" : "";
        obj.Vdo = $('#SearchVideo').hasClass('active') ? "Yes" : "";
        obj.KTSBlank = (KTSBlank == true ? true : "");
        obj.LengthBlank = (LengthBlank == true ? true : "");
        obj.WidthBlank = (WidthBlank == true ? true : "");
        obj.DepthBlank = (DepthBlank == true ? true : "");
        obj.DepthPerBlank = (DepthPerBlank == true ? true : "");
        obj.TablePerBlank = (TablePerBlank == true ? true : "");
        obj.CrAngBlank = (CrAngBlank == true ? true : "");
        obj.CrHtBlank = (CrHtBlank == true ? true : "");
        obj.PavAngBlank = (PavAngBlank == true ? true : "");
        obj.PavHtBlank = (PavHtBlank == true ? true : "");
        $.ajax({
            url: "/LabStock/LabSearchGrid",
            type: "POST",
            data: obj,
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

                    //$('#tab1SupplierCostDiscPer').html(formatNumber(searchSummary.AVG_OFFER_DISC_PER));
                    //$('#tab1SupplierCostValue').html(formatNumber(searchSummary.TOT_OFFER_DISC_VALUE));

                    $('#tab1SunriseDiscPer').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    $('#tab1SunriseValueUSDoller').html(formatNumber(searchSummary.TOT_NET_AMOUNT));

                    //$('#tab1SuppBaseOfferPer').html(formatNumber(searchSummary.AVG_SUPP_BASE_OFFER_PER));
                    //$('#tab1SuppBaseOfferValue').html(formatNumber(searchSummary.TOT_SUPP_BASE_VALUE));

                } else {
                    if (data.Data.length == 0) {
                        $("#divFilter").show();
                        $("#divGridView").hide();
                        toastr.error("No data found.");
                    }
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1TCount').hide();
                    $('#tab1Pcs').html('0');
                    $('#tab1CTS').html('0');
                    $('#tab1RapValue').html('0');

                    //$('#tab1SupplierCostDiscPer').html('0');
                    //$('#tab1SupplierCostValue').html('0');

                    $('#tab1SunriseDiscPer').html('0');
                    $('#tab1SunriseValueUSDoller').html('0');

                    //$('#tab1SuppBaseOfferPer').html('0');
                    //$('#tab1SuppBaseOfferValue').html('0');

                } 
                if ($('#myGrid .ag-paging-panel').length > 0) {
                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $(function () {
                        $("#ddlPagesize").change(function () {
                            onPageSizeChanged($('option:selected', this).text());
                        });
                    });
                    clearInterval(showEntryVar);
                    $('#ddlPagesize').val(GridpgSize);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();

                contentHeight();
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
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
function Download() {
    $('#ExcelModal').modal('show');
}
function DownloadOK() {
    DownloadExcel();
}
function DownloadExcel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    var obj = {};
    $('#ExcelModal').modal('hide');
    if ($('#customRadio3').prop('checked')) {
        obj.RefNo = $("#txtRefNo").val().replace(/ /g, ',');
    }
    else {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            var REF_NO = _.pluck(gridOptions.api.getSelectedRows(), 'REF_NO').join(",");
            obj.RefNo = REF_NO;
        } else {
            toastr.warning("No stone selected for download as a excel !");
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            return;
        }
    }
    
    var PointerSizeLst = "";
    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true });
    if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
        NewSizeGroup();
    }
    if (SizeGroupList.length != 0) {
        PointerSizeLst = _.pluck(SizeGroupList, 'Size').join(",");
    }
    if (lst.length != 0) {
        PointerSizeLst = _.pluck(_.filter(CaratList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    }

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var cutLst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var symLst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var bgmLst = _.pluck(_.filter(BGMList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tableopen = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crownopen = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var pavopen = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var girdleopen = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    obj.PgNo = 1;
    obj.PgSize = 10;
    obj.OrderBy = "";
    obj.FromDate = $("#txtFromDate").val();
    obj.ToDate = $("#txtToDate").val();
    obj.iVendor = $('#ddlTransId').val().join(",");
    obj.sShape = shapeLst;
    obj.sPointer = PointerSizeLst;
    obj.sColorType = Color_Type;

    if (Color_Type == "Fancy") {
        obj.sColor = "";
        obj.sINTENSITY = _.pluck(Check_Color_1, 'Symbol').join(",");
        obj.sOVERTONE = _.pluck(Check_Color_2, 'Symbol').join(",");
        obj.sFANCY_COLOR = _.pluck(Check_Color_3, 'Symbol').join(",");
    }
    else if (Color_Type == "Regular") {
        obj.sColor = colorLst;
        obj.sINTENSITY = "";
        obj.sOVERTONE = "";
        obj.sFANCY_COLOR = "";
    }

    obj.sClarity = clarityLst;
    obj.sCut = cutLst;
    obj.sPolish = polishLst;
    obj.sSymm = symLst;
    obj.sFls = fluoLst;
    obj.sLab = labLst;

    obj.dFromDisc = $('#FromDiscount').val();
    obj.dToDisc = $('#ToDiscount').val();
    obj.dFromTotAmt = $('#FromTotalAmt').val();
    obj.dToTotAmt = $('#ToTotalAmt').val();

    obj.dFromLength = $('#FromLength').val();
    obj.dToLength = $('#ToLength').val();
    obj.dFromWidth = $('#FromWidth').val();
    obj.dToWidth = $('#ToWidth').val();
    obj.dFromDepth = $('#FromDepth').val();
    obj.dToDepth = $('#ToDepth').val();
    obj.dFromDepthPer = $('#FromDepthPer').val();
    obj.dToDepthPer = $('#ToDepthPer').val();
    obj.dFromTablePer = $('#FromTablePer').val();
    obj.dToTablePer = $('#ToTablePer').val();
    obj.dFromCrAng = $('#FromCrAng').val();
    obj.dToCrAng = $('#ToCrAng').val();
    obj.dFromCrHt = $('#FromCrHt').val();
    obj.dToCrHt = $('#ToCrHt').val();
    obj.dFromPavAng = $('#FromPavAng').val();
    obj.dToPavAng = $('#ToPavAng').val();
    obj.dFromPavHt = $('#FromPavHt').val();
    obj.dToPavHt = $('#ToPavHt').val();
    obj.dKeytosymbol = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;
    obj.dCheckKTS = KeyToSymLst_Check;
    obj.dUNCheckKTS = KeyToSymLst_uncheck;
    obj.sBGM = bgmLst;
    obj.sCrownBlack = crwnattsLst;
    obj.sTableBlack = tblnattsLst;
    obj.sCrownWhite = crwincLst;
    obj.sTableWhite = tblincLst;
    obj.sTableOpen = tableopen;
    obj.sCrownOpen = crownopen;
    obj.sPavOpen = pavopen;
    obj.sGirdleOpen = girdleopen;
    obj.Img = $('#SearchImage').hasClass('active') ? "Yes" : "";
    obj.Vdo = $('#SearchVideo').hasClass('active') ? "Yes" : "";
    obj.KTSBlank = (KTSBlank == true ? true : "");
    obj.LengthBlank = (LengthBlank == true ? true : "");
    obj.WidthBlank = (WidthBlank == true ? true : "");
    obj.DepthBlank = (DepthBlank == true ? true : "");
    obj.DepthPerBlank = (DepthPerBlank == true ? true : "");
    obj.TablePerBlank = (TablePerBlank == true ? true : "");
    obj.CrAngBlank = (CrAngBlank == true ? true : "");
    obj.CrHtBlank = (CrHtBlank == true ? true : "");
    obj.PavAngBlank = (PavAngBlank == true ? true : "");
    obj.PavHtBlank = (PavHtBlank == true ? true : "");
    obj.ExcelType = (isCustomer == true ? 2 : 3);
    
    $.ajax({
        url: "/LabStock/LabSearchGridExcel",
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data found.') > -1) {
                toastr.error(data);
            }
            else if (data.indexOf('ExcelFile') > -1) {
                window.location = data;
            }
            else {
                toastr.error(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
var ClearRemoveModel = function () {
    $("#ByRequestAreyousure_Modal").modal("hide");
}
//function ByRequestModal() {
//    var count = 0; count = rowData.length;
//    if (_.pluck(gridOptions.api.getSelectedRows(), 'REF_NO') != "" && count != 0) {
//        $("#ByRequestAreyousure_Modal").modal("show");
//    }
//    else {
//        toastr.warning("No Stone selected for By Request");
//    }
//}
function ByRequestCart() {
    var count = 0; count = rowData.length;
    if (_.pluck(gridOptions.api.getSelectedRows(), 'REF_NO') != "" && count != 0) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        //$("#ByRequestAreyousure_Modal").modal("hide");

        setTimeout(function () {
            var REFNO_List = _.pluck(gridOptions.api.getSelectedRows(), 'REF_NO').join(",");
            var obj = {};
            obj.REF_NO = REFNO_List;

            debugger
            $.ajax({
                url: "/LabStock/ByRequest_Cart",
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
                            GetHoldDataGrid();
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
        //$("#ByRequestAreyousure_Modal").modal("hide");
        toastr.warning("No Stone selected for By Cart");
    }
}
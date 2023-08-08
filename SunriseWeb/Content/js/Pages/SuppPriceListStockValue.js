var SupplierList = [];
var PointerList = [];
var GoodsTypeList = [];
var LocationList = [];
var ShapeList = [];
var CaratList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var PolishList = [];
var SymList = [];
var FlsList = [];
var LabList = [];
var KTSList = [];
var BGMList = [];
var CrownBlackList = [];
var TableBlackList = [];
var CrownWhiteList = [];
var TableWhiteList = [];
var CheckedSupplierValue = "";
var CheckedPointerValue = "";
var CheckedGoodsTypeValue = "";
var CheckedLocationValue = "";
var CheckedShapeValue = "";
var CheckedColorValue = "";
var CheckedClarityValue = "";
var CheckedCutValue = "";
var CheckedPolValue = "";
var CheckedSymValue = "";
var CheckedLabValue = "";
var CheckedCaratValue = "";
var CheckedFLsValue = "";
var CheckedBgmValue = "";
var CheckedCrnBlackValue = "";
var CheckedTblBlackValue = "";
var CheckedCrnWhiteValue = "";
var CheckedTblWhiteValue = "";
var ColumnList = [];
var _pointerlst = [];

var SSN_CARAT = [];
var CheckedCaratValue = '';
var CaratFrom = '';
var CaratTo = '';
var Color = '';
var IsCaratFT = true
var CARAT = false;
var FromSize1 = "";
var ToSize1 = "";
var FromSize2 = "";
var ToSize2 = "";
var FromSize3 = "";
var ToSize3 = "";
var FromSize4 = "";
var ToSize4 = "";
var FromSize5 = "";
var ToSize5 = "";
var FromSize6 = "";
var ToSize6 = "";
var FromSize7 = "";
var ToSize7 = "";
var FromSize8 = "";
var ToSize8 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize10 = "";
var ToSize10 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize12 = "";
var ToSize12 = "";
var FromSize13 = "";
var ToSize13 = "";
var FromSize14 = "";
var ToSize14 = "";
var FromSize15 = "";
var ToSize15 = "";
var FromSize16 = "";
var ToSize16 = "";
var FromSize17 = "";
var ToSize17 = "";
var FromSize18 = "";
var ToSize18 = "";

var CARAT_Size2 = false;
var CARAT_Size3 = false;
var CARAT_Size4 = false;
var CARAT_Size5 = false;
var CARAT_Size6 = false;
var CARAT_Size7 = false;
var CARAT_Size8 = false;
var CARAT_Size9 = false;
var CARAT_Size10 = false;
var CARAT_Size11 = false;
var CARAT_Size12 = false;
var CARAT_Size13 = false;
var CARAT_Size14 = false;
var CARAT_Size15 = false;
var CARAT_Size16 = false;
var CARAT_Size17 = false;
var CARAT_Size18 = false;
var Carat = "";
var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];
var Regular = true, Fancy = false;
var Regular_All = false, Fancy_All = false;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var Check_Color_1 = [], Check_Color_2 = [], Check_Color_3 = [];
var FC = "";

$(document).ready(function () {
    Reset_API_Filter();
    Get_API_StockFilter();
    BindKeyToSymbolList();

    $("#tblCustomerFilters").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tblCustomerFilters #tblBodyCustomerFilters").find('tr').length) == 0) {
            $("#lblCustNoFound").show();
            $("#tblCustomerFilters").hide();
        }
        else {
            $("#lblCustNoFound").hide();
            $("#tblCustomerFilters").show();
        }
        var idd = 1;
        $("#tblCustomerFilters #tblBodyCustomerFilters tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });

    $("#tblCostFilters").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tblCostFilters #tblBodyCostFilters").find('tr').length) == 0) {
            $("#lblCostNoFound").show();
            $("#tblCostFilters").hide();
        }
        else {
            $("#lblCostNoFound").hide();
            $("#tblCostFilters").show();
        }
        var idd = 1;
        $("#tblCostFilters #tblBodyCostFilters tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });
    Bind_RColor();
    FcolorBind();
    $('#ColorModal').on('show.bs.modal', function (event) {
        color_ddl_close();
    });
    $('#chk_Regular_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Regular_All = $(this).is(':checked');
        SetSearchParameter();
    });
    $('#chk_Fancy_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Fancy_All = $(this).is(':checked');
        SetSearchParameter();
    });
});

function Get_API_StockFilter() {
    $("#loading").css("display", "block");
    $.ajax({
        //url: "/Supplier/Get_API_StockFilter",
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            debugger
            if (data.Status == "1" && data.Message == "SUCCESS") {debugger
                $.each(data.Data, function (i, item) {
                    if (item.ListType == "BGM") { BGMList.push(item); }
                    if (item.ListType == "CLARITY") { ClarityList.push(item); }
                    if (item.ListType == "COLOR") { ColorList.push(item); }
                    if (item.ListType == "CUT") { CutList.push(item); }
                    if (item.ListType == "FLS") { FlsList.push(item); }
                    if (item.ListType == "LAB") { LabList.push(item); }
                    if (item.ListType == "LOCATION") { LocationList.push(item); }
                    if (item.ListType == "POINTER") { PointerList.push(item); }
                    if (item.ListType == "POLISH") { PolishList.push(item); }
                    if (item.ListType == "SHAPE") { ShapeList.push(item); }
                    if (item.ListType == "SYMM") { SymList.push(item); }
                    if (item.ListType == "CROWN_NATTS") { CrownBlackList.push(item); }
                    if (item.ListType == "CROWN_INCL") { CrownWhiteList.push(item); }
                    if (item.ListType == "TABLE_NATTS") { TableBlackList.push(item); }
                    if (item.ListType == "TABLE_INCL") { TableWhiteList.push(item); }


                    //if (item.ListType == "Goods Type") { GoodsTypeList.push(item); }
                    //if (item.ListType == "Supplier") { SupplierList.push(item); }
                });

                LocationList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'LOCATION', isActive: false });
                //SupplierList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'Supplier', isActive: false });
                //GoodsTypeList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'Goods Type', isActive: false });
                ShapeList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'SHAPE', isActive: false });
                ColorList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'COLOR', isActive: false });
                ClarityList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'CLARITY', isActive: false });
                CutList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'CUT', isActive: false });
                PolishList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'POLISH', isActive: false });
                SymList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'SYMM', isActive: false });
                FlsList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'FLS', isActive: false });
                LabList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'LAB', isActive: false });
                BGMList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'BGM', isActive: false });
                CrownBlackList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'CROWN_NATTS', isActive: false });
                CrownWhiteList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'CROWN_INCL', isActive: false });
                TableBlackList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'TABLE_NATTS', isActive: false });
                TableWhiteList.unshift({ Id: 500, Value: 'ALL', SORT_NO: 0, ListType: 'TABLE_INCL', isActive: false });

                INTENSITY = ['W-X', 'Y-Z', 'FAINT', 'VERY LIGHT', 'LIGHT', 'FANCY LIGHT', 'FANCY', 'FANCY INTENSE', 'FANCY GREEN', 'FANCY DARK', 'FANCY DEEP', 'FANCY VIVID', 'FANCY FAINT', 'DARK'];
                OVERTONE = ['NONE', 'BROWNISH', 'GRAYISH', 'GREENISH', 'YELLOWISH', 'PINKISH', 'ORANGEY', 'BLUISH', 'REDDISH', 'PURPLISH'];
                FANCY_COLOR = ['YELLOW', 'PINK', 'RED', 'GREEN', 'ORANGE', 'VIOLET', 'BROWN', 'GRAY', 'BLUE', 'PURPLE'];

                INTENSITY.sort();
                OVERTONE.sort();
                FANCY_COLOR.sort();
                INTENSITY.unshift("ALL SELECTED");
                OVERTONE.unshift("ALL SELECTED");
                FANCY_COLOR.unshift("ALL SELECTED");
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
var ModalShow = function (ParameterLabel, ObjLst) {
    if (ParameterLabel == "CrownBlack")
        $('#exampleModalLabel').text("Crown Black");
    else if (ParameterLabel == "TableBlack")
        $('#exampleModalLabel').text("Table Black");
    else if (ParameterLabel == "CrownWhite")
        $('#exampleModalLabel').text("Crown White");
    else if (ParameterLabel == "TableWhite")
        $('#exampleModalLabel').text("Table White");
    else if (ParameterLabel == "GoodsType")
        $('#exampleModalLabel').text("Goods Type");
    else
        $('#exampleModalLabel').text(ParameterLabel);

    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var c = 0, IsAllActiveC = 0;
    var list = [];
    list = ObjLst;
    list.forEach(function (item) {
        content += '<li id="li_' + ParameterLabel + '_' + (ParameterLabel == "Supplier" ? c : item.Id) + '" onclick="ItemClicked(\'' + ParameterLabel + '\',\'' + item.Value + '\',\'' + (ParameterLabel == "Supplier" ? c : item.Id) + '\', this);" class="';
        if (item.isActive) {
            content += 'active';

            if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
                || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
                || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
                || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
                || ParameterLabel == "GoodsType") {
                IsAllActiveC = parseInt(IsAllActiveC) + 1;
            }
        }
        content += '">' + item.Value + '</li>';
        c = parseInt(c) + 1;
    });
    content += '</ul>';
    $('#divModal').empty();
    $('#divModal').append(content);

    $("#mpdal-footer").append('<button type="button" class="btn btn-primary" ng-click="ResetSelectedAttr(' + ParameterLabel + ');">Reset</button><button type="button" class="btn btn-secondary" data-dismiss="modal">Done</button>');

    if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
        || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
        || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
        || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
        || ParameterLabel == "GoodsType") {
        if (IsAllActiveC == ObjLst.length - 1) {
            $("#li_" + ParameterLabel + "_500").addClass('active');
        }
    }
    $('#myModal').modal('toggle');
}
var ResetSelectedAttr = function (attr, obj) {
    _.each(obj, function (itm) {
        itm.isActive = false;
    });
    $(attr).empty();
}
var ResetCheckCarat = function () {
    _pointerlst = [];
    $(".divCheckedCaratValue").empty();
    $(".divCheckedPointerValue").empty();
}
function PricingMethodDDL() {
    if ($("#PricingMethod").val() == "") {
        document.getElementById("txtValue").disabled = true;
        document.getElementById("txtDisc").disabled = true;
        $("#txtValue").show();
        $("#txtDisc").hide();
        $("#txtValue").val("");
    }
    else {
        if ($("#PricingMethod").val() == "Disc") {
            document.getElementById("txtDisc").disabled = false;
            $("#txtDisc").show();
            $("#txtValue").hide();
            $("#txtDisc").val("");
        }
        if ($("#PricingMethod").val() == "Value") {
            document.getElementById("txtValue").disabled = false;
            $("#txtValue").show();
            $("#txtDisc").hide();
            $("#txtValue").val("");
        }
    }
    $("#lblPMErr").html("");
    $("#lblPMErr").hide();
}
var ItemClicked = function (ParameterLabel, item, c, curritem) {
    var list = [];
    if (ParameterLabel == 'Supplier') {
        list = SupplierList;
    }
    if (ParameterLabel == 'Carat') {
        list = PointerList;
    }
    if (ParameterLabel == 'GoodsType') {
        list = GoodsTypeList;
    }
    if (ParameterLabel == 'Location') {
        list = LocationList;
    }
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Cut') {
        list = CutList;
    }
    if (ParameterLabel == 'Polish') {
        list = PolishList;
    }
    if (ParameterLabel == 'Sym') {
        list = SymList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
    if (ParameterLabel == 'Fls') {
        list = FlsList;
    }
    if (ParameterLabel == 'BGM') {
        list = BGMList;
    }
    if (ParameterLabel == 'CrownBlack') {
        list = CrownBlackList;
    }
    if (ParameterLabel == 'TableBlack') {
        list = TableBlackList;
    }
    if (ParameterLabel == 'CrownWhite') {
        list = CrownWhiteList;
    }
    if (ParameterLabel == 'TableWhite') {
        list = TableWhiteList;
    }
    if (item == "ALL") {
        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
            || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
            || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
            || ParameterLabel == "GoodsType") {
            if (ParameterLabel == "Color" && item == "ALL" && $("#li_" + ParameterLabel + "_500").hasClass("active") == true) {
                R_F_All_Only_Checkbox_Clr_Rst("1");
            }
            else {
                for (var j = 0; j <= list.length - 1; j++) {
                    if (list[j].Value != "ALL") {
                        var itm = _.find(list, function (i) {
                            return i.Value == list[j].Value
                        });
                        if ($("#li_" + ParameterLabel + "_500").hasClass("active")) {
                            itm.isActive = true;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].Id)).addClass('active');
                        }
                        else {
                            itm.isActive = false;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].Id)).removeClass('active');
                        }
                        //itm.isActive = !itm.isActive;
                    }
                    else {
                        $("#li_" + ParameterLabel + "_500").toggleClass('active');
                    }
                }
            }
        }
        else if (ParameterLabel == "Carat") {
            _pointerlst = PointerList;
            $(".divCheckedPointerValue").empty();
            for (var j = 0; j <= list.length - 1; j++) {
                list[j].isActive = true;
                $('.divCheckedPointerValue').append('<li id="C_' + list[j].Id + '" class="carat-li-top allcrt">' + list[j].Value + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + list[j].Id + ');"></i></li>');
            }
        }
    }
    else {
        if (ParameterLabel == "Color") {
            R_F_All_Only_Checkbox_Clr_Rst("-0");
        }
        var itm = _.find(list, function (i) { return i.Value == item });
        if ($("#li_" + ParameterLabel + "_" + c).hasClass("active")) {
            itm.isActive = false;
            $("#li_" + ParameterLabel + "_" + c).removeClass('active');
        }
        else {
            itm.isActive = true;
            $("#li_" + ParameterLabel + "_" + c).addClass('active');
        }

        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
            || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
            || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
            || ParameterLabel == "GoodsType") {
            var IsAllActiveC = 0;
            for (var j = 0; j <= list.length - 1; j++) {
                if (list[j].Value != "ALL") {
                    if (list[j].isActive == true) {
                        IsAllActiveC = parseInt(IsAllActiveC) + 1;
                    }
                }
            }
            if (IsAllActiveC == list.length - 1) {
                $("#li_" + ParameterLabel + "_500").addClass('active');
            }
            else {
                $("#li_" + ParameterLabel + "_500").removeClass('active');
            }
        }
        //$(curritem).toggleClass('active');
    }
    SetSearchParameter();
}
var SetSearchParameter = function () {
    var goodstypeLst = _.pluck(_.filter(GoodsTypeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var supplierLst = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
    var pointerLst = _.pluck(_.filter(PointerList, function (e) { return e.isActive == true }), 'Value').join(",");
    var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");
    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
    var cutlst = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Pollst = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Symlst = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
    var flslst = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
    var bgmlst = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnblacklst = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblblacklst = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnwhitelst = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblwhitelst = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");

    CheckedSupplierValue = supplierLst;
    CheckedPointerValue = pointerLst;
    CheckedGoodsTypeValue = goodstypeLst;
    CheckedLocationValue = locationLst;
    CheckedShapeValue = shapeLst;
    CheckedColorValue = colorLst;
    CheckedClarityValue = clarityLst;
    CheckedCutValue = cutlst;
    CheckedPolValue = Pollst;
    CheckedSymValue = Symlst;
    CheckedLabValue = labLst;
    CheckedFLsValue = flslst;
    CheckedBgmValue = bgmlst;
    CheckedCrnBlackValue = crnblacklst;
    CheckedTblBlackValue = tblblacklst;
    CheckedCrnWhiteValue = crnwhitelst;
    CheckedTblWhiteValue = tblwhitelst;

    if (CheckedSupplierValue.split(",").length >= 1) {
        $(".divCheckedSupplierValue").empty();
        $(".divCheckedSupplierValue").append(CheckedSupplierValue);
        $(".divCheckedSupplierValue").attr({
            "title": CheckedSupplierValue
        });
    }
    if (CheckedGoodsTypeValue.split(",").length >= 1) {
        $(".divCheckedGoodsTypeValue").empty();
        $(".divCheckedGoodsTypeValue").append(CheckedGoodsTypeValue);
        $(".divCheckedGoodsTypeValue").attr({
            "title": CheckedGoodsTypeValue
        });
    }
    if (CheckedLocationValue.split(",").length >= 1) {
        $(".divCheckedLocationValue").empty();
        $(".divCheckedLocationValue").append(CheckedLocationValue);
        $(".divCheckedLocationValue").attr({
            "title": CheckedLocationValue
        });
    }
    if (CheckedShapeValue.split(",").length >= 1) {
        $(".divCheckedShapeValue").empty();
        $(".divCheckedShapeValue").append(CheckedShapeValue);
        $(".divCheckedShapeValue").attr({
            "title": CheckedShapeValue
        });
    }
    if (CheckedPointerValue.split(",").length >= 1) {
        $(".divCheckedCaratValue").empty();
        $(".divCheckedCaratValue").append(_.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(","));
        $(".divCheckedCaratValue").attr({
            "title": _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",")
        });
    }
    if (CheckedColorValue.split(",").length >= 1) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append(CheckedColorValue);
        $(".divCheckedColorValue").attr({
            "title": CheckedColorValue
        });
    }
    if (CheckedColorValue == "") {
        Set_FancyColor();
    }
    if (Regular_All == true) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append("<b>REGULAR ALL</b>");
        $(".divCheckedColorValue").attr({
            "title": "<b>REGULAR ALL</b>"
        });
    }
    else if (Fancy_All == true) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append("<b>FANCY ALL</b>");
        $(".divCheckedColorValue").attr({
            "title": "<b>FANCY ALL</b>"
        });
    }
    if (CheckedClarityValue.split(",").length >= 1) {
        $(".divCheckedClarityValue").empty();
        $(".divCheckedClarityValue").append(CheckedClarityValue);
        $(".divCheckedClarityValue").attr({
            "title": CheckedClarityValue
        });
    }
    if (CheckedCutValue.split(",").length >= 1) {
        $(".divCheckedCutValue").empty();
        $(".divCheckedCutValue").append(CheckedCutValue);
        $(".divCheckedCutValue").attr({
            "title": CheckedCutValue
        });
    }
    if (CheckedPolValue.split(",").length >= 1) {
        $(".divCheckedPolValue").empty();
        $(".divCheckedPolValue").append(CheckedPolValue);
        $(".divCheckedPolValue").attr({
            "title": CheckedPolValue
        });
    }
    if (CheckedSymValue.split(",").length >= 1) {
        $(".divCheckedSymValue").empty();
        $(".divCheckedSymValue").append(CheckedSymValue);
        $(".divCheckedSymValue").attr({
            "title": CheckedSymValue
        });
    }
    if (CheckedLabValue.split(",").length >= 1) {
        $(".divCheckedLabValue").empty();
        $(".divCheckedLabValue").append(CheckedLabValue);
        $(".divCheckedLabValue").attr({
            "title": CheckedLabValue
        });
    }
    if (CheckedFLsValue.split(",").length >= 1) {
        $(".divCheckedFLsValue").empty();
        $(".divCheckedFLsValue").append(CheckedFLsValue);
        $(".divCheckedFLsValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedBgmValue.split(",").length >= 1) {
        $(".divCheckedBGMValue").empty();
        $(".divCheckedBGMValue").append(CheckedBgmValue);
        $(".divCheckedBGMValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedCrnBlackValue.split(",").length >= 1) {
        $(".divCheckedCrnBlackValue").empty();
        $(".divCheckedCrnBlackValue").append(CheckedCrnBlackValue);
        $(".divCheckedCrnBlackValue").attr({
            "title": CheckedCrnBlackValue
        });
    }
    if (CheckedCrnWhiteValue.split(",").length >= 1) {
        $(".divCheckedCrnWhiteValue").empty();
        $(".divCheckedCrnWhiteValue").append(CheckedCrnWhiteValue);
        $(".divCheckedCrnWhiteValue").attr({
            "title": CheckedCrnWhiteValue
        });
    }
    if (CheckedTblBlackValue.split(",").length >= 1) {
        $(".divCheckedTblBlackValue").empty();
        $(".divCheckedTblBlackValue").append(CheckedTblBlackValue);
        $(".divCheckedTblBlackValue").attr({
            "title": CheckedTblBlackValue
        });
    }
    if (CheckedTblWhiteValue.split(",").length >= 1) {
        $(".divCheckedTblWhiteValue").empty();
        $(".divCheckedTblWhiteValue").append(CheckedTblWhiteValue);
        $(".divCheckedTblWhiteValue").attr({
            "title": CheckedTblWhiteValue
        });
    }
}

var AddNewRow = function (type) {
    if ($("#PricingMethod").val() == undefined || $("#PricingMethod").val() == "") {
        toastr.warning("Please Select Price Method !", { timeOut: 2500 });
        $("#PricingMethod").focus();
        return;
    }
    if ($("#PricingMethod").val() == "Disc") {
        if ($("#txtDisc").val() == undefined || $("#txtDisc").val() == "") {
            toastr.warning("Please Enter Percentage !", { timeOut: 2500 });
            $("#txtDisc").focus();
            return;
        }
    }
    if ($("#PricingMethod").val() == "Value") {
        if ($("#txtValue").val() == undefined || $("#txtValue").val() == "") {
            toastr.warning("Please Enter Percentage !", { timeOut: 2500 });
            $("#txtValue").focus();
            return;
        }
    }
    debugger
    if (type == "Add_in_Customer") {
        $("#btn_Add_in_Customer").attr("disabled", true);
        $("#tblCustomerFilters").show();
    }
    else if (type == "Add_in_Cost") {
        $("#btn_Add_in_Cost").attr("disabled", true);
        $("#tblCostFilters").show();
    }
    else {
        $("#btn_Add_in_Customer_Cost").attr("disabled", true);
        $("#tblCustomerFilters").show();
        $("#tblCostFilters").show();
    }


    var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    var Location = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");
    var GoodsType = _.pluck(_.filter(GoodsTypeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Carat = _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",");
    var Color_Type = (Regular_All == true ? "Regular" : (Fancy_All == true ? "Fancy" : ""));
    var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
    var F_INTENSITY = _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
    var F_OVERTONE = _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
    var F_FANCY_COLOR = _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
    var MixColor = "";
    if (Color != "") {
        MixColor = Color;
    }
    else if (FC != "") {
        MixColor = FC;
    }
    if (Color_Type != "") {
        MixColor = (Color_Type == "Regular" ? "<b>REGULAR ALL</b>" : Color_Type == "Fancy" ? "<b>FANCY ALL</b>" : "");
    }
    var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Sym = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
    var FromLength = $("#FromLength").val() == "" || $("#FromLength").val() == undefined ? "" : $("#FromLength").val();
    var ToLength = $("#ToLength").val() == "" || $("#ToLength").val() == undefined ? "" : $("#ToLength").val();
    var FromWidth = $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined ? "" : $("#FromWidth").val();
    var ToWidth = $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined ? "" : $("#ToWidth").val();
    var FromDepth = $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined ? "" : $("#FromDepth").val();
    var ToDepth = $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined ? "" : $("#ToDepth").val();
    var FromDepthinPer = $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined ? "" : $("#FromDepthPer").val();
    var ToDepthinPer = $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined ? "" : $("#ToDepthPer").val();
    var FromTableinPer = $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined ? "" : $("#FromTablePer").val();
    var ToTableinPer = $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined ? "" : $("#ToTablePer").val();
    var FromCrAng = $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined ? "" : $("#FromCrAng").val();
    var ToCrAng = $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined ? "" : $("#ToCrAng").val();
    var FromCrHt = $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined ? "" : $("#FromCrHt").val();
    var ToCrHt = $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined ? "" : $("#ToCrHt").val();
    var FromPavAng = $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined ? "" : $("#FromPavAng").val();
    var ToPavAng = $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined ? "" : $("#ToPavAng").val();
    var FromPavHt = $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined ? "" : $("#FromPavHt").val();
    var ToPavHt = $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined ? "" : $("#ToPavHt").val();
    var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
    var dCheckKTS = KeyToSymLst_Check1;
    var dUNCheckKTS = KeyToSymLst_uncheck1;
    var BGM = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
    var CrownBlack = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var TableBlack = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var CrownWhite = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var TableWhite = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Image = $('#Img:checked').val();
    var Video = $('#Vdo:checked').val();
    var PriceMethod = $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val();
    var per = "";
    if ($("#PricingMethod").val() == "Disc") {
        per = $("#txtDisc").val();
    }
    if ($("#PricingMethod").val() == "Value") {
        per = $("#txtValue").val();
    }
    var Percentage = per;

    var html = "<tr id='tr'>";
    html += "<th class='Row Fi-Criteria' style=''></th>";
    html += "<td style=''><span class='Fi-Criteria Location'>" + Location + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria GoodsType'>" + GoodsType + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + Color_Type + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + Color + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + F_INTENSITY + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + F_OVERTONE + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + F_FANCY_COLOR + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Image'>" + Image + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Video'>" + Video + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + PriceMethod + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Percentage'>" + Percentage + "</span></td>";
    html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
    html += "</tr>";

    debugger
    if (type == "Add_in_Customer") {
        debugger
        $("#btn_Add_in_Customer").attr("disabled", false);

        $("#tblCustomerFilters #tblBodyCustomerFilters").append(html);
        $("#lblCustNoFound").hide();
        $("#tblCustomerFilters").show();
    }
    else if (type == "Add_in_Cost") {
        debugger
        $("#btn_Add_in_Cost").attr("disabled", false);

        $("#tblCostFilters #tblBodyCostFilters").append(html);
        $("#lblCostNoFound").hide();
        $("#tblCostFilters").show();
    }
    else {
        debugger
        $("#btn_Add_in_Customer_Cost").attr("disabled", false);

        $("#tblCustomerFilters #tblBodyCustomerFilters").append(html);
        $("#lblCustNoFound").hide();
        $("#tblCustomerFilters").show();

        var id1 = 1;
        $("#tblCustomerFilters #tblBodyCustomerFilters tr").each(function () {
            $(this).find("th:eq(0)").html(id1);
            id1 += 1;
        });

        $("#tblCostFilters #tblBodyCostFilters").append(html);
        $("#lblCostNoFound").hide();
        $("#tblCostFilters").show();

        var id2 = 1;
        $("#tblCostFilters #tblBodyCostFilters tr").each(function () {
            $(this).find("th:eq(0)").html(id2);
            id2 += 1;
        });
    }

    Reset_API_Filter();
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
function Reset_API_Filter() {
    ResetSelectedAttr('.divCheckedSupplierValue', SupplierList);
    ResetSelectedAttr('.divCheckedLocationValue', LocationList);
    ResetSelectedAttr('.divCheckedGoodsTypeValue', GoodsTypeList);
    ResetSelectedAttr('.divCheckedShapeValue', ShapeList);
    ResetCheckCarat();
    ResetCheckColors();
    Regular = true;
    Fancy = false;
    $("#Regular").addClass("btn-spn-opt-active");
    $("#Fancy").removeClass("btn-spn-opt-active");
    $("#div_Regular").show();
    $("#div_Fancy").hide();
    ResetSelectedAttr('.divCheckedColorValue', ColorList);
    ResetSelectedAttr('.divCheckedClarityValue', ClarityList);
    ResetSelectedAttr('.divCheckedCutValue', CutList);
    ResetSelectedAttr('.divCheckedPolValue', PolishList);
    ResetSelectedAttr('.divCheckedSymValue', SymList);
    ResetSelectedAttr('.divCheckedFLsValue', FlsList);
    ResetSelectedAttr('.divCheckedLabValue', LabList);
    $("#FromLength").val("");
    $("#ToLength").val("");
    $("#FromWidth").val("");
    $("#ToWidth").val("");
    $("#FromDepth").val("");
    $("#ToDepth").val("");
    $("#FromDepthPer").val("");
    $("#ToDepthPer").val("");
    $("#FromTablePer").val("");
    $("#ToTablePer").val("");
    $("#FromCrAng").val("");
    $("#ToCrAng").val("");
    $("#FromCrHt").val("");
    $("#ToCrHt").val("");
    $("#FromPavAng").val("");
    $("#ToPavAng").val("");
    $("#FromPavHt").val("");
    $("#ToPavHt").val("");
    resetKeytoSymbol();
    ResetSelectedAttr('.divCheckedBGMValue', BGMList);
    ResetSelectedAttr('.divCheckedCrnBlackValue', CrownBlackList);
    ResetSelectedAttr('.divCheckedTblBlackValue', CrownWhiteList);
    ResetSelectedAttr('.divCheckedCrnWhiteValue', TableBlackList);
    ResetSelectedAttr('.divCheckedTblWhiteValue', TableWhiteList);

    $(".IgAll").prop("checked", true);
    $(".VdAll").prop("checked", true);

    $("#PricingMethod").val("")
    $("#txtValue").show();
    $("#txtDisc").hide();
    $("#txtValue").val("");
    document.getElementById("txtValue").disabled = true;
    document.getElementById("txtDisc").disabled = true;

    supplierlst = "";
    locationLst = "";
    shapeLst = "";
    pointerlst = "";
    _pointerlst = [];
    colorLst = "";
    clarityLst = "";
    cutlst = "";
    Pollst = "";
    Symlst = "";
    labLst = "";
    flslst = "";
    bgmlst = "";
    crnblacklst = "";
    tblblacklst = "";
    crnwhitelst = "";
    tblwhitelst = "";

    CheckedSupplierValue = "";
    CheckedLocationValue = "";
    CheckedShapeValue = "";
    CheckedPointerValue = "";
    CheckedColorValue = "";
    CheckedClarityValue = "";
    CheckedCutValue = "";
    CheckedPolValue = "";
    CheckedSymValue = "";
    CheckedLabValue = "";
    CheckedFLsValue = "";
    CheckedBgmValue = "";
    CheckedCrnBlackValue = "";
    CheckedTblBlackValue = "";
    CheckedCrnWhiteValue = "";
    CheckedTblWhiteValue = "";
}
var SaveData = function () {
    debugger
    var List1 = [], List2 = [];

    if (parseInt($("#tblCustomerFilters #tblBodyCustomerFilters").find('tr').length) == 0) {
        debugger
        List1.push({
            SupplierId: $("#DdlSupplierName").val(),
            Location: "",
            GoodsType: "",
            Shape: "",
            Pointer: "",
            ColorType: "",
            Color: "",
            INTENSITY: "",
            OVERTONE: "",
            FANCY_COLOR: "",
            Clarity: "",
            Cut: "",
            Polish: "",
            Symm: "",
            Fls: "",
            Lab: "",
            FromLength: "",
            ToLength: "",
            FromWidth: "",
            ToWidth: "",
            FromDepth: "",
            ToDepth: "",
            FromDepthPer: "",
            ToDepthPer: "",
            FromTablePer: "",
            ToTablePer: "",
            FromCrAng: "",
            ToCrAng: "",
            FromCrHt: "",
            ToCrHt: "",
            FromPavAng: "",
            ToPavAng: "",
            FromPavHt: "",
            ToPavHt: "",
            KeyToSymbol: "",
            CheckKTS: "",
            UNCheckKTS: "",
            BGM: "",
            CrownBlack: "",
            TableBlack: "",
            CrownWhite: "",
            TableWhite: "",
            Img: "",
            Vdo: "",
            PriceMethod: "",
            PricePer: ""
        });
    }
    else {
        debugger
        $("#tblCustomerFilters #tblBodyCustomerFilters tr").each(function () {
            var Index = $(this).index();
            var Location = $(this).find('.Location').html();
            var GoodsType = $(this).find('.GoodsType').html();
            var Shape = $(this).find('.Shape').html();
            var Carat = $(this).find('.Carat').html();
            var ColorType = $(this).find('.ColorType').html();
            var Color = $(this).find('.Color').html();
            var INTENSITY = $(this).find('.dCheckINTENSITY').html();
            var OVERTONE = $(this).find('.dCheckOVERTONE').html();
            var FANCY_COLOR = $(this).find('.dCheckFANCY_COLOR').html();
            var Clarity = $(this).find('.Clarity').html();
            var Cut = $(this).find('.Cut').html();
            var Polish = $(this).find('.Polish').html();
            var Sym = $(this).find('.Sym').html();
            var Fls = $(this).find('.Fls').html();
            var Lab = $(this).find('.Lab').html();
            var FromLength = $(this).find('.FromLength').html();
            var ToLength = $(this).find('.ToLength').html();
            var FromWidth = $(this).find('.FromWidth').html();
            var ToWidth = $(this).find('.ToWidth').html();
            var FromDepth = $(this).find('.FromDepth').html();
            var ToDepth = $(this).find('.ToDepth').html();
            var FromDepthinPer = $(this).find('.FromDepthinPer').html();
            var ToDepthinPer = $(this).find('.ToDepthinPer').html();
            var FromTableinPer = $(this).find('.FromTableinPer').html();
            var ToTableinPer = $(this).find('.ToTableinPer').html();
            var FromCrAng = $(this).find('.FromCrAng').html();
            var ToCrAng = $(this).find('.ToCrAng').html();
            var FromCrHt = $(this).find('.FromCrHt').html();
            var ToCrHt = $(this).find('.ToCrHt').html();
            var FromPavAng = $(this).find('.FromPavAng').html();
            var ToPavAng = $(this).find('.ToPavAng').html();
            var FromPavHt = $(this).find('.FromPavHt').html();
            var ToPavHt = $(this).find('.ToPavHt').html();
            var Keytosymbol = $(this).find('.Keytosymbol').html();
            var dCheckKTS = $(this).find('.dCheckKTS').html();
            var dUNCheckKTS = $(this).find('.dUNCheckKTS').html();
            var BGM = $(this).find('.BGM').html();
            var CrownBlack = $(this).find('.CrownBlack').html();
            var TableBlack = $(this).find('.TableBlack').html();
            var CrownWhite = $(this).find('.CrownWhite').html();
            var TableWhite = $(this).find('.TableWhite').html();
            var Image = $(this).find('.Image').html();
            var Video = $(this).find('.Video').html();
            var PriceMethod = $(this).find('.PriceMethod').html();
            var Percentage = $(this).find('.Percentage').html();

            List1.push({
                SupplierId: $("#DdlSupplierName").val(),
                Location: Location,
                GoodsType: GoodsType,
                Shape: Shape,
                Pointer: Carat,
                ColorType: ColorType,
                Color: Color,
                INTENSITY: INTENSITY,
                OVERTONE: OVERTONE,
                FANCY_COLOR: FANCY_COLOR,
                Clarity: Clarity,
                Cut: Cut,
                Polish: Polish,
                Symm: Sym,
                Fls: Fls,
                Lab: Lab,
                FromLength: FromLength,
                ToLength: ToLength,
                FromWidth: FromWidth,
                ToWidth: ToWidth,
                FromDepth: FromDepth,
                ToDepth: ToDepth,
                FromDepthPer: FromDepthinPer,
                ToDepthPer: ToDepthinPer,
                FromTablePer: FromTableinPer,
                ToTablePer: ToTableinPer,
                FromCrAng: FromCrAng,
                ToCrAng: ToCrAng,
                FromCrHt: FromCrHt,
                ToCrHt: ToCrHt,
                FromPavAng: FromPavAng,
                ToPavAng: ToPavAng,
                FromPavHt: FromPavHt,
                ToPavHt: ToPavHt,
                KeyToSymbol: Keytosymbol,
                CheckKTS: dCheckKTS,
                UNCheckKTS: dUNCheckKTS,
                BGM: BGM,
                CrownBlack: CrownBlack,
                TableBlack: TableBlack,
                CrownWhite: CrownWhite,
                TableWhite: TableWhite,
                Img: Image,
                Vdo: Video,
                PriceMethod: PriceMethod,
                PricePer: Percentage
            });
        });
    }

    if (parseInt($("#tblCostFilters #tblBodyCostFilters").find('tr').length) == 0) {
        debugger
        List2.push({
            SupplierId: $("#DdlSupplierName").val(),
            Location: "",
            GoodsType: "",
            Shape: "",
            Pointer: "",
            ColorType: "",
            Color: "",
            INTENSITY: "",
            OVERTONE: "",
            FANCY_COLOR: "",
            Clarity: "",
            Cut: "",
            Polish: "",
            Symm: "",
            Fls: "",
            Lab: "",
            FromLength: "",
            ToLength: "",
            FromWidth: "",
            ToWidth: "",
            FromDepth: "",
            ToDepth: "",
            FromDepthPer: "",
            ToDepthPer: "",
            FromTablePer: "",
            ToTablePer: "",
            FromCrAng: "",
            ToCrAng: "",
            FromCrHt: "",
            ToCrHt: "",
            FromPavAng: "",
            ToPavAng: "",
            FromPavHt: "",
            ToPavHt: "",
            KeyToSymbol: "",
            CheckKTS: "",
            UNCheckKTS: "",
            BGM: "",
            CrownBlack: "",
            TableBlack: "",
            CrownWhite: "",
            TableWhite: "",
            Img: "",
            Vdo: "",
            PriceMethod: "",
            PricePer: ""
        });
    }
    else {
        debugger
        $("#tblCostFilters #tblBodyCostFilters tr").each(function () {
            var Index = $(this).index();
            var Location = $(this).find('.Location').html();
            var GoodsType = $(this).find('.GoodsType').html();
            var Shape = $(this).find('.Shape').html();
            var Carat = $(this).find('.Carat').html();
            var ColorType = $(this).find('.ColorType').html();
            var Color = $(this).find('.Color').html();
            var INTENSITY = $(this).find('.dCheckINTENSITY').html();
            var OVERTONE = $(this).find('.dCheckOVERTONE').html();
            var FANCY_COLOR = $(this).find('.dCheckFANCY_COLOR').html();
            var Clarity = $(this).find('.Clarity').html();
            var Cut = $(this).find('.Cut').html();
            var Polish = $(this).find('.Polish').html();
            var Sym = $(this).find('.Sym').html();
            var Fls = $(this).find('.Fls').html();
            var Lab = $(this).find('.Lab').html();
            var FromLength = $(this).find('.FromLength').html();
            var ToLength = $(this).find('.ToLength').html();
            var FromWidth = $(this).find('.FromWidth').html();
            var ToWidth = $(this).find('.ToWidth').html();
            var FromDepth = $(this).find('.FromDepth').html();
            var ToDepth = $(this).find('.ToDepth').html();
            var FromDepthinPer = $(this).find('.FromDepthinPer').html();
            var ToDepthinPer = $(this).find('.ToDepthinPer').html();
            var FromTableinPer = $(this).find('.FromTableinPer').html();
            var ToTableinPer = $(this).find('.ToTableinPer').html();
            var FromCrAng = $(this).find('.FromCrAng').html();
            var ToCrAng = $(this).find('.ToCrAng').html();
            var FromCrHt = $(this).find('.FromCrHt').html();
            var ToCrHt = $(this).find('.ToCrHt').html();
            var FromPavAng = $(this).find('.FromPavAng').html();
            var ToPavAng = $(this).find('.ToPavAng').html();
            var FromPavHt = $(this).find('.FromPavHt').html();
            var ToPavHt = $(this).find('.ToPavHt').html();
            var Keytosymbol = $(this).find('.Keytosymbol').html();
            var dCheckKTS = $(this).find('.dCheckKTS').html();
            var dUNCheckKTS = $(this).find('.dUNCheckKTS').html();
            var BGM = $(this).find('.BGM').html();
            var CrownBlack = $(this).find('.CrownBlack').html();
            var TableBlack = $(this).find('.TableBlack').html();
            var CrownWhite = $(this).find('.CrownWhite').html();
            var TableWhite = $(this).find('.TableWhite').html();
            var Image = $(this).find('.Image').html();
            var Video = $(this).find('.Video').html();
            var PriceMethod = $(this).find('.PriceMethod').html();
            var Percentage = $(this).find('.Percentage').html();

            List2.push({
                SupplierId: $("#DdlSupplierName").val(),
                Location: Location,
                GoodsType: GoodsType,
                Shape: Shape,
                Pointer: Carat,
                ColorType: ColorType,
                Color: Color,
                INTENSITY: INTENSITY,
                OVERTONE: OVERTONE,
                FANCY_COLOR: FANCY_COLOR,
                Clarity: Clarity,
                Cut: Cut,
                Polish: Polish,
                Symm: Sym,
                Fls: Fls,
                Lab: Lab,
                FromLength: FromLength,
                ToLength: ToLength,
                FromWidth: FromWidth,
                ToWidth: ToWidth,
                FromDepth: FromDepth,
                ToDepth: ToDepth,
                FromDepthPer: FromDepthinPer,
                ToDepthPer: ToDepthinPer,
                FromTablePer: FromTableinPer,
                ToTablePer: ToTableinPer,
                FromCrAng: FromCrAng,
                ToCrAng: ToCrAng,
                FromCrHt: FromCrHt,
                ToCrHt: ToCrHt,
                FromPavAng: FromPavAng,
                ToPavAng: ToPavAng,
                FromPavHt: FromPavHt,
                ToPavHt: ToPavHt,
                KeyToSymbol: Keytosymbol,
                CheckKTS: dCheckKTS,
                UNCheckKTS: dUNCheckKTS,
                BGM: BGM,
                CrownBlack: CrownBlack,
                TableBlack: TableBlack,
                CrownWhite: CrownWhite,
                TableWhite: TableWhite,
                Img: Image,
                Vdo: Video,
                PriceMethod: PriceMethod,
                PricePer: Percentage
            });
        });
    }

    var obj1 = {}
    obj1.Filters = List1;

    var obj2 = {}
    obj2.Filters = List2;

    $.ajax({
        url: "/Settings/Save_SuppStockValue",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ save_suppstockvalue: obj1 }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            debugger
            if (data.Status == "0") {
                toastr.error(data.Message);
            }
            else if (data.Status == "1") {


                $.ajax({
                    url: "/Settings/Save_SuppCostValue",
                    async: false,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify({ save_suppcostvalue: obj2 }),
                    contentType: "application/json; charset=utf-8",
                    success: function (data, textStatus, jqXHR) {
                        debugger
                        if (data.Status == "0") {
                            toastr.error(data.Message);
                        }
                        else if (data.Status == "1") {
                            var result = [];
                            result = data.Message.split("_414_");
                            toastr.success(result[1]);

                            setTimeout(function () {
                                location.href = "/Settings/SuppPriceListDet" + "?Id=" + result[0];
                            }, 2000);
                        }
                        $("#loading").css("display", "none");
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("#loading").css("display", "none");
                        toastr.error(textStatus);
                    }
                });


            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
            toastr.error(textStatus);
        }
    });
};
function BindKeyToSymbolList() {
    $.ajax({
        //url: "/Supplier/Get_KeyToSymbol",
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
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $(".carat-dropdown-main").show();
            KTS = 1;
        }
        else {
            $(".carat-dropdown-main").hide();
            KTS = 0;
        }
    }, 2);
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
function setFromCarat() {
    if ($('#txtfromcarat').val() != "") {
        $('#txtfromcarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
        $('#txttocarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
    } else {
        $('#txtfromcarat').val("");
    }
    if ($('#txttocarat').val() == "") {
        $('#txttocarat').val("");
    }
}
function setToCarat() {
    if ($('#txttocarat').val() != "") {
        $('#txttocarat').val(parseFloat($('#txttocarat').val()).toFixed(2));
    } else {
        $('#txttocarat').val("");
    }
    if ($('#txtfromcarat').val() == "") {
        $('#txtfromcarat').val("");
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
function NewSizeGroup() {
    fcarat = $('#txtfromcarat').val();
    tcarat = $('#txttocarat').val();

    if (fcarat == "" && tcarat == "" || fcarat == 0 && tcarat == 0) {
        toastr.warning("Please Enter Carat !!");
        return false;
    }
    if (fcarat == "") {
        fcarat = "0";
    }
    var _pointerlst_ = [];
    _pointerlst_.push({
        "Id": _pointerlst.length + 1,
        "Value": fcarat + '-' + tcarat,
        "isActive": true,
    });

    var lst = _.filter(_pointerlst, function (e) { return (e.Value == _pointerlst_[0].Value) });
    if (lst.length == 0) {
        var _pointerlst1 = _pointerlst;
        _pointerlst = [];
        for (var i = 0; i <= _pointerlst1.length - 1; i++) {
            _pointerlst.push({
                "Id": parseInt(i) + 1,
                "Value": _pointerlst1[i].Value,
                "isActive": _pointerlst1[i].isActive,
            });
        }

        _pointerlst.push({
            "Id": parseInt(_pointerlst.length) + 1,
            "Value": _pointerlst_[0].Value,
            "isActive": _pointerlst_[0].isActive,
        });

        $(".divCheckedPointerValue").empty();
        for (var j = 0; j <= _pointerlst.length - 1; j++) {
            $('.divCheckedPointerValue').append('<li id="C_' + _pointerlst[j].Id + '" class="carat-li-top allcrt">' + _pointerlst[j].Value + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + _pointerlst[j].Id + ');"></i></li>');
        }
        SetSearchParameter();
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
    }
    else {
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
        toastr.warning("Carat is already exist !!");
    }
    //SetSearchParameter();
}
function NewSizeGroupRemove(id) {
    $('#C_' + id).remove();
    var cList = _.reject(_pointerlst, function (e) { return e.Id == id });
    _pointerlst = cList;
    SetSearchParameter();
}
var ResetCheckColors = function () {
    colorLst = [];
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);

    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);

    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);

    $("#sym-sec0 .carat-dropdown-main").hide();
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();

    C3 = 0, C1 = 0, KTS = 0, C2 = 0;
    _.each(ColorList, function (itm) {
        itm.isActive = false;
    });
    for (var j = 0; j <= ColorList.length - 1; j++) {
        $("#li_Color_" + j).removeClass('active');
    }

    R_F_All_Only_Checkbox_Clr_Rst("1");
    SetSearchParameter();
}
function Bind_RColor() {
    $("#divCheckedColorValue1").empty();
    for (var j = 0; j <= ColorList.length - 1; j++) {
        $('#divCheckedColorValue1').append('<li id="li_Color_' + ColorList[j].Id + '" onclick="ItemClicked(\'Color\',\'' + ColorList[j].Value + '\',\'' + ColorList[j].Id + '\', this);">' + ColorList[j].Value + '</li>');
    }
}
function FcolorBind() {
    for (var i = 0; i <= INTENSITY.length - 1; i++) {
        $('#ddl_INTENSITY').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_I_' + i + '" name="CHK_I_' + i + '" onclick="GetCheck_INTENSITY_List(\'' + INTENSITY[i] + '\',' + i + ');">'
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function GetCheck_OVERTONE_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function GetCheck_FANCY_COLOR_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function resetINTENSITY() {
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
    C1 = 1;
    INTENSITYShow();
    SetSearchParameter();
}
function resetOVERTONE() {
    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
    C2 = 1;
    OVERTONEShow();
    SetSearchParameter();
}
function resetFANCY_COLOR() {
    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    C3 = 1;
    FANCY_COLORShow();
    SetSearchParameter();
}
function Set_FancyColor() {
    FC = "";
    if (Check_Color_1.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
        FC += _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
    }
    if (Check_Color_2.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
        FC += _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
    }
    if (Check_Color_3.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
        FC += _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
    }
    $(".divCheckedColorValue").empty();
    $(".divCheckedColorValue").append(FC);
    $(".divCheckedColorValue").attr({
        "title": FC
    });
}
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        //$("#" + id).removeClass("btn-spn-opt-active");
        //if (id == "Regular") {
        //    Regular = false;
        //}
        //if (id == "Fancy") {
        //    Fancy = false;
        //}
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "Regular") {
            Regular = true;
            Fancy = false;
            $("#Fancy").removeClass("btn-spn-opt-active");
            $("#div_Regular").show();
            $("#div_Fancy").hide();
        }
        if (id == "Fancy") {
            Fancy = true;
            Regular = false;
            $("#Regular").removeClass("btn-spn-opt-active");
            $("#div_Regular").hide();
            $("#div_Fancy").show();
        }
        R_F_All_Only_Checkbox_Clr_Rst("1");
        ResetCheckColors();
    }
}
function R_F_All_Only_Checkbox_Clr_Rst(where) {
    if (where != "-1") {
        $('#chk_Regular_All').prop('checked', false);
        $('#chk_Fancy_All').prop('checked', false);
    }
    if (where == "-1") {
        where = "1";
    }
    if (where != "-0") {
        for (var j = 0; j <= ColorList.length - 1; j++) {
            if (ColorList[j].Value != "ALL") {
                ColorList[j].isActive = false;
            }
            $("#li_Color_" + ColorList[j].Id).removeClass('active');
        }
    }
    if (where == "-0") {
        where = "1";
    }

    Regular_All = false;
    Fancy_All = false;
    if (where == "1") {
        Check_Color_1 = [];
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
        $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
        Check_Color_2 = [];
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
        $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
        Check_Color_3 = [];
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
        $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    }


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
function color_ddl_close() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
}
var SetFinalColors = function () {
    //$(".divCheckedCaratValue").empty();
    //$(".divCheckedCaratValue").append(_.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(","));
    //$(".divCheckedCaratValue").attr({
    //    "title": _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",")
    //});
}
function numvalid(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function DisValValid(type, evt) {
    if (type == "Value") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    if (type == "Disc") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
}
function LenCheckr(type) {
    if (type == "Value") {
        if (parseFloat($("#txtValue").val()) > 100 || parseFloat($("#txtValue").val()) < 0) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only 0.00 to 100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtValue").val("");
        }
        else if (parseFloat($("#txtValue").val()) == NaN) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only 0.00 to 100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtValue").val("");
        }
        else {
            $("#lblPMErr").html("");
            $("#lblPMErr").hide();
        }
    }
    if (type == "Disc") {
        if (parseFloat($("#txtDisc").val()) < -100 || parseFloat($("#txtDisc").val()) > -0) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only -0.00 to -100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtDisc").val("");
        }
        else if (parseFloat($("#txtDisc").val()) == NaN) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only -0.00 to -100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtDisc").val("");
        }
        else {
            $("#lblPMErr").html("");
            $("#lblPMErr").hide();
        }
    }
}
function Get_SuppStockValue() {
    $("#loading").css("display", "block");
    setTimeout(function () {
        debugger
        $.ajax({
            url: "/Settings/Get_SuppStockValue",
            async: false,
            type: "POST",
            data: { SupplierPriceList_Id: $("#hdnId").val() },
            success: function (data, textStatus, jqXHR) {
                debugger
                $("#loading").css("display", "none");
                if (data.Status == "1" && data.Data != null) {
                    debugger
                    $("#tblCustomerFilters #tblBodyCustomerFilters").html("");
                    var cntRow = 0;
                    _(data.Data).each(function (obj, i) {
                        cntRow = parseInt(cntRow) + 1;

                        var MixColor = "";

                        if (obj.Color != null) {
                            MixColor += obj.Color;
                        }
                        if (obj.INTENSITY != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
                            MixColor += obj.INTENSITY;
                        }
                        if (obj.OVERTONE != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
                            MixColor += obj.OVERTONE;
                        }
                        if (obj.FANCY_COLOR != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
                            MixColor += obj.FANCY_COLOR;
                        }
                        if (obj.ColorType != null) {
                            MixColor += (obj.ColorType == "Regular" ? "<b>REGULAR ALL</b>" : obj.ColorType == "Fancy" ? "<b>FANCY ALL</b>" : "");
                        }

                        var html = "<tr id='tr'>";
                        html += "<th class='Row Fi-Criteria' style=''>" + cntRow.toString() + "</th>";
                        html += "<td style=''><span class='Fi-Criteria Location'>" + (obj.Location == null ? "" : obj.Location) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria GoodsType'>" + (obj.GoodsType == null ? "" : obj.GoodsType) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Shape'>" + (obj.Shape == null ? "" : obj.Shape) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Carat'>" + (obj.Pointer == null ? "" : obj.Pointer) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + (obj.ColorType == null ? "" : obj.ColorType) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + (obj.Color == null ? "" : obj.Color) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + (obj.INTENSITY == null ? "" : obj.INTENSITY) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + (obj.OVERTONE == null ? "" : obj.OVERTONE) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + (obj.FANCY_COLOR == null ? "" : obj.FANCY_COLOR) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Clarity'>" + (obj.Clarity == null ? "" : obj.Clarity) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Cut'>" + (obj.Cut == null ? "" : obj.Cut) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Polish'>" + (obj.Polish == null ? "" : obj.Polish) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Sym'>" + (obj.Symm == null ? "" : obj.Symm) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Fls'>" + (obj.Fls == null ? "" : obj.Fls) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Lab'>" + (obj.Lab == null ? "" : obj.Lab) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromLength'>" + (obj.FromLength == null ? "" : obj.FromLength.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToLength'>" + (obj.ToLength == null ? "" : obj.ToLength.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromWidth'>" + (obj.FromWidth == null ? "" : obj.FromWidth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToWidth'>" + (obj.ToWidth == null ? "" : obj.ToWidth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepth'>" + (obj.FromDepth == null ? "" : obj.FromDepth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepth'>" + (obj.ToDepth == null ? "" : obj.ToDepth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + (obj.FromDepthPer == null ? "" : obj.FromDepthPer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + (obj.ToDepthPer == null ? "" : obj.ToDepthPer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + (obj.FromTablePer == null ? "" : obj.FromTablePer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + (obj.ToTablePer == null ? "" : obj.ToTablePer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + (obj.FromCrAng == null ? "" : obj.FromCrAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + (obj.ToCrAng == null ? "" : obj.ToCrAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + (obj.FromCrHt == null ? "" : obj.FromCrHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + (obj.ToCrHt == null ? "" : obj.ToCrHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + (obj.FromPavAng == null ? "" : obj.FromPavAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + (obj.ToPavAng == null ? "" : obj.ToPavAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + (obj.FromPavHt == null ? "" : obj.FromPavHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + (obj.ToPavHt == null ? "" : obj.ToPavHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + (obj.KeyToSymbol == null ? "" : obj.KeyToSymbol) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + (obj.CheckKTS == null ? "" : obj.CheckKTS) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + (obj.UNCheckKTS == null ? "" : obj.UNCheckKTS) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria BGM'>" + (obj.BGM == null ? "" : obj.BGM) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + (obj.CrownBlack == null ? "" : obj.CrownBlack) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableBlack'>" + (obj.TableBlack == null ? "" : obj.TableBlack) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + (obj.CrownWhite == null ? "" : obj.CrownWhite) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableWhite'>" + (obj.TableWhite == null ? "" : obj.TableWhite) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Image'>" + (obj.Img == null ? "" : obj.Img) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Video'>" + (obj.Vdo == null ? "" : obj.Vdo) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + (obj.PriceMethod == null ? "" : obj.PriceMethod) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Percentage'>" + (obj.PricePer == null ? "" : obj.PricePer) + "</span></td>";
                        html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
                        html += "</tr>";

                        $("#tblCustomerFilters #tblBodyCustomerFilters").append(html);
                    });
                    $("#lblCustNoFound").hide();
                    $("#tblCustomerFilters").show();
                }
                else {debugger
                    $("#lblCustNoFound").show();
                    $("#tblCustomerFilters").hide();
                    $("#tblCustomerFilters #tblBodyCustomerFilters").html("");

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#loading").css("display", "none");
            }
        });


        debugger
        $.ajax({
            url: "/Settings/Get_SuppCostValue",
            async: false,
            type: "POST",
            data: { SupplierPriceList_Id: $("#hdnId").val() },
            success: function (data, textStatus, jqXHR) {
                debugger
                $("#loading").css("display", "none");
                if (data.Status == "1" && data.Data != null) {
                    debugger

                    $("#tblCostFilters #tblBodyCostFilters").html("");
                    var cntRow = 0;
                    _(data.Data).each(function (obj, i) {
                        cntRow = parseInt(cntRow) + 1;

                        var MixColor = "";

                        if (obj.Color != null) {
                            MixColor += obj.Color;
                        }
                        if (obj.INTENSITY != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
                            MixColor += obj.INTENSITY;
                        }
                        if (obj.OVERTONE != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
                            MixColor += obj.OVERTONE;
                        }
                        if (obj.FANCY_COLOR != null) {
                            MixColor += (MixColor == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
                            MixColor += obj.FANCY_COLOR;
                        }
                        if (obj.ColorType != null) {
                            MixColor += (obj.ColorType == "Regular" ? "<b>REGULAR ALL</b>" : obj.ColorType == "Fancy" ? "<b>FANCY ALL</b>" : "");
                        }

                        var html = "<tr id='tr'>";
                        html += "<th class='Row Fi-Criteria' style=''>" + cntRow.toString() + "</th>";
                        html += "<td style=''><span class='Fi-Criteria Location'>" + (obj.Location == null ? "" : obj.Location) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria GoodsType'>" + (obj.GoodsType == null ? "" : obj.GoodsType) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Shape'>" + (obj.Shape == null ? "" : obj.Shape) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Carat'>" + (obj.Pointer == null ? "" : obj.Pointer) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + (obj.ColorType == null ? "" : obj.ColorType) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + (obj.Color == null ? "" : obj.Color) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + (obj.INTENSITY == null ? "" : obj.INTENSITY) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + (obj.OVERTONE == null ? "" : obj.OVERTONE) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + (obj.FANCY_COLOR == null ? "" : obj.FANCY_COLOR) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Clarity'>" + (obj.Clarity == null ? "" : obj.Clarity) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Cut'>" + (obj.Cut == null ? "" : obj.Cut) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Polish'>" + (obj.Polish == null ? "" : obj.Polish) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Sym'>" + (obj.Symm == null ? "" : obj.Symm) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Fls'>" + (obj.Fls == null ? "" : obj.Fls) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Lab'>" + (obj.Lab == null ? "" : obj.Lab) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromLength'>" + (obj.FromLength == null ? "" : obj.FromLength.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToLength'>" + (obj.ToLength == null ? "" : obj.ToLength.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromWidth'>" + (obj.FromWidth == null ? "" : obj.FromWidth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToWidth'>" + (obj.ToWidth == null ? "" : obj.ToWidth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepth'>" + (obj.FromDepth == null ? "" : obj.FromDepth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepth'>" + (obj.ToDepth == null ? "" : obj.ToDepth.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + (obj.FromDepthPer == null ? "" : obj.FromDepthPer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + (obj.ToDepthPer == null ? "" : obj.ToDepthPer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + (obj.FromTablePer == null ? "" : obj.FromTablePer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + (obj.ToTablePer == null ? "" : obj.ToTablePer.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + (obj.FromCrAng == null ? "" : obj.FromCrAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + (obj.ToCrAng == null ? "" : obj.ToCrAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + (obj.FromCrHt == null ? "" : obj.FromCrHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + (obj.ToCrHt == null ? "" : obj.ToCrHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + (obj.FromPavAng == null ? "" : obj.FromPavAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + (obj.ToPavAng == null ? "" : obj.ToPavAng.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + (obj.FromPavHt == null ? "" : obj.FromPavHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + (obj.ToPavHt == null ? "" : obj.ToPavHt.toFixed(2)) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + (obj.KeyToSymbol == null ? "" : obj.KeyToSymbol) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + (obj.CheckKTS == null ? "" : obj.CheckKTS) + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + (obj.UNCheckKTS == null ? "" : obj.UNCheckKTS) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria BGM'>" + (obj.BGM == null ? "" : obj.BGM) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + (obj.CrownBlack == null ? "" : obj.CrownBlack) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableBlack'>" + (obj.TableBlack == null ? "" : obj.TableBlack) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + (obj.CrownWhite == null ? "" : obj.CrownWhite) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableWhite'>" + (obj.TableWhite == null ? "" : obj.TableWhite) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Image'>" + (obj.Img == null ? "" : obj.Img) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Video'>" + (obj.Vdo == null ? "" : obj.Vdo) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + (obj.PriceMethod == null ? "" : obj.PriceMethod) + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Percentage'>" + (obj.PricePer == null ? "" : obj.PricePer) + "</span></td>";
                        html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
                        html += "</tr>";

                        $("#tblCostFilters #tblBodyCostFilters").append(html);
                    });
                    $("#lblCostNoFound").hide();
                    $("#tblCostFilters").show();
                }
                else {
                    debugger
                    $("#lblCostNoFound").show();
                    $("#tblCostFilters").hide();
                    $("#tblCostFilters #tblBodyCostFilters").html("");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#loading").css("display", "none");
            }
        });


    }, 50);
}
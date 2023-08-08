var ParameterList;
var isModify = 0;
var gridOptions1 = {};
var gridOptions2 = {};
var gridOptions3 = {};
var gridOptions4 = {};
var gridOptions5 = {};
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
var LusterList = [];
var LocationList = [];
var CurencyList = [];
var TableOpenList = [];
var CrownOpenList = [];
var PavOpenList = [];
var GirdleOpenList = [];
var prno = 0;
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
//var GalleryDatalist = [];
//var limit = 0;
//var renderLimit = 0;
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
var AllD = false;
var tabno = 1;
var CurrentTab = 1;
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";

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

/*------------ order-history-dropdown-select ------------*/
$(document).ready(function (e) {
    $(".aEmailStone").tooltip({ placement: "bottom" });

    GetDashboardCount();
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

    $(".numeric").numeric({ decimal: ".", negative: true, decimalPlaces: 2 });
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
    GetCompanyList();
    $("#txtCompanyName").focusout(function () {
        CmpnynmSelectRequired();
    });
});
$(document).ready(function () {
    $(".card").click(function () {
        $(".card").removeClass("active");
        $(this).addClass("active");
    });
});

/*CARAT2 li*/
$(document).ready(function () {
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

});

/*common li*/
$(document).ready(function () {
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

});

/*top icon bar*/
$(document).ready(function () {
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
    $("#myTab").on("click", "a", function (e) {
        //   $(this).tab('show');
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
});

new WOW().init();

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

$(document).ready(function () {
    GET_Scheme_Disc();
    GetSearchParameter();
    BindColumnsSettings();
    BindKeyToSymbolList();
    $('.cntCart').click(function () {
        location.href = '/Cart/Index';
    });
    $('.cntwishlist').click(function () {
        var url = '';
        if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1)
            url = '/Wishlist/Admin_Wishlist';
        else
            url = '/Wishlist/Index';
        location.href = url;
    });
    $('.sym-sec').on('click', function () {
        $('.sym-sec').toggleClass('active');
    });
});
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
        navbarHei = $(".navbar").height(),
        resultHei = 0,
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 75;
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
function SetModifyParameter(tabNo) {
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
        url: "/PairSearch/GetModifyStockParameter",
        async: false,
        type: "POST",
        data: { tabNo: tabNo },
        success: function (data, textStatus, jqXHR) {
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

            debugger
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
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            ParameterList = data.Data;
            if (ParameterList.length > 0) {
                _.each(ParameterList, function (itm) {
                    itm.ACTIVE = false;
                });

                ParameterList.push({
                    Id: 12, Value: "OTHERS", ListType: "SHAPE",
                    UrlValue: "https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg",
                    UrlValueHov: "https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png"
                });

                ParameterList.push({
                    Id: 13, Value: "ALL", ListType: "SHAPE",
                    UrlValue: "",
                    UrlValueHov: ""
                });
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
                $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
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

            debugger
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

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
function BindKeyToSymbolList() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetKeyToSymbolList",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var KeytoSymbolList = data.Data;
            $('#searchkeytosymbol').html("");
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
                        + '<li class="col">'
                        + '<span>' + itm.sSymbol + '</span>'
                        + '</li>'
                        + '</ul>'
                        + '</div>')
                    //itm.ACTIVE = false;
                    //itm.INACTIVE = false;
                });
                $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

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
            // $("#txtemail").val('');
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
    var cut_Lst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polish_Lst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var sym_Lst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    //if (cut_Lst == "EX" && polish_Lst == "EX" && sym_Lst == "EX") {
    //    $('#TripalEX').addClass('active1');
    //    $('#TripalEX').addClass('active');
    //    StockSearch.TripalEX = true;
    //}
    //else {
    //    $('#TripalEX').removeClass('active');
    //    $('#TripalEX').removeClass('active1');
    //    StockSearch.TripalEX = false;
    //}
    //if (cut_Lst == "EX,VG" && polish_Lst == "EX,VG" && sym_Lst == "EX,VG") {
    //    $('#TripalVG').addClass('active1');
    //    $('#TripalVG').addClass('active');
    //    StockSearch.TripalVG = true;
    //}
    //else {
    //    $('#TripalVG').removeClass('active');
    //    $('#TripalVG').removeClass('active1');
    //    StockSearch.TripalVG = false;
    //}

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var cutLst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var symLst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BGMLst = _.pluck(_.filter(BgmList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var BLACKLst = _.pluck(_.filter(BlackList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var milkyLst = _.pluck(_.filter(LusterList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    debugger
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
        "Color": colorLst,
        "Clarity": clarityLst,
        "Cut": cutLst,
        "Lab": labLst,
        "Polish": polishLst,
        "Location": locationLst,
        "Symm": symLst,
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
        "SmartSearch": "",
        "ShapeColorPurity": $('#hdnShapeColorPurity').val(),

        "KeyToSymbol": KeyToSymLst_Check + '-' + KeyToSymLst_uncheck,//FinalList_KTS, //KeyToSymLst, //,StockSearch.KeyToSymbol == undefined ? "" : StockSearch.KeyToSymbol,
        "CheckKTS": KeyToSymLst_Check,
        "UNCheckKTS": KeyToSymLst_uncheck,
        "HasImage": $('#SearchImage').hasClass('active') ? true : "",
        "HasHDMovie": $('#SearchVideo').hasClass('active') ? true : "",
        "Shade": "",
        "ReviseStockFlag": "",
        "IsPromotion": "",
        "PageNo": 1,
        "TokenNo": "",
        "StoneStatus": "",
        "Table_Open": tableopenlst,
        "Crown_Open": crownopenlst,
        "Pav_Open": pavopenlst,
        "Girdle_Open": girdleopenlst,
    };
    if (flag == 'savesearch') {
        var Searchname = $('#txtSearchName').val();
        $("#save-src-modl").modal("hide");
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/SaveSearchData",
            type: "POST",
            data: { SearchId: $('#hdnSavedSearchId').val(), Searchname: Searchname, model: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
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
                obj1 = obj;
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
            GetSearch();
        } else {
            toastr.warning($("#hdn_You_Can_Search_Maximum_5_Result").val());
        }
        contentHeight();
    }
}
function CloseTab(tabid) {

    $('#iresult' + tabid + '').css('display', 'none');
    $('#result' + tabid + '').css('display', 'none');
    $('#result' + tabid + '').removeClass('active');
    $('#profile-tab' + tabid + '').removeClass("active").removeClass("show");
    tabno = 0;
    CurrentTab = 0;
    $('#tabhome').tab("show");
}
function BindColumnsSettings() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetColumnsConfigForSearch",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
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
                        filter: false,
                        cellClass: function (params) {
                            if (params.data != undefined) {
                                if (params.data.PairLastColumn) {
                                    return 'paircolumn';
                                }
                            }
                        }
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
                else {
                    if (field == 'shape') {
                        width = 60;
                        headerName = $("#hdn_Shape").val();
                    }
                    else if (field == 'certi_no') {
                        width = 80;
                        headerName = $("#hdn_Certi_No").val();
                    }
                    else if (field == 'BGM') {
                        width = 70;
                        headerName = $("#hdn_BGM").val();
                    }
                    else if (field == 'price_per_cts') {
                        headerName = $("#hdn_Price_Cts").val();
                        width = 75;
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
                    else if (field == 'symbol') {
                        headerName = $("#hdn_Key_to_symbol").val();
                    }
                    else if (field == 'sCulet') {
                        headerName = $("#hdn_Culet").val();
                    }
                    else if (field == 'table_natts') {
                        headerName = $("#hdn_Table_Black").val();
                    }
                    else if (field == 'Crown_Natts') {
                        headerName = $("#hdn_Crown_Natts").val();
                    }
                    else if (field == 'inclusion') {
                        headerName = $("#hdn_Table_White").val();
                    }
                    else if (field == 'Crown_Inclusion') {
                        headerName = $("#hdn_Crown_White").val();
                    }
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
                    }
                    else if (field == 'Table_Open') {
                        headerName = $("#hdn_Table_Open").val();
                        width = 75;
                    }
                    else if (field == 'Crown_Open') {
                        headerName = $("#hdn_Crown_Open").val();
                        width = 80;
                    }
                    else if (field == 'Pav_Open') {
                        headerName = $("#hdn_Pav_Open").val();
                        width = 70;
                    }
                    else if (field == 'Girdle_Open') {
                        headerName = $("#hdn_Girdle_Open").val();
                        width = 80;
                    }
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
                            else if (field == 'sales_disc_per' || field == 'net_amount' || field == 'price_per_cts' || field == 'cur_rap_rate' || field == 'rap_amount' || field == 'length' || field == 'width' || field == 'depth' || field == 'depth_per' || field == 'table_per' || field == 'crown_angle' || field == 'crown_height' || field == 'pav_angle' || field == 'pav_height' || field == 'girdle_per') {
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
                                else if (field == 'sales_disc_per' || field == 'net_amount' || field == 'price_per_cts' || field == 'cur_rap_rate' || field == 'rap_amount' || field == 'length' || field == 'width' || field == 'depth' || field == 'depth_per' || field == 'table_per' || field == 'crown_angle' || field == 'crown_height' || field == 'pav_angle' || field == 'pav_height' || field == 'girdle_per') {
                                    return formatNumber(params.value);
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
                                if (field == 'sales_disc_per' || field == 'net_amount') {
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
                        iColumnId: item.iColumnId
                    };
                }
                columnDefs.push(column);
            });

            for (var i = 0; i < columnDefs.length; i++) {
                if (columnDefs[i].field == "Final_Disc") {
                    columnDefs.splice(i, 1);
                }
                if (columnDefs[i].field == "Final_Value") {
                    columnDefs.splice(i, 1);
                }
            }
            var Hold_columns = [
                { headerName: "Hold_Party_Code", field: "Hold_Party_Code", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "Hold_Username", field: "Hold_Username", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "Hold_CompName", field: "Hold_CompName", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForCust_Hold", field: "ForCust_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForAssist_Hold", field: "ForAssist_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
                { headerName: "ForAdmin_Hold", field: "ForAdmin_Hold", cellRenderer: function (params) { return params.value; }, hide: true },
            ];
            for (var i = 0; i < Hold_columns.length; i++) {
                columnDefs.push(Hold_columns[i]);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

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
    setTimeout(function () {
        $('.paircolumn').parent().addClass('pairrow');
    }, 0);
    //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.value + '</a></div>';
    //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.value + '</a></div>';
    return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.stone_ref_no + '">' + params.value + '</a></div>';
}
function viewIndicator(params) {

    return params.value;

}
//function countryIndicator(params) {
//    var value = '';
//    if (params.value == 'India') {
//        value = 'Flag_of_India.svg.png';
//    } else if (params.value == 'Hong Kong') {
//        value = 'Flag_of_Hong_Kong.png';
//    } else {
//        value = 'airplane-flight.png';
//    }
//    var element = '<span><img title="' + params.value + '" src="/Content/images/' + value + '" style="height: 20px;width: 25px;"/></span>';
//    return element;
//}

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
    setTimeout(function () {
        $('.offercls').parent().addClass('offerrow');
        $('.upcomingcls').parent().addClass('upcomingrow');
    }, 0);
    if (params.data != undefined) {
        if (params.value == "GIA") {
            return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
        }
        else if (params.value == "HRD") {
            return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
        }
        else if (params.value == "IGI") {
            return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
        }
        else {
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
/*-------------------------------------------------------- GET SEARCH AND AGGRID BINDING START------------------------------------------------*/
function onSelectionChanged(params) {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;
    if (CurrentTab == 1) {
        if (gridOptions1.api.getSelectedRows().length > 0) {
            dDisc = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions1.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
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

    //$('#tab' + CurrentTab + 'cts').html($("#hdn_Cts").val() +' : ' + formatNumber(TOT_CTS) + '');
    //$('#tab' + CurrentTab + 'disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
    //$('#tab' + CurrentTab + 'ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
    //$('#tab' + CurrentTab + 'totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TOT_NET_AMOUNT) + '');
    //$('#tab' + CurrentTab + 'pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');

    $('#tab' + CurrentTab + 'cts').html(formatNumber(TOT_CTS));
    $('#tab' + CurrentTab + 'disc').html(formatNumber(AVG_SALES_DISC_PER));
    $('#tab' + CurrentTab + 'ppcts').html(formatNumber(AVG_PRICE_PER_CTS));
    $('#tab' + CurrentTab + 'totAmt').html(formatNumber(TOT_NET_AMOUNT));
    $('#tab' + CurrentTab + 'pcs').html(TOT_PCS);
    $('#tab' + CurrentTab + 'Web_Disc').html(formatNumber(Web_Benefit));
    $('#tab' + CurrentTab + 'Net_Value').html(formatNumber(Net_Value));
    $('#tab' + CurrentTab + 'Final_Disc').html(formatNumber(Final_Disc));
}
function columnVisible(params) {
    if (params.column.colId == 0 && params.visible) {
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
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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
        if (gridOptions1.api != undefined) {
            gridOptions1.api.destroy();
        }
        gridOptions1 = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 1,
            components: {
                deltaIndicator: deltaIndicator,
                statusIndicator: statusIndicator,
                labIndicator: labIndicator,
                viewIndicator: viewIndicator,
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
            rowModelType: 'serverSide',
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
                viewIndicator: viewIndicator,
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
        $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
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

    debugger
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

    //$('#txtfromcarat').val("");
    //$('#txttocarat').val("");
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
const datasource1 = {
    getRows(params) {

        console.log(JSON.stringify(params.request, null, 1));
        obj1.FormName = 'Pair Search';
        obj1.ActivityType = 'Get';
        obj1.PgSize = pgSize1;
        obj1.PageNo = gridOptions1.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj1.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
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

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        Datalist1 = [];
        debugger
        $.ajax({
            url: "/PairSearch/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj1, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    summary1 = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary1.TOT_PCS);
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist1.push(obj);
                    });

                    //$('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary1.TOT_CTS) + '');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary1.AVG_SALES_DISC_PER) + '');
                    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary1.AVG_PRICE_PER_CTS) + '');
                    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary1.TOT_NET_AMOUNT) + '');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + summary1.TOT_PCS + '');
                    $('#tab1TCount').show();
                    $('#tab1cts').html(formatNumber(summary1.TOT_CTS));
                    $('#tab1disc').html(formatNumber(summary1.AVG_SALES_DISC_PER));
                    $('#tab1ppcts').html(formatNumber(summary1.AVG_PRICE_PER_CTS));
                    $('#tab1totAmt').html(formatNumber(summary1.TOT_NET_AMOUNT));
                    $('#tab1pcs').html(summary1.TOT_PCS);
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                } else {
                    params.successCallback(0, 0);
                    gridOptions1.api.showNoRowsOverlay();
                    //$('#tab1cts').html($("#hdn_Cts").val() +' : 0');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab1TCount').hide();
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    $('#tab1ppcts').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1pcs').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
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
                    $('#dvGalleryView1').html("");
                    limit1 = 0;
                    if (Datalist1.length > 12) {
                        renderLimit1 = 12;
                        $('#btnLoadMore1').show();
                    } else {
                        renderLimit1 = Datalist1.length;
                        $('#btnLoadMore1').hide();
                    }
                    BindGalleryView(1);
                }, 1000);
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
        console.log(JSON.stringify(params.request, null, 1));
        obj2.FormName = 'Pair Search';
        obj2.ActivityType = 'Get';
        obj2.PgSize = pgSize2;
        obj2.PageNo = gridOptions2.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj2.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
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

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        Datalist2 = [];
        $.ajax({
            url: "/PairSearch/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj2, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    summary2 = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary2.TOT_PCS);
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist2.push(obj);
                    });

                    //$('#tab2cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary2.TOT_CTS) + '');
                    //$('#tab2disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary2.AVG_SALES_DISC_PER) + '');
                    //$('#tab2ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary2.AVG_PRICE_PER_CTS) + '');
                    //$('#tab2totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary2.TOT_NET_AMOUNT) + '');
                    //$('#tab2pcs').html($("#hdn_Pcs").val() + ' : ' + summary2.TOT_PCS + '');
                    $('#tab2TCount').show();
                    $('#tab2cts').html(formatNumber(summary2.TOT_CTS));
                    $('#tab2disc').html(formatNumber(summary2.AVG_SALES_DISC_PER));
                    $('#tab2ppcts').html(formatNumber(summary2.AVG_PRICE_PER_CTS));
                    $('#tab2totAmt').html(formatNumber(summary2.TOT_NET_AMOUNT));
                    $('#tab2pcs').html(summary2.TOT_PCS);
                    $('#tab2_WebDisc_t').hide();
                    $('#tab2_FinalValue_t').hide();
                    $('#tab2_FinalDisc_t').hide();
                } else {
                    params.successCallback(0, 0);
                    gridOptions2.api.showNoRowsOverlay();
                    //$('#tab2cts').html($("#hdn_Cts").val() +' : 0');
                    //$('#tab2disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab2ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab2totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    //$('#tab2pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab2TCount').hide();
                    $('#tab2cts').html('0');
                    $('#tab2disc').html('0');
                    $('#tab2ppcts').html('0');
                    $('#tab2totAmt').html('0');
                    $('#tab2pcs').html('0');
                    $('#tab2_WebDisc_t').hide();
                    $('#tab2_FinalValue_t').hide();
                    $('#tab2_FinalDisc_t').hide();
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
                instance = gridOptions2.api.getFilterInstance('clarity');
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
                    $('#dvGalleryView2').html("");
                    limit2 = 0;
                    if (Datalist2.length > 12) {
                        renderLimit2 = 12;
                        $('#btnLoadMore2').show();
                    } else {
                        renderLimit2 = Datalist2.length;
                        $('#btnLoadMore2').hide();
                    }
                    BindGalleryView(2);
                }, 1000);
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
        console.log(JSON.stringify(params.request, null, 1));
        obj3.FormName = 'Pair Search';
        obj3.ActivityType = 'Get';
        obj3.PgSize = pgSize3;
        obj3.PageNo = gridOptions3.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj3.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
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

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        Datalist3 = [];
        $.ajax({
            url: "/PairSearch/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj3, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    summary3 = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary3.TOT_PCS);
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist3.push(obj);
                    });

                    //$('#tab3cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary3.TOT_CTS) + '');
                    //$('#tab3disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary3.AVG_SALES_DISC_PER) + '');
                    //$('#tab3ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary3.AVG_PRICE_PER_CTS) + '');
                    //$('#tab3totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary3.TOT_NET_AMOUNT) + '');
                    //$('#tab3pcs').html($("#hdn_Pcs").val() + ' : ' + summary3.TOT_PCS + '');
                    $('#tab3TCount').show();
                    $('#tab3cts').html(formatNumber(summary3.TOT_CTS));
                    $('#tab3disc').html(formatNumber(summary3.AVG_SALES_DISC_PER));
                    $('#tab3ppcts').html(formatNumber(summary3.AVG_PRICE_PER_CTS));
                    $('#tab3totAmt').html(formatNumber(summary3.TOT_NET_AMOUNT));
                    $('#tab3pcs').html(summary3.TOT_PCS);
                    $('#tab3_WebDisc_t').hide();
                    $('#tab3_FinalValue_t').hide();
                    $('#tab3_FinalDisc_t').hide();
                } else {
                    params.successCallback(0, 0);
                    gridOptions3.api.showNoRowsOverlay();
                    //$('#tab3cts').html($("#hdn_Cts").val() +' : 0');
                    //$('#tab3disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab3ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab3totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    //$('#tab3pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab3TCount').hide();
                    $('#tab3cts').html('0');
                    $('#tab3disc').html('0');
                    $('#tab3ppcts').html('0');
                    $('#tab3totAmt').html('0');
                    $('#tab3pcs').html('0');
                    $('#tab3_WebDisc_t').hide();
                    $('#tab3_FinalValue_t').hide();
                    $('#tab3_FinalDisc_t').hide();
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
                    $('#dvGalleryView3').html("");
                    limit3 = 0;
                    if (Datalist3.length > 12) {
                        renderLimit3 = 12;
                        $('#btnLoadMore3').show();
                    } else {
                        renderLimit3 = Datalist3.length;
                        $('#btnLoadMore3').hide();
                    }
                    BindGalleryView(3);
                }, 1000);
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
        console.log(JSON.stringify(params.request, null, 1));
        obj4.FormName = 'Pair Search';
        obj4.ActivityType = 'Get';
        obj4.PgSize = pgSize4;
        obj4.PageNo = gridOptions4.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj4.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
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

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        Datalist4 = [];
        $.ajax({
            url: "/PairSearch/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj4, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    summary4 = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary4.TOT_PCS);
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist4.push(obj);
                    });

                    //$('#tab4cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary4.TOT_CTS) + '');
                    //$('#tab4disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary4.AVG_SALES_DISC_PER) + '');
                    //$('#tab4ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary4.AVG_PRICE_PER_CTS) + '');
                    //$('#tab4totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary4.TOT_NET_AMOUNT) + '');
                    //$('#tab4pcs').html($("#hdn_Pcs").val() + ' : ' + summary4.TOT_PCS + '');
                    $('#tab4TCount').show();
                    $('#tab4cts').html(formatNumber(summary4.TOT_CTS));
                    $('#tab4disc').html(formatNumber(summary4.AVG_SALES_DISC_PER));
                    $('#tab4ppcts').html(formatNumber(summary4.AVG_PRICE_PER_CTS));
                    $('#tab4totAmt').html(formatNumber(summary4.TOT_NET_AMOUNT));
                    $('#tab4pcs').html(summary4.TOT_PCS);
                    $('#tab4_WebDisc_t').hide();
                    $('#tab4_FinalValue_t').hide();
                    $('#tab4_FinalDisc_t').hide();
                } else {
                    params.successCallback(0, 0);
                    gridOptions4.api.showNoRowsOverlay();
                    //$('#tab4cts').html($("#hdn_Cts").val() +' : 0');
                    //$('#tab4disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab4ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab4totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    //$('#tab4pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab4TCount').hide();
                    $('#tab4cts').html('0');
                    $('#tab4disc').html('0');
                    $('#tab4ppcts').html('0');
                    $('#tab4totAmt').html('0');
                    $('#tab4pcs').html('0');
                    $('#tab4_WebDisc_t').hide();
                    $('#tab4_FinalValue_t').hide();
                    $('#tab4_FinalDisc_t').hide();
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
                    $('#dvGalleryView4').html("");
                    limit4 = 0;
                    if (Datalist4.length > 12) {
                        renderLimit4 = 12;
                        $('#btnLoadMore4').show();
                    } else {
                        renderLimit4 = Datalist4.length;
                        $('#btnLoadMore4').hide();
                    }
                    BindGalleryView(4);
                }, 1000);
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
        console.log(JSON.stringify(params.request, null, 1));
        obj5.FormName = 'Pair Search';
        obj5.ActivityType = 'Get';
        obj5.PgSize = pgSize5;
        obj5.PageNo = gridOptions5.api.paginationGetCurrentPage() + 1;
        if (params.request.sortModel.length > 0) {
            obj5.OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
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

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        Datalist5 = [];
        $.ajax({
            url: "/PairSearch/GetSearchStock",
            async: false,
            type: "POST",
            data: { obj: obj5, tabNo: CurrentTab },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    summary5 = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary5.TOT_PCS);
                    $.map(data.Data[0].DataList, function (obj) {
                        Datalist5.push(obj);
                    });

                    //$('#tab5cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary5.TOT_CTS) + '');
                    //$('#tab5disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary5.AVG_SALES_DISC_PER) + '');
                    //$('#tab5ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(summary5.AVG_PRICE_PER_CTS) + '');
                    //$('#tab5totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(summary5.TOT_NET_AMOUNT) + '');
                    //$('#tab5pcs').html($("#hdn_Pcs").val() + ' : ' + summary5.TOT_PCS + '');
                    $('#tab5TCount').show();
                    $('#tab5cts').html(formatNumber(summary5.TOT_CTS));
                    $('#tab5disc').html(formatNumber(summary5.AVG_SALES_DISC_PER));
                    $('#tab5ppcts').html(formatNumber(summary5.AVG_PRICE_PER_CTS));
                    $('#tab5totAmt').html(formatNumber(summary5.TOT_NET_AMOUNT));
                    $('#tab5pcs').html(summary5.TOT_PCS);
                    $('#tab5_WebDisc_t').hide();
                    $('#tab5_FinalValue_t').hide();
                    $('#tab5_FinalDisc_t').hide();
                } else {
                    params.successCallback(0, 0);
                    gridOptions5.api.showNoRowsOverlay();
                    //$('#tab5cts').html($("#hdn_Cts").val() +' : 0');
                    //$('#tab5disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    //$('#tab5ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab5totAmt').html($("#hdn_Total_Amount").val() + ' : 0');
                    //$('#tab5pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab5TCount').hide();
                    $('#tab5cts').html('0');
                    $('#tab5disc').html('0');
                    $('#tab5ppcts').html('0');
                    $('#tab5totAmt').html('0');
                    $('#tab5pcs').html('0');
                    $('#tab5_WebDisc_t').hide();
                    $('#tab5_FinalValue_t').hide();
                    $('#tab5_FinalDisc_t').hide();
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
                    $('#dvGalleryView5').html("");
                    limit5 = 0;
                    if (Datalist5.length > 12) {
                        renderLimit5 = 12;
                        $('#btnLoadMore5').show();
                    } else {
                        renderLimit5 = Datalist5.length;
                        $('#btnLoadMore5').hide();
                    }
                    BindGalleryView(5);
                }, 1000);
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
function LoadMore(TN) {
    if (TN == 1) {
        renderLimit1 = renderLimit1 + 12;
        if (Datalist1.length > renderLimit1) {
            $('#btnLoadMore1').show();
        } else {
            renderLimit1 = Datalist1.length;
            $('#btnLoadMore1').hide();
        }
        BindGalleryView(TN);
    }
    if (TN == 2) {
        renderLimit2 = renderLimit2 + 12;
        if (Datalist2.length > renderLimit2) {
            $('#btnLoadMore2').show();
        } else {
            renderLimit2 = Datalist2.length;
            $('#btnLoadMore2').hide();
        }
        BindGalleryView(TN);
    }
    if (TN == 3) {
        renderLimit3 = renderLimit3 + 12;
        if (Datalist3.length > renderLimit3) {
            $('#btnLoadMore3').show();
        } else {
            renderLimit3 = Datalist3.length;
            $('#btnLoadMore3').hide();
        }
        BindGalleryView(TN);
    }
    if (TN == 4) {
        renderLimit4 = renderLimit4 + 12;
        if (Datalist4.length > renderLimit4) {
            $('#btnLoadMore4').show();
        } else {
            renderLimit4 = Datalist4.length;
            $('#btnLoadMore4').hide();
        }
        BindGalleryView(TN);
    }
    if (TN == 5) {
        renderLimit5 = renderLimit5 + 12;
        if (Datalist5.length > renderLimit5) {
            $('#btnLoadMore5').show();
        } else {
            renderLimit5 = Datalist5.length;
            $('#btnLoadMore5').hide();
        }
        BindGalleryView(TN);
    }

    //renderLimit = renderLimit + 12;
    //if (GalleryDatalist.length > renderLimit) {
    //    $('#btnLoadMore').show();
    //} else {
    //    renderLimit = GalleryDatalist.length;
    //    $('#btnLoadMore').hide();
    //}
    //BindGalleryView();
}
//function BindGalleryView() {
//    for (var i = limit; i < renderLimit; i++) {
//        limit = limit + 1;

//        $('#dvGalleryView').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
//            '    <div class="gallery-card">' +
//            '        <div class="card-img ">' +
//            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (GalleryDatalist[i].bPRimg ? $("#External_ImageURL").val() + GalleryDatalist[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
//            '        </div>' +
//            '        <div class="grid-check-sign">' +
//            '            <i class="fa fa-check"></i>' +
//            '        </div>' +
//            '        <div class="card-content src-shape-main-pcscroll">' +
//            '            <div class="grid-box-main">' +
//            '                <div class="center-text">' +
//            '                    <div class="text-center">' +
//            '                        <p class="heading">Stock ID : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
//            '                    </div>' +
//            '                    <p><span class="spc">Shape</span>:<span>"' + GalleryDatalist[i].shape + '"</span></p>' +
//            '                    <p><span class="spc">Carat</span>:<span>"' + GalleryDatalist[i].cts + '"</span></p>' +
//            '                    <p><span class="spc">Color</span>:<span>"' + GalleryDatalist[i].color + '"</span></p>' +
//            '                    <p><span class="spc">Clarity</span>:<span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
//            '                    <p><span class="spc">Cut</span>:<span>"' + GalleryDatalist[i].cut + '"</span></p>' +
//            '                    <p><span class="spc">Fls</span>:<span>"' + GalleryDatalist[i].fls + '"</span></p>' +
//            '                    <p><span class="spc">Discount (%)</span>:<span>"' + GalleryDatalist[i].sales_disc_per + '"%</span></p>' +
//            '                    <p><span class="spc">Net Amt</span>:<span>"' + GalleryDatalist[i].net_amount + '"$</span></p>' +
//            '                </div>' +
//            '            </div>' +
//            '            <div class="text-center mt-1  ">' +
////            '                <a href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">VIEW DETAIL</a>' +
//            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">VIEW DETAIL</a>' +
//            '            </div>' +
//            '        </div>' +
//            '    </div>' +
//            '    <div class="inner-text">' +
//            '        <div class="left-text">' +
//            '            <p>Ref :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
//            '            <p>Lab : <span><a href="">"' + GalleryDatalist[i].lab + '"</a></span></p>' +
//            '        </div>' +
//            '        <div class="right-text">' +
//            '            <p>Clarity : <span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
//            '            <p>Color : <span>"' + GalleryDatalist[i].color + '"</span></p>' +
//            '        </div>' +
//            '    </div>' +
//            '</div>');
//    }
//}
function BindGalleryView(TN) {
    var limit, renderlimit;
    var gallerydatalist = [];
    if (TN == 1) {
        limit = limit1;
        renderlimit = renderLimit1;
        gallerydatalist = Datalist1;
    }
    else if (TN == 2) {
        limit = limit2;
        renderlimit = renderLimit2;
        gallerydatalist = Datalist2;
    }
    else if (TN == 3) {
        limit = limit3;
        renderlimit = renderLimit3;
        gallerydatalist = Datalist3;
    }
    else if (TN == 4) {
        limit = limit4;
        renderlimit = renderLimit4;
        gallerydatalist = Datalist4;
    }
    else if (TN == 5) {
        limit = limit5;
        renderlimit = renderLimit5;
        gallerydatalist = Datalist5;
    }

    for (var i = limit; i < renderlimit; i++) {
        limit = limit + 1;
        $('#dvGalleryView' + TN).append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/content/images/no-img1.jpg" src="' + (gallerydatalist[i].bPRimg ? $("#External_ImageURL").val() + gallerydatalist[i].certi_no + "/pr.jpg" : "/content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + gallerydatalist[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + gallerydatalist[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + gallerydatalist[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + gallerydatalist[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + gallerydatalist[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + gallerydatalist[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + gallerydatalist[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Disc_Per").val() + '</span>:<span>"' + gallerydatalist[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Offer_Value_Dollar").val() + '</span>:<span>"' + gallerydatalist[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + gallerydatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + gallerydatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + gallerydatalist[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + gallerydatalist[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + gallerydatalist[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + gallerydatalist[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }

    if (TN == 1) {
        limit1 = limit;
    }
    else if (TN == 2) {
        limit2 = limit;
    }
    else if (TN == 3) {
        limit3 = limit;
    }
    else if (TN == 4) {
        limit4 = limit;
    }
    else if (TN == 5) {
        limit5 = limit;
    }
}
/*--------------------------------------------------------DATA SOURCE END--------------------------------------------------*/

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
                        str += '<th><span>' + $("#hdn_Offer_Disc_Per").val() + ' : </span></th>';
                        ComparStoneList.Disc.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.Disc[i]) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
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
                            debugger
                            if (data.Status != "1" && data.Message != "Success") {
                                toastr.error(data.Message);
                                $('.loading-overlay-image-container').hide();
                                $('.loading-overlay').hide();
                                return;
                            }
                            else {
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
/*--------------------------------------------------------SEND EMAIL START----------------------------------------------------*/
function SendMail() {
    var isValid = $('#frmSendMail').valid();
    if (!isValid) {
        return false;
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var sobj = {};
    if (CurrentTab == 1) {
        sobj = obj1;
    }
    else if (CurrentTab == 2) {
        sobj = obj2;
    }
    else if (CurrentTab == 3) {
        sobj = obj3;
    }
    else if (CurrentTab == 4) {
        sobj = obj4;
    }
    else if (CurrentTab == 5) {
        sobj = obj5;
    }

    if ($('#customRadiomail').prop('checked')) {
        sobj.StoneID = '';
    }
    else {
        var stoneno = '';
        if (CurrentTab == 1) {
            stoneno = _.pluck(gridOptions1.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 2) {
            stoneno = _.pluck(gridOptions2.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 3) {
            stoneno = _.pluck(gridOptions3.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 4) {
            stoneno = _.pluck(gridOptions4.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        else if (CurrentTab == 5) {
            stoneno = _.pluck(gridOptions5.api.getSelectedRows(), 'stone_ref_no').join(",");
        }
        sobj.StoneID = stoneno;
    }

    $.ajax({
        url: "/SearchStock/PairEmailSelectedStone",
        type: "POST",
        data: { SearchCriteria: sobj, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val() },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
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
}
function DownloadExcel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var sobj = {};
    if (CurrentTab == 1) {
        obj1.FormName = 'Pair Search';
        obj1.ActivityType = 'Excel Export';
        sobj = obj1;
    }
    else if (CurrentTab == 2) {
        obj2.FormName = 'Pair Search';
        obj2.ActivityType = 'Excel Export';
        sobj = obj2;
    }
    else if (CurrentTab == 3) {
        obj3.FormName = 'Pair Search';
        obj3.ActivityType = 'Excel Export';
        sobj = obj3;
    }
    else if (CurrentTab == 4) {
        obj4.FormName = 'Pair Search';
        obj4.ActivityType = 'Excel Export';
        sobj = obj4;
    }
    else if (CurrentTab == 5) {
        obj5.FormName = 'Pair Search';
        obj5.ActivityType = 'Excel Export';
        sobj = obj5;
    }
    sobj.PageNo = 0;

    if ($('#customRadio3').prop('checked')) {
        sobj.StoneID = ''
    }
    else {
        var stoneno = '';
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
        sobj.StoneID = stoneno;
    }

    $.ajax({
        url: "/Common/PairSearchExcelDownloadBySearchObject",
        type: "POST",
        data: sobj,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.indexOf('No record') > -1) {
                toastr.error(data);
            }
            else {
                location.href = data;
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}

function DownloadMedia() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {};
        if (CurrentTab == 1) {
            obj1.FormName = 'Pair Search';
            obj1.ActivityType = $('#hdnDownloadType').val() + ' Download';
            sobj = obj1;
        }
        else if (CurrentTab == 2) {
            obj2.FormName = 'Pair Search';
            obj2.ActivityType = $('#hdnDownloadType').val() + ' Download';
            sobj = obj2;
        }
        else if (CurrentTab == 3) {
            obj3.FormName = 'Pair Search';
            obj3.ActivityType = $('#hdnDownloadType').val() + ' Download';
            sobj = obj3;
        }
        else if (CurrentTab == 4) {
            obj4.FormName = 'Pair Search';
            obj4.ActivityType = $('#hdnDownloadType').val() + ' Download';
            sobj = obj4;
        }
        else if (CurrentTab == 5) {
            obj5.FormName = 'Pair Search';
            obj5.ActivityType = $('#hdnDownloadType').val() + ' Download';
            sobj = obj5;
        }
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/PairSearchMediaDownloadBySearchObject",
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
    } else {
        var stoneno = '';
        var count = 0;
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
                data: { StoneID: stoneno, MediaType: $('#hdnDownloadType').val(), FormName: 'Pair Search', ActivityType: $('#hdnDownloadType').val() + ' Download' },
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

        if ($scope.SaveSearchList.length == 0) {
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
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/SearchStock/SaveSearch',
        data: JSON.stringify({ SearchId: SavedSearchID, Searchname: Searchname, model: $scope.SaveSearchList }),
    }).then(function (data) {
        $('#ConfirmOrderModal').modal('hide');
        $('#ConfirmOrderWarningModal').modal('hide');
        if (data.data == "Search Save data successfully.") {
            $("#save-src-modl").modal("hide");
            toastr.success($("#hdn_Record_Save_Successfully").val());
            $scope.SearchStone();
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

var myPieChart = null;
var myLineChart = null;
var orderSummarylabels = [];
var orderSummarychartdata = [];
var stockSummarylabels = [];
var stockSummaryAmtdata = [];
var stockSummaryctsdata = [];
var stockSummarypiecedata = [];
var img_tot = 0, img_status = 0;
var SavedSearchList = [];
var SavedSearchList_1 = [];
var RecentSearchList = [];
var RecentSearchList_1 = [];

$(document).ready(function () {
    loaderShow();
    //if ($("#hdn_MessageId").val() != "" && $("#hdn_MessageShow").val().toUpperCase() != "FALSE") {
    //    $('#myModal').modal('toggle');

    //    $.ajax({
    //        url: "/User/Get_MessageMst",
    //        async: false,
    //        type: "POST",
    //        data: { Id: $("#hdn_MessageId").val() },
    //        success: function (data, textStatus, jqXHR) {
    //            if (data.Data != null && data.Data.length > 0) {
    //                $("#divModal").html(data.Data[0].Message);
    //            }
    //        },
    //        error: function (jqXHR, textStatus, errorThrown) {
    //        }
    //    });
    //}
    //else {
    //    OpenEventModel();
    //}
    GetLastLoggedin();
    //$('#EventModel').modal('show');
    $('.select2').select2();
    GetDashboardCount();
    BindYear();
    GetStockSummaryData();
    //OpenEventModel(); // comment this when message code execute
    //GetSaveSearch();
    //GetRecentSearch();
    Colla();

    //OdrHis_Smry_contentHeight();
    $(window).resize(function () {
        OdrHis_Smry_contentHeight();
    });
    GetDashboardCount_1Min();

    $('#myModal').on('hide.bs.modal', function (event) {
        CustModal();
    });
});
function OdrHis_Smry_contentHeight() {
    var winH = $(window).height();
    if (winH >= 597) {
        $(".bc-fc-c").css("font-size", "11px");
        $(".bc-fc-c").css("margin-right", "");
        $(".bc-fc-t").css("font-size", "8.5px");
        $(".bc-fc-t").css("margin-right", "");
    }
    if (winH >= 657) {
        $(".bc-fc-c").css("font-size", "12.5px");
        $(".bc-fc-c").css("margin-right", "");
        $(".bc-fc-t").css("font-size", "10.5px");
        $(".bc-fc-t").css("margin-right", "");
    }
    if (winH >= 730) {
        $(".bc-fc-c").css("font-size", "14.5px");
        $(".bc-fc-c").css("margin-right", "");
        $(".bc-fc-t").css("font-size", "12px");
        $(".bc-fc-t").css("margin-right", "");
    }
    if (winH >= 821) {
        $(".bc-fc-c").css("font-size", "15.5px");
        $(".bc-fc-c").css("margin-right", "3px");
        $(".bc-fc-t").css("font-size", "12.5px");
        $(".bc-fc-t").css("margin-right", "15px");
    }
    
    //$("#myGrid").css("height", contentHei);
}
function Colla() {
    //if ($('.cartbody.CB-show').length != 0) {
    if ($('.cartbody').css('display') == 'block') {
        //$('.cartbody').removeClass('CB-show');
        //$('.cartbody').addClass('CB-hide');
        $('.crt').removeClass('fa fa-caret-up');
        $('.crt').addClass('fa fa-caret-down');
        $(".cartbody").hide(250);
    }
    else {
        //$('.cartbody').removeClass('CB-hide');
        //$('.cartbody').addClass('CB-show');
        $('.crt').removeClass('fa fa-caret-down');
        $('.crt').addClass('fa fa-caret-up');
        //GetSaveSearch();
        GetRecentSearch();
        $(".cartbody").show(250);
    }
}
function GetSaveSearch() {
    $.ajax({
        url: "/SearchStock/Dashboard_GetSavedSearchList",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $("#Body_SavedSearch").html("");
            var SavedSearch = '';
            SavedSearchList = [];
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        SavedSearchList = data.Data;
                        for (var i = 0; i <= data.Data.length - 1; i++) {
                            SavedSearch += '<tr>';
                            SavedSearch += '<td style="font-weight: 100;color: #6f6f6f;">';
                            SavedSearch += '<i onclick="ModifySaveSearch(' + data.Data[i].iSearchId + ',\'Show\')" class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:16px;margin-top:-5px;margin-bottom:-5px;color:#003d66;"></i>';
                            SavedSearch += '&nbsp;&nbsp;<i onclick="RemoveSaveSearch(' + data.Data[i].iSearchId + ')" class="fa fa-trash-o" aria-hidden="true" style="font-size:16px;margin-top:-5px;margin-bottom:-5px;color:#d81500;"></i>';
                            SavedSearch += '</td>';
                            SavedSearch += '<td onclick="SaveSearch_LoadSearchData(' + data.Data[i].iSearchId + ',\'Show\')">' + data.Data[i].TransDate + '</td>';
                            SavedSearch += '<td onclick="SaveSearch_LoadSearchData(' + data.Data[i].iSearchId + ',\'Show\')" style="font-weight: 100;color: #6f6f6f;">' + data.Data[i].sSearchName + '</td>';
                            SavedSearch += '<td onclick="SaveSearch_LoadSearchData(' + data.Data[i].iSearchId + ',\'Show\')" style="font-weight: 100;color: #6f6f6f;">' + data.Data[i].sDescription + '</td>';
                            SavedSearch += '</tr>';
                        }
                        $("#spn_SS_NF").hide();
                        $("#tbl_SS").show();
                        $("#Body_SavedSearch").html(SavedSearch);
                    }
                    else {
                        $("#spn_SS_NF").show();
                        $("#tbl_SS").hide();
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}
function GetRecentSearch() {
    $.ajax({
        url: "/SearchStock/Dashboard_GetRecentSearchList",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $("#Body_RecentSearch").html("");
            var RecentSearch = '';
            RecentSearchList = [];
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        RecentSearchList = data.Data;
                        for (var i = 0; i <= data.Data.length - 1; i++) {
                            RecentSearch += '<tr style="height: 32px;" onclick="RecentSearch_LoadSearchData(' + data.Data[i].iTransId + ',\'Show\')">';
                            RecentSearch += '<td>' + data.Data[i].dTransDate+'</td>';
                            //RecentSearch += '<td style="font-weight: 100;color: #6f6f6f;">' + formatNumber(data.Data[i].Total_Rec) + '</td>';
                            RecentSearch += '<td style="font-weight: 100;color: #6f6f6f;">' + data.Data[i].Description + '</td>';
                            RecentSearch += '</tr>';
                        }//text-shadow: 0px 0px 1px #003d66;
                        $("#spn_RS_NF").hide();
                        $("#tbl_RS").show();
                        $("#Body_RecentSearch").html(RecentSearch);
                    }
                    else {
                        $("#spn_RS_NF").show();
                        $("#tbl_RS").hide();
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}
function RecentSearch_LoadSearchData(iTransId, type) {
    RecentSearchList_1 = [];
    for (var i = 0; i <= RecentSearchList.length - 1; i++) {
        if (RecentSearchList[i].iTransId == iTransId) {
            RecentSearchList_1 = RecentSearchList[i];
        }
    }
    //var LoadSearchList_ = _.filter(gridOptions.rowData, function (e) { return (e.iTransId == params) });
    var obj = {
        "KeyToSymbol": RecentSearchList_1.skeytosymbol,
        "FromCts": RecentSearchList_1.FROM_CTS == 0 ? "" : RecentSearchList_1.FROM_CTS,
        "ToCts": RecentSearchList_1.TO_CTS == 0 ? "" : RecentSearchList_1.TO_CTS,
        "Shape": RecentSearchList_1.SHAPE,
        "Color": RecentSearchList_1.COLOR,
        "Clarity": RecentSearchList_1.CLARITY,
        "Cut": RecentSearchList_1.CUT,
        "Lab": RecentSearchList_1.LAB,
        "Polish": RecentSearchList_1.POLISH,
        "Location": RecentSearchList_1.LOCATION,
        "Symm": RecentSearchList_1.SYMM,
        "Pointer": RecentSearchList_1.POINTER,
        "Fls": RecentSearchList_1.FLS,
        "Inclusion": RecentSearchList_1.INCLUSION,
        "Natts": RecentSearchList_1.NATTS,
        "CrownInclusion": RecentSearchList_1.CROWN_INCLUSION,
        "CrownNatts": RecentSearchList_1.CROWN_NATTS,
        "Luster": RecentSearchList_1.LUSTER,

        "FormPricePerCts": RecentSearchList_1.FROM_PRICECTS == 0 ? "" : RecentSearchList_1.FROM_PRICECTS,
        "ToPricePerCts": RecentSearchList_1.TO_PRICECTS == 0 ? "" : RecentSearchList_1.TO_PRICECTS,
        "FormDisc": RecentSearchList_1.FROM_DISC == 0 ? "" : RecentSearchList_1.FROM_DISC,
        "ToDisc": RecentSearchList_1.TO_DISC == 0 ? "" : RecentSearchList_1.TO_DISC,

        "FormNetAmt": RecentSearchList_1.FROM_NETAMT == 0 ? null : RecentSearchList_1.FROM_NETAMT,
        "ToNetAmt": RecentSearchList_1.TO_NETAMT == 0 ? null : RecentSearchList_1.TO_NETAMT,

        "FromCrownHeight": RecentSearchList_1.FROM_CR_HT == 0 ? null : RecentSearchList_1.FROM_CR_HT,
        "ToCrownHeight": RecentSearchList_1.TO_CR_HT == 0 ? null : RecentSearchList_1.TO_CR_HT,

        "FromCrownAngle": RecentSearchList_1.FROM_CR_ANG == 0 ? null : RecentSearchList_1.FROM_CR_ANG,
        "ToCrownAngle": RecentSearchList_1.TO_CR_ANG == 0 ? null : RecentSearchList_1.TO_CR_ANG,

        "FromPavHeight": RecentSearchList_1.FROM_PAV_HT == 0 ? null : RecentSearchList_1.FROM_PAV_HT,
        "ToPavHeight": RecentSearchList_1.TO_PAV_HT == 0 ? null : RecentSearchList_1.TO_PAV_HT,

        "FromPavAngle": RecentSearchList_1.FROM_PAV_ANG == 0 ? null : RecentSearchList_1.FROM_PAV_ANG,
        "ToPavAngle": RecentSearchList_1.TO_PAV_ANG == 0 ? null : RecentSearchList_1.TO_PAV_ANG,

        "FormTablePer": RecentSearchList_1.FROM_TABLE_PER == 0 ? null : RecentSearchList_1.FROM_TABLE_PER,
        "ToTablePer": RecentSearchList_1.TO_TABLE_PER == 0 ? null : RecentSearchList_1.TO_TABLE_PER,
        "FormDepthPer": RecentSearchList_1.FROM_DEPTH_PER == 0 ? null : RecentSearchList_1.FROM_DEPTH_PER,
        "ToDepthPer": RecentSearchList_1.TO_DEPTH_PER == 0 ? null : RecentSearchList_1.TO_DEPTH_PER,
        "FormLength": RecentSearchList_1.FROM_LENGTH == 0 ? null : RecentSearchList_1.FROM_LENGTH,
        "ToLength": RecentSearchList_1.TO_LENGTH == 0 ? null : RecentSearchList_1.TO_LENGTH,
        "FormWidth": RecentSearchList_1.FROM_WIDTH == 0 ? null : RecentSearchList_1.FROM_WIDTH,
        "ToWidth": RecentSearchList_1.TO_WIDTH == 0 ? null : RecentSearchList_1.TO_WIDTH,
        "FormDepth": RecentSearchList_1.FROM_DEPTH == 0 ? null : RecentSearchList_1.FROM_DEPTH,
        "ToDepth": RecentSearchList_1.TO_DEPTH == 0 ? null : RecentSearchList_1.TO_DEPTH,
        "CertiNo": RecentSearchList_1.CERTI_NO,
        "StoneID": RecentSearchList_1.STONE_REF_NO == 0 ? "" : RecentSearchList_1.STONE_REF_NO,
        "ColorType": RecentSearchList_1.ColorType,
        "Intensity": RecentSearchList_1.Intensity,
        "Overtone": RecentSearchList_1.Overtone,
        "Fancy_Color": RecentSearchList_1.Fancy_Color,
        "Table_Open": RecentSearchList_1.Table_Open,
        "Crown_Open": RecentSearchList_1.Crown_Open,
        "Pav_Open": RecentSearchList_1.Pav_Open,
        "Girdle_Open": RecentSearchList_1.Girdle_Open,
    }
    
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/RecentSearchDataSessionStore",
        async: false,
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (type == 'Show') {
                window.location.href = '/SearchStock/Search?type=Recent';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function SaveSearch_LoadSearchData(iSearchId, type) {
    SavedSearchList_1 = [];
    for (var i = 0; i <= SavedSearchList.length - 1; i++) {
        if (SavedSearchList[i].iSearchId == iSearchId) {
            SavedSearchList_1 = SavedSearchList[i];
        }
    } 
    //var LoadSearchList_ = _.filter(gridOptions.rowData, function (e) { return (e.iSearchId == params) });
    var obj = {
        "SearchID": SavedSearchList_1.iSearchId,
        "SearchName": SavedSearchList_1.sSearchName,
        "KeyToSymbol": SavedSearchList_1.SkeyToSymbol,
        "FromCts": SavedSearchList_1.dFromCts == 0 ? "" : SavedSearchList_1.dFromCts,
        "ToCts": SavedSearchList_1.dToCts == 0 ? "" : SavedSearchList_1.dToCts,
        "Shape": SavedSearchList_1.sShape,
        "Color": SavedSearchList_1.sColor,
        "Clarity": SavedSearchList_1.sClarity,
        "Cut": SavedSearchList_1.sCut,
        "Lab": SavedSearchList_1.sLab,
        "Polish": SavedSearchList_1.sPolish,
        "Location": SavedSearchList_1.location1,
        "Symm": SavedSearchList_1.sSymm,
        "Pointer": SavedSearchList_1.sPointer,
        "BGM": SavedSearchList_1.bgm,
        "Fls": SavedSearchList_1.sFls,
        "BLACK": SavedSearchList_1.black,
        "Inclusion": SavedSearchList_1.sInclusion,
        "Natts": SavedSearchList_1.sNatts,
        "CrownInclusion": SavedSearchList_1.sCrownInclusion,
        "CrownNatts": SavedSearchList_1.sCrownNatts,
        "Luster": SavedSearchList_1.LUSTER,

        "FormPricePerCts": SavedSearchList_1.dFromPriceCts == 0 ? "" : SavedSearchList_1.dFromPriceCts,
        "ToPricePerCts": SavedSearchList_1.dToPriceCts == 0 ? "" : SavedSearchList_1.dToPriceCts,
        "FormDisc": SavedSearchList_1.dFromDisc == 0 ? "" : SavedSearchList_1.dFromDisc,
        "ToDisc": SavedSearchList_1.dToDisc == 0 ? "" : SavedSearchList_1.dToDisc,

        //  "PricePerCtsTo": SavedSearchList_1.CERTI_NO,
        //  "PricePerCtsTo": SavedSearchList_1.CERTI_NO,

        "FormNetAmt": SavedSearchList_1.dFromNetPrice == 0 ? null : SavedSearchList_1.dFromNetPrice,
        "ToNetAmt": SavedSearchList_1.dToNetPrice == 0 ? null : SavedSearchList_1.dToNetPrice,

        "FromCrownHeight": SavedSearchList_1.dFromCrHt == 0 ? null : SavedSearchList_1.dFromCrHt,
        "ToCrownHeight": SavedSearchList_1.dToCrHt == 0 ? null : SavedSearchList_1.dToCrHt,

        "FromCrownAngle": SavedSearchList_1.dFromCrAng == 0 ? null : SavedSearchList_1.dFromCrAng,
        "ToCrownAngle": SavedSearchList_1.dToCrAng == 0 ? null : SavedSearchList_1.dToCrAng,

        "FromPavHeight": SavedSearchList_1.dFromPavHt == 0 ? null : SavedSearchList_1.dFromPavHt,
        "ToPavHeight": SavedSearchList_1.dToPavHt == 0 ? null : SavedSearchList_1.dToPavHt,

        "FromPavAngle": SavedSearchList_1.dFromPavAng == 0 ? null : SavedSearchList_1.dFromPavAng,
        "ToPavAngle": SavedSearchList_1.dToPavAng == 0 ? null : SavedSearchList_1.dToPavAng,

        "FormTablePer": SavedSearchList_1.dFromTablePer == 0 ? null : SavedSearchList_1.dFromTablePer,
        "ToTablePer": SavedSearchList_1.dToTablePer == 0 ? null : SavedSearchList_1.dToTablePer,
        "FormDepthPer": SavedSearchList_1.dFromDepthPer == 0 ? null : SavedSearchList_1.dFromDepthPer,
        "ToDepthPer": SavedSearchList_1.dToDepthPer == 0 ? null : SavedSearchList_1.dToDepthPer,
        "FormLength": SavedSearchList_1.dFromLength == 0 ? null : SavedSearchList_1.dFromLength,
        "ToLength": SavedSearchList_1.dToLength == 0 ? null : SavedSearchList_1.dToLength,
        "FormWidth": SavedSearchList_1.dFromWidth == 0 ? null : SavedSearchList_1.dFromWidth,
        "ToWidth": SavedSearchList_1.dToWidth == 0 ? null : SavedSearchList_1.dToWidth,
        "FormDepth": SavedSearchList_1.dFromDepth == 0 ? null : SavedSearchList_1.dFromDepth,
        "ToDepth": SavedSearchList_1.dToDepth == 0 ? null : SavedSearchList_1.dToDepth,
        "CertiNo": SavedSearchList_1.sCertiNo,
        "StoneID": SavedSearchList_1.sRefNo == 0 ? "" : SavedSearchList_1.sRefNo,
        "HasImage": SavedSearchList_1.bImage,
        "HasHDMovie": SavedSearchList_1.bHDMovie,
        "ColorType": SavedSearchList_1.ColorType,
        "Intensity": SavedSearchList_1.Intensity,
        "Overtone": SavedSearchList_1.Overtone,
        "Fancy_Color": SavedSearchList_1.Fancy_Color,
        "Table_Open": SavedSearchList_1.Table_Open,
        "Crown_Open": SavedSearchList_1.Crown_Open,
        "Pav_Open": SavedSearchList_1.Pav_Open,
        "Girdle_Open": SavedSearchList_1.Girdle_Open,
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/SavedSearchDataSessionStore",
        async: false,
        type: "POST",
        data: { obj: obj },
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (type == 'Show') {
                window.location.href = '/SearchStock/Search?type=SaveSearch';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function RemoveSaveSearch(iSearchId) {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/DeleteUserSearch",
        async: false,
        type: "POST",
        data: { SearchID: iSearchId },
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
            } else {
                //GetSaveSearch();
                toastr.success(data.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function ModifySaveSearch(iSearchId, type) {
    SavedSearchList_1 = [];
    for (var i = 0; i <= SavedSearchList.length - 1; i++) {
        if (SavedSearchList[i].iSearchId == iSearchId) {
            SavedSearchList_1 = SavedSearchList[i];
        }
    }
    //var LoadSearchList_ = _.filter(gridOptions.rowData, function (e) { return (e.iSearchId == params) });
    var obj = {
        "SearchID": SavedSearchList_1.iSearchId,
        "SearchName": SavedSearchList_1.sSearchName,
        "KeyToSymbol": SavedSearchList_1.SkeyToSymbol,
        "FromCts": SavedSearchList_1.dFromCts == 0 ? "" : SavedSearchList_1.dFromCts,
        "ToCts": SavedSearchList_1.dToCts == 0 ? "" : SavedSearchList_1.dToCts,
        "Shape": SavedSearchList_1.sShape,
        "Color": SavedSearchList_1.sColor,
        "Clarity": SavedSearchList_1.sClarity,
        "Cut": SavedSearchList_1.sCut,
        "Lab": SavedSearchList_1.sLab,
        "Polish": SavedSearchList_1.sPolish,
        "Location": SavedSearchList_1.location1,
        "Symm": SavedSearchList_1.sSymm,
        "Pointer": SavedSearchList_1.sPointer,
        "BGM": SavedSearchList_1.bgm,
        "Fls": SavedSearchList_1.sFls,
        "BLACK": SavedSearchList_1.black,
        "Inclusion": SavedSearchList_1.sInclusion,
        "Natts": SavedSearchList_1.sNatts,
        "CrownInclusion": SavedSearchList_1.sCrownInclusion,
        "CrownNatts": SavedSearchList_1.sCrownNatts,
        "Luster": SavedSearchList_1.LUSTER,
        "FormPricePerCts": SavedSearchList_1.dFromPriceCts == 0 ? "" : SavedSearchList_1.dFromPriceCts,
        "ToPricePerCts": SavedSearchList_1.dToPriceCts == 0 ? "" : SavedSearchList_1.dToPriceCts,
        "FormDisc": SavedSearchList_1.dFromDisc == 0 ? "" : SavedSearchList_1.dFromDisc,
        "ToDisc": SavedSearchList_1.dToDisc == 0 ? "" : SavedSearchList_1.dToDisc,
        "FormNetAmt": SavedSearchList_1.dFromNetPrice == 0 ? null : SavedSearchList_1.dFromNetPrice,
        "ToNetAmt": SavedSearchList_1.dToNetPrice == 0 ? null : SavedSearchList_1.dToNetPrice,
        "FromCrownHeight": SavedSearchList_1.dFromCrHt == 0 ? null : SavedSearchList_1.dFromCrHt,
        "ToCrownHeight": SavedSearchList_1.dToCrHt == 0 ? null : SavedSearchList_1.dToCrHt,
        "FromCrownAngle": SavedSearchList_1.dFromCrAng == 0 ? null : SavedSearchList_1.dFromCrAng,
        "ToCrownAngle": SavedSearchList_1.dToCrAng == 0 ? null : SavedSearchList_1.dToCrAng,
        "FromPavHeight": SavedSearchList_1.dFromPavHt == 0 ? null : SavedSearchList_1.dFromPavHt,
        "ToPavHeight": SavedSearchList_1.dToPavHt == 0 ? null : SavedSearchList_1.dToPavHt,
        "FromPavAngle": SavedSearchList_1.dFromPavAng == 0 ? null : SavedSearchList_1.dFromPavAng,
        "ToPavAngle": SavedSearchList_1.dToPavAng == 0 ? null : SavedSearchList_1.dToPavAng,
        "FormTablePer": SavedSearchList_1.dFromTablePer == 0 ? null : SavedSearchList_1.dFromTablePer,
        "ToTablePer": SavedSearchList_1.dToTablePer == 0 ? null : SavedSearchList_1.dToTablePer,
        "FormDepthPer": SavedSearchList_1.dFromDepthPer == 0 ? null : SavedSearchList_1.dFromDepthPer,
        "ToDepthPer": SavedSearchList_1.dToDepthPer == 0 ? null : SavedSearchList_1.dToDepthPer,
        "FormLength": SavedSearchList_1.dFromLength == 0 ? null : SavedSearchList_1.dFromLength,
        "ToLength": SavedSearchList_1.dToLength == 0 ? null : SavedSearchList_1.dToLength,
        "FormWidth": SavedSearchList_1.dFromWidth == 0 ? null : SavedSearchList_1.dFromWidth,
        "ToWidth": SavedSearchList_1.dToWidth == 0 ? null : SavedSearchList_1.dToWidth,
        "FormDepth": SavedSearchList_1.dFromDepth == 0 ? null : SavedSearchList_1.dFromDepth,
        "ToDepth": SavedSearchList_1.dToDepth == 0 ? null : SavedSearchList_1.dToDepth,
        "CertiNo": SavedSearchList_1.sCertiNo,
        "StoneID": SavedSearchList_1.sRefNo == 0 ? "" : SavedSearchList_1.sRefNo,
        "HasImage": SavedSearchList_1.bImage,
        "HasHDMovie": SavedSearchList_1.bHDMovie,
        "ColorType": SavedSearchList_1.ColorType,
        "Intensity": SavedSearchList_1.Intensity,
        "Overtone": SavedSearchList_1.Overtone,
        "Fancy_Color": SavedSearchList_1.Fancy_Color,
        "Table_Open": SavedSearchList_1.Table_Open,
        "Crown_Open": SavedSearchList_1.Crown_Open,
        "Pav_Open": SavedSearchList_1.Pav_Open,
        "Girdle_Open": SavedSearchList_1.Girdle_Open,
    }
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/ModifySavedSearchDataSessionStore",
        async: false,
        type: "POST",
        data: { obj: obj },
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (type == 'Show') {
                window.location.href = '/SearchStock/Search?type=Modify';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}

function OpenEventModel() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    $.ajax({
        url: "/Dashboard/OpenEventModel",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            
            //MoveToErrorPage(data.Status);
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        img_tot = data.Data.length, i = 0, html = ''; 
                        var apiUrl = $("#apiurl").val();
                        apiUrl = apiUrl.replace(/api/g, 'InfoImages/');
                        for (; i < img_tot; i++) {
                            img_status = parseInt(img_status) + 1;
                            
                            html = '<div class="modal fade" tabindex="-1" id="EventModel' + i.toString() + '" data-keyboard="false" data-backdrop="static">' +
                                '<div id="imagemodel' + i.toString() + '" class="modal-dialog modal-lg modalPopupnew" style="max-width:' + data.Data[i].NaturalWidth+'px">' +
                                '<div class="modal-content">' +
                                '<div class="modal-header">' +
                                '<div class="text-center">' +
                                data.Data[i].InformationName +
                                '</div>' +
                                '<button type="button" class="close" data-dismiss="modal" style="">×</button>' +
                                '<input id="hdnInformationID' + i.toString() + '" type="hidden" />' +
                                '</div>' +
                                '<center>' +
                                '<div class="modal-body" id="img_div">' +
                                '<img style="max-width: 100%;" id="imgEvent' + i.toString() + '" src="">' +
                                '</div>' +
                                '</center>' +
                                '<div class="modal-footer">' +
                                '<a href="javascript:void(0);" data-dismiss="modal">Skip</a>&nbsp;&nbsp;&nbsp;' +
                                '<a href="javascript:void(0);" onclick="EventAction(\'Dismiss\',' + i.toString() + ')">Dismiss</a>' +
                                //'<button type="submit" data-dismiss="modal" class="btn btn-primary button button4">Skip</button>' +
                                //'<button type="button" id="btnHideModal" onclick="EventAction(\'Dismiss\',' + i.toString() + ')" class="btn btn-primary button button4">Dismiss</button>' +
                                '</div>' +
                                '</div>' +
                                '</div>';
                            '</div>';
                            $('#divModels').append(html);

                            $("#hdnInformationID" + i.toString()).val(data.Data[i].InformationID);
                            $('#imgEvent' + i.toString()).attr('src', (apiUrl + data.Data[i].FileName));
                            $('#EventModel' + i.toString()).modal('show');
                        }
                    }
                }
            }
            else {
                window.location = GetLoginUrl();
            }
            //loaderHide();  // comment this when message code execute
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
            loaderHide();
        }
    });
}
function loaderShow() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}
function loaderHide() {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}
function formatNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

function formatNumberWithPoint(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

function ChartSearch_LoadSearchData(value) {debugger
    var obj = {};
    if ($("#ddlChartFilterType").val() == "PO") {
        obj.FromCts = value.split("-")[0];
        obj.ToCts = value.split("-")[1];
        obj.Pointer = value;
    }
    else if ($("#ddlChartFilterType").val() == "SH") {
        obj.Shape = value;
    }
    else if ($("#ddlChartFilterType").val() == "CO") {
        obj.Color = value;
    }
    else if ($("#ddlChartFilterType").val() == "CL") {
        obj.Clarity = value;
    }
    else if ($("#ddlChartFilterType").val() == "CU") {
        obj.Cut = value;
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/ChartSearchDataSessionStore",
        async: false,
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            debugger
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data == 'success') {
                window.location.href = '/SearchStock/Search?type=ChartSearch';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}


function EventAction(action, i) {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    $.ajax({
        url: "/Dashboard/EventAction",
        type: "POST",
        async: false,
        data: { Action: action, InformationID: $("#hdnInformationID" + i.toString()).val() },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    toastr.success(data.Message);
                }
                else {
                    toastr.error(data.Message);
                }
                //$('.loading-overlay-image-container').hide();
                //$('.loading-overlay').hide();
                $('#EventModel' + i.toString()).modal('hide');
            }
            else {
                window.location = GetLoginUrl();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
            //$('.loading-overlay-image-container').hide();
            //$('.loading-overlay').hide();
            $('#EventModel' + i.toString()).modal('hide');
        }
    });
}

function GetDashboardCount() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    $.ajax({
        url: "/Dashboard/GetDashboardCount",
        type: "POST",
        async: false,
        data: null,
        success: function (data, textStatus, jqXHR) {
            //$('.loading-overlay-image-container').hide();
            //$('.loading-overlay').hide();
            if (data.Data != null) {
                for (var i = 0; i < data.Data.length; i++) {
                    if (data.Data[i].Type == "NewArrival") {
                        $('#cntNewArrival').html(formatNumber(data.Data[i].sCnt));
                    }
                    else if (data.Data[i].Type == "offer") {
                        $('#cntMyOffer').html(formatNumber(data.Data[i].sCnt));
                    }
                    else if (data.Data[i].Type == "WishList") {
                        $('#cntWishlist').html(formatNumber(data.Data[i].sCnt));
                    }
                    else if (data.Data[i].Type == "MyOrder") {
                        $('#cntOrderHistory').html(formatNumber(data.Data[i].sCnt));
                    }
                    else if (data.Data[i].Type == "Stock") {
                        $('#cntSearchStock').html(formatNumber(data.Data[i].sCnt));
                    }
                    else if (data.Data[i].Type == "MyCart") {
                        $('#cntMyCart').html(formatNumber(data.Data[i].sCnt));
                    }
                    //else if (data.Data[i].Type == "PickUp") {
                    //    $('#CPickUp').html(formatNumber(data.Data[i].sCnt));
                    //}
                    //else if (data.Data[i].Type == "NotPickUp") {
                    //    $('#CNotPickUp').html(formatNumber(data.Data[i].sCnt));
                    //}
                    //else if (data.Data[i].Type == "Collected") {
                    //    $('#CCollected').html(formatNumber(data.Data[i].sCnt));
                    //}
                }
                $("#divOdr-Smry").show();
            }
            //$.each(data.Data, function (key, obj) {

            //    if (obj.Type == "NewArrival") {
            //        $('#cntNewArrival').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "offer") {
            //        $('#cntMyOffer').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "WishList") {
            //        $('#cntWishlist').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "MyOrder") {
            //        $('#cntOrderHistory').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "Stock") {
            //        $('#cntSearchStock').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "OverseasStock") {
            //        $('#cntOverseas_Stock').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "PickUp") {
            //        $('#CPickUp').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "NotPickUp") {
            //        $('#CNotPickUp').html(formatNumber(obj.sCnt));
            //    }
            //    else if (obj.Type == "Collected") {
            //        $('#CCollected').html(formatNumber(obj.sCnt));
            //    }
            //    $("#divOdr-Smry").show();
            //});
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function GetLastLoggedin() {
    $.ajax({
        url: "/Dashboard/GetLastLoggedin",
        type: "POST",
        data: { DeviceType: "Web" },
        success: function (data, textStatus, jqXHR) {
            if (data.Data.length > 0) {
                $("#divLastLogged").show();
                $("#sLastLogged").html(data.Data[0].LoginDate);
                $("#divTotalBx").removeClass("bx-hgt");
            }
            else {
                $("#divLastLogged").hide();
                $("#divTotalBx").addClass("bx-hgt");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function BindDynamicChart() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();

    if (myPieChart != null) {
        myPieChart.destroy();
    }
    var type = $('#ddlChartType').val();
    var ctxP = document.getElementById("pieChart").getContext('2d');

    var canvas = document.getElementById("pieChart");

    var chartdata = [];
    if ($("#lnkamount").hasClass("active")) {
        chartdata = stockSummaryAmtdata;
    }
    else {
        //chartdata = stockSummaryctsdata
        chartdata = stockSummarypiecedata
    }
    if (type == "pie" || type == "doughnut" || type == "bar") {
        myPieChart = new Chart(ctxP, {
            type: type,
            data: {
                labels: stockSummarylabels,
                datasets: [{
                    label: (type == "pie" || type == "doughnut") ? stockSummarylabels : "Stock Summary",
                    data: chartdata,
                    backgroundColor: ['rgba(108, 158, 237, 1)', 'rgba(236, 122, 27, 1)', 'rgba(242, 194, 44, 1)', 'rgba(45, 122, 62, 1)', 'rgba(7, 59, 128, 1)', 'rgba(17, 234, 252, 1)', 'rgba(50, 68, 96, 1)', 'rgba(46, 165, 186, 1)', 'rgba(174, 229, 239, 1)', 'rgba(219, 237, 64, 1)', 'rgba(95, 102, 38, 1)', 'rgba(224, 181, 112, 1)', 'rgba(239, 219, 186, 1)', 'rgba(216, 21, 0, 1)', 'rgba(224, 133, 123, 1)', 'rgba(247, 241, 128, 1)', 'rgba(78, 18, 102, 1)', 'rgba(191, 102, 226, 1)', 'rgba(125, 237, 186, 1)', 'rgba(218, 2, 242, 1)', 'rgba(244, 12, 194, 1)', 'rgba(199, 102, 227, 1)', 'rgba(129, 237, 187, 1)', 'rgba(217, 2, 247, 1)', 'rgba(246, 12, 199, 1)'],
                    borderColor: ['rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)'],
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: (type == "pie" || type == "doughnut") ? 'right' : 'bottom',
                    responsive: true,
                    maintainAspectRatio: false,
                    onResize: null,
                },
                scales: {
                    yAxes: [{
                        display: (type == "pie" || type == "doughnut") ? false : true,
                        ticks: {
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                if (type == "pie" || type == "doughnut") {
                                    return null;
                                }
                                else if (!$("#lnkamount").hasClass("active")) {
                                    return value;
                                }
                                else {
                                    return '$' + value.toLocaleString('en-US');
                                }
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            if ($("#lnkamount").hasClass("active")) {
                                return (' ' + data['datasets'][0]['label'][tooltipItem['index']] + ' - $'
                                    + formatNumberWithPoint(data['datasets'][0]['data'][tooltipItem['index']]));
                            }
                            else {
                                return (' ' + data['datasets'][0]['label'][tooltipItem['index']] + ' - '
                                    + data['datasets'][0]['data'][tooltipItem['index']]
                                    + ' Piece');
                            }
                        },
                    }
                }
            }
        });
    } else {
        myPieChart = new Chart(ctxP, {
            type: type,
            data: {
                labels: stockSummarylabels,
                datasets: [{
                    label: "Stock Summary",
                    data: chartdata,
                    borderColor: ['rgba(108, 158, 237, 1)', 'rgba(236, 122, 27, 1)', 'rgba(242, 194, 44, 1)', 'rgba(45, 122, 62, 1)', 'rgba(7, 59, 128, 1)', 'rgba(17, 234, 252, 1)', 'rgba(50, 68, 96, 1)', 'rgba(46, 165, 186, 1)', 'rgba(174, 229, 239, 1)', 'rgba(219, 237, 64, 1)', 'rgba(95, 102, 38, 1)', 'rgba(224, 181, 112, 1)', 'rgba(239, 219, 186, 1)', 'rgba(216, 21, 0, 1)', 'rgba(224, 133, 123, 1)', 'rgba(247, 241, 128, 1)', 'rgba(78, 18, 102, 1)', 'rgba(191, 102, 226, 1)', 'rgba(125, 237, 186, 1)', 'rgba(218, 2, 242, 1)', 'rgba(244, 12, 194, 1)', 'rgba(199, 102, 227, 1)', 'rgba(129, 237, 187, 1)', 'rgba(217, 2, 247, 1)', 'rgba(246, 12, 199, 1)'],
                    borderWidth: 1,
                    fill: false,
                    borderCapStyle: 'square'
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: 'bottom',
                    responsive: true,
                    maintainAspectRatio: false,
                    onResize: null,
                },
                scales: {
                    yAxes: [{
                        display: (type == "pie" || type == "doughnut") ? false : true,
                        ticks: {
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                if (type == "pie" || type == "doughnut") {
                                    return null;
                                }
                                else if (!$("#lnkamount").hasClass("active")) {
                                    return value;
                                }
                                else {
                                    return '$' + value.toLocaleString('en-US');
                                }
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            if ($("#lnkamount").hasClass("active")) {
                                return (' $'
                                    + formatNumberWithPoint(data['datasets'][0]['data'][tooltipItem['index']]));
                            }
                            else {
                                return (data['datasets'][0]['data'][tooltipItem['index']] + ' Piece');
                            }
                        },
                    }
                }
            }
        });
    }

    canvas.onclick = function (evt) {
        var activePoints = myPieChart.getElementsAtEvent(evt);
        if (activePoints[0]) {
            var chartData = activePoints[0]['_chart'].config.data;
            var idx = activePoints[0]['_index'];
            var label = chartData.labels[idx];
            var value = chartData.datasets[0].data[idx];
            console.log(label);
            console.log(value);debugger
            if (label != "") {
                ChartSearch_LoadSearchData(label);
            }
        }
    };
    //$('#pieChart').css('width', '587px');
    //$('#pieChart').css('height', '293px');
    //$('.loading-overlay-image-container').hide();
    //$('.loading-overlay').hide();
}

function GetStockSummaryData() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    stockSummarylabels = [];
    stockSummaryAmtdata = [];
    stockSummaryctsdata = [];
    stockSummarypiecedata = [];
    $.ajax({
        url: "/Dashboard/GetDynamicChartData",
        type: "POST",
        data: { para: $('#ddlChartFilterType').val() },
        success: function (data, textStatus, jqXHR) {
            MoveToErrorPage(data.Status);
            $.each(data.Data, function (key, obj) {
                stockSummarylabels.push(obj.para);
                stockSummaryAmtdata.push(obj.amount);//amount
                //stockSummaryctsdata.push(obj.cts);
                stockSummarypiecedata.push(obj.TotalPiece);
            });
            BindDynamicChart();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function GetOrderSummaryData() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    orderSummarylabels = [];
    orderSummarychartdata = [];
    $.ajax({
        url: "/Dashboard/GetOrderSummaryChartData",
        type: "POST",
        data: { yearID: $('#ddlYear').val() },
        success: function (data, textStatus, jqXHR) {
            MoveToErrorPage(data.Status);
            $.each(data.Data, function (key, obj) {
                orderSummarylabels.push(obj.OrderMonth);
                orderSummarychartdata.push(obj.NetPrice.toFixed(0));//Cts
            });
            BindOrderSummaryChart();
            //$('.loading-overlay-image-container').hide();
            //$('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function BindOrderSummaryChart() {
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();
    if (myLineChart != null) {
        myLineChart.destroy();
    }
    var type = $('#ddlOrderChartType').val();
    var ctxL = document.getElementById("lineChart").getContext('2d');
    var canvas = document.getElementById("lineChart");

    if (type == "pie" || type == "doughnut" || type == "bar") {
        myLineChart = new Chart(ctxL, {
            type: type,
            data: {
                labels: orderSummarylabels,
                datasets: [{
                    label: (type == "pie" || type == "doughnut") ? orderSummarylabels : "Order Summary",
                    data: orderSummarychartdata,
                    backgroundColor: ['rgba(108, 158, 237, 1)', 'rgba(236, 122, 27, 1)', 'rgba(242, 194, 44, 1)', 'rgba(45, 122, 62, 1)', 'rgba(7, 59, 128, 1)', 'rgba(17, 234, 252, 1)', 'rgba(50, 68, 96, 1)', 'rgba(46, 165, 186, 1)', 'rgba(174, 229, 239, 1)', 'rgba(219, 237, 64, 1)', 'rgba(95, 102, 38, 1)', 'rgba(224, 181, 112, 1)', 'rgba(239, 219, 186, 1)', 'rgba(216, 21, 0, 1)', 'rgba(224, 133, 123, 1)', 'rgba(247, 241, 128, 1)', 'rgba(78, 18, 102, 1)', 'rgba(191, 102, 226, 1)', 'rgba(125, 237, 186, 1)', 'rgba(218, 2, 242, 1)', 'rgba(244, 12, 194, 1)', 'rgba(199, 102, 227, 1)', 'rgba(129, 237, 187, 1)', 'rgba(217, 2, 247, 1)', 'rgba(246, 12, 199, 1)'],
                    borderColor: ['rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)', 'rgba(255, 255, 255, 1)'],
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: (type == "pie" || type == "doughnut") ? 'right' : 'bottom',
                    responsive: true,
                    maintainAspectRatio: false,
                    onResize: null,
                },
                scales: {
                    yAxes: [{
                        display: (type == "pie" || type == "doughnut") ? false : true,
                        ticks: {
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                if (type == "pie" || type == "doughnut") {
                                    return null;
                                } else {
                                    return '$' + value.toLocaleString('en-US');
                                }
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            if (data['datasets'][0]['label'] == "Order Summary") {
                                return ('$' + formatNumber(data['datasets'][0]['data'][tooltipItem['index']]));
                            }
                            else {
                                return (' ' + data['datasets'][0]['label'][tooltipItem['index']] + ' - $' + formatNumber(data['datasets'][0]['data'][tooltipItem['index']]));
                            }
                        },
                    }
                }
            }
        });
    }
    else {
        myLineChart = new Chart(ctxL, {
            type: type,
            data: {
                labels: orderSummarylabels,
                datasets: [{
                    label: "Order Summary",
                    data: orderSummarychartdata,
                    borderColor: ['rgba(108, 158, 237, 1)', 'rgba(236, 122, 27, 1)', 'rgba(242, 194, 44, 1)', 'rgba(45, 122, 62, 1)', 'rgba(7, 59, 128, 1)', 'rgba(17, 234, 252, 1)', 'rgba(50, 68, 96, 1)', 'rgba(46, 165, 186, 1)', 'rgba(174, 229, 239, 1)', 'rgba(219, 237, 64, 1)', 'rgba(95, 102, 38, 1)', 'rgba(224, 181, 112, 1)', 'rgba(239, 219, 186, 1)', 'rgba(216, 21, 0, 1)', 'rgba(224, 133, 123, 1)', 'rgba(247, 241, 128, 1)', 'rgba(78, 18, 102, 1)', 'rgba(191, 102, 226, 1)', 'rgba(125, 237, 186, 1)', 'rgba(218, 2, 242, 1)', 'rgba(244, 12, 194, 1)', 'rgba(199, 102, 227, 1)', 'rgba(129, 237, 187, 1)', 'rgba(217, 2, 247, 1)', 'rgba(246, 12, 199, 1)'],
                    borderWidth: 1,
                    fill: false,
                    borderCapStyle: 'square'
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: 'bottom',
                    responsive: true,
                    maintainAspectRatio: false,
                    onResize: null,
                },
                scales: {
                    yAxes: [{
                        display: (type == "pie" || type == "doughnut") ? false : true,
                        ticks: {
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                if (type == "pie" || type == "doughnut") {
                                    return null;
                                } else {
                                    return '$' + value.toLocaleString('en-US');
                                }
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return '$' + formatNumber(data['datasets'][0]['data'][tooltipItem['index']]);
                        },
                    }
                }
            }
        });
    }

    canvas.onclick = function (evt) {
        var activePoints = myLineChart.getElementsAtEvent(evt);
        if (activePoints[0]) {
            var chartData = activePoints[0]['_chart'].config.data;
            var idx = activePoints[0]['_index'];
            var label = chartData.labels[idx];
            var value = chartData.datasets[0].data[idx];
            console.log(label);
            console.log(value); debugger
            if (label != "") {
                ChartSearch_LoadSearchData(label);
            }
        }
    };

    loaderHide();   // un comment this when message code execute
    // $('#lineChart').css('width', '587px');
    //$('#lineChart').css('height', '293px');
    //$('.loading-overlay-image-container').hide();
    //$('.loading-overlay').hide();
}
function setActiveclass(e) {
    if ($(e).attr('id') == 'lnkcarat') {
        $('#lnkcarat').addClass('active');
        $('#lnkamount').removeClass('active');
    } else {
        $('#lnkcarat').removeClass('active');
        $('#lnkamount').addClass('active');
    }
    BindDynamicChart();
}
function setActiveclass(e) {
    if ($(e).attr('id') == 'lnkcarat') {
        $('#lnkcarat').addClass('active');
        $('#lnkamount').removeClass('active');
    } else {
        $('#lnkcarat').removeClass('active');
        $('#lnkamount').addClass('active');
    }
    BindDynamicChart();
}
function BindYear() {
    $('#ddlYear').html("");
    //$('.loading-overlay-image-container').show();
    //$('.loading-overlay').show();

    $.ajax({
        url: "/Dashboard/GetYearData",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            MoveToErrorPage(data.Status);
            $.each(data.Data, function (key, obj) {
                $('#ddlYear').append("<option value='" + obj.iYearId + "'>" + obj.sYear + "</option>");
            });

            GetOrderSummaryData();
            //$('.loading-overlay-image-container').hide();
            //$('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function redirectPage(page) {
    var url = "";
    if (page == "SearchStock") {
        url = "/SearchStock/Search";
    }
    else if (page == "Order") {
        url = "/Order/OrderHistory";
    }
    else if (page == "MyCart") {
        url = "/Cart";
    }
    else if (page == "NewArrival") {
        if ($("#cntNewArrival").html() != "0") {
            url = "/SearchStock/Search?type=NewArrival";
        }
        else {
            toastr.warning("New Arrival Stock Not Found !");
        }
    }
    else if (page == "Offer") {
        if (('@SunriseWeb.Helper.SessionFacade.UserSession'.toLowerCase() != 'null') &&
            ('@SunriseWeb.Helper.SessionFacade.UserSession.isadmin' == '1' || '@SunriseWeb.Helper.SessionFacade.UserSession.isemp' == '1')) {
            toastr.error('This facility is not available for you');
        }
        else {
            url = "/Offer/Index";
        }
    }
    else if (page == "Wishlist") {
        if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1)
            url = "/Wishlist/Admin_Wishlist";
        else
            url = "/Wishlist/Index";
    }
    else if (page == "OverseasStock") {
        url = "/SearchStock/OverseasSearch";
    }
    if (url != "") {
        location.href = url;
    }
}
function GetDashboardCount_1Min() {
    setInterval(function () {
        GetDashboardCount();
    }, 60 * 1000); // 60 * 1000 milsec
}
function CustModal() {
    if ($("#hdn_IsLogout").val().toUpperCase() == "TRUE") {
        var url = $("#hdn_LogoutURL").val();
        if (url != "") {
            window.location.href = url;
        }
    }
    else {
        $.ajax({
            url: "/Login/CustModalOK",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    }
}

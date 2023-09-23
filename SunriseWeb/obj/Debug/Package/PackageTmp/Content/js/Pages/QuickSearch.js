var ColorArr = [];
var ClarityArr = [];
var arrIsfncy = [];
var str = '';
var arr = [];
var IsFirst = true;
$(document).ready(function () {
    upper_convert();
    GetSearchParameter();
    BindData();
});
function upper_convert() {
    $("#sp_color").html($("#sp_color").html().toUpperCase());
    $("#sp_clarity").html($("#sp_clarity").html().toUpperCase());
}
function BindData() {
    $('#tbodyquicksearch').html("");
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    var FormName = '', ActivityType = '';
    if (IsFirst == true) {
        FormName = 'Quick Search';
        ActivityType = 'Get';
    }
    $.ajax({
        url: "/SearchStock/GetQuickSearchList",
        async: false,
        type: "POST",
        data: { cut: $('#ddlCut').val().join(","), Fls: $('#ddlFls').val().join(","), FormName: FormName, ActivityType: ActivityType  },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            IsFirst = false;
            var QuickDataList = [];
            if (data.Data.length > 0) {
                var QuickDataList = data.Data;
                if (QuickDataList.length > 0) {
                    _.each(QuickDataList, function (itm) {
                        $('#tbodyquicksearch').append('<tr>' +
                            '<td onclick="popup_open(\'p_018_022\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_018_022 + ');"><span>' + (itm.p_018_022 == 0 ? "-" : itm.p_018_022) + '</span></td>' +
                            '<td onclick="popup_open(\'p_023_029\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_023_029 + ');"><span>' + (itm.p_023_029 == 0 ? "-" : itm.p_023_029) + '</span></td>' +
                            '<td onclick="popup_open(\'p_030_039\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_030_039 + ');"><span>' + (itm.p_030_039 == 0 ? "-" : itm.p_030_039) + '</span></td>' +
                            '<td onclick="popup_open(\'p_040_049\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_040_049 + ');"><span>' + (itm.p_040_049 == 0 ? "-" : itm.p_040_049) + '</span></td>' +
                            '<td onclick="popup_open(\'p_050_059\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_050_059 + ');"><span>' + (itm.p_050_059 == 0 ? "-" : itm.p_050_059) + '</span></td>' +
                            '<td onclick="popup_open(\'p_060_069\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_060_069 + ');"><span>' + (itm.p_060_069 == 0 ? "-" : itm.p_060_069) + '</span></td>' +
                            '<td onclick="popup_open(\'p_070_079\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_070_079 + ');"><span>' + (itm.p_070_079 == 0 ? "-" : itm.p_070_079) + '</span></td>' +
                            '<td onclick="popup_open(\'p_080_089\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_080_089 + ');"><span>' + (itm.p_080_089 == 0 ? "-" : itm.p_080_089) + '</span></td>' +
                            '<td onclick="popup_open(\'p_090_099\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_090_099 + ');"><span>' + (itm.p_090_099 == 0 ? "-" : itm.p_090_099) + '</span></td>' +
                            '<td onclick="popup_open(\'p_100_119\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_100_119 + ');"><span>' + (itm.p_100_119 == 0 ? "-" : itm.p_100_119) + '</span></td>' +
                            '<td onclick="popup_open(\'p_120_149\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_120_149 + ');"><span>' + (itm.p_120_149 == 0 ? "-" : itm.p_120_149) + '</span></td>' +
                            '<td onclick="popup_open(\'p_150_199\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_150_199 + ');"><span>' + (itm.p_150_199 == 0 ? "-" : itm.p_150_199) + '</span></td>' +
                            '<td onclick="popup_open(\'p_200_299\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_200_299 + ');"><span>' + (itm.p_200_299 == 0 ? "-" : itm.p_200_299) + '</span></td>' +
                            '<td onclick="popup_open(\'p_300_9999\',' + itm.col_grp_sr + ',' + itm.purity_grp_sr + ',' + itm.p_300_9999 + ');"><span>' + (itm.p_300_9999 == 0 ? "-" : itm.p_300_9999) + '</span></td>' +
                            '</tr>');
                    });
                }
            }
            else {
                toastr.error(data.Data.Message, 2500);
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
function popup_open(Pointer, ColorGrp, PurityGrp, itm) {
    ColorArr = [];
    ClarityArr = [];
    arrIsfncy = [];
    arr = [];
    $('#tbodyQuickSearchInner').html("");
    if (itm != "-") {
        $('#quick-search-main').modal('show');

        if (Pointer == "p_018_022") {
            Pointer = "0.18-0.22";
        }
        if (Pointer == "p_023_029") {
            Pointer = "0.23-0.29";
        }
        if (Pointer == "p_030_039") {
            Pointer = "0.30-0.39";
        }
        if (Pointer == "p_040_049") {
            Pointer = "0.40-0.49";
        }
        if (Pointer == "p_050_059") {
            Pointer = "0.50-0.59";
        }
        if (Pointer == "p_060_069") {
            Pointer = "0.60-0.69";
        }
        if (Pointer == "p_070_079") {
            Pointer = "0.70-0.79";
        }
        if (Pointer == "p_080_089") {
            Pointer = "0.80-0.89";
        }
        if (Pointer == "p_090_099") {
            Pointer = "0.90-0.99";
        }
        if (Pointer == "p_100_119") {
            Pointer = "1.00-1.19";
        }
        if (Pointer == "p_120_149") {
            Pointer = "1.20-1.49";
        }
        if (Pointer == "p_150_199") {
            Pointer = "1.50-1.99";
        }
        if (Pointer == "p_200_299") {
            Pointer = "2.00-2.99";
        }
        if (Pointer == "p_300_9999") {
            Pointer = "3.00-99.99";
        }
        $('#modalheader').html(Pointer);
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/GetSubQuickSearchData",
            async: false,
            type: "POST",
            data: { Pointer: Pointer, ColorGroup: ColorGrp, PurityGroup: PurityGrp, cut: $('#ddlCut').val().join(","), Fls: $('#ddlFls').val().join(",") },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    var SubQuickDataList = data.Data;
                    var total_len = 0, total_len1 = 0;
                    
                    if (SubQuickDataList.length > 0) {
                        total_len = SubQuickDataList.length;
                        var str = '';
                        _.each(SubQuickDataList, function (itm) {
                            total_len1 = parseInt(total_len1) + 1;

                            str = str + '<tr>';
                            str = str + '<td>' + itm.Shape_Color + '</td>';
                            if (itm.Is_Header == 1) {
                                if (itm.IS_FL == 1) {
                                    str = str + '<td>FL</td>';
                                }
                                if (itm.IS_IF == 1) {
                                    str = str + '<td>IF</td>';
                                }
                                if (itm.IS_VVS1 == 1) {
                                    str = str + '<td>VVS1</td>';
                                }
                                if (itm.IS_VVS2 == 1) {
                                    str = str + '<td>VVS2</td>';
                                }
                                if (itm.IS_VS1 == 1) {
                                    str = str + '<td>VS1</td>';
                                }
                                if (itm.IS_VS2 == 1) {
                                    str = str + '<td>VS2</td>';
                                }
                                if (itm.IS_SI1 == 1) {
                                    str = str + '<td>SI1</td>';
                                }
                                if (itm.IS_SI2 == 1) {
                                    str = str + '<td>SI2</td>';
                                }
                                if (itm.IS_I1 == 1) {
                                    str = str + '<td>I1</td>';
                                }
                                if (itm.IS_I2 == 1) {
                                    str = str + '<td>I2</td>';
                                }
                                if (itm.IS_I3 == 1) {
                                    str = str + '<td>I3</td>';
                                }
                                str = str + '<td>Total</td>';
                            } else {
                                if (itm.IS_FL == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'FL\',' + itm.FL + ',this)">' + itm.FL + '</td>';
                                }
                                if (itm.IS_IF == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'IF\',' + itm.IF + ',this)">' + itm.IF + '</td>';
                                }
                                if (itm.IS_VVS1 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'VVS1\',' + itm.VVS1 + ',this)">' + itm.VVS1 + '</td>';
                                }
                                if (itm.IS_VVS2 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'VVS2\',' + itm.VVS2 + ',this)">' + itm.VVS2 + '</td>';
                                }
                                if (itm.IS_VS1 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'VS1\',' + itm.VS1 + ',this)">' + itm.VS1 + '</td>';
                                }
                                if (itm.IS_VS2 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'VS2\',' + itm.VS2 + ',this)">' + itm.VS2 + '</td>';
                                }
                                if (itm.IS_SI1 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'SI1\',' + itm.SI1 + ',this)">' + itm.SI1 + '</td>';
                                }
                                if (itm.IS_SI2 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'SI2\',' + itm.SI2 + ',this)">' + itm.SI2 + '</td>';
                                }
                                if (itm.IS_I1 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'I1\',' + itm.I1 + ',this)">' + itm.I1 + '</td>';
                                }
                                if (itm.IS_I2 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'I2\',' + itm.I2 + ',this)">' + itm.I2 + '</td>';
                                }
                                if (itm.IS_I3 == 1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'I3\',' + itm.I3 + ',this)">' + itm.I3 + '</td>';
                                }
                                
                                var _tot_color = '';
                                if (itm.IS_FL == 1) {
                                    _tot_color = commaset(_tot_color) + 'FL';
                                }
                                if (itm.IS_IF == 1) {
                                    _tot_color = commaset(_tot_color) + 'IF';
                                }
                                if (itm.IS_VVS1 == 1) {
                                    _tot_color = commaset(_tot_color) + 'VVS1';
                                }
                                if (itm.IS_VVS2 == 1) {
                                    _tot_color = commaset(_tot_color) + 'VVS2';
                                }
                                if (itm.IS_VS1 == 1) {
                                    _tot_color = commaset(_tot_color) + 'VS1';
                                }
                                if (itm.IS_VS2 == 1) {
                                    _tot_color = commaset(_tot_color) + 'VS2';
                                }
                                if (itm.IS_SI1 == 1) {
                                    _tot_color = commaset(_tot_color) + 'SI1';
                                }
                                if (itm.IS_SI2 == 1) {
                                    _tot_color = commaset(_tot_color) + 'SI2';
                                }
                                if (itm.IS_I1 == 1) {
                                    _tot_color = commaset(_tot_color) + 'I1';
                                }
                                if (itm.IS_I2 == 1) {
                                    _tot_color = commaset(_tot_color) + 'I2';
                                }
                                if (itm.IS_I3 == 1) {
                                    _tot_color = commaset(_tot_color) + 'I3';
                                }
                                //str = str + '<td>' + itm.Total + '</td>';

                                if (total_len == total_len1) {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'' + _tot_color + '\',' + itm.Total + ',this)">' + itm.Total + '</td>';
                                }
                                else {
                                    str = str + '<td onclick="SelectPara(\'' + itm.Is_Fancy + '\',\'' + itm.Shape_Color + '\',\'' + _tot_color + '\',' + itm.Total + ',this)">' + itm.Total + '</td>';
                                }
                                
                            }
                            str = str + '</tr>';

                            //// class="tab-light-bg"
                            ////if (itm.Shape_Color == "ROUND" || itm.Shape_Color == "FANCY" || itm.Shape_Color == "TOTAL") {
                            //if (itm.Shape_Color == "ROUND") {
                            //    $('.quik-src .iner-tbl table tbody tr td').addClass('tab-light-bg');
                            //    //if (Color == "ROUND" && Color == "FANCY" && Color == "TOTAL") {

                            //}
                            //if (itm.Shape_Color == "FANCY") {
                            //    $('.quik-src .iner-tbl table tbody tr td').addClass('tab-light-bg');
                            //    //if (Color == "ROUND" && Color == "FANCY" && Color == "TOTAL") {

                            //}
                            //if (itm.Shape_Color == "TOTAL") {
                            //    $('.quik-src .iner-tbl table tbody tr td').addClass('tab-light-bg');
                            //    //if (Color == "ROUND" && Color == "FANCY" && Color == "TOTAL") {

                            //}
                        });
                        $('#tbodyQuickSearchInner').html(str);
                    }
                }
                else {
                    toastr.error(data.Message, 2500);
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
function commaset(value) {
    if (value != '') {
        return value = value + ',';
    }
    else {
        return value;
    }
}
function Submit() {
    $('#quick-search-main').modal('hide');
    if (ColorArr.length > 0 && ClarityArr.length > 0 && arrIsfncy.length > 0) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        var colorLst = _.uniq(ColorArr).join(",");
        var ClarityLst = _.uniq(ClarityArr).join(",");
        var ShapeLst = _.uniq(arrIsfncy).join(",");
        var ShapeColorPurityLst = _.uniq(arr).join(",");
        var obj = {
            "Pointer": $('#modalheader').html(),
            "CaratType": 'General',
            "Shape": ShapeLst,
            "Color": colorLst,
            "Clarity": ClarityLst,
            "ShapeColorPurity": ShapeColorPurityLst,
            "Cut": $('#ddlCut').val().join(","),
            "Fls": $('#ddlFls').val().join(",")
        };
        $.ajax({
            url: "/SearchStock/SetFinalQuickSearchData",
            async: false,
            type: "POST",
            data: obj,
            success: function (data, textStatus, jqXHR) {
                window.location.href = '/SearchStock/Search?type=QuickSearch';
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
        toastr.warning($("#hdn_Please_Select_Atleast_One_Criteria_For_Search").val() +'!')
    }
}
function SelectPara(Isfancy, Color, Clarity, item, obj) {
    if (item > 0) {
        if (Color != "ROUND" && Color != "FANCY") { //  && Color != "TOTAL"
            if ($(obj).hasClass('active')) {
                $(obj).removeClass('active');
                if (arr.length > 0) {
                    arr.forEach(function (item) {
                        if (item == str) {
                            var I = (arr.indexOf(item));
                            arr.splice(I, 1);
                        }
                    });
                }
                if (arrIsfncy.length > 0) {
                    arrIsfncy.forEach(function (item) {
                        if (item == Isfancy) {
                            var I = (arrIsfncy.indexOf(item));
                            arrIsfncy.splice(I, 1);
                        }
                    });
                }
                if (ColorArr.length > 0) {
                    var I = -1;
                    ColorArr.forEach(function (item, i) {
                        if (Color == "TOTAL") {
                            $("#tbodyQuickSearchInner").find("tr").each(function (i, obj) {
                                if ($($(obj).find('td')[0]).text() != 'ROUND' && $($(obj).find('td')[0]).text() != 'FANCY'
                                    && $($(obj).find('td')[0]).text() != 'TOTAL') {
                                    if (item == $($(obj).find('td')[0]).text()) {
                                        I = (ColorArr.indexOf(item));
                                        ColorArr.splice(I, 1);
                                    }
                                }
                            });
                        }
                        else {
                            if (item == Color) {
                                I = (ColorArr.indexOf(item));
                                ColorArr.splice(I, 1);
                            }
                        }
                    });
                }
                if (ClarityArr.length > 0) {
                    ClarityArr.forEach(function (item, i) {
                        if (item == Clarity) {
                            var I = (ClarityArr.indexOf(item));
                            ClarityArr.splice(I, 1);
                        }
                    });
                }
            }
            else {
                //var str = Isfancy + ':' + Color + ':' + Clarity;
                var _str = '', str_array = Clarity.split(',');

                var i = 0;
                for (i = 0; i < str_array.length; i++) {
                    // Trim the excess whitespace.
                    str_array[i] = str_array[i].replace(/^\s*/, "").replace(/\s*$/, "");

                    if (Color == "TOTAL") {
                        $("#tbodyQuickSearchInner").find("tr").each(function (k, obj) {
                            _str = '';
                            if ($($(obj).find('td')[0]).text() != 'ROUND' && $($(obj).find('td')[0]).text() != 'FANCY' && $($(obj).find('td')[0]).text() != 'TOTAL') {
                                _str = commaset(_str) + Isfancy + ':' + $($(obj).find('td')[0]).text() + ':' + str_array[i];
                                arr.push(_str);
                            }
                        });
                    }
                    else {
                        _str = commaset(_str) + Isfancy + ':' + Color + ':' + str_array[i];
                        arr.push(_str);
                    }
                }

                $(obj).addClass('active');
                //arr.push(_str);
                arrIsfncy.push(Isfancy);

                if (Color == "TOTAL") {
                    $("#tbodyQuickSearchInner").find("tr").each(function (k, obj) {
                        if ($($(obj).find('td')[0]).text() != 'ROUND' && $($(obj).find('td')[0]).text() != 'FANCY' && $($(obj).find('td')[0]).text() != 'TOTAL') {
                            ColorArr.push($($(obj).find('td')[0]).text());
                        }
                    });
                }
                else {
                    ColorArr.push(Color);
                }

                ClarityArr.push(Clarity);
            }
        }
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
            ParameterList = data.Data;
            if (ParameterList.length > 0) {
                _.each(ParameterList, function (itm) {
                    itm.ACTIVE = false;
                });
            }

            $('#ddlCut').html("");
            var CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
            _(CutList).each(function (cut, i) {
                $('#ddlCut').append('<option value="' + cut.Value + '">' + cut.Value + '</option>');
            });
            $('#ddlCut').multiselect({
                includeSelectAllOption: true
            });

            $('#ddlFls').html("");
            var FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });
            _(FlouList).each(function (fls, i) {
                $('#ddlFls').append('<option value="' + fls.Value + '">' + fls.Value + '</option>');
            });
            $('#ddlFls').multiselect({
                includeSelectAllOption: true
            });

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
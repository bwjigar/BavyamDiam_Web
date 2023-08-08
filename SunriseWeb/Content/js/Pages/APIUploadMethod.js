$(document).ready(function () {
    $("input[name$='ApiMethod']").click(function () {
        var Name = $(this).val();

        $(".Div-Method").hide();
        $("#APIMethod" + Name).show();
    });
    //$("input[name$='AllChck']").click(function () {
    //    if ($("#AllChck").prop("checked") == true) {
    //        $(".chebox-fill").removeClass('img-none');
    //        $(".chebox-fill").addClass('img-block');

    //        $(".chebox-empty").addClass('img-none');
    //        $(".chebox-empty").removeClass('img-block');
    //    }
    //    else {
    //        $(".chebox-fill").addClass('img-none');
    //        $(".chebox-fill").removeClass('img-block');

    //        $(".chebox-empty").removeClass('img-none');
    //        $(".chebox-empty").addClass('img-block');
    //    }
    //});
    BindKeyToSymbolList();
    $('.sym-sec').on('click', function () {
        $('.sym-sec').toggleClass('active');
    });
    $("#btnAddNewRow").click(function () {
        AddNewRow();
    });

    if (parseInt($("#mytable1 #myTableBody1").find('tr').length) > 0) {
        $("#mytable1").show();
    }

    $("#mytable1").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#mytable1 #myTableBody1").find('tr').length) == 0) {
            $("#mytable1").hide();
        }
        var idd = 1;
        $("#mytable1 #myTableBody1 tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });

});
function chebox_fill(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_fillImg_Header").removeClass('img-block');
        $("#chebox_fillImg_Header").addClass('img-none');

        $("#chebox_emptyImg_Header").removeClass('img-none');
        $("#chebox_emptyImg_Header").addClass('img-block');

        $(".chebox-fill").addClass('img-none');
        $(".chebox-fill").removeClass('img-block');

        $(".chebox-empty").removeClass('img-none');
        $(".chebox-empty").addClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).addClass('img-none');
        $("#chebox_fillImg_" + icolumnId).removeClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).removeClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).addClass('img-block');
    }
}
function chebox_empty(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_emptyImg_Header").removeClass('img-block');
        $("#chebox_emptyImg_Header").addClass('img-none');

        $("#chebox_fillImg_Header").removeClass('img-none');
        $("#chebox_fillImg_Header").addClass('img-block');

        $(".chebox-fill").removeClass('img-none');
        $(".chebox-fill").addClass('img-block');

        $(".chebox-empty").addClass('img-none');
        $(".chebox-empty").removeClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).removeClass('img-none');
        $("#chebox_fillImg_" + icolumnId).addClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).addClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).removeClass('img-block');
    }
}

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

function UserDDL() {
    if ($("#For_iUserId").val() != "") {
        loaderShow();
        var data = {};
        data.iUserid = $("#For_iUserId").val()

        $.ajax({
            type: 'POST',
            url: '/Api/UserwiseCompany_select',
            data: data,
            dataType: "json",
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    $("#sCompany").html(data.Data[0].CompanyName);
                } else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
                toastr.error(textStatus);
            }
        });
    }
    else {
        $("#sCompany").html("");
    }
}

function FTPTypeDDL() {
    if ($("#FTPType").val() == "FTP") {
        $("#FTPHost").val("ftp://");
    }
    else {
        $("#FTPHost").val("");
    }
}
function PricingMethodDDL() {
    if ($("#PricingMethod").val() != "") {
        document.getElementById("Percentage").disabled = false;
    }
    else {
        $("#Percentage").val("0");
        document.getElementById("Percentage").disabled = true;
    }
}
function loaderShow() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

function loaderHide() {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}
function Reset_API_Filter() {
    $("#Vendor").val("");
    ResetSelectedAttr('.divCheckedLocationValue', LocationList);
    ResetSelectedAttr('.divCheckedShapeValue', ShapeList);
    ResetCheckCarat();
    ResetSelectedAttr('.divCheckedColorValue', ColorList);
    ResetSelectedAttr('.divCheckedClarityValue', ClarityList);
    ResetSelectedAttr('.divCheckedCutValue', CutList);
    ResetSelectedAttr('.divCheckedPolValue', PolishList);
    ResetSelectedAttr('.divCheckedSymValue', SymmList);
    ResetSelectedAttr('.divCheckedFLsValue', FlsList);
    ResetSelectedAttr('.divCheckedLabValue', LabList);
    $("#FromLength").val("0.00");
    $("#ToLength").val("0.00");
    $("#FromWidth").val("0.00");
    $("#ToWidth").val("0.00");
    $("#FromDepth").val("0.00");
    $("#ToDepth").val("0.00");
    $("#FromDepthPer").val("0.00");
    $("#ToDepthPer").val("0.00");
    $("#FromTablePer").val("0.00");
    $("#ToTablePer").val("0.00");
    $("#FromCrAng").val("0.00");
    $("#ToCrAng").val("0.00");
    $("#FromCrHt").val("0.00");
    $("#ToCrHt").val("0.00");
    $("#FromPavAng").val("0.00");
    $("#ToPavAng").val("0.00");
    $("#FromPavHt").val("0.00");
    $("#ToPavHt").val("0.00");
    resetKeytoSymbol();
    ResetSelectedAttr('.divCheckedBGMValue', BgmList);
    ResetSelectedAttr('.divCheckedCrnBlackValue', CrnBlackList);
    ResetSelectedAttr('.divCheckedTblBlackValue', TblBlackList);
    ResetSelectedAttr('.divCheckedCrnWhiteValue', CrnWhiteList);
    ResetSelectedAttr('.divCheckedTblWhiteValue', TblWhiteList);
    $("#PricingMethod").val("");
    $("#Percentage").val("0");
    document.getElementById("Percentage").disabled = true;
    $(".IgAll").prop("checked", true);
    $(".VdAll").prop("checked", true);
    $("#aLocationselection").show();
    $("#aLocationrefresh").show();
}
var GetSelectedCarat = function () {
    CheckedCaratValue = "";
    IsCaratFT = false;
    var FromCarat = "";
    var ToCarat = "";
    var Carat = null;

    if ($("#CARAT_Size2").is(":checked") == true) {
        var FROM_VAL = $("#FromSize2").val() == undefined ? "0.18" : $("#FromSize2").val() == "" ? "0.18" : $("#FromSize2").val();
        var TO_VAL = $("#ToSize2").val() == undefined ? "0.22" : $("#ToSize2").val() == "" ? "0.22" : $("#ToSize2").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 1, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 1) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 1) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size3").is(":checked") == true) {
        var FROM_VAL = $("#FromSize3").val() == undefined ? "0.23" : $("#FromSize3").val() == "" ? "0.23" : $("#FromSize3").val();
        var TO_VAL = $("#ToSize3").val() == undefined ? "0.29" : $("#ToSize3").val() == "" ? "0.29" : $("#ToSize3").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 2, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 2) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 2) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size4").is(":checked") == true) {
        var FROM_VAL = $("#FromSize4").val() == undefined ? "0.30" : $("#FromSize4").val() == "" ? "0.30" : $("#FromSize4").val();
        var TO_VAL = $("#ToSize4").val() == undefined ? "0.39" : $("#ToSize4").val() == "" ? "0.39" : $("#ToSize4").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 3, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 3) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 3) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size5").is(":checked") == true) {
        var FROM_VAL = $("#FromSize5").val() == undefined ? "0.40" : $("#FromSize5").val() == "" ? "0.40" : $("#FromSize5").val();
        var TO_VAL = $("#ToSize5").val() == undefined ? "0.49" : $("#ToSize5").val() == "" ? "0.49" : $("#ToSize5").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 4, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 4) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 4) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size6").is(":checked") == true) {
        var FROM_VAL = $("#FromSize6").val() == undefined ? "0.50" : $("#FromSize6").val() == "" ? "0.50" : $("#FromSize6").val();
        var TO_VAL = $("#ToSize6").val() == undefined ? "0.59" : $("#ToSize6").val() == "" ? "0.59" : $("#ToSize6").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 5, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 5) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 5) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size7").is(":checked") == true) {
        var FROM_VAL = $("#FromSize7").val() == undefined ? "0.60" : $("#FromSize7").val() == "" ? "0.60" : $("#FromSize7").val();
        var TO_VAL = $("#ToSize7").val() == undefined ? "0.69" : $("#ToSize7").val() == "" ? "0.69" : $("#ToSize7").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 6, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 6) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 6) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size8").is(":checked") == true) {
        var FROM_VAL = $("#FromSize8").val() == undefined ? "0.70" : $("#FromSize8").val() == "" ? "0.70" : $("#FromSize8").val();
        var TO_VAL = $("#ToSize8").val() == undefined ? "0.79" : $("#ToSize8").val() == "" ? "0.79" : $("#ToSize8").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 7, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 7) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 7) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size9").is(":checked") == true) {
        var FROM_VAL = $("#FromSize9").val() == undefined ? "0.80" : $("#FromSize9").val() == "" ? "0.80" : $("#FromSize9").val();
        var TO_VAL = $("#ToSize9").val() == undefined ? "0.89" : $("#ToSize9").val() == "" ? "0.89" : $("#ToSize9").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 8, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 8) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 8) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size10").is(":checked") == true) {
        var FROM_VAL = $("#FromSize10").val() == undefined ? "0.90" : $("#FromSize10").val() == "" ? "0.90" : $("#FromSize10").val();
        var TO_VAL = $("#ToSize10").val() == undefined ? "0.99" : $("#ToSize10").val() == "" ? "0.99" : $("#ToSize10").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 9, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 9) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 9) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size11").is(":checked") == true) {
        var FROM_VAL = $("#FromSize11").val() == undefined ? "1.00" : $("#FromSize11").val() == "" ? "1.00" : $("#FromSize11").val();
        var TO_VAL = $("#ToSize11").val() == undefined ? "1.19" : $("#ToSize11").val() == "" ? "1.19" : $("#ToSize11").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 10, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 10) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 10) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size12").is(":checked") == true) {
        var FROM_VAL = $("#FromSize12").val() == undefined ? "1.20" : $("#FromSize12").val() == "" ? "1.20" : $("#FromSize12").val();
        var TO_VAL = $("#ToSize12").val() == undefined ? "1.49" : $("#ToSize12").val() == "" ? "1.49" : $("#ToSize12").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 11, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 11) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 11) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size13").is(":checked") == true) {
        var FROM_VAL = $("#FromSize13").val() == undefined ? "1.50" : $("#FromSize13").val() == "" ? "1.50" : $("#FromSize13").val();
        var TO_VAL = $("#ToSize13").val() == undefined ? "1.99" : $("#ToSize13").val() == "" ? "1.99" : $("#ToSize13").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 12, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 12) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 12) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size15").is(":checked") == true) {
        var FROM_VAL = $("#FromSize15").val() == undefined ? "2.00" : $("#FromSize15").val() == "" ? "2.00" : $("#FromSize15").val();
        var TO_VAL = $("#ToSize15").val() == undefined ? "2.99" : $("#ToSize15").val() == "" ? "2.99" : $("#ToSize15").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 13, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 13) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 13) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size17").is(":checked") == true) {
        var FROM_VAL = $("#FromSize17").val() == undefined ? "3.00" : $("#FromSize17").val() == "" ? "3.00" : $("#FromSize17").val();
        var TO_VAL = $("#ToSize17").val() == undefined ? "99.99" : $("#ToSize17").val() == "" ? "99.99" : $("#ToSize17").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 14, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 14) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 14) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    SSN_CARAT.forEach(function (e) {
        CheckedCaratValue = CheckedCaratValue + e.Carat + ",";
    });
    CheckedCaratValue = CheckedCaratValue.replace(/,\s*$/, "");

    if (CheckedCaratValue != "") {

        $(".divCheckedCaratValue").empty();
        $(".divCheckedCaratValue").append(CheckedCaratValue);
    }

    SetSearchParameter();
}

var ResetCheckCarat = function () {
    SSN_CARAT = [];
    CheckedCaratValue = '';

    $('#CaratModal input:checkbox').prop('checked', false);
    $(".divCheckedCaratValue").empty();
}
var ResetSelectedAttr = function (attr, obj) {
    _.each(obj, function (itm) {
        itm.isActive = false;
    });
    $(attr).empty();
}

var ModalShow = function (ParameterLabel, ObjLst) {
    $('#exampleModalLabel').text(ParameterLabel);
    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var list = [];
    list = ObjLst;
    var a = 'ShapeList';
    //if (ParameterLabel == 'Shape') {
    //    list = ShapeList;
    //}
    //if (ParameterLabel == 'Color') {
    //    list = ColorList;
    //}
    //if (ParameterLabel == 'Clarity') {
    //    list = ClarityList;
    //}
    //if (ParameterLabel == 'Lab') {
    //    list = LabList;
    //}
    list.forEach(function (item) {
        content += '<li onclick="ItemClicked(\'' + ParameterLabel + '\',\'' + item.sName + '\', this);" class="';
        if (item.isActive) {
            content += 'active';
        }
        content += '">' + item.sName + '</li>';
    });

    content += '</ul>';
    $('#divModal').empty();
    $('#divModal').append(content);

    $("#mpdal-footer").append('<button type="button" class="btn btn-primary" ng-click="ResetSelectedAttr(' + ParameterLabel + ');">Reset</button><button type="button" class="btn btn-secondary" data-dismiss="modal">Done</button>');

    $('#myModal').modal('toggle');
}
var ItemClicked = function (ParameterLabel, item, curritem) {
    var list = [];
    //   list = ObjLst;
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
        list = SymmList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
    if (ParameterLabel == 'Fls') {
        list = FlsList;
    }
    if (ParameterLabel == 'Bgm') {
        list = BgmList;
    }
    if (ParameterLabel == 'CrownBlack') {
        list = CrnBlackList;
    }
    if (ParameterLabel == 'TableBlack') {
        list = TblBlackList;
    }
    if (ParameterLabel == 'CrownWhite') {
        list = CrnWhiteList;
    }
    if (ParameterLabel == 'TableWhite') {
        list = TblWhiteList;
    } 
    var itm = _.find(list, function (i) { return i.sName == item });
    itm.isActive = !itm.isActive;
    $(curritem).toggleClass('active');
    SetSearchParameter();
}

var LeaveTextBox = function (ele, type) {

    if (type == "CARAT") {
        $("#FromCarat").val($("#FromCarat").val() == "" ? "0.00" : $("#FromCarat").val() == undefined || $("#FromCarat").val() == "NAN" ? "0.00" : parseFloat($("#FromCarat").val()).toFixed(2));
        $("#ToCarat").val($("#ToCarat").val() == "" ? "0.00" : $("#ToCarat").val() == undefined || $("#ToCarat").val() == "NAN" ? "0.00" : parseFloat($("#ToCarat").val()).toFixed(2));

        var fromCarat = parseFloat($("#FromCarat").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCarat").val()).toFixed(2);
        var toCarat = parseFloat($("#ToCarat").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCarat").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCarat).toFixed(2) > parseFloat(toCarat).toFixed(2)) {
                $("#ToCarat").val(fromCarat);
                if (fromCarat == 0) {
                    $("#FromCarat").val(0);
                    $("#ToCarat").val(0);
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCarat).toFixed(2) < parseFloat(fromCarat).toFixed(2)) {
                $("#FromCarat").val($("#ToCarat").val());
                if (toCarat == 0) {
                    $("#FromCarat").val(0);
                    $("#ToCarat").val(0);
                }
            }
        }
    }
    if (type == "LENGTH") {
        $("#FromLength").val($("#FromLength").val() == "" ? "0.00" : $("#FromLength").val() == undefined ? "0.00" : parseFloat($("#FromLength").val()).toFixed(2));
        $("#ToLength").val($("#ToLength").val() == "" ? "0.00" : $("#ToLength").val() == undefined ? "0.00" : parseFloat($("#ToLength").val()).toFixed(2));

        var fromLength = parseFloat($("#FromLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromLength").val()).toFixed(2);
        var toLength = parseFloat($("#ToLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToLength").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromLength).toFixed(2) > parseFloat(toLength).toFixed(2)) {
                $("#ToLength").val(fromLength);
                if (fromLength == 0) {
                    $("#FromLength").val(0);
                    $("#ToLength").val(0);
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toLength).toFixed(2) < parseFloat(fromLength).toFixed(2)) {
                $("#FromLength").val($("#ToLength").val());
                if (toLength == 0) {
                    $("#FromLength").val(0);
                    $("#ToLength").val(0);
                }
            }
        }
    }
    if (type == "WIDTH") {
        $("#FromWidth").val($("#FromWidth").val() == "" ? "0.00" : $("#FromWidth").val() == undefined ? "0.00" : parseFloat($("#FromWidth").val()).toFixed(2));
        $("#ToWidth").val($("#ToWidth").val() == "" ? "0.00" : $("#ToWidth").val() == undefined ? "0.00" : parseFloat($("#ToWidth").val()).toFixed(2));

        var fromWidth = parseFloat($("#FromWidth").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromWidth").val()).toFixed(2);
        var toWidth = parseFloat($("#ToWidth").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToWidth").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromWidth).toFixed(2) > parseFloat(toWidth).toFixed(2)) {
                $("#ToWidth").val(fromWidth);
                if (fromWidth == 0) {
                    $("#FromWidth").val("0.00");
                    $("#ToWidth").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toWidth).toFixed(2) < parseFloat(fromWidth).toFixed(2)) {
                $("#FromWidth").val($("#ToWidth").val());
                if (toWidth == 0) {
                    $("#FromWidth").val("0.00");
                    $("#ToWidth").val("0.00");
                }
            }
        }
    }
    if (type == "DEPTH") {
        $("#FromDepth").val($("#FromDepth").val() == "" ? "0.00" : $("#FromDepth").val() == undefined ? "0.00" : parseFloat($("#FromDepth").val()).toFixed(2));
        $("#ToDepth").val($("#ToDepth").val() == "" ? "0.00" : $("#ToDepth").val() == undefined ? "0.00" : parseFloat($("#ToDepth").val()).toFixed(2));

        var fromDepth = parseFloat($("#FromDepth").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromDepth").val()).toFixed(2);
        var toDepth = parseFloat($("#ToDepth").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDepth").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromDepth).toFixed(2) > parseFloat(toDepth).toFixed(2)) {
                $("#ToDepth").val(fromDepth);
                if (fromDepth == 0) {
                    $("#FromDepth").val("0.00");
                    $("#ToDepth").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDepth).toFixed(2) < parseFloat(fromDepth).toFixed(2)) {
                $("#FromDepth").val($("#ToDepth").val());
                if (toDepth == 0) {
                    $("#FromDepth").val("0.00");
                    $("#ToDepth").val("0.00");
                }
            }
        }
    }
    if (type == "DEPTHPER") {
        $("#FromDepthPer").val($("#FromDepthPer").val() == "" ? "0.00" : $("#FromDepthPer").val() == undefined ? "0.00" : parseFloat($("#FromDepthPer").val()).toFixed(2));
        $("#ToDepthPer").val($("#ToDepthPer").val() == "" ? "0.00" : $("#ToDepthPer").val() == undefined ? "0.00" : parseFloat($("#ToDepthPer").val()).toFixed(2));

        var fromDepthPer = parseFloat($("#FromDepthPer").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromDepthPer").val()).toFixed(2);
        var toDepthPer = parseFloat($("#ToDepthPer").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDepthPer").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromDepthPer).toFixed(2) > parseFloat(toDepthPer).toFixed(2)) {
                $("#ToDepthPer").val(fromDepthPer);
                if (fromDepthPer == 0) {
                    $("#FromDepthPer").val("0.00");
                    $("#ToDepthPer").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDepthPer).toFixed(2) < parseFloat(fromDepthPer).toFixed(2)) {
                $("#FromDepthPer").val($("#ToDepthPer").val());
                if (toDepthPer == 0) {
                    $("#FromDepthPer").val("0.00");
                    $("#ToDepthPer").val("0.00");
                }
            }
        }

    }
    if (type == "TABLEPER") {
        $("#FromTablePer").val($("#FromTablePer").val() == "" ? "0.00" : $("#FromTablePer").val() == undefined ? "0.00" : parseFloat($("#FromTablePer").val()).toFixed(2));
        $("#ToTablePer").val($("#ToTablePer").val() == "" ? "0.00" : $("#ToTablePer").val() == undefined ? "0.00" : parseFloat($("#ToTablePer").val()).toFixed(2));

        var fromTablePer = parseFloat($("#FromTablePer").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromTablePer").val()).toFixed(2);
        var toTablePer = parseFloat($("#ToTablePer").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToTablePer").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromTablePer).toFixed(2) > parseFloat(toTablePer).toFixed(2)) {
                $("#ToTablePer").val(fromTablePer);
                if (fromTablePer == 0) {
                    $("#FromTablePer").val("0.00");
                    $("#ToTablePer").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toTablePer).toFixed(2) < parseFloat(fromTablePer).toFixed(2)) {
                $("#FromTablePer").val($("#ToTablePer").val());
                if (toTablePer == 0) {
                    $("#FromTablePer").val("0.00");
                    $("#ToTablePer").val("0.00");
                }
            }
        }
    }
    if (type == "CRANG") {
        $("#FromCrAng").val($("#FromCrAng").val() == "" ? "0.00" : $("#FromCrAng").val() == undefined ? "0.00" : parseFloat($("#FromCrAng").val()).toFixed(2));
        $("#ToCrAng").val($("#ToCrAng").val() == "" ? "0.00" : $("#ToCrAng").val() == undefined ? "0.00" : parseFloat($("#ToCrAng").val()).toFixed(2));

        var fromCrAng = parseFloat($("#FromCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrAng").val()).toFixed(2);
        var toCrAng = parseFloat($("#ToCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCrAng").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCrAng).toFixed(2) > parseFloat(toCrAng).toFixed(2)) {
                $("#ToCrAng").val(fromCrAng);
                if (fromCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToCrAng").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCrAng).toFixed(2) < parseFloat(fromCrAng).toFixed(2)) {
                $("#FromCrAng").val($("#ToCrAng").val());
                if (toCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToCrAng").val("0.00");
                }
            }
        }
    }
    if (type == "CRHT") {
        $("#FromCrHt").val($("#FromCrHt").val() == "" ? "0.00" : $("#FromCrHt").val() == undefined ? "0.00" : parseFloat($("#FromCrHt").val()).toFixed(2));
        $("#ToCrHt").val($("#ToCrHt").val() == "" ? "0.00" : $("#ToCrHt").val() == undefined ? "0.00" : parseFloat($("#ToCrHt").val()).toFixed(2));

        var fromCrHt = parseFloat($("#FromCrHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrHt").val()).toFixed(2);
        var toCrHt = parseFloat($("#ToCrHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCrHt").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCrHt).toFixed(2) > parseFloat(toCrHt).toFixed(2)) {
                $("#ToCrHt").val(fromCrHt);
                if (fromCrHt == 0) {
                    $("#FromCrHt").val("0.00");
                    $("#ToCrHt").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCrHt).toFixed(2) < parseFloat(fromCrHt).toFixed(2)) {
                $("#FromCrHt").val($("#ToCrHt").val());
                if (toCrHt == 0) {
                    $("#FromCrHt").val("0.00");
                    $("#ToCrHt").val("0.00");
                }
            }
        }
    }
    if (type == "PAVANG") {
        $("#FromPavAng").val($("#FromPavAng").val() == "" ? "0.00" : $("#FromPavAng").val() == undefined ? "0.00" : parseFloat($("#FromPavAng").val()).toFixed(2));
        $("#ToPavAng").val($("#ToPavAng").val() == "" ? "0.00" : $("#ToPavAng").val() == undefined ? "0.00" : parseFloat($("#ToPavAng").val()).toFixed(2));

        var fromPavAng = parseFloat($("#FromPavAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromPavAng").val()).toFixed(2);
        var toPavAng = parseFloat($("#ToPavAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToPavAng").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromPavAng).toFixed(2) > parseFloat(toPavAng).toFixed(2)) {
                $("#ToPavAng").val(fromPavAng);
                if (fromPavAng == 0) {
                    $("#FromPavAng").val("0.00");
                    $("#ToPavAng").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toPavAng).toFixed(2) < parseFloat(fromPavAng).toFixed(2)) {
                $("#FromPavAng").val($("#ToPavAng").val());
                if (toPavAng == 0) {
                    $("#FromPavAng").val("0.00");
                    $("#ToPavAng").val("0.00");
                }
            }
        }

    }
    if (type == "PAVHT") {
        $("#FromPavHt").val($("#FromPavHt").val() == "" ? "0.00" : $("#FromPavHt").val() == undefined ? "0.00" : parseFloat($("#FromPavHt").val()).toFixed(2));
        $("#ToPavHt").val($("#ToPavHt").val() == "" ? "0.00" : $("#ToPavHt").val() == undefined ? "0.00" : parseFloat($("#ToPavHt").val()).toFixed(2));

        var fromPavHt = parseFloat($("#FromPavHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromPavHt").val()).toFixed(2);
        var toPavHt = parseFloat($("#ToPavHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToPavHt").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromPavHt).toFixed(2) > parseFloat(toPavHt).toFixed(2)) {
                $("#ToPavHt").val(fromPavHt);
                if (fromPavHt == 0) {
                    $("#FromPavHt").val("0.00");
                    $("#ToPavHt").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toPavHt).toFixed(2) < parseFloat(fromPavHt).toFixed(2)) {
                $("#FromPavHt").val($("#ToPavHt").val());
                if (toPavHt == 0) {
                    $("#FromPavHt").val("0.00");
                    $("#ToPavHt").val("0.00");
                }
            }
        }
    }
    if (type == "DISC") {
        $("#FromCrAng").val($("#FromCrAng").val() == "" ? "0.00" : $("#FromCrAng").val() == undefined ? "0.00" : parseFloat($("#FromCrAng").val()).toFixed(2));
        $("#ToDisc").val($("#ToDisc").val() == "" ? "0.00" : $("#ToDisc").val() == undefined ? "0.00" : parseFloat($("#ToDisc").val()).toFixed(2));

        var FromCrAng = parseFloat($("#FromCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrAng").val()).toFixed(2);
        var toDisc = parseFloat($("#ToDisc").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDisc").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(FromCrAng).toFixed(2) > parseFloat(toDisc).toFixed(2)) {
                $("#ToDisc").val(FromCrAng);
                if (FromCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToDisc").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDisc).toFixed(2) < parseFloat(FromCrAng).toFixed(2)) {
                $("#FromCrAng").val($("#ToDisc").val());
                if (toDisc == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToDisc").val("0.00");
                }
            }
        }
    }
}


function BindKeyToSymbolList() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetKeyToSymbolList",
        async: false,
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
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
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
function resetKeytoSymbol() {
    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
}
var Reset = function () {
    window.location = '/Api/UploadMethod';
}

var Clear = function () {
    Reset();
}
var Upload_Method_View = function () {
    window.location.href = '/Api/UploadMethodGet';
}

var CreateModel = function () {
    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");
    var Locationselected = [];
    $.each($("#Location").select2('data'), function (key, item) {
        Locationselected.push(item.text);
    });
    

    var obj = {
        "ApiMethod": $('#ApiMethod:checked').val(),
        "WebAPIUserName": $('#ApiMethod:checked').val() == "WEBAPI" ? $("#WebAPIUserName").val() == "" && $("#WebAPIUserName").val() == undefined ? "" : $("#WebAPIUserName").val() : "",
        "WebAPIPassword": $('#ApiMethod:checked').val() == "WEBAPI" ? $("#WebAPIPassword").val() == "" && $("#WebAPIPassword").val() == undefined ? "" : $("#WebAPIPassword").val() : "",
        "FTPHost": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPHost").val() == "" && $("#FTPHost").val() == undefined ? "" : $("#FTPHost").val() : "",
        "FTPUser": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPUser").val() == "" && $("#FTPUser").val() == undefined ? "" : $("#FTPUser").val() : "",
        "FTPPass": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPPass").val() == "" && $("#FTPPass").val() == undefined ? "" : $("#FTPPass").val() : "",
        "FTPType": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPType").val() == "" && $("#FTPType").val() == undefined ? "" : $("#FTPType").val() : "",
        "FTPExportType": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPExportType").val() == "" && $("#FTPExportType").val() == undefined ? "" : $("#FTPExportType").val() : "",
        "URLUserName": $('#ApiMethod:checked').val() == "URL" ? $("#URLUserName").val() == "" && $("#URLUserName").val() == undefined ? "" : $("#URLUserName").val() : "",
        "URLPassword": $('#ApiMethod:checked').val() == "URL" ? $("#URLPassword").val() == "" && $("#URLPassword").val() == undefined ? "" : $("#URLPassword").val() : "",
        "URLExportType": $('#ApiMethod:checked').val() == "URL" ? $("#URLExportType").val() == "" && $("#URLExportType").val() == undefined ? "" : $("#URLExportType").val() : "",

        "iVendor": $("#Vendor").val() == "" && $("#Vendor").val() == undefined ? "" : $("#Vendor").val(),
        "iLocation": _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'sName').join(","), //Locationselected.join(','), //$("#Location").select2('val').join(','),
        "sShape": _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sPointer": CheckedCaratValue,
        "sColor": _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sClarity": _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sCut": _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'sName').join(","),// $scope.CutObj,              
        "sPolish": _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sSymm": _.pluck(_.filter(SymmList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sFls": _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sLab": _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'sName').join(","),
        "dFromLength": $("#FromLength").val() == "" || $("#FromLength").val() == undefined || ($("#FromLength").val() == "0.00" && $("#FromLength").val() == "0.00") ? "" : $("#FromLength").val(),
        "dToLength": $("#ToLength").val() == "" || $("#ToLength").val() == undefined || $("#ToLength").val() == "0.00" ? "" : $("#ToLength").val(),
        "dFromWidth": $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined || ($("#FromWidth").val() == "0.00" && $("#FromWidth").val() == "0.00") ? "0.00" : $("#FromWidth").val(),
        "dToWidth": $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined || $("#ToWidth").val() == "0.00" ? "" : $("#ToWidth").val(),
        "dFromDepth": $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined || ($("#FromDepth").val() == "0.00" && $("#FromDepth").val() == "0.00") ? "0.00" : $("#FromDepth").val(),
        "dToDepth": $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined || $("#ToDepth").val() == "0.00" ? "" : $("#ToDepth").val(),
        "dFromDepthPer": $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined || ($("#FromDepthPer").val() == "0.00" && $("#FromDepthPer").val() == "0.00") ? "0.00" : $("#FromDepthPer").val(),
        "dToDepthPer": $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined || $("#ToDepthPer").val() == "0.00" ? "" : $("#ToDepthPer").val(),
        "dFromTablePer": $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined || ($("#FromTablePer").val() == "0.00" && $("#FromTablePer").val() == "0.00") ? "0.00" : $("#FromTablePer").val(),
        "dToTablePer": $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined || $("#ToTablePer").val() == "0.00" ? "" : $("#ToTablePer").val(),
        "dFromCrAng": $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined || ($("#FromCrAng").val() == "0.00" && $("#FromCrAng").val() == "0.00") ? "0.00" : $("#FromCrAng").val(),
        "dToCrAng": $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined || $("#ToCrAng").val() == "0.00" ? "" : $("#ToCrAng").val(),
        "dFromCrHt": $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined || ($("#FromCrHt").val() == "0.00" && $("#FromCrHt").val() == "0.00") ? "0.00" : $("#FromCrHt").val(),
        "dToCrHt": $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined || $("#ToCrHt").val() == "0.00" ? "" : $("#ToCrHt").val(),
        "dFromPavAng": $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined || ($("#FromPavAng").val() == "0.00" && $("#FromPavAng").val() == "0.00") ? "0.00" : $("#FromPavAng").val(),
        "dToPavAng": $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined || $("#ToPavAng").val() == "0.00" ? "" : $("#ToPavAng").val(),
        "dFromPavHt": $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined || ($("#FromPavHt").val() == "0.00" && $("#FromPavHt").val() == "0.00") ? "0.00" : $("#FromPavHt").val(),
        "dToPavHt": $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined || $("#ToPavHt").val() == "0.00" ? "" : $("#ToPavHt").val(),
        "dKeyToSymbol": KeyToSymLst_Check + (KeyToSymLst_Check == "" || KeyToSymLst_uncheck == "" ? "" : "-") + KeyToSymLst_uncheck,
        "dCheckKTS": KeyToSymLst_Check,
        "dUNCheckKTS": KeyToSymLst_uncheck,
        "sBGM": _.pluck(_.filter(BgmList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sCrownBlack": _.pluck(_.filter(CrnBlackList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sTableBlack": _.pluck(_.filter(TblBlackList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sCrownWhite": _.pluck(_.filter(CrnWhiteList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sTableWhite": _.pluck(_.filter(TblWhiteList, function (e) { return e.isActive == true }), 'sName').join(","),
        "Img": $('#Img:checked').val(),
        "Vdo": $('#Vdo:checked').val(),
        "PriceMethod": $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val(),
        "PricePer": $("#PricingMethod").val() == "" || $("#Percentage").val() == "" || $("#Percentage").val() == undefined ? "" : $("#Percentage").val(),
        "APIName": $("#APIName").val() == "" && $("#APIName").val() == undefined ? "" : $("#APIName").val(),
        "APIStatus": document.getElementById("APIStatus").checked,
        "iTransId": $("#TransId").val(),
        "For_iUserId": $("#For_iUserId").val()
    };
    return obj;
}
var SaveApiData = function () {
    if ($('#ApiMethod:checked').val() == "WEBAPI") {
        if ($("#WebAPIUserName").val() == undefined || $("#WebAPIUserName").val() == "") {
            toastr.warning("Please Enter User Name !", { timeOut: 2500 });
            $("#WebAPIUserName").focus();
            return;
        }
        if ($("#WebAPIPassword").val() == undefined || $("#WebAPIPassword").val() == "") {
            toastr.warning("Please Enter Password !", { timeOut: 2500 });
            $("#WebAPIPassword").focus();
            return;
        }
    }
    if ($('#ApiMethod:checked').val() == "FTP") {
        if ($("#FTPType").val() == undefined || $("#FTPType").val() == "") {
            toastr.warning("Please Enter Type !", { timeOut: 2500 });
            $("#FTPType").focus();
            return;
        }
        if ($("#FTPHost").val() == undefined || $("#FTPHost").val() == "") {
            toastr.warning("Please Enter FTP Host !", { timeOut: 2500 });
            $("#FTPHost").focus();
            return;
        }
        if ($("#FTPUser").val() == undefined || $("#FTPUser").val() == "") {
            toastr.warning("Please Enter FTP Uname !", { timeOut: 2500 });
            $("#FTPUser").focus();
            return;
        }
        if ($("#FTPPass").val() == undefined || $("#FTPPass").val() == "") {
            toastr.warning("Please Enter FTP Password !", { timeOut: 2500 });
            $("#FTPPass").focus();
            return;
        }
        if ($("#FTPExportType").val() == undefined || $("#FTPExportType").val() == "") {
            toastr.warning("Please Enter Export Type !", { timeOut: 2500 });
            $("#FTPExportType").focus();
            return;
        }
    }
    if ($('#ApiMethod:checked').val() == "URL") {
        if ($("#URLUserName").val() == undefined || $("#URLUserName").val() == "") {
            toastr.warning("Please Enter User Name !", { timeOut: 2500 });
            $("#URLUserName").focus();
            return;
        }
        if ($("#URLPassword").val() == undefined || $("#URLPassword").val() == "") {
            toastr.warning("Please Enter Password !", { timeOut: 2500 });
            $("#URLPassword").focus();
            return;
        }
        if ($("#URLExportType").val() == undefined || $("#URLExportType").val() == "") {
            toastr.warning("Please Enter Export Type !", { timeOut: 2500 });
            $("#URLExportType").focus();
            return;
        }
    }
    if ($("#APIName").val() == undefined || $("#APIName").val() == "") {
        toastr.warning("Please Enter File Name !", { timeOut: 2500 });
        $("#APIName").focus();
        return;
    }
    if ($("#For_iUserId").val() == undefined || $("#For_iUserId").val() == "") {
        toastr.warning("Please Select User !", { timeOut: 2500 });
        $("#For_iUserId").focus();
        return;
    }
    if ($("#mytable1 #myTableBody1").find('tr').length == 0) {
        toastr.warning("Please Add Minimum 1 API Filter !", { timeOut: 2500 });
        return;
    }

    loaderShow();

    var Arr1 = [];
    var Arr2 = [];
    $("#mytable tbody tr").each(function () {
        var Index = $(this).index();
        var icolumnId = $(this).find("td:eq(4)").html().trim();
        var ColumnName = $(this).find("td:eq(2)").html().trim();
        var EditColumnName = $(this).find("input").val();
        //var Visibility = $(this).find("#checkbox2").prop("checked");
        if ($('#chebox_fillImg_' + icolumnId).hasClass('img-block')) {
            var Visibility = true;
        }
        else {
            var Visibility = false;
        }
        Arr2.push({ iPriority: Index, sUser_ColumnName: ColumnName, IsActive: Visibility, EditColumnName: EditColumnName, icolumnId: icolumnId });
        Arr1 = _.filter(Arr2, function (e) { return e.IsActive == true });
    });
    
    var List1 = [];
    Arr1.forEach(function (e) {
        List1.push({
            "icolumnId": e.icolumnId,
            "iPriority": e.iPriority + 1,
            "sUser_ColumnName": e.sUser_ColumnName,
            "sCustMiseCaption": e.EditColumnName,
        });
    });

    var List2 = [];
    $("#mytable1 #myTableBody1 tr").each(function () {
        var Index = $(this).index();
        var Vendor = $(this).find('.Vendor').html();
        var Location = $(this).find('.Location').html();
        var Shape = $(this).find('.Shape').html();
        var Carat = $(this).find('.Carat').html();
        var Color = $(this).find('.Color').html();
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
            iVendor: Vendor,
            iLocation: Location,
            sShape: Shape,
            sPointer: Carat,
            sColor: Color,
            sClarity: Clarity,
            sCut: Cut,
            sPolish: Polish,
            sSymm: Sym,
            sFls: Fls,
            sLab: Lab,
            dFromLength: FromLength,
            dToLength: ToLength,
            dFromWidth: FromWidth,
            dToWidth: ToWidth,
            dFromDepth: FromDepth,
            dToDepth: ToDepth,
            dFromDepthPer: FromDepthinPer,
            dToDepthPer: ToDepthinPer,
            dFromTablePer: FromTableinPer,
            dToTablePer: ToTableinPer,
            dFromCrAng: FromCrAng,
            dToCrAng: ToCrAng,
            dFromCrHt: FromCrHt,
            dToCrHt: ToCrHt,
            dFromPavAng: FromPavAng,
            dToPavAng: ToPavAng,
            dFromPavHt: FromPavHt,
            dToPavHt: ToPavHt,
            dKeyToSymbol: Keytosymbol,
            dCheckKTS: dCheckKTS,
            dUNCheckKTS: dUNCheckKTS,
            sBGM: BGM,
            sCrownBlack: CrownBlack,
            sTableBlack: TableBlack,
            sCrownWhite: CrownWhite,
            sTableWhite: TableWhite,
            Img: Image,
            Vdo: Video,
            PriceMethod: PriceMethod,
            PricePer: Percentage
        });
    });

    var obj = CreateModel();
    obj.ColumnsSettings = List1;
    obj.APIFilters = List2;

    $.ajax({
        url: "/Api/SaveUploadMethod",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ apiuploadmethod: obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            loaderHide();
            if (data.Status == "1") {
                if ($("#TransId").val() == "0") {
                    toastr.success("Api Save Successfully !!", { timeOut: 2500 });
                    location.href = window.location.href + "?Type=modify&TransId=" + data.Message;
                }
                else {
                    toastr.success("Api Update Successfully !!", { timeOut: 2500 });
                    location.reload(true);
                }
            } else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
            toastr.error(textStatus);
        }
    });

};

var AddNewRow = function () {
    if ($("#PricingMethod").val() == undefined || $("#PricingMethod").val() == "") {
        toastr.warning("Please Select Price Method !", { timeOut: 2500 });
        $("#PricingMethod").focus();
        return;
    }

    if ($("#PricingMethod").val() != undefined || $("#PricingMethod").val() != "") {
        if ($("#Percentage").val() == undefined || $("#Percentage").val() == "") {
            toastr.warning("Please Enter Percentage !", { timeOut: 2500 });
            $("#Percentage").focus();
            return;
        }
    }

    $("#btnAddNewRow").attr("disabled", true);
    $("#mytable1").show();

    var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    var cntRow = parseInt($("#mytable1 #myTableBody1").find('tr').length) + 1;

    var Vendor = $("#Vendor").val() == "" && $("#Vendor").val() == undefined ? "" : $("#Vendor").val();
    var Location = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Carat = CheckedCaratValue;
    var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Sym = _.pluck(_.filter(SymmList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'sName').join(",");
    var FromLength = $("#FromLength").val() == "" || $("#FromLength").val() == undefined || $("#FromLength").val() == "0.00" ? "0" : $("#FromLength").val();
    var ToLength = $("#ToLength").val() == "" || $("#ToLength").val() == undefined || $("#ToLength").val() == "0.00" ? "0" : $("#ToLength").val();
    var FromWidth = $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined || $("#FromWidth").val() == "0.00" ? "0" : $("#FromWidth").val();
    var ToWidth = $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined || $("#ToWidth").val() == "0.00" ? "0" : $("#ToWidth").val();
    var FromDepth = $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined || $("#FromDepth").val() == "0.00" ? "0" : $("#FromDepth").val();
    var ToDepth = $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined || $("#ToDepth").val() == "0.00" ? "0" : $("#ToDepth").val();
    var FromDepthinPer = $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined || $("#FromDepthPer").val() == "0.00" ? "0" : $("#FromDepthPer").val();
    var ToDepthinPer = $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined || $("#ToDepthPer").val() == "0.00" ? "0" : $("#ToDepthPer").val();
    var FromTableinPer = $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined || $("#FromTablePer").val() == "0.00" ? "0" : $("#FromTablePer").val();
    var ToTableinPer = $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined || $("#ToTablePer").val() == "0.00" ? "0" : $("#ToTablePer").val();
    var FromCrAng = $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined || $("#FromCrAng").val() == "0.00" ? "0" : $("#FromCrAng").val();
    var ToCrAng = $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined || $("#ToCrAng").val() == "0.00" ? "0" : $("#ToCrAng").val();
    var FromCrHt = $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined || $("#FromCrHt").val() == "0.00" ? "0" : $("#FromCrHt").val();
    var ToCrHt = $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined || $("#ToCrHt").val() == "0.00" ? "0" : $("#ToCrHt").val();
    var FromPavAng = $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined || $("#FromPavAng").val() == "0.00" ? "0" : $("#FromPavAng").val();
    var ToPavAng = $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined || $("#ToPavAng").val() == "0.00" ? "0" : $("#ToPavAng").val();
    var FromPavHt = $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined || $("#FromPavHt").val() == "0.00" ? "0" : $("#FromPavHt").val();
    var ToPavHt = $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined || $("#ToPavHt").val() == "0.00" ? "0" : $("#ToPavHt").val();
    var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
    var dCheckKTS = KeyToSymLst_Check1;
    var dUNCheckKTS = KeyToSymLst_uncheck1;
    var BGM = _.pluck(_.filter(BgmList, function (e) { return e.isActive == true }), 'sName').join(",");
    var CrownBlack = _.pluck(_.filter(CrnBlackList, function (e) { return e.isActive == true }), 'sName').join(",");
    var TableBlack = _.pluck(_.filter(TblBlackList, function (e) { return e.isActive == true }), 'sName').join(",");
    var CrownWhite = _.pluck(_.filter(CrnWhiteList, function (e) { return e.isActive == true }), 'sName').join(",");
    var TableWhite = _.pluck(_.filter(TblWhiteList, function (e) { return e.isActive == true }), 'sName').join(",");
    var Image = $('#Img:checked').val();
    var Video = $('#Vdo:checked').val();
    var PriceMethod = $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val();
    var Percentage = $("#PricingMethod").val() == "" || $("#Percentage").val() == "" || $("#Percentage").val() == undefined ? "" : $("#Percentage").val();

    var html = "<tr id='tr'>";
    html += "<th class='Row Fi-Criteria' style=''>" + cntRow.toString() + "</th>";
    html += "<td style=''><span class='Fi-Criteria Vendor'>" + Vendor + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Location'>" + Location + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Color'>" + Color + "</span></td>";
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

    $("#mytable1 #myTableBody1").append(html);

    $("#btnAddNewRow").attr("disabled", false);
    Reset_API_Filter();
}

var BindPara = function (iTransId) {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/API/UploadMethodGetList',
        data: "{'fromDate':'', 'ToDate':'', 'sTransId':'" + iTransId + "', 'PageNo':'1', 'PageSize':'50'}",
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            SetModifyParameter(data.Data[0]);
            setTimeout(function () {
                loaderHide();
            }, 2000)
        }
    });
}
var SetModifyParameter = function (model) {
    console.log(model);
    if (model != null) { //
        if (model.sPointer != null) {
            CheckedCaratValue = model.sPointer;

            var Poinertlst = CheckedCaratValue.split(",");
            ``
            _.each(Poinertlst, function (item) {
                if (item == "0.18-0.22") {
                    CARAT_Size2 = true;
                    $("#CARAT_Size2").prop('checked', true);
                }
                if (item == "0.23-0.29") {
                    CARAT_Size3 = true;
                    $("#CARAT_Size3").prop('checked', true);
                }
                if (item == "0.30-0.39") {
                    CARAT_Size4 = true;
                    $("#CARAT_Size4").prop('checked', true);
                }
                if (item == "0.40-0.49") {
                    CARAT_Size5 = true;
                    $("#CARAT_Size5").prop('checked', true);
                }
                if (item == "0.50-0.59") {
                    CARAT_Size6 = true;
                    $("#CARAT_Size6").prop('checked', true);
                }
                if (item == "0.60-0.69") {
                    CARAT_Size7 = true;
                    $("#CARAT_Size7").prop('checked', true);
                }
                if (item == "0.70-0.79") {
                    CARAT_Size8 = true;
                    $("#CARAT_Size8").prop('checked', true);
                }
                if (item == "0.80-0.89") {
                    CARAT_Size9 = true;
                    $("#CARAT_Size9").prop('checked', true);
                }
                if (item == "0.90-0.99") {
                    CARAT_Size10 = true;
                    $("#CARAT_Size10").prop('checked', true);
                }
                if (item == "1.00-1.19") {
                    CARAT_Size11 = true;
                    $("#CARAT_Size11").prop('checked', true);
                }
                if (item == "1.20-1.49") {
                    CARAT_Size12 = true;
                    $("#CARAT_Size12").prop('checked', true);
                }
                if (item == "1.50-1.99") {
                    CARAT_Size13 = true;
                    $("#CARAT_Size13").prop('checked', true);
                }
                if (item == "2.00-2.99") {
                    CARAT_Size15 = true;
                    $("#CARAT_Size15").prop('checked', true);
                }
                if (item == "3.00-99.99") {
                    CARAT_Size17 = true;
                    $("#CARAT_Size17").prop('checked', true);
                }
            });
            GetSelectedCarat();
        }


        if (model.sTransDate != null) {
            $("#TransDate").val(model.sTransDate);
        }

        if (model.iLocation != null) {
            var locationlst = model.iLocation.split(",");
            _.each(locationlst, function (item) {
                _.each(_.filter(LocationList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
        }
        if (model.sShape != null) {
            var shapelst = model.sShape.split(",");
            _.each(shapelst, function (item) {
                _.each(_.filter(ShapeList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
        }
        if (model.sColor != null) {
            var colorlst = model.sColor.split(",");
            _.each(colorlst, function (item) {
                _.each(_.filter(ColorList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
        }
        if (model.sClarity != null) {
            var clariytlst = model.sClarity.split(",");
            _.each(clariytlst, function (item) {
                _.each(_.filter(ClarityList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
        }
        if (model.sCut != null) {
            var cutlst = model.sCut.split(",");
            _.each(cutlst, function (item) {
                _.each(_.filter(CutList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
            $("#FromCut").select2("val", $("#select option:contains('" + cutlst[0] + "')").val());
            $("#ToCut").select2("val", $("#select option:contains('" + cutlst[1] + "')").val());
        }
        if (model.sPolish != null) {
            var polishlst = model.sPolish.split(",");

            _.each(polishlst, function (item) {

                _.each(_.filter(PolishList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
            $("#FromPolish").select2("val", $("#select option:contains('" + polishlst[0] + "')").val());
            $("#ToPolish").select2("val", $("#select option:contains('" + polishlst[1] + "')").val());
        }
        if (model.sSymm != null) {
            var symmlst = model.sSymm.split(",");
            _.each(symmlst, function (item) {

                _.each(_.filter(SymmList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
            $("#FromSymm").select2("val", $("#select option:contains('" + symmlst[0] + "')").val());
            $("#ToSymm").select2("val", $("#select option:contains('" + symmlst[1] + "')").val());
        }
        if (model.sFls != null) {
            var flslst = model.sFls.split(",");
            _.each(flslst, function (item) {

                _.each(_.filter(FlsList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
            $("#FromFls").select2("val", $("#select option:contains('" + flslst[0] + "')").val());
            $("#ToFls").select2("val", $("#select option:contains('" + flslst[1] + "')").val());
        }
        if (model.sShade != null) {
            var shadelst = model.sShade.split(",");
            _.each(shadelst, function (item) {
                _.each(_.filter(ShadeList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
            $("#FromShade").select2("val", $("#select option:contains('" + shadelst[0] + "')").val());
            $("#ToShade").select2("val", $("#select option:contains('" + shadelst[1] + "')").val());
        }
        if (model.sTableNatts != null) {
            var nattslst = model.sTableNatts.split(",");
            _.each(nattslst, function (item) {
                _.each(_.filter(NattsList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
            $("#FromNatts").select2("val", $("#select option:contains('" + nattslst[0] + "')").val());
            $("#ToNatts").select2("val", $("#select option:contains('" + nattslst[1] + "')").val());
        }
        if (model.sInclusion != null) {
            var sInclusionlst = model.sInclusion.split(",");
            _.each(sInclusionlst, function (item) {
                _.each(_.filter(InclusionList, function (e) { return e.sName == item }), function (itm) {
                    itm.isActive = true;
                });
            });
            $("#FromInclusion").select2("val", $("#select option:contains('" + sInclusionlst[0] + "')").val());
            $("#ToInclusion").select2("val", $("#select option:contains('" + sInclusionlst[1] + "')").val());
        }

        //if (model.iLocation != null && model.iLocation != "") {
        //    var sLocationlst = model.iLocation.split(',');
        //    var selectedLoc = [];
        //    _.each(sLocationlst, function (item) {
        //        _.each(_.filter(LocationList, function (e) { return e.iSr == item }), function (itm) {
        //            selectedLoc.push({ id: itm.iSr, text: itm.sName });
        //        });
        //    });

        //    $("#Location").val(sLocationlst).trigger('change');
        //}
        if (model.sLab != null) {
            var sLablst = model.sLab.split(",");
            _.each(sLablst, function (item) {

                _.each(_.filter(LabList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.sBGM != null) {
            var sBGMlst = model.sBGM.split(",");
            _.each(sBGMlst, function (item) {

                _.each(_.filter(BgmList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.sCrownBlack != null) {
            var sCrownBlacklst = model.sCrownBlack.split(",");
            _.each(sCrownBlacklst, function (item) {

                _.each(_.filter(CrnBlackList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.sTableBlack != null) {
            var sTableBlacklst = model.sTableBlack.split(",");
            _.each(sTableBlacklst, function (item) {

                _.each(_.filter(TblBlackList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.sCrownWhite != null) {
            var sCrownWhitelst = model.sCrownWhite.split(",");
            _.each(sCrownWhitelst, function (item) {

                _.each(_.filter(CrnWhiteList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.sTableWhite != null) {
            var sTableWhitelst = model.sTableWhite.split(",");
            _.each(sTableWhitelst, function (item) {

                _.each(_.filter(TblWhiteList, function (e) { return e.sName == item }), function (itm) {

                    itm.isActive = true;
                });
            });
        }
        if (model.dFromcts != null) {
            $("#FromCarat").val(model.dFromcts);
        }
        if (model.dToCts != null) {
            $("#ToCarat").val(model.dToCts);
        }
        if (model.sFromDisc != null) {
            $("#FromDisc").val(model.sFromDisc);
        }
        if (model.dToDisc != null) {
            $("#ToDisc").val(model.dToDisc);
        }
        if (model.dFromLength != null) {
            $("#FromLength").val(model.dFromLength);
        }
        if (model.dToLength != null) {
            $("#ToLength").val(model.dToLength);
        }
        if (model.dFromWidth != null) {
            $("#FromWidth").val(model.dFromWidth);
        }
        if (model.dToWidth != null) {

            $("#ToWidth").val(model.dToWidth);
        }
        if (model.dFromDepth != null) {
            $("#FromDepth").val(model.dFromDepth);
        }
        if (model.dToDepth != null) {
            $("#ToDepth").val(model.dToDepth);
        }
        if (model.dFromDepthPer != null) {
            $("#FromDepthPer").val(model.dFromDepthPer);
        }
        if (model.dToDepthPer != null) {
            $("#ToDepthPer").val(model.dToDepthPer);
        }
        if (model.dFromTablePer != null) {
            $("#FromTablePer").val(model.dFromTablePer);
        }
        if (model.dToTablePer != null) {
            $("#ToTablePer").val(model.dToTablePer);
        }
        if (model.dFromCrAng != null) {
            $("#FromCrAng").val(model.dFromCrAng);
        }
        if (model.dToCrAng != null) {
            $("#ToCrAng").val(model.dToCrAng);
        }
        if (model.dFromCrHt != null) {
            $("#FromCrHt").val(model.dFromCrHt);
        }
        if (model.dToCrHt != null) {
            $("#ToCrHt").val(model.dToCrHt);
        }
        if (model.dFromPavAng != null) {
            $("#FromPavAng").val(model.dFromPavAng);
        }
        if (model.dToPavAng != null) {
            $("#ToPavAng").val(model.dToPavAng);
        }
        if (model.dFromPavHt != null) {
            $("#FromPavHt").val(model.dFromPavHt);
        }
        if (model.dToPavHt != null) {
            $("#ToPavHt").val(model.dToPavHt);
        }

        var checkkts = model.dCheckKTS;
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
        var uncheckkts = model.dUNCheckKTS;
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

        $("#FTPExportType").val('');
        if (model.FTPExportType != null) {
            //$scope.APIView.ExportType = model.sExpType;
            $("#FTPExportType").val(model.FTPExportType);
        }
        $("#URLExportType").val('');
        if (model.URLExportType != null) {
            //$scope.APIView.ExportType = model.sExpType;
            $("#URLExportType").val(model.URLExportType);
        }

        //if (model.sApiName != null) {
        //    $("#APIName").val(model.sApiName);
        //    //$scope.APIName = model.sApiName;
        //}
        //if (model.sExpType != null) {
        //    //$scope.APIView.ExportType = model.sExpType;
        //    $("#ExportType").select2("val", $("#ExportType option:contains('" + model.sExpType + "')").val());
        //}
        //if (model.FTP_NAME != null) {
        //    $("#FTPName").val(model.FTP_NAME);
        //    //$scope.FTPName = model.sFtpName;
        //}
        //if (model.Ftp_User != null) {
        //    $("#FTPUser").val(model.Ftp_User);
        //    //$scope.FTPUser = model.sftpUser;
        //}
        //if (model.FTP_PASSWORD != null) {
        //    $("#FTPPassword").val(model.FTP_PASSWORD);
        //    //$scope.FTPPassword = model.sftpPass;
        //}
        //if (model.FTP_UPLOAD_TIME != null) {
        //    $("#UploadTime").val(model.FTP_UPLOAD_TIME);
        //}
        //if (model.sSeprator != null) {
        //    //$scope.APIView.Separator = model.sSeprator;
        //    $("#Separator").select2("val", $("#Separator option:contains('" + model.sSeprator + "')").val());
        //}

        //if (model.APIURL1 != null) {
        //    $("#APIURL1").text(model.APIURL1);
        //}
        //if (model.ApiUrl != null) {
        //    $("#ApiUrl").prop('checked', model.ApiUrl == "True" ? true : false);
        //}
        //if (model.sMailUploadTime != null) {
        //    $("#sMailUploadTime").val(model.sMailUploadTime);
        //}
        //if (model.sRepeat != null) {
        //    // $scope.APIView.Repeat = model.sRepeat;
        //    $("#Repeat").select2("val", $("#Repeat option:contains('" + model.sRepeat + "')").val());
        //}
        //if (model.sEmail != null) {
        //    $("#Email").val(model.sEmail);
        //}
        ColumnsettingList(model.ColumnsSettings);
        SetSearchParameter();
    }

}
var ColumnsettingList = function (Detaillist) {
    loaderShow(); 
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: '/Api/GetApiColumns',
        data: {},
        dataType: 'json',
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            ColumnList = [];
            NewColumnList = [];
            data.Data.forEach(function (item, index) {
                item.icolumnId = item.iid,
                    item.iPriority = index + 1;
                item.IsActive = false;
                item.sCustMiseCaption = item.caption;
                item.sUser_ColumnName = item.caption;
            });
            ColumnList = data.Data; 
            if ($("#IsModify").val() == 'True') {
                if (Detaillist.length > 0) {
                    ColumnList.forEach(function (e) {
                        Detaillist.forEach(function (item) {
                            if (e.iid == item.icolumnId) {
                                e.IsActive = true;
                                e.CustomeCaption = item.sCustMiseCaption;
                                e.iid = parseFloat(item.icolumnId);
                            }
                            e.CustomeCaption = e.sCustMiseCaption;
                            e.iid = e.iid;
                        });
                    }); 
                    // $scope.TR_lst =$filter('orderBy')($scope.ColumnList, function (e) { return e.IsActive == true });
                    var TR_lst = _.filter(ColumnList, function (e) { return e.IsActive == false });
                    var NTR_lst = _.sortBy(Detaillist, function (e) { return e.iPriority });
                    
                    //$scope.ColumnList.forEach(function (e) {
                    NTR_lst.forEach(function (item) {
                        item.CustomeCaption = item.sCustMiseCaption;
                        item.caption = item.sCaption;
                        item.IsActive = true;
                        item.iid = parseFloat(item.icolumnId);
                    });

                    var Arr = (NTR_lst.concat(TR_lst));
                    Arr.forEach(function (e, i) {
                        e.iPriority = i + 1;
                    }); 
                    var List = [];
                    Arr.forEach(function (e) {
                        List.push({
                            "icolumnId": e.iid,
                            "iPriority": e.iPriority,
                            "sUser_ColumnName": e.caption,
                            "sCustMiseCaption": e.CustomeCaption,
                            "IsActive": e.IsActive
                        });
                    }); 
                    ColumnList = List; //$scope.Arr;
                    CreateTable(ColumnList)
                }
            }
            loaderHide();
        }
    });
}

var CreateTable = function (Columns) {
    var trs = ""; 
    $("#myTableBody").empty();
    Columns.forEach(function (item) {
        trs += "";
        trs += '<tr>'
        trs += '<td id="lblCoolName" style="display: none;"></td>';
        trs += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i></td>';
        trs += '<td id="lblFieldName" class="onbinding">' + item.sUser_ColumnName + '</td>';
        trs += '<td class="CustName">';
        trs += '<input onblur="" type="text" class="form-control input-sm onpristine onvalid onnot-empty onvalid-maxlength ontouched" value="' + item.sCustMiseCaption + '" maxlength="100">';
        trs += '</td>';
        trs += '<td id="lblColId" style="display: none; " class="onbinding">' + item.icolumnId + '</td>';
        trs += '<td id="lblOrder" class="ColumnOrder onbinding">' + item.iPriority + '</td>';
        trs += '<td><center>';
        //trs += '<label class="cust-chk-bx text-left">';
        //if (item.IsActive) {
        //    trs += '<input type="checkbox" id="checkbox2" name="checkbox-1" checked class="onpristine onuntouched onvalid onempty">';
        //}
        //else {
        //    trs += '<input type="checkbox" id="checkbox2" name="checkbox-1" class="onpristine onuntouched onvalid onempty">';
        //}
        //trs += '<span class="cust-chkmark"></span>';
        //trs += '</label>';
        if (item.IsActive) {
            trs += '<img src="/Content/images/chebox-fill.png" class="chebox-fill img-block" id="chebox_fillImg_' + item.icolumnId + '" onclick="chebox_fill(' + item.icolumnId + ')" style="cursor:pointer;width: 20px;" />';
            trs += '<img src="/Content/images/chebox-empty.png" class="chebox-empty img-none" id="chebox_emptyImg_' + item.icolumnId + '" onclick="chebox_empty(' + item.icolumnId + ')" style="cursor:pointer;width: 20px;margin-bottom: 7px;" />';
        }
        else {
            trs += '<img src="/Content/images/chebox-fill.png" class="chebox-fill img-none" id="chebox_fillImg_' + item.icolumnId + '" onclick="chebox_fill(' + item.icolumnId + ')" style="cursor:pointer;width: 20px" />';
            trs += '<img src="/Content/images/chebox-empty.png" class="chebox-empty img-block" id="chebox_emptyImg_' + item.icolumnId + '" onclick="chebox_empty(' + item.icolumnId + ')" style="cursor:pointer;width: 20px;margin-top: 0px;" />';
        }
        trs += '</center></td>';
        trs += '</tr>';
        
    });
    $("#myTableBody").html(trs);
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
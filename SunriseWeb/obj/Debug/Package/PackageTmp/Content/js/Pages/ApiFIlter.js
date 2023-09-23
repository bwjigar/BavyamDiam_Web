function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function loaderShow() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

function loaderHide() {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
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

var validmail = function (event) {
    var emailID = $(event.target).val().trim();
    if (!checkemail(emailID)) {
        toastr.error("Invalid Email Formate.", { timeOut: 2500 });
        $("#Email").val("");
        return;
    }
}

var checkemail = function (valemail) {
    var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$)/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
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
        $(".ddlCheckedCaratValue").addClass("ng-hide");
        $(".divCheckedCaratValue").removeClass("ng-hide");
        $(".divCheckedCaratValue").empty();
        $(".divCheckedCaratValue").append("<p title='" + CheckedCaratValue + "'>" + CheckedCaratValue + "</p>");
    }
    else {
        $(".ddlCheckedCaratValue").removeClass("ng-hide");
        $(".divCheckedCaratValue").addClass("ng-hide");
        $(".divCheckedCaratValue").empty();
    }
    SetSearchParameter();
}

var ResetCheckCarat = function () {
    SSN_CARAT = [];
    CheckedCaratValue = '';
    CaratFrom = '';
    CaratTo = '';
    Color = '';
    IsCaratFT = true
    CARAT = false;
    FromSize1 = "";
    ToSize1 = "";
    FromSize2 = "";
    ToSize2 = "";
    FromSize3 = "";
    ToSize3 = "";
    FromSize4 = "";
    ToSize4 = "";
    FromSize5 = "";
    ToSize5 = "";
    FromSize6 = "";
    ToSize6 = "";
    FromSize7 = "";
    ToSize7 = "";
    FromSize8 = "";
    ToSize8 = "";
    FromSize11 = "";
    ToSize11 = "";
    FromSize10 = "";
    ToSize10 = "";
    FromSize11 = "";
    ToSize11 = "";
    FromSize12 = "";
    ToSize12 = "";
    FromSize13 = "";
    ToSize13 = "";
    FromSize14 = "";
    ToSize14 = "";
    FromSize15 = "";
    ToSize15 = "";
    FromSize16 = "";
    ToSize16 = "";
    FromSize17 = "";
    ToSize17 = "";
    FromSize18 = "";
    ToSize18 = "";

    CARAT_Size2 = false;
    CARAT_Size3 = false;
    CARAT_Size4 = false;
    CARAT_Size5 = false;
    CARAT_Size6 = false;
    CARAT_Size7 = false;
    CARAT_Size8 = false;
    CARAT_Size9 = false;
    CARAT_Size10 = false;
    CARAT_Size11 = false;
    CARAT_Size12 = false;
    CARAT_Size13 = false;
    CARAT_Size14 = false;
    CARAT_Size15 = false;
    CARAT_Size16 = false;
    CARAT_Size17 = false;
    CARAT_Size18 = false;
    Carat = "";
    
    $('#CaratModal input:checkbox').prop('checked', false);
    $("#FromCarat").val("0.00");
    $("#ToCarat").val("0.00");
    
    $(".ddlCheckedCaratValue").removeClass("ng-hide");
    $(".divCheckedCaratValue").addClass("ng-hide");
    $(".divCheckedCaratValue").empty();
}

var ModalShow = function (ParameterLabel) {
    $('#exampleModalLabel').text(ParameterLabel);
    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var list = [];
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
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
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }

    var itm = _.find(list, function (i) { return i.sName == item });
    itm.isActive = !itm.isActive;
    $(curritem).toggleClass('active');
    SetSearchParameter();
}

var CreateModel = function () {
    var obj = {
        "iTransId": $("#IsModify").val() == "True" ? $("#TransId").val() : "",
        "sTransDate": $("#IsModify").val() == "True" ? $("#dTransDate").val() : "",
        "sShape": _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sColor": _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sClarity": _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sCut": _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'sName').join(","),// $scope.CutObj,              
        "sPolish": _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sSymm": _.pluck(_.filter(SymmList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sFls": _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sLab": _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'sName').join(","),
        "dFromcts": $("#FromCarat").val() == "" || $("#FromCarat").val() == undefined || ($("#FromCarat").val() == "0.00" && $("#FromCarat").val() == "0.00") ? "" : $("#FromCarat").val(),
        "dToCts": $("#ToCarat").val() == "" || $("#ToCarat").val() == undefined || $("#ToCarat").val() == "0.00" ? "" : $("#ToCarat").val(),
        "sFromCrAng": $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined || ($("#FromCrAng").val() == "0.00" && $("#FromCrAng").val() == "0.00") ? "0.00" : $("#FromCrAng").val(),
        "dToDisc": $("#ToDisc").val() == "" || $("#ToDisc").val() == undefined || $("#ToDisc").val() == "0.00" ? "" : $("#ToDisc").val(),
        "sPointer": CheckedCaratValue,
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
        "sShade": _.pluck(_.filter(ShadeList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sInclusion": _.pluck(_.filter(InclusionList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sTableNatts": _.pluck(_.filter(NattsList, function (e) { return e.isActive == true }), 'sName').join(","),
        "sApiName": $("#APIName").val(),
        "sExpType": $("#ExportType").select2('val') == undefined ? '' : $("#ExportType").select2('val'),
        "sCrownNatts": "",//$scope.APIView.NetAmountFrom == undefined ? "" : $scope.StockSearch.NetAmountFrom,
        "sCrownInclusion": "",//$scope.StockSearch.NetAmountTo == undefined ? "" : $scope.StockSearch.NetAmountTo,
        "sFtpName": $("#FTPName").val(),
        "sftpUser": $("#FTPUser").val(),
        "sftpPass": $("#FTPPassword").val(),
        "iFtpUploadTime": $("#UploadTime").val() == undefined ? "" : $("#UploadTime").val(),
        "sSeprator": $("#Separator").select2('val') == undefined ? "" : $("#Separator").select2('val'),
        "sRepeat": $("#Repeat").select2('val') == undefined ? "" : $("#Repeat").select2('val'),
        "sEmail": $("#Email").val() == undefined ? "" : $("#Email").val(),
        "sMailUploadTime": $("#sMailUploadTime").val() == undefined ? "" : $("#sMailUploadTime").val(),
        "iLocation": $("#Location").select2('val') == undefined ? "" : $("#Location").select2('val'),
        //"iLocation": $("#Location").select2('val').join(','),
        "ApiUrl": $("#ApiUrl").is(":checked"),
        "APIURL1": $("#IsModify").val() == "True" ? $("#APIURL1").val() : "",
        "iTransId": $("#IsModify").val() == "True" ? $("#TransId").val() : ""
    };
    return obj;
}

var SaveApiData = function () {
    // alert('111')
    if ($("#APIName").val() == undefined || $("#APIName").val() == "") {
        toastr.warning("Please Enter API Name ... ", { timeOut: 2500 });
        return;
    }
    if ($("#FTPName").val() == undefined || $("#FTPName").val() == "") {
        toastr.warning("Please Enter FTP Name ... ", { timeOut: 2500 });
        return;
    }
    if ($("#FTPUser").val() == undefined || $("#FTPUser").val() == "") {
        toastr.warning("Please Enter FTP User ... ", { timeOut: 2500 });
        return;
    }
    if ($("#FTPPassword").val() == undefined || $("#FTPPassword").val() == "") {
        toastr.warning("Please Enter FTP PAssword ... ", { timeOut: 2500 });
        return;
    }

    if ($("#UploadTime").val() == undefined || $("#UploadTime").val() == "") {
        toastr.warning("Please Enter Upload Time... ", { timeOut: 2500 });
        return;
    }
    if ($("#UploadTime").val() < 60) {
        toastr.warning("Please Enter FTP Upload Time Greater  or Equal to 60 Min ... ", { timeOut: 2500 });
        return;
    }

    //if ($("#Email").val() == undefined || $("#Email").val() == "") {
    //    toastr.warning("Please Enter Email Address... ", { timeOut: 2500 });
    //    return;
    //}
    if ($("#Repeat").val() == undefined || $("#Repeat").val() == "") {
        toastr.warning("Please Enter Occurence... ", { timeOut: 2500 });
        return;
    }
    if ($("#sMailUploadTime").val() == undefined || $("#sMailUploadTime").val() == "") {
        toastr.warning("Please Enter Mail Ocurance Time... ", { timeOut: 2500 });
        return;
    }
    if ($("#sMailUploadTime").val() > 24) {
        toastr.warning("Please Enter Mail Upload Time Less than or Equal to 24 Hrs.... ", { timeOut: 2500 });
        return;
    }

    loaderShow();

    var Arr1 = [];
    var Arr2 = [];
    $("#mytable tbody tr").each(function () {
        var Index = $(this).index();
        var ColumnName = $(this).find("td:eq(2)").html().trim();
        var Visibility = $(this).find("#checkbox2").prop("checked");
        Arr2.push({ iPriority: Index, sUser_ColumnName: ColumnName, IsActive: Visibility });

        Arr1 = _.filter(Arr2, function (e) { return e.IsActive == true });
    });

    var lst = [];
    ColumnList.forEach(function (item) {
        Arr1.forEach(function (e) {
            if (e.sUser_ColumnName == item.sUser_ColumnName) {
                _.find(ColumnList, function (i) { return i.sUser_ColumnName == item.sUser_ColumnName }).IsActive = true;
            }
        });
    });
    var lst = _.filter(ColumnList, function (e) { return e.IsActive == true });

    var List = [];
    Arr1.forEach(function (item) {
        lst.forEach(function (e) {
            if (e.sUser_ColumnName == item.sUser_ColumnName)
                e.iPriority = item.iPriority;
        });
    });
    lst.forEach(function (e) {
        List.push({
            "icolumnId": e.icolumnId,
            "iPriority": e.iPriority + 1,
            "sUser_ColumnName": e.sUser_ColumnName,
            "sCustMiseCaption": e.sCustMiseCaption,
        });
    });
    //var ColumnsettingList = List;
    var obj = CreateModel();
    obj.ColumnsSettings = List;
    $.ajax({
        url: "/api/Save",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ apiDetails: obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            loaderHide();
            if (data.Status == "1") {
                toastr.success("API Save Successfully ... ", { timeOut: 2500 });
            } else {
                // BindData();
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

var API_View = function () {
    window.location.href = '/Api/Views';
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
        trs = "";
        trs += '<tr>'
        trs += '<td id="lblCoolName" style="display: none;"></td>';
        trs += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i></td>';
        trs += '<td id="lblFieldName" class="onbinding">' + item.sUser_ColumnName + '</td>';
        trs += '<td class="CustName">';
        trs += '<input onblur="" type="text" class="form-control input-sm onpristine onvalid onnot-empty onvalid-maxlength ontouched" value="' + item.sCustMiseCaption + '" maxlength="100">';
        trs += '</td>';
        trs += '<td id="lblColId" style="display: none; " class="onbinding">' + item.icolumnId + '</td>';
        trs += '<td id="lblOrder" class="ColumnOrder onbinding">' + item.iPriority + '</td>';
        trs += '<td>';
        trs += '<label class="cust-chk-bx text-left">';
        if (item.IsActive) {
            trs += '<input type="checkbox" id="checkbox2" name="checkbox-1" checked class="onpristine onuntouched onvalid onempty">';
        }
        else {
            trs += '<input type="checkbox" id="checkbox2" name="checkbox-1" class="onpristine onuntouched onvalid onempty">';
        }
        trs += '<span class="cust-chkmark"></span>';
        trs += '</label>';
        trs += '</td>';
        trs += '</tr>';
        $("#myTableBody").append(trs);
    });
}

var BindPara = function (iTransId) {

    loaderShow();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/API/GetAPIData',
        data: "{ 'TransID':" + iTransId + "}",
        success: function (data) {
            SetModifyParameter(data.Data[0]);
            setTimeout(function () {
                loaderHide();
            }, 2000)
        }
    });
}
var Reset = function () {
    window.location = '/Api/Filter';
}

var Clear = function () {
    Reset();
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
        
        if (model.iLocation != null && model.iLocation != "") {
            //var sLocationlst = model.iLocation.split(',');
            
            //$("#Location").val(sLocationlst).trigger('change');
            $("#Location").val(model.iLocation).trigger('change');
        }
        if (model.sLab != null) {
            var sLablst = model.sLab.split(",");
            _.each(sLablst, function (item) {

                _.each(_.filter(LabList, function (e) { return e.sName == item }), function (itm) {

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

        if (model.sApiName != null) {
            $("#APIName").val(model.sApiName);
            //$scope.APIName = model.sApiName;
        }
        if (model.sExpType != null) {
            //$scope.APIView.ExportType = model.sExpType;
            $("#ExportType").select2("val", $("#ExportType option:contains('" + model.sExpType + "')").val());
        }
        if (model.FTP_NAME != null) {
            $("#FTPName").val(model.FTP_NAME);
            //$scope.FTPName = model.sFtpName;
        }
        if (model.Ftp_User != null) {
            $("#FTPUser").val(model.Ftp_User);
            //$scope.FTPUser = model.sftpUser;
        }
        if (model.FTP_PASSWORD != null) {
            $("#FTPPassword").val(model.FTP_PASSWORD);
            //$scope.FTPPassword = model.sftpPass;
        }
        if (model.FTP_UPLOAD_TIME != null) {
            $("#UploadTime").val(model.FTP_UPLOAD_TIME);
        }
        if (model.sSeprator != null) {
            //$scope.APIView.Separator = model.sSeprator;
            $("#Separator").select2("val", $("#Separator option:contains('" + model.sSeprator + "')").val());
        }

        if (model.APIURL1 != null) {
            $("#APIURL1").text(model.APIURL1);
        }
        if (model.ApiUrl != null) {
            $("#ApiUrl").prop('checked', model.ApiUrl == "True" ? true : false);
        }
        if (model.sMailUploadTime != null) {
            $("#sMailUploadTime").val(model.sMailUploadTime);
        }
        if (model.sRepeat != null) {
            // $scope.APIView.Repeat = model.sRepeat;
            $("#Repeat").select2("val", $("#Repeat option:contains('" + model.sRepeat + "')").val());
        }
        if (model.sEmail != null) {
            $("#Email").val(model.sEmail);
        }
        ColumnsettingList(model.ColumnsSettings);
        SetSearchParameter();
    }

}
var shapeDrop = '';
var labDrop = '';
var colorDrop = '';
var clarityDrop = '';
var cutDrop = '';
var polishDrop = '';
var symmDrop = '';
var flsDrop = '';
var discTypeDrop = '';
var signDrop = '';
var cntRow = 0;

$(document).ready(function (e) {
    GeDropdownData();

    GetVendorData();

    $('#frm').validate({
        rules: {
            Vendor: {
                required: true
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "Vendor") {
                error.insertAfter('#divVendor');
            }
        }
    });

    $('#btnSave').click(function () {
        SaveParaDisc();
    });

    $('#btnAddNew').click(function () {
        AddNewDisc();
    });

    $('#btnReset').click(function () {
        GetParaDiscData();
    });
});

function GeDropdownData() {
    loaderShow();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Status != undefined) {
                var ParameterList = data.Data;
                var ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
                var LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
                var CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
                var ColorList = _.filter(ParameterList, function (e) { return e.ListType == 'COLOR' });
                var ClarityList = _.filter(ParameterList, function (e) { return e.ListType == 'CLARITY' });
                var CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
                var PolishList = _.filter(ParameterList, function (e) { return e.ListType == 'POLISH' });
                var SymList = _.filter(ParameterList, function (e) { return e.ListType == 'SYMM' });
                var FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });

                shapeDrop = '';
                _(ShapeList).each(function (obj, i) {
                    shapeDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                labDrop = '';
                labDrop += '<option value="">Select</option>';
                _(LabList).each(function (obj, i) {
                    labDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                //html = '';
                //html = '<select id="ddlCts' + type + rowno + '" class="form-control">';
                //html += '<option value="">Select</option>';
                //_(CaratList).each(function (obj, i) {
                //    html += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                //});
                //html += '</select>';

                colorDrop = '';
                colorDrop += '<option value="">Select</option>';
                _(ColorList).each(function (obj, i) {
                    colorDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                clarityDrop = '';
                clarityDrop += '<option value="">Select</option>';
                _(ClarityList).each(function (obj, i) {
                    clarityDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                cutDrop = '';
                cutDrop += '<option value="">Select</option>';
                _(CutList).each(function (obj, i) {
                    cutDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                polishDrop = '';
                polishDrop += '<option value="">Select</option>';
                _(PolishList).each(function (obj, i) {
                    polishDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                symmDrop = '';
                symmDrop += '<option value="">Select</option>';
                _(SymList).each(function (obj, i) {
                    symmDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                flsDrop = '';
                flsDrop += '<option value="">Select</option>';
                _(FlouList).each(function (obj, i) {
                    flsDrop += '<option value="' + obj.Value + '">' + obj.Value + '</option>';
                });

                discTypeDrop = '';
                discTypeDrop += '<option value="Disc">Discount</option>';
                //discTypeDrop += '<option value="Val">Value</option>';

                signDrop = '';
                signDrop += '<option value="+">+</option>';
                signDrop += '<option value="-">-</option>';

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

function SaveParaDisc() {
    var isValid = $("#frm").valid();
    if (!isValid)
        return;

    loaderShow();

    var list = $("#myTableBody").find('tr');
    var i = 0, tot = list.length, rowNo = 0;
    var xmlStr = "";
    if (tot > 0) {
        xmlStr = '<REQUEST>';

        var obj = {};
        var arrObj = [];
        var j = 0; arrLength = 0, flag = false;
        for (; i < tot; i++) {

            rowNo = $(list[i]).attr('id').substring(2);
            obj = {};
            obj.rowNo = rowNo;
            obj.shape = ($("#ddlShape" + rowNo).val() != null ? $("#ddlShape" + rowNo).val().join(',') : "");
            obj.lab = $("#ddlLab" + rowNo).val();
            obj.ctsFrom = $("#txtCtsFrom" + rowNo).val();
            obj.ctsTo = $("#txtCtsTo" + rowNo).val();
            obj.colorFrom = $("#ddlColorFrom" + rowNo).val();
            obj.colorTo = $("#ddlColorTo" + rowNo).val();
            obj.clarityFrom = $("#ddlClarityFrom" + rowNo).val();
            obj.clarityTo = $("#ddlClarityTo" + rowNo).val();
            obj.cutFrom = $("#ddlCutFrom" + rowNo).val();
            obj.cutTo = $("#ddlCutTo" + rowNo).val();
            obj.polishFrom = $("#ddlPolishFrom" + rowNo).val();
            obj.polishTo = $("#ddlPolishTo" + rowNo).val();
            obj.symmFrom = $("#ddlSymmFrom" + rowNo).val();
            obj.symmTo = $("#ddlSymmTo" + rowNo).val();
            obj.flsFrom = $("#ddlFlsFrom" + rowNo).val();
            obj.flsTo = $("#ddlFlsTo" + rowNo).val();
            obj.disc = parseFloat($("#txtDisc" + rowNo).val());
            obj.disc = (isNaN(obj.disc) ? 0 : obj.disc.toFixed(2));
            obj.discType = $("#ddlDiscType" + rowNo).val();
            obj.sign = $("#ddlSign" + rowNo).val();

            j = 0; arrLength = arrObj.length; flag = false;
            for (; j < arrLength; j++) {
                if (obj.shape == arrObj[j].shape && obj.lab == arrObj[j].lab && obj.ctsFrom == arrObj[j].ctsFrom && obj.ctsTo == arrObj[j].ctsTo
                    && obj.colorFrom == arrObj[j].colorFrom && obj.colorTo == arrObj[j].colorTo && obj.clarityFrom == arrObj[j].clarityFrom
                    && obj.clarityTo == arrObj[j].clarityTo && obj.cutFrom == arrObj[j].cutFrom && obj.cutTo == arrObj[j].cutTo
                    && obj.polishFrom == arrObj[j].polishFrom && obj.polishTo == arrObj[j].polishTo && obj.symmFrom == arrObj[j].symmFrom
                    && obj.symmTo == arrObj[j].symmTo && obj.flsFrom == arrObj[j].flsFrom && obj.flsTo == arrObj[j].flsTo
                    && obj.disc == arrObj[j].disc && obj.discType == arrObj[j].discType && obj.sign == arrObj[j].sign) {
                    flag = true;
                    break;
                }
            }

            if (flag == false) {
                if (obj.shape == "" && obj.lab == "" && obj.ctsFrom == "" && obj.ctsTo == ""
                    && obj.colorFrom == "" && obj.colorTo == "" && obj.clarityFrom == ""
                    && obj.clarityTo == "" && obj.cutFrom == "" && obj.cutTo == ""
                    && obj.polishFrom == "" && obj.polishTo == "" && obj.symmFrom == "" && obj.symmTo == ""
                    && obj.flsFrom == "" && obj.flsTo == "" && obj.discType == "") {
                    toastr.error("Select atleast one column value from each row...!");
                    loaderHide();
                    return;
                }
                else if (obj.disc == "" || obj.disc.toString() == "0" || obj.disc.toString() == "0.00") {
                    toastr.error("Discount is required...!");
                    loaderHide();
                    return;
                }
                else {
                    xmlStr += '<ROW><SHAPE>' + obj.shape + '</SHAPE>';
                    xmlStr += '<LAB>' + obj.lab + '</LAB>';
                    xmlStr += '<FROMCTS>' + obj.ctsFrom + '</FROMCTS>';
                    xmlStr += '<TOCTS>' + obj.ctsTo + '</TOCTS>';
                    xmlStr += '<FROMCOLOR>' + obj.colorFrom + '</FROMCOLOR>';
                    xmlStr += '<TOCOLOR>' + obj.colorTo + '</TOCOLOR>';
                    xmlStr += '<FROMCLARITY>' + obj.clarityFrom + '</FROMCLARITY>';
                    xmlStr += '<TOCLARITY>' + obj.clarityTo + '</TOCLARITY>';
                    xmlStr += '<FROMCUT>' + obj.cutFrom + '</FROMCUT>';
                    xmlStr += '<TOCUT>' + obj.cutTo + '</TOCUT>';
                    xmlStr += '<FROMPOLISH>' + obj.polishFrom + '</FROMPOLISH>';
                    xmlStr += '<TOPOLISH>' + obj.polishTo + '</TOPOLISH>';
                    xmlStr += '<FROMSYMM>' + obj.symmFrom + '</FROMSYMM>';
                    xmlStr += '<TOSYMM>' + obj.symmTo + '</TOSYMM>';
                    xmlStr += '<FROMFLS>' + obj.flsFrom + '</FROMFLS>';
                    xmlStr += '<TOFLS>' + obj.flsTo + '</TOFLS>';
                    xmlStr += '<DISC>' + obj.disc + '</DISC>';
                    xmlStr += '<DISCTYPE>' + obj.discType + '</DISCTYPE>';
                    xmlStr += '<SIGN>' + obj.sign + '</SIGN></ROW>';

                    arrObj.push(obj);
                }
            }
            else {
                toastr.error("Row " + rowNo + " is duplicate with Row " + arrObj[j].rowNo + "...!");
                loaderHide();
                return;
            }
        }
        xmlStr += '</REQUEST>';
    }

    if (xmlStr == "") {
        toastr.error("Add atleast one new row...!");
        return;
    }

    $.ajax({
        url: "/Customer/SaveParaDisc",
        async: false,
        type: "POST",
        data: { CustId: $("#ddlVendor").val(), Oper: 'Save', Input: escape(xmlStr) },
        success: function (data) {
            if (data.Message != undefined) {
                if (data.Status == '1') {
                    toastr.success("Saved successfully...!");
                }
                else {
                    toastr.error(data.Message);
                }
            }
            else {
                window.location = GetLoginUrl();
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

function RemoveDisc(rowNo) {
    var custDiscId = $("#hdnCustDiscId" + rowNo).val();

    custDiscId = parseInt(custDiscId);
    custDiscId = isNaN(custDiscId) ? 0 : custDiscId;

    if (custDiscId > 0) {
        var obj = {};
        obj.rowNo = rowNo;
        obj.shape = ($("#ddlShape" + rowNo).val() != null ? $("#ddlShape" + rowNo).val().join(',') : "");
        obj.lab = $("#ddlLab" + rowNo).val();
        obj.ctsFrom = $("#txtCtsFrom" + rowNo).val();
        obj.ctsTo = $("#txtCtsTo" + rowNo).val();
        obj.colorFrom = $("#ddlColorFrom" + rowNo).val();
        obj.colorTo = $("#ddlColorTo" + rowNo).val();
        obj.clarityFrom = $("#ddlClarityFrom" + rowNo).val();
        obj.clarityTo = $("#ddlClarityTo" + rowNo).val();
        obj.cutFrom = $("#ddlCutFrom" + rowNo).val();
        obj.cutTo = $("#ddlCutTo" + rowNo).val();
        obj.polishFrom = $("#ddlPolishFrom" + rowNo).val();
        obj.polishTo = $("#ddlPolishTo" + rowNo).val();
        obj.symmFrom = $("#ddlSymmFrom" + rowNo).val();
        obj.symmTo = $("#ddlSymmTo" + rowNo).val();
        obj.flsFrom = $("#ddlFlsFrom" + rowNo).val();
        obj.flsTo = $("#ddlFlsTo" + rowNo).val();
        obj.disc = $("#txtDisc" + rowNo).val();
        obj.discType = $("#ddlDiscType" + rowNo).val();
        obj.sign = $("#ddlSign" + rowNo).val();

        var xmlStr = '<REQUEST>';
        xmlStr += '<ROW><SHAPE>' + obj.shape + '</SHAPE>';
        xmlStr += '<LAB>' + obj.lab + '</LAB>';
        xmlStr += '<FROMCTS>' + obj.ctsFrom + '</FROMCTS>';
        xmlStr += '<TOCTS>' + obj.ctsTo + '</TOCTS>';
        xmlStr += '<FROMCOLOR>' + obj.colorFrom + '</FROMCOLOR>';
        xmlStr += '<TOCOLOR>' + obj.colorTo + '</TOCOLOR>';
        xmlStr += '<FROMCLARITY>' + obj.clarityFrom + '</FROMCLARITY>';
        xmlStr += '<TOCLARITY>' + obj.clarityTo + '</TOCLARITY>';
        xmlStr += '<FROMCUT>' + obj.cutFrom + '</FROMCUT>';
        xmlStr += '<TOCUT>' + obj.cutTo + '</TOCUT>';
        xmlStr += '<FROMPOLISH>' + obj.polishFrom + '</FROMPOLISH>';
        xmlStr += '<TOPOLISH>' + obj.polishTo + '</TOPOLISH>';
        xmlStr += '<FROMSYMM>' + obj.symmFrom + '</FROMSYMM>';
        xmlStr += '<TOSYMM>' + obj.symmTo + '</TOSYMM>';
        xmlStr += '<FROMFLS>' + obj.flsFrom + '</FROMFLS>';
        xmlStr += '<TOFLS>' + obj.flsTo + '</TOFLS>';
        xmlStr += '<DISC>' + obj.disc + '</DISC>';
        xmlStr += '<DISCTYPE>' + obj.discType + '</DISCTYPE>';
        xmlStr += '<SIGN>' + obj.sign + '</SIGN></ROW>';
        xmlStr += '</REQUEST>';

        $.ajax({
            url: "/Customer/SaveParaDisc",
            async: false,
            type: "POST",
            data: { CustId: $("#ddlVendor").val(), Oper: 'Delete', Input: escape(xmlStr) },
            success: function (data) {
                if (data.Message != undefined) {
                    if (data.Status == '1') {
                        toastr.success("Deleted successfully...!");
                        GetParaDiscData();
                    }
                    else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    }
                }
                else {
                    window.location = GetLoginUrl();
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
    else {
        $("#tr" + rowNo).remove();
        if ($("#myTableBody").find('tr').length == 0) {
            $("#mytable").hide();
        }
    }
}

function GetVendorData() {
    loaderShow();
    $("#ddlVendor").html("<option value=''>Select customer</option>");

    var obj = {
        PartyName: '',
        ContactPerson: '',
        PartyPrefix: '',
        CountryId: 0,
        PageNo: 0,
        PageSize: 0,
        OrderBy: ''
    };

    $.ajax({
        url: "/Customer/PartyInfo",
        async: false,
        type: "POST",
        data: obj,
        success: function (data) {
            if (data.Status != undefined) {
                var list = data.Data;
                _(list).each(function (obj, i) {
                    $("#ddlVendor").append('<option value="' + obj.Id + '">' + obj.sPartyName + '</option>');
                });

                loaderHide();
            }
            else {
                window.location = GetLoginUrl();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

function GetParaDiscData() {
    $("#mytable").hide();
    $("#myTableBody").html("");
    $("#btnAddNew").hide();

    if ($("#ddlVendor").val() != "") {
        $.ajax({
            url: "/Customer/GetCustomerDisc",
            async: false,
            type: "POST",
            data: { CustId: $("#ddlVendor").val() },
            success: function (data) {
                if (data.Message != undefined) {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    if (data.Status == '1' && data.Data.length > 0) {
                        var i = 0, tot = data.Data.length;
                        var html = "", top = 0;
                        for (; i < tot; i++) {

                            cntRow = data.Data[i].RowNo;
                            html = "";
                            html += "<tr id='tr" + cntRow + "'>";
                            html += "<td>" + cntRow + "</td>";
                            html += "<td>" + '<select id="ddlShape' + cntRow + '" multiple="multiple" class="form-control" style="width: 100%">';
                            html += shapeDrop + '</select><input id="hdnCustDiscId' + cntRow + '" type="hidden" /></td>';
                            html += "<td>" + '<select id="ddlLab' + cntRow + '" class="form-control">';
                            html += labDrop + "</select></td>";
                            html += "<td>" + '<input type="number" id="txtCtsFrom' + cntRow + '" class="form-control" />' + "</td>";
                            html += "<td>" + '<input type="number" id="txtCtsTo' + cntRow + '" class="form-control" />' + "</td>";
                            html += "<td>" + '<select id="ddlColorFrom' + cntRow + '" class="form-control">';
                            html += colorDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlColorTo' + cntRow + '" class="form-control">';
                            html += colorDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlClarityFrom' + cntRow + '" class="form-control">';
                            html += clarityDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlClarityTo' + cntRow + '" class="form-control">';
                            html += clarityDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlCutFrom' + cntRow + '" class="form-control">';
                            html += cutDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlCutTo' + cntRow + '" class="form-control">';
                            html += cutDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlPolishFrom' + cntRow + '" class="form-control">';
                            html += polishDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlPolishTo' + cntRow + '" class="form-control">';
                            html += polishDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlSymmFrom' + cntRow + '" class="form-control">';
                            html += symmDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlSymmTo' + cntRow + '" class="form-control">';
                            html += symmDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlFlsFrom' + cntRow + '" class="form-control">';
                            html += flsDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlFlsTo' + cntRow + '" class="form-control">';
                            html += flsDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlDiscType' + cntRow + '" class="form-control">';
                            html += discTypeDrop + "</select></td>";
                            html += "<td>" + '<select id="ddlSign' + cntRow + '" class="form-control">';
                            html += signDrop + "</select></td>";
                            html += "<td>" + '<input type="text" onkeypress="return isPositiveNumber(event)" id="txtDisc' + cntRow + '" class="form-control" style="width:100px;" />' + "</td>";
                            html += "<td>" + '<i onclick="RemoveDisc(' + cntRow + ')" class="error fa fa-times-circle fa-2x"></i>' + "</td>";
                            html += "</tr>";

                            $(html).appendTo("#myTableBody");
                            $("#ddlShape" + cntRow).multiselect({
                                includeSelectAllOption: true
                            });

                            top = (10 * cntRow) + 420;
                            $("#ddlShape" + cntRow).parent().find('.dropdown-menu.show').css('top', top);
                            $("#ddlShape" + cntRow).val(data.Data[i].sShape.split(','));
                            $("#ddlShape" + cntRow).multiselect("refresh");

                            $("#hdnCustDiscId" + cntRow).val(data.Data[i].CustDiscId);
                            $("#ddlLab" + cntRow).val(data.Data[i].sLab);
                            $("#txtCtsFrom" + cntRow).val(formatNumberWithPoint(data.Data[i].sFromCts));
                            $("#txtCtsTo" + cntRow).val(formatNumberWithPoint(data.Data[i].sToCts));
                            $("#ddlColorFrom" + cntRow).val(data.Data[i].sFromColor);
                            $("#ddlColorTo" + cntRow).val(data.Data[i].sToColor);
                            $("#ddlClarityFrom" + cntRow).val(data.Data[i].sFromClarity);
                            $("#ddlClarityTo" + cntRow).val(data.Data[i].sToClarity);
                            $("#ddlCutFrom" + cntRow).val(data.Data[i].sFromCut);
                            $("#ddlCutTo" + cntRow).val(data.Data[i].sToCut);
                            $("#ddlPolishFrom" + cntRow).val(data.Data[i].sFromPolish);
                            $("#ddlPolishTo" + cntRow).val(data.Data[i].sToPolish);
                            $("#ddlSymmFrom" + cntRow).val(data.Data[i].sFromSymm);
                            $("#ddlSymmTo" + cntRow).val(data.Data[i].sToSymm);
                            $("#ddlFlsFrom" + cntRow).val(data.Data[i].sFromFls);
                            $("#ddlFlsTo" + cntRow).val(data.Data[i].sToFls);
                            $("#txtDisc" + cntRow).val(formatNumberWithPoint(data.Data[i].sDisc));
                            $("#ddlDiscType" + cntRow).val(data.Data[i].DiscType);
                            $("#ddlSign" + cntRow).val(data.Data[i].sSign);
                        }

                        $("#mytable").show();
                    }

                    $("#btnAddNew").show();

                    //var width = $('div[col-id="sToCts"].ag-header-cell').width();
                    //$('div[col-id="sToCts"].ag-header-cell').remove();
                    //$('div[col-id="sFromCts"].ag-header-cell').css('width', ($('div[col-id="sFromCts"].ag-header-cell').width() + width));
                }
                else {
                    window.location = GetLoginUrl();
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
}

function AddNewDisc() {
    $("#btnAddNew").attr("disabled", true);
    $("#mytable").show();

    cntRow += 1;

    var html = "<tr id='tr" + cntRow + "'>";
    html += "<td>" + cntRow.toString() + "</td>";
    html += "<td>" + '<select id="ddlShape' + cntRow + '" multiple="multiple" class="form-control" style="width: 100%">';
    html += shapeDrop + '</select><input id="hdnCustDiscId' + cntRow + '" type="hidden" /></td>';
    html += "<td>" + '<select id="ddlLab' + cntRow + '" class="form-control">';
    html += labDrop + "</select></td>";
    html += "<td>" + '<input type="number" id="txtCtsFrom' + cntRow + '" class="form-control" />' + "</td>";
    html += "<td>" + '<input type="number" id="txtCtsTo' + cntRow + '" class="form-control" />' + "</td>";
    html += "<td>" + '<select id="ddlColorFrom' + cntRow + '" class="form-control">';
    html += colorDrop + "</select></td>";
    html += "<td>" + '<select id="ddlColorTo' + cntRow + '" class="form-control">';
    html += colorDrop + "</select></td>";
    html += "<td>" + '<select id="ddlClarityFrom' + cntRow + '" class="form-control">';
    html += clarityDrop + "</select></td>";
    html += "<td>" + '<select id="ddlClarityTo' + cntRow + '" class="form-control">';
    html += clarityDrop + "</select></td>";
    html += "<td>" + '<select id="ddlCutFrom' + cntRow + '" class="form-control">';
    html += cutDrop + "</select></td>";
    html += "<td>" + '<select id="ddlCutTo' + cntRow + '" class="form-control">';
    html += cutDrop + "</select></td>";
    html += "<td>" + '<select id="ddlPolishFrom' + cntRow + '" class="form-control">';
    html += polishDrop + "</select></td>";
    html += "<td>" + '<select id="ddlPolishTo' + cntRow + '" class="form-control">';
    html += polishDrop + "</select></td>";
    html += "<td>" + '<select id="ddlSymmFrom' + cntRow + '" class="form-control">';
    html += symmDrop + "</select></td>";
    html += "<td>" + '<select id="ddlSymmTo' + cntRow + '" class="form-control">';
    html += symmDrop + "</select></td>";
    html += "<td>" + '<select id="ddlFlsFrom' + cntRow + '" class="form-control">';
    html += flsDrop + "</select></td>";
    html += "<td>" + '<select id="ddlFlsTo' + cntRow + '" class="form-control">';
    html += flsDrop + "</select></td>";
    html += "<td>" + '<select id="ddlDiscType' + cntRow + '" class="form-control">';
    html += discTypeDrop + "</select></td>";
    html += "<td>" + '<select id="ddlSign' + cntRow + '" class="form-control">';
    html += signDrop + "</select></td>";
    html += "<td>" + '<input type="text" onkeypress="return isPositiveNumber(event)" id="txtDisc' + cntRow + '" class="form-control"  style="width:100px;" />' + "</td>";
    html += "<td>" + '<i onclick="RemoveDisc(' + cntRow + ')" class="error fa fa-times-circle fa-2x"></i>' + "</td>";
    html += "</tr>";

    $(html).appendTo("#myTableBody");
    $("#ddlShape" + cntRow).multiselect({
        includeSelectAllOption: true
    });
    var top = (10 * cntRow) + 420;

    $("#ddlShape" + cntRow).parent().find('.dropdown-menu').css('top', top);
    $("#btnAddNew").attr("disabled", false);
}

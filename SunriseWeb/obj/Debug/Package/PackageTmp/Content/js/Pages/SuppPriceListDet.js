var LOCATION_MAS = [];
var POINTER_MAS = [];
var prefix_row = $('#tblbody_Prefix').find('tr').length;
var prefix_row_cnt = 0;
var PrefixList = [];

$(document).ready(function () {
    Master_Bind();

    //if ($("#hdnId").val() != "0") {

    //}
    $("#tbl_Prefix").on('click', '.RemovePrefix', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tbl_Prefix #tblbody_Prefix").find('tr').length) == 0) {
            AddNewPrefix();
        }
        prefix_row_cnt = 1;
        prefix_row = 1;
        $("#tbl_Prefix #tblbody_Prefix tr").each(function () {
            $(this).find("td:eq(0)").html(prefix_row_cnt);
            prefix_row_cnt += 1;
            prefix_row += 1;
        });
        if (prefix_row > 0) {
            prefix_row = parseInt(prefix_row) - 1;
        }
    });

});
function SuppPriceList_View() {
    location.href = "/Settings/SuppPriceList"
}
function opendv(Name, set) {
    $(".tablinks").removeClass("active");
    $(".tabcontent").hide();

    $(".btn" + Name).addClass("active");
    $("#div" + Name).show();

    if (set == true) {
        $.ajax({
            url: "/Settings/TabSessionSet",
            type: "POST",
            data: { Type: Name },
            success: function (data, textStatus, jqXHR) {
            },
        });
    }
}
function Master_Bind() {
    $.ajax({
        url: "/Settings/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { OrderBy: "SupplierName ASC", For_RefNo_Pricing: true },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                $('#DdlSupplierName').append("<option value=''>Select</option>");
                _(data.Data).each(function (obj, i) {
                    $('#DdlSupplierName').append("<option value=\"" + obj.Id + "\">" + obj.SupplierName + "</option>");
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });

    if ($("#hdnId").val() != "0") {
        $.ajax({
            url: "/Settings/SupplierGetFrom_PriceList",
            async: false,
            type: "POST",
            data: { SupplierPriceList_Id: $("#hdnId").val() },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "1" && data.Data != null) {
                    $("#DdlSupplierName").val(data.Data[0].Supplier_Mas_Id);
                    SupplierNameChange();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    }

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            LOCATION_MAS = _.filter(data.Data, function (e) { return e.ListType == "LOCATION" });
            POINTER_MAS = _.filter(data.Data, function (e) { return e.ListType == 'POINTER' });
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    //$.ajax({
    //    url: "/Settings/Get_API_StockFilter",
    //    async: false,
    //    type: "POST",
    //    processData: false,
    //    contentType: false,
    //    success: function (data, textStatus, jqXHR) {
    //        if (data.Status == "1" && data.Data != null) {
    //            LOCATION_MAS = _.filter(data.Data, function (e) { return e.Type == "LOC" });
    //            POINTER_MAS = _.filter(data.Data, function (e) { return e.Type == "POINTER" });
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {
    //    }
    //});
}
function SupplierNameChange() {
    $('#tblbody_Prefix').html("");
    prefix_row = $('#tblbody_Prefix').find('tr').length;
    prefix_row_cnt = 0;
    debugger
    if ($("#DdlSupplierName").val() != "") {
        debugger
        CheckSupp_PrefixStkValue_Permision($("#DdlSupplierName").val());
        CheckSuppExistOrNot();

        $("#Save_btn").show();
        $("#dv_PrefixValue").show();
        
        debugger
        if ($("#hdnId").val() != "0") {
            Get_SupplierPrefix();
            Get_SuppStockValue();
        }
        else {
            AddNewPrefix();
            $("#tblFilters").hide();
            //$("#divButton").hide();

            $("#lblCustNoFound").show();
            $("#tblCustomerFilters").hide();
            $("#tblCustomerFilters #tblBodyCustomerFilters").html("");

            $("#lblCostNoFound").show();
            $("#tblCostFilters").hide();
            $("#tblCostFilters #tblBodyCostFilters").html("");
        }
    }
    else {
        $("#Save_btn").hide();
        $("#dv_PrefixValue").hide();
    }
}
function CheckSupp_PrefixStkValue_Permision(Id) {debugger
    $.ajax({
        url: "/Settings/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { Id: Id },
        success: function (data, textStatus, jqXHR) {
            debugger
            if (data.Status == "1" && data.Data != null) {
                debugger
                if (data.Data[0].NewRefNoGen == false) {
                    $(".btnStockPrefix").hide();
                    $("#divStockPrefix").html("");
                    opendv("StockValue", false);
                }
                if (data.Data[0].NewDiscGen == false) {
                    $(".btnStockValue").hide();
                    $("#divStockValue").html("");
                    opendv("StockPrefix", false);
                }

                if (data.Data[0].NewRefNoGen == true && data.Data[0].NewDiscGen == true) {
                    $.ajax({
                        url: "/Settings/TabSessionGet",
                        type: "POST",
                        success: function (data, textStatus, jqXHR) {
                            opendv(data, false);
                        },
                    });
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function CheckSuppExistOrNot() {
    $.ajax({
        url: "/Settings/SupplierGetFrom_PriceList",
        async: false,
        type: "POST",
        data: { Supplier_Mas_Id: $("#DdlSupplierName").val() },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                $("#hdnId").val(data.Data[0].Id);
            }
            else {
                $("#hdnId").val("0");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function Get_SupplierPrefix() {
    $("#loading").css("display", "block");
    setTimeout(function () {

        $.ajax({
            url: "/Settings/Get_SupplierPrefix",
            async: false,
            type: "POST",
            data: { SupplierPriceList_Id: $("#hdnId").val() },
            success: function (data, textStatus, jqXHR) {
                debugger
                $("#loading").css("display", "none");
                if (data.Status == "1" && data.Message == "SUCCESS") {
                    _(data.Data).each(function (_obj, i) {
                        var location = "", pointer = "";
                        prefix_row = parseInt(prefix_row) + 1;

                        pointer = "<option value=''>Select</option>";
                        _(POINTER_MAS).each(function (obj, i) {
                            pointer += "<option value=\"" + obj.Id + "\"" + (parseInt(obj.Id) == parseInt(_obj.Pointer_Id) ? 'Selected' : '') + ">" + obj.Value + "</option>";
                        });

                        location = "<option value=''>Select</option>";
                        _(LOCATION_MAS).each(function (obj, i) {
                            location += "<option value=\"" + obj.Id + "\"" + (parseInt(obj.Id) == parseInt(_obj.Location_Id) ? 'Selected' : '') + ">" + obj.Value + "</option>";
                        });

                        $('#tblbody_Prefix').append(
                            '<tr>' +
                            '<td class="tblbody_sr">' + prefix_row.toString() + '</td>' +
                            '<td><center><select onchange="ddlpointerOnChange(\'' + prefix_row + '\');" id="ddlpre_' + prefix_row + '" class="form-control prefixpotr">' + pointer + '</select></center></td>' +
                            '<td><center><select onchange="ddllocationOnChange(\'' + prefix_row + '\');" id="ddlloc_' + prefix_row + '" class="form-control prefixloc">' + location + '</select></center></td>' +
                            '<td><center><input value=\"' + _obj.Prefix + '\"" type="text" class="form-control prefix" autocomplete="off" id="txt_prefix' + prefix_row + '" /></center></td>' +
                            '<td style="width: 50px"><i style="cursor:pointer;" class="error RemovePrefix"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i></td>' +
                            '</tr>'
                        );
                    });

                }
                else {
                    AddNewPrefix();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#loading").css("display", "none");
            }
        });

    }, 50);
}
function AddNewPrefix() {
    var location = "", pointer = "";
    prefix_row = parseInt(prefix_row) + 1;

    pointer = "<option value=''>Select</option>";
    _(POINTER_MAS).each(function (obj, i) {
        pointer += "<option value=\"" + obj.Id + "\">" + obj.Value + "</option>";
    });

    location = "<option value=''>Select</option>";
    _(LOCATION_MAS).each(function (obj, i) {
        location += "<option value=\"" + obj.Id + "\">" + obj.Value + "</option>";
    });

    $('#tblbody_Prefix').append(
        '<tr>' +
        '<td class="tblbody_sr">' + prefix_row.toString() + '</td>' +
        '<td><center><select onchange="ddlpointerOnChange(\'' + prefix_row + '\');" id="ddlpre_' + prefix_row + '" class="form-control prefixpotr">' + pointer + '</select></center></td>' +
        '<td><center><select onchange="ddllocationOnChange(\'' + prefix_row + '\');" id="ddlloc_' + prefix_row + '" class="form-control prefixloc">' + location + '</select></center></td>' +
        '<td><center><input type="text" class="form-control prefix" autocomplete="off" id="txt_prefix' + prefix_row + '" /></center></td>' +
        '<td style="width: 50px"><i style="cursor:pointer;" class="error RemovePrefix"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i></td>' +
        '</tr>'
    );
}
function ddlpointerOnChange(id) {
    if ($("#ddlpre_" + id).val() != "") {
        var p = 0;
        $("#tbl_Prefix #tblbody_Prefix tr").each(function () {
            p = $(this).find('.prefixpotr').attr("Id").split("_")[1];
            if ($(this).find('.CustomColumn').val() != "") {
                if (p != parseInt(id) && $("#ddlpre_" + id).val() != "" && $("#ddlpre_" + id).val() == $(this).find('.prefixpotr').val()) {
                    toastr.error($("#ddlpre_" + id).children(":selected").text() + " alredy selected.");
                    $("#ddlpre_" + id).val("");
                }
            }
        });
    }
}
function ddllocationOnChange(id) {

}
function SavePrefixData() {
    var p = "", s = 0;
    $("#tbl_Prefix #tblbody_Prefix tr").each(function () {
        p = $(this).find('.prefixpotr').attr("Id").split("_")[1];
        if ($("#ddlpre_" + p).val() == "") {
            s = 1;
            toastr.error("Please select Pointer.");
            $("#ddlpre_" + p).focus();
            return false;
        }
        if ($("#txt_prefix" + p).val() == "") {
            s = 1;
            toastr.error("Please enter Prefix.");
            $("#txt_prefix" + p).focus();
            return false;
        }
    });

    if (s == 0) {
        PrefixList = [];
        $("#tbl_Prefix #tblbody_Prefix tr").each(function () {
            PrefixList.push({
                Supplier_Id: $("#DdlSupplierName").val(),
                Pointer_Id: $(this).find('.prefixpotr').val(),
                Location_Id: $(this).find('.prefixloc').val(),
                Prefix: $(this).find('.prefix').val()
            });
        });

        var obj = {};
        obj.SuppPre = PrefixList;
        $.ajax({
            url: "/Settings/Save_SuppPrefix",
            async: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ save_supprefix: obj }),
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, jqXHR) {
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
}
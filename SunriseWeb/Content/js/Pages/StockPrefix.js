var POINTER_MAS = [];
var prefix_row = $('#tblbody_Prefix').find('tr').length;
var prefix_row_cnt = 0;
var PrefixList = [];

$(document).ready(function () {
    Masters();
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

function Masters() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            POINTER_MAS = _.filter(data.Data, function (e) { return e.ListType == 'POINTER' });
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $.ajax({
        url: "/Settings/SupplierGet",
        async: false,
        type: "POST",
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                $('#DdlSupplierName').html("<option value=''>Select</option>");
                $('#DdlSupplierName').append("<option value='0'>SUNRISE</option>");
                _(data.Data).each(function (obj, i) {
                    $('#DdlSupplierName').append("<option value=\"" + obj.Id + "\">" + obj.sPartyName + "</option>");
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
function SupplierNameChange() {
    $('#tblbody_Prefix').html("");
    prefix_row = $('#tblbody_Prefix').find('tr').length;

    if ($("#DdlSupplierName").val() != "") {
        Get_SupplierPrefix();
        $("#Save_btn").show();
        $("#dv_PrefixValue").show();
    }
    else {
        $("#Save_btn").hide();
        $("#dv_PrefixValue").hide();
    }
}

function Get_SupplierPrefix() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    setTimeout(function () {
        
        $.ajax({
            url: "/Settings/Get_SupplierPrefix",
            async: false,
            type: "POST",
            data: { Supplier_Id: $("#DdlSupplierName").val() },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "1" && data.Message == "SUCCESS") {
                    _(data.Data).each(function (_obj, i) {
                        var pointer = "";
                        prefix_row = parseInt(prefix_row) + 1;

                        pointer = "<option value=''>Select</option>";
                        _(POINTER_MAS).each(function (obj, i) {
                            pointer += "<option value=\"" + obj.Id + "\"" + (parseInt(obj.Id) == parseInt(_obj.Pointer_Id) ? 'Selected' : '') + ">" + obj.Value + "</option>";
                        });
                      
                        $('#tblbody_Prefix').append(
                            '<tr>' +
                            '<td class="tblbody_sr">' + prefix_row.toString() + '</td>' +
                            '<td><center><select onchange="ddlpointerOnChange(\'' + prefix_row + '\');" id="ddlpre_' + prefix_row + '" class="form-control prefixpotr">' + pointer + '</select></center></td>' +
                            '<td><center><input value=\"' + _obj.Prefix + '\"" type="text" class="form-control prefix" autocomplete="off" id="txt_prefix' + prefix_row + '" /></center></td>' +
                            '<td style="width: 50px"><i style="cursor:pointer;" class="error RemovePrefix"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i></td>' +
                            '</tr>'
                        );
                    });
                    $("#Remove_btn").show();
                }
                else {
                    AddNewPrefix();
                    $("#Remove_btn").hide();
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    }, 50);
}
function AddNewPrefix() {
    var pointer = "";
    prefix_row = parseInt(prefix_row) + 1;

    pointer = "<option value=''>Select</option>";
    _(POINTER_MAS).each(function (obj, i) {
        pointer += "<option value=\"" + obj.Id + "\">" + obj.Value + "</option>";
    });

    $('#tblbody_Prefix').append(
        '<tr>' +
        '<td class="tblbody_sr">' + prefix_row.toString() + '</td>' +
        '<td><center><select onchange="ddlpointerOnChange(\'' + prefix_row + '\');" id="ddlpre_' + prefix_row + '" class="form-control prefixpotr">' + pointer + '</select></center></td>' +
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
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        PrefixList = [];
        $("#tbl_Prefix #tblbody_Prefix tr").each(function () {
            PrefixList.push({
                Supplier_Id: $("#DdlSupplierName").val(),
                Pointer_Id: $(this).find('.prefixpotr').val(),
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
                    toastr.success(data.Message);

                    setTimeout(function () {
                        SupplierNameChange();
                    }, 1000);
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
function RemovePrefixData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    setTimeout(function () {

        $.ajax({
            url: "/Settings/Delete_SuppPrefix",
            async: false,
            type: "POST",
            data: { Supplier_Id: $("#DdlSupplierName").val() },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    toastr.error(data.Message);
                }
                else if (data.Status == "1") {
                    toastr.success("Prefix Delete Successfully");

                    setTimeout(function () {
                        SupplierNameChange();
                    }, 1000);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    }, 50);
}
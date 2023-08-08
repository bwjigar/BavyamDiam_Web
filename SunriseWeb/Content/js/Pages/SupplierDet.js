function Clear() {
    window.location = '/Settings/SupplierDet';
}
var API_List_View = function () {
    window.location.href = '/Settings/SupplierMas';
}
function Repeatevery() {
    if ($("#DdlRepeatevery").val() == "Minute") {
        $("#txtMinute").val("");
        $("#txtMinute").show();
        $("#txtHour").hide();
    }
    else if ($("#DdlRepeatevery").val() == "Hour") {
        $("#txtHour").val("");
        $("#txtMinute").hide();
        $("#txtHour").show();
    }
}
function SaveApiData() {
    if ($("#txtSupplierName").val() == "") {
        toastr.warning("Please Enter Supplier Name !", { timeOut: 2500 });
        $("#txtSupplierName").focus();
        return;
    }

    if ($("#txtURL").val() == "") {
        toastr.warning("Please Enter Supplier URL !", { timeOut: 2500 });
        $("#txtURL").focus();
        return;
    }

    if ($("#ddlAPIResponse").val() == "") {
        toastr.warning("Please Select API Response !", { timeOut: 2500 });
        $("#ddlAPIResponse").focus();
        return;
    }

    if ($("#txtFileName").val() == "") {
        toastr.warning("Please Enter File Name !", { timeOut: 2500 });
        $("#txtFileName").focus();
        return;
    }

    if ($("#txtFileLocation").val() == "") {
        toastr.warning("Please Enter File Location !", { timeOut: 2500 });
        $("#txtFileLocation").focus();
        return;
    }

    if ($("#LocationExportType").val() == "") {
        toastr.warning("Please Select Export Type !", { timeOut: 2500 });
        $("#LocationExportType").focus();
        return;
    }

    if ($("#DdlRepeatevery").val() == "Minute") {
        if ($("#txtMinute").val() == "") {
            toastr.warning("Please Enter Minute !", { timeOut: 2500 });
            $("#txtMinute").focus();
            return;
        }
    }
    else if ($("#DdlRepeatevery").val() == "Hour") {
        if ($("#txtHour").val() == "") {
            toastr.warning("Please Select Hour !", { timeOut: 2500 });
            $("#txtHour").focus();
            return;
        }
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var obj = {};
    obj.Id = $("#hdnId").val();
    obj.SupplierURL = $("#txtURL").val();
    obj.SupplierName = $("#txtSupplierName").val();
    obj.SupplierResponseFormat = $("#ddlAPIResponse").val();
    obj.SupplierAPIMethod = $("#ddlAPIMethod").val();
    obj.FileName = $("#txtFileName").val();
    obj.FileLocation = $("#txtFileLocation").val();
    obj.LocationExportType = $("#LocationExportType").val();
    obj.RepeateveryType = $("#DdlRepeatevery").val();
    obj.Repeatevery = $('#DdlRepeatevery').val() == "Minute" ? $("#txtMinute").val() : $("#txtHour").val();
    obj.Active = document.getElementById("APIStatus").checked;
    //obj.UserName = $("#txtUserName").val();
    //obj.Password = $("#txtPassword").val();
    obj.DiscInverse = document.getElementById("DiscInverse").checked;
    obj.NewRefNoGen = document.getElementById("NewRefNoGen").checked;
    obj.NewDiscGen = document.getElementById("NewDiscGen").checked;
    
    $.ajax({
        url: "/Settings/SaveSupplierMaster",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ saveapimst: obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "0") {
                toastr.error(data.Message, { timeOut: 2500 });
            }
            else if (data.Status == "1") {
                if ($("#hdnId").val() == "0") {
                    toastr.success("Supplier Save Successfully !!", { timeOut: 2500 });
                    setTimeout(function () {
                        location.href = window.location.href + "?Id=" + data.Message;
                    }, 1100);
                }
                else {
                    toastr.success("Supplier Update Successfully !!", { timeOut: 2500 });
                    setTimeout(function () {
                        location.reload(true);
                    }, 1100);
                }
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            toastr.error(textStatus);
        }
    });
}
function Get_APIMst(Id) {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/Settings/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { Id: Id },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                $("#txtURL").val(data.Data[0].SupplierURL);
                $("#txtSupplierName").val(data.Data[0].SupplierName);
                $("#ddlAPIResponse").val(data.Data[0].SupplierResponseFormat);
                $("#txtFileName").val(data.Data[0].FileName)
                $("#txtFileLocation").val(data.Data[0].FileLocation);
                $("#LocationExportType").val(data.Data[0].LocationExportType);
                $("#DdlRepeatevery").val(data.Data[0].RepeateveryType);
                Repeatevery();
                if ($("#DdlRepeatevery").val() == "Minute") {
                    $("#txtMinute").val(data.Data[0].Repeatevery);
                }
                else if ($("#DdlRepeatevery").val() == "Hour") {
                    $("#txtHour").val(data.Data[0].Repeatevery);
                }
                $("#ddlAPIMethod").val(data.Data[0].SupplierAPIMethod);
                $("#APIStatus").attr("checked", data.Data[0].Active);
                
                $("#NewRefNoGen").attr("checked", data.Data[0].NewRefNoGen);
                $("#NewDiscGen").attr("checked", data.Data[0].NewDiscGen);
                if (data.Data[0].NewDiscGen == true) {
                    $(".DiscInverse").show();
                    $("#DiscInverse").attr("checked", data.Data[0].DiscInverse);
                }
                else {
                    $(".DiscInverse").hide();
                    $("#DiscInverse").attr("checked", false);
                }
                
                //$("#txtUserName").val(data.Data[0].UserName);
                //$("#txtPassword").val(data.Data[0].Password);
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
$(document).ready(function () {
    if ($("#hdnId").val() != "0") {
        Get_APIMst($("#hdnId").val());
    }
});
function NewDiscGen() {
    if ($("#NewDiscGen").is(":checked")) {
        $(".DiscInverse").show();
        $("#DiscInverse").attr("checked", false);
    }
    else {
        $(".DiscInverse").hide();
    }
}
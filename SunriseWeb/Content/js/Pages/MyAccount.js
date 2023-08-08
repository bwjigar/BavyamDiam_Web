var validator = $('#frmMyAccount').validate({ // initialize the plugin
    ignore: "",
    rules: {
        field: {
            required: true,
            email: true
        }
    },
    messages: {
        FirstName: "Enter First Name",
        LastName: "Enter Last Name",
        CompanyName: "Enter Company Name",
        Address1: "Enter Address1",
        City: "Select City",
        ZipCode: "Enter Zip code",
        Country: "Enter Country",
        Mobile1: "Enter Mobile1",
        EmailId1: {
            required: "Enter Email Address 1",
            email: "Please Enter a Valid Email Address 1.",
        }
    },
    errorPlacement: function (error, element) {
        $('<div>' + error.html() + '</div>').appendTo('#diverror');  // suppresses error message text
    }
});
$(document).ready(function () {
    $('#btnSubmit').click(function () {
        $('#diverror').html("");
        var isvalid = $('#frmMyAccount').valid();
        var email = "0";

        var forgetfilter = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if ($("#txtEmailId1").val() != "") {
            if (!forgetfilter.test($("#txtEmailId1").val())) {
                email = "1";
                $('<div>Please Enter Valid Email Address 1 Format.</div>').appendTo('#diverror');
            }
        }
        if ($("#txtEmailId2").val() != "") {
            if (!forgetfilter.test($("#txtEmailId2").val())) {
                email = "2";
                $('<div>Please Enter Valid Email Address 2 Format.</div>').appendTo('#diverror');
            }
        }
        if (!isvalid) {
            $('#errorModal').modal('show');
            return false;
        }
        if (email != "0") {
            $('#errorModal').modal('show');
            return false;
        }


        var formData = new FormData();

        //var obj = {
        formData.append('FirstName', $('#txtFirstName').val());
        formData.append('LastName', $('#txtLastName').val());
        formData.append('CompanyName', $('#txtCompanyName').val());
        formData.append('CompanyAddress', $('#txtAddress1').val());
        formData.append('CompanyAddress2', $('#txtAddress2').val());
        formData.append('CompCity', $('#txtCity').val());
        formData.append('CompZipCode', $('#txtZipCode').val());
        formData.append('CompCountry', $('#txtCountry').val());
        formData.append('CompMobile', (($('#txtmobile1STDCode').val() == "" || $('#txtmobile1STDCode').val() == undefined || $('#txtmobile1STDCode').val() == null) ? $('#txtMobile1').val() : $('#txtmobile1STDCode').val() + '-' + $('#txtMobile1').val()));
        formData.append('CompMobile2', (($('#txtmobile2STDCode').val() == "" || $('#txtmobile2STDCode').val() == undefined || $('#txtmobile2STDCode').val() == null) ? $('#txtMobile2').val() : $('#txtmobile2STDCode').val() + '-' + $('#txtMobile2').val()));
        formData.append('CompPhone', (($('#txtoffice1STDCode').val() == "" || $('#txtoffice1STDCode').val() == undefined || $('#txtoffice1STDCode').val() == null) ? $('#txtOfficePh1').val() : $('#txtoffice1STDCode').val() + '-' + $('#txtOfficePh1').val()));
        formData.append('CompPhone2', (($('#txtoffice2STDCode').val() == "" || $('#txtoffice2STDCode').val() == undefined || $('#txtoffice2STDCode').val() == null) ? $('#txtOfficePh2').val() : $('#txtoffice2STDCode').val() + '-' + $('#txtOfficePh2').val()));
        formData.append('CompFaxNo', (($('#txtFaxSTDCode').val() == "" || $('#txtFaxSTDCode').val() == undefined || $('#txtFaxSTDCode').val() == null) ? $('#txtFaxNo').val() : $('#txtFaxSTDCode').val() + '-' + $('#txtFaxNo').val()));
        formData.append('CompEmail', $('#txtEmailId1').val());
        formData.append('RapnetID', $('#txtRapId').val());
        formData.append('CompRegNo', $('#txtBusiRegNo').val());
        formData.append('CompEmail2', $('#txtEmailId2').val());
        formData.append('WeChatId', $('#txtWeChatId').val());
        formData.append('SkypeId', $('#txtSkypeId').val());
        formData.append('Website', $('#txtWebsite').val());
        formData.append('CompCityId', parseInt($("#hdnCityCode").val()));
        formData.append('CompCountryId', parseInt($("#hdnCountryCode").val()));
        formData.append('IsProfileChanged', $("#hdnIsProfileChanged").val());
        if ($("#hdnIsProfileChanged").val() == '1' && $('#imageUpload').get(0).files.length > 0) {
            formData.append("_empImg", $("#hdnIsProfileChanged").val() == '1' ? $('#imageUpload').get(0).files[0] : null);
        }
        //formData.append('ProfilePic', $("#hdnIsProfileChanged").val() == '1' ? $('#imageUpload').files[0] ', null
        //};

        if (isvalid) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
            $.ajax({
                url: "/Account/UpdateUser",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "1") {
                        if ($("#hdnIsProfileChanged").val() == "1") {
                            $('#img-profile').attr('src', $('#imagePreview1').attr('src'));
                        }
                        $("#hdnIsProfileChanged").val("0");
                        toastr.success(data.Message);
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    }
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    });

    $(".number").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode

        if (!(keyCode >= 48 && keyCode <= 57)) {
            return false;
        } else {
            return;
        }
    });

    $(".alphabet").bind("keypress", function (e) {
        isIE = document.all ? 1 : 0
        keyEntry = !isIE ? e.which : event.keyCode;
        if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
            return true;
        else {
            return false;
        }
    });

    $("#txtCity").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Common/GetCitystateCountryAutoComplete",
                data: "{ 'sSearch': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.Data, function (item) {
                        return {
                            label: item.sCityName,
                            val: item.sCityName,
                            CityCode: item.iCityId,
                            CountryCode: item.iCountryId,
                            CountryName: item.sCountryName,
                            STDCode: item.iSTDCode,
                            ISDCode: item.sISDCode
                        }
                    }))
                },
                error: function (response) {
                    toastr.error(response.responseText);
                },
                failure: function (response) {
                    toastr.error(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#hdnCityCode").val(i.item.CityCode);
            $("#hdnCountryCode").val(i.item.CountryCode);
            $("#txtCity").val(i.item.val);
            $("#txtCountry").val(i.item.CountryName);
            $('.isdcode').val('+' + i.item.ISDCode);
            
            if (i.item.STDCode == "0") {
                $('.stdisdcode').val('+' + i.item.ISDCode);
                $('.stdcode').val('');
            } else {
                $('.stdisdcode').val('+' + i.item.ISDCode + ' ' + i.item.STDCode);
                $('.stdcode').val('+' + i.item.STDCode);
            }
        },
        change: function (event, ui) {
            if (!ui.item) {
                $("#hdnCityCode").val("0");
                $("#hdnCountryCode").val("0");
                $("#txtCity").val("");
                $("#txtCountry").val("");
                $('.isdcode').val("");
                $('.stdcode').val("");
                $('.stdisdcode').val("");
                toastr.warning('select city value from dropdown');
            }
        },
        minLength: 1
    });
    $('input[type="text"]').attr('autocomplete', 'nope');
    GetProfilePic();

});

var GetProfilePic = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: '/Account/GetProfileImage',
        type: "POST",
        success: function (msg, textStatus, jqXHR) {
            if (msg == "" || msg == "null") {
            }
            else if (msg != "Error found.") {
                var src = 'data:image/png;base64,' + msg;
                $('#imagePreview1').attr('src', src);
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }

    });
}


var Image_Browse1 = function (event) {

    var files = event.target.files; //FileList object
    var file = files[0];
    var Arr = [];
    var reader = new FileReader();
    Arr.push(file.name);
    setTimeout(function () {
        reader.readAsDataURL(file);
    }, 500);
    var PROFILEPICBIND1 = file.name;

    PROFILEPICBIND1 = Arr.length > 0 ? Arr[0] : "";
    if (event.target.files && event.target.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview1').attr('src', e.target.result);
        }
        reader.readAsDataURL(event.target.files[0]);
    }
    PROFILEPICBIND1 = Arr.length > 0 ? Arr[0] : "";
    $("#hdnIsProfileChanged").val(Arr.length > 0 ? '1' : '0')
    // $scope.UpdateProfilePic();
};

var Back = function () {
    window.location = '/DashBoard';
}

var ClearControl = function () {
    validator.resetForm();
    $('#txtUserName').val("");
    $('#txtPassword').val("");
    $('#txtFirstName').val("");
    $('#txtLastName').val("");
    $('#txtCompanyName').val("");
    $('#txtAddress1').val("");
    $('#txtAddress2').val("");
    $('#txtCity').val("");
    $('#txtZipCode').val("");
    $('#txtCountry').val("");
    $('#txtMobile1').val("");
    $('#txtMobile2').val("");
    $('#txtOfficePh1').val("");
    $('#txtOfficePh2').val("");
    $('#txtFaxNo').val("");
    $('#txtEmailId1').val("");
    $('#txtRapId').val("");
    $('#txtBusiRegNo').val("");
    $('#txtEmailId2').val("");
    $('#txtWeChatId').val("");
    $('#txtSkypeId').val("");
    $('#txtWebsite').val("");
    $("#hdnCityCode").val("");
    $("#hdnCountryCode").val("");
    $("#hdnIsProfileChanged").val("0");
    $('#imagePreview1').attr('src', '/Content/images/NA.png');
}

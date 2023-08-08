var Image_Browse1 = function (event) {
    var files = event.target.files; //FileList object
    if (files.length > 0) {
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
    }
    else {
        $("#hdnIsProfileChanged").val('0');
    }
};

var deltaIndicator = function (params) {
    var element = '<a href="javascript:void(0);" onclick="EditInfo(' + params.data.InformationID + ',\'' + params.data.InformationName + '\',\'' + params.data.FromDate + '\',\'' + params.data.ToDate + '\',\'' + params.data.FileName + '\',\'' + params.data.IsBeforeLogin + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
    element += '&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" onclick="DeleteInfo(' + params.data.InformationID + ',\'' + params.data.InformationName + '\',\'' + params.data.FromDate + '\',\'' + params.data.ToDate + '\',\'' + params.data.FileName + '\',\'' + params.data.IsBeforeLogin + '\')" ><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
    return element;
}

var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

var apiUrl = '';
var gridOptions = {};
var columnDefs = [
    { headerName: "InformationID", field: "InformationID", hide: true, tooltip: function (params) { return (params.value); } },
    { headerName: "Information Name", field: "InformationName", hide: false, tooltip: function (params) { return (params.value); }, width: 250 },
    { headerName: "From Date", field: "FromDate", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "To Date", field: "ToDate", tooltip: function (params) { return (params.value); }, width: 150 },
    {
        headerName: "View Image", tooltip: function (params) { return (params.value); }, field: "FileName", width: 80, cellRenderer: function (params) {
            var image_url = (params.value != null) ? 'frame.svg' : 'image-not-available.svg';
            var image_url1 = (params.value != null) ? (apiUrl + params.value) : 'javascript:void(0);';
            return '<ul class="flat-icon-ul"><li><a href="' + image_url1 + '" target="_blank" title="View Image"><img src="../Content/images/' + image_url + '" class="frame-icon"></li></ul></a>';
        },
        //suppressSorting: true,
        //suppressMenu: true,
    },
    { headerName: "Is Before Login", field: "IsBeforeLogin", tooltip: function (params) { return (params.value); }, width: 100, cellRenderer: IsBeforeLogin, },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 120, cellRenderer: 'deltaIndicator', }
];
function IsBeforeLogin(params) {
    if (params.data.IsBeforeLogin == true) {
        return "<span class='Yes'> Yes </span>";
    }
    else if (params.data.IsBeforeLogin == false) {
        return "<span class='Yes'> No </span>";
    }
}
function contentHeight(deduct) {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - deduct;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    
    jQuery.validator.addMethod("greaterThanDate", function (value, element) {
        var fDate = $.trim($('#txtFromDate').val());
        var tDate = $.trim($('#txtToDate').val());
        if (fDate != "" && tDate != "") {
            return (new Date(tDate) >= new Date(fDate));
        }
        else {
            $("#txtFromDate").removeClass("error");
            $("#txtToDate").removeClass("error");
            $("#txtFromDate-error").remove();
            $("#txtToDate-error").remove();
            return true;
        }
    }, "To date must be greater than From date");


    $("#frm").validate({
        rules: {
            Information: "required",
            From: {
                required: true,
                greaterThanDate: true,
            },
            To: {
                required: true,
                greaterThanDate: true,
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "Information" || element.attr("name") == "From" || element.attr("name") == "To") {
                if (element.attr("name") == "From" || element.attr("name") == "To") {
                    if ($("#txtFromDate-error").length > 0) {
                        $("#txtFromDate-error").remove();
                    }
                    if ($("#txtToDate-error").length > 0) {
                        $("#txtToDate-error").remove();
                    }
                    error.insertAfter($(element).parent());
                }
                error.insertAfter($(element).parent());
            }
            else {
                error.insertAfter(element);
            }
        }
    });

    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        //startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        //startDate: moment(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    });
    apiUrl = getApiUrl();
    apiUrl = apiUrl.replace(/api/g, 'InfoImages/');
    GetList();

    $('#btnSave').click(function () {
        $('#txtInformation').val($.trim($('#txtInformation').val()));
        var isvalid = $('#frm').valid();
        if (!isvalid) {
            return;
        }
        var formData = new FormData();

        var id = $('#hdnInformationID').val();
        id = isNaN(id) ? 0 : parseInt(id);
        var optType = (id > 0) ? 'Edit' : 'Save';

        formData.append('InformationID', id);
        formData.append('InformationName', $('#txtInformation').val());
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('OptType', optType);
        formData.append('IsBeforeLogin', document.getElementById("chkBLogin").checked);
        formData.append('IsProfileChanged', $("#hdnIsProfileChanged").val());
        formData.append('NaturalHeight', document.getElementById('imagePreview1').naturalHeight);
        formData.append('NaturalWidth', document.getElementById('imagePreview1').naturalWidth);
        
        if ($("#hdnIsProfileChanged").val() == '1' && $('#imageUpload').get(0).files.length > 0) {
            formData.append("_Img", $('#imageUpload').get(0).files[0]);
        }
        if (isvalid) {
            loaderShow();
            $.ajax({
                url: "/Information/Save",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (data, textStatus, jqXHR) {
                    MoveToErrorPage(data.Status);
                    if (data.Status != undefined) {
                        if (data.Status == "1") {
                            clearControls();
                            toastr.success(data.Message);
                            GetList();
                        } else {
                            toastr.error(data.Message);
                        }
                        loaderHide();
                    }
                    else {
                        window.location = GetLoginUrl();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    MoveToErrorPage(0);
                }
            });
        }
    });

    $('#btnReset').click(function () {
        clearControls();
    });
});

$(document).ready(function () {
    
    if (navigator.userAgent.indexOf('iPhone') > -1) {
        contentHeight(-200);
    }
    else if (navigator.userAgent.indexOf('iPad') > -1) {
        contentHeight(100);
    }
    else if (navigator.userAgent.indexOf('Windows') > -1) {
        contentHeight(70);
    }
    else {
        contentHeight(-100);
    }
});

$(window).resize(function (e) {
    
    if (navigator.userAgent.indexOf('iPhone') > -1) {
        contentHeight(-200);
    }
    else if (navigator.userAgent.indexOf('iPad') > -1) {
        contentHeight(100);
    }
    else if (navigator.userAgent.indexOf('Windows') > -1) {
        contentHeight(70);
    }
    else {
        contentHeight(-100);
    }
});

var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

var clearControls = function () {
    $('#hdnInformationID').val('');
    $("#hdnIsProfileChanged").val("0");
    $("#txtInformation").val("");
    $("#txtFromDate").val(F_date);
    $("#txtToDate").val(F_date);
    $("#imageUpload").val('');
    $('#imagePreview1').attr('src', '../Content/images/NA.png');
    document.getElementById("chkBLogin").checked = false;
    $('label.error').remove();
    $("#txtFromDate").removeClass("error");
    $("#txtToDate").removeClass("error");
}

function EditInfo(Id, Information, From, To, File, IsBeforeLogin) {
    $('#hdnInformationID').val(Id);
    $('#txtInformation').val(Information);
    DateSelect(From, To);

    if (IsBeforeLogin == "true") {
        document.getElementById("chkBLogin").checked = true;
    }
    else if (IsBeforeLogin == "false") {
        document.getElementById("chkBLogin").checked = false;
    }

    $("#hdnIsProfileChanged").val('0');
    if (File != null && File != 'null' && File != '')
        $('#imagePreview1').attr('src', (apiUrl + File));
    else
        $('#imagePreview1').attr('src', '../Content/images/NA.png');
}

function DeleteInfo(Id, Information, From, To, File, IsBeforeLogin) {
    $('#hdnInformationID').val(Id);
    $('#txtInformation').val(Information);
    DateSelect(From, To);
    if (IsBeforeLogin == "true") {
        document.getElementById("chkBLogin").checked = true;
    }
    else if (IsBeforeLogin == "false") {
        document.getElementById("chkBLogin").checked = false;
    }
    $("#hdnIsProfileChanged").val('0');
    $("#Remove").modal("show");

}

function DateSelect(FromDate, ToDate) {
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: FromDate,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: ToDate,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
}

var ClearRemoveModel = function () {
    $("#hdnInformationID").val("0");
    $("#Remove").modal("hide");
}

var DeleteInformation = function () {
    var id = $('#hdnInformationID').val();
    id = isNaN(id) ? 0 : parseInt(id);
    if (id > 0) {
        loaderShow();
        var formData = new FormData();
        var optType = 'Delete';
        formData.append('InformationID', id);
        formData.append('InformationName', $('#txtInformation').val());
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('OptType', optType);
        formData.append('IsBeforeLogin', document.getElementById("chkBLogin").checked);
        formData.append('IsProfileChanged', $("#hdnIsProfileChanged").val());
        
        $.ajax({
            url: "/Information/Save",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                MoveToErrorPage(data.Status);
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        clearControls();
                        toastr.success(data.Message, { timeOut: 3000 });
                        GetList();
                    } else {
                        toastr.error(data.Message);
                    }
                    loaderHide();
                    ClearRemoveModel();
                }
                else {
                    window.location = GetLoginUrl();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                MoveToErrorPage(0);
            }
        });
    }
    else {
        MoveToErrorPage(0);
    }
}

function GetList() {
    loaderShow();
    $.ajax({
        url: "/Information/GetList",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            MoveToErrorPage(data.Status);
            if (data.Status != undefined) {
                if (data.Status == "1") {
                    if (data.Data != null && data.Data.length > 0) {
                        BindList(data.Data);
                    }
                    else {
                        BindList([]);
                    }
                } else {
                    BindList([]);
                    //toastr.error(data.Message);
                }
                loaderHide();
            }
            else {
                window.location = GetLoginUrl();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            MoveToErrorPage(0);
        }
    });
}

function BindList(data) {
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        detailRowHeight: 70,
        defaultColDef: {
            enableSorting: true,
            sortable: true,
            resizable: true,
            filter: 'agTextColumnFilter',
            filterParams: {
                applyButton: true,
                resetButton: true,
            }
        },
        components: {
            deltaIndicator: deltaIndicator,
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        rowData: data,
        cacheBlockSize: 50,
        paginationPageSize: 10,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
}
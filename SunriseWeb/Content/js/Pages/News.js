var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function DateConvert(Date) {
    var dateParts = Date.split("/");
    return dateParts[0] + "-" + m_names[parseInt(dateParts[1]-1, 10)] + "-" + dateParts[2];
}
function BtnClick() {
    var URL, isValid = $('#frmNews').valid();
    if (!isValid) {
        return false;
    }
    else {
        CallingData("");
    }
}
function CallingData(when) {
    loaderShow();
    var msg = "", data = {}
    data.FromDate = $("#txtFromDate").val() != '' ? $("#txtFromDate").val() : '1900-01-01';
    data.ToDate = $("#txtToDate").val() != '' ? $("#txtToDate").val() : '1900-01-01';
    data.Description = $("#txtDescription").val();
    data.FontColor = $("#txtFontColor").val()
    data.iID = $("#hdnNewsId").val();
    
    if (when == "Delete") {
        data.Flag = "Delete";
        msg = "Record Deleted Successfully"
    }
    else if (when == "Select") {
        data.Flag = "Select";
    }
    else if ($("#hdnNewsId").val() == "0") {
        data.Flag = "Insert";
        msg = "Record Inserted Successfully"
    }
    else if ($("#hdnNewsId").val() != "0") {
        data.Flag = "Update";
        msg = "Record Updated Successfully"
    }
    
   
    $.ajax({
        url: "/User/NewsMaster",
        async: false,
        type: "POST",
        data: data,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (when == "Select") {
                $("#tblbody").html("");
                var _d = "", _Fdate = "", _Tdate = "";
                for (var i = 0; i < data.Data.length; i++) {
                    _Fdate = data.Data[i].FromDate != '' && data.Data[i].FromDate!= null ? DateConvert(data.Data[i].FromDate) : '';
                    _Tdate = data.Data[i].ToDate != '' && data.Data[i].ToDate != null ? DateConvert(data.Data[i].ToDate) : '';



                    _d += "<tr class='News'>" +
                        "<td class='News'>" + _Fdate + "</td>" +
                        "<td class='News'>" + _Tdate + "</td>" +
                        "<td class='News'>" + data.Data[i].Description + "</td>" +
                        "<td class='News'><span style='padding:6px 37px; background-color:" + data.Data[i].FontColor + "';></span></td>" +
                        "<td class='News'>" +
                        "<center>" +
                        "<a href='javascript: void (0);' onclick=\"EditInfo('" + data.Data[i].iID + "', '" + _Fdate + "', '" + _Tdate + "', \'" + data.Data[i].Description.replace(/'/g, ' &_ ') + "'\, '" + data.Data[i].FontColor + "')\"><i class='fa fa-pencil-square-o' aria-hidden='true' style='font-size:19px;'></i></a>&nbsp;" +
                        "<a href='javascript: void (0);' onclick=\"DeleteInfo('" + data.Data[i].iID + "')\"><i class='fa fa-trash-o' aria-hidden='true' style='font-size:19px;'></i></a>" +
                        "</center>" +
                        "</td>" +
                        "</tr>";
                } 
                $("#tblbody").append(_d);
            }
            else {
                if (data.Status == "0") {
                    toastr.error(data.Message);
                } else {
                    toastr.success(msg);
                    //ResetData();
                    CallingData("Select");
                }
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            loaderHide();
        }
    });
}
function EditInfo(iID, FromDate, ToDate, Description, FontColor) {
    ResetData();
    $("#hdnNewsId").val(iID);
    if (FromDate != '') {
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
    }
    else {
        $('#txtFromDate').val('');
    }
    if (ToDate != '') {
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
    else {
        $('#txtToDate').val('');
    }
    $("#txtDescription").val(Description.replace(/ &_ /g, "'"));
    $("#txtFontColor").val(FontColor);
    $(window).scrollTop(0);
}
function DeleteInfo(iID) {
    var r = confirm("Are you sure you want to delete this News ?");
    if (r == true) {
        $("#hdnNewsId").val(iID);
        CallingData("Delete");
        CallingData("Select");
    } 
}
function ResetData() {
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        autoUpdateInput: true,
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
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: SetCurrentDate(),
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
    
    $("#txtDescription").val("");
    $("#txtFontColor").val("#000000");
    $("#hdnNewsId").val("0");
}
function ToDateBlank() {
    $('#txtToDate').val("");
}

$(document).ready(function () {
    
    $("#txtToDate").keydown(function () {
        setTimeout(ToDateBlank, 400);
        setTimeout(ToDateBlank, 500);
        setTimeout(ToDateBlank, 600);
    });
    
    $('#frmNews').validate({
        rules: {
            txtFromDate: {
                required: true
            },
            txtDescription: {
                required: true
            }
        },
        messages: {
            txtFromDate: "",
            txtDescription: ""
        }
    });
    CallingData("Select");
    
});
var GIA = "";
var Image = "";
var Video = "";

function GetData(type) {
    var stoneid = '', certino = '';
    if (type == 'StockId') {
        stoneid = $("#txtStockId").val();
    } else if (type == 'CertiNo') {
        certino = $("#txtCertiNo").val();
    }
    
    if (stoneid != '' || certino != '') {
        loaderShow();
        var formData = new FormData();
        formData.append('StockId', stoneid);
        formData.append('CertiNo', certino);

        $.ajax({
            url: "/User/PacketTraceGetList",
            async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                GIA = "", Image = "", Video = "";
                $("#myGrid").show();
                var _Body = '';

                if (data.Data.length == 0) {
                    _Body += "<tr>";
                    _Body += "<td align='center' colspan='17' style='font-weight:700;'>NO DATA FOUND</td>";
                    _Body += "</tr>";

                    $("#btnGIA").hide();
                    $("#btnImage").hide();
                    $("#btnVideo").hide();
                }
                else {
                    for (var i = 0; i < data.Data.length; i++) {
                        _Body += "<tr>";
                        _Body += "<td align='center'>" + data.Data[i].TRANS_DATE.substring(0, 10); + "</td>";
                        _Body += "<td>" + (data.Data[i].PROCESS != null ? data.Data[i].PROCESS : '') + "</td>";
                        _Body += "<td>" + (data.Data[i].PARTY != null ? data.Data[i].PARTY : '') + "</td>";
                        _Body += "<td>" + (data.Data[i].SOURCE_PARTY != null ? data.Data[i].SOURCE_PARTY : '') + "</td>";
                        _Body += "<td align='center'>" + (data.Data[i].REF_NO != null ? data.Data[i].REF_NO : '') + "</td>";
                        _Body += "<td align='center'>" + (data.Data[i].CERTI_NO != null ? data.Data[i].CERTI_NO : '') + "</td>";
                        _Body += "<td align='right'>" + (formatNumberWithPoint(data.Data[i].CTS)) + "</td>";
                        _Body += "<td align='center'>" + (data.Data[i].COLOR != null ? data.Data[i].COLOR : '') + "</td>";
                        _Body += "<td align='center'>" + (data.Data[i].PURITY != null ? data.Data[i].PURITY : '') + "</td>";
                        _Body += "<td align='center'><span class='boldCls'>" + (data.Data[i].CUT != null ? data.Data[i].CUT : '') + "</span></td>";
                        _Body += "<td align='center'><span class='boldCls'>" + (data.Data[i].POLISH != null ? data.Data[i].POLISH : '') + "</span></td>";
                        _Body += "<td align='center'><span class='boldCls'>" + (data.Data[i].SYMM != null ? data.Data[i].SYMM : '') + "</span></td>";
                        _Body += "<td align='center'>" + (data.Data[i].FLS != null ? data.Data[i].FLS : '') + "</td>";
                        _Body += "<td align='right'>" + (formatNumberWithPoint(data.Data[i].DISC_OFFER)) + "</td>";
                        _Body += "<td align='right'>" + (formatNumberWithPoint(data.Data[i].RAP_PRICE)) + "</td>";
                        _Body += "<td align='right'>" + (formatNumberWithPoint(data.Data[i].RAP_VALUE)) + "</td>";
                        _Body += "<td align='center'>" + (data.Data[i].BGM != null ? data.Data[i].BGM : '') + "</td>";
                        _Body += "</tr>";
                    }
                    

                    if (data.Data[0].CERTI_NO != "" && data.Data[0].LAB == "GIA") {
                        //GIA = "javascript:window.open('http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=" + Convert.ToString(ViewState["CertiNo"]) + "');";
                        GIA = "https://www.sunrisediamonds.com.hk/certi/" + data.Data[0].CERTI_NO + ".pdf";
                    }
                    else {
                        GIA = "";
                    }

                    if (data.Data[0].REF_NO != "" && data.Data[0].WEB_IMG_STATUS == "Y") {
                        //Image = "javascript:window.open('" + @Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "") + @"/ViewImg.aspx?Loc=H&RefNo=" + data.Data[0].REF_NO + "');";
                        Image = $("#External_ImageURL").val() + data.Data[0].CERTI_NO + "/PR.jpg";
                    }
                    //else if (Convert.ToString(data.Data[0].REF_NO) != "" && (data.Data[0].WEB_IMG_STATUS == "N" || data.Data[0].WEB_IMG_STATUS == "") && data.Data[0].WEB_HDIMG_STATUS == "Y") {
                    //    Image = "javascript:window.open('" + @Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "") + @"/ViewImage.aspx?StoneID=" + Convert.ToString(data.Data[0].SEQ_NO) + "');";
                    //}
                    else {
                        Image = "";
                    }

                    //if (data.Data[0].WEB_IMG_STATUS == "N" && data.Data[0].WEB_HDIMG_STATUS == "Y") {
                    //    Video = "https://www.sunrisediamonds.com.hk/ViewVideoMp4.aspx?seqno=" + data.Data[0].SEQ_NO;
                    //    $("#btnVideo").show();
                    //} else if (data.Data[0].WEB_IMG_STATUS == "Y" && data.Data[0].WEB_HDIMG_STATUS == "N") {
                    //    Video = "https://cdn2.brainwaves.co.in/ViewHDImage.aspx?stoneid=" + data.Data[0].REF_NO;
                    //    $("#btnVideo").show();
                    //}
                    //else {
                    //    Video = "";
                    //    $("#btnVideo").hide();
                    //}

                    if (data.Data[0].SEQ_NO != "") {
                        Video = "https://www.sunrisediamonds.com.hk/ViewVideoMp4.aspx?seqno=" + data.Data[0].SEQ_NO;
                    }
                    else {
                        Video = "";
                    }
                    $("#btnGIA").show();
                    $("#btnImage").show();
                    $("#btnVideo").show();
                }
                $("#Body_PackageTrace").html(_Body);
                $("#txtStockId").val('');
                $("#txtCertiNo").val('');

                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                MoveToErrorPage(0);
                loaderHide();
            }
        });
    }
}
function GIA_Show() {
    if (GIA != '')
        window.open(GIA);
    else
        toastr.warning("No Certificate avilable !!");
}
function IMAGE_Show() {
    if (Image != '')
        window.open(Image);
    else
        toastr.warning("No Image avilable !!");
}
function VIDEO_Show() {
    if (Video != '')
        window.open(Video);
    else
        toastr.warning("No Video avilable !!");
}

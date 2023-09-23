$(document).ready(function () {
    Masters();
});
function Masters() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/Settings/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { OrderBy: "SupplierName ASC" },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                $('#DdlSupplierName').append("<option value=''>Select</option>");
                _(data.Data).each(function (obj, i) {
                    $('#DdlSupplierName').append("<option value=\"" + obj.Id + "\">" + obj.SupplierName + "</option>");
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
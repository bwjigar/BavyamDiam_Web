var pgSize = 50;
var showEntryVar = null;
var total_record = null;
var UserType = null;
var orderBy = '';
var Regular = true, Fancy = false;
var lbl = '<div style="float: left;font-size: 14px;position: absolute;left: 5%;transform: translateX(-50%);text-transform: capitalize;">'
    + '<button type="button" id="btnAddFilters" onclick="AddFilters();" class="offer-btn"><i class="fa fa-plus" aria-hidden="true"></i>Add Filters</button></div>';
var showEntryHtml = '<div class="show_entry">'
    + '<label>Show <select id="ddlPagesize" onchange="onPageSizeChanged()">'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="500">500</option>'
    + '</select> entries</label></div>';
var gridOptions = {};
var ErrorMsg = [];
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 15,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "Sr", field: "iSr", width: 40, tooltip: function (params) { return (params.value); }, sortable: false, hide: true },
    { headerName: "iUserid", field: "iUserid", width: 40, tooltip: function (params) { return (params.value); }, sortable: false, hide: true },
    { headerName: "User Name", field: "sUsername", tooltip: function (params) { return (params.value); }, width: 100, sortable: true },
    { headerName: "Type", field: "Type", tooltip: function (params) { return (params.value); }, width: 110, sortable: true, hide: true },
    { headerName: "Customer Name", field: "CustName", tooltip: function (params) { return (params.value); }, width: 123, sortable: true },
    { headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200, sortable: true },
    {
        headerName: "Primary user",
        field: "IsPrimary",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 50,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Sub user",
        field: "IsSubuser",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 40,
        sortable: true,
        cellRenderer: function (params) { if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; } }
    },
    {
        headerName: "Active",
        field: "IsActive",
        tooltip: function (params) { if (params.value == true) { return "Yes" } else if (params.value == false) { return "No" } },
        width: 40,
        sortable: true,
        cellRenderer: function (params) {
            if (params.value == true) { return '<p class="spn-Yes1">YES</p>'; }
            else if (params.value == false) { return '<p class="spn-No1">NO</p>'; }
        }
    },
    {
        headerName: "Action", field: "Action", width: 35, cellRenderer: 'deltaIndicator', sortable: false, hide: true,
        cellRenderer: function (params) {
            var element = '<a title="Edit User" onclick="Edit_StockDisc(\'' + params.data.iUserid + '\',' + '\'' + params.data.sUsername + '\')"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            element += '&nbsp;&nbsp;&nbsp;<a title="Delete User" onclick="Delete_StockDisc(' + params.data.iUserid + ')"><i class="fa fa-trash-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
            return element;
        }
    },
];
function selectAllRendererDetail(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');
    cb.setAttribute('id', 'checkboxAll');
    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            if (Filtered_Data.length > 0) {
                gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        }
        else {
            params.api.deselectAll();
            var data = [];
            gridOptions_Selected_Calculation(data);
        }

    });

    return eHeader;
}
function GetCustomerData() {
    $("#ddlCustomer").html("");
    $(".GidData").hide();
    if ($.trim($("#txtCompanyName").val()).length == 0) {
        $("#ddlCustomer").html("");
    }
    else {
        $.ajax({
            url: "/Customer/GetCustomer",
            async: false,
            type: "POST",
            data: { SearchText: $("#txtCompanyName").val() },
            success: function (data) {
                if (data.Status == '1') {
                    var list = data.Data;
                    var tot = list.length, i = 0;
                    //var selected = [];
                    for (; i < tot; i++) {
                        $("#ddlCustomer").append("<option value='" + list[i].iUserid + "'>" + list[i].sFullName + "</option>");
                        //selected.push(list[i].iUserid);
                    }
                    //$("#ddlCustomer").val(selected);
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}
function GetUserList() {
    if ($("#ddlCustomer").val().join(',') != "") {
        $(".GidData").show();
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        if (gridOptions.api != undefined) {
            gridOptions.api.destroy();
        }
        gridOptions = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 2,
            components: {
                myDetailCellRenderer: DetailCellRenderer
            },
            defaultColDef: {
                enableValue: false,
                enableRowGroup: false,
                enableSorting: false,
                sortable: false,
                resizable: true,
                enablePivot: false,
                filter: true
            },
            pagination: true,
            icons: {
                groupExpanded:
                    '<i class="fa fa-minus-circle"/>',
                groupContracted:
                    '<i class="fa fa-plus-circle"/>'
            },
            rowSelection: 'multiple',
            overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
            suppressRowClickSelection: true,
            columnDefs: columnDefs,
            rowModelType: 'serverSide',
            onGridReady: onGridReady,
            cacheBlockSize: pgSize, // you can have your custom page size
            paginationPageSize: pgSize, //pagesize
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };

        var gridDiv = document.querySelector('#myGrid');
        new agGrid.Grid(gridDiv, gridOptions);
        gridOptions.api.setServerSideDatasource(datasource1);

        $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

        showEntryVar = setInterval(function () {
            if ($('#myGrid .ag-paging-panel').length > 0) {
                $(lbl).appendTo('#myGrid .ag-paging-panel');
                $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                $('#ddlPagesize').val(pgSize);
                clearInterval(showEntryVar);
            }
        }, 1000);
        $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        });
    }
}
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var PageSize = pgSize;
        if (params.request.sortModel.length > 0) {
            orderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var obj1 = {
            iPgNo: PageNo,
            iPgSize: PageSize,
            sOrderBy: orderBy,
            UserList: $("#ddlCustomer").val().join(',')
        };
        $.ajax({
            url: "/Customer/Get_StockDiscMgt",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    total_record = data.Data[0].iTotalRec;
                    params.successCallback(data.Data, total_record);
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                gridOptions.api.showNoRowsOverlay();
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    }
};
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetUserList();
}
function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        resultHei = $(".Filters").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - resultHei - 75;
    contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}
function AddFilters() {
    if (_.pluck(_.filter(gridOptions.api.getSelectedRows()), 'iUserid').join(",") != "") {
        $("#h2Title").hide();
        $("#h2AddTitle").show();
        $("#divSearchFilter").hide();
        $("#divGrid").hide();
        $("#divAddFilters").show();
        //alert(_.pluck(_.filter(gridOptions.api.getSelectedRows()), 'iUserid').join(","));
        //alert(_.pluck(_.filter(gridOptions.api.getSelectedRows()), 'sUsername').join(","));
        $("#spnUsernmList").html('<span style="font-weight:600;">User Name :&nbsp;</span>' + _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'sUsername').join(", "));
        //$("#DivAddFilters").show();
        $(".import").hide();
        $(".order-title").addClass("col-xl-12");
        $("#btnBack").show();
    }
    else {
        toastr.warning("Please select user for Add Stock & Disc Filters");
    }
}
function BindKeyToSymbolList() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetKeyToSymbolList",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var KeytoSymbolList = data.Data;
            $('#searchkeytosymbol').html("");
            if (KeytoSymbolList.length > 0) {
                $.each(KeytoSymbolList, function (i, itm) {
                    $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                        + '<ul class="row m-0">'
                        + '<li class="carat-dropdown-chkbox">'
                        + '<div class="main-cust-check">'
                        + '<label class="cust-rdi-bx mn-check">'
                        + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + (i + 1) + '" name="radio' + (i + 1) + '" onclick="GetCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                        + '<span class="cust-rdi-check">'
                        + '<i class="fa fa-check"></i>'
                        + '</span>'
                        + '</label>'
                        + '<label class="cust-rdi-bx mn-time">'
                        + '<input type="radio" id="UNCHK_KTS_Radio_' + (i + 1) + '" class="checkradio" name="radio' + (i + 1) + '" onclick="GetUnCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                        + '<span class="cust-rdi-check">'
                        + '<i class="fa fa-times"></i>'
                        + '</span>'
                        + '</label>'
                        + '</div>'
                        + '</li>'
                        + '<li class="col">'
                        + '<span>' + itm.sSymbol + '</span>'
                        + '</li>'
                        + '</ul>'
                        + '</div>')
                });
                $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
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
function GetCheck_KTS_List(item) {
    var SList = _.reject(UnCheckKeyToSymbolList, function (e) { return e.Symbol == item });
    UnCheckKeyToSymbolList = SList;

    var res = _.filter(CheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        CheckKeyToSymbolList.push({
            "NewID": CheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
function GetUnCheck_KTS_List(item) {
    var SList = _.reject(CheckKeyToSymbolList, function (e) { return e.Symbol == item });
    CheckKeyToSymbolList = SList

    var res = _.filter(UnCheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        UnCheckKeyToSymbolList.push({
            "NewID": UnCheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
function resetKeytoSymbol() {
    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
}
var ModalShow = function (ParameterLabel, ObjLst) {
    $('#exampleModalLabel').text(ParameterLabel);
    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var c = 0, IsAllActiveC = 0;
    var list = [];
    list = ObjLst;
    list.forEach(function (item) {
        content += '<li id="li_' + ParameterLabel + '_' + (ParameterLabel == "Supplier" ? c : item.iSr) + '" onclick="ItemClicked(\'' + ParameterLabel + '\',\'' + item.sName + '\',\'' + (ParameterLabel == "Supplier" ? c : item.iSr) + '\', this);" class="';
        if (item.isActive) {
            content += 'active';

            if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
                || ParameterLabel == "Clarity" || ParameterLabel == "Cut") {
                IsAllActiveC = parseInt(IsAllActiveC) + 1;
            }
        }
        content += '">' + item.sName + '</li>';
        c = parseInt(c) + 1;
    });
    content += '</ul>';
    $('#divModal').empty();
    $('#divModal').append(content);

    $("#mpdal-footer").append('<button type="button" class="btn btn-primary" ng-click="ResetSelectedAttr(' + ParameterLabel + ');">Reset</button><button type="button" class="btn btn-secondary" data-dismiss="modal">Done</button>');

    if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
        || ParameterLabel == "Clarity" || ParameterLabel == "Cut") {
        if (IsAllActiveC == ObjLst.length - 1) {
            $("#li_" + ParameterLabel + "_0").addClass('active');
        }
    }
    $('#myModal').modal('toggle');
}
var ResetSelectedAttr = function (attr, obj) {
    _.each(obj, function (itm) {
        itm.isActive = false;
    });
    $(attr).empty();
}
var ItemClicked = function (ParameterLabel, item, c, curritem) {
    var list = [];
    if (ParameterLabel == 'Supplier') {
        list = SupplierList;
    }
    if (ParameterLabel == 'Location') {
        list = LocationList;
    }
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Carat') {
        list = PointerList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Cut') {
        list = CutList;
    }
    if (ParameterLabel == 'Polish') {
        list = PolishList;
    }
    if (ParameterLabel == 'Sym') {
        list = SymmList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
    if (ParameterLabel == 'Fls') {
        list = FlsList;
    }
    if (ParameterLabel == 'Bgm') {
        list = BgmList;
    }
    if (ParameterLabel == 'CrownBlack') {
        list = CrnBlackList;
    }
    if (ParameterLabel == 'TableBlack') {
        list = TblBlackList;
    }
    if (ParameterLabel == 'CrownWhite') {
        list = CrnWhiteList;
    }
    if (ParameterLabel == 'TableWhite') {
        list = TblWhiteList;
    }

    if (item == "ALL") {
        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut") {
            if (ParameterLabel == "Color" && item == "ALL" && $("#li_" + ParameterLabel + "_0").hasClass("active") == true) {
                R_F_All_Only_Checkbox_Clr_Rst("1");
            }
            else {
                for (var j = 0; j <= list.length - 1; j++) {
                    if (list[j].sName != "ALL") {
                        var itm = _.find(list, function (i) {
                            return i.sName == list[j].sName
                        });
                        if ($("#li_" + ParameterLabel + "_0").hasClass("active")) {
                            itm.isActive = true;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].iSr)).addClass('active');
                        }
                        else {
                            itm.isActive = false;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].iSr)).removeClass('active');
                        }
                        //itm.isActive = !itm.isActive;
                    }
                    else {
                        $("#li_" + ParameterLabel + "_0").toggleClass('active');
                    }
                }
            }
        }
        else if (ParameterLabel == "Carat") {
            _pointerlst = PointerList;
            $(".divCheckedPointerValue").empty();
            for (var j = 0; j <= list.length - 1; j++) {
                list[j].isActive = true;
                $('.divCheckedPointerValue').append('<li id="C_' + list[j].iSr + '" class="carat-li-top allcrt">' + list[j].sName + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + list[j].iSr + ');"></i></li>');
            }
        }
    }
    else {
        if (ParameterLabel == "Color") {
            R_F_All_Only_Checkbox_Clr_Rst("-0");
        }
        var itm = _.find(list, function (i) { return i.sName == item });
        if ($("#li_" + ParameterLabel + "_" + c).hasClass("active")) {
            itm.isActive = false;
            $("#li_" + ParameterLabel + "_" + c).removeClass('active');
        }
        else {
            itm.isActive = true;
            $("#li_" + ParameterLabel + "_" + c).addClass('active');
        }

        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut") {
            var IsAllActiveC = 0;
            for (var j = 0; j <= list.length - 1; j++) {
                if (list[j].sName != "ALL") {
                    if (list[j].isActive == true) {
                        IsAllActiveC = parseInt(IsAllActiveC) + 1;
                    }
                }
            }
            if (IsAllActiveC == list.length - 1) {
                $("#li_" + ParameterLabel + "_0").addClass('active');
            }
            else {
                $("#li_" + ParameterLabel + "_0").removeClass('active');
            }
        }
        //$(curritem).toggleClass('active');
    }
    SetSearchParameter();
}
function NewSizeGroupRemove(id) {
    $('#C_' + id).remove();
    var cList = _.reject(_pointerlst, function (e) { return e.iSr == id });
    _pointerlst = cList;
    SetSearchParameter();
}
function PricingMethodDDL() {
    if ($("#PricingMethod").val() == "") {
        document.getElementById("txtValue").disabled = true;
        document.getElementById("txtDisc").disabled = true;
        $("#txtValue").show();
        $("#txtDisc").hide();
        $("#txtValue").val("");
    }
    else {
        if ($("#PricingMethod").val() == "Disc") {
            document.getElementById("txtDisc").disabled = false;
            $("#txtDisc").show();
            $("#txtValue").hide();
            $("#txtDisc").val("");
        }
        if ($("#PricingMethod").val() == "Value") {
            document.getElementById("txtValue").disabled = false;
            $("#txtValue").show();
            $("#txtDisc").hide();
            $("#txtValue").val("");
        }
    }
    $("#lblPMErr").html("");
    $("#lblPMErr").hide();
}
function Reset() {
    $("#ddlCustomer").val([]);
    $("#txtCompanyName").val("");
    GetCustomerData();
    GetUserList();
    $(".GidData").hide();
}
var LeaveTextBox = function (ele, type) {
    if (type == "LENGTH") {
        $("#FromLength").val($("#FromLength").val() == "" ? "0.00" : $("#FromLength").val() == undefined ? "0.00" : parseFloat($("#FromLength").val()).toFixed(2));
        $("#ToLength").val($("#ToLength").val() == "" ? "0.00" : $("#ToLength").val() == undefined ? "0.00" : parseFloat($("#ToLength").val()).toFixed(2));

        var fromLength = parseFloat($("#FromLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromLength").val()).toFixed(2);
        var toLength = parseFloat($("#ToLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToLength").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromLength).toFixed(2) > parseFloat(toLength).toFixed(2)) {
                $("#ToLength").val(fromLength);
                if (fromLength == 0) {
                    $("#FromLength").val("");
                    $("#ToLength").val("");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toLength).toFixed(2) < parseFloat(fromLength).toFixed(2)) {
                $("#FromLength").val($("#ToLength").val());
                if (toLength == 0) {
                    $("#FromLength").val("");
                    $("#ToLength").val("");
                }
            }
        }
        if (parseFloat($("#FromLength").val()) == "0" && parseFloat($("#ToLength").val()) == "0") {
            $("#FromLength").val("");
            $("#ToLength").val("");
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
        if (parseFloat($("#FromWidth").val()) == "0" && parseFloat($("#ToWidth").val()) == "0") {
            $("#FromWidth").val("");
            $("#ToWidth").val("");
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
        if (parseFloat($("#FromDepth").val()) == "0" && parseFloat($("#ToDepth").val()) == "0") {
            $("#FromDepth").val("");
            $("#ToDepth").val("");
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
        if (parseFloat($("#FromDepthPer").val()) == "0" && parseFloat($("#ToDepthPer").val()) == "0") {
            $("#FromDepthPer").val("");
            $("#ToDepthPer").val("");
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
        if (parseFloat($("#FromTablePer").val()) == "0" && parseFloat($("#ToTablePer").val()) == "0") {
            $("#FromTablePer").val("");
            $("#ToTablePer").val("");
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
        if (parseFloat($("#FromCrAng").val()) == "0" && parseFloat($("#ToCrAng").val()) == "0") {
            $("#FromCrAng").val("");
            $("#ToCrAng").val("");
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
        if (parseFloat($("#FromCrHt").val()) == "0" && parseFloat($("#ToCrHt").val()) == "0") {
            $("#FromCrHt").val("");
            $("#ToCrHt").val("");
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
        if (parseFloat($("#FromPavAng").val()) == "0" && parseFloat($("#ToPavAng").val()) == "0") {
            $("#FromPavAng").val("");
            $("#ToPavAng").val("");
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
        if (parseFloat($("#FromPavHt").val()) == "0" && parseFloat($("#ToPavHt").val()) == "0") {
            $("#FromPavHt").val("");
            $("#ToPavHt").val("");
        }
    }
}
var GetError = function () {
    ErrorMsg = [];
    //if (_.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Supplier.",
    //    });
    //}
    //if (_.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Location.",
    //    });
    //}
    //if (_.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Shape.",
    //    });
    //}
    //if (_.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Carat.",
    //    });
    //}
    //if (_.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Color.",
    //    });
    //}
    //if (_.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Clarity.",
    //    });
    //}
    //if (_.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Cut.",
    //    });
    //}
    //if (_.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Fls.",
    //    });
    //}
    //if (_.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select Lab.",
    //    });
    //}
    //if (_.pluck(_.filter(BgmList, function (e) { return e.isActive == true }), 'sName').join(",") == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select BGM.",
    //    });
    //}
    if ($('#View:checked').val() == undefined && $('#Download:checked').val() == undefined) {
        ErrorMsg.push({
            'Error': "Please Select Used For.",
        });
    }
    if ($("#PricingMethod").val() == "") {
        ErrorMsg.push({
            'Error': "Please Select Price Method.",
        });
        ErrorMsg.push({
            'Error': "Please Enter Percentage of Price Method.",
        });
    }
    else if ($("#PricingMethod").val() != "") {
        if ($("#PricingMethod").val() == "Value" && $("#txtValue").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Value Percentage.",
            });
        }
        if ($("#PricingMethod").val() == "Disc" && $("#txtDisc").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Discount Percentage.",
            });
        }
    }
    return ErrorMsg;
}
var ErroClearRemoveModel = function () {
    $("#ErrorModel").modal("hide");
}
function AddNewRow() {
    ErrorMsg = GetError();
    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        $("#btnAddNewRow").attr("disabled", true);
        $("#mytable1").show();

        var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
        var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

        var cntRow = parseInt($("#mytable1 #myTableBody1").find('tr').length) + 1;

        var Supplier = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Location = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Carat = _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'sName').join(",");
        var Color_Type = (Regular_All == true ? "Regular" : (Fancy_All == true ? "Fancy" : ""));
        var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'sName').join(",");
        var F_INTENSITY = _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
        var F_OVERTONE = _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
        var F_FANCY_COLOR = _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
        var MixColor = "";
        if (Color != "") {
            MixColor = Color;
        }
        else if (FC != "") {
            MixColor = FC;
        }
        if (Color_Type != "") {
            MixColor = (Color_Type == "Regular" ? "<b>REGULAR ALL</b>" : Color_Type == "Fancy" ? "<b>FANCY ALL</b>" : "");
        }
        var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Sym = _.pluck(_.filter(SymmList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'sName').join(",");
        var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'sName').join(",");
        var FromLength = $("#FromLength").val() == "" || $("#FromLength").val() == undefined ? "" : $("#FromLength").val();
        var ToLength = $("#ToLength").val() == "" || $("#ToLength").val() == undefined ? "" : $("#ToLength").val();
        var FromWidth = $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined ? "" : $("#FromWidth").val();
        var ToWidth = $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined ? "" : $("#ToWidth").val();
        var FromDepth = $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined ? "" : $("#FromDepth").val();
        var ToDepth = $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined ? "" : $("#ToDepth").val();
        var FromDepthinPer = $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined ? "" : $("#FromDepthPer").val();
        var ToDepthinPer = $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined ? "" : $("#ToDepthPer").val();
        var FromTableinPer = $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined ? "" : $("#FromTablePer").val();
        var ToTableinPer = $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined ? "" : $("#ToTablePer").val();
        var FromCrAng = $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined ? "" : $("#FromCrAng").val();
        var ToCrAng = $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined ? "" : $("#ToCrAng").val();
        var FromCrHt = $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined ? "" : $("#FromCrHt").val();
        var ToCrHt = $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined ? "" : $("#ToCrHt").val();
        var FromPavAng = $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined ? "" : $("#FromPavAng").val();
        var ToPavAng = $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined ? "" : $("#ToPavAng").val();
        var FromPavHt = $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined ? "" : $("#FromPavHt").val();
        var ToPavHt = $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined ? "" : $("#ToPavHt").val();
        var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
        var dCheckKTS = KeyToSymLst_Check1;
        var dUNCheckKTS = KeyToSymLst_uncheck1;
        var BGM = _.pluck(_.filter(BgmList, function (e) { return e.isActive == true }), 'sName').join(",");
        var CrownBlack = _.pluck(_.filter(CrnBlackList, function (e) { return e.isActive == true }), 'sName').join(",");
        var TableBlack = _.pluck(_.filter(TblBlackList, function (e) { return e.isActive == true }), 'sName').join(",");
        var CrownWhite = _.pluck(_.filter(CrnWhiteList, function (e) { return e.isActive == true }), 'sName').join(",");
        var TableWhite = _.pluck(_.filter(TblWhiteList, function (e) { return e.isActive == true }), 'sName').join(",");

        var UsedFor = "", View = "", Download = "";
        View = ($('#View:checked').val() == "true" ? true : false);
        Download = ($('#Download:checked').val() == "true" ? true : false);
        UsedFor = (View == true ? "View" : "");
        UsedFor += (Download == true ? (View == true ? ", Download" : "Download") : "");

        var Image = $('#Img:checked').val();
        var Video = $('#Vdo:checked').val();
        var PriceMethod = $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val();
        var per = "";
        if ($("#PricingMethod").val() == "Disc") {
            per = $("#txtDisc").val();
        }
        if ($("#PricingMethod").val() == "Value") {
            per = $("#txtValue").val();
        }
        var Percentage = per;

        var html = "<tr id='tr'>";
        html += "<th class='Row Fi-Criteria' style=''>" + cntRow.toString() + "</th>";
        html += "<td style=''><span class='Fi-Criteria Supplier'>" + Supplier + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Location'>" + Location + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + Color_Type + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + Color + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + F_INTENSITY + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + F_OVERTONE + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + F_FANCY_COLOR + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria View'>" + View + "</span></td>";
        html += "<td style='display:none;'><span class='Fi-Criteria Download'>" + Download + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria UsedFor'>" + UsedFor + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Image'>" + Image + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Video'>" + Video + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + PriceMethod + "</span></td>";
        html += "<td style=''><span class='Fi-Criteria Percentage'>" + Percentage + "</span></td>";
        html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
        html += "</tr>";

        $("#mytable1 #myTableBody1").append(html);

        $("#btnAddNewRow").attr("disabled", false);
        Reset_API_Filter();
        if (parseInt($("#mytable1 #myTableBody1").find('tr').length) == 0) {
            $("#divButton").hide();
        }
        else {
            $("#divButton").show();
        }
    }
}
var SaveData = function () {
    if ($("#mytable1 #myTableBody1").find('tr').length == 0) {
        toastr.warning("Please Add Minimum 1 Stock & Disc Filter !");
        return;
    }

    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var List2 = [];
    $("#mytable1 #myTableBody1 tr").each(function () {
        var Index = $(this).index();
        var Supplier = $(this).find('.Supplier').html();
        var Location = $(this).find('.Location').html();
        var Shape = $(this).find('.Shape').html();
        var Carat = $(this).find('.Carat').html();
        var ColorType = $(this).find('.ColorType').html();
        var Color = $(this).find('.Color').html();
        var INTENSITY = $(this).find('.dCheckINTENSITY').html();
        var OVERTONE = $(this).find('.dCheckOVERTONE').html();
        var FANCY_COLOR = $(this).find('.dCheckFANCY_COLOR').html();
        var Clarity = $(this).find('.Clarity').html();
        var Cut = $(this).find('.Cut').html();
        var Polish = $(this).find('.Polish').html();
        var Sym = $(this).find('.Sym').html();
        var Fls = $(this).find('.Fls').html();
        var Lab = $(this).find('.Lab').html();
        var FromLength = $(this).find('.FromLength').html();
        var ToLength = $(this).find('.ToLength').html();
        var FromWidth = $(this).find('.FromWidth').html();
        var ToWidth = $(this).find('.ToWidth').html();
        var FromDepth = $(this).find('.FromDepth').html();
        var ToDepth = $(this).find('.ToDepth').html();
        var FromDepthinPer = $(this).find('.FromDepthinPer').html();
        var ToDepthinPer = $(this).find('.ToDepthinPer').html();
        var FromTableinPer = $(this).find('.FromTableinPer').html();
        var ToTableinPer = $(this).find('.ToTableinPer').html();
        var FromCrAng = $(this).find('.FromCrAng').html();
        var ToCrAng = $(this).find('.ToCrAng').html();
        var FromCrHt = $(this).find('.FromCrHt').html();
        var ToCrHt = $(this).find('.ToCrHt').html();
        var FromPavAng = $(this).find('.FromPavAng').html();
        var ToPavAng = $(this).find('.ToPavAng').html();
        var FromPavHt = $(this).find('.FromPavHt').html();
        var ToPavHt = $(this).find('.ToPavHt').html();
        var Keytosymbol = $(this).find('.Keytosymbol').html();
        var dCheckKTS = $(this).find('.dCheckKTS').html();
        var dUNCheckKTS = $(this).find('.dUNCheckKTS').html();
        var BGM = $(this).find('.BGM').html();
        var CrownBlack = $(this).find('.CrownBlack').html();
        var TableBlack = $(this).find('.TableBlack').html();
        var CrownWhite = $(this).find('.CrownWhite').html();
        var TableWhite = $(this).find('.TableWhite').html();
        var View = $(this).find('.View').html();
        var Download = $(this).find('.Download').html();
        var Image = $(this).find('.Image').html();
        var Video = $(this).find('.Video').html();
        var PriceMethod = $(this).find('.PriceMethod').html();
        var Percentage = $(this).find('.Percentage').html();

        List2.push({
            iSupplier: Supplier,
            iLocation: Location,
            sShape: Shape,
            sPointer: Carat,
            sColorType: ColorType,
            sColor: Color,
            sINTENSITY: INTENSITY,
            sOVERTONE: OVERTONE,
            sFANCY_COLOR: FANCY_COLOR,
            sClarity: Clarity,
            sCut: Cut,
            sPolish: Polish,
            sSymm: Sym,
            sFls: Fls,
            sLab: Lab,
            dFromLength: FromLength,
            dToLength: ToLength,
            dFromWidth: FromWidth,
            dToWidth: ToWidth,
            dFromDepth: FromDepth,
            dToDepth: ToDepth,
            dFromDepthPer: FromDepthinPer,
            dToDepthPer: ToDepthinPer,
            dFromTablePer: FromTableinPer,
            dToTablePer: ToTableinPer,
            dFromCrAng: FromCrAng,
            dToCrAng: ToCrAng,
            dFromCrHt: FromCrHt,
            dToCrHt: ToCrHt,
            dFromPavAng: FromPavAng,
            dToPavAng: ToPavAng,
            dFromPavHt: FromPavHt,
            dToPavHt: ToPavHt,
            dKeyToSymbol: Keytosymbol,
            dCheckKTS: dCheckKTS,
            dUNCheckKTS: dUNCheckKTS,
            sBGM: BGM,
            sCrownBlack: CrownBlack,
            sTableBlack: TableBlack,
            sCrownWhite: CrownWhite,
            sTableWhite: TableWhite,
            View: View,
            Download: Download,
            Img: Image,
            Vdo: Video,
            PriceMethod: PriceMethod,
            PricePer: Percentage
        });
    });

    var obj = {}
    obj.Type = "Insert";
    obj.Filters = List2;
    obj.UserIdList = _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'iUserid').join(",")

    $.ajax({
        url: "/Customer/SaveStockDisc",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ savestockdiscreq: obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();

            if (data.Status == "1") {
                toastr.success(data.Message);
                setTimeout(function () {
                    location.href = window.location.href;
                }, 1000);
            }
            else if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.warning(data.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            toastr.error(textStatus);
        }
    });
};
function Back() {
    $("#h2Title").show();
    $("#h2AddTitle").hide();
    $("#divSearchFilter").show();
    $("#divGrid").show();
    $("#divAddFilters").hide();
    $("#btnBack").hide();
    $(".import").show();
    $(".order-title").removeClass("col-xl-12");
}
function Reset_API_Filter() {
    ResetSelectedAttr('.divCheckedSupplierValue', SupplierList);
    ResetSelectedAttr('.divCheckedLocationValue', LocationList);
    ResetSelectedAttr('.divCheckedShapeValue', ShapeList);
    ResetCheckCarat();
    ResetCheckColors();
    Regular = true;
    Fancy = false;
    $("#Regular").addClass("btn-spn-opt-active");
    $("#Fancy").removeClass("btn-spn-opt-active");
    $("#div_Regular").show();
    $("#div_Fancy").hide();
    ResetSelectedAttr('.divCheckedColorValue', ColorList);
    ResetSelectedAttr('.divCheckedClarityValue', ClarityList);
    ResetSelectedAttr('.divCheckedCutValue', CutList);
    ResetSelectedAttr('.divCheckedPolValue', PolishList);
    ResetSelectedAttr('.divCheckedSymValue', SymmList);
    ResetSelectedAttr('.divCheckedFLsValue', FlsList);
    ResetSelectedAttr('.divCheckedLabValue', LabList);
    $("#FromLength").val("");
    $("#ToLength").val("");
    $("#FromWidth").val("");
    $("#ToWidth").val("");
    $("#FromDepth").val("");
    $("#ToDepth").val("");
    $("#FromDepthPer").val("");
    $("#ToDepthPer").val("");
    $("#FromTablePer").val("");
    $("#ToTablePer").val("");
    $("#FromCrAng").val("");
    $("#ToCrAng").val("");
    $("#FromCrHt").val("");
    $("#ToCrHt").val("");
    $("#FromPavAng").val("");
    $("#ToPavAng").val("");
    $("#FromPavHt").val("");
    $("#ToPavHt").val("");
    resetKeytoSymbol();
    ResetSelectedAttr('.divCheckedBGMValue', BgmList);
    ResetSelectedAttr('.divCheckedCrnBlackValue', CrnBlackList);
    ResetSelectedAttr('.divCheckedTblBlackValue', TblBlackList);
    ResetSelectedAttr('.divCheckedCrnWhiteValue', CrnWhiteList);
    ResetSelectedAttr('.divCheckedTblWhiteValue', TblWhiteList);

    $(".UsedFor").prop("checked", true);

    $(".IgAll").prop("checked", true);
    $(".VdAll").prop("checked", true);

    $("#PricingMethod").val("")
    $("#txtValue").show();
    $("#txtDisc").hide();
    $("#txtValue").val("");
    document.getElementById("txtValue").disabled = true;
    document.getElementById("txtDisc").disabled = true;

    supplierlst = "";
    locationLst = "";
    shapeLst = "";
    pointerlst = "";
    _pointerlst = [];
    colorLst = "";
    clarityLst = "";
    cutlst = "";
    Pollst = "";
    Symlst = "";
    labLst = "";
    flslst = "";
    bgmlst = "";
    crnblacklst = "";
    tblblacklst = "";
    crnwhitelst = "";
    tblwhitelst = "";

    CheckedSupplierValue = "";
    CheckedLocationValue = "";
    CheckedShapeValue = "";
    CheckedPointerValue = "";
    CheckedColorValue = "";
    CheckedClarityValue = "";
    CheckedCutValue = "";
    CheckedPolValue = "";
    CheckedSymValue = "";
    CheckedLabValue = "";
    CheckedFLsValue = "";
    CheckedBgmValue = "";
    CheckedCrnBlackValue = "";
    CheckedTblBlackValue = "";
    CheckedCrnWhiteValue = "";
    CheckedTblWhiteValue = "";
}
$(document).ready(function () {
    contentHeight();
    BindKeyToSymbolList();
    $('.sym-sec').on('click', function () {
        $('.sym-sec').toggleClass('active');
    });
    $('#btnSave').click(function () {
        GetUserList();
    });
    $('#btnReset').click(function () {
        Reset();
    });
    $('#btnAddNewRow').click(function () {
        AddNewRow();
    });
    $("#mytable1").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#mytable1 #myTableBody1").find('tr').length) == 0) {
            $("#mytable1").hide();
            $("#divButton").hide();
        }
        else {
            $("#divButton").show();
        }
        var idd = 1;
        $("#mytable1 #myTableBody1 tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });
    Bind_RColor();
    FcolorBind();
    $('#ColorModal').on('show.bs.modal', function (event) {
        color_ddl_close();
    });
    $('#chk_Regular_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Regular_All = $(this).is(':checked');
        SetSearchParameter();
    });
    $('#chk_Fancy_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Fancy_All = $(this).is(':checked');
        SetSearchParameter();
    });
});
function color_ddl_close() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
}
function numvalid(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function DisValValid(type, evt) {
    if (type == "Value") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    if (type == "Disc") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
}
function LenCheckr(type) {
    if (type == "Value") {
        if (parseFloat($("#txtValue").val()) > 100 || parseFloat($("#txtValue").val()) < 0) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only 0.00 to 100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtValue").val("");
        }
        else if (parseFloat($("#txtValue").val()) == NaN) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only 0.00 to 100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtValue").val("");
        }
        else {
            $("#lblPMErr").html("");
            $("#lblPMErr").hide();
        }
    }
    if (type == "Disc") {
        if (parseFloat($("#txtDisc").val()) < -100 || parseFloat($("#txtDisc").val()) > -0) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only -0.00 to -100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtDisc").val("");
        }
        else if (parseFloat($("#txtDisc").val()) == NaN) {
            setTimeout(function () {
                $("#lblPMErr").html("Allowed only -0.00 to -100 percentage.");
                $("#lblPMErr").show();
            }, 50);
            return $("#txtDisc").val("");
        }
        else {
            $("#lblPMErr").html("");
            $("#lblPMErr").hide();
        }
    }
}

$(window).resize(function () {
    contentHeight();
});
function Bind_RColor() {
    $("#divCheckedColorValue1").empty();
    for (var j = 0; j <= ColorList.length - 1; j++) {
        $('#divCheckedColorValue1').append('<li id="li_Color_' + ColorList[j].iSr + '" onclick="ItemClicked(\'Color\',\'' + ColorList[j].sName + '\',\'' + ColorList[j].iSr + '\', this);">' + ColorList[j].sName + '</li>');
    }
}
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        //$("#" + id).removeClass("btn-spn-opt-active");
        //if (id == "Regular") {
        //    Regular = false;
        //}
        //if (id == "Fancy") {
        //    Fancy = false;
        //}
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "Regular") {
            Regular = true;
            Fancy = false;
            $("#Fancy").removeClass("btn-spn-opt-active");
            $("#div_Regular").show();
            $("#div_Fancy").hide();
        }
        if (id == "Fancy") {
            Fancy = true;
            Regular = false;
            $("#Regular").removeClass("btn-spn-opt-active");
            $("#div_Regular").hide();
            $("#div_Fancy").show();
        }
        R_F_All_Only_Checkbox_Clr_Rst("1");
        ResetCheckColors();
    }
}
function R_F_All_Only_Checkbox_Clr_Rst(where) {
    if (where != "-1") {
        $('#chk_Regular_All').prop('checked', false);
        $('#chk_Fancy_All').prop('checked', false);
    }
    if (where == "-1") {
        where = "1";
    }
    if (where != "-0") {
        for (var j = 0; j <= ColorList.length - 1; j++) {
            if (ColorList[j].sName != "ALL") {
                ColorList[j].isActive = false;
            }
            $("#li_Color_" + ColorList[j].iSr).removeClass('active');
        }
    } 
    if (where == "-0") {
        where = "1";
    }

    Regular_All = false;
    Fancy_All = false;
    if (where == "1") {
        Check_Color_1 = [];
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
        $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
        Check_Color_2 = [];
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
        $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
        Check_Color_3 = [];
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
        $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    }


}
function INTENSITYShow() {
    setTimeout(function () {
        if (C1 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").show();
            C1 = 1;
            KTS = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec1 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function OVERTONEShow() {
    setTimeout(function () {
        if (C2 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").show();
            C2 = 1;
            C1 = 0, KTS = 0, C3 = 0;
        }
        else {
            $("#sym-sec2 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FANCY_COLORShow() {
    setTimeout(function () {
        if (C3 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").show();
            C3 = 1;
            C1 = 0, KTS = 0, C2 = 0;
        }
        else {
            $("#sym-sec3 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FcolorBind() {
    for (var i = 0; i <= INTENSITY.length - 1; i++) {
        $('#ddl_INTENSITY').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_I_' + i + '" name="CHK_I_' + i + '" onclick="GetCheck_INTENSITY_List(\'' + INTENSITY[i] + '\',' + i + ');">'
            + '<span class="cust-rdi-check" style="font-size:15px;">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + INTENSITY[i] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_INTENSITY').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var j = 0; j <= OVERTONE.length - 1; j++) {
        $('#ddl_OVERTONE').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_O_' + j + '" name="CHK_O_' + j + '" onclick="GetCheck_OVERTONE_List(\'' + OVERTONE[j] + '\',' + j + ');">'
            + '<span class="cust-rdi-check" style="font-size:15px;">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + OVERTONE[j] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_OVERTONE').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var k = 0; k <= FANCY_COLOR.length - 1; k++) {
        $('#ddl_FANCY_COLOR').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_F_' + k + '" name="CHK_F_' + k + '" onclick="GetCheck_FANCY_COLOR_List(\'' + FANCY_COLOR[k] + '\',' + k + ');" style="cursor:pointer;">'
            + '<span class="cust-rdi-check" style="font-size:15px;">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + FANCY_COLOR[k] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_FANCY_COLOR').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
}
function GetCheck_INTENSITY_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
    var res = _.filter(Check_Color_1, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_1 = [];
        if ($("#CHK_I_0").prop("checked") == true) {
            for (var i = 1; i <= INTENSITY.length - 1; i++) {
                Check_Color_1.push({
                    "NewID": Check_Color_1.length + 1,
                    "Symbol": INTENSITY[i],
                });
                $("#CHK_I_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= INTENSITY.length - 1; i++) {
                $("#CHK_I_" + i).prop('checked', false);
            }
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }
    else {
        $("#CHK_I_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_1.push({
                "NewID": Check_Color_1.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_1.length - 1; i++) {
                if (Check_Color_1[i].Symbol == item) {
                    $("#CHK_I_" + id).prop('checked', false);
                    Check_Color_1.splice(i, 1);
                }
            }
        }
        if (INTENSITY.length - 1 == Check_Color_1.length) {
            $("#CHK_I_0").prop('checked', true);
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }

    setTimeout(function () {
        $("#sym-sec1 .carat-dropdown-main").show();
    }, 2);
    Set_FancyColor();
}
function GetCheck_OVERTONE_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
    var res = _.filter(Check_Color_2, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_2 = [];
        if ($("#CHK_O_0").prop("checked") == true) {
            for (var i = 1; i <= OVERTONE.length - 1; i++) {
                Check_Color_2.push({
                    "NewID": Check_Color_2.length + 1,
                    "Symbol": OVERTONE[i],
                });
                $("#CHK_O_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= OVERTONE.length - 1; i++) {
                $("#CHK_O_" + i).prop('checked', false);
            }
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    else {
        $("#CHK_O_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_2.push({
                "NewID": Check_Color_2.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_2.length - 1; i++) {
                if (Check_Color_2[i].Symbol == item) {
                    $("#CHK_O_" + id).prop('checked', false);
                    Check_Color_2.splice(i, 1);
                }
            }
        }
        if (OVERTONE.length - 1 == Check_Color_2.length) {
            $("#CHK_O_0").prop('checked', true);
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec2 .carat-dropdown-main").show();
    }, 2);
    Set_FancyColor();
}
function GetCheck_FANCY_COLOR_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
    var res = _.filter(Check_Color_3, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_3 = [];
        if ($("#CHK_F_0").prop("checked") == true) {
            for (var i = 1; i <= FANCY_COLOR.length - 1; i++) {
                Check_Color_3.push({
                    "NewID": Check_Color_3.length + 1,
                    "Symbol": FANCY_COLOR[i],
                });
                $("#CHK_F_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= FANCY_COLOR.length - 1; i++) {
                $("#CHK_F_" + i).prop('checked', false);
            }
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    else {
        $("#CHK_F_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_3.push({
                "NewID": Check_Color_3.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_3.length - 1; i++) {
                if (Check_Color_3[i].Symbol == item) {
                    $("#CHK_F_" + id).prop('checked', false);
                    Check_Color_3.splice(i, 1);
                }
            }
        }
        if (FANCY_COLOR.length - 1 == Check_Color_3.length) {
            $("#CHK_F_0").prop('checked', true);
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec3 .carat-dropdown-main").show();
    }, 2);
    Set_FancyColor();
}
function resetINTENSITY() {
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
    C1 = 1;
    INTENSITYShow();
    SetSearchParameter();
}
function resetOVERTONE() {
    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
    C2 = 1;
    OVERTONEShow();
    SetSearchParameter();
}
function resetFANCY_COLOR() {
    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    C3 = 1;
    FANCY_COLORShow();
    SetSearchParameter();
}
function Set_FancyColor() {
    FC = "";
    if (Check_Color_1.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
        FC += _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
    }
    if (Check_Color_2.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
        FC += _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
    }
    if (Check_Color_3.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
        FC += _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
    }
    $(".divCheckedColorValue").empty();
    $(".divCheckedColorValue").append(FC);
    $(".divCheckedColorValue").attr({
        "title": FC
    });
}
//function bs_input_file() {
//    debugger
//    $(".input-file").before(
//        function () {debugger
//            if (!$(this).prev().hasClass('input-ghost')) {
//                debugger
//                var element = $("<input type='file' id='dataFile' name='upload' class='input-ghost' style='visibility:hidden; height:0'>");
//                element.attr("name", $(this).attr("name"));
//                element.change(function () {
//                    element.next(element).find('input').val((element.val()).split('\\').pop());
//                });
//                $(this).find("button.btn-choose").click(function () {
//                    element.click();
//                });
//                $(this).find("button.btn-reset").click(function () {
//                    element.val(null);
//                    $(this).parents(".input-file").find('input').val('');
//                });
//                $(this).find('input').css("cursor", "pointer");
//                $(this).find('input').mousedown(function () {
//                    $(this).parents('.input-file').prev().click();
//                    return false;
//                });
//                return element;
//            }
//        }
//    );
//}
//$(function () {
//    bs_input_file();
//});

$(document).on("click", "#btnUpload", function () {debugger
    var files = $("#importFile").get(0).files;
    if (files.length == 0) {
        toastr.warning("No File Selected");
    }
    else if ($("#importFile").get(0).files[0].name.split('.')[$("#importFile").get(0).files[0].name.split('.').length - 1] != "csv") {
        toastr.warning("Please Upload only .csv File");
    }
    else {
        var formData = new FormData();
        formData.append('importFile', files[0]);
        debugger
        $.ajax({
            url: '/Customer/ImportStockDisc',
            data: formData,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (data) {
                debugger
                if (data.Status == "1") {
                    toastr.success(data.Message);
                } else {
                    toastr.error(data.Message);
                }
            }
        });
    }
    $('#importFile').val(''); 
});  
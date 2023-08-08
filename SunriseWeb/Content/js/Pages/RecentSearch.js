var columnDefs = [
    {
        headerName: $("#hdn_Date").val(), field: "dTransDate", rowGroup: false, width: 110, tooltip: function (params) { return (params.value); }, },
    //{ headerName: $("#hdn_Total_found").val(), field: "Total_Rec", width: 200, tooltip: function (params) { return (params.value); }, },
    { headerName: $("#hdn_Criteria").val(), field: "Description", width: 662, tooltip: function (params) { return (params.value); }, },
    //{ headerName: "Load Search", field: "Load_search", width: 140, tooltip: function (params) { return (params.value); }, cellRenderer: LoadSearchGetter },
    { headerName: $("#hdn_Show_result").val(), field: "Show_result", width: 140, tooltip: function (params) { return (params.value); }, cellRenderer: ShowResultGetter },
];
var gridOptions = {};
function LoadSearchGetter(params) {
    return '<a title="Load Search" href="javascript:void(0);" onclick="LoadSearchData(' + params.data.iTransId + ',\'Recent\')"><i class="fa fa-search" aria-hidden="true"></i></a>';
}
function ShowResultGetter(params) {
    return '<a title="Show Result" href="javascript:void(0);" onclick="LoadSearchData(' + params.data.iTransId + ',\'Show\')"><i class="fa fa-long-arrow-right" aria-hidden="true"></i></a>';
}
function BindData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/GetRecentSearchList",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            gridOptions = {
                columnDefs: columnDefs,
                rowData: data.Data,
                enableSorting: true,
                suppressSorting: true,
                suppressCellSelection: true,
                enableColResize: true,
                suppressMenu: true,
                sortingOrder: ['desc', 'asc', null],
                rowSelection: 'multiple',
                overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
                suppressDragLeaveHidesColumns: true,
                headerHeight: 35,
                rowHeight: 30,
                animateRows: true,
                pagination: true,
                paginationAutoPageSize: false,
                detailRowHeight: 330,
                enableRangeSelection: true,
                rowMultiSelectWithClick: true,
                suppressRowClickSelection: true,
                onGridReady: onGridReady,
                masterDetail: true,
                detailCellRendererParams: {
                    detailGridOptions: {
                    },
                    getDetailRowData: function (params) {
                        params.successCallback(params.data.callRecords);
                    },
                },

            };
            var gridDiv = document.querySelector('#myGrid');
            new agGrid.Grid(gridDiv, gridOptions);            
           
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function onGridReady(params) {
    this.api.sizeColumnsToFit()
}
$(document).ready(function () {
    BindData();
});
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 86;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    contentHeight();
});
$(window).resize(function () {
    contentHeight();
});

function LoadSearchData(params, type) {
    var LoadSearchList_ = _.filter(gridOptions.rowData, function (e) { return (e.iTransId == params) });
    var obj = {
        "KeyToSymbol": LoadSearchList_[0].skeytosymbol,
        "FromCts": LoadSearchList_[0].FROM_CTS == 0 ? "" : LoadSearchList_[0].FROM_CTS,
        "ToCts": LoadSearchList_[0].TO_CTS == 0 ? "" : LoadSearchList_[0].TO_CTS,
        "Shape": LoadSearchList_[0].SHAPE,
        "Color": LoadSearchList_[0].COLOR,
        "Clarity": LoadSearchList_[0].CLARITY,
        "Cut": LoadSearchList_[0].CUT,
        "Lab": LoadSearchList_[0].LAB,
        "Polish": LoadSearchList_[0].POLISH,
        "Location": LoadSearchList_[0].LOCATION,
        "Symm": LoadSearchList_[0].SYMM,
        "Pointer": LoadSearchList_[0].POINTER,
        //  "BGM": LoadSearchList_[0].CERTI_NO,
        "Fls": LoadSearchList_[0].FLS,
        // "BLACK": LoadSearchList_[0].CERTI_NO,
        "Inclusion": LoadSearchList_[0].INCLUSION,
        "Natts": LoadSearchList_[0].NATTS,
        "CrownInclusion": LoadSearchList_[0].CROWN_INCLUSION,
        "CrownNatts": LoadSearchList_[0].CROWN_NATTS,
        "Luster": LoadSearchList_[0].LUSTER,

        "FormPricePerCts": LoadSearchList_[0].FROM_PRICECTS == 0 ? "" : LoadSearchList_[0].FROM_PRICECTS,
        "ToPricePerCts": LoadSearchList_[0].TO_PRICECTS == 0 ? "" : LoadSearchList_[0].TO_PRICECTS,
        "FormDisc": LoadSearchList_[0].FROM_DISC == 0 ? "" : LoadSearchList_[0].FROM_DISC,
        "ToDisc": LoadSearchList_[0].TO_DISC == 0 ? "" : LoadSearchList_[0].TO_DISC,

        //  "PricePerCtsTo": LoadSearchList_[0].CERTI_NO,
        //  "PricePerCtsTo": LoadSearchList_[0].CERTI_NO,

        "FormNetAmt": LoadSearchList_[0].FROM_NETAMT == 0 ? null : LoadSearchList_[0].FROM_NETAMT,
        "ToNetAmt": LoadSearchList_[0].TO_NETAMT == 0 ? null : LoadSearchList_[0].TO_NETAMT,

        "FromCrownHeight": LoadSearchList_[0].FROM_CR_HT == 0 ? null : LoadSearchList_[0].FROM_CR_HT,
        "ToCrownHeight": LoadSearchList_[0].TO_CR_HT == 0 ? null : LoadSearchList_[0].TO_CR_HT,

        "FromCrownAngle": LoadSearchList_[0].FROM_CR_ANG == 0 ? null : LoadSearchList_[0].FROM_CR_ANG,
        "ToCrownAngle": LoadSearchList_[0].TO_CR_ANG == 0 ? null : LoadSearchList_[0].TO_CR_ANG,

        "FromPavHeight": LoadSearchList_[0].FROM_PAV_HT == 0 ? null : LoadSearchList_[0].FROM_PAV_HT,
        "ToPavHeight": LoadSearchList_[0].TO_PAV_HT == 0 ? null : LoadSearchList_[0].TO_PAV_HT,

        "FromPavAngle": LoadSearchList_[0].FROM_PAV_ANG == 0 ? null : LoadSearchList_[0].FROM_PAV_ANG,
        "ToPavAngle": LoadSearchList_[0].TO_PAV_ANG == 0 ? null : LoadSearchList_[0].TO_PAV_ANG,

        "FormTablePer": LoadSearchList_[0].FROM_TABLE_PER == 0 ? null : LoadSearchList_[0].FROM_TABLE_PER,
        "ToTablePer": LoadSearchList_[0].TO_TABLE_PER == 0 ? null : LoadSearchList_[0].TO_TABLE_PER,
        "FormDepthPer": LoadSearchList_[0].FROM_DEPTH_PER == 0 ? null : LoadSearchList_[0].FROM_DEPTH_PER,
        "ToDepthPer": LoadSearchList_[0].TO_DEPTH_PER == 0 ? null : LoadSearchList_[0].TO_DEPTH_PER,
        "FormLength": LoadSearchList_[0].FROM_LENGTH == 0 ? null : LoadSearchList_[0].FROM_LENGTH,
        "ToLength": LoadSearchList_[0].TO_LENGTH == 0 ? null : LoadSearchList_[0].TO_LENGTH,
        "FormWidth": LoadSearchList_[0].FROM_WIDTH == 0 ? null : LoadSearchList_[0].FROM_WIDTH,
        "ToWidth": LoadSearchList_[0].TO_WIDTH == 0 ? null : LoadSearchList_[0].TO_WIDTH,
        "FormDepth": LoadSearchList_[0].FROM_DEPTH == 0 ? null : LoadSearchList_[0].FROM_DEPTH,
        "ToDepth": LoadSearchList_[0].TO_DEPTH == 0 ? null : LoadSearchList_[0].TO_DEPTH,
        "CertiNo": LoadSearchList_[0].CERTI_NO,
        "StoneID": LoadSearchList_[0].STONE_REF_NO == 0 ? "" : LoadSearchList_[0].STONE_REF_NO,
        "ColorType": LoadSearchList_[0].ColorType,
        "Intensity": LoadSearchList_[0].Intensity,
        "Overtone": LoadSearchList_[0].Overtone,
        "Fancy_Color": LoadSearchList_[0].Fancy_Color,
        "Table_Open": LoadSearchList_[0].Table_Open,
        "Crown_Open": LoadSearchList_[0].Crown_Open,
        "Pav_Open": LoadSearchList_[0].Pav_Open,
        "Girdle_Open": LoadSearchList_[0].Girdle_Open,
    }
    
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/RecentSearchDataSessionStore",
        async: false,
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (type == 'Recent') {
                window.location.href = '/SearchStock/Search?type=Recent&Set=Loadsearch';
            }
            else if (type == 'Show') {
                window.location.href = '/SearchStock/Search?type=Recent';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });


    //loaderShow();
    //$http({
    //    method: "POST",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    url: '/RecentSearch/RecentSearchDataSessionStore',
    //    data: JSON.stringify({ model: obj }),
    //}).then(function (data) {

    //    loaderHide();
    //    if ($scope.Type == 'Recent') {
    //        window.location.href = '/SearchStock/Search1?type=' + $scope.Type + '&Set=' + 'Loadsearch';
    //    }
    //    else if ($scope.Type == 'Show') {
    //        $scope.Type = 'Recent';

    //        window.location.href = '/SearchStock/Search1?type=' + $scope.Type;
    //    }
    //});

}
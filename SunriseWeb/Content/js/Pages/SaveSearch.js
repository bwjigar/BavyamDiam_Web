var columnDefs = [
    { headerName: $("#hdn_Date").val(), field: "TransDate", width: 140, tooltip: function (params) { return (params.value); }, },
    { headerName: $("#hdn_Save_Name").val(), field: "sSearchName", width: 200, tooltip: function (params) { return (params.value); }, },
    { headerName: $("#hdn_Criteria").val(), field: "sDescription", width: 662, tooltip: function (params) { return (params.value); }, },
    //{ headerName: $("#hdn_Load_Search").val(), field: "Load_search", width: 140, tooltip: function (params) { return (params.value); }, cellRenderer: LoadSearchGetter },
    { headerName: $("#hdn_Show_result").val(), field: "Show_result", width: 140, tooltip: function (params) { return (params.value); }, cellRenderer: ShowResultGetter },
    { headerName: $("#hdn_Action").val(), field: "", width: 110, cellRenderer: DeleteAction, },
];
var SearchId = 0;
var gridOptions = {};
//function LoadSearchGetter(params) {
//    return '<a title="Load Search" href="javascript:void(0);" onclick="LoadSearchData(' + params.data.iSearchId + ',\'SaveSearch\')"><i class="fa fa-search" aria-hidden="true"></i></a>';
//}
function ShowResultGetter(params) {
    return '<a title="Show Result" href="javascript:void(0);" onclick="LoadSearchData(' + params.data.iSearchId + ',\'Show\')"><i class="fa fa-long-arrow-right" aria-hidden="true"></i></a>';
}
function DeleteAction(params) {
    return '<a title="Delete Saved Search" href="javascript:void(0);" onclick="DeleteSaveSearch(' + params.data.iSearchId + ')"><i class="fa fa-trash" aria-hidden="true"></i></a>';
}
function DeleteSaveSearch(id) {
    SearchId = id;
    $("#Remove").modal("show");
}
function ClearRemoveModel() {
    SearchId = 0;
    $("#Remove").modal("hide");
}
function RemoveSaveSearch() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/DeleteUserSearch",
        async: false,
        type: "POST",
        data: { SearchID: SearchId },
        success: function (data, textStatus, jqXHR) {
            SearchId = 0;
            $("#Remove").modal("hide");
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            } else {
                BindData();
                toastr.success(data.Message);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function BindData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/GetSavedSearchList",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (gridOptions.api != undefined) {
                gridOptions.api.destroy();
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
                masterDetail: true,
                onGridReady: onGridReady,
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
function onGridReady(params) {
    this.api.sizeColumnsToFit()
}
function LoadSearchData(params, type) {
    var LoadSearchList_ = _.filter(gridOptions.rowData, function (e) { return (e.iSearchId == params) });
    
    var obj = {
        "SearchID": params,
        "SearchName": LoadSearchList_[0].sSearchName ,
        "KeyToSymbol": LoadSearchList_[0].SkeyToSymbol,
        "FromCts": LoadSearchList_[0].dFromCts == 0 ? "" : LoadSearchList_[0].dFromCts,
        "ToCts": LoadSearchList_[0].dToCts == 0 ? "" : LoadSearchList_[0].dToCts,
        "Shape": LoadSearchList_[0].sShape,
        "Color": LoadSearchList_[0].sColor,
        "Clarity": LoadSearchList_[0].sClarity,
        "Cut": LoadSearchList_[0].sCut,
        "Lab": LoadSearchList_[0].sLab,
        "Polish": LoadSearchList_[0].sPolish,
        "Location": LoadSearchList_[0].location1,
        "Symm": LoadSearchList_[0].sSymm,
        "Pointer": LoadSearchList_[0].sPointer,
        "BGM": LoadSearchList_[0].bgm,
        "Fls": LoadSearchList_[0].sFls,
        "BLACK": LoadSearchList_[0].black,
        "Inclusion": LoadSearchList_[0].sInclusion,
        "Natts": LoadSearchList_[0].sNatts,
        "CrownInclusion": LoadSearchList_[0].sCrownInclusion,
        "CrownNatts": LoadSearchList_[0].sCrownNatts,
        "Luster": LoadSearchList_[0].LUSTER,

        "FormPricePerCts": LoadSearchList_[0].dFromPriceCts == 0 ? "" : LoadSearchList_[0].dFromPriceCts,
        "ToPricePerCts": LoadSearchList_[0].dToPriceCts == 0 ? "" : LoadSearchList_[0].dToPriceCts,
        "FormDisc": LoadSearchList_[0].dFromDisc == 0 ? "" : LoadSearchList_[0].dFromDisc,
        "ToDisc": LoadSearchList_[0].dToDisc == 0 ? "" : LoadSearchList_[0].dToDisc,

        //  "PricePerCtsTo": LoadSearchList_[0].CERTI_NO,
        //  "PricePerCtsTo": LoadSearchList_[0].CERTI_NO,

        "FormNetAmt": LoadSearchList_[0].dFromNetPrice == 0 ? null : LoadSearchList_[0].dFromNetPrice,
        "ToNetAmt": LoadSearchList_[0].dToNetPrice == 0 ? null : LoadSearchList_[0].dToNetPrice,

        "FromCrownHeight": LoadSearchList_[0].dFromCrHt == 0 ? null : LoadSearchList_[0].dFromCrHt,
        "ToCrownHeight": LoadSearchList_[0].dToCrHt == 0 ? null : LoadSearchList_[0].dToCrHt,

        "FromCrownAngle": LoadSearchList_[0].dFromCrAng == 0 ? null : LoadSearchList_[0].dFromCrAng,
        "ToCrownAngle": LoadSearchList_[0].dToCrAng == 0 ? null : LoadSearchList_[0].dToCrAng,

        "FromPavHeight": LoadSearchList_[0].dFromPavHt == 0 ? null : LoadSearchList_[0].dFromPavHt,
        "ToPavHeight": LoadSearchList_[0].dToPavHt == 0 ? null : LoadSearchList_[0].dToPavHt,

        "FromPavAngle": LoadSearchList_[0].dFromPavAng == 0 ? null : LoadSearchList_[0].dFromPavAng,
        "ToPavAngle": LoadSearchList_[0].dToPavAng == 0 ? null : LoadSearchList_[0].dToPavAng,

        "FormTablePer": LoadSearchList_[0].dFromTablePer == 0 ? null : LoadSearchList_[0].dFromTablePer,
        "ToTablePer": LoadSearchList_[0].dToTablePer == 0 ? null : LoadSearchList_[0].dToTablePer,
        "FormDepthPer": LoadSearchList_[0].dFromDepthPer == 0 ? null : LoadSearchList_[0].dFromDepthPer,
        "ToDepthPer": LoadSearchList_[0].dToDepthPer == 0 ? null : LoadSearchList_[0].dToDepthPer,
        "FormLength": LoadSearchList_[0].dFromLength == 0 ? null : LoadSearchList_[0].dFromLength,
        "ToLength": LoadSearchList_[0].dToLength == 0 ? null : LoadSearchList_[0].dToLength,
        "FormWidth": LoadSearchList_[0].dFromWidth == 0 ? null : LoadSearchList_[0].dFromWidth,
        "ToWidth": LoadSearchList_[0].dToWidth == 0 ? null : LoadSearchList_[0].dToWidth,
        "FormDepth": LoadSearchList_[0].dFromDepth == 0 ? null : LoadSearchList_[0].dFromDepth,
        "ToDepth": LoadSearchList_[0].dToDepth == 0 ? null : LoadSearchList_[0].dToDepth,
        "CertiNo": LoadSearchList_[0].sCertiNo,
        "StoneID": LoadSearchList_[0].sRefNo == 0 ? "" : LoadSearchList_[0].sRefNo,
        "HasImage": LoadSearchList_[0].bImage,
        "HasHDMovie": LoadSearchList_[0].bHDMovie,
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
        url: "/SearchStock/SavedSearchDataSessionStore",
        async: false,
        type: "POST",
        data: { obj: obj},
        success: function (data, textStatus, jqXHR) {
            

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (type == 'SaveSearch') {
                window.location.href = '/SearchStock/Search?type=SaveSearch&Set=Loadsearch';
            }
            else if (type == 'Show') {
                window.location.href = '/SearchStock/Search?type=SaveSearch';
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            params.successCallback([], 0);
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
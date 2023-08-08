var ShapeList = [];
var CaratList = [];
var LabList = [];
var ColorList = [];
var PolishList = [];
var FlouList = [];
var ClarityList = [];
var CutList = [];
var SymList = [];
var LocationList = [];
var AllD = false;
var gridDiv = document.querySelector('#Price-Revised-Grid');
var summary1 = [];
var GalleryDatalist = [];
var limit = 0;
var renderLimit = 0;
function OpenDownloadCheck() {
    if (gridOptions.api.getSelectedRows().length > 0) {
        $(".download-toggle #liAll").show();
    } else {
        $(".download-toggle #liAll").hide();
    }
}
function ALLDownload() {
    AllD = true;
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $("#customRadio4").prop("checked", true);
    $('#hdnDownloadType').val("Image");
    DownloadMedia();
    $('#hdnDownloadType').val("Certificate");
    DownloadMedia();
    $('#hdnDownloadType').val("Video");
    DownloadMedia();
    $('#hdnDownloadType').val("Excel");
    DownloadExcel();
    AllD = false;
}
function StatusValueGetter(params) {

    if (params.value == "N")
        return '<div class="newStatus"><span>N</span></div>';
    else if (params.value == "AVAILABLE")
        return '<div class="activeStatus"><span>A</span></div>';
    else if (params.value == "AVAILABLE OFFER")
        return '<div class="offerStatus"><span>O</span></div>';

    else if (params.value == "BUSS. PROCESS")
        return '<div class="busyStatus"><span>B</span></div>';
}
function LotValueGetter(params) {
    setTimeout(function () {
        $('.offercls').parent().addClass('offerrow');
        $('.upcomingcls').parent().addClass('upcomingrow');
    }, 0);
    if (params.data != undefined) {
        if (params.data.lab == "GIA") {
            return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
        }
        else if (params.data.lab == "HRD") {
            return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
        }
        else if (params.data.lab == "IGI") {
            return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.certi_no + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.data.lab + '</a>';
        }
        else {
            return '';
        }
    }
    else {
        return '';
    }

}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function ImageValueGetter(params) {

    //var image_url = (params.data.image_url != null) ? 'frame.svg' : 'image-not-available.svg';
    //var movie_url = (params.data.movie_url != null) ? 'video-recording.svg' : 'video-recording-not-available.svg';
    //var certi_url = (params.data.view_certi_url != null) ? 'medal.svg' : 'medal-not-available.svg';
    //var image_url1 = (params.data.image_url != null) ? params.data.image_url : 'javascript:void(0);';
    //var movie_url1 = (params.data.movie_url != null) ? params.data.movie_url : 'javascript:void(0);';
    //var certi_url1 = (params.data.view_certi_url != null) ? params.data.view_certi_url : 'javascript:void(0);';
    //var data =
    //    '<ul class="flat-icon-ul"><li><a href="' + image_url1 + '" target="_blank" title="View Diamond Images"><img src="../Content/images/' + image_url + '" class="frame-icon"></a></li><li><a href="' + movie_url1 + '" target="_blank" title="View Diamond Images"><img src="../Content/images/' + movie_url + '" class="frame-icon"></a></li><li><a href="' + certi_url1 + '" target="_blank" title="View Diamond Certificate"><img src="../Content/images/' + certi_url + '" class="medal-icon"></a></li></ul>';
    //return data;
    return params.value;
}
function onBodyScroll(params) {
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
}
//===========================================  Summery Calculation  =============================================//
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
            onSelectionChanged();
        }

    });

    return eHeader;
}

function GetSearchParameter() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    $.ajax({
        url: "/SearchStock/GetSearchParameter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var ParameterList = data.Data;
            ShapeList = _.filter(ParameterList, function (e) { return e.ListType == 'SHAPE' });
            CaratList = _.filter(ParameterList, function (e) { return e.ListType == 'POINTER' });
            LabList = _.filter(ParameterList, function (e) { return e.ListType == 'LAB' });
            ColorList = _.filter(ParameterList, function (e) { return e.ListType == 'COLOR' });
            PolishList = _.filter(ParameterList, function (e) { return e.ListType == 'POLISH' });
            FlouList = _.filter(ParameterList, function (e) { return e.ListType == 'FLS' });
            ClarityList = _.filter(ParameterList, function (e) { return e.ListType == 'CLARITY' });
            CutList = _.filter(ParameterList, function (e) { return e.ListType == 'CUT' });
            SymList = _.filter(ParameterList, function (e) { return e.ListType == 'SYMM' });
            LocationList = _.filter(ParameterList, function (e) { return e.ListType == 'LOCATION' });

            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
GetSearchParameter();

$(document).ready(function (e) {
    GetDashboardCount();
    $('#gallerypoplia').on('click', function () {
        $('#gallery-popup').toggleClass('show');
        $('.aggrid-section.gallery-grid').toggleClass('close');
    });
    $('#ExcelModalAll').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadio4').prop('checked', true);
        } else {
            $('#customRadio3').prop('checked', true);
        }
    });
    $('#EmailModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadiomail2').prop('checked', true);
        } else {
            $('#customRadiomail').prop('checked', true);
        }
    });
    $('.result-three li a.download-popup').on('click', function (event) {
        $('.download-toggle').toggleClass('active');
        event.stopPropagation();
    });
    $(document).click(function (event) {
        if (!$(event.target).hasClass('active')) {
            $(".download-toggle").removeClass("active");
        }
    });
    
});

function getValuesAsync1(field) {
    if (field == "shape" || field == "lab" || field == "pointer" || field == "color" || field == "clarity" || field == "cut" || field == "symm" || field == "fls"
        || field == "polish" || field == "Location") {
        return "agSetColumnFilter";
    }
    else if (field == "cts") {
        return "agNumberColumnFilter";
    }
    else {
        return false;
    }
}

function getValuesAsync(field) {
    if (field == "shape") {
        return _.pluck(ShapeList, 'Value');
    }
    else if (field == "lab") {
        return _.pluck(LabList, 'Value');
    }
    else if (field == "pointer") {
        return _.pluck(CaratList, 'Value');
    }
    else if (field == "color") {
        return _.pluck(ColorList, 'Value');
    }
    else if (field == "clarity") {
        return _.pluck(ClarityList, 'Value');
    }
    else if (field == "cut") {
        return _.pluck(CutList, 'Value');
    }
    else if (field == "symm") {
        return _.pluck(SymList, 'Value');
    }
    else if (field == "fls") {
        return _.pluck(FlouList, 'Value');
    }
    else if (field == "polish") {
        return _.pluck(PolishList, 'Value');
    }
    else if (field == "Location") {
        return _.pluck(LocationList, 'Value');
    }
    else {
        return null;
    }
}

var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    //  { headerName: "SR.NO", field: "Sr", rowGroup: false, width: 100 }, 
    {
        headerName: $("#hdn_View_Image").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, field: "ImagesLink", width: 90, cellRenderer: ImageValueGetter, suppressSorting: true,
        suppressMenu: true,
    },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "stone_ref_no", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            //return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
            return '<div class="stock-font"><a target="_blank" href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + params.data.stone_ref_no + '">' + params.data.stone_ref_no + '</a></div>';
        }
    },
    {
        headerName: $("#hdn_Location").val(), field: "Location", tooltip: function (params) { return (params.value); }, width: 70,
        cellClass: function (params) {
            if (params.data != undefined) {
                if (params.data.status == 'AVAILABLE OFFER') {
                    return 'offercls';
                }
                if (params.data.Location == 'Upcoming') {
                    return 'upcomingcls';
                }
            }
        },
        filter: getValuesAsync1("Location"),
        filterParams: {
            values: getValuesAsync("Location"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Status").val(), field: "StoneStatus", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) {

            if (params.data == undefined) {
                return '';
            }
            return params.data.StoneStatus;
        }
    },
    {
        headerName: $("#hdn_Shape").val(), field: "shape", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 60,
        filter: getValuesAsync1("shape"),
        filterParams: {
            values: getValuesAsync("shape"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Pointer").val(), field: "pointer", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 60,
        filter: getValuesAsync1("pointer"),
        filterParams: {
            values: getValuesAsync("pointer"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Lab").val(), field: "Lab", width: 40, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, cellRenderer: LotValueGetter,
        filter: getValuesAsync1("lab"),
        filterParams: {
            values: getValuesAsync("lab"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 70
    },
    {
        headerName: $("#hdn_Certi_No").val(), field: "certi_no", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, rowGroup: false, width: 80
    },
    {
        headerName: $("#hdn_Color").val(), field: "color", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 50,
        filter: getValuesAsync1("color"),
        filterParams: {
            values: getValuesAsync("color"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },// COL
    {
        headerName: $("#hdn_Clarity").val(), field: "clarity", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 60,
        filter: getValuesAsync1("clarity"),
        filterParams: {
            values: getValuesAsync("clarity"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },//CLAR
    {
        headerName: $("#hdn_CTS").val(), field: "cts", filter: 'agNumberColumnFilter', tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, width: 50, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
        filter: getValuesAsync1("cts"),
        filterParams: {
            values: getValuesAsync('cts'),
            resetButton: true,
            applyButton: true,
            filterOptions: ['inRange']
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Rap_Price_Doller").val(), field: "cur_rap_rate", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        }, width: 80, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        },
    },
    {
        headerName: $("#hdn_Rap_Amt_Doller").val(), field: "rap_amount", width: 90, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        },
    },

    {
        headerName: $("#hdn_Disc_Per").val(), field: "sales_disc_per", width: 60, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Net_Amt").val(), field: "net_amount", width: 90, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        }, cellStyle: { color: 'red', 'font-weight': 'bold' }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        },
    },
    {
        headerName: $("#hdn_Price_Cts").val(), field: "price_per_cts", width: 75, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return formatNumber(params.value)
        },
    },
    {
        headerName: $("#hdn_Cut").val(), field: "cut", width: 50, tooltip: function (params) { return (params.value); },
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        cellStyle: function (params) {
            if (params.value == undefined) {
                return '';
            }
            if (params.value == '3EX')
                return { 'font-weight': 'bold' };
        },
        filter: getValuesAsync1("cut"),
        filterParams: {
            values: getValuesAsync("cut"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Polish").val(), field: "polish", width: 60, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data != undefined) {
                if (params.data.cut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        },
        filter: getValuesAsync1("polish"),
        filterParams: {
            values: getValuesAsync("polish"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Symm").val(), field: "symm", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data != undefined) {
                if (params.data.cut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        },
        filter: getValuesAsync1("symm"),
        filterParams: {
            values: getValuesAsync("symm"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Fls").val(), field: "fls", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 50,
        filter: getValuesAsync1("fls"),
        filterParams: {
            values: getValuesAsync("fls"),
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab']
    },
    {
        headerName: $("#hdn_Length").val(), field: "length", width: 60, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Width").val(), field: "width", width: 50, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Depth").val(), field: "depth", width: 50, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Depth_Per").val(), field: "depth_per", width: 60, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Table_Per").val(), field: "table_per", width: 60, tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Key_to_symbol").val(), field: "symbol", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 350,
    },
    { headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    //{ headerName: "Luster /Milky", field: "Luster", tooltip: function (params) { return (params.value); }, width: 90 },
    {
        headerName: $("#hdn_Table_Black").val(), field: "table_natts", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 90
    },
    {
        headerName: $("#hdn_Crown_Natts").val(), field: "Crown_Natts", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 90
    },
    {
        headerName: $("#hdn_Table_White").val(), field: "inclusion", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 80
    },
    {
        headerName: $("#hdn_Crown_White").val(), field: "Crown_Inclusion", tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, width: 90
    },
    {
        headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, field: "crown_angle", width: 60, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_CR_HT").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, field: "crown_height", width: 50, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, field: "pav_angle", width: 60, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    {
        headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        }, field: "pav_height", width: 60, cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return parseFloat(params.value).toFixed(2);
        },
    },
    { headerName: $("#hdn_girdle").val(), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88 },
    {
        headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, field: "girdle_type", width: 90
    },
    {
        headerName: $("#hdn_Laser_in_SC").val(), tooltip: function (params) {
            if (params.value == undefined) {
                return '';
            }
            return (params.value);
        }, field: "sInscription", width: 90
    }
];
var gridOptions = {
    masterDetail: true,
    detailCellRenderer: 'myDetailCellRenderer',
    detailRowHeight: 70,
    groupDefaultExpanded: 1,
    components: {
        //deltaIndicator: deltaIndicator,
        // agColumnHeader: CustomHeader,
        statusIndicator: StatusValueGetter,
        ImageValueGetter: ImageValueGetter,
        LotValueGetter: LotValueGetter
    },
    defaultColDef: {
        enableSorting: true,
        sortable: true,
        resizable: true
    },
    pagination: true,
    icons: {
        groupExpanded:
            '<i class="fa fa-minus-circle"/>',
        groupContracted:
            '<i class="fa fa-plus-circle"/>'
    },
    rowSelection: 'multiple',
    onColumnVisible: columnVisible,
    onBodyScroll: onBodyScroll,
    onSelectionChanged: onSelectionChanged,
    overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
    suppressRowClickSelection: true,
    columnDefs: columnDefs,
    rowModelType: 'serverSide',
    cacheBlockSize: 50, // you can have your custom page size
    paginationPageSize: 50, //pagesize
    paginationNumberFormatter: function (params) {
        return '[' + params.value.toLocaleString() + ']';
    }
};

document.addEventListener('DOMContentLoaded', function () {
    agGrid.LicenseManager.setLicenseKey("345b4a029e68391149aa2162aaa0807c");
    GetRevisedPriceData();
    $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
});
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 135;
    $("#Price-Revised-Grid").css("height", contentHei);
}

$(document).ready(function () {
    $('#ConfirmOrderModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#frmSaveOrder #Selected').show();
            $('#frmSaveOrder #NotSelected').hide();
            $('.modal-footer #btnsaveOrderstone').show();
        } else {
            $('#frmSaveOrder #Selected').hide();
            $('#frmSaveOrder #NotSelected').show();
            $('.modal-footer #btnsaveOrderstone').hide();
        }
    });
    contentHeight();
});
$(window).resize(function () {
    contentHeight();
});
function closeOrderConfirmModal() {
    if ($("#hdnIsSubUser").val() == "False" || ($("#hdnIsSubUser").val() == "True" && $("#hdnOrderHisrory").val() == "True")) {
        window.location.href = "/Order/OrderHistory";
    }
    else {
        $('#order-confirm-modal').modal('hide');
    }
}
function columnVisible(params) {
    if (params.column.colId == 0 && params.visible) {
        $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');
        $('#Price-Revised-Grid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            onSelectionChanged();
        });
    }
}
function onSelectionChanged(params) {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    if (gridOptions.api.getSelectedRows().length > 0) {
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'cts'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'net_amount'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'rap_amount'), function (memo, num) { return memo + num; }, 0);
        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;
    } else {
        TOT_CTS = summary1.TOT_CTS;
        AVG_SALES_DISC_PER = summary1.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = summary1.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = summary1.TOT_NET_AMOUNT;
        TOT_PCS = summary1.TOT_PCS;
    }

    $('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(TOT_CTS) + '');
    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
    $('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
    $('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TOT_NET_AMOUNT) + '');
    $('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');
}
function GetRevisedPriceData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);
    
    setTimeout(function () {
        var a = $('.ag-header-select-all')[0];
        $(a).removeClass('ag-hidden');
    }, 100);
}
const datasource1 = {
    getRows(params) {
        
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        var obj1 = { pageNO: PageNo, certiNo: "" };

        if (params.request.filterModel.cts) {
            var str = "";
            if (params.request.filterModel.cts.operator == "AND" || params.request.filterModel.cts.operator == "OR") {
                if (params.request.filterModel.cts.condition1) {
                    str = params.request.filterModel.cts.condition1.filter + "-";
                    if (params.request.filterModel.cts.condition1.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition1.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition1.filter
                    }
                }
                if (params.request.filterModel.cts.condition2) {
                    if (str != "")
                        str = str + ",";
                    str = params.request.filterModel.cts.condition2.filter + "-";
                    if (params.request.filterModel.cts.condition2.filterTo != null) {
                        str = str + params.request.filterModel.cts.condition2.filterTo
                    } else {
                        str = str + params.request.filterModel.cts.condition2.filter
                    }
                }
            }
            else {
                str = params.request.filterModel.cts.filter + "-";
                if (params.request.filterModel.cts.filterTo != null) {
                    str = str + params.request.filterModel.cts.filterTo
                } else {
                    str = str + params.request.filterModel.cts.filter
                }
            }
            obj1.Pointer = str;
        }
        else {
            obj1.Pointer = "";
        }

        if (params.request.filterModel.shape) {
            obj1.Shape = params.request.filterModel.shape.values.join(",");
        }
        else {
            obj1.Shape = "";
        }
        
        if (params.request.filterModel.pointer) {
            obj1.Pointer = params.request.filterModel.pointer.values.join(",");
        }
        else {
            if (obj1.Pointer == undefined || obj1.Pointer == "")
                obj1.Pointer = "";
        }
        
        if (params.request.filterModel.Lab) {
            obj1.Lab = params.request.filterModel.Lab.values.join(",");
        }
        else {
            obj1.Lab = "";
        }

        if (params.request.filterModel.color) {
            obj1.Color = params.request.filterModel.color.values.join(",");
        }
        else {
            obj1.Color = "";
        }
        
        if (params.request.filterModel.polish) {
            obj1.Polish = params.request.filterModel.polish.values.join(",");
        }
        else {
            obj1.Polish = "";
        }

        if (params.request.filterModel.clarity) {
            obj1.Clarity = params.request.filterModel.clarity.values.join(",");
        }
        else {
            obj1.Clarity = "";
        }

        if (params.request.filterModel.fls) {
            obj1.Fls = params.request.filterModel.fls.values.join(",");
        }
        else {
            obj1.Fls = "";
        }

        if (params.request.filterModel.cut) {
            obj1.Cut = params.request.filterModel.cut.values.join(",");
        }
        else {
            obj1.Cut = "";
        }

        if (params.request.filterModel.symm) {
            obj1.Symm = params.request.filterModel.symm.values.join(",");
        }
        else {
            obj1.Symm = "";
        }

        if (params.request.filterModel.Location) {
            obj1.Location = params.request.filterModel.Location.values.join(",");
        }
        else {
            obj1.Location = "";
        }

        $.ajax({
            url: "/SearchStock/GetRevisedPriceStock",
            async: false,
            type: "POST",
            data: obj1,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    var DataList = [];
                    DataList = data.Data[0].DataList;
                    summary1 = data.Data[0].DataSummary;

                    $.map(DataList, function (obj) {
                        GalleryDatalist.push(obj);
                    });

                    DataList.forEach(function (itm) {

                        if (itm.movie_url != null || itm.movie_url != undefined)
                            itm.IsMovieUrl = true;
                        else
                            itm.IsMovieUrl = false;
                        if (itm.ImageUrl1 != null || itm.ImageUrl1 != undefined)
                            itm.IsImageUrl = true;
                        else
                            itm.IsImageUrl = false;
                        if (itm.view_certi_url != null || itm.view_certi_url != undefined)
                            itm.IsCertiUrl = true;
                        else
                            itm.IsCertiUrl = false;


                        itm.ImageUrl1 = itm.ImageUrl1 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl1;
                        itm.ImageUrl2 = itm.ImageUrl2 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl2;
                        itm.ImageUrl3 = itm.ImageUrl3 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl3;
                        itm.ImageUrl4 = itm.ImageUrl4 == null ? "../Content/images/no-img1.jpg" : itm.ImageUrl4;
                    });

                    params.successCallback(DataList, summary1.TOT_PCS);

                    $('#tab1cts').html($("#hdn_Cts").val() +' : ' + formatNumber(summary1.TOT_CTS) + '');
                    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : ' + formatNumber(summary1.AVG_SALES_DISC_PER) + '');
                    $('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() +' : $ ' + formatNumber(summary1.AVG_PRICE_PER_CTS) + '');
                    $('#tab1totAmt').html($("#hdn_Total_Amount").val() +' : $ ' + formatNumber(summary1.TOT_NET_AMOUNT) + '');
                    $('#tab1pcs').html($("#hdn_Pcs").val() +' : ' + summary1.TOT_PCS + '');

                }
                else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    $('#tab1cts').html($("#hdn_Cts").val() +' : 0');
                    $('#tab1disc').html($("#hdn_Avg_Disc_Per").val() +' : 0');
                    $('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() +' : $ 0');
                    $('#tab1totAmt').html($("#hdn_Total_Amount").val() +' : $ 0');
                    $('#tab1pcs').html($("#hdn_Pcs").val() +' : 0');
                }

                setTimeout(function () {
                    if (GalleryDatalist.length > 12) {
                        renderLimit = renderLimit + 12;
                        $('#btnLoadMore').show();
                    } else {
                        renderLimit = GalleryDatalist.length;
                        $('#btnLoadMore').hide();
                    }
                    BindGalleryView();
                }, 1000);
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
function LoadMore() {
    renderLimit = renderLimit + 12;
    if (GalleryDatalist.length > renderLimit) {
        $('#btnLoadMore').show();
    } else {
        renderLimit = GalleryDatalist.length;
        $('#btnLoadMore').hide();
    }
    BindGalleryView();
}
function BindGalleryView() {
    //$('#dvGalleryView1').html("");

    for (var i = limit; i < renderLimit; i++) {
        limit = limit + 1;

        $('#dvGalleryView').append('<div class="col-xl-2 col-lg-2 col-md-3 col-sm-6 col-12 my-1 px-1">' +
            '    <div class="gallery-card">' +
            '        <div class="card-img ">' +
            '            <img class="loading" altsrc="~/Content/images/no-img1.jpg" src="' + (GalleryDatalist[i].bPRimg ? $("#External_ImageURL").val() + GalleryDatalist[i].certi_no + "/PR.jpg" : "/Content/images/no-img1.jpg") + '">' +
            '        </div>' +
            '        <div class="grid-check-sign">' +
            '            <i class="fa fa-check"></i>' +
            '        </div>' +
            '        <div class="card-content src-shape-main-pcscroll">' +
            '            <div class="grid-box-main">' +
            '                <div class="center-text">' +
            '                    <div class="text-center">' +
            '                        <p class="heading">' + $("#hdn_Stock_Id").val() + ' : <span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
            '                    </div>' +
            '                    <p><span class="spc">' + $("#hdn_Shape").val() + '</span>:<span>"' + GalleryDatalist[i].shape + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Carat_Weight").val() + '</span>:<span>"' + GalleryDatalist[i].cts + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Color").val() + '</span>:<span>"' + GalleryDatalist[i].color + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Clarity").val() + '</span>:<span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Cut").val() + '</span>:<span>"' + GalleryDatalist[i].cut + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Fls").val() + '</span>:<span>"' + GalleryDatalist[i].fls + '"</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Discount").val() + '</span>:<span>"' + GalleryDatalist[i].sales_disc_per + '"%</span></p>' +
            '                    <p><span class="spc">' + $("#hdn_Net_Amt").val() + '</span>:<span>"' + GalleryDatalist[i].net_amount + '"$</span></p>' +
            '                </div>' +
            '            </div>' +
            '            <div class="text-center mt-1  ">' +
            //'                <a href="/DNA/StoneDetail?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '                <a href="https://4e0s0i2r4n0u1s0.com/clientvideo/viewdetail.html?StoneNo=' + GalleryDatalist[i].stone_ref_no + '" class="gallary-viewdetail-btn" target="_blank">' + $("#hdn_View_Details").val() + '</a>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '    <div class="inner-text">' +
            '        <div class="left-text">' +
            '            <p>' + $("#hdn_Ref").val() + ' :&nbsp;<span style="width: 78px; float: right; white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">"' + GalleryDatalist[i].stone_ref_no + '"</span></p>' +
            '            <p>' + $("#hdn_Lab").val() + ' : <span><a href="">"' + GalleryDatalist[i].lab + '"</a></span></p>' +
            '        </div>' +
            '        <div class="right-text">' +
            '            <p>' + $("#hdn_Clarity").val() + ' : <span>"' + GalleryDatalist[i].clarity + '"</span></p>' +
            '            <p>' + $("#hdn_Color").val() + ' : <span>"' + GalleryDatalist[i].color + '"</span></p>' +
            '        </div>' +
            '    </div>' +
            '</div>');
    }
}

/*--------------------------------------------------------ADD TO CART START--------------------------------------------------*/
function AddToCart() {
    var stoneList = [];
    stoneList = gridOptions.api.getSelectedRows();
    
    var availabelstonelist = '';
    var offerstonelist = '';
    if ($('#hdnisadminflg').val() == '1' || $('#hdnisempflg').val() == '1') {
        availabelstonelist = _.pluck(stoneList, 'stone_ref_no').join(",");
    } else {
        availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' }), 'stone_ref_no').join(",");
        offerstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' }), 'stone_ref_no').join(",");
    }

    if (availabelstonelist != '') {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/AddToCart",
            type: "POST",
            data: { stoneNo: availabelstonelist, transType: 'A' },
            success: function (data, textStatus, jqXHR) {
                data.Message = data.Message.replace('Stone(s) added in cart successfully', $("#hdn_Stones_added_in_cart_successfully").val());
                data.Message = data.Message.replace('Stone(s) removed from cart successfully', $("#hdn_Stones_removed_from_cart_successfully").val());
                data.Message = data.Message.replace('This Stones(s) are already added to cart', $("#hdn_This_Stones_are_already_added_to_cart").val());

                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    if (data.Message.indexOf('already added to cart') > -1) {
                        $('#cartresMsg').html('<div>' + data.Message + '</div>');
                        $('#cartModal').modal('show');
                        GetDashboardCount();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                } else {
                    if (offerstonelist != '') {
                        $('#cartresMsg').html(' <div>' + offerstonelist + ' </div>' +
                            '<div>' + $("#hdn_This_Stone_is_not_Available_Stone_You_can_add_only_available_Stone_into_Cart").val() + '...!</div>' +
                            ' <div>' + $("#hdn_Other_Stones_are_added_into_cart_successfully").val() + '...!</div>')
                    } else {
                        $('#cartresMsg').html(data.Message)
                    }
                    $('#cartModal').modal('show');
                    GetDashboardCount();
                }

                if (gridOptions != null) {
                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(false);
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
    else if (offerstonelist != '') {
        toastr.warning($("#hdn_Select_Avail_Stone_for_add_to_cart").val() + '!');
    }
    else {
        toastr.warning($("#hdn_No_Stone_Selected_for_add_to_cart").val() + '!');
    }
}

function GoToCart() {
    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
    if (stoneno == '') {
        window.location = "/Cart/Index";
    }
    else {
        AddToCart();
    }
}
/*--------------------------------------------------------ADD TO CART END----------------------------------------------------*/
/*--------------------------------------------------------ADD TO WISHLIST START----------------------------------------------*/

function AddToWishlist() {
    var stoneno = '';
    var count = 0;
    count = gridOptions.api.getSelectedRows().length;
    stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (count > 0) {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/AddToWishlist",
            type: "POST",
            data: { stoneNo: stoneno, transType: 'A' },
            success: function (data, textStatus, jqXHR) {
                data.Message = data.Message.replace('Stone(s) added in wishList successfully', $("#hdn_Stones_added_in_wishlist_successfully").val());
                data.Message = data.Message.replace('Stone(s) removed from wishList successfully', $("#hdn_Stones_removed_from_wishList_successfully").val());
                data.Message = data.Message.replace('Add in to wishList failed', $("#hdn_Add_in_to_wishList_failed").val());

                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    if (data.Message.indexOf('already added to wishlist') > -1) {
                        $('#wishlistresMsg').html(data.Message);
                        $('#WishlistModal').modal('show');
                        GetDashboardCount();
                    }
                    else {
                        toastr.error(data.Message);
                    }
                } else {
                    $('#wishlistresMsg').html(data.Message)
                    $('#WishlistModal').modal('show');
                    GetDashboardCount();
                }

                if (gridOptions != null) {
                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(false);
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
    } else {
        toastr.warning($("#hdn_No_stone_selected_for_add_to_wishlist").val() + '!');
    }
}

function GoToWishlist() {
    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (stoneno == '') {
        window.location = "/Wishlist/Index";
    }
    else {
        AddToWishlist(TN);
    }
}

function GetDashboardCount() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/Dashboard/GetDashboardCount",
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            $.each(data.Data, function (key, obj) {
                if (obj.Type == "MyCart") {
                    $('.cntCart').html(obj.sCnt);
                }
                else if (obj.Type == "WishList") {
                    $('.cntwishlist').html(obj.sCnt);
                }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
/*--------------------------------------------------------ADD TO WISHLIST END------------------------------------------------*/
function SendMail() {

    var isValid = $('#frmSendMail').valid();
    if (!isValid) {
        return false;
    }

    if ($('#customRadiomail').prop('checked')) {
        var sobj = {};
        sobj.FormName = 'Revised Price';
        sobj.ActivityType = 'Excel Export';

        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/SearchStock/EmailAllStone",
            type: "POST",
            data: { SearchCriteria: sobj, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val(), isRevised:true },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {
                    data.Message = data.Message.replace('Mail sent successfully', $("#hdn_Mail_sent_successfully").val());
                    toastr.success(data.Message);
                }
                $('#EmailModal').modal('hide');
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });

    } else {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        if (count > 0) {
            $.ajax({
                url: "/SearchStock/EmailSelectedStone",
                type: "POST",
                data: {
                    StoneID: stoneno, ToAddress: $('#txtemail').val(), Comments: $('#txtNotes').val(),
                    FormName: 'Revised Price', ActivityType: 'Excel Email'
                },
                success: function (data, textStatus, jqXHR) {
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else {
                        data.Message = data.Message.replace('Mail sent successfully', $("#hdn_Mail_sent_successfully").val());
                        toastr.success(data.Message);
                    }
                    CloseSendMailPopup();
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        } else {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            toastr.warning($("#hdn_No_stones_selected_to_send_email").val() + '!');
        }
    }
}
function OpenComparStoneModel() {
    var stoneno = '';
    var count = 0;
    count = gridOptions.api.getSelectedRows().length;
    stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");

    if (count <= 1) {
        return toastr.warning($("#hdn_Please_select_at_least_2_item_for_compare").val());
    }
    else if (count >= 5) {
        return toastr.warning($("#hdn_You_can_compare_maximum_4_item").val());
    }
    else {
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/CompareStones",
            type: "POST",
            data: { stoneNo: stoneno },
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {

                    var str = '';
                    if (data.Data.length > 0) {
                        var ComparStoneList = data.Data[0];
                        str += '<tbody><tr>';
                        str += '<th><span>' + $("#hdn_Stock_Id").val() + ' :</span></th>';
                        ComparStoneList.ReferenceNo.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Photo_Real").val() + ' :</span></th>';
                        ComparStoneList.Imge1.forEach(function (item) {
                            str += '<td>';
                            str += '<span>';
                            if (item != "") {
                                str += '<img src="' + item + '" /></span>';
                            }
                            else {
                                str += '<img src="/Content/images/no-img1.jpg" /></span>';
                            }
                            str += '</td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Status").val() + ' :</span></th>';
                        ComparStoneList.Status.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Shape").val() + ' :</span></th>';
                        ComparStoneList.Shape.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Certi_No").val() + ' :</span></th>';
                        ComparStoneList.Lab.forEach(function (item, i) {
                            str += '<td><span>' + ComparStoneList.Lab[i] + '&nbsp;' + ComparStoneList.CertiNo[i] + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_BGM").val() + ' :</span></th>';
                        ComparStoneList.Shade.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Color").val() + ' :</span></th>';
                        ComparStoneList.Color.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Clarity").val() + ' :</span></th>';
                        ComparStoneList.Clarity.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Carat_Weight").val() + ' :</span></th>';
                        ComparStoneList.CaratWeight.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Rap_Price_Doller").val() + ' :</span></th>';
                        ComparStoneList.RapPrice.forEach(function (item) {
                            str += '<td><span>' + formatNumber(item) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Rap_Amt_Doller").val() + ' :</span></th>';
                        ComparStoneList.RapAmt.forEach(function (item) {
                            str += '<td><span>' + formatNumber(item) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Disc_Per").val() + ' : </span></th>';
                        ComparStoneList.Disc.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.Disc[i]) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Net_Amt").val() + ' :</span></th>';
                        ComparStoneList.net_amount.forEach(function (item, i) {
                            str += '<td><span style="color: red">' + formatNumber(ComparStoneList.net_amount[i]) + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Cut").val() + ' :</span></th>';
                        ComparStoneList.Cut.forEach(function (item, i) {
                            if (item == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Polish").val() + ' :</span></th>';
                        ComparStoneList.Polish.forEach(function (item, i) {
                            if (ComparStoneList.Cut[i] == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Symm").val() + ' :</span></th>';
                        ComparStoneList.Symmetry.forEach(function (item, i) {
                            if (ComparStoneList.Cut[i] == '3EX')
                                str += '<td><span><strong>' + item + '</strong></span></td>';
                            else
                                str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Fls").val() + ' :</span></th>';
                        ComparStoneList.Flurescence.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Length").val() + ' :</span></th>';
                        ComparStoneList.Length.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Width").val() + ' :</span></th>';
                        ComparStoneList.Width.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Depth").val() + ' :</span></th>';
                        ComparStoneList.Depth.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Depth_Per").val() + ' :</span></th>';
                        ComparStoneList.TotalDepth.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_Per").val() + ' :</span></th>';
                        ComparStoneList.Table.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Key_to_symbol").val() + ' :</span></th>';
                        ComparStoneList.KeytoSymbol.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_White").val() + ' :</span></th>';
                        ComparStoneList.table_natts.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_White").val() + '  :</span></th>';
                        ComparStoneList.Crown_Natts.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Table_Black").val() + '  :</span></th>';
                        ComparStoneList.inclusion.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_Natts").val() + ' :</span></th>';
                        ComparStoneList.Crown_Inclusion.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Crown_Angle").val() + ' : </span></th>';
                        ComparStoneList.CrAng.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_CR_HT").val() + ' : </span></th>';
                        ComparStoneList.CrHt.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Pav_Ang").val() + ' :</span></th>';
                        ComparStoneList.PavAng.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Pav_HT").val() + ' :</span></th>';
                        ComparStoneList.PavHt.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '<tr>'
                        str += '<th><span>' + $("#hdn_Girdle_Type").val() + ' :</span></th>';
                        ComparStoneList.GirdleType.forEach(function (item) {
                            str += '<td><span>' + item + '</span></td>';
                        });
                        str += '</tr>';

                        str += '</tbody>';
                        $("#tblCompare").empty().append(str);
                    }

                    $('#CompareStone').modal('show');
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
function validmail(e) {
    var emailID = $(e).val();
    emailID = emailID.split(',');
    for (var i = 0; i < emailID.length; i++) {
        if (!checkemail(emailID[i])) {
            toastr.error($("#hdn_Invalid_email_format").val());
            $("#txtemail").val('');
            return;
        }
    }
}
function checkemail(valemail) {
    var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$)/;  ///^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (forgetfilter.test(valemail)) {

        return true;
    }
    else {

        return false;
    }
}
function CloseSendMailPopup() {
    $('#EmailModal').modal('hide');
    $('#txtemail').val("");
    $('#txtNotes').val("");
}
function ClearSendMail() {
    $('#txtemail').val("");
    $('#txtNotes').val("");
}
/*--------------------------------------------------------PLACE ORDER START--------------------------------------------------*/
function ConfirmOrderModal() {
    $('#ConfirmOrderModal').modal('hide');
    $('#Comments').val("");
}
function SaveOrder() {
    var isValid = $('#frmSaveOrder').valid();
    if (!isValid) {
        return false;
    }
    var stoneList = gridOptions.api.getSelectedRows();
    var availabelstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status == 'AVAILABLE' || e.status == 'NEW' }), 'stone_ref_no').join(",");
    var offerstonelist = _.pluck(_.filter(stoneList, function (e) { return e.status != 'AVAILABLE' && e.status != 'NEW' }), 'stone_ref_no').join(",");
    if (availabelstonelist != '') {
        $('#hdnAvailableStone').val(availabelstonelist);
        if (offerstonelist != '') {
            $('#PlaceOrderMsg').html(
                '<div>' + offerstonelist + ' ' + $("#hdn_PlaceOrderMsg_1").val() + ' ...!</div>' +
                ' <div>' + $("#hdn_PlaceOrderMsg_2").val() + ' ? </div>');
            ConfirmOrderModal();
            $('#ConfirmOrderWarningModal').modal('show');
        } else {
            PlaceOrder();
        }
    }
    else if (offerstonelist != '') {
        //ConfirmOrderModal();
        //toastr.warning($("#hdn_Select_Avail_Stone_for_place_order").val() + '!');
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        $.ajax({
            url: "/Order/GetAssistPersonDetail",
            type: "POST",
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {
                    $('#PlaceOrderMsg').html('<div>' + $("#hdn_Select_Avail_Stone_for_place_order").val() + '!<br>' + $("#hdn_PleaseContact").val() + data.Message + '</div>');
                    ConfirmOrderModal();
                    $('#ConfirmOrderWarningModal').modal('show');
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
    else {
        ConfirmOrderModal();
        toastr.warning($("#hdn_No_Stone_Selected_for_place_order").val() + '!');;
    }
}

function PlaceOrder() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    $.ajax({
        url: "/SearchStock/PlaceOrder",
        type: "POST",
        data: { stoneNo: $('#hdnAvailableStone').val(), Comments: $('#Comments').val() },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            } else {
                $('#ConfirmOrderModal').modal('hide');
                $('#ConfirmOrderWarningModal').modal('hide');
                //if (data.Message == 'Your Transaction Done Successfully') {
                //    $('#lblcheckingavailability').html($("#hdn_order_placed_success").val());
                //} else {
                //    $('#lblcheckingavailability').html($("#hdn_Transaction_Done_Success").val());
                //}

                data.Message = data.Message.replace('Your Transaction Done Successfully', $("#hdn_Your_Transaction_Done_Successfully").val());
                data.Message = data.Message.replace('This Stone(s) are subject to avaibility', $("#hdn_This_Stones_are_subject_to_availbility").val());
                data.Message = data.Message.replace('Please contact your sales person', $("#hdn_Please_contact_your_sales_person").val());

                $('#lblcheckingavailability').html(data.Message);

                $('#order-confirm-modal').modal('show');
                GetRevisedPriceData();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
/*--------------------------------------------------------PLACE ORDER END----------------------------------------------------*/
/*--------------------------------------------------------DOWNLOAD ALL START----------------------------------------------------*/
function OpenDownloadPopup(downloadType) {
    $('#hdnDownloadType').val(downloadType);
    $('#ExcelModalAll').modal('show');
}
function DownloadAll() {
    $('#ExcelModalAll').modal('hide');
    if ($('#hdnDownloadType').val() == "Excel") {
        DownloadExcel();
    }
    else if ($('#hdnDownloadType').val() == "Pdf") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Image") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Video") {
        DownloadMedia();
    }
    else if ($('#hdnDownloadType').val() == "Certificate") {
        DownloadMedia();
    }
}
function DownloadExcel() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {
            "Reviseflg": "1"
        };
        sobj.FormName = 'Revised Price';
        sobj.ActivityType = 'Excel Export';
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/StockExcelDownloadBySearchObject",
            type: "POST",
            data: sobj,
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                location.href = data;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    } else {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (count > 0) {
            $.ajax({
                url: "/Common/StockExcelDownloadByStoneId",
                type: "POST",
                data: { StoneID: stoneno, FormName: 'Revised Price', ActivityType: 'Excel Export' },
                success: function (data, textStatus, jqXHR) {
                    location.href = data;
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        } else {
            toastr.warning($("#hdn_No_stone_selected_for_download_as_a_excel").val() + '!');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }

}
function DownloadMedia() {
    if ($('#customRadio3').prop('checked')) {
        var sobj = {
            ReviseStockFlag: '1'
        };
        sobj.FormName = 'Revised Price';
        sobj.ActivityType = $('#hdnDownloadType').val() + ' Download';
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();
        $.ajax({
            url: "/Common/StockMediaDownloadBySearchObject",
            type: "POST",
            data: { obj: sobj, MediaType: $('#hdnDownloadType').val()},
            success: function (data, textStatus, jqXHR) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
                if (data.search('.zip') == -1 && data.search('.pdf') == -1) {
                    if (data.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    data = data.replace('Error to download video, video is not MP4', $("#hdn_Error_to_download_video_video_is_not_MP4").val());
                    data = data.replace('Image is not available in this stone', $("#hdn_Image_is_not_available_in_this_stone").val());
                    toastr.error(data);
                } else {
                    location.href = data;
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
    else
    {
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'stone_ref_no').join(",");
        var count = gridOptions.api.getSelectedRows().length;
        if (AllD == false) {
            $('.loading-overlay-image-container').show();
            $('.loading-overlay').show();
        }
        if (count > 0) {

            $.ajax({
                url: "/Common/StockMediaDownloadByStoneId",
                type: "POST",
                data: { StoneID: stoneno, MediaType: $('#hdnDownloadType').val(), FormName: 'Revised Price', ActivityType: $('#hdnDownloadType').val() + ' Download' },
                success: function (data, textStatus, jqXHR) {
                    if (data.search('.zip') == -1 && data.search('.pdf') == -1) {
                        toastr.error(data);
                    } else {
                        location.href = data;
                    }
                    if (AllD == false) {
                        $('.loading-overlay-image-container').hide();
                        $('.loading-overlay').hide();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('.loading-overlay-image-container').hide();
                    $('.loading-overlay').hide();
                }
            });
        } else {
            toastr.warning($("#hdn_No_stone_selected_for_download_as_a").val() + ' ' + $('#hdnDownloadType').val() + ' !');
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    }    
}
/*--------------------------------------------------------DOWNLOAD ALL END----------------------------------------------------*/
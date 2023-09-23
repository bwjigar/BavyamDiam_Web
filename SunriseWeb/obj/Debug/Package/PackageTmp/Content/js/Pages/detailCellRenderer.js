function DetailCellRenderer() { }

DetailCellRenderer.prototype.init = function (params) {

  //  var firstRecord = params.data.callRecords[0];

    this.eGui = document.createElement('div');
    this.eGui.innerHTML =
        '<form>' +
        '  <div>' +
        '  <p>' +
        '    <label>' +
        '      Call Id:<br>' +
        '    <input type="text" value="">' +
        '    </label>' +
        '  </p>' +
        '  <p>' +
        '    <label>' +
        '      Number:<br>' +
        '    <input type="text" value="">' +
        '    </label>' +
        '  </p>' +
        '  <p>' +
        '    <label>' +
        '      Direction:<br>' +
        '    <input type="text" value="">' +
        '    </label>' +
        '  </p>' +
        '</form>' +
        '</div>';
};

DetailCellRenderer.prototype.getGui = function () {
    return this.eGui;
};
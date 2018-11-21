function TableManager(tableID) {
    var table = $("#" + tableID);
    table.html("");

    var addRowDivTextField = $("<input type=\"text\" />")[0];
    var addRowDivAddButton = $("<input class='btn' type=\"button\" value=\"Add\" />");

    table.append($("<tr>")
        .append($("<th>")
            .append(addRowDivTextField))
        .append($("<th>")
            .append(addRowDivAddButton)
        )
    );

    addRowDivAddButton.click(function () {
        addRow(addRowDivTextField.value);
    });

    this.getData = function() {
        var data = [];

        var rows = $(table).find("tr");
        for(var i = 1;i < rows.length;i++) {
            var rowData = $($(rows[i]).children()[0]).html();
            data.push(rowData);
        }

        return data;
    };

    var _onChangeCallback = function () { }
    this.onchange = function (callback) {
        _onChangeCallback = callback;
    };

    function addRow(value) {
        table.append(getRowHTML(value));
        _onChangeCallback();
    }

    function didRemoveRow() {
        _onChangeCallback();
    }

    function getRowHTML(value) {
        return $("<tr>")
            .append($("<td>")
                .append(value))
            .append($("<td>")
                .append($("<input class='btn' type='button' value='Delete' />")
                    .click(function () {
                        $(this).parent().parent().remove();
                        didRemoveRow();
                    })));
    }
};
function didClickRaw() {
    $("#rawDataRow").css("display", "block");
    $("#parsedDataRow").css("display", "none");
    $("#parsedDataOptions").css("display", "none");

    $("#rawButton").addClass("active");
    $("#parsedButton").removeClass("active");
}

function didClickParsed() {
    $("#rawDataRow").css("display", "none");
    $("#parsedDataRow").css("display", "block");
    $("#parsedDataOptions").css("display", "block");

    $("#rawButton").removeClass("active");
    $("#parsedButton").addClass("active");
    parse();
}

function parse() {
    var data = $.csv.toArrays($("#rawData").val());
    var table = $("#parsedTable");
    
    table.html("");
    
    var settings = getSettings();
    
    var i = 0;
    
    if(settings.firstRowHeader) {
        var row = getHeader(data[i], settings.lineNumbers);
        table.append(row);
        i++;
    }
    
    var lineNumber = 1;
    var maxLines = data.length;
    if(settings.show1000 && data.length > 1000) {
        maxLines = 1000;
    }
    
    for(;i < maxLines;i++) {
        var row = getRow(data[i], settings.lineNumbers, lineNumber);
        table.append(row);
        lineNumber++;
    }
}

function getHeader(rowData, includeLineNumbers) {
    var row = "<tr>";
    
    if(includeLineNumbers) {
        row += "<th class=\"lineNumber\"></th>";
    }
    
    for(var i = 0;i < rowData.length;i++) {
        row += "<th>";
        row += rowData[i];
        row += "</th>";
    }
    row += "</tr>";
    
    return row;
}

function getRow(rowData, includeLineNumbers, lineNumber) {
    var row = "<tr>";
    
    if(includeLineNumbers) {
        row += "<td class=\"lineNumber\">" + lineNumber + "</td>";
    }
    
    for(var i = 0;i < rowData.length;i++) {
        row += "<td>";
        row += rowData[i];
        row += "</td>";
    }
    row += "</tr>";
    
    return row;
}

function onSettingsChanged() {
    $("input").css("cursor", "wait");
    $("label").css("cursor", "wait");
    
    setTimeout(function() {
        parse();
        $("input").css("cursor", "pointer");
        $("label").css("cursor", "default");
    }, 100);
}

function getSettings() {
    return {
        lineNumbers: document.getElementById("lineNumbers").checked == true,
        firstRowHeader: document.getElementById("firstRowHeader").checked == true,
        show1000: document.getElementById("show1000").checked == true,
    }
}


function handleFileSelect(evt) {
    evt.stopPropagation();
    evt.preventDefault();

    $("#rawData").css("cursor", "wait");

    var files = evt.dataTransfer.files; // FileList object.
    var reader = new FileReader();
    reader.onload = function(event) {
        document.getElementById('rawData').value = event.target.result;
        $("#rawData").css("cursor", "text");
        didClickParsed();
    }

    setTimeout(function() {
        reader.readAsText(files[0], "UTF-8");
    }, 100);
}

function handleDragOver(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    evt.dataTransfer.dropEffect = 'copy'; // Explicitly show this is a copy.
}

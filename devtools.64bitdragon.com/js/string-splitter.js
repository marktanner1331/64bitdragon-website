function didClickAutodetect() {
    var shouldAutodetect = $("#autodetectDelimeter").is(":checked");

    if (shouldAutodetect) {
        $("#delimeterContainer").hide();
    } else {
        $("#delimeterContainer").show();
    }
}

function didClickRaw() {
    $("#toolCell").css("display", "");
    $("#parsedDataRow").css("display", "none");

    $("#rawButton").addClass("active");
    $("#parsedButton").removeClass("active");

    resize();
}

function didClickParsed() {
    $("#toolCell").css("display", "none");
    $("#parsedDataRow").css("display", "");

    $("#rawButton").removeClass("active");
    $("#parsedButton").addClass("active");

    parse();
    resize();
}

function parse() {
    var rawData = $("#textArea").val();
    var shouldDetectDelimeter = $("#autodetectDelimeter").is(":checked");;

    var delimeter;
    if (shouldDetectDelimeter) {
        delimeter = detectDelimeter(rawData);
    } else {
        delimeter = $("#delimeter").val();
    }
    
    var split = rawData.split(delimeter);

    var output = "Splitting with delimeter: " + delimeter;
    output += "\r\n\r\n";
    output += split.join("\r\n");

    $("#parsedData").val(output);
}

function detectDelimeter(text) {
    var delimeters = [];
    
    for (var i = 0; i < text.length; i++) {
        var code = text.charCodeAt(i);
        if ((code > 47 && code < 58) == false && // numeric (0-9)
            (code > 64 && code < 91) == false && // upper alpha (A-Z)
            (code > 96 && code < 123) == false) { // lower alpha (a-z)
            var char = String.fromCharCode(code);

            if (delimeters.indexOf(char) == -1) {
                delimeters.push(char);
            }
        }
    }

    switch (delimeters.length) {
        case 0:
            return null;
        case 1:
            return delimeters[0];
        case 2:
            if (deiimeters.indexOf("&") != -1 && delimeters.indexOf("=") != -1) {
                return "&";
            } else {
                return getBestDelimeter(text, delimeters);
            }
        default:
            return getBestDelimeter(text, delimeters);
    }
}

function getBestDelimeter(text, delimeters) {
    var bestDelimeter = delimeters[0];
    var bestDelimeterVariance = getVariance(getSegmentLengths(text, delimeters[0]));
    
    for (var i = 1; i < delimeters.length; i++) {
        var variance = getVariance(getSegmentLengths(text, delimeters[i]));
        
        if (variance < bestDelimeterVariance) {
            bestDelimeter = delimeters[i];
            bestDelimeterVariance = variance;
        }
    }

    return bestDelimeter;
}

function getVariance(array) {
    var mean = getMean(array);
    var gaps = [];

    for (var i = 0; i < array.length; i++) {
        gaps.push(Math.abs(array[i] - mean));
    }
    
    return getMean(gaps);
}

//splits the text by the delimeter into substrings
//and returns an array of lengths (one for each substring)
function getSegmentLengths(text, delimeter) {
    var lengths = [];

    var mostRecentDelimeterPos = 0;
    for (var i = 0; i < text.length; i++) {
        if (text.charAt(i) == delimeter) {
            lengths.push(i - mostRecentDelimeterPos);
            mostRecentDelimeterPos = i + 1;
        }
    }

    lengths.push(text.length - mostRecentDelimeterPos);
    
    return lengths;
}

function getMean(array) {
    var sum = 0;

    for (var i = 0; i < array.length; i++) {
        sum += array[i];
    }

    return sum / array.length;
}
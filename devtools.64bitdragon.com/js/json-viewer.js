$(document).ready(function () {
    document.body.addEventListener("click", onBodyClick, false);
});

function onBodyClick(e) {
    if (event.target.className == 'collapsed') {
        var parent = $(event.target).parent();
        parent.find(".collapsed").css("display", "none");
        parent.find(".uncollapsed").css("display", "inline-block");

        parent.nextAll("ul").css("display", "block");
    } else if (event.target.className == 'uncollapsed') {
        var parent = $(event.target).parent();
        parent.find(".collapsed").css("display", "inline-block");
        parent.find(".uncollapsed").css("display", "none");

        parent.nextAll("ul").css("display", "none");
    }
}

function didClickRaw() {
    $("#toolCell").css("display", "");
    $("#parsedDataRow").css("display", "none");

    $("#rawButton").addClass("active");
    $("#parsedButton").removeClass("active");
}

function didClickParsed() {
    $("#toolCell").css("display", "none");
    $("#parsedDataRow").css("display", "");

    $("#rawButton").removeClass("active");
    $("#parsedButton").addClass("active");

    parse();
    uncollapseAll();
    resize();
}

function uncollapseAll() {
    $("#parsedData").find(".collapsed").css("display", "none");
    $("#parsedData").find(".uncollapsed").css("display", "inline-block");

    $("#parsedData").find("ul").css("display", "block");
}

function parse() {
    var rawData = $("#textArea").val();

    var jsonObj = JSON.parse(rawData);
    var html = jsonToHTML(jsonObj);

    $("#parsedData").html(html);
}

function jsonToHTML(jsonObj) {
    if (jsonObj == null) {
        return "<span class=\"nullObject\">null</span>";
    }

    if ((typeof jsonObj) == "number") {
        return "<span class=\"numberObject\">" + jsonObj + "</span>";
    }

    if ((typeof jsonObj) == "boolean") {
        return "<span class=\"booleanObject\">" + jsonObj + "</span>";
    }

    if ((typeof jsonObj) == "string") {
        if (/^https?:\/\/[^\s]+$/.test(jsonObj)) {
            return "<a href=\"" + encode(jsonObj) + "\">" + encode(jsonObj) + "</a>";
        } else {
            return "<span class=\"stringObject\">\"" + encode(jsonObj) + "\"</span>";
        }
    }

    if (jsonObj.constructor == Array) {
        if (jsonObj.length == 0) {
            return "[]";
        }
        
        var html = "[<ul>";

        for (var i = 0; i < jsonObj.length; i++) {
            html += "<li>";

            if (valueIsArrayOrObject(jsonObj[i])) {
                html += "<span><span class=\"uncollapsed\">-</span><span class=\"collapsed\">+</span></span>";
            }

            html += jsonToHTML(jsonObj[i]);

            if (i < jsonObj.length - 1) {
                html += ",";
            }

            html += "</li>";
        }
        
        html += "</ul>]";

        return html;
    }

    if ((typeof jsonObj) == "object") {
        var keys = Object.keys(jsonObj);

        if (keys.length == 0) {
            return "{}";
        }

        var html = "{<ul>";

        for (var i = 0; i < keys.length; i++) {
            html += "<li>";

            if (valueIsArrayOrObject(jsonObj[keys[i]])) {
                html += "<span><span class=\"uncollapsed\">-</span><span class=\"collapsed\">+</span></span>";
            }

            html += "<span class=\"key\">" + keys[i] + ":</span>";
            html += jsonToHTML(jsonObj[keys[i]]);

            if (i < keys.length - 1) {
                html += ",";
            }

            html += "</li>";
        }

        html += "</ul>}";

        return html;
    }

    return "error";
}

function valueIsArrayOrObject(value) {
    return value != null && (value.contructor == Array || typeof value == "object");
}

function encode(value) {
    return value.replace(/&/g, "&amp;")
                .replace(/"/g, "&quot;")
                .replace(/</g, "&lt;")
                .replace(/>/g, "&gt;");
}
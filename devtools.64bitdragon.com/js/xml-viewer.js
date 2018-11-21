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

    resize();
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

    var xmlObj = (new window.DOMParser()).parseFromString(rawData, "text/xml");

    var html = xmlToHTML(xmlObj);

    $("#parsedData").html(html);
}

function openingNodeToHTML(xmlNode) {
    var html = "<span class=\"bracket\">&lt;</span>";

    html += "<span class=\"nodeName\">" + xmlNode.nodeName + "</span>";

    if (xmlNode.attributes != undefined) {
        for (var i = 0; i < xmlNode.attributes.length; i++) {
            html += " " + "<span class=\"attributeName\">" + encode(xmlNode.attributes[i].nodeName) + "</span>";
            html += "<span class=\"equals\">=</span>";
            html += "<span class=\"attributeValue\">\"" + encode(xmlNode.attributes[i].nodeValue) + "\"</span>";
        }
    }

    html += "<span class=\"bracket\">&gt;</span>";

    return html;
}

function closingNodeToHTML(xmlNode) {
    var html =  "<span class=\"bracket\">&lt;/</span>";
    html += "<span class=\"nodeName\">" + encode(xmlNode.nodeName) + "</span>";
    html += "<span class=\"bracket\">&gt;</span>";
    
    return html;
}

function valueToHTML(value) {
    if (/^https?:\/\/[^\s]+$/.test(value)) {
            return "<a href=\"" + encode(value) + "\">" + encode(value) + "</a>";
        } else {
            return "<span class=\"innerValue\">" + encode(value) + "</span>";
        }    
}

function nodeToHTML(xmlNode) {
    if (xmlNode.childElementCount == 0) {
        var html = "";

        html += openingNodeToHTML(xmlNode);
        html += valueToHTML(xmlNode.innerHTML);
        html += closingNodeToHTML(xmlNode);

        return html;
    } else {
        var html = "";
        html += "<span><span class=\"uncollapsed\">-</span><span class=\"collapsed\">+</span></span>";
        html += openingNodeToHTML(xmlNode);

        html += "<ul>";
        for (var i = 0; i < xmlNode.childNodes.length; i++) {
            if(xmlNode.childNodes[i].nodeType == 3) {
                continue;
            }
            
            html += "<li>";

            html += nodeToHTML(xmlNode.childNodes[i]);

            html += "</li>"
        }

        html += "</ul>";

        html += closingNodeToHTML(xmlNode);

        return html;
    }
}

function xmlToHTML(xmlObj) {
    var html = "";

    html += "<ul>";
    for (var i = 0; i < xmlObj.childNodes.length; i++) {
        html += "<li>";
        html += nodeToHTML(xmlObj.childNodes[i]);
        html += "</li>"
    }
    html += "</ul>";

    return html;
}

function encode(value) {
    return value.replace(/&/g, "&amp;")
        .replace(/"/g, "&quot;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");
}

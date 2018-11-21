function didClickRequest() {
    $("#urlRow").css("display", "");
    $("#postDataRow").css("display", "");
    $("#responseRow").css("display", "none");

    $("#requestButton").addClass("active");
    $("#responseButton").removeClass("active");
}

function didClickResponse() {
    $("#urlRow").css("display", "none");
    $("#postDataRow").css("display", "none");
    $("#responseRow").css("display", "");

    $("#requestButton").removeClass("active");
    $("#responseButton").addClass("active");
}

function didClickSubmit() {
    $("#responseFrame").attr("src", "about:blank");

    setTimeout(function () {
        var url = $("#urlBox").val();
        if (url == "") {
            url = "http://httpbin.org/post";
        }

        var form = $("#hiddenForm");
        form.attr("action", url);
        form.html("");

        var postData = $("#postData").val();

        var pairs = [];
        var reg = /(?<=^|&)([^=]+)=([^&]*)(?=$|&)/g;
        var result;
        while ((result = reg.exec(postData)) !== null) {
            form.append("<input type=\"text\" name=\"" + result[1] + "\" value=\"" + result[2] + "\" />");
        }

        document.hiddenForm.submit();
        didClickResponse();
    }, 100);
}
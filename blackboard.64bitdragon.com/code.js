CanvasManager = {
    //objects containing:
    //startX
    //startY
    //endX
    //endY
    _paths: [],

    _currentX: 0,
    _currentY: 0,

    initializeCanvas: function () {
        var canvasDiv = $("#canvasDiv");
        var canvas = this._getCanvasHTML();

        canvas.mousedown(CanvasManager._mouseDown);

        canvasDiv.append(canvas);

        this._refreshCanvasSize();

        $(window).resize(function () {
            CanvasManager._refreshCanvasSize();
        });
    },

    _refreshCanvasSize() {
        var canvas = $('#canvas');
        var canvasDiv = $('#canvasDiv');
        canvas[0].width = canvasDiv.width() - 25;

        canvas[0].height = 0;
        canvasDiv.css("height", "100%");

        var height = canvasDiv.height();
        canvasDiv.css("height", height + "px");

        canvas[0].height = Math.max(canvas[0].height, 5000);

        this._redraw();
    },

    _mouseDown: function (e) {
        CanvasManager._currentX = e.pageX - this.offsetLeft;
        CanvasManager._currentY = e.pageY - this.offsetTop + $("#canvasDiv").scrollTop();
        
        $('#canvas').mousemove(CanvasManager._mouseMove);
        $('#canvas').mouseup(CanvasManager._mouseUp);
    },

    _mouseMove: function (e) {
        var x = e.pageX - this.offsetLeft;
        var y = e.pageY - this.offsetTop + $("#canvasDiv").scrollTop();

        if (CanvasManager._startX == x && CanvasManager._startY == y) {
            return;
        }

        CanvasManager._paths.push({
            startX: CanvasManager._currentX,
            startY: CanvasManager._currentY,
            endX: x,
            endY: y
        });
        
        CanvasManager._currentX = x;
        CanvasManager._currentY = y;
        CanvasManager._redraw();
    },

    _mouseUp: function (e) {
        $('#canvas').unbind('mousemove');
        $('#canvas').unbind('mouseup');
        CanvasManager._startX = 0;
        CanvasManager._startY = 0;
    },

    _redraw: function () {
        var canvas = $("#canvas")[0];
        var context = canvas.getContext("2d");

        context.clearRect(0, 0, canvas.width, canvas.height);

        context.strokeStyle = "#df4b26";
        context.lineJoin = "round";
        context.lineWidth = 5;
        
        for (var i = 0; i < this._paths.length; i++) {
            context.beginPath();

            context.moveTo(this._paths[i].startX, this._paths[i].startY);
            context.lineTo(this._paths[i].endX, this._paths[i].endY);

            context.closePath();
            context.stroke();
        }
    },

    _getCanvasHTML() {
        return $("<canvas>").attr("width", 100)
                            .attr("height", 100)
                            .attr("id", "canvas");
    }
}
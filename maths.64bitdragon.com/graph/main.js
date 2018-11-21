function didClickEquation() {
    $("#equationRow").css("display", "table-row");
    $("#graphRow").css("display", "none");

    $("#equationButton").addClass("active");
    $("#graphButton").removeClass("active");

    $("#canvasWrapper").resize(function () { });
}

function didClickGraph() {
    $("#equationRow").css("display", "none");
    $("#graphRow").css("display", "table-row");

    $("#equationButton").removeClass("active");
    $("#graphButton").addClass("active");

    var equation = EquationSettingsManager.getEquation();
    var xBounds = EquationSettingsManager.getXBounds();

    var points = EquationFunctions.interpolateEquation(equation, xBounds.minX, xBounds.maxX, 100);
    GraphManager.draw("graphCanvas", points);

    $("#canvasWrapper").resize(function () {
        GraphManager.refreshSize();
    });
}

EquationSettingsManager = {
    getEquation() {
        return $("#equation").val();
    },

    getXBounds() {
        return { minX: 0, maxX: 50 };
    }
}

GraphManager = {
    settings: {},
    _canvasID: "",
    _dataPoints: [],
    _minX: 0,
    _minY: 0,
    _maxX: 0,
    _maxY: 0,

    draw(canvasID, dataPoints) {
        this._canvasID = canvasID;
        this._dataPoints = dataPoints;
        this.settings = this._getSettings();

        this._minX = DataFunctions.getMinimumX(dataPoints);
        this._minY = DataFunctions.getMinimumY(dataPoints);
        this._maxX = DataFunctions.getMaximumX(dataPoints);
        this._maxY = DataFunctions.getMaximumY(dataPoints);

        //make sure that the minimum x and y are negative numbers, so that 
        //the axis show
        var width = this._maxX - this._minX;
        this._minX = Math.min(this._minX, -width / 10);

        var height = this._maxY - this._minY;
        this._minY = Math.min(this._minY, -height / 10);

        this.refreshSize();
    },

    refreshSize() {
        var canvas = $("#" + this._canvasID);
        canvas.attr("width", canvas.width());
        canvas.attr("height", canvas.height());

        var context = document.getElementById(this._canvasID).getContext("2d");

        GraphManager.clearGraph(context);
        GraphManager.drawAxis(context, this._minX, this._minY, this._maxX, this._maxY);
        GraphManager.drawPoints(context, this._minX, this._minY, this._maxX, this._maxY, this._dataPoints);
    },

    drawPoints(context, minX, minY, maxX, maxY, dataPoints) {
        context.beginPath();

        var start = dataPoints[0];
        start = DataFunctions.normalizePoint(start, minX, minY, maxX, maxY);
        
        start.x *= context.canvas.width;
        start.y *= context.canvas.height;
        start.y = context.canvas.height - start.y;
        
        context.moveTo(start.x, start.y);

        for (var i = 1; i < dataPoints.length; i++) {
            var point = dataPoints[i];
            point = DataFunctions.normalizePoint(point, minX, minY, maxX, maxY);

            point.x *= context.canvas.width;
            point.y *= context.canvas.height;
            point.y = context.canvas.height - point.y;

            context.lineTo(point.x, point.y);
        }

        context.lineWidth = 2;
        context.strokeStyle = "#0000ff";
        context.stroke();
    },

    //we want to split the line up into segments for the axis labels
    //this function will return how large the gaps should be between the labels
    //preferring as least rounding as possible
    getDelta(width) {
        var divisors = [8, 9, 10, 11, 12];

        var bestDelta = 0;
        var bestDeltaError = Number.MAX_VALUE;

        for (var i = 0; i < divisors.length; i++) {
            var perfectDelta = width / divisors[i];
            
            var normalizer = Math.pow(10, Math.ceil(Math.log10(perfectDelta)));

            var normalizedDelta = perfectDelta / normalizer;
            
            var roundedDelta = GraphManager.getClosestNumber(normalizedDelta, [0.1, 0.25, 0.5, 1]);

            var delta = roundedDelta * normalizer;

            var error = Math.abs(perfectDelta - delta);

            if (error < bestDeltaError) {
                bestDeltaError = error;
                bestDelta = delta;
            }
        }

        return bestDelta;
    },

    getClosestNumber(needle, collection) {
        var distance = Math.abs(collection[0] - needle);
        var idx = 0;
        for (var c = 1; c < collection.length; c++) {
            var cdistance = Math.abs(collection[c] - needle);
            if(cdistance < distance){
                idx = c;
                distance = cdistance;
            }
        }

        return collection[idx];
    },

    //TODO, split this method up into smaller methods to make it more readable
    drawAxis(context, minX, minY, maxX, maxY) {
        var origin = {
            x: 0,
            y: 0
        };

        //0,0 might become 0.5,0.5 (if e.g., the minX == maxX && minY == maxY)
        origin = DataFunctions.normalizePoint(origin, minX, minY, maxX, maxY);

        //now we convert to canvas space
        origin.x *= context.canvas.width;
        origin.y *= context.canvas.height;
        origin.y = context.canvas.height - origin.y;

        context.lineWidth = 2;
        context.strokeStyle = "#000000";

        //draw the x axis
        context.beginPath();
        context.moveTo(0, origin.y);
        context.lineTo(context.canvas.width, origin.y);
        context.stroke();

        //drawth y axis
        context.beginPath();
        context.moveTo(origin.x, 0);
        context.lineTo(origin.x, context.canvas.height);
        context.stroke();

        //delta stores the size of the gap between each axis label
        //delta is in equation space
        var delta = this.getDelta(maxX - minX);
        for (var i = 0; i < maxX; i += delta) {
            context.lineWidth = 1;
            context.strokeStyle = "#777777";

            var p = {
                x: i,
                y: 0
            }

            //convert p from equation space into screen space
            p = DataFunctions.normalizePoint(p, minX, minY, maxX, maxY);
            p.x *= context.canvas.width;

            context.beginPath();
            context.moveTo(p.x, 0);
            context.lineTo(p.x, context.canvas.height);
            context.stroke();

            context.fillStyle = "#000000";
            context.font = '32px serif';
            context.textAlign = "start";
            context.fillText(i.toString(), p.x + 2, origin.y + 32);
        }

        delta = GraphManager.getDelta(maxY - minY);

        for (var i = 0; i < maxY; i += delta) {
            context.lineWidth = 1;
            context.strokeStyle = "#777777";

            var p = {
                x: 0,
                y: i
            }
            p = DataFunctions.normalizePoint(p, minX, minY, maxX, maxY);
            p.y *= context.canvas.height;
            p.y = context.canvas.height - p.y;

            context.beginPath();
            context.moveTo(0, p.y);
            context.lineTo(context.canvas.width, p.y);
            context.stroke();

            context.fillStyle = "#000000";
            context.font = '32px serif';
            context.textAlign = "end";
            context.fillText(i.toString(), origin.x - 2, p.y - 2);
        }
    },

    clearGraph(context) {
        context.clearRect(0, 0, context.canvas.width, context.canvas.height);
    },

    _getSettings() {

    },
}

EquationFunctions = {
    //returns an array of objects, each object has an x and y
    interpolateEquation(equation, minX, maxX, numPoints) {
        var points = [];

        var delta = (maxX - minX) / numPoints;

        for (var i = minX; i <= maxX; i += delta) {
            var expression = equation.replace(/x/g, i);
            var y = eval(expression);

            points.push({
                x: i,
                y: y
            });
        }

        return points;
    },
}

DataFunctions = {
    //converts a point to be between 0 and 1
    normalizePoint(point, minX, minY, maxX, maxY) {
        return {
            x: (point.x - minX) / (maxX - minX),
            y: (point.y - minY) / (maxY - minY)
        }
    },

    normalizePointWithBounds(point, bounds) {
        return {
            x: (point.x - bounds.minX) / (bounds.maxX - bounds.minX),
            y: (point.y - bounds.minY) / (bounds.maxY - bounds.minY)
        }
    },

    //returns an object containing minX, maxX, minY and maxY
    //it encloses the given data points
    getBounds(dataPoints) {
        var bounds = {
            minX: Number.MAX_VALUE,
            maxX: 1 - Number.MAX_VALUE,
            minY: Number.MAX_VALUE,
            maxY: 1 - Number.MAX_VALUE
        }

        for (var i = 0; i < dataPoints.length; i++) {
            var point = dataPoints[i];

            bounds.minX = Math.min(bounds.minX, point.x);
            bounds.maxX = Math.max(bounds.maxX, point.x);
            bounds.minY = Math.min(bounds.minY, point.y);
            bounds.maxY = Math.max(bounds.maxY, point.y);
        }

        return bounds;
    },

    getMinimumX(dataPoints) {
        var xValues = ArrayFunctions.selectProperty(dataPoints, "x");

        return Math.min.apply(null, xValues);
    },

    getMinimumY(dataPoints) {
        var yValues = ArrayFunctions.selectProperty(dataPoints, "y");

        return Math.min.apply(null, yValues);
    },

    getMaximumX(dataPoints) {
        var xValues = ArrayFunctions.selectProperty(dataPoints, "x");

        return Math.max.apply(null, xValues);
    },

    getMaximumY(dataPoints) {
        var yValues = ArrayFunctions.selectProperty(dataPoints, "y");

        return Math.max.apply(null, yValues);
    },
}

ArrayFunctions = {
    selectProperty: function (array, propertyName) {
        var ret = [];
        for (var i = 0; i < array.length; i++) {
            ret.push(array[i][propertyName]);
        }

        return ret;
    },

    selectWithSelector: function (array, selector) {
        var ret = [];
        for (var i = 0; i < array.length; i++) {
            ret.push(selector(array[i]));
        }

        return ret;
    },
}

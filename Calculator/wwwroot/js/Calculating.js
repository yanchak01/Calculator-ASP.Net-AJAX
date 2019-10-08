function addToExpression(item) {
    var form = document.forms["calculatorForm"];
    var b = form.elements["Text"].value;
    var temp = new Array(b);
    var numbers = temp.join("");

    if (numbers.length == 0 && !isDigit(item)) {
        form.elements["Text"].value += "";
        return;
    }
    if (isDigit(item) && numbers[numbers.length - 1] == "0") {
        if (isDigit(numbers[numbers.length - 2]) || numbers[numbers.length - 2] == ".") {
            form.elements["Text"].value += item;
            return;
        }
        else {
            form.elements["Text"].value += "";
            return;
        }
    }
    if (item == numbers[numbers.length - 1] && !isDigit(item)) {
        form.elements["Text"].value += "";
    }
    else {
        form.elements["Text"].value += item;
    }
};

function isDigit(item) {
    if (item >= 0 && item <= 9) {
        return true;
    }
    else
        return false;
}

function GetAllResults() {

    $.ajax({
        url: '/Home/GetAll',
        type: 'GET',
        contentType: "aplication/json",
        success: function (results) {
            var rows = "";
            $.each(results, function (index, result) {
                rows += row(result.text);
            })

            $("table tbody").append(rows);
        }
    });
}

var row = function (ex) {
    return "<tr><td>" + ex + "</td></tr>";
};

function CreateExample(formula) {
    $.ajax({
        url: '/Home/Insert',
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            text: formula
        }),
        success: function (result) {
            clearForm();
            resulrWriter(result.result);

            +$("table tbody").append(row(result.text));
        }
    });
}

function clearForm() {
    var form = document.forms["calculatorForm"];
    form.reset();
}

function resulrWriter(text) {
    var form = document.forms["calculatorForm"];
    form.elements["Text"].value = text;
}

$("form").submit(function (e) {
    e.preventDefault();
    $('#errors').empty();
    $('#errors').hide();
    var id = this.elements["id"].value;
    var formula = this.elements["Text"].value;
    if (id == 0)
        CreateExample(formula);
    else {
        id == null;
        CreateExample(formula);
    }
});
GetAllResults();
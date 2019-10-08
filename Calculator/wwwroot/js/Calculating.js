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
                rows += row(result);
            })

            $("table tbody").append(rows);
        }
    });
}

var row = function (result) {
    return "<tr><td>" + result.text + "</td></tr>";
};

function CreateExample(text) {
    $.ajax({
        url: '/Home/Insert',
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            text: text

        }),
        success: function (result) {
            clearForm();
            resulrWriter(result);

            +$("table tbody").append(row(result));
        }
    });
}

function clearForm() {
    var form = document.forms["calculatorForm"];
    form.reset();
}

function resulrWriter(result) {
    var form = document.forms["calculatorForm"];
    form.elements["Text"].value = result.text;
}

$("form").submit(function (e) {
    e.preventDefault();
    $('#errors').empty();
    $('#errors').hide();
    var id = this.elements["id"].value;
    var formula = document.forms["calculatorForm"].elements["Text"].value;
    if (id == 0)
        CreateExample(formula);
    else {
        id == null;
        CreateExample(formula);
    }
});

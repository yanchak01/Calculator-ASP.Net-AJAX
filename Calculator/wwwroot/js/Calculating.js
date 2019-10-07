
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

function GetAllResults(){
    $.ajax({
        url: '/Home/GetAll',
        method: "GET",
        contentType: "aplication/json",
        success: function (results) {
            var rows = "";
            results = document.
            $.each(results, function (index,result) {
                rows += row(result);
            })
            $("table tbody").append(rows);
        }
    });
}

var row = function (result) {
    return "<tr><td>" + result.Text + "</td></tr>";
};

function CreateExample(text) {
    $.ajax({
        url: '/Home/Insert',
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Text: text
        }),
        success: function (example) {
            clearForm();
            resulrWriter(example.Text);
            $("table tbody").append(row(result));    
        }
    });
}

function clearForm() {
    var form = document.forms["calculatorForm"];
    form.reset();
}

function resulrWriter(text) {
    var form = document.forms["calculatorForm"];
    form.elements["Text"].value = text
}

$("form").submit(function (e) {
    e.preventDefault();
    $('#errors').empty();
    $('#errors').hide();
    var id = this.elements["id"].value;
    var Text = this.elements["Text"].value;
    if (id == 0)
        CreateExample(Text);
    else {
        id == null;
        CreateExample(Text);
    }
});
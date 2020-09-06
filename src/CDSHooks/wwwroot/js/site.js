// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function appendGridAddValidationSpan(ctrl, tbCell, uniqueIndex) {
    var parent = $(ctrl).parent()
    var span = document.createElement("span")
    span.classList.add("text-danger", "field-validation-valid")
    span.setAttribute("data-valmsg-for", ctrl.name)
    span.setAttribute("data-valmsg-replace", "true")
    parent.append(span)
}

function fixFormInputRowsNumber(inputNamePrefix) {
    return function (e) {
        let patternContextIndex = new RegExp(`${inputNamePrefix}\\[(\\d+)\\]`);
        var formInputs = $('#form :input')
        var inputs = formInputs.filter(i => formInputs[i].name.startsWith("Context["))
        var dict = {}
        for (var i = 0; i < inputs.length; i++) {
            var index = patternContextIndex.exec(inputs[i].name)[1]
            dict[index] = [...(dict[index] || []), inputs[i]]
        }
        Object.keys(dict).sort().forEach((key, i) => {
            for (var j = 0; j < dict[key].length; j++) {
                dict[key][j].name = dict[key][j].name.replace(new RegExp(`(${inputNamePrefix}\\[)(\\d+)(\\].*)`), `$1${i}$3`)
            }
        })
    }
}
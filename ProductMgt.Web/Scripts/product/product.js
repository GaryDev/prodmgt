$(function () {
    var id = $("#ProductID").val();
    var txtSku = $("#Sku");
    txtSku.attr("readonly", id > 0);
    txtSku.keyup(function () {
        var errors = $(".validation-summary-errors > ul");
        if (errors && errors.length > 0) {
            errors.empty();
        }
    });

});
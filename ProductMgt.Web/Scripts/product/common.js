$(function () {
    $("form").submit(function () {
        $("input:text, input[type=password]").attr("readonly", true);
        $("input[type=submit]").attr("disabled", true);
    });
});
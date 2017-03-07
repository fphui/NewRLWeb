$(function () {
    $.validator.addMethod('date',
    function (value, element) {
        if (this.optional(element)) {
            return true;
        }
        var valid = true;
        try {
            $.datepicker.parseDate('dd/MM/yy', value);
        }
        catch (err) {
            valid = false;
        }
        return valid;
    });
    $(".datetype").datepicker({ dateFormat: 'dd/MM/yy' });
});
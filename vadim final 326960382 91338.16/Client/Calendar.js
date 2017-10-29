// bootstrap calendars creating
$(document).ready(function () {

    $('[name="start"]').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        clearBtn: true,
        orientation: 'top'
    });

    $('[name="finish"]').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        clearBtn: true,
        orientation: 'top'
    });

    $('[name="birthday"]').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        clearBtn: true,
        orientation: 'top'
    });
    $('[name="BirthDate"]').datepicker({
        autoclose: true,
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        clearBtn: true,
        orientation: 'top'
    });
    $('[name="YearManufacture"]').datepicker({
        autoclose: true,
        format: " yyyy-12-12", // Notice the Extra space at the beginning
        viewMode: "years", 
        minViewMode: "years",
        clearBtn: true,
        orientation: 'bottom'
    });
});
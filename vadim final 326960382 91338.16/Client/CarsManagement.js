var ID = 0, dist = 0;
var obj = {};
//car id return
function ReturnCar(id, uid) {
    ID = id;
}
// return car to park & show price
$(".remove").on('click', function (e) {
    $('#msgs').empty();
    $('#msgs1').empty();
    $('#buttom-mes').empty();
    $('#buttom-mes1').empty();
    var whichtr = $(this).closest("tr");
    if (whichtr.find('input.update-box').is(':checked')) {
        obj = {
            'Id': ID,
            'Dist': dist
        };
    }
    else {
        obj = {
            'Id': ID
        };
    }
    $.ajax({
        type: 'POST',
        url: 'http://localhost:54096/staff/worker/free/',
        dataType: 'json',
        data: obj,
        success: function (msg) {
            console.log(msg);
            if (msg.text != null) {
                $('#msgs').html("<div class='alert alert-danger text-center role='alert''><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" + msg.text + "</div>");
                $('#buttom-mes').html("<div class='alert alert-danger text-center'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" + msg.text + "</div>");
                $('.d-update').attr("style", "display: none !important");
            }
            else {
                $('#msgs').html("<div class='alert alert-success text-center'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" + msg.CarModel + " " + msg.ID + "  returned to park!!!</div>");
                $('#buttom-mes').html("<div class='alert alert-success text-center'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" + msg.CarModel + " " + msg.ID + "  returned to park!!!</div>");
                whichtr.remove();

                $.ajax({
                        type: 'POST',
                        url: 'http://localhost:54096/staff/worker/price/',
                        dataType: 'json',
                        data: {'model':msg.CarModel },
                        success: function (resp) {
                            $('#buttom-mes1').html("<div class='alert alert-success text-center'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button> Payment for " + msg.Days +" days is " + msg.Days * resp.daily + "$</div>");
                            $('#msgs1').html("<div class='alert alert-success text-center'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button> Payment for "+msg.Days +" days is " + msg.Days * resp.daily + "$</div>");
                        },
                    });

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#msgs').html("<div class='alert alert-danger'>Detected problem :(( try reload page or call for support.</div>");
        }
    });
});
// input validation
function ValidateD(x, orign) {
    if (isNaN(x) || x < 1 || x > Infinity) {
        return false;
    } else {
        return true;
    }
}
// input validation
function ValidateEndEvn(x, orign) {
    if (isNaN(x) || x < orign || x > Infinity) {
        return false;
    } else {
        return true;
    }
}
 // validation 
function checkValidation(valFunc, val, input) {

    if (valFunc(val, input.data('val')) && val != input.data('val')) {
        input.siblings('.d-update').attr("style", "display: inline-block !important");
        dist = val;
    }
    else {
        input.siblings('.d-update').attr("style", "display: none !important");
        input.val(input.data('val'));
        input.parent().attr("style", "background-color : red");
        var func = function () {
            input.parent().attr("style", "background-color : white");
        }
        setTimeout(func.bind(this), 1000);
    }
}

$(document).ready(function () {
    
    $('[name = "distance"]').on("input", function () {
        checkValidation(ValidateD, this.value, $(this));
    });
    $('[name = "distance"]').one('focusin', function (event) {
        $(this).data('val', $(this).val());

    }).on('focusout', function (e) {
        checkValidation(ValidateEndEvn, this.value, $(this));
    });

});


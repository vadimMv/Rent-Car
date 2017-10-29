$(document).ready(function () {
    var model = "";
    // adding options list 
    for (var i = 0; i < 100; i++) {
        var name = i;
        var sel = document.getElementById("list");
        if (sel != null)
            sel.options[sel.options.length] = new Option(name, i);
    }
     // open price calculate modal view 
    $('.open-AddBookDialog').on('click', function () {
        $('#addBookDialog').modal('show');
        model = $(this).data('id');
        $(".modal-body #model").val(model);
    });
     // finish & start date validation calculate price before booking
    $('[name="finish"]').change(function () {

        var finish = new Date($(this).val());
        var start = new Date($('[name="start"]').val());
        var now = new Date();
        now.setHours(0, 0, 0, 0);
        if ((start.getTime() >= finish.getTime()) || (start < now)) {
            alert("wrong date!");
            $(this).val('');
            $('[name="start"]').val('');
        } else if (isNaN(start.getTime()) || isNaN(finish.getTime())) {
            $(this).val('');
            $('[name="start"]').val('');
            alert("Pick up Date must be selected!!!");
        }
        else {
            var daysRent = (function (finish, start) {
                return Math.round(Math.abs((finish.getTime() - start.getTime()) / (24 * 60 * 60 * 1000)));
            })(finish, start);
            $('.order-r').attr("style", "display: inline-block !important");
            $('.order-d').prop("readonly", true);
            $.ajax({
                type: 'POST',
                url: 'http://localhost:54096/cars/cardata/',
                dataType: 'json',
                data: {
                    'model': $('[name="model"]').val(),
                },
                success: function (msg) {
                    $('#notify_wrapp')
                     .html('<div class="alert alert-info alert-dismissable"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>Pay Attention:)))</strong> Total cost is ' + msg.Cost * daysRent + ' $ ,for ' + daysRent + ' days. Click Ok if you agree.</div>');
                }
            });
        }
    });
    // get information from server about car 
    $('#list').change(function () {
        var d = $(this).val();
        $.ajax({
            type: 'POST',
            url: 'http://localhost:54096/cars/cardata/',
            dataType: 'json',
            data: {
                'model': model,
            },
            success: function (msg) {
                Success(msg, d);
            }
        });

    });
    // get car models from server  & populate optionals list
    $('#Manufacturer').on('change', function () {
        $("#Model option").remove();
        var m = $(this).val();
        if (m && m != '') {
            $.ajax({
                type: 'POST',
                url: 'http://localhost:54096/cars/carmodel/',
                dataType: 'json',
                data: {
                    'm': m,
                },
                success: function (msg) {
                    console.log(msg);
                    var select = $('#Model');
                    $.each(msg, function (i, v) {
                        select.append($('<option>' + v + '</option>'));
                    });
                }
            });
        }
    });
    // clean price view by hidding
    $('#addBookDialog').on('hidden.bs.modal', function () {
        $('#list').val(0);
        $('#privew').text('');
    });
    // change css prop display by hidding
    $('#RentCarDialog').on('hidden.bs.modal', function () {
        $("#img_wrapp").attr("style", "display: inline !important");
        $("#alt_wrap").attr("style", "display: none !important");
    });
    // check exsisting car in branch
    $('.open-OrderDialog').on('click', function () {
        $('#addOrderDialog').modal('show');
        var model = $(this).data('content');
        var CarId = $(this).data('id');
        $.ajax({
            type: 'POST',
            url: 'http://localhost:54096/cars/branches/',
            dataType: 'json',
            data: {
                'model': model,
            },
            success: function (msg) {
                ValidToOrder(msg, CarId, model);

            }
        });
    });
    // rent information view 
    $('.open-carinfo').on('click', function () {
        $('#RentCarDialog').modal('show');
        var Id = $(this).data('id');
        $.ajax({
            type: 'POST',
            url: 'http://localhost:54096/cars/rentalcardata/',
            dataType: 'json',
            data: {
                'Id': Id,
            },
            success: function (msg) {
                if (msg.Image != null) {
                    $("#img_wrapp").html('<img class="info-auto" src="data:image/png;base64,' + _arrayBufferToBase64(msg.Image) + '" />');
                }
                else {
                    $("#img_wrapp").attr("style", "display: none !important");
                    $("#alt_wrap").attr("style", "display: inline !important");
                }
                $("#data_wrapp").html("<span>" + msg.CurrentDistance + "</span><span>" + msg.Branch + "</span><span>" + msg.Model + "</span>");
                $(".list-group").html('<li class="list-group-item list-group-item-success">' + msg.Model + '</li><li class="list-group-item list-group-item-success">Current Distance ' + msg.CurrentDistance + '</li>');
                if (msg.free)
                    $(".list-group").append('<li class="list-group-item list-group-item-success">Now free in  ' + msg.Branch + ' branch!!!</li>');
            }
        });
    });

});

// refresh table last viewed cars from local storage
window.onload = function () {
    if (window.location.pathname == "/Cars/List") {
        TableRefresh();
    }

};
// refresh table last viewed cars from local storage
function TableRefresh() {
    if (localStorage.length == 0) {
        $(".ld").hide();
        $("#emptyTable").show();
    }
    if (localStorage.length >= 20) {
        localStorage.clear();
    }
    if (localStorage.length > 0 && localStorage.length < 20) {
        $("#emptyTable").hide();
        $(".ld").show();
        for (var i = 0, len = localStorage.length; i < len; ++i) {
            var row = $.parseJSON(localStorage.getItem(localStorage.key(i)));
            $('.table-striped tr:last')
            .after('<tr><td>' + row["Manufacturer"] + '</td><td>' + row["Model"] + '</td><td>' + row["Cost"] + '</td><td>' + row["Delay"] + '</td><td>' + row["Geer"] + '</td><td>' + parseJsonDate(row["Year"]) + '</td></tr>');
        }
    }
}
// adding viewed car data to local storage
function Success(data, days) {
    localStorage.setItem(data.Model, JSON.stringify(data));
    $('#privew').text(" ");
    $('#privew').append('<div class="alert alert-info up" role="alert"><p>For ' + days + ' days you total rent price will ' + (days * data.Cost) + ' $</p><p>Delay cost ' + data.Delay + ' $ per day...</p></div>');
    $("#emptyTable").hide();
    $(".ld").show();
    var row = $.parseJSON(localStorage.getItem(data.Model));
    $('.table-striped tr:last')
    .after('<tr><td>' + row["Manufacturer"] + '</td><td>' + row["Model"] + '</td><td>' + row["Cost"] + '</td><td>' + row["Delay"] + '</td><td>' + row["Geer"] + '</td><td>' + parseJsonDate(row["Year"]) + '</td></tr>');

}
// redirection to order page 
function ValidToOrder(msg, id, model) {
    if (msg.length == 0) {
        $(".dropdown").hide();
        $('#area').html("<div class='alert alert-danger'>" + model + ' currently no available in any branch' + "</div>");
    } else {
        if ($("dropdown").is(":visible") == false) {
            $(".dropdown").show();
            $('#area').empty();
        }
        var options = $("#options");
        options.empty();
        $.each(msg, function (i, v) {
            options.append($('<li><a href="#">' + v + '</a></li>'));
        });
        $("ul >li>a").click(function () {
            window.location.href = "../rent/order/?" + "Id=" + id + "&branch=" + $(this).text();
        });
    }
}

function EnableButton() {
    var allFilled = true;
    $('.order-d').each(function () {
        if ($(this).val() == '') {
            allFilled = false;
            return false;
        }
    });
    
}
// converting image ToBase64 string
function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}
// Date parsing
function parseJsonDate(jsonDate) {

    var fullDate = new Date(parseInt(jsonDate.substr(6)));
    var twoDigitMonth = (fullDate.getMonth() + 1) + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;

    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
    var currentDate = twoDigitDate + "." + twoDigitMonth + "." + fullDate.getFullYear();

    return currentDate;
};


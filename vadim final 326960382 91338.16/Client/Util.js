var id;
$(document).ready(function () {

    var eMail, Id;
    var Img = [];
    // tooltip
    $('[data-toggle="tooltip"]').tooltip();
    // user data loading
    $.ajax({
        type: 'POST',
        url: 'http://localhost:54096/user/userdata/',
        dataType: 'json',
        data: {
            'FullName': $('[name="user"]').val(),
        },
        success: function (msg) {
            console.log(msg);
            eMail = msg.Email;
            Id = msg.IdNumber;
            id = msg.id;
            Img = _arrayBufferToBase64(msg.Image);
          
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

    if ($('ul.list-group').outerHeight() >= 91) {
        $('ul.list-group').css('overflow-y', 'auto');
    }
    // load user info popover
    $('[data-toggle="popover"]').hover(function () {
        var curr = $(this);
        if (curr.attr("class") == "navbar-button account") {
            curr.popover({
                html: true,
                trigger: 'manual',
                placement: 'bottom',
                title: 'Account info',
                content: function () {
                    return '<img class="pop" src="data:image/png;base64,' + Img + '" /></br><h4 class="text-center">' + eMail + '</h4><button class="btn btn-md btn-info">Edit</button>';
                }
            }).on('click', function (e) {
                $(this).popover('show');
                e.stopPropagation();
            });

        }
        else {
            curr.popover({
                html: true,
                trigger: 'hover',
                placement: 'bottom',
                content: function () { return '<img class="pop" src="' + $(this).data('img') + '" /></br><p class="bg-success text-center">Click & Get Now!!!!))) </p>'; }
            });
        }

    });
    // image converting
    function _arrayBufferToBase64(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }

});
    // close popover
$(document).click(function (e) {
    if (($('.popover').has(e.target).length == 0)) {
        $('.navbar-button.account').popover('hide');
    }
});
    // redirect to user edit page
$('body').on('click', '.btn.btn-md.btn-info', function () {
    window.location.href = "../user/edit/" + id;
});




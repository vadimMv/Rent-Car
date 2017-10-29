$(document).ready(function () {
    var ID = 0;
    var str, fin, rfin;
    // get data from inputs by click
    $('.rent-i').on('click', function () {
        ID = $(this).data('id');
        $('#usr').val($(this).data('u'));
        $('#car').val($(this).data('c'));
        $('#str').val($(this).data('s'));
        $('#fin').val($(this).data('f'));
        $('#rfin').val($(this).data('rf')); 
        str = $(this).data('s');
        fin = $(this).data('f');
        rfin = $(this).data('rf');
        $('#EditModal').modal('show');
    });
    // mode id & carNum readonly 
    $('.btn-info').on('click', function () {

        $(".form-control.editable").each(function () {

            if ($(this).attr('id') == 'usr' || $(this).attr('id') == 'car') {
                $(this).attr('readonly', true);
            }
            else
                $(this).attr('readonly', false);
        });
        $('.drop1').css({ opacity: 1.0, visibility: "visible" });
    });
     // send edited data to server 
    $('.drop1').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'http://localhost:54096/staff/worker/edit/',
            dataType: 'json',
            data: {
                id: ID,
                NumUser: $('#usr').val(),
                NumCar: $('#car').val(),
                StartRent: $('#str').val(),
                FinishRent: $('#fin').val(),
                RealFinishRent: $('#rfin').val(),
            },
            success: function (msg) {
                // succsess messages
                if (ID == msg.id) {
                    $('#str').val(parseJsonDate(msg.StartRent));
                    $('#fin').val(parseJsonDate(msg.FinishRent));
                    $('#rfin').val(parseJsonDate(msg.RealFinishRent));
                    $("#msg").html('<div class="alert alert-info" role="alert" style="margin-top:15px"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>System Message. </strong> Updated successfully!!!</div>');
                } else {
                    $("#msg").html('<div class="alert alert-warning" role="alert" style="margin-top:15px"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>System Message. </strong> Error validation detected !!!</div>');
                    $('#str').val(str);
                    $('#fin').val(fin);
                    $('#rfin').val(rfin);
                }
            }
        });
    });
    // reload page by hidding modal view & make all inputs readonly
    $('#EditModal').on('hidden.bs.modal', function () {
        $(".form-control.editable").each(function () {
            $(this).attr('readonly', true);
        });
        $('.drop1').css({ opacity: 1.0, visibility: "collapse" });
        $('#msg').html("");
        window.setTimeout('location.reload()', 500);

    });
});


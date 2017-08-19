$(document).ready(function () {
    $("#adresUlkeID").change(function () {
        var id = $(this).val();
        var ilList = $("#adresILID");
        ilList.empty();

        $.ajax({
            url: '/Kisi/ilList',
            type: 'POST',
            dataType: 'json',
            data: { 'id': id },
            success: function (data) {
                $.each(data, function (index, option) {
                    ilList.append('<option value=' + option.Value + '>' + option.Text + '</option>')
                });

            }
        });
    });
    $("#adresUlkeID2").change(function () {
        var id = $(this).val();
        var ilList = $("#adresILID2");
        ilList.empty();

        $.ajax({
            url: '/Kisi/ilList',
            type: 'POST',
            dataType: 'json',
            data: { 'id': id },
            success: function (data) {
                $.each(data, function (index, option) {
                    ilList.append('<option value=' + option.Value + '>' + option.Text + '</option>')
                });

            }
        });
    });
});
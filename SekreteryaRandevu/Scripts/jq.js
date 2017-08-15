$(document).ready(function () {
    var i = 1, k = 1, m = 1;
    $("#eklecep").click(function (e) {
        var y = '<div id="row1' + (i++) + '" class="form-group">' +
            '<label class="col-md-2">Cep Telefonu : </label>' +
            '<input type="tel" name="value' + (i++) + '" />' +
            '<button id="' + (i++) + '" type="button">s</button>' +
        '</div>'
        $("#icep").append(y);
    })
    $("#ekleis").click(function (e) {
        var s = '<div id="row2' + (k++) + '" class="form-group">' +
            '<label class="col-md-2">İs Telefonu : </label>' +
            '<input type="tel" name="value' + (k++) + '" />' +
            '<button id="' + (k++) + '"type="button">xSil</button>' +
        '</div>'
        $("#iis").append(s);
    })
    $("#ekleev").click(function (e) {
        m++;
        var d = '<div id="row' + m + '" class="form-group">' +
       '<label class="col-md-2">Ev Telefonu : </label>' +
       '<input type="tel" name="value' + m + '" /> ' +
       '<button id="' + m + '"  name="sil" type="button" class="btn-danger">xSil</button>' +
    '</div>'    
        $("#iev").append(d);
    })
    $(document).on('click', '.btn-danger', function (e) {
        var btn_id = $(this).attr('id');
        $('#row' + btn_id + '').remove();

    })
});
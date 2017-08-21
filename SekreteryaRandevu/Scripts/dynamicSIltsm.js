$(document).ready(function () {
    var i = 1, k = 1, m = 1, x = 1, c = 1, s = 1, t = 6;
    $("#eklecep").click(function (e) {
        i++;
        var y = '<div id="rowc' + i + '" class="col-md-10">' +
            '<label class="col-md-2">Cep Telefonu : </label>' +
            '<input type="tel" name="iletisimToSirkets[' + t + '].value" />' +
            '<button id="c' + i + '" type="button" name="sil" class="btn-danger">Sil</button>' +
            '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="1"></option></select>' +
        '</div>'
        $("#icep").append(y);
        t++;
    })
    $("#ekleis").click(function (e) {
        k++;
        var s = '<div id="rowi' + k + '" class="col-md-10">' +
            '<label class="col-md-2">İs Telefonu : </label>' +
            '<input type="tel" name="iletisimToSirkets[' + t + '].value" />' +
            '<button id="i' + k + '"type="button" name="sil" class="btn-danger">Sil</button>' +
                        '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="6"></option></select>' +

        '</div>'
        $("#iis").append(s);
        t++;
    })
    $("#ekleev").click(function (e) {
        m++;
        var d = '<div id="rowe' + m + '" class="col-md-10">' +
       '<label class="col-md-2">Ev Telefonu : </label>' +
       '<input type="tel" name="iletisimToSirkets[' + t + '].value" /> ' +
       '<button id="e' + m + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
                   '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="4"></option></select>' +

    '</div>'
        $("#iev").append(d);
        t++;
    })
    $("#eklemail").click(function (e) {
        x++;
        var d = '<div id="rowm' + x + '" class="col-md-10">' +
       '<label class="col-md-2">E-Mail : </label>' +
       '<input type="email" name="iletisimToSirkets[' + t + '].value" /> ' +
       '<button id="m' + x + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
                   '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="11"></option></select>' +

    '</div>'
        $("#imail").append(d);
        t++;
    })
    $("#eklefax").click(function (e) {
        c++;
        var d = '<div id="rowf' + c + '" class="col-md-10">' +
       '<label class="col-md-2">Fax No : </label>' +
       '<input type="tel" name="iletisimToSirkets[' + t + '].value" /> ' +
       '<button id="f' + c + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
                   '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="9"></option></select>' +

    '</div>'
        $("#ifax").append(d);
        t++;
    })
    $("#eklesite").click(function (e) {
        s++;
        var d = '<div id="rows' + s + '" class="col-md-10">' +
       '<label class="col-md-2">Web Site : </label>' +
       '<input type="text" name="iletisimToSirkets[' + t + '].value" /> ' +
       '<button id="s' + s + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
                   '<select name="iletisimToSirkets[' + t + '].iletisimID" style="display:none"><option value="12"></option></select>' +

    '</div>'
        $("#isite").append(d);
        t++;
    })
    $(document).on('click', '.btn-danger', function (e) {
        var btn_id = $(this).attr('id');
        $('#row' + btn_id + '').remove();

    })
});
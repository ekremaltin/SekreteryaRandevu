$(document).ready(function () {
    var i = 1, k = 1, m = 1, x = 1, c = 1,s=1;
    $("#eklecep").click(function (e) {
        i++;
        var y = '<div id="rowc' + i + '" class="form-group">' +
            '<label class="col-md-2">Cep Telefonu : </label>' +
            '<input type="tel" name="cep" />' +
            '<button id="c' + i + '" type="button" name="sil" class="btn-danger">Sil</button>' +
        '</div>'
        $("#icep").append(y);
    })
    $("#ekleis").click(function (e) {
        k++;
        var s = '<div id="rowi' + k + '" class="form-group">' +
            '<label class="col-md-2">İs Telefonu : </label>' +
            '<input type="tel" name="is" />' +
            '<button id="i' + k + '"type="button" name="sil" class="btn-danger">Sil</button>' +
        '</div>'
        $("#iis").append(s);
    })
    $("#ekleev").click(function (e) {
        m++;
        var d = '<div id="rowe' + m + '" class="form-group">' +
       '<label class="col-md-2">Ev Telefonu : </label>' +
       '<input type="tel" name="ev" /> ' +
       '<button id="e' + m + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
    '</div>'
        $("#iev").append(d);
    })
    $("#eklemail").click(function (e) {
        x++;
        var d = '<div id="rowm' + x + '" class="form-group">' +
       '<label class="col-md-2">E-Mail : </label>' +
       '<input type="email" name="mail" /> ' +
       '<button id="m' + x + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
    '</div>'
        $("#imail").append(d);
    })
    $("#eklefax").click(function (e) {
        c++;
        var d = '<div id="rowf' + c + '" class="form-group">' +
       '<label class="col-md-2">Fax No : </label>' +
       '<input type="tel" name="fax" /> ' +
       '<button id="f' + c + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
    '</div>'
        $("#ifax").append(d);
    })
    $("#eklesite").click(function (e) {
        s++;
        var d = '<div id="rows' + s + '" class="form-group">' +
       '<label class="col-md-2">Web Site : </label>' +
       '<input type="text" name="site" /> ' +
       '<button id="s' + s + '"  name="sil" type="button" class="btn-danger">Sil</button>' +
    '</div>'
        $("#isite").append(d);
    })
    $(document).on('click', '.btn-danger', function (e) {
        var btn_id = $(this).attr('id');
        $('#row' + btn_id + '').remove();

    })
});
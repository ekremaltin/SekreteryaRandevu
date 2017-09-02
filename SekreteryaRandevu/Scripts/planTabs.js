$('#tabs').tabs();
$('li[id=btnTab]').click(function () {
    var btn_id = $(this).attr('id');
    //$('#framework2').multiselect('select', []);
    $('#txtTabName').val("");
    $("#NewTabsPerson").modal("show");
    //$('#hdEventID').val(selectedEvent.eventID);
})
$(document).on('click', 'li[id^=sb]', function (e) {
    var tabKisiID = $(this).children().children().attr('id');
    var text = $(this).children().attr('href') + ' ' + '#calendar';
    $('#whichTab').val(text);
    $('#kaynakKisiID').val(tabKisiID);
    FetchEventAndRenderCalendar(tabKisiID);

})
$('#framework').multiselect({
    nonSelectedText: 'Toplantıya Katımlımcı Ekleyiniz',
    includeSelectAllOption: true, //Tüm seçim
    selectAllJustVisible: false, //Görünmeyenlerinde tümünün seçimi
    selectAllText: 'Herkesi seç!',
    enableClickableOptGroups: true, //Departman altı gruplandırma
    enableFiltering: true, //Arama aktif etme
    enableCaseInsensitiveFiltering: true
})
$('#framework2').multiselect({
    nonSelectedText: 'Randevu Sistem Kişileri',
    includeSelectAllOption: true, //Tüm seçim
    selectAllJustVisible: false, //Görünmeyenlerinde tümünün seçimi
    selectAllText: 'Herkesi seç!',
    enableClickableOptGroups: true, //Departman altı gruplandırma
    enableFiltering: true, //Arama aktif etme
    enableCaseInsensitiveFiltering: true
})
$('#framework3').multiselect({
    nonSelectedText: 'Randevu Sistem Kişileri',
    includeSelectAllOption: true, //Tüm seçim
    selectAllJustVisible: false, //Görünmeyenlerinde tümünün seçimi
    selectAllText: 'Herkesi seç!',
    enableClickableOptGroups: true, //Departman altı gruplandırma
    enableFiltering: true, //Arama aktif etme
    enableCaseInsensitiveFiltering: true
})


//$('#btnSave1').click(function () {
//    var list = $('#framework').val();
//    var a = [];
//    $.each($('#framework').val(), function (i, v) {
//        a.push(v);
//    });
//    alert(a);
//})
var i = 4;
$('#btnSaveTab').click(function () {

    //var kisi = $('#framework2').val();            
    if ($('#txtTabName').val().trim() == "") {
        alert('Takvim kişisi seçiniz!');
        return;
    }
    var y = '<li id="sby' + i + '"><a href="#tab' + i + '" ><input type="hidden" id="' + $('#framework2').val() + '" />' + $('#txtTabName').val() + '</a></li>';
    var yDiv = '<div id="tab4"><div></div></div>';
    $('#tabs').append(yDiv);
    $('#tabsUL').append(y);
    i++;
    $("#NewTabsPerson").modal("hide");
    $('#tabs').tabs('refresh')

})
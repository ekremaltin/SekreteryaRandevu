$(document).ready(function () {
    var events = [];
    FetchEventAndRenderCalendar();
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Plan/GetEvents",
            success: function (data) {
                $.each(data, function (i, v) {
                    events.push({
                        eventID: v.planID,
                        title: v.planKisaBilgi,
                        description: v.planUzunBilgi,
                        start: moment(v.planStartTarih),
                        end: v.planEndTarih != null ? moment(v.planEndTarih) : null,
                        place: v.planMekan,
                        color: v.planColor,
                        allDay: v.planFullDay,
                        isComp: v.planisCompleted,
                        ekBilgi: v.planEkBilgi,
                        kimin: v.planKisiID,
                        olusturan: v.planUserID
                    });
                })

                GenerateCalender(events);
            },
            error: function (error) {
                alert('failed');
            }
        })
    }

    function SearchKisi() {
        $.ajax({
            type: 'GET',
            url: '/Plan/SearchKisiList',
            datatype: 'json',
            data: { searchKelime: $("#txtSearchKisi").val() },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.kisiAdi, value: item.kisiAdi };
                }));

            },
            error: function (xhr, status, error) {
                alert("search yok");
            }
        })
    }


    function GenerateCalender(events) {
        $('#calendar').fullCalendar('destroy');
        $('#calendar').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            //dayClick: function(){
            //    alert('tıklandı');
            //},
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                selectedEvent = calEvent;
                $('#myModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Başlama Tarihi:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>Bitiş Tarihi:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Randevu Açıklama:</b>' + calEvent.description));
                $description.append($('<p/>').html('<b>Randevu Yeri:</b>' + calEvent.place));
                $description.append($('<p/>').html('<b>Randevu Ek Bilgi:</b>' + calEvent.ekBilgi));
                $('#myModal #pDetails').empty().html($description);
                $('#myModal').modal();
            },
            selectable: true, //Birden fazla gün seçimi çoklu seçim
            select: function (start, end) {
                selectedEvent = {
                    eventID: 0,
                    title: '',
                    description: '',
                    start: start,
                    end: end,
                    allDay: false,
                    color: ''
                };
                openAddEditForm();
                $('#calendar').fullCalendar('unselect');
            },
            editable: true,
            eventDrop: function (event) {
                var data = {
                    planID: event.eventID,
                    planKisaBilgi: event.title,
                    planStartTarih: event.start.format('DD/MM/YYYY HH:mm A'),
                    planEndTarih: event.end != null ? event.end.format('DD/MM/YYYY HH:mm A') : null,
                    planUzunBilgi: event.description,
                    planColor: event.color,
                    planFullDay: event.allDay,
                    planMekan: event.place,
                    planEkBilgi:event.ekBilgi,
                    planKisiID: event.kimin
                };
                SaveEvent(data);
            }
        })
    }
    $('#btnEdit').click(function () {
        //Open modal dialog for edit event
        openAddEditForm();
    })
    $('#btnDelete').click(function () {
        if (selectedEvent != null && confirm('Emin misiniz?')) {
            $.ajax({
                type: "POST",
                url: '/Plan/DeleteEvent',
                data: { 'id': selectedEvent.eventID },
                success: function (data) {
                    if (data.status) {
                        //Refresh the calender
                        FetchEventAndRenderCalendar();
                        $('#myModal').modal('hide');
                    }
                },
                error: function () {
                    alert('Failed');
                }
            })
        }
    })
    $('#dtp1,#dtp2').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A'
    });
    $('#chkIsFullDay').change(function () {
        if ($(this).is(':checked')) {
            $('#divEndDate').hide();
        }
        else {
            $('#divEndDate').show();
        }
    });
    $('#txtSearchKisi').autocomplete({
        source: function (request, response) {
            var kisiList = $("#txtSearchKisi");
            $.ajax({
                url: '/Plan/SearchKisiList',
                datatype: 'json',
                data: { searchKelime: $("#txtSearchKisi").val() },
                success: function (data) {
                    $.each(data, function (index, option) {
                        kisiList.append('<option value=' + option.kisiID + '>' + option.kisiAdi + '</option>')
                    });
                    //response($.map(data, function (item) {
                    //    return {
                    //        label: item.kisiAdi, value: item.kisiAdi, id: item.kisiID
                    //    };
                    //}));
                },
                error: function (xhr, status, error) {
                    alert("search yok");
                }
            });
        },
        //select: function (event, ui) {

        //}

    });
    $('#btnSave').click(function () {
        //Validation/
        if ($('#txtSubject').val().trim() == "") {
            alert('Subject required');
            return;
        }
        if ($('#txtStart').val().trim() == "") {
            alert('Start date required');
            return;
        }
        if ($('#chkIsFullDay').is(':checked') == false && $('#txtEnd').val().trim() == "") {
            alert('End date required');
            return;
        }
        else {
            var startDate = moment($('#txtStart').val(), "DD/MM/YYYY HH:mm A").toDate();
            var endDate = moment($('#txtEnd').val(), "DD/MM/YYYY HH:mm A").toDate();
            if (startDate > endDate) {
                alert('Invalid end date');
                return;
            }
        }

        var data = {
            planID: $('#hdEventID').val(),
            planKisaBilgi: $('#txtSubject').val().trim(),
            planStartTarih: $('#txtStart').val().trim(),
            planEndTarih: $('#chkIsFullDay').is(':checked') ? null : $('#txtEnd').val().trim(),
            planUzunBilgi: $('#txtDescription').val(),
            planColor: $('#ddThemeColor').val(),
            planFullDay: $('#chkIsFullDay').is(':checked'),
            planMekan: $('#txtMekan').val().trim(),
            planEkBilgi: $('#txtEkBilgi').val(),
            planKisiID: $('#txtSearchKisi').val()
        }
        SaveEvent(data);
        // call function for submit data to the server
    })

    function openAddEditForm() {
        if (selectedEvent != null) {
            $('#hdEventID').val(selectedEvent.eventID);
            $('#txtSubject').val(selectedEvent.title);
            $('#txtStart').val(selectedEvent.start.format('DD/MM/YYYY HH:mm A'));
            $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
            $('#chkIsFullDay').change();
            $('#txtEnd').val(selectedEvent.end != null ? selectedEvent.end.format('DD/MM/YYYY HH:mm A') : '');
            $('#txtDescription').val(selectedEvent.description);
            $('#ddThemeColor').val(selectedEvent.color);
            $('#txtMekan').val(selectedEvent.place);
            $('#txtEkBilgi').val(selectedEvent.ekBilgi);
            $('#txtSearchKisi').val(selectedEvent.olusturan);
        }
        $('#myModal').modal('hide');
        $('#myModalSave').modal();
    }
    function SaveEvent(data) {
        $.ajax({
            type: "POST",
            url: '/Plan/SaveEvent',
            data: data,
            success: function (data) {
                if (data.status) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar();
                    $('#myModalSave').modal('hide');
                }
            },
            error: function () {
                alert('Failed');
            }
        })
    }
})
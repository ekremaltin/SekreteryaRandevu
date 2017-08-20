$(document).ready(function () {
    function deleteData() {
        var id = $(this).val();
        $.ajax({
            type: 'POST',
            url: '/kisi/nesneSil',
            data: { 'id': id },
            success: function (data) {

            }

        })
    }
    
});
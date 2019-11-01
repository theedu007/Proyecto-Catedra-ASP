var id_relation;

$(document).ready(function () {
    reloadForm();
});

$('#id_compra').on('change', function () {
    $("#fecha_limite option").remove();
    reloadForm();
});

$('#id_venta').on('change', function () {
    $("#fecha_limite option").remove();
    reloadForm();
});

function reloadForm() {
    var url = window.location.pathname.split("/");
    if (url[1] == "CxP")
        id_relation = '#id_compra';
    else if (url[1] == "CxC")
        id_relation = '#id_venta';


    $('#submit_button').attr("disabled", true);
    if ($(id_relation).has('option').length > 0) {
        setMaxAbonado(url[1]);
    }
}

function setMaxAbonado(trans){

    $.ajax({
        url: '/' + trans +'/getCantidadTrans',
        type: 'post',
        dataType: "json",
        data: { data: $(id_relation + " option:selected").val() },
        success: function (data)
        {
            $.each(data.fechas, function (key, val) {
                date = val.split('/');
                $('#fecha_limite').append('<option value=\'' + date[2] + '-' + date[1] + '-' + date[0] + '\'>' + (key + 1) * 30 +' dias &emsp; --> &emsp; ('+ val + ') </option>');
            });

            $("#abono_inicial").attr({
                "min": 1,
                "max": Number(data.cantidad_max)
            });

            $('#submit_button').attr("disabled", false);
        },
        error: function (data) { alert(JSON.stringify(data)); }
    });

}
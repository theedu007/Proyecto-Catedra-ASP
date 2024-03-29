﻿/*---------LOCAL VARIABLES----------*/
var precio_producto;
/*----------------------------------*/

$(document).ready(function () {
    setLimitVenta();
});

$('#id_producto').on('change', function () {
    setLimitVenta();
});


function setLimitVenta() {
    resetFields(true);
    id_producto = $('#id_producto option:selected').val();
    $.ajax({
        url: '/Ventas/getProductbyId',
        type: 'post',
        data: { data:id_producto },
        success: function (data) {
            precio_producto = data.precio;
            if (data.cantidad > 0) {
                $("#cantidad_venta").attr({
                    "min": 1,
                    "max": data.cantidad
                });
                resetFields(false);
            }
            else {
                alert('Lo sentimos no existen existencias de este producto');
            }
        },
        error: function (data) {
            alert('La llamada (ajax)getProductbyId ha fallado');
        }
    });
}

function resetFields(toState) {
    $('#submit').attr('disabled', toState);
    $('#cantidad_venta').attr('readonly', toState);
    $('#cantidad_venta').val('');
    $('#precio_final').val('')
}


$('#cantidad_venta').keyup(function () {
    var precio_final = precio_producto * $(this).val();
    $('#precio_final').val(precio_final);
});
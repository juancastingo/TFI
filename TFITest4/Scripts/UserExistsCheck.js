$('#crearProv').prop('disabled', true);

$('#UserCheck').focusout(function () {
    $('#UserCheck span').text("");
    var nombre = $('#UserCheck input').val()
    if (nombre != "" && nombre.length < 16) {
        $('#UserCheck img').toggleClass("oculto");
        console.log("In FocusOut " + nombre);
        //var serviceURL = '@Url.Action("AjaxUserCheck", "Account")';
        var serviceURL = '/Account/AjaxUserCheck';
        var dataToSend = { name: nombre }
        $.ajax({
            type: "POST",
            url: serviceURL,
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successFuncCheck,
            error: errorFuncCheck
        });
    }

    function successFuncCheck(data, status) {
        console.log(data);
        console.log("bien");
        $('#UserCheck img').toggleClass("oculto");
        if (data.existe) {
            $('#UserCheck span').text("Error, el nombre de usuario ya existe");
            $('#UserCheck span').text("@Language.UsuarioExistente");
            $('#UserCheck span').addClass("mal");
            $('#UserCheck span').removeClass("bien");
            $('#crearProv').prop('disabled', true);
        } else {
            $('#UserCheck span').text("El usuario está disponible");
            $('#UserCheck span').text("@Language.UsuarioExistenteOK");
            $('#UserCheck span').addClass("bien");
            $('#UserCheck span').removeClass("mal");
            $('#crearProv').prop('disabled', false);
        }

    }
    function errorFuncCheck(data, status) {
        console.log("ERROR in ajax request");
        console.log(data);
        console.log(status);
    }
});
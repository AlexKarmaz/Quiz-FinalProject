$(document).ready(function () {

    $('button#nav_button_login').click(function () {
        location.href = window.location.origin + '/Account/Login';
    });
    $('button#nav_button_register').click(function () {
        location.href = window.location.origin + "/Account/Register";
    });

});

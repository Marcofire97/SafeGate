$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();

        var username = $('#username').val();
        var password = $('#password').val();

        $.ajax({
            url: '/Login/LoginValidation',
            type: 'POST',
            data: {
                username: username,
                password: password
            },
            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.href = '/Home/Index';
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Errore nella richiesta AJAX: " + error);
            }
        });
    });

    $('#toggle-password').click(function () {
        var passwordInput = $('#password');
        var type = passwordInput.attr('type') === 'password' ? 'text' : 'password';
        passwordInput.attr('type', type);

        var eyeIcon = $(this).find('i');
        if (type === 'password') {
            eyeIcon.removeClass('fa-eye-slash').addClass('fa-eye');
        } else {
            eyeIcon.removeClass('fa-eye').addClass('fa-eye-slash');
        }
    });
});



function init(username) {
    $('[data-toggle="popover"]').popover({
        html: true,
        title: function () {
            return $("#popover-head").html();
        },
        content: function () {
            return $("#popover-content").html();
        }
    })

    if (username != '') {
        $('#lbtn').html(username + ' - logout');
        $('#lbtn').popover("hide");
        $('#lbtn').popover("disable");
        loggedin = true;
        $('#registerButton').hide();
    }
    else {
        $('#lbtn').html('Login');
        $('#lbtn').popover("enable");
        loggedin = false;
    }
}

var loggedin = false;

function logout() {
    $('#registerButton').attr('visible', 'true');
    if (loggedin) {
        $.ajax({
            method: "GET",
            url: "/Account/AjaxLogout",
            error: function () { alert('error'); }
        }).done(function (html) {
            $('#lbtn').html('login');
            $('#lbtn').popover("enable");
            loggedin = false;
            $('#registerButton').show();
        });
        location.reload();
    }
}

function login() {
    if ($('#email').val() == '') {
        alert('Email field is empty');
        return;
    }

    $.ajax({
        method: "GET",
        url: "/Account/AjaxLogin",
        data: {
            email: $('#email').val(),
            password: $('#password').val()
        },
        error: function () { alert('error'); }
    }).done(function (data) {
        if (data.status == 'sent') {
            $('#dologin').html('Login');
            $('#password').removeAttr('disabled');
        }
        else
            if (data.status == 'noemail') {
                alert("Email is incorrect");
            }
            else if (data.status == 'ok') {
                $('#lbtn').html(data.username + ' - logout');
                $('#lbtn').popover("hide");
                $('#lbtn').popover("disable");
                if (delayed)
                {
                    $.ajax({
                        method: "GET",
                        url: delayed,
                        data: {},
                    });
                }

                loggedin = true;
                location.reload();
            } else if (data.status == 'nopass') {
                alert('Password is incorrect or expired.');
            }
    });
}

function register() {
    if ($('#regemail').val() == '') {
        alert('Email field is empty');
        return;
    }

    $.ajax({
        method: "GET",
        url: "/Account/AjaxRegister",
        data: {
            email: $('#regemail').val(),
            name: $('#name').val()
        },
        error: function () { alert('error'); }
    }).done(function (r) {
        if (r == 'ok') {
            alert('Confirmation email sent. Please, confirm it to complete registration.');
        } else {
            alert('An error has occured during sending email. Please, check it. If error continue happening, let us know.');
        }
    });
}
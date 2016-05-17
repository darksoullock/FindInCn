

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
    }
    else {
        $('#lbtn').html('Login');
        $('#lbtn').popover("enable");
        loggedin = false;
    }
}

var loggedin = false;

//function load_content() {
//    $.ajax({
//        method: "GET",
//        url: "/Home/AjaxContent",
//        error: function () { alert('error loading page'); }
//    }).done(function (html) {
//        $("#content").html(html);
//    });
//}

function logout() {
    if (loggedin) {
        $.ajax({
            method: "GET",
            url: "/Account/AjaxLogout",
            error: function () { alert('error'); }
        }).done(function (html) {
            $('#lbtn').html('login');
            $('#lbtn').popover("enable");
            loggedin = false;
            load_content();
        });
        location.reload();
    }
}

function login() {
    $.ajax({
        method: "GET",
        url: "/Account/AjaxLogin",
        data: {
            email: $('#email').val(),
            password: $('#password').val()
        },
        error: function () { alert('error'); }
    }).done(function (jsonString) {
        var data = JSON.parse(jsonString);
        if (data.status == 'sent') {
            $('#dologin').html('Login');
            $('#password').removeAttr('disabled');
        }
        else
            if (data.status == 'noemail') {
                alert("Email is incorrect");
            }
            else if (data.status=='ok')
            {
                $('#lbtn').html(data.username + ' - logout');
                $('#lbtn').popover("hide");
                $('#lbtn').popover("disable");
                loggedin = true;
                location.reload();
            }
    });
}

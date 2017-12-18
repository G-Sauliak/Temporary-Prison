window.IsValidClass = function (urlIsValidLogin, urlIsvalidEmail) {
    var _urlLogin = urlIsValidLogin;
    var _urlEmail = urlIsvalidEmail;

    function _Request(typeReq, urlReq, getData, callback) {
        $.ajax({
            type: typeReq,
            url: urlReq,
            data: getData(),
            success: function (result) {
                callback(result);
            },
            error: function (e) {
                console.log(e);
            }
        });
    }

    function IsExistsByUserName() {
        var iconIsValid = $("#NameisValid");
        var iconIsNotValid = $("#NameisNotValid");

        HideAllIcon();

        function IsValid(userName) {
            if (userName.length >= 3 && userName.length <= 15) {
                var regex = /^[a-zA-Z]*$/;
                return regex.test(userName);
            }
            return false;
        }
        var callback = function (result) {
            switch (result.isValid) {
                case true:
                    iconIsNotValid.show();
                    break;

                case false: iconIsValid.show();
                    break;

                default: HideAllIcon();
            }
        }
        var getUserName = function () {
            var editorFor = $("#userName");
            var userName = { userName: editorFor.val() };
            return userName;
        }
        if (IsValid($("#userName").val())) {
            _Request("Get", _urlLogin, getUserName, callback);
        }

        function HideAllIcon() {
            iconIsValid.hide();
            iconIsNotValid.hide();
        }
    }

    function IsExistsByEmail() {

        var iconIsValid = $("#EmailisValid");
        var iconIsNotValid = $("#EmailisNotValid");

        function IsValid(email) {
            var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return regex.test(email);
        }
        var callback = function (result) {
            switch (result.isValid) {
                case true: iconIsNotValid.show();
                    break;

                case false: iconIsValid.show();
                    break;

                default: HideAllIcon();
            }
        }
        var getEmail = function () {
            var editorFor = $("#email");
            var email = { email: editorFor.val() };
            return email;
        }
        if (IsValid($("#email").val())) {
            _Request("Get", _urlEmail, getEmail, callback);
        }
        else
        {
            HideAllIcon();
        }
        function HideAllIcon() {
            iconIsValid.hide();
            iconIsNotValid.hide();
        }
    }
    return {
        IsExistsByUserName: IsExistsByUserName,
        IsExistsByEmail: IsExistsByEmail
    };
}
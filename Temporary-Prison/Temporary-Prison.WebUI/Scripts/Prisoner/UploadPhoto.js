window.UploadPhoto = function (urlUpload) {
    var _urlUpload = urlUpload;

    function _uploadPhoto(typeReq, urlReq, getData, callback) {
        if (!_inProgress) {
            $.ajax({
                type: 'POST',
                url: _urlUpload,
                data: getData(),
                beforeSend: function () {
                    _inProgress = true;
                },
                success: function (result) {
                    callback(result);
                },
                error: function (e) {
                    console.log(e);
                },
                complete: function () {
                    _inProgress = false;
                }
            });
        }
    }

    function GetMessageByCode() {
        var callback = function (result) {
            var label = $("#lblMesg");
            label.text(result.message);
        }
        var getData = function () {
            var code = $(".codeCss");
            var data = { id: code.val() };
            return data;
        }
        _uploadPhoto("Get", _urlGet, getData, callback);
    }

}

$(document).ready(function () {
    $("#photoUpload").change(function () {
        var formData = new FormData();
        var totalFiles = $("#photoUpload").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = $("#photoUpload").files[i];
            formData.append("Photo ".concat(i), file);
        }
        $.ajax({
            type: "POST",
            url: '/Home/Upload',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false
            //success: function(response) {
            //    alert('succes!!');
            //},
            //error: function(error) {
            //    alert("errror");
            //}
        }).done(function () {
            alert('success');
        }.fail(function (xhr, status, errorThrown) {
            alert('fail');
        };
    });
});
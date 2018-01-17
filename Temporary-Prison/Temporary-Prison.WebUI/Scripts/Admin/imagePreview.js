window.PreviewImages = function () {

    function PreviewAvatar()
    {
        var preview = $("#AvatarOfPrisoner");
        var valueUpload = $("#fileDefaultAvatarOfPrisoner").val();
        if (valueUpload.length > 0) {
            preview.show();
            $("#btnAvatarRemove").show();
        }
        else {
            preview.hide();
            $("#btnAvatarRemove").hide();
        }
        var currentPhoto = $("#currentDefaultAvatar");
        if (currentPhoto.length) {
            currentPhoto.remove();
        }
        var file = $("#fileDefaultAvatarOfPrisoner").prop('files')[0];
        var reader = new FileReader();

        reader.addEventListener("load", function () {
            preview.attr('src', reader.result);
        }, false);

        if (file) {
            reader.readAsDataURL(file);
        }
    }
    function PreviewPhoto() {

        var preview = $("#photoOfPrisoner");
        var valueUpload = $("#fileDefaultPhotoOfPrisoner").val();
        if (valueUpload.length > 0) {
            preview.show();
            $("#btnPhotoRemove").show();
        }
        else {
            preview.hide();
            $("#btnPhotoRemove").hide();
        }
        var currentPhoto = $("#currentDefaultPhoto");
        if (currentPhoto.length) {
            currentPhoto.remove();
        }
        var file = $("#fileDefaultPhotoOfPrisoner").prop('files')[0];
        var reader = new FileReader();

        reader.addEventListener("load", function () {
            preview.attr('src', reader.result);
        }, false);

        if (file) {
            reader.readAsDataURL(file);
        }
    }

    return {
        PreviewAvatar: PreviewAvatar,
        PreviewPhoto: PreviewPhoto
    };
}

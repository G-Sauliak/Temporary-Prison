function PreviewImage() {
    var preview = $('img');
    var valueUpload = $("#photoUpload").val();
    if (valueUpload.length > 0) {
        $("#photoOfPrisoner").show();
        $("#btnRemove").show();
    }
    else {
        $("#photoOfPrisoner").hide();
        $("#btnRemove").hide();
    }
    var currentPhoto = $("#currentPhoto");
    if (currentPhoto.length) {
        currentPhoto.remove();
    }
    var file = $("#photoUpload").prop('files')[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        preview.attr('src', reader.result);
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

window.MultiplePhones = function () {

    var RemoveBtn = $('.delete_phone');

    function RemovePhone() {
        var lastPhone = $('p.number:last');
        var id = lastPhone.attr("id").valueOf();
        if (id === '1') {
            RemoveBtn.hide();
        }
        $('p.number:last').remove();
    }
    function AddPhone() {
        var lastPhoneNumber = $('p.number:last');
        var lastId = lastPhoneNumber.attr("id").valueOf();
        lastPhoneNumber.
            after('<p class="number" id=" ">'.
                concat($('p.number').html()).
                concat('</p>'));
        var newPhoneNumber = $('p.number:last');
        newPhoneNumber.attr("id", ++lastId);
        newPhoneNumber.children().val("");
        RemoveBtn.show();
    }

    return {
        AddPhone: AddPhone,
        RemovePhone: RemovePhone
    };
}

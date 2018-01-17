window.FindPrisoners = function (serachUrl) {
    var _serachUrl = serachUrl;

    function _Request(reqUrl, targetLoad, searchString) {
        targetLoad.load(reqUrl, searchString, function (response, status, xhr) {
            if (status === "error") {
                targetLoad.empty();
            }
        });
    }

    function Reset()
    {
        $("#UserListBox").empty();
        $("#findByName").val('');
        $("#findByDateOfDetention").val('');
        $("#findByAddress").val('');
    }

    function SearchFilter() {
        var Name = $("#findByName");
        var DateOfDetention = $("#findByDateOfDetention");
        var PlaceOfDetention = $("#findByAddress");
        var searchString =
            {
                DateOfDetention: DateOfDetention.val(),
                Name: Name.val(),
                Address: PlaceOfDetention.val()
            };

        _Request(serachUrl, $("#UserListBox"), searchString);
    }

    return {
        SearchFilter: SearchFilter,
        Reset: Reset
    };
}

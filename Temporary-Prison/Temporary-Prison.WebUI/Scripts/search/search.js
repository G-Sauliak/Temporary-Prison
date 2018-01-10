window.FindPrisoners = function (serachUrl) {
    var _serachUrl = serachUrl;

    function _Request(reqUrl, targetLoad, searchString) {
        targetLoad.load(reqUrl, searchString)
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
    };
}

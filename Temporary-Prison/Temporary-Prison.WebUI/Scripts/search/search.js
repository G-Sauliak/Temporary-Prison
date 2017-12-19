window.FindPriosners = function (serachUrl) {
    var _serachUrl = serachUrl;

    function _Request(reqUrl, targetLoad, searchString) {
        targetLoad.load(reqUrl, searchString)
    }

    function FindPrisonersByName() {
        var editorFor = $("#findPrisonersByName");
        var searchString = {search: editorFor.val()};
        _Request(serachUrl, $("#UserListBox"), searchString);
    }

    function FindPrisonersByDateofDetained() { };

    function FindPrisonersByAddress() { };

    return {
        FindPrisonersByName: FindPrisonersByName,
    };
}
window.FindUsers = function (serachUrl) {
    var _serachUrl = serachUrl;

    function _Request(reqUrl, targetLoad, searchString) {
        targetLoad.load(reqUrl, searchString)
    }
    return {
        FindUsersByName: FindUsersByName,
    };
}

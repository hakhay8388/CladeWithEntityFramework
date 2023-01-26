import "../WebGraph/TagComponents/Utilities/History";
import "../WebGraph/Enums/Enums";


window.GetHost = function () {
    var __Url = window.location.href;
    var __NoHttp = __Url.split("//")[1];
    return __NoHttp.split("/")[0];
};

window.GetHostHttp = function () {
    var __Url = window.location.href;
    var __Http = __Url.split("//")[0];
    return __Http + "//";
};

window.GetHostName = function () {
    return window.location.hostname;
};

window.GetLanguageCodeFromUrl = function () {
    var __UrlPaths = window.GetUrl().split('/');
    if (__UrlPaths[0].length == 2) {
        ///Dil listesinden kontrol edilecek
        if (__UrlPaths[0] == "tr" || __UrlPaths[0] == "en") {
            return __UrlPaths[0];
        }
    }

    return "tr";
};

window.GetUrlParams = function () {
    var __Params = window.location.search ? window.location.search : "";
    var __Params = __Params.split("?");
    if (__Params.length > 1) {
        return "?" + __Params[1];
    }
    return "";
};


window.GetUrl = function () {
    var __Result = window.location.pathname;
    if (__Result.startsWith("/")) {
        var __Result = __Result.substring(1);
    }
    return __Result;
};

window.GetLanguageCodeFromUrl = function () {
    var __UrlPaths = window.GetUrl().split('/');
    if (__UrlPaths[0].length == 2) {
        ///Dil listesinden kontrol edilecek
        if (__UrlPaths[0] == "tr" || __UrlPaths[0] == "en") {
            return __UrlPaths[0];
        }
    }

    return "tr";
};
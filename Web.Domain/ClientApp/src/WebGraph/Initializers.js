//import "../WebGraph/TagComponents/Utilities/History";
import "../WebGraph/Enums/Enums";




if (!Array.prototype.includes) {
    Object.defineProperty(Array.prototype, "includes", {
        enumerable: false,
        value: function (obj) {
            var newArr = this.filter(function (el) {
                return el == obj;
            });
            return newArr.length > 0;
        }
    });
}

if (!Array.prototype.unique) {
    Object.defineProperty(Array.prototype, "unique", {
        enumerable: false,
        value: function (_function) {
            var a = this.concat();
            for (var i = 0; i < a.length; ++i) {
                for (var j = i + 1; j < a.length; ++j) {
                    if (_function(a[i], a[j])) {
                        a.splice(j--, 1);
                    }
                }
            }

            return a;
        }
    });

}
String.prototype.left = function (count) {
    return this.slice(0, count);
}

String.prototype.right = function (count) {
    return this.slice(-count);
}

String.prototype.format = function () {
    var __Formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp("\\{" + i + "\\}", "gi");
        __Formatted = __Formatted.replace(regexp, arguments[i]);
    }
    return __Formatted;
};

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
};


String.prototype.turkishToLower = function () {
    var result = this.replace(/I/g, "ý").toLocaleLowerCase();
    result = result.replace("ü", "u");
    result = result.replace("ý", "i");
    result = result.replace("þ", "s");
    result = result.replace("ð", "g");
    result = result.replace("ö", "o");
    result = result.replace("ç", "c");
    return result;
};
String.prototype.htmlDecode = function () {
    return this.replace(/&([a-z]+);/ig, (match, entity) => {
        const entities = { amp: '&', apos: '\'', gt: '>', lt: '<', nbsp: '\xa0', quot: '"' };
        entity = entity.toLowerCase();
        if (entities.hasOwnProperty(entity)) {
            return entities[entity];
        }
        return match;
    });
};

if (!String.prototype.startsWith) {
    try {
        Object.defineProperty(String.prototype, "startsWith", {
            value: function (search, pos) {
                pos = !pos || pos < 0 ? 0 : +pos;
                return this.substring(pos, pos + search.length) === search;
            },
        });
    } catch (_Ex) { }
}

if (typeof String.prototype.endsWith !== 'function') {
    try {
        String.prototype.endsWith = function (suffix) {
            return this.indexOf(suffix, this.length - suffix.length) !== -1;
        };
    } catch (_Ex) { }
}


Array.prototype.sortBy = function (p) {
    return this.slice(0).sort(function (a, b) {
        return a[p] > b[p] ? 1 : a[p] < b[p] ? -1 : 0;
    });
};
Array.prototype.sortByDesc = function (p) {
    return this.slice(0).sort(function (a, b) {
        return a[p] > b[p] ? -1 : a[p] < b[p] ? 1 : 0;
    });
};




window.SetUrl = function (_Url, _First = false) {

    if (_Url.startsWith("/")) {
        _Url = _Url.substring(1);
    }

    if (_Url.endsWith("/")) {
        _Url = _Url.substring(0, _Url.length - 1);
    }

    if (_First) {
        if (_Url == "") {
            window.History.replace(_Url, { firstPage: true });
        }
        else {
            window.History.replace("/" + _Url, { firstPage: true });
        }
    }
    else {
        if (_Url == "") {
            window.History.push(_Url);
        }
        else {
            window.History.push("/" + _Url);
        }

    }
};


window.GoPage = function (_Page, _First = false) {
    if (_Page == null) {
        window.SetUrl("", _First);
    } else {
        window.SetUrl(_Page, _First);
    }

    /*if (GenericWebGraph.MainPage && GenericWebGraph.MainPage != null) {
      if (_Page == null) {
        window.SetUrl("", _First);
      } else {
        window.SetUrl(_Page, _First);
      }
    } else {
      Pages.LoadPages(function () 
      {
        window.App.App.forceUpdate();
        //WebGraph.ForceUpdateAllWithAsyncLoad(true);
       //console.log("Pages Loaded...");
      });
    }*/
};

window.GoMainPage = function (_First = false) {
    window.GoPage("", _First);
};




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
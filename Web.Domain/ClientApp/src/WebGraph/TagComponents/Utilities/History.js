import { createBrowserHistory } from "history";
import GenericWebGraph from "../../../WebGraph/GenericWebController/GenericWebGraph";
import { WebGraph } from "../../../WebGraph/GenericCoreGraph/WebGraph/WebGraph";

window.History = createBrowserHistory();

/*
window.History.listen((_Location, _Action) =>
{
    console.log("_Location");
    console.log(_Location);
    console.log("_Action");
    console.log(_Action);
  
});
*/


window.History = createBrowserHistory();

window.LastPage = "";

window.History.listen((_Location, _Action) => {

    var __Url = _Location.location.pathname;
    if (__Url.startsWith("/")) {
        var __Url = __Url.substring(1);
    }


    if (__Url == window.LastPage) {
        WebGraph.OnUrlChanged();
    }

    window.LastPage = __Url;

    /*if (_Action == "POP") {
        setTimeout(function () {
            WebGraph.ForceUpdateAllForPop(true);
        });
    }*/

});



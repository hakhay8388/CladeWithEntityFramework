import { createBrowserHistory } from "history";
//import GenericWebGraph from "../../../WebGraph/GenericWebController/GenericWebGraph";
//import { WebGraph } from "../../../WebGraph/GenericCoreGraph/WebGraph/WebGraph";

window.History = createBrowserHistory();


window.History.listen((_Location, _Action) =>
{
    console.log("_Location");
    console.log(_Location);
    console.log("_Action");
    console.log(_Action);
  
});


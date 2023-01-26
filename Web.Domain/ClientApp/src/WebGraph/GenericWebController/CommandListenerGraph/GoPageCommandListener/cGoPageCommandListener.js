
import {
    Class,
    ObjectTypes,
} from "../../../GenericCoreGraph/ClassFramework/Class";



import cBaseCommandListener from "../cBaseCommandListener"
import {
    CommandInterfaces
} from "../../CommandInterpreter/cCommandInterpreter"
import GenericWebGraph from "../../../../WebGraph/GenericWebController/GenericWebGraph";


var cGoPageCommandListener = Class(
    cBaseCommandListener,
    CommandInterfaces.IGoPageCommandReceiver,

    {
        ObjectType: ObjectTypes.Get("cGoPageCommandListener"),
        constructor: function () {
            cGoPageCommandListener.BaseObject.constructor.call(this);
        }
        ,
        Receive_GoPageCommand: function (_Data) {
            if (_Data.Params == null) {
                GenericWebGraph.GoPage(_Data.Page.Url);
            }
            else {
                GenericWebGraph.GoPage(_Data.Page.Url + "?" + _Data.Params);
            }
        }
        ,
        Destroy: function () {
            //GenericWebGraph.CommandInterpreter.DisconnectToCommands(this);
            cGoPageCommandListener.BaseObject.Destroy.call(this);
        },
    },
    {}
);

export default cGoPageCommandListener;

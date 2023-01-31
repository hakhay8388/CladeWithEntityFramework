import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class"
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject"
//import GenericWebGraph from "../../GenericWebController/GenericWebGraph"
import cList from "../../GenericCoreGraph/List/cList"
import cLogInOutCommandListener from "./LogInOutCommandListener/cLogInOutCommandListener"
import cGoPageCommandListener from "./GoPageCommandListener/cGoPageCommandListener"
import cGeneralCommandListener from "./GeneralCommandListener/cGeneralCommandListener"
import cCacheCommandListener from "./CacheCommandListener/cCacheCommandListener"

var cCommandListenerGraph = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cCommandListenerGraph")
    ,
    constructor: function ()
    {
      cCommandListenerGraph.BaseObject.constructor.call(this);
      this.CommandListenerList = new cList(ObjectTypes.cBaseCommandListener);
      this.InitializeCommandListeners();
    }
    ,
    InitializeCommandListeners: function ()
    {
      this.LogInOutCommandListener = new cLogInOutCommandListener();
      this.GoPageCommandListener = new cGoPageCommandListener();
      this.GeneralCommandListener = new cGeneralCommandListener();
      this.CacheCommandListener = new cCacheCommandListener();
    }
    ,
    Destroy: function ()
    {

      this.LogInOutCommandListener.Destroy(); 
      this.GoPageCommandListener.Destroy();
      this.GeneralCommandListener.Destroy();
      this.CacheCommandListener.Destroy();


      this.CommandListenerList.Destroy();

      delete this.LogInOutCommandListener; 
      delete this.GoPageCommandListener;
      delete this.GeneralCommandListener;
      delete this.CacheCommandListener;

      delete this.CommandListenerList;
      delete this.WebGraph;
      cBaseObject.prototype.Destroy.call(this);
    }

  }, {});

export default cCommandListenerGraph






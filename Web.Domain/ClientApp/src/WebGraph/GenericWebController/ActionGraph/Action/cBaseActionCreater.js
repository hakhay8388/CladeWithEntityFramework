import { cObjType, DebugAlert, Class, Interface, Abstract, ObjectTypes, GlobalEval } from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";


var cBaseActionCreater = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cBaseActionCreater")
    , ActionGraph: null
    , ActionID: null
    , Enabled: true
    ,
    constructor: function (_ActionGraph)
    {
      cBaseActionCreater.BaseObject.constructor.call(this);
      this.ActionGraph = _ActionGraph;
      _ActionGraph.ActionInterpreterList.Add(this);
    }
    ,
    Destroy: function ()
    {
      delete this.ActionGraph;
      delete this.ActionID;
      delete this.Enabled;
      cBaseObject.prototype.Destroy.call(this);
    }
    ,
    InterpretAction: function (_Action, _ActionData, _DomItem)
    {
      DebugAlert.Show("cBaseActionCreater İçindeki InterpretAction Metodu Override Edilmemiş..!");
    }
    ,
    CreateAction: function ()
    {
      DebugAlert.Show("cBaseActionCreater İçindeki CreateAction Metodu Override Edilmemiş..!");
    }

  }, {});

export default cBaseActionCreater;








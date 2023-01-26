import { cObjType, DebugAlert, Class, Interface, Abstract, ObjectTypes, GlobalEval, cListForBase } from "../../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject";
import cList from "../../GenericCoreGraph/List/cList";
import { ActionIDs } from "./ActionIDs/ActionIDs";
import cBaseActionCreater from "./Action/cBaseActionCreater";
import cAction from "./Action/cAction";
import Actions from "./Actions";


const CreateActionDataFunctionsClass = function ()
{
};

export const CreateActionDataFunctions = new CreateActionDataFunctionsClass();

const ActionCreatesClass = function ()
{
};

export const ActionCreates = new ActionCreatesClass();

var cActionGraph = Class( cBaseObject,
  {
    ObjectType: ObjectTypes.Get( "cActionGraph" )
    , InAction: false
    , ActionInterpreterList: null
    , ActionQueue: null
    ,
    constructor: function ()
    {
      cActionGraph.BaseObject.constructor.call( this );
      this.ActionInterpreterList = new cList( ObjectTypes.cBaseActionCreater );
      this.ActionQueue = new cListForBase();
      this.InitActions( ActionIDs.ActionIDList );
    }
    ,
    InitActions: function ( _ActionIDList )
    {
      var __Count = _ActionIDList.Count();
      for ( var i = 0; i < __Count; i++ )
      {
        var __Item = _ActionIDList.GetItem( i );
        this.InitSingleAction( __Item );
      }
    }
    ,
    InitSingleAction: function ( _ActionItem )
    {
      this.CreateCommandsObjectTypes( _ActionItem );
      this.CreateActionDatas( _ActionItem );
      this.CreateActionCreaters( _ActionItem );
      this.InitializeActionCreaters( _ActionItem );
      this.AddActionsToGlobalActionCaller( _ActionItem );
    }
    ,
    InitializeActionCreaters: function ( _ActionItem )
    {
      var EActionCreates = ActionCreates;
      var __EvalString = "this." + _ActionItem.Name + "ActionCreater = new EActionCreates.c" + _ActionItem.Name + "ActionCreater(this);";
      eval( __EvalString );
    }
    ,
    AddActionsToGlobalActionCaller: function ( _ActionItem )
    {
      var EActions = Actions;
      var EThis = this;
      var __Parameters = "";
      for ( var j = 0; j < _ActionItem.Parameters.length; j += 2 )
      {
        if ( j == 0 )
        {
          __Parameters = "_" + _ActionItem.Parameters[j];
        }
        else
        {
          __Parameters += ", _" + _ActionItem.Parameters[j];
        }
      }
      if ( __Parameters == "" )
      {
        __Parameters += "_ResultFunction";
      }
      else
      {
        __Parameters += ", _ResultFunction";
      }

      __Parameters += ", _ActionConfigurationOptions";

      var __EvalString =
        "EActions." + _ActionItem.Name + "= function(" + __Parameters + ")" +
        "{" +
        "EThis." + _ActionItem.Name + "ActionCreater.CreateAction(" + __Parameters + ");" +
        "}";
      eval( __EvalString );

    }
    ,
    CreateCommandsObjectTypes: function ( _ActionItem )
    {
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var __EvalString = "EObjectTypes.c" + _ActionItem.Name + "ActionCreater = new EObjType(\"c" + _ActionItem.Name + "ActionCreater\");";
      eval( __EvalString );
    }
    ,
    HandleControlActionParams: function ()
    {
      var __ActionItem = arguments[0];

      if (arguments.length > __ActionItem.Parameters.length + 3)
      {
        return false;
      }

      for (var i = 0; i < __ActionItem.Parameters.length; i += 2)
      {
        var __SendedParamValue = arguments[(i / 2) + 1];
        //var __RealParamType = __ActionItem.Parameters[i + 1];
        if (typeof (__SendedParamValue) == 'function' || typeof (__SendedParamValue) == 'undefined')
        {
          return false;
        }
      }
      return true;
    }
    ,
    CreateActionCreaters: function ( _ActionItem )
    {

      var EClass = Class;
      var EInterface = Interface;
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var EBaseActionCreater = cBaseActionCreater;
      var EActionIDs = ActionIDs;
      var EAction = cAction;
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var ECreateActionDataFunctions = CreateActionDataFunctions;
      var EActionCreates = ActionCreates;
      var EActionItem = _ActionItem;
      var EThis = this;

      var __Parameters = "";
      for ( var j = 0; j < _ActionItem.Parameters.length; j += 2 )
      {
        if ( j == 0 )
        {
          __Parameters = "_" + _ActionItem.Parameters[j];
        }
        else
        {
          __Parameters += ", _" + _ActionItem.Parameters[j];
        }
      }

      if ( __Parameters == "" )
      {
        __Parameters += "_ResultFunction";
      }
      else
      {
        __Parameters += ", _ResultFunction";
      }
      __Parameters += ", _ActionConfigurationOptions";

      var __EvalString =
        "EActionCreates.c" + _ActionItem.Name + "ActionCreater = EClass(EBaseActionCreater," +
        "{" +
        "ObjectType: EObjectTypes.c" + _ActionItem.Name + "ActionCreater" +
        ", ActionID: EActionIDs." + _ActionItem.Name + "Action" +
        "," +
        "constructor: function (_ActionGraph)" +
        "{" +
        "EActionCreates.c" + _ActionItem.Name + "ActionCreater.BaseObject.constructor.call(this, _ActionGraph);" +
        "}" +
        ", BaseObject: function () { return EBaseActionCreater.prototype;},	Destroy: function (){EBaseActionCreater.prototype.Destroy.call(this);}," +
        "CreateAction: function(" + __Parameters + ")" +
        "{" +
        "if (EThis.HandleControlActionParams.apply(EThis.HandleControlActionParams, [EActionItem, ...arguments]))" +
        "{" +
        "var __TempData = new ECreateActionDataFunctions.c" + _ActionItem.Name + "ActionData(" + __Parameters + ");" +
        "var __Action = new EAction(this, __TempData, _ResultFunction, _ActionConfigurationOptions);" +
        "}" +
        "else" +
        "{" +
        "console.log('Parametre sayısı eksik veya fazla olabilir. Action ID : ' + '" + _ActionItem.Name + "');" +
        "console.log('Parameters');" +
        "console.log(" + __Parameters + ");" +
        "console.log('--------------------------------------------------');" +
        "console.log('arguments');" +
        "console.log(arguments);" +
        "}" +
        "}" +
        "}, {});";
      eval( __EvalString );

    }
    ,
    CreateActionDatas: function ()
    {
      var ECreateActionDataFunctions = CreateActionDataFunctions;
      var __Count = ActionIDs.ActionIDList.Count();
      for ( var i = 0; i < __Count; i++ )
      {
        var __Item = ActionIDs.ActionIDList.GetItem( i );
        var __Parameters = "";
        var __InParameters = "";
        for ( var j = 0; j < __Item.Parameters.length; j += 2 )
        {
          if ( j == 0 )
          {
            __Parameters = "_" + __Item.Parameters[j];
          }
          else
          {
            __Parameters += ", _" + __Item.Parameters[j];
          }
          __InParameters += "this." + __Item.Parameters[j] + " = _" + __Item.Parameters[j] + ";";
        }

        var __EvalString =
          "ECreateActionDataFunctions.c" + __Item.Name + "ActionData = function(" + __Parameters + ")" +
          "{" + __InParameters + "}";
        eval( __EvalString );
      }
    }
    ,
    Destroy: function ()
    {
      this.ActionQueue.Destroy();
      delete this.ActionQueue;
      cBaseObject.prototype.Destroy.call( this );
    }
    ,
    AddAction: function ( _Item )
    {
      this.ActionQueue.Add( _Item );
      if ( !this.InAction )
      {
        this.StartUpdater();
      }
    }
    ,
    StartUpdater: function ()
    {
      this.InAction = true;
      while ( this.ActionQueue.Count() > 0 )
      {
        try
        {
          var __Item = this.ActionQueue.GetItem( 0 );
          __Item.Action();
          this.ActionQueue.RemoveAt( 0 );
          //__Item.Destroy();
        }
        catch ( e )
        {
          DebugAlert.Show( "cActionGraph içindeki StartUpdater Sıkıntı Çıktı..!" );
        }
      }
      this.InAction = false;
    }
    ,
    InterpretAction: function ( _Action, _ActionData, _DomItem )
    {
      for ( var j = 0; j < _Action.length; j++ )
      {
        var __Count = this.ActionInterpreterList.Count();
        for ( var i = 0; i < __Count; i++ )
        {
          var __Item = this.ActionInterpreterList.GetItem( i );
          if ( __Item.ActionID.ID == _Action[j].ActionID.ID )
          {
            if ( __Item.Enabled )
            {
              __Item.InterpretAction( _Action[j], _ActionData, _DomItem );
            }
          }
        }
      }
    }
  }, {} );


export default cActionGraph;







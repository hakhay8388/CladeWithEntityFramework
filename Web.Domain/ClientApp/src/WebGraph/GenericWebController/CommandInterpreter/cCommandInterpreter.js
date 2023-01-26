import { cObjType, DebugAlert, Class, Interface, Abstract, ObjectTypes, GlobalEval } from "../../GenericCoreGraph/ClassFramework/Class"
import cList from "../../GenericCoreGraph/List/cList"
import { CommandIDs } from "./CommandIDs/CommandIDs"
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject"
import cBaseCommand from "../CommandInterpreter/Commands/cBaseCommand"

const CommandInterfacesClass = function ()
{
}

export const CommandInterfaces = new CommandInterfacesClass();

const CommandClassesClass = function ()
{
}

export const CommandClasses = new CommandClassesClass();


export const cCommandInterpreter = Class(cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cCommandInterpreter")
    , CommandList: null
    ,
    constructor: function ()
    {
      cCommandInterpreter.BaseObject.constructor.call(this);
      this.CommandList = new cList(ObjectTypes.cBaseCommand);
      this.CreateCommandsObjectTypes();
      this.CreateCommandReceivers();
      this.CreateCommands();
      this.InitializeCommand();
    }
    ,
    InitializeCommand: function ()
    {
      var ECommandClasses = CommandClasses;
      var EThis = this;
      var __Count = CommandIDs.CommandIDList.Count();
      for (var i = 0; i < __Count; i++)
      {
        var __CoreName = CommandIDs.CommandIDList.GetItem(i).CommandName;

        eval("var __TempFunction = function() { " +
          "var c" + __CoreName + "Command = module.exports.c" + __CoreName + "Command;" +
          "EThis." + __CoreName + "Command = new c" + __CoreName + "Command(EThis);" +

          "}()");
      }

    }
    ,
    ConnectToCommands: function (_Object)
    {
      for (var __Properties in _Object)
      {
        if (__Properties.match(/I[a-zA-Z]*CommandReceiver/))
        {
          var __TepString = __Properties.replace(/CommandReceiver/, "");
          __TepString = __TepString.substring(1, __TepString.length);
          var __Eval = "this." + __TepString + "Command.Connect(_Object);";
          eval(__Eval);
        }
      }
    }
    ,
    DisconnectToCommands: function (_Object)
    {
      for (var __Properties in _Object)
      {
        if (__Properties.match(/I[a-zA-Z]*CommandReceiver/))
        {
          var __TepString = __Properties.replace(/CommandReceiver/, "");
          __TepString = __TepString.substring(1, __TepString.length);
          var __Eval = "this." + __TepString + "Command.Disconnect(_Object);";
          eval(__Eval);
        }
      }
    }
    ,
    Destroy: function ()
    {
      this.CommandList.Destroy();
      this.cLoginCommand.Destroy();

      delete this.CommandList;
      delete this.cLoginCommand;
      delete this.WebGraph;
      cBaseObject.prototype.Destroy.call(this);
    }
    ,
    AddCommand: function (_Command)
    {
      this.CommandList.Add(_Command);
    }
    ,
    CreateCommandsObjectTypes: function ()
    {
      var __Count = CommandIDs.CommandIDList.Count();
      for (var i = 0; i < __Count; i++)
      {
        GlobalEval.call(this, "var aa = function() { " +



          "}()");
        var __Item = CommandIDs.CommandIDList.GetItem(i);
        var EObjType = cObjType;
        var EObjectTypes = ObjectTypes;
        var __EvalString = "EObjectTypes.c" + __Item.CommandName + "Command = new EObjType(\"c" + __Item.CommandName + "Command\");"
        eval(__EvalString);
        var __EvalString = "EObjectTypes.I" + __Item.CommandName + "CommandReceiver = new EObjType(\"I" + __Item.CommandName + "CommandReceiver\");"
        eval(__EvalString);
        var __EvalString = "EObjectTypes.c" + __Item.CommandName + "CommandConnector = new EObjType(\"c" + __Item.CommandName + "CommandConnector\");"
        eval(__EvalString);
      }
    }
    ,
    CreateCommandReceivers: function ()
    {

      var EClass = Class;
      var EInterface = Interface;
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var ECommandInterfaces = CommandInterfaces;

      var __Count = CommandIDs.CommandIDList.Count();
      for (var i = 0; i < __Count; i++)
      {
        var __CoreName = CommandIDs.CommandIDList.GetItem(i).CommandName;
        var __EvalString =
          "module.exports.I" + __CoreName + "CommandReceiver = EClass(EInterface," +
          "{" +
          "ObjectType : EObjectTypes.I" + __CoreName + "CommandReceiver," +
          "Receive_" + __CoreName + "Command : function(_Data){}" +
          "}, {});";
        eval(__EvalString);
        eval("ECommandInterfaces.I" + __CoreName + "CommandReceiver = module.exports.I" + __CoreName + "CommandReceiver");

      }
    }
    ,
    CreateCommands: function ()
    {
      var EClass = Class;
      var EInterface = Interface;
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var EBaseCommand = cBaseCommand;
      var ECommandIDs = CommandIDs;
      var EObjType = cObjType;
      var EObjectTypes = ObjectTypes;
      var ECommandInterfaces = CommandInterfaces;
      var ECommandClasses = CommandClasses;

      var __Count = CommandIDs.CommandIDList.Count();
      for (var i = 0; i < __Count; i++)
      {
        var __CoreName = CommandIDs.CommandIDList.GetItem(i).CommandName;
        var __EvalString =

          "module.exports.c" + __CoreName + "Command = EClass(EBaseCommand, {" +
          "ObjectType: EObjectTypes.c" + __CoreName + "Command" +
          ", Command: ECommandIDs." + __CoreName + "Command" +
          ", ReceiverInterface : module.exports.I" + __CoreName + "CommandReceiver" +
          ", ReceiverFunctionName : \"Receive_" + __CoreName + "Command\"" +
          "," +
          "constructor: function (_CommandInterpreter)" +
          "{" +
          "EBaseCommand.prototype.constructor.call(this, _CommandInterpreter);" +
          "},	BaseObject: function ()	{ return EBaseCommand.prototype;},Destroy: function (){	EBaseCommand.prototype.Destroy.call(this);}}, {});";

        eval(__EvalString);

      }
    }
    ,
    RemoveCommand: function (_Command)
    {
      this.CommandList.Remove(_Command);
    }
    ,
    InterpretCommand: function (_MsgObject)
    {
      for (var j = 0; j < _MsgObject.length; j++)
      {
        var __Count = this.CommandList.Count();
        for (var i = 0; i < __Count; i++)
        {
          var __Item = this.CommandList.GetItem(i);
          if (__Item.Command.CommandID == _MsgObject[j].ActionID.ID)
          {
            if (__Item.Enabled)
            {
              if (_MsgObject.ActionProps)
              {
                _MsgObject[j].Data.ActionProps = _MsgObject.ActionProps;
              }
              __Item.Run(_MsgObject[j].Data);
            }
          }
        }
      }
    }
  }, {});

export default {
  cCommandInterpreter,
  CommandInterfaces,
  CommandClasses
}








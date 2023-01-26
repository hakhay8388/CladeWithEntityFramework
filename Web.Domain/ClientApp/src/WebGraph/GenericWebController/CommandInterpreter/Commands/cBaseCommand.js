import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject"


var cBaseCommand = Class(cBaseObject, {
	ObjectType: ObjectTypes.Get("cBaseCommand")
	, CommandInterpreter : null
	, Command : null
	, Enabled : true	
	, ReceiverList : null
	, ReceiverInterface : null
	, ReceiverFunctionName : null
	,
	constructor: function (_CommandInterpreter)
	{
		cBaseCommand.BaseObject.constructor.call(this);
		this.CommandInterpreter = _CommandInterpreter;
		this.CommandInterpreter.AddCommand(this);		
		this.ReceiverList = new cListForBase();

	}
	,
	Destroy: function ()
	{
	    delete this.ObjectType;
	    delete this.CommandInterpreter;
	    this.Command.Destroy();
	    delete this.Command;
	    delete this.m_Enabled;
	    this.ReceiverList.Destroy();
	    delete this.ReceiverList;
		cBaseObject.prototype.Destroy.call(this);
	}
	,
  Run: function (_MsgObject)
	{
		var __Count = this.ReceiverList.Count();
		for (var i = 0; i < __Count; i++)
		{
			var __Item = this.ReceiverList.GetItem(i);
      eval("__Item." + this.ReceiverFunctionName + "(_MsgObject)");
		}
 }
  ,
  IsConnect: function (_Receiver) {
    if (this.ReceiverInterface.IsInstance(_Receiver))
    {
      return this.ReceiverList.Find(__Item => __Item == _Receiver);
    }
    else {
      DebugAlert.Show(this.Command.CommandInfo + " Komutuna Bağlanmak İsteniyor Fakat '" + this.ReceiverFunctionName + "' Fonksiyonu Bulunamadı yada Interfaceden Türetilmemiş..!");
    }
  }
	,
	Connect : function(_Receiver)
	{
		if (this.ReceiverInterface.IsInstance(_Receiver))
		{
			this.ReceiverList.Add(_Receiver);
		}
		else
		{
			DebugAlert.Show(this.Command.CommandInfo + " Komutuna Bağlanmak İsteniyor Fakat '" + this.ReceiverFunctionName + "' Fonksiyonu Bulunamadı yada Interfaceden Türetilmemiş..!");
		}
	}
	,
	Disconnect: function (_Receiver)
	{
		this.ReceiverList.Remove(_Receiver);
	}
	
}, {});

export default cBaseCommand







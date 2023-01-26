import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class"
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph"


var cBaseCommandListener = Class(cBaseObject, 
{
  ObjectType: ObjectTypes.Get("cBaseCommandListener")
	, 
	constructor: function ()
	{
		cBaseCommandListener.BaseObject.constructor.call(this);
      GenericWebGraph.CommandInterpreter.ConnectToCommands(this);
	}
	,
	Destroy: function ()
	{
    GenericWebGraph.CommandInterpreter.DisconnectToCommands(this);
		cBaseObject.prototype.Destroy.call(this);
	}
  }, {});

export default cBaseCommandListener








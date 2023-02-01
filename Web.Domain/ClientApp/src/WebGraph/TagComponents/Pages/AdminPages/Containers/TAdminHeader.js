import React from "react";
import { Class, JSTypeOperator, ObjectTypes } from "../../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../../TObject";
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import Actions from "../../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import { withStyles } from "@mui/styles";

import AdminStyles from "../../../../../ScriptStyles/AdminStyles";

import TBaseHeader from "../../TBaseHeader";

var TAdminHeader = Class(TBaseHeader,
  {
      ObjectType: ObjectTypes.Get("TAdminHeader"),
    constructor: function (_Props) {
        TAdminHeader.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state        
      };
    },
    Destroy: function () {
        TAdminHeader.BaseObject.Destroy.call(this);
    },
    AsyncLoad: function () {
        TAdminHeader.BaseObject.AsyncLoad.call(this);
    },
    render() 
    {
      var __This = this;
      const { children, classes, ...attributes } = this.props;
      return (
        <div style={{ width: "100%" }}>
          admin Header
        </div>
      );
    },
  },
  {}
);

export default withStyles(AdminStyles)(TAdminHeader);

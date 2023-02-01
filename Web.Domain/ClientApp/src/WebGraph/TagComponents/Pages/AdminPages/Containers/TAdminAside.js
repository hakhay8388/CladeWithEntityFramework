import React from 'react';
import {Class, JSTypeOperator, ObjectTypes} from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject"
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import { withStyles } from "@mui/styles";
import AsideStyles from "../../../../../ScriptStyles/AsideStyles";
import TBaseAside from "../../TBaseAside";


var TAdminAside = Class(TBaseAside,
  {
      ObjectType: ObjectTypes.Get("TAdminAside")
    ,
    constructor: function (_Props) {
        TAdminAside.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
      };
    }
    ,
    Destroy: function () {
        TAdminAside.BaseObject.Destroy.call(this);
    }
  }, {});
export default withStyles(AsideStyles)(TAdminAside);


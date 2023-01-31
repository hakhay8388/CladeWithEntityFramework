import React from 'react';
import {Class, JSTypeOperator, ObjectTypes} from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../TObject"
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph";
import { withStyles } from "@mui/styles";
import AsideStyles from "../../../../../ScriptStyles/AsideStyles";


var TAdminAside = Class(TObject,
  {
      ObjectType: ObjectTypes.Get("TAdminAside")
    ,
    constructor: function (_Props) {
        TAdminAside.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        GlobalChannel_List: []
      };
      window.App.Aside = this;
    }
    ,
    componentWillUnmount() {
        TAdminAside.BaseObject.componentWillUnmount.call(this);
    }
    ,
    HandleToggle: function (_Tab) {
        if (this.state.activeTab !== _Tab) {
        this.setState({
            activeTab: _Tab,
        });
      }
      window.App.Aside = this;
    }
    ,
    Destroy: function () {
        TAdminAside.BaseObject.Destroy.call(this);
    }
    ,
    AsyncLoad: function (_Force) {
        TAdminAside.BaseObject.AsyncLoad.call(this, _Force);
      this.state.GlobalChannel_List = [];
    }
    ,
    render() {
      const {children, classes, ...attributes} = this.props;

      return (
        <React.Fragment>
          <div className={classes.fragmentStyle}>
            <div style={{marginBlockEnd: "80px"}}>
              <div className={classes.notificationTitle}>{this.state.Language.NotificationGeneralNotices}</div>
              {<div>bildirimler</div>}
            </div>
          </div>
        </React.Fragment>
      );
    }
  }, {});
export default withStyles(AsideStyles)(TAdminAside);


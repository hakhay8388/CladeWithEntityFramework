import React from 'react';
import {Class, JSTypeOperator, ObjectTypes} from "../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../TObject"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import { withStyles } from "@mui/styles";
import AsideStyles from "../../../ScriptStyles/AsideStyles";


var TBaseAside = Class(TObject,
  {
      ObjectType: ObjectTypes.Get("TBaseAside")
    ,
    constructor: function (_Props) {
        TBaseAside.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        GlobalChannel_List: []
      };
        window.App.Aside = this;
        GenericWebGraph.ManagersWithListener.NotificationManager.HandleAddChannelNotificationEvent(this, window.Enums.ENotificationChannels.GlobalChannel, this.HandleGlobalNotificationsFunction);
    }
    ,
      componentWillUnmount() {
          GenericWebGraph.ManagersWithListener.NotificationManager.HandleRemoveChannelNotificationEvent(window.Enums.ENotificationChannels.GlobalChannel, this.HandleGlobalNotificationsFunction);
          TBaseAside.BaseObject.componentWillUnmount.call(this);
      }
      ,
      HandleGetUnreadedCount: function () {
          var __Counter = 0;
          for (var i = 0; i < this.state.GlobalChannel_List.length; i++) {
              if (!this.state.GlobalChannel_List[i].Readed) {
                  __Counter++;
              }
          }
          return __Counter;
      }
      ,
      HandleGlobalNotificationsFunction(_Data) {
          var __TempList = [...this.state.GlobalChannel_List];
          __TempList.splice(0, 0, { ..._Data });

          __TempList = __TempList.unique(function (_Item1, _Item2) {
              return _Item1.NotificationID == _Item2.NotificationID;
          });

          this.setState({
              GlobalChannel_List: __TempList,
          }, () => {
              if (JSTypeOperator.IsFunction(window.App.Header.HandleRefreshNotifications)) {
                  window.App.Header.HandleRefreshNotifications();
              }
          });
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
        TBaseAside.BaseObject.Destroy.call(this);
    }
    ,
    AsyncLoad: function (_Force) {
        TBaseAside.BaseObject.AsyncLoad.call(this, _Force);
        this.state.GlobalChannel_List = [];
        GenericWebGraph.ManagersWithListener.NotificationManager.HandleLoadNotifications();
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
export default withStyles(AsideStyles)(TBaseAside);


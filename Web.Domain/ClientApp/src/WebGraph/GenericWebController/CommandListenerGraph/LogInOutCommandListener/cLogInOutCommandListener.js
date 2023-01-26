import React, { Component } from "react";
import
{
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import cBaseCommandListener from "../cBaseCommandListener";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";
import queryString from "query-string";

var cLogInOutCommandListener = Class(
  cBaseCommandListener,
  CommandInterfaces.ILogInOutCommandReceiver,
  CommandInterfaces.ISetUserOnClientCommandReceiver,
  CommandInterfaces.IForceLogoutCommandReceiver,
  CommandInterfaces.IDoCheckLoginRequestCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("cLogInOutCommandListener"),
    constructor: function ()
    {
      cLogInOutCommandListener.BaseObject.constructor.call(this);

      this.HashChanged = this.HashChanged.bind(this);
      this.HashControlFunction(this.HashChanged);
      Actions.CheckLogin();
    },
    Destroy: function ()
    {
      cBaseCommandListener.prototype.Destroy.call(this);
    },
    HashControlFunction: function (_Function)
    {
      if ("onhashchange" in window)
      {
        window.onhashchange = function ()
        {
          _Function(window.location.hash);
        };
      } else
      {
        var prevHash = window.location.hash;
        window.setInterval(function ()
        {
          if (window.location.hash != prevHash)
          {
            prevHash = window.location.hash;
            _Function(window.location.hash);
          }
        }, 100);
      }
    },
    HashChanged: function ()
    {
      var __AnyAction = false;
      var __Url = window.GetUrlParams();
      var __Params = queryString.parse(__Url);
      if (__Params.lang)
      {
        if (
          GenericWebGraph.Managers.LanguageManager.ActiveLanguage
            .LanguageCode != __Params.lang
        )
        {
          __AnyAction = true;
          GenericWebGraph.Managers.LanguageManager.SetLanguage(
            __Params.lang
          );
        }
      } else
      {
        var __Url = window.GetUrlPage();
        if (window.App.User == null)
        {
          if (
            __Url != window.Pages.Names.LoginPage &&
            __Url != window.Pages.Names.ForgotPassword &&
            __Url != window.Pages.Names.ForgotPasswordConfirm &&
            __Url != window.Pages.Names.RegisterPage &&
            __Url != window.Pages.Names.SellerRegisterPage &&
            __Url != window.Pages.Names.MainPage
          )
          {
            __AnyAction = true;
            window.App.User = null;
            window.App.SessionID = null;
            GenericWebGraph.GoPage(window.Pages.Names.MainPage);
          }
        } else
        {
          if (
            __Url == window.Pages.Names.LoginPage ||
            __Url == window.Pages.Names.ForgotPassword ||
            __Url == window.Pages.Names.ForgotPasswordConfirm ||
            __Url == window.Pages.Names.RegisterPage ||
            __Url == window.Pages.Names.SellerRegisterPage ||
            __Url == window.Pages.Names.MainPage
          )
          {
            __AnyAction = true;
            GenericWebGraph.GoMainPage();
          }
        }
      }
      if (!__AnyAction)
      {
        WebGraph.ForceUpdateAllWithAsyncLoad(true);
      }
    },
    Receive_SetUserOnClientCommand: function (_Data)
    {
      window.App.User = _Data.User;
    }
    ,
    Receive_DoCheckLoginRequestCommand: function (_Data)
    {
      if ((_Data.IsLogined && window.App.User == null) || (!_Data.IsLogined && window.App.User != null))
      {
        setTimeout(function ()
        {
          Actions.CheckLogin();
        }, 500);
      }
    }
    ,
    Receive_LogInOutCommand: function (_Data)
    {
      window.App.Checked = true;
      var __Url = window.GetUrl();
      var __UrlParams = window.GetUrlParams();
      if (_Data.LoginState && window.App.User == null)
      {
        GenericWebGraph.CloseAllModals();
        DebugAlert.Show("Logined");
        GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();

        window.ClearPages();
        window.App.User = _Data.User;
        window.App.SessionID = _Data.SessionID;
        GenericWebGraph.MainPage = null;

        GenericWebGraph.Managers.LanguageManager.SetLanguage(
          window.App.User.Language
        );

        if (__Url == "" || __Url == "/")
        {
          GenericWebGraph.GoMainPage();
        } else
        {
          GenericWebGraph.GoPage(__Url + __UrlParams);
        }
      } else if (_Data.LoginState)
      {
        DebugAlert.Show("Logined");
        GenericWebGraph.CloseAllModals();
        GenericWebGraph.ManagersWithListener.SignalListerner.HandleConnect();

        window.ClearPages();
        window.App.User = _Data.User;
        window.App.SessionID = _Data.SessionID;
        GenericWebGraph.MainPage = null;
        GenericWebGraph.Managers.LanguageManager.SetLanguage(
          window.App.User.Language
        );

        if (__Url == "" || __Url == "/")
        {
          GenericWebGraph.GoMainPage();
        } else
        {
          GenericWebGraph.GoPage(__Url + __UrlParams);
        }
      }
      /*else if (window.App.User != null)
      {
        window.location.href = "./";
      }*/
      else
      {
        GenericWebGraph.CloseAllModals();
        GenericWebGraph.CloseAllHotSpotMessage();
        if (
          window.App &&
          window.App.Header &&
          window.App.Header != null &&
          JSTypeOperator.IsFunction(window.App.Header.HandleLeftMenuOpenForce)
        )
        {
          window.App.Header.HandleLeftMenuOpenForce(false);
        }

        window.ClearPages();
        GenericWebGraph.MainPage = null;
        window.App.User = null;
        window.App.SessionID = null;
        GenericWebGraph.ManagersWithListener.SignalListerner.HandleDisconnect();

        if (__Url == "" || __Url == "/")
        {
          GenericWebGraph.GoMainPage();
        } else
        {
          GenericWebGraph.GoPage(__Url + __UrlParams);
        }
      }
    },
    Receive_ForceLogoutCommand: function (_Data)
    {
      window.App.Checked = true;
      var __This = this;
      GenericWebGraph.CloseAllModals();
      GenericWebGraph.CloseAllHotSpotMessage();
      if (
        window.App &&
        window.App.Header &&
        window.App.Header != null &&
        JSTypeOperator.IsFunction(window.App.Header.HandleLeftMenuOpenForce)
      )
      {
        window.App.Header.HandleLeftMenuOpenForce(false);
      }

      window.ClearPages();
      GenericWebGraph.MainPage = null;
      window.App.User = null;
      window.App.SessionID = null;
      GenericWebGraph.SignalListerner.HandleDisconnect();
      GenericWebGraph.SetCookie({
        name: "GenericWebScaffoldSessionID",
        value: "",
        seconds: 1,
      });

      var _ParamObject = {
        Text1: GenericWebGraph.Managers.LanguageManager.ActiveLanguage.Session,
        Text2: GenericWebGraph.Managers.LanguageManager.ActiveLanguage.SecondShort,
        Text3: GenericWebGraph.Managers.LanguageManager.ActiveLanguage.WillBeClosed,
        RemainingTime: 5,
      };

      window.App.CountDownSpinner.HandleClickOpen(
        _ParamObject,
        function (_Refresh)
        {
          GenericWebGraph.CloseAllModals();
          GenericWebGraph.GoMainPage();
        }
      );
    },
  },
  {}
);

export default cLogInOutCommandListener;

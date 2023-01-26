import React, { Component } from "react";
import { Redirect } from "react-router-dom";
import {  DebugAlert,  Class,  Interface,  Abstract,  ObjectTypes,  JSTypeOperator } from "../GenericCoreGraph/ClassFramework/Class";
import cBaseObject from "../GenericCoreGraph/BaseObject/cBaseObject";
import { WebGraph } from "../GenericCoreGraph/WebGraph/WebGraph";
import cDelegate from "../GenericCoreGraph/Delegate/cDelegate";
import { CommandInterfaces } from "../GenericWebController/CommandInterpreter/cCommandInterpreter";
import GenericWebGraph from "../../WebGraph/GenericWebController/GenericWebGraph";
import queryString from "query-string";
import Grid from "@mui/material/Grid";
import CircularProgress from "@mui/material/CircularProgress";
import { Breadcrumbs, Link, Typography } from "@mui/material";
import DefaultTheme from "../../Themes/DefaultTheme";

var TObject = Class(
  cBaseObject,
  CommandInterfaces.ISetStateCommandReceiver,
  CommandInterfaces.ISetVariableCommandReceiver,
  CommandInterfaces.IAsyncLoadCommandReceiver,
  CommandInterfaces.IForceUpdateCommandReceiver,
  {
    ObjectType: ObjectTypes.Get("TObject"),
    constructor: function (_Props) {
      TObject.BaseObject.constructor.call(this, _Props);
      this.keyCount = 0;
      this.getKey = this.getKey.bind(this);
      GenericWebGraph.CommandInterpreter.ConnectToCommands(this);
      this.AsyncLoad = this.AsyncLoad.bind(this);
      this.OnUrlPop = this.OnUrlPop.bind(this);
      this.OnUrlChanged = this.OnUrlChanged.bind(this);
      this.OnHistoryChanged = this.OnHistoryChanged.bind(this);

      this.OnSmDown = new cDelegate(ObjectTypes.TObject, false);
      this.OnSmUp = new cDelegate(ObjectTypes.TObject, false);
      this.OnMdDown = new cDelegate(ObjectTypes.TObject, false);
      this.OnMdUp = new cDelegate(ObjectTypes.TObject, false);
      this.OnLgDown = new cDelegate(ObjectTypes.TObject, false);
      this.OnLgUp = new cDelegate(ObjectTypes.TObject, false);
      this.OnXlDown = new cDelegate(ObjectTypes.TObject, false);
      this.OnXlUp = new cDelegate(ObjectTypes.TObject, false);
      this.NeedUpdate = true;
      this.RenderCount = 0;

      var __Params = {};

      var __Url = window.GetUrlParams();
      var __Params = queryString.parse(__Url);
      __Params.Path = window.GetUrl();

      if (!this.state) {
        this.state = {
          Version: new Date().getTime(),
          UrlParams: __Params,
          IsXs: GenericWebGraph.MainContainerSize.Width < 600,
          Language:
            GenericWebGraph.Managers &&
            GenericWebGraph.Managers.LanguageManager &&
            GenericWebGraph.Managers.LanguageManager.ActiveLanguage
              ? GenericWebGraph.Managers.LanguageManager.ActiveLanguage
              : {},
        };
      } else {
        this.state = {
          ...this.state,
          Version: new Date().getTime(),
          UrlParams: __Params,
          IsXs: GenericWebGraph.MainContainerSize.Width < 600,
          Language:
            GenericWebGraph.Managers &&
            GenericWebGraph.Managers.LanguageManager &&
            GenericWebGraph.Managers.LanguageManager.ActiveLanguage
              ? GenericWebGraph.Managers.LanguageManager.ActiveLanguage
              : {},
        };
      }
      
      this.HistoryListener = window.History.listen(this.OnHistoryChanged);
    },
    OnHistoryChanged: function (_Location, _Action) {
      var __This = this;
      var __Url = window.GetUrlParams();
      var __Params = queryString.parse(__Url);
      __Params.Path = window.GetUrl();

      if (__This.state.UrlParams?.Path != __Params.Path)
      {
        setTimeout(function ()
        {
          __This.AsyncLoad();
        });
      }
      __This.state.UrlParams = __Params;
    },
    getKey() {
      return this.keyCount++;
    },
    componentWillUpdate(_NextProps, _NextState) {},
    /*componentWillUpdate: function ()
    {
      var __Params = {}

      var __Url = window.GetUrlParams();
      var __Params = queryString.parse(__Url);
      __Params.Path = window.GetUrl()

      this.state.UrlParams = __Params
    }
    ,*/
    OnUrlChanged: function () {},
    OnUrlPop: function () {},
    shouldComponentUpdate(_NextProps, _NextState) {
      return this.NeedUpdate;
    },
    componentDidMount: function () {
        TObject.BaseObject.componentDidMount.call(this);

        this.OnSmDown.AddIfNotExists(this, this.HandleOnSmDown);
        this.OnSmUp.AddIfNotExists(this, this.HandleOnSmUp);

      this.AsyncLoad();
    },
    AsyncLoad: function () {
      var __Params = {};
      var __Url = window.GetUrlParams();
      __Params = queryString.parse(__Url);
      __Params.Path = window.GetUrl();

      this.setState({
        UrlParams: __Params,
        Language: GenericWebGraph.Managers.LanguageManager.ActiveLanguage,
      });
        },
      HandleOnSignalStatusChanged: function (_Size)
      {
      }
    ,
    HandleOnResizeMain: function (_Size) {
      if (_Size.Width < 600) {
        this.OnSmDown.Run(_Size);
      }
      if (_Size.Width >= 600) {
        this.OnSmUp.Run(_Size);
      }

      if (_Size.Width < 960) {
        this.OnMdDown.Run(_Size);
      }
      if (_Size.Width >= 960) {
        this.OnMdUp.Run(_Size);
      }

      if (_Size.Width < 1280) {
        this.OnLgDown.Run(_Size);
      }
      if (_Size.Width >= 1280) {
        this.OnLgUp.Run(_Size);
      }

      if (_Size.Width < 1920) {
        this.OnXlDown.Run(_Size);
      }
      if (_Size.Width >= 1920) {
        this.OnXlUp.Run(_Size);
      }
        },

      HandleOnSmDown: function (_Size) {
          this.setState({
              IsXs: true,
          });
      }
      ,
      HandleOnSmUp: function (_Size) {
          this.setState({
              IsXs: false,
          });
      }
    ,
      componentWillMount: function () {
      TObject.BaseObject.componentWillMount.call(this);
          GenericWebGraph.AddResizeEvent(this.HandleOnResizeMain);
        GenericWebGraph.ManagersWithListener.SignalListerner.HandleAddSignalEvent(this.HandleOnSignalStatusChanged);
      //GenericWebGraph.Managers.KeyboardManager.ConnectKeypress(this);
    },

    Destroy: function () {
      TObject.BaseObject.Destroy.call(this);
    },
    componentWillUnmount() {
      GenericWebGraph.CommandInterpreter.DisconnectToCommands(this);
      TObject.BaseObject.componentWillUnmount.call(this);
        this.HistoryListener();
        GenericWebGraph.RemoveResizeEvent(this.HandleOnResizeMain);
        this.OnSmDown.Remove(this, this.HandleOnSmDown);
        this.OnSmUp.Remove(this, this.HandleOnSmUp);
        GenericWebGraph.ManagersWithListener.SignalListerner.HandleRemoveSignalEvent(this.HandleOnSignalStatusChanged);
      //GenericWebGraph.Managers.KeyboardManager.DisconnectKeypress(this);
    },
    Receive_ForceUpdateCommand: function (_Data) {
      if (_Data.ObjectTypeName == this.GetObjectType().ObjectName) {
        this.forceUpdate();
      }
    },
    Receive_AsyncLoadCommand: function (_Data) {
      if (_Data.ObjectTypeName == this.GetObjectType().ObjectName) {
        this.AsyncLoad();
      }
    },
    Receive_SetStateCommand: function (_Data) {
      if (_Data.ObjectTypeName == this.GetObjectType().ObjectName) {
        this.setState({
          [_Data.Name]: _Data.Value,
        });
      }
    },
    Receive_SetVariableCommand: function (_Data) {
      if (_Data.ObjectTypeName == this.GetObjectType().ObjectName) {
        this[_Data.Name] = _Data.Value;
        if (_Data.ForceUpdate) this.forceUpdate();
      }
    },
    Handle_AClick: function (_Event, _GoPageUrl) {
      if (_Event != null && _Event != undefined) {
        _Event.preventDefault();
        GenericWebGraph.GoPage(_GoPageUrl);
      }
    },

    Handle_GetAppBreadcrumbDetails: function (_BreakCrumbDict) {
      var __This = this;
      var __NewKeyValueList = [];
      for (const [key, value] of Object.entries(_BreakCrumbDict)) {
        __NewKeyValueList.push({
          Key: key,
          Value: value,
        });
      }
      return __NewKeyValueList.map((__Item, __Index) => {
        return __Index + 1 === __NewKeyValueList.length ? (
          <Typography color="text.primary">{__Item.Key}</Typography>
        ) : (
          <Link
            underline={"none"}
            style={{color: DefaultTheme.palette.dark.main, textDecoration: "none"}}
            href={__Item.Value}
            onClick={(event) => __This.Handle_AClick(event, __Item.Value)}
            onMouseEnter={(__Event)=>{__Event.target.style.color = DefaultTheme.palette.primary.main}}
            onMouseOut={(__Event)=>{__Event.target.style.color = DefaultTheme.palette.dark.main}}
          >
            <Typography>{__Item.Key}</Typography>
          </Link>
        );
      });
    },
    HandleLoading: function () {
      //return < div className="animated fadeIn pt-1 text-center" >{this.state.Language.Loading}</div>
      // return <div className="animated fadeIn pt-3 text-center"><h2>{this.state.Language.Loading} {' '}<Spinner color="primary" /></h2></div>
      return (
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="center"
          style={{ minHeight: "30vh" }}
        >
          <Grid item>
            <h4 style={{ color: "#969696", fontFamily: "Montserrat" }}>
              {GenericWebGraph.Managers.LanguageManager.ActiveLanguage.Loading}{" "}
            </h4>
          </Grid>
          <Grid item style={{ paddingLeft: 10 }}>
            <CircularProgress />
          </Grid>
        </Grid>
      );

      /*
            <Grid
              container
              direction="row"
              justifyContent="center"
              alignItems="center"
              style={{ minHeight: '30vh' }}
            >
              <Grid item>
                <h4 style={{ color: "#969696" }}>
                  {GenericWebGraph.Managers.LanguageManager.ActiveLanguage.Loading}{" "}
                </h4>
              </Grid>
              <Grid item style={{ paddingLeft: 10 }}>
                <CircularProgress  />
              </Grid>

            </Grid>
            */
    },
    componentDidUpdate(_PrevProps, _PrevState) {
      this.RenderCount++;
      if (this.props.location && this.props.location.search) {
        var __This = this;
        var __Url = window.GetUrlParams();
        var __Params = queryString.parse(__Url);
        var __NeedUpdate = false;
        for (var __Item in __Params) {
          if (__This.state.UrlParams[__Item] != __Params[__Item]) {
            __NeedUpdate = true;
            break;
          }
        }
        if (__NeedUpdate) {
          setTimeout(function () {
            __This.AsyncLoad();
          });
        }
      }
    },
  },
  {}
);

export default TObject;

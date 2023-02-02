import React, { Component } from 'react';


import GenericWebGraph from "../../GenericWebController/GenericWebGraph"
//import { CommandInterfaces } from "../GenericWebController/CommandInterpreter/cCommandInterpreter"
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../GenericCoreGraph/ClassFramework/Class"
import cBaseObject from "../../GenericCoreGraph/BaseObject/cBaseObject";

//import TObject from "../../WebGraph/TagComponents/TObject"
//import Actions from "../../WebGraph/GenericWebController/ActionGraph/Actions"
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"
import { ActionIDs } from "../../GenericWebController/ActionGraph/ActionIDs/ActionIDs";
//import { WebGraph } from "../../WebGraph/GenericCoreGraph/WebGraph/WebGraph"
//import cCommandListenerGraph from "../../WebGraph/GenericWebController/CommandListenerGraph/cCommandListenerGraph"
//import cManagersWithListener from "../../WebGraph/GenericWebController/ManagersWithListener/cManagersWithListener"



import cManagers from "../../GenericWebController/Managers/cManagers";
import { cCommandInterpreter } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import cActionGraph from "../../GenericWebController/ActionGraph/cActionGraph";
import Pages from "../../TagComponents/Pages/Pages";



//const cGlobalParamsManager = React.lazy(() => import("../GenericWebController/ManagersWithListener/GlobalParamsManager/cGlobalParamsManager"));

const TMessageBox = React.lazy(() => import("../Listeners/TMessageBox"));
const THotSpotMessage = React.lazy(() => import("../Listeners/THotSpotMessage"));





const TGlobalLoading = React.lazy(() => import("../Utilities/TGlobalLoading"));



var TDynamicLoader = Class(cBaseObject, //TObject,
    {
        ObjectType: ObjectTypes.Get("TDynamicLoader")
        ,
        constructor: function (_Props) {
            TDynamicLoader.BaseObject.constructor.call(this, _Props);
            this.state = {
                innerChilds: <div className="container">
                    <div className="center">
                        <div className="lds-ripple"><div></div><div></div></div>
                    </div>
                </div>,
            }
            this.FirstInitData = null;
            this.IndependentChilds = <div></div>;
            window.App.DynamicLoader = this;
            this.InitFirstLoad();
        }
        ,
        InitFirstLoad: function () {
            var __LanguageCode = window.GetLanguageCodeFromUrl();
            if (__LanguageCode.length != 2) {
                __LanguageCode = "";
            }

            var __This = this;
            fetch('/webgraph/webapi', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    CID: 1,
                    Data: {
                        LanguageCode: __LanguageCode
                    }
                })
            })
                .then(response => response.json())
                .then(response => {
                    __This.FirstInitData = response;
                    var __CommandList = __This.GetCommandByNameInCommandArray(response, "CommandList");
                    CommandIDs.LoadCommands(__CommandList.Data.CommandList);

                    var __ActionList = __This.GetCommandByNameInCommandArray(response, "ActionList");
                    ActionIDs.LoadActions(__ActionList.Data.ActionList);

                    GenericWebGraph.Managers = new cManagers();

                    var __Language = __This.GetCommandByNameInCommandArray(response, "Language");
                    GenericWebGraph.Managers.LanguageManager.HandleSetActiveLanguage(__Language.Data);

                    GenericWebGraph.CommandInterpreter = new cCommandInterpreter();
                    GenericWebGraph.ActionGraph = new cActionGraph();

                    var __PageResult = __This.GetCommandByNameInCommandArray(response, "PageResult");
                    Pages.LoadPages(__PageResult.Data);

                    var __SetUserOnClient = __This.GetCommandByNameInCommandArray(response, "SetUserOnClient");

                    window.GenericWebGraph = GenericWebGraph;

                    GenericWebGraph.Init(function () {
                        GenericWebGraph.CommandInterpreter.InterpretCommand([__SetUserOnClient]);
                        __This.IndependentChilds = __This.HandleGetIndependentChilds();
                        __This.HandleSetChilds();

                    });

                }).catch(err => {
                    DebugAlert.Show("hata", err);
                });

        }
        ,
        HandleGetIndependentChilds: function () {
            return (<div>
                <TMessageBox />
                <THotSpotMessage />
                <TGlobalLoading />
            </div>
          );
      }
        ,
      HandleSetChilds: function ()
      {
          var __This = this;
          this.setState({
              innerChilds: <div>
                  {__This.props.getInnerChilds()}
              </div>
          });
      }
      ,
      HandleRefresh: function () {
          var __This = this;
          Pages.ReloadPages(function ()
          {
              __This.HandleSetChilds();
          });
      }
        ,
      GetCommandByNameInCommandArray: function (_CommandArray, _CommandName)
      {
          for (var i = 0; i < _CommandArray.length; i++)
          {
              if (_CommandArray[i].ActionID.Code === _CommandName)
              {
                  return _CommandArray[i];
              }
          }
      }
    ,
    Destroy: function ()
    {
      TDynamicLoader.BaseObject.Destroy.call(this);
    }
    ,
    render()
    {
        return (<div>
            {this.IndependentChilds}
            {this.state.innerChilds }
        </div>
            
      );
    }
  }, {});

export default TDynamicLoader;

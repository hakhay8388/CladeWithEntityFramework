import React, { Component } from 'react';
import { JSTypeOperator, cObjType, DebugAlert, Class, Interface, Abstract, ObjectTypes, GlobalEval } from "../../../GenericCoreGraph/ClassFramework/Class";
import CID from "./CID";
import GenericWebGraph from "../../GenericWebGraph";
import Actions from "../../ActionGraph/Actions";


var cAction = Class(Component,
  {
    ActionCreater: null
    , ActionProps: null
    ,
    constructor: function (_ActionCreater, _JsonableObject, _ResultFunction, _ActionConfigurationOptions)
    {
      this.ActionCreater = _ActionCreater;

      this.ActionProps =
      {
        JsonableObject: (_JsonableObject ? _JsonableObject : null),
        ResultFunction: this.GetResultFunction(_ResultFunction),
        Options: this.GetOptions(_ResultFunction, _ActionConfigurationOptions),
        RequestAction: new CID(this.ActionCreater.ActionID.ID, (_JsonableObject ? _JsonableObject : null), window.App.SessionID)
      }

      this.HandleControlInCache();
    }
    ,
    GetResultFunction: function (_ResultFunction)
    {
      if (JSTypeOperator.IsDefined(_ResultFunction)
        && JSTypeOperator.IsFunction(_ResultFunction)
      )
      {
        return _ResultFunction;
      }
      return null;
    }
    ,
    GetOptions: function (_ResultFunction, _ActionConfigurationOptions)
    {
      if (!JSTypeOperator.IsDefined(_ActionConfigurationOptions)
        && JSTypeOperator.IsDefined(_ResultFunction)
        && !JSTypeOperator.IsFunction(_ResultFunction)
        && JSTypeOperator.IsObject(_ResultFunction))
      {
        return _ResultFunction;
      }
      else if (JSTypeOperator.IsDefined(_ActionConfigurationOptions)
        && JSTypeOperator.IsObject(_ActionConfigurationOptions))
      {
        return _ActionConfigurationOptions;
      }
      return null;
    }
    ,
    HandleControlInCache: function ()
    {
      if (GenericWebGraph.CommandListenerGraph)
      {
        var __Result = GenericWebGraph.CommandListenerGraph.CacheCommandListener.HandleCacheControl(this.ActionProps);
        if (__Result != null)
        {
          try
          {
            __Result.ActionProps = this.ActionProps;
            if (JSTypeOperator.IsFunction(this.ActionProps.ResultFunction))
            {

              var __ResultValue = this.ActionProps.ResultFunction(__Result);
              if (!JSTypeOperator.IsDefined(__ResultValue) || __ResultValue)
              {
                try
                {
                  GenericWebGraph.CommandInterpreter.InterpretCommand(__Result);
                }
                catch (_Ex)
                {
                  DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
                }
              }
            }
            else
            {
              try
              {
                GenericWebGraph.CommandInterpreter.InterpretCommand(__Result);
              }
              catch (_Ex)
              {
                DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
              }
            }
          }
          catch (_Ex)
          {
            DebugAlert.Show("Action sonrası çalışacak fonksiyonda sorun var!", _Ex);
          }
        }
        else
        {
          this.ActionCreater.ActionGraph.AddAction(this);
        }
      }
      else
      {
        this.ActionCreater.ActionGraph.AddAction(this);
      }
    }
    ,
    Destroy: function ()
    {
      delete this.ActionProps;
      delete this.ActionCreater;
    }
    ,
    Action: function ()
    {
      var __This = this;
      try
      {
        if (this.ActionProps.JsonableObject != null)
        {
          if (this.ActionProps.Options != null && JSTypeOperator.IsDefined(this.ActionProps.Options.ShowLoading))
          {
            if (JSTypeOperator.IsDefined(this.ActionProps.Options.LoadingText))
            {
              window.App.GlobalLoading.Show(this.ActionProps.Options.LoadingText);
            }
            else
            {
              window.App.GlobalLoading.Show();
            }
          }

          if (window.App.GlobalLoading != null && Actions.IsLoadingEnabled) window.App.GlobalLoading.Show();
          //var __CID = new CID( this.ActionCreater.ActionID.ID, this.JsonableObject, window.App.SessionID );
          //var __This = this;

          if (this.ActionProps.Options != null && JSTypeOperator.IsDefined(this.ActionProps.Options.SendOnSignal) && this.ActionProps.Options.SendOnSignal)
          {
            GenericWebGraph.ManagersWithListener.SignalListerner.HandleSendAction(JSON.stringify(__This.ActionProps.RequestAction));
            __This.Destroy();
          }
          else
          {
              fetch('/webgraph/webapi', {
              method: 'POST',
              headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
              },
              body: JSON.stringify(__This.ActionProps.RequestAction)
            })
              .then(response => response.json())
              .then(response =>
              {
                try
                {
                  response.ActionProps = __This.ActionProps;
                  if (JSTypeOperator.IsFunction(__This.ActionProps.ResultFunction))
                  {

                    var __ResultValue = __This.ActionProps.ResultFunction(response);
                    if (!JSTypeOperator.IsDefined(__ResultValue) || __ResultValue)
                    {
                      try
                      {
                        GenericWebGraph.CommandInterpreter.InterpretCommand(response);
                      }
                      catch (_Ex)
                      {
                        DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
                      }
                    }
                  }
                  else
                  {
                    try
                    {
                      GenericWebGraph.CommandInterpreter.InterpretCommand(response);
                    }
                    catch (_Ex)
                    {
                      DebugAlert.Show("CommandInterpreter'a data gelmedi..!", _Ex);
                    }
                  }
                }
                catch (_Ex)
                {
                  DebugAlert.Show("Action sonrası çalışacak fonksiyonda sorun var!", _Ex);
                }
                if (__This.ActionProps.Options != null && JSTypeOperator.IsDefined(__This.ActionProps.Options.ShowLoading))
                {
                  window.App.GlobalLoading.Hide();
                }
                else if (window.App.GlobalLoading != null && Actions.IsLoadingEnabled)
                {
                  window.App.GlobalLoading.Hide();
                }
                __This.Destroy();
              }).catch(err =>
              {
                if (__This.ActionProps.Options != null && JSTypeOperator.IsDefined(__This.ActionProps.Options.ShowLoading))
                {
                  window.App.GlobalLoading.Hide();
                }
                else if (window.App.GlobalLoading != null && Actions.IsLoadingEnabled)
                {
                  window.App.GlobalLoading.Hide();
                }
                DebugAlert.Show("hata", err);
                __This.Destroy();
              });
          }
        }
        else
        {
          DebugAlert.Show(this.ActionCreater.ActionID.ID + " Action da Boş Data Gönderilmeye Çalışılıyor..!");
        }
      }
      catch (_Ex)
      {
        DebugAlert.Show(this.ActionCreater.ActionID.ID + " Action da hata var..!", _Ex);
      }
    }

  }, {});

export default cAction;








import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../ClassFramework/Class"
import { WebGraph } from "../WebGraph/WebGraph"


var cBaseObject = Class(Component, {

  CreateID: 0
  , CID: 0
  , ObjectType: ObjectTypes.cBaseObject
  ,
  constructor: function (_Props)
  {
    cBaseObject.BaseObject.constructor.call(this, _Props);
    this.CreateID = WebGraph.GetNewCreateID();
    this.CID = parseInt(_Props ? (_Props.cid ? _Props.cid : -1) : -1);
    //DebugAlert.Show(this.ObjectType.ObjectName);


    //Burayı Class oluşturma ortamının içine taşındı
    // bir sorun çıkarsa ora kapatılıp burası açılacak
    // ilgili yer ClassFramework içinde "GetBinderConstructor" metodu
    /*for (var __Function in this)
    {
      if (typeof this[__Function] === "function")
      {
        if (__Function.startsWith("Handle"))
        {
          this[__Function] = this[__Function].bind(this);
        }
      }
    }*/
  }
  ,
  OnComponentLoadedInner(_Type, _Function, _Count)
  {
    if (JSTypeOperator.IsFunction(_Function))
    {
      var __Component = WebGraph.GetItemsByType(_Type);
      if (__Component.Count() > 0)
      {
        _Function(__Component.GetItem(0));
      }
      else
      {
        var __This = this;
        if (_Count < 1200)
        {
          setTimeout(function ()
          {
            __This.OnComponentLoadedInner(_Type, _Function, _Count + 1);
          }, 100);
        }
      }
    }
  }
  ,
  OnComponentLoaded(_Type, _Function)
  {
    this.OnComponentLoadedInner(_Type, _Function, 0);
  }
  ,
  OnComponentUnloadedInner(_Type, _Function, _Count) {
    if (JSTypeOperator.IsFunction(_Function))
    {
      var __Component = WebGraph.GetItemsByType(_Type);
      if (__Component.Count() < 1)
      {
        _Function();
      }
      else
      {
        var __This = this;
        if (_Count < 1200)
        {
          setTimeout(function () {
            __This.OnComponentUnloadedInner(_Type, _Function, _Count + 1);
          }, 100);
        }
      }
    }
  }
  ,
  OnComponentUnloaded(_Type, _Function)
  {
    this.OnComponentUnloadedInner(_Type, _Function, 0);
  }
  ,
  GetObjectType: function ()
  {
    return this.ObjectType;
  }
  ,
  GetTypeID: function ()
  {
    return this.ObjectType.ObjectTypeID;
  },
  GetCreateID: function ()
  {
    return this.CreateID;
  },
  ToString: function ()
  {
    return this.ObjectType.ObjectName;
  }
  ,
  componentDidMount: function ()
  {
    WebGraph.Add(this);
  }
  ,
  componentWillUnmount()
  {
    this.Destroy();
  }
  ,
  componentWillMount: function ()
  {
  }
  ,
  Destroy: function ()
  {
    WebGraph.Remove(this);
    delete this.CreateID;
    delete this.ObjectType;
  }
}, {});

export default cBaseObject




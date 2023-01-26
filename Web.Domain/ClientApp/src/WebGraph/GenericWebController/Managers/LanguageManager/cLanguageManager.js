import React, { Component } from "react";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph";
import cBaseObject from "../../../GenericCoreGraph/BaseObject/cBaseObject";
//import $ from "jquery";
import queryString from "query-string";

var cLanguageManager = Class(
  cBaseObject,
  {
    ObjectType: ObjectTypes.Get("cLanguageManager"),
    constructor: function () {
      cLanguageManager.BaseObject.constructor.call(this);
      this.SetLanguage = this.SetLanguage.bind(this);

    },
    HandleSetActiveLanguage: function (_Language) {
      this.ActiveLanguage = {};
      this.DefinedLanguages = {}; 
      this.ActiveLanguage.LanguageCode = _Language.LanguageCode;
      this.DefinedLanguages = _Language.DefinedLanguages;

      for (var __Item in _Language.Language) {
        this.ActiveLanguage[__Item] = _Language.Language[__Item].message;
      }
    },
    SetLanguage(_LanguageCode) {
      if (this.ActiveLanguage.LanguageCode != _LanguageCode) {
        var __Host = window.GetHost();
        var __Result = "";
        if ((window.GetUrl() + window.GetUrlParams()).left(3).right(1) == "/") {
          var __UrlLength = (window.GetUrl() + window.GetUrlParams()).length;
          __Result =
            _LanguageCode +
            "/" +
            (window.GetUrl() + window.GetUrlParams()).right(__UrlLength - 3);
        } else {
          var __UrlLength = (window.GetUrl() + window.GetUrlParams()).length;
          __Result =
            _LanguageCode + "/" + (window.GetUrl() + window.GetUrlParams());
        }
        window.location.href = window.GetHostHttp() + __Host + "/" + __Result;
      }
    },
    FirstLoadSetLanguage(_LanguageCode) {
    
    },
    Destroy: function () {
      cLanguageManager.BaseObject.Destroy.call(this);
    },
  },
  {}
);

export default cLanguageManager;

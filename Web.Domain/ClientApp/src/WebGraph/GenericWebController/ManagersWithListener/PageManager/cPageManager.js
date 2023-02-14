import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../GenericCoreGraph/ClassFramework/Class"
import cBaseManagersWithListener from "../cBaseManagersWithListener";
import GenericWebGraph from "../../GenericWebGraph"
import { WebGraph } from "../../../GenericCoreGraph/WebGraph/WebGraph"
import {
    CommandInterfaces
} from "../../CommandInterpreter/cCommandInterpreter"
import Actions from "../../ActionGraph/Actions"
import { CommandIDs } from "../../CommandInterpreter/CommandIDs/CommandIDs"
import queryString from 'query-string';


var cPageListener = Class(cBaseManagersWithListener
    , CommandInterfaces.IPageResultCommandReceiver
    ,
    {
        ObjectType: ObjectTypes.Get("cPageListener")
        ,
        constructor: function () {
            cPageListener.BaseObject.constructor.call(this);
            this.Routes = [];
        }
        ,
        Destroy: function () {
            cPageListener.BaseObject.Destroy.call(this);
        }
        ,
        HandleLoadPages: function (_Pages) {

            var __This = this;
            window.Pages = {};
            this.Routes = [];

            _Pages.PagesItems.map(function (_Item, _Index) {

                window.Pages[_Item.Name] = _Item;

                __This.Routes.push({
                    Path: "/" + __This.HandleGetRouteString(_Item, true),
                    PurePath: "/" + _Item.Path,
                    IsMainPage: _Item.IsMainPage,
                    //Name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[_Item.Name],
                    Name: _Item.Name,
                    Component: React.lazy(() => import("../../../TagComponents/Pages/GlobalPages/" + _Item.Component)
                        .catch(
                            () => import("../../../TagComponents/Pages/AdminPages/" + _Item.Component)
                                .catch(
                                    () => import("../../../TagComponents/Pages/UserPages/" + _Item.Component)
                                        .catch(
                                            () => import("../../../TagComponents/Pages/UnloginedPages/" + _Item.Component)
                                        )
                                )
                        )
                    ),
                    Exact: _Item.SubParamName.length === 0
                });

            });
        }
        ,
        HandleGetRouteString: function (_Item, _UsePageName)
        {
            var __Result = "";
            if (_UsePageName) {
                __Result = _Item.Path;
                if (__Result.startsWith("/")) {
                    __Result = __Result.substring(1);
                }
            }

            if (_Item.SubParamName && JSTypeOperator.IsArray(_Item.SubParamName)) {
                for (var i = 0; i < _Item.SubParamName.length; i++) {
                    __Result += "/:" + _Item.SubParamName;
                }
                return __Result;
            }
            return __Result;
        },
        HandleGetRoutePureName: function (_Item)
        {
            var __Result = "";
            __Result = _Item.Path;
            if (__Result.startsWith("/")) {
                __Result = __Result.substring(1);
            }
            return __Result;
        }
        ,
        HandleIsPageExists: function (_PageName, _ControlOnlyRootPage)
        {
            if (_PageName == "/" || _PageName == "") {
                return true;
            }

            if (!_PageName.startsWith("/")) {
                _PageName = "/" + _PageName;
            }

            if (_ControlOnlyRootPage) {
                var __PageNameList = _PageName.split("/");
                _PageName = "/" + __PageNameList[1];
            }
            var __Found = this.Routes.find((__Item) => __Item.PurePath == _PageName);
            if (__Found == undefined) {
                if (__PageNameList.length > 2 && __PageNameList[1].length == 2) {
                    _PageName = "/" + __PageNameList[2];
                }
                __Found = this.Routes.find((__Item) => __Item.PurePath == _PageName);
            }
            return JSTypeOperator.IsDefined(__Found) && __Found;
        }
        ,
        Receive_PageResultCommand: function (_Data)
        {
            this.HandleLoadPages(_Data);
        }
    }, {});

export default cPageListener








import React from "react";

import GenericWebGraph from "../../../WebGraph/GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  Class,
    ObjectTypes,
    JSTypeOperator
} from "../../../WebGraph/GenericCoreGraph/ClassFramework/Class";
import Actions from "../../../WebGraph/GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";


const Pages = {};
window.Pages = Pages;
Pages.Routes = [];

Pages.GetRouteString = function (_Item, _UsePageName) {
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
};

Pages.GetRoutePureName = function (_Item)
{
    var __Result = "";
    __Result = _Item.Path;
    if (__Result.startsWith("/")) {
        __Result = __Result.substring(1);
    }
    return __Result;
};

Pages.LoadPages = function (_Pages, _CallbackFunction) {

    _Pages.PagesItems.map(function (_Item, _Index) {

        Pages.Routes.push({
            path: "/" + Pages.GetRouteString(_Item, true),
            purepath: "/" + _Item.Path,
            name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[_Item.Name],
            component: React.lazy(() => import("./GlobalPages/" + _Item.Component)
                .catch(
                    () => import("./AdminPages/" + _Item.Component)
                        .catch(
                            () => import("./UserPages/" + _Item.Component)
                                .catch(
                                    () => import("./UnloginedPages/" + _Item.Component)
                                )
                        )
                )
            ),
            exact: true,
        });     

    });

}

Pages.IsPageExists = function (_PageName, _ControlOnlyRootPage)
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
    var __Found = Pages.Routes.find((__Item) => __Item.purepath == _PageName);
    if (__Found == undefined) {
      if (__PageNameList.length > 2 && __PageNameList[1].length == 2) {
        _PageName = "/" + __PageNameList[2];
      }
      __Found = Pages.Routes.find((__Item) => __Item.purepath == _PageName);
    }
    return JSTypeOperator.IsDefined(__Found) && __Found;
};

export default Pages;
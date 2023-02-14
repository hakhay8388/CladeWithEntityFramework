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

Pages.LoadPages = function (_Pages, _CallbackFunction) {

    for (var __Item in Pages)
    {
        if (!JSTypeOperator.IsFunction(Pages[__Item])) {
            delete Pages[__Item];
        }        
    }

    Pages.Routes = [];

    _Pages.PagesItems.map(function (_Item, _Index) {

        Pages[_Item.Name] = _Item;

        Pages.Routes.push({
            Path: "/" + Pages.GetRouteString(_Item, true),
            PurePath: "/" + _Item.Path,
            IsMainPage: _Item.IsMainPage,
            //Name: GenericWebGraph.Managers.LanguageManager.ActiveLanguage[_Item.Name],
            Name: _Item.Name,
            Component: React.lazy(() => import("./GlobalPages/" + _Item.Component)
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
            Exact: _Item.SubParamName.length === 0
        });     

    });
    if (JSTypeOperator.IsFunction(_CallbackFunction)) {
        _CallbackFunction();
    }
}

export default Pages;
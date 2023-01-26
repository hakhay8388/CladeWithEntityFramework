import React, { Component, Suspense } from "react";
import GenericWebGraph from "../../../GenericWebController/GenericWebGraph";
import { CommandInterfaces } from "../../../GenericWebController/CommandInterpreter/cCommandInterpreter";
import {
  DebugAlert,
  Class,
  Interface,
  Abstract,
  ObjectTypes,
  JSTypeOperator,
} from "../../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TObject";
import Actions from "../../../GenericWebController/ActionGraph/Actions";
import { CommandIDs } from "../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs";


import { withStyles } from "@material-ui/core/styles";
import GlobalStyles from "../../../../ScriptStyles/GlobalStyles";
import classNames from "classnames";

import LandingPage3 from "../../../../views/LandingPage/LandingPage3";
import Helmet from "react-helmet";

var TMainPage = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("TMainPage"),
    constructor: function (_Props) {
      TMainPage.BaseObject.constructor.call(this, _Props);
      this.state = {
        ...this.state,
        bestLessons: null,
        lastSellers: null,
        mostPurchasedLessonList: null,
      };
    },
    AsyncLoad: function () {
      var __This = this;
      Actions.GetBestLessonList(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            bestLessons: _Data.Item.BestLessonList,
          });
        });
      });
      Actions.GetAllSellerWithLesson(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            lastSellers: _Data.Item.SellerListWithLesson,
          });
        });
      });
      Actions.GetMostPurchasedLessons(function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            loading: false,
            mostPurchasedLessonList: _Data.Item.LessonList,
          });
        });
      });
    },
    Destroy: function () {
      TMainPage.BaseObject.Destroy.call(this);
    },
    shouldComponentUpdate: function (nextProps, nextState) {
      return true;
    },
    render() {
      const { classes } = this.props;

      return (
        <div className="application" style={{ overflow: "hidden" }}>
        </div>
      )
    },
  },
  {}
);

export default withStyles(GlobalStyles)(TMainPage);

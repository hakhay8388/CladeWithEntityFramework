import React, { Suspense } from "react";
import { Routes, Route, Switch} from "react-router-dom";
import { Class, JSTypeOperator, ObjectTypes } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import Pages from "../../TagComponents/Pages/Pages";
import classNames from "classnames";
import DefaultTheme from "../../../Themes/DefaultTheme";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import { Accordion, AccordionDetails, Breadcrumbs, Drawer, Link } from "@mui/material";

var TBaseContainerLayout = Class(
    TObject,
    {
        ObjectType: ObjectTypes.Get("TBaseContainerLayout"),
        constructor: function (_Props) {
            TBaseContainerLayout.BaseObject.constructor.call(this, _Props);

            this.state = {
                ...this.state,
            };
            window.App.ContainerLayout = this;
        },
        Destroy: function () {
            TBaseContainerLayout.BaseObject.Destroy.call(this);
        },
        AsyncLoad: function (_Force) {
            TBaseContainerLayout.BaseObject.AsyncLoad.call(this, _Force);
        }
        ,
        HandleGetRoutes: function (_Lang) {
            var __IsPageExists = Pages.IsPageExists(this.props.router.location.pathname, true);
            if (!__IsPageExists) {
                GenericWebGraph.GoMainPage();
            }
            else {
                return (
                    <Routes>
                        {Pages.Routes.map((_Route, _Index) =>
                        {
                            if (_Route.IsMainPage)
                            {
                                return (
                                    <Route
                                        key={_Index}
                                        path={_Lang + "/"}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component />}
                                    />
                                );
                            }
                            else {
                                return (
                                    <Route
                                        key={_Index}
                                        path={_Lang + _Route.Path}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component />}
                                    />
                                );
                            }                            
                        })}
                    </Routes>
                );
            }
        },
        /*,
        shouldComponentUpdate(nextProps, nextState)
        {
          return false;
        }*/
        render: function() {
            const { classes } = this.props;

            //var __ShowShadow = document.body.classList.contains("sidebar-lg-show");
            //https://html-css-js.com/css/generator/box-shadow/

            return (
                 <React.Suspense fallback={<div className="container">
                    <div className="center">
                        <div className="lds-ripple"><div></div><div></div></div>
                    </div>
                </div>}>
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "column",
                            minHeight: "100vh",
                        }}
                    >
                        <div style={{ width: "100%", position: "fixed", zIndex: 1020 }}>
                            {this.HandleGetHeader()}
                        </div>
                        <div
                            style={{
                                marginTop: "76px",
                                flexDirection: "row",
                                flexGrow: 1,
                                overflowX: "hidden",
                            }}
                        >
                            <main onClick={this.HandleMainClicked}>
                                <div className={classes.container}>
                                   {this.HandleGetRoutes("/" + GenericWebGraph.Managers.LanguageManager.ActiveLanguage.LanguageCode)}
                                   {this.HandleGetRoutes("")}
                                </div>
                            </main>
                        </div>
                        <div
                            style={{
                                //backgroundColor: DefaultTheme.palette.dark.darkAlternative,
                                flex: "0 0 50px",
                                display: "flex",
                                flexWrap: "wrap",
                                alignItems: "center",
                                padding: "12px",
                            }}
                            id="footerStatus"
                        >
                            {this.HandleGetFooter()}
                        </div>
                    </div>
                </React.Suspense>


               
            );
        },
    },
    {}
);

export default TBaseContainerLayout;
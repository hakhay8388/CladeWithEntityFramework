import React, { Suspense } from "react";
import { Routes, Route} from "react-router-dom";
import { Class, JSTypeOperator, ObjectTypes } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import Pages from "../../TagComponents/Pages/Pages";
import classNames from "classnames";
import DefaultTheme from "../../../Themes/DefaultTheme";
import {
    ListItem, ListItemButton, ListItemIcon, ListItemText,
    Grid, Typography, Accordion, AccordionDetails, Breadcrumbs, Drawer, Link, AccordionSummary
} from "@mui/material";


import { MoveToInbox, ExpandMoreIcon, Mail } from "@mui/icons-material";

var TBaseContainerLayout = Class(
    TObject,
    {
        ObjectType: ObjectTypes.Get("TBaseContainerLayout"),
        constructor: function (_Props) {
            TBaseContainerLayout.BaseObject.constructor.call(this, _Props);

            this.state = {
                ...this.state,
                leftMenu: false,
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
        HandleOpenLeftDrawer: function () {
            this.setState({ leftMenu: true });
        },
        HandleCloseLeftDrawer: function () {
            this.setState({ leftMenu: false });
        },
        HandleToggleLeftDrawer: function () {
            this.setState({ leftMenu: !this.state.leftMenu });
        }
        ,
        HandleMenuClick: function (_Event, _Item, _Submenu) {
        }
        ,
        HandleChangeExpandedMenu: function (_Event, _Menu) {
            _Event.preventDefault();
            _Event.stopPropagation();
        }
        ,
        HandleGetMenu: function () {
            const { classes } = this.props;

            /*icon: "fas fa-search"
            name: "Ara"
            url: "search"*/

            console.log(DefaultTheme.palette.secondary.contrastText);
            var __This = this;
            return window.App.DynamicLoader.MenuItems.map(
                (_Item, _Index) => {
                    console.log(__This.state.Language[_Item.Name]);
                    if (_Item.MainMenu) {
                        return <ListItem key={_Item.Name} disablePadding>
                            <ListItemButton>
                                <ListItemIcon>
                                    {_Index % 2 === 0 ? <MoveToInbox /> : <Mail />}
                                </ListItemIcon>
                                <ListItemText primary={__This.state.Language[_Item.Name]} />
                            </ListItemButton>
                        </ListItem>;
                       /* return (
                            <Accordion
                                style={{
                                    backgroundColor: DefaultTheme.palette.dark.darkAlternative,
                                }}
                                className={this.state.expandedMenu && classes.expandedMargin}
                                elevation={0}
                                expanded={this.state.expandedMenu === _Item.Name}
                                onChange={(_Event) => {
                                    this.HandleChangeExpandedMenu(_Event, _Item.Name);
                                }}
                            >
                                <AccordionSummary
                                    expandIcon={
                                        <ExpandMoreIcon
                                            style={{
                                                color: DefaultTheme.palette.secondary.contrastText,
                                            }}
                                        />
                                    }
                                    aria-controls="panel1bh-content"
                                    id="panel1bh-header"
                                >
                                    <Grid container style={{ width: "200px" }}>
                                        <Grid item xs={2}>
                                            <i style={{ color: "#73818f" }} className={_Item.Icon} />
                                        </Grid>
                                        <Grid item xs={10} style={{ margin: "auto" }}>
                                            <Typography
                                                style={{
                                                    fontSize: "14px",
                                                    color: DefaultTheme.palette.secondary.contrastText,
                                                }}
                                            >
                                                {this.state.Language[_Item.Name]}
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                </AccordionSummary>
                                <AccordionDetails>
                                    <Grid containe>
                                        {_Item.SubMenu.map((__SubMenu) => {
                                            return (
                                                <Grid item xs={12}>
                                                    <a
                                                        className={
                                                            _Item.Active
                                                                ? classNames("nav-link", "active")
                                                                : classNames("nav-link")
                                                        }
                                                        style={{
                                                            backgroundColor:
                                                                __SubMenu.Active &&
                                                                DefaultTheme.palette.dark.fourthAlternative,
                                                        }}
                                                        href={"#"}
                                                        onClick={
                                                            __SubMenu.attributes &&
                                                                __SubMenu.attributes.onClick
                                                                ? __SubMenu.attributes.onClick
                                                                : (_Event) =>
                                                                    this.HandleMenuClick(
                                                                        _Event,
                                                                        __SubMenu,
                                                                        true
                                                                    )
                                                        }
                                                    >
                                                        <Grid container style={{ width: "200px" }}>
                                                            <Grid item xs={2}>
                                                                <i
                                                                    style={{
                                                                        color: __SubMenu.Active
                                                                            ? "#f44336"
                                                                            : "#73818f",
                                                                        fontSize: "13px",
                                                                    }}
                                                                    className={__SubMenu.icon}
                                                                />
                                                            </Grid>
                                                            <Grid item xs={10} style={{ margin: "auto" }}>
                                                                <Typography
                                                                    style={{
                                                                        fontSize: "13px",
                                                                        color: __SubMenu.Active
                                                                            ? "#f44336"
                                                                            : DefaultTheme.palette.secondary
                                                                                .contrastText,
                                                                    }}
                                                                >
                                                                    {this.state.Language[__SubMenu.name]}
                                                                </Typography>
                                                            </Grid>
                                                        </Grid>
                                                    </a>
                                                </Grid>
                                            );
                                        })}
                                    </Grid>
                                </AccordionDetails>
                            </Accordion>
                        );*/
                    } else {

                        return <Grid key={_Item.Name}  container style={{ width: "200px" }}>
                            <Grid item xs={2}>
                                <Typography
                                    style={{
                                        fontSize: "14px",
                                        color: _Item.Active
                                            ? "#f44336"
                                            : DefaultTheme.palette.secondary.contrastText,
                                    }}
                                >
                                    {__This.state.Language[_Item.Name]}
                                </Typography>
                            </Grid>
                        </Grid>

                        /*return <ListItem key={_Item.Name} disablePadding>
                            <ListItemButton>
                                <ListItemIcon>
                                    {_Index % 2 === 0 ? <MoveToInbox /> : <Mail />}
                                </ListItemIcon>
                                <ListItemText primary={__This.state.Language[_Item.Name]} />
                            </ListItemButton>
                        </ListItem>;*/

                        /*return (
                            <a
                                className={
                                    _Item.Active
                                        ? classNames("nav-link", "active")
                                        : classNames("nav-link")
                                }
                                style={{
                                    backgroundColor:
                                        _Item.Active && DefaultTheme.palette.dark.fourthAlternative,
                                }}
                                href={"#"}
                                onClick={
                                    _Item.attributes && _Item.attributes.onClick
                                        ? _Item.attributes.onClick
                                        : (_Event) => this.HandleMenuClick(_Event, _Item, false)
                                }
                            >
                                <Grid container style={{ width: "200px" }}>
                                    <Grid item xs={2}>
                                        <i
                                            style={{ color: _Item.Active ? "#f44336" : "#73818f" }}
                                            className={_Item.Icon}
                                        />
                                    </Grid>
                                    <Grid item xs={10} style={{ margin: "auto" }}>
                                        <Typography
                                            style={{
                                                fontSize: "14px",
                                                color: _Item.Active
                                                    ? "#f44336"
                                                    : DefaultTheme.palette.secondary.contrastText,
                                            }}
                                        >
                                            {this.state.Language[_Item.Name]}
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </a>
                        );*/
                    }
                }
            );
        }
        ,
        HandleGetRoutes: function (_Lang) {
            var __This = this;
            var __IsPageExists = Pages.IsPageExists(this.props.router.location.pathname, true);
            if (!__IsPageExists) {
                window.GoMainPage();
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
                                        {...__This.props}
                                        key={_Index}
                                        path={_Lang + "/"}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component ownerLayout={__This } />}
                                    />
                                );
                            }
                            else {
                                return (
                                    <Route
                                        {...__This.props}
                                        key={_Index}
                                        path={_Lang + _Route.Path}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component ownerLayout={__This} />}
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
        render: function () {
            var __This = this;
            const { classes } = this.props;

            //var __ShowShadow = document.body.classList.contains("sidebar-lg-show");
            //https://html-css-js.com/css/generator/box-shadow/

            return (
                 <Suspense fallback={<div className="container">
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
                            <Suspense>
                                {this.HandleGetHeader()}
                            </Suspense>
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
                                    <Drawer
                                        PaperProps={{
                                            sx: {
                                                //backgroundColor: "pink"
                                            }
                                        }}

                                        sx={{
                                            width: "300px",
                                            flexShrink: 0,
                                            '& .MuiDrawer-paper': {
                                                width: "300px",
                                                boxSizing: 'border-box',
                                            },
                                        }}
                                        variant="persistent"
                                        anchor={"left"}
                                        id={"leftDrawer"}
                                        open={this.state.leftMenu}
                                        //onClose={this.HandleCloseLeftDrawer}
                                        //classes={{ paper: classes.paper }}
                                       >
                                        {this.HandleGetMenu()}
                                      </Drawer>

                                   {/*this.HandleGetRoutes("/" + GenericWebGraph.Managers.LanguageManager.ActiveLanguage.LanguageCode)*/}
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
                            <Suspense>
                                {this.HandleGetFooter()}
                            </Suspense>
                        </div>
                    </div>
                </Suspense>


               
            );
        },
    },
    {}
);

export default TBaseContainerLayout;
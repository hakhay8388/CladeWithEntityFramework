import React, { Suspense } from "react";
import { Routes, Route } from "react-router-dom";
import { Class, JSTypeOperator, ObjectTypes } from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";
import classNames from "classnames";
import {
    ListItem, ListItemButton, ListItemIcon, ListItemText, Box,
    Grid, Typography, Accordion, AccordionDetails, Breadcrumbs, Drawer, Link, AccordionSummary, Divider
} from "@mui/material";

import KeyboardArrowDown from '@mui/icons-material/KeyboardArrowDown';


var TBaseContainerLayout = Class(
    TObject,
    {
        ObjectType: ObjectTypes.Get("TBaseContainerLayout"),
        constructor: function (_Props) {
            TBaseContainerLayout.BaseObject.constructor.call(this, _Props);

            this.state = {
                ...this.state,
                leftMenu: false
            };
            this.MainClickEventList = [];
            this.DrawWidth = 500;
            window.App.ContainerLayout = this;
        },
        Destroy: function () {
            TBaseContainerLayout.BaseObject.Destroy.call(this);
        },
        AsyncLoad: function (_Force) {
            TBaseContainerLayout.BaseObject.AsyncLoad.call(this, _Force);
        }
        ,
        HandleMainClicked: function (_Event) {
            /* if (window.App.Header !== null && (this.state.leftMenu || this.state.rightMenu)) {
                 this.setState({
                     leftMenu: false,
                     rightMenu: false
                 });
             }*/
        }
        ,
        HandleIsShowFooter: function ()
        {
            return true;
        }
        ,
        HandleIsShowHeader: function ()
        {
            return true;
        }
        ,
        HandleOpenLeftDrawer: function () {
            this.setState({ leftMenu: true });
        },
        HandleCloseLeftDrawer: function () {
            this.setState({ leftMenu: false });
        },
        HandleToggleLeftDrawer: function () {
            var __Temp = !this.state.leftMenu;
            this.setState({ leftMenu: !this.state.leftMenu });
            return __Temp;
        }
        ,
        HandleMenuClick: function (_Event, _Item, _Submenu) {
            alert("aa");
        }
        ,
        HandleChangeExpandedMenu: function (_Event, _Menu) {
            _Event.preventDefault();
            _Event.stopPropagation();
        }
        ,
        HandleGetMenu: function () {
            const { classes } = this.props;

            var __This = this;
            return window.MenuManager.MenuItems.map(
                (_Item, _Index) => {

                    if (_Item.MainMenu) {
                        if (!JSTypeOperator.IsDefined(__This.state["Open_" + _Item.Name])) {
                            __This.state["Open_" + _Item.Name] = false;
                        }

                        return <Box key={"MenuBox_" + _Item.Name}>
                            <ListItem key={_Item.Name} disablePadding sx={{ padding: "5px" }}>
                                <ListItemButton onClick={function (_Event) {
                                    var __Temp = !__This.state["Open_" + _Item.Name];

                                    __This.setState({
                                        ["Open_" + _Item.Name]: __Temp
                                    });

                                }}>
                                    <ListItemIcon sx={{ fontSize: 20, color: (_Item.Active ? "themeLightText.activeColor" : "themeLightText.pasiveColor") }}>
                                        <i className={_Item.Icon} />
                                    </ListItemIcon>
                                    <ListItemText

                                        sx={{ color: (_Item.Active ? "themeLightText.activeColor" : "themeLightText.pasiveColor") }}
                                        primary={__This.state.Language[_Item.Name]}
                                        primaryTypographyProps={{
                                            fontSize: 16,
                                            fontWeight: 'medium',
                                            letterSpacing: 0,
                                        }}
                                    />
                                    <KeyboardArrowDown
                                        key={"KeyboardArrowDown" + _Item.Name}
                                        sx={{
                                            color: (_Item.Active ? "themeLightText.activeColor" : "themeLightText.pasiveColor"),
                                            mr: -1,
                                            opacity: 1,
                                            transform: __This.state["Open_" + _Item.Name] ? 'rotate(0deg)' : 'rotate(90deg)',
                                            transition: '0.2s',
                                        }}
                                    />
                                </ListItemButton>
                            </ListItem>

                            {__This.state["Open_" + _Item.Name] &&
                                _Item.SubMenu.map((__SubMenu) => (
                                    <ListItem key={_Item.Name} disablePadding sx={{ paddingLeft: "25px" }}>
                                        <ListItemButton
                                            onClick={function (_Event) {
                                                window.GoPage(__SubMenu.Url);
                                            }}
                                            key={__SubMenu.Name}
                                            sx={{ py: 0, minHeight: 32, color: 'rgba(255,255,255,.8)' }}
                                        >
                                            <ListItemIcon sx={{ color: 'inherit' }}>
                                                <i className={__SubMenu.Icon} />
                                            </ListItemIcon>
                                            <ListItemText
                                                primary={__This.state.Language[__SubMenu.Name]}
                                                primaryTypographyProps={{ fontSize: 14, fontWeight: 'medium' }}
                                            />
                                        </ListItemButton>
                                    </ListItem>
                                ))}
                        </Box>

                    } else {

                        return <ListItem key={_Item.Name} disablePadding sx={{ padding: "5px" }}>
                            <ListItemButton selected={_Item.Active}
                                onClick={function (_Event) {
                                    window.GoPage(_Item.Url);
                                }}                            >
                                <ListItemIcon sx={{ fontSize: 20, color: (_Item.Active ? "themeLightText.activeColor" : "themeLightText.pasiveColor") }}>
                                    <i className={_Item.Icon} />
                                </ListItemIcon>
                                <ListItemText
                                    sx={{ color: (_Item.Active ? "themeLightText.activeColor" : "themeLightText.pasiveColor") }}
                                    primary={__This.state.Language[_Item.Name]}
                                    primaryTypographyProps={{
                                        fontSize: 16,
                                        fontWeight: 'medium',
                                        letterSpacing: 0,
                                    }}
                                />
                            </ListItemButton>
                        </ListItem>;


                    }
                }
            );
        }
        ,
        HandleGetRoutes: function (_Lang) {
            var __This = this;
            var __IsPageExists = window.PageManager.IsPageExists(window.App.App.props.router.location.pathname, true);
            if (!__IsPageExists) {
                window.GoMainPage();
            }
            else {
                return (
                    <Routes>
                        {window.PageManager.Routes.map((_Route, _Index) => {
                            if (_Route.IsMainPage) {
                                return (
                                    <Route
                                        key={_Index}
                                        path={_Lang + "/"}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component ownerLayout={__This} />}
                                    />
                                );
                            }
                        })}
                        {window.PageManager.Routes.map((_Route, _Index) => {
                                return (
                                    <Route
                                        key={_Index}
                                        path={_Lang + _Route.Path}
                                        exact={_Route.Exact}
                                        name={_Route.Name}
                                        element={<_Route.Component ownerLayout={__This} />}
                                    />
                                );
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
                        {this.HandleIsShowHeader() &&
                            <div style={{ width: "100%", position: "fixed", zIndex: 1020 }}>
                                <Suspense>
                                    {this.HandleGetHeader()}
                                </Suspense>
                            </div>
                        }
                        <div
                            style={{
                                marginTop: this.HandleIsShowHeader() ? window.Settings.HeaderHeight + "px" : 0,
                                flexDirection: "row",
                                flexGrow: 1,
                                overflowX: "hidden",
                            }}
                        >
                            <main onClick={this.HandleMainClicked}>
                                <div className={classes.container}>
                                    <Drawer
                                        sx={{
                                            width: window.Settings.DrawerWidth,
                                            flexShrink: 0,
                                            '& .MuiDrawer-paper': {
                                                width: window.Settings.DrawerWidth,
                                                boxSizing: 'border-box',
                                                marginTop: window.Settings.HeaderHeight + "px",
                                            },
                                        }}
                                        variant="persistent"
                                        anchor={"left"}
                                        id={"leftDrawer"}
                                        open={this.state.leftMenu}
                                    //onClose={this.HandleCloseLeftDrawer}
                                    //classes={{ paper: classes.paper }}
                                    >
                                        <ListItem key={"MenuHeader"} disablePadding>
                                            <ListItemButton>
                                                <ListItemText
                                                    sx={{ color: "themeLightText.activeColor" }}
                                                    primary="Menu"
                                                    primaryTypographyProps={{
                                                        fontSize: 20,
                                                        fontWeight: 'medium',
                                                        letterSpacing: 0,
                                                    }}
                                                />
                                            </ListItemButton>
                                        </ListItem>
                                        <Divider sx={{ borderColor: "themeLightDivider.color" }} />
                                        {this.HandleGetMenu()}
                                    </Drawer>

                                    <Grid container

                                        /*sx={[
                                            {
                                                width: `calc(100%)`,
                                                marginLeft: 0,
                                                transition: (theme) => theme.transitions.create(['margin', 'width', 'transform'], {
                                                    easing: theme.transitions.easing.sharp,
                                                    duration: theme.transitions.duration.leavingScreen,
                                                })

                                            },
                                            __This.state.leftMenu && {
                                                marginLeft: `${window.Settings.DrawerWidth}px`,
                                                width: `calc(100% - ${window.Settings.DrawerWidth}px)`,
                                            }
                                        ]}*/
                                    >
                                        <Suspense fallback={<div className="container">
                                            <div className="center">
                                                <div className="lds-ripple"><div></div><div></div></div>
                                            </div>
                                        </div>}>
                                            <Grid item xs={12} sx={{ padding : "10px" }}>
                                                {/*this.HandleGetRoutes("/" + GenericWebGraph.Managers.LanguageManager.ActiveLanguage.LanguageCode)*/}
                                                {this.HandleGetRoutes("")}

                                            </Grid>
                                        </Suspense>
                                    </Grid>

                                </div>
                            </main>
                        </div>
                        {this.HandleIsShowFooter() && <div
                            style={{
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
                        </div>}
                    </div>
                </Suspense>



            );
        },
    },
    {}
);

export default TBaseContainerLayout;
import React, { Component } from "react";
import { Class, JSTypeOperator, ObjectTypes } from "../GenericCoreGraph/ClassFramework/Class";
import withRouter from "./Utilities/withRouter";
import TLoading from "./Utilities/TLoading";
import { ThemeProvider } from "@mui/material/styles";
import { WebGraph } from "../GenericCoreGraph/WebGraph/WebGraph";
import TObject from "./TObject";

import Button from '@mui/material/Button';


const TUnloginedLayout = React.lazy(() => import("./Pages/UnloginedPages/Containers/TUnloginedLayout"));
const TAdminLayout = React.lazy(() => import("./Pages/AdminPages/Containers/TAdminLayout"));
const TUserLayout = React.lazy(() => import("./Pages/UserPages/Containers/TUserLayout"));


const TMessageBox = React.lazy(() => import("./Listeners/TMessageBox"));
const THotSpotMessage = React.lazy(() => import("./Listeners/THotSpotMessage"));
const TGlobalLoading = React.lazy(() => import("./Utilities/TGlobalLoading"));



var TApp = Class(TObject,
    {
        ObjectType: ObjectTypes.Get("TApp")
        ,
        constructor: function (_Props)
        {
            TApp.BaseObject.constructor.call(this, _Props);
            this.state =
            {
                ...this.state,
            }
            window.App.App = this;
        }
        ,
        Destroy: function ()
        {
            TApp.BaseObject.Destroy.call(this);
        }
        ,
        HandleGetTheme()
        {
            return window.Themes.DefaultTheme;
        }
        ,
        HandleGetRoleLayout()
        {
            if (window.App.User == null)
            {
                return <TUnloginedLayout />
            }
            else
            {
                var __Found = window.App.User.Roles.find(__Item => __Item.MainCode == "Admin");
                if (JSTypeOperator.IsDefined(__Found) && __Found != null)
                {
                    return <TAdminLayout />
                }
                else
                {
                    __Found = window.App.User.Roles.find(__Item => __Item.MainCode == "User");
                    if (JSTypeOperator.IsDefined(__Found) && __Found != null)
                    {
                        return <TUserLayout />
                    }

                }
                console.log(window.App.User);

            }
        }
        ,
        render: function ()
        {
            var __This = this;
            return (
                <div style={{ fontFamily: "Arial" }}>
                    <React.Suspense fallback={<div className="container">
                        <div className="center">
                            <div className="lds-ripple"><div></div><div></div></div>
                        </div>
                    </div>}>
                        <ThemeProvider theme={__This.HandleGetTheme()}>
                            <TMessageBox />
                            <THotSpotMessage />
                            <TGlobalLoading />
                            {__This.HandleGetRoleLayout()}
                        </ThemeProvider>
                    </React.Suspense>
                </div>
            );
        }
    }, {});

export default withRouter(TApp)

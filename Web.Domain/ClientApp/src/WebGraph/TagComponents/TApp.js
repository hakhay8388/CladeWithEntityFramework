import React, { Component } from "react";
import withRouter from "./Utilities/withRouter";
import TLoading from "./Utilities/TLoading";
import { ThemeProvider } from "@mui/material/styles";
import { WebGraph } from "../GenericCoreGraph/WebGraph/WebGraph";

import Button from '@mui/material/Button';


const TUnloginedLayout = React.lazy(() => import("./Pages/UnloginedPages/Containers/TUnloginedLayout"));
const TAdminLayout = React.lazy(() => import("./Pages/AdminPages/Containers/TAdminLayout"));


const TMessageBox = React.lazy(() => import("./Listeners/TMessageBox"));
const THotSpotMessage = React.lazy(() => import("./Listeners/THotSpotMessage"));
const TGlobalLoading = React.lazy(() => import("./Utilities/TGlobalLoading"));


class TApp extends Component {
    constructor(_Props) {
        super();

        this.GetTheme = this.GetTheme.bind(this);
        this.GetRoleLayout = this.GetRoleLayout.bind(this);

        window.App.App = this;
        this.state = {
            ...this.state,
        };
    }


    GetTheme() {
        return window.Themes.DefaultTheme;
    }

    GetRoleLayout()
    {
        if (window.App.User == null)
        {
            return <TUnloginedLayout />
        }
        else
        {
            return <TAdminLayout />
        }
    }

    render() {
        var __This = this;
        return (
            <div style={{ fontFamily: "Arial" }}>
                <React.Suspense fallback={<div className="container">
                    <div className="center">
                        <div className="lds-ripple"><div></div><div></div></div>
                    </div>
                </div>}>
                    <ThemeProvider theme={__This.GetTheme()}>
                        <TMessageBox />
                        <THotSpotMessage />
                        <TGlobalLoading />
                        {__This.GetRoleLayout()}
                    </ThemeProvider>
                </React.Suspense>
            </div>
        );
    }
}

export default withRouter(TApp)


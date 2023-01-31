import React, { Component } from "react";
import withRouter from "./Utilities/withRouter";
import TLoading from "./Utilities/TLoading";
import { ThemeProvider } from "@mui/material/styles";
import { WebGraph } from "../GenericCoreGraph/WebGraph/WebGraph";

import Button from '@mui/material/Button';

const TDynamicLoader = React.lazy(() => import("./Loaders/TDynamicLoader"));
const TUnloginedLayout = React.lazy(() => import("./Pages/UnloginedPages/Containers/TUnloginedLayout"));
const TAdminLayout = React.lazy(() => import("./Pages/AdminPages/Containers/TAdminLayout"));

class TApp extends Component {
    constructor(_Props) {
        super();

        this.GetTheme = this.GetTheme.bind(this);

        window.App.App = this;
        this.state = {
            ...this.state,
        };
        WebGraph.Init();
    }


    GetTheme() {
        return window.Themes.DefaultTheme;
    }

    pu

    render() {
        var __This = this;
        return (
            <div style={{ fontFamily: "Montserrat" }}>
                <React.Suspense fallback={<div className="container">
                    <div className="center">
                        <div className="lds-ripple"><div></div><div></div></div>
                    </div>
                </div>}>
                    <ThemeProvider theme={this.GetTheme()}>
                        <TDynamicLoader getInnerChilds={() => {
                            return <div>
                                <TAdminLayout {...this.props} />
                            </div>
                        }} />
                    </ThemeProvider>
                </React.Suspense>
            </div>
        );
    }
}

export default withRouter(TApp)


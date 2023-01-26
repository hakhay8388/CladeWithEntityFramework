import React, { Component } from "react";
import withRouter from "./Utilities/withRouter";
import TLoading from "./Utilities/TLoading";
import { ThemeProvider } from "@mui/material/styles";
import { WebGraph } from "../GenericCoreGraph/WebGraph/WebGraph";

import Button from '@mui/material/Button';

const TDynamicLoader = React.lazy(() => import("./TDynamicLoader"));
const TLogin = React.lazy(() => import("./Pages/UnloginedPages/TLogin"));

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

    render() {
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
                                <TLogin />
                            </div>
                        }} />
                    </ThemeProvider>
                </React.Suspense>
            </div>
        );
    }
}

export default withRouter(TApp)


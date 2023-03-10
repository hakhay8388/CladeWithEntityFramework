import "./Assets/Fonts/Awesome/css/all.min.css";
import "./WebGraph/Initializers";
import "./Themes/Themes.js";
import "./WebGraph/App";
import { WebGraph } from "./WebGraph/GenericCoreGraph/WebGraph/WebGraph";
import GenericWebGraph from "./WebGraph/GenericWebController/GenericWebGraph";

import React from "react";
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from "react-router-dom";

//import TApp from "./WebGraph/TagComponents/TApp";

import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';


WebGraph.Init();
GenericWebGraph.Init(() => {

    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
    const rootElement = document.getElementById('root');
    const root = createRoot(rootElement);

    import("./WebGraph/TagComponents/TApp")
        .then((_Event1) => {
            var TApp = _Event1.default;
            root.render(<BrowserRouter basename={baseUrl}>
                <TApp />
            </BrowserRouter>);
        });


   


});

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

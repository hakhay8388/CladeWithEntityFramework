import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TBaseContainerLayout from "../../TBaseContainerLayout"

import { withStyles } from "@mui/styles";
import UserStyles from "../../../../../ScriptStyles/UserStyles";


const TUserAside = React.lazy(() => import('./TUserAside'));
const TUserHeader = React.lazy(() => import('./TUserHeader'));
const TUserFooter = React.lazy(() => import('./TUserFooter'));


var TUserLayout = Class(TBaseContainerLayout,
    {
        ObjectType: ObjectTypes.Get("TUserLayout")
        ,
        constructor: function (_Props) {
            TUserLayout.BaseObject.constructor.call(this, _Props);
        }
        ,
        HandleGetAside: function () {
            return <TUserAside />
        }
        ,
        HandleGetFooter: function () {
            return <TUserFooter />
        }
        ,
        HandleGetHeader: function () {
            return <TUserHeader />;
        }
        ,
        HandleGetModals: function () {
            return <div>
            </div>;
        }
        ,
        Destroy: function () {
            TUserLayout.BaseObject.Destroy.call(this);
        }
    }, {});

export default withStyles(UserStyles)(TUserLayout);



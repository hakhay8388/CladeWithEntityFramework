import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TBaseContainerLayout from "../../TBaseContainerLayout"

import { withStyles } from "@mui/styles";
import UnLoginStyles from "../../../../../ScriptStyles/UnLoginStyles";


const UnloginFooter = React.lazy(() => import('./TUnloginFooter'));
const UnloginHeader = React.lazy(() => import('./TUnloginHeader'));



var TUnloginedLayout = Class(TBaseContainerLayout,
    {
        ObjectType: ObjectTypes.Get("TUnloginedLayout")
        ,
        constructor: function (_Props)
        {
            TUnloginedLayout.BaseObject.constructor.call(this, _Props);
        }
        ,
        HandleGetAside: function ()
        {
            return null;
        }
        ,
        HandleGetFooter: function ()
        {
            var __This = this;
            return <UnloginFooter {...this.props} />
        }
        ,
        HandleIsShowFooter: function ()
        {
            return false;
        }
        ,
        HandleGetHeader: function ()
        {
            return <UnloginHeader {...this.props} />;
        }
        ,
        HandleIsShowHeader: function ()
        {
            return false;
        }
        ,
        Destroy: function ()
        {
            TUnloginedLayout.BaseObject.Destroy.call(this);
        }
    }, {});

export default withStyles(UnLoginStyles)(TUnloginedLayout);



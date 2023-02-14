import React, { Component, Suspense } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TBaseContainerLayout from "../../TBaseContainerLayout"

import { withStyles } from "@mui/styles";
import AdminStyles from "../../../../../ScriptStyles/AdminStyles";


const TAdminAside = React.lazy(() => import('./TAdminAside'));
const TAdminHeader = React.lazy(() => import('./TAdminHeader'));
const TAdminFooter = React.lazy(() => import('./TAdminFooter'));

const TBatchJobEditModal = React.lazy(() => import('../Modals/TBatchJobEditModal'));
const TLanguageEditModal = React.lazy(() => import('../Modals/TLanguageEditModal'));



var TAdminLayout = Class(TBaseContainerLayout,
    {
        ObjectType: ObjectTypes.Get("TAdminLayout")
        ,
        constructor: function (_Props) {
            TAdminLayout.BaseObject.constructor.call(this, _Props);
        }
        ,
        HandleGetAside: function () {
            return <TAdminAside />
        }
        ,
        HandleGetFooter: function () {
            return <TAdminFooter />
        }
        ,
        HandleGetHeader: function () {
            return <TAdminHeader />;
        }
        ,
        HandleGetModals: function () {
            return <div>
                <TBatchJobEditModal />
                <TLanguageEditModal />
            </div>;
        }
        ,
        Destroy: function () {
            TAdminLayout.BaseObject.Destroy.call(this);
        }
    }, {});

export default withStyles(AdminStyles)(TAdminLayout);



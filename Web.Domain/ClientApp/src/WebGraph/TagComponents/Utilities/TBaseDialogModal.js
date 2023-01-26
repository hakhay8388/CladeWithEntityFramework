import React, { useRef } from "react";

import {
  Class,
  JSTypeOperator,
  ObjectTypes,
} from "../../GenericCoreGraph/ClassFramework/Class";
import TObject from "../../TagComponents/TObject";
import GenericWebGraph from "../../GenericWebController/GenericWebGraph";

import CloseIcon from '@mui/icons-material/Close';


import { Dialog, Grid, IconButton } from '@mui/material';


var TBaseDialogModal = Class(
  TObject,
  {
    ObjectType: ObjectTypes.Get("TBaseDialogModal"),
    constructor: function (_Props) {
      TBaseDialogModal.BaseObject.constructor.call(this, _Props);
      this.NeedUpdate = false;

      this.child = React.createRef();
      this.state = {
        ...this.state,
        IsFullScreen:
          GenericWebGraph.MainContainerSize.Width < 600 ||
          GenericWebGraph.MainContainerSize.Height < 600,
        IsModalLock: false,
        open: false,
      };

      },
      HandleOnSmDown: function (_Size) {
          this.setState({
              IsXs: true,
              IsFullScreen: true,
          });
      }
      ,
      HandleOnSmUp: function (_Size) {
          this.setState({
              IsXs: false,
              IsFullScreen: false || GenericWebGraph.MainContainerSize.Height < 600,
          });
      }
      ,
    HandleStopRenderWhenClosed: function () {
      var __This = this;
      if (!this.state.open) {
        this.NeedUpdate = false;
      } else {
        setTimeout(function () {
          __This.HandleStopRenderWhenClosed();
        }, 500);
      }
    },
    HandleClickOpen: function (_ParamObject, _CallbackOnClose) {
      this.NeedUpdate = true;

      var __This = this;
      if (_ParamObject != null && _ParamObject.ModalLock) {
        __This.setState({IsModalLock: true})
      } else {
        __This.setState({IsModalLock: false})
      }
    },
    HandleClose: function (_Event, _Reason) {
      alert("InnerHandleClose Override edilmedi..!");
    },
    HandleInnerClose: function (_Event, _Reason) {
      var __This = this;
      if (
        _Reason != null &&
        __This.state.IsModalLock &&
        (_Reason == "escapeKeyDown" || _Reason == "backdropClick")
      ) {
      } else {
        this.HandleClose(_Event, _Reason);
        this.HandleStopRenderWhenClosed();
      }
    },
    HandleGetSize: function () {
      return "md";
    },
    HandleGetCloseButton: function () {
      var __This = this;
      return (
        <IconButton
          style={{ float: "right" }}
          size={"medium"}
          onClick={() => {
            __This.HandleInnerClose();
          }}
        >
          <CloseIcon fontSize={"medium"} />
        </IconButton>
      );
    },

    HandleWrapModal: function (_Inner, _DisableEnforceFocus) {
      if (this.state.open) {
        return (
          <Dialog
            open={this.state.open}
            fullScreen={this.state.IsFullScreen}
            onClose={this.HandleInnerClose}
            maxWidth={this.HandleGetSize()}
            fullWidth
            aria-labelledby="form-dialog-title"
            disableEnforceFocus={JSTypeOperator.IsDefined(_DisableEnforceFocus) ? _DisableEnforceFocus: false}
          >
            <Grid
              container
              direction="row"
              justifyContent="center"
              style={{ height: "8px", overflow: "visible", zIndex: 99999 }}
            >
              <Grid
                item
                xs={12}
                style={{
                  paddingRight: "10px",
                  paddingTop: "10px",
                  height: "0px",
                }}
              >
                {this.HandleGetCloseButton()}
              </Grid>
            </Grid>
            {_Inner.props.children}
          </Dialog>
        );
      } else {
        return null;
      }
    },
    AsyncLoad: function () {},
    Destroy: function () {
      TBaseDialogModal.BaseObject.Destroy.call(this);
    },
    HandleOnCloseDefaultFunction: function () {},
    render: function () {
      return { NeedRender: this.NeedUpdate };
    },
  },
  {}
);

export default TBaseDialogModal;

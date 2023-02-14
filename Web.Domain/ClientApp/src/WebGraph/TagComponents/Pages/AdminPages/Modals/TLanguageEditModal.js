import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../../TagComponents/TObject"
import { CommandInterfaces } from "../../../../GenericWebController/CommandInterpreter/cCommandInterpreter"
import Actions from "../../../../GenericWebController/ActionGraph/Actions"
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph"
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"

import { withStyles } from "@mui/styles";
import {
    Grid, Button, TextField, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, LinearProgress,
    FormControlLabel, Switch
} from '@mui/material';

import LanguageEditModalStyles from "../../../../../ScriptStyles/LanguageEditModalStyles"

import TBaseDialogModal from "../../../Utilities/TBaseDialogModal";

var TLanguageEditModal = Class(TBaseDialogModal,
  {
    ObjectType: ObjectTypes.Get("TLanguageEditModal")
    ,
    constructor: function (_Props) {
      TLanguageEditModal.BaseObject.constructor.call(this, _Props);
      this.state =
      {
        ...this.state,
        open: false,
        loading: false,
        ID: 0,
        LanguageCode: "",
        Words: [],
        ParamCount: 0,
      };
      window.App.LanguageEditModal = this;
    }
    ,
    Destroy: function () {
      TLanguageEditModal.BaseObject.Destroy.call(this);
    }
    ,
    HandleLoadDetail: function (_LanguageCode) {
      var __This = this;

      Actions.GetLanguageWordByCode(_LanguageCode, function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          __This.setState({
            open: true,
            Words: _Data.Item.Words,
            loading: false,
          }, () => {
          });

        });

      });

    },
    Handle_SaveLanguageWords: function () {
      var __This = this;
      Actions.SaveLanguageWordList(__This.state.Words, function (_Message) {
        CommandIDs.SuccessResultCommand.RunIfHas(_Message, function (_Data) {
          __This.HandleClose();
        });
      });
    }
    ,
    Handle_GetLanguageWordsControls: function () {
      var __This = this;
      if (__This.state.Words != null && __This.state.Words.length > 0) {
        return __This.state.Words.map((_Item, _Index) => {
          return __This.Handle_GetTextBox(_Item);
        })
      }
      else {
        return null;
      }

    },
    HandleChange_TextBox(event) {
      var __This = this;

      var __TempParamList = __This.state.Words;

      __TempParamList.forEach((__Element, __Index) => {
        if (__Element.Name === event.target.name) {
          __Element.Word = event.target.value;
          __TempParamList[__Index] = __Element;
        }
      });
      __This.setState({
        Words: __TempParamList
      });
    },
    Handle_GetTextBox: function (_Item) {
      var __This = this;

      return <Grid item xs={12} sm={12} md={12} lg={12}>

        <TextField
          id={"txt_" + _Item.ID.toString()}
          label={_Item.Name}
          value={_Item.Word}
          name={_Item.Name}
          onChange={this.HandleChange_TextBox}
          variant={"outlined"}
          fullWidth
        />
      </Grid>
    },
    HandleClickOpen: function (_ParamObject, _CallbackOnClose) {
      TLanguageEditModal.BaseObject.HandleClickOpen.call(this, _ParamObject, _CallbackOnClose);
      var __This = this;

      this.CallbackOnClose = _CallbackOnClose;
      __This.setState({
        open: true,
        LanguageCode: _ParamObject.LanguageCode,
        ParamCount: _ParamObject.ParamCount,
        loading: true,
      }, () => {
        __This.HandleLoadDetail(__This.state.LanguageCode);
      });

    }
    ,
    HandleClose: function () {
      var __This = this;
      this.setState({
        open: false
      });

      if (JSTypeOperator.IsFunction(__This.CallbackOnClose)) {
        var __Function = __This.CallbackOnClose;
        __This.CallbackOnClose = null;
        __Function();
      }
    },
    render: function () {
      const { classes } = this.props;
      var __This = this;
      var __Params = TLanguageEditModal.BaseObject.render.call(this);
      if (!__Params.NeedRender) {
        return  null;
      }
      return (
        <div>
          <Dialog maxWidth={"md"} classes={{ paper: classes.DialogModal }} open={__This.state.open} onClose={__This.HandleInnerClose} aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">{"#" + __This.state.LanguageCode + "# (" + this.state.Language.ParamCount.format(this.state.ParamCount) + " )" + __This.state.Language.LanguagePageDetail}</DialogTitle>
            <DialogContent>
              <DialogContentText style={{ width: "1024px" }}>
              </DialogContentText>
              {__This.state.loading ? (
                <LinearProgress />
              ) : (
                <React.Fragment>

                  <Grid container spacing={2}>

                    {this.Handle_GetLanguageWordsControls()}
                  </Grid>
                </React.Fragment>
              )}

            </DialogContent>
            <DialogActions>
              <Button onClick={__This.Handle_SaveLanguageWords} color="primary">
                {__This.state.Language.Update}
              </Button>
              <Button onClick={__This.HandleClose} color="primary">
                {__This.state.Language.Cancel}
              </Button>
            </DialogActions>
          </Dialog>
        </div>
      );
    }
  }, {});

export default withStyles(LanguageEditModalStyles)(TLanguageEditModal)



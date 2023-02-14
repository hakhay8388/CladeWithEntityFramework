import React, { Component } from 'react';
import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../../../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../../../TagComponents/TObject"
import { CommandInterfaces } from "../../../../GenericWebController/CommandInterpreter/cCommandInterpreter"
import Actions from "../../../../GenericWebController/ActionGraph/Actions"
import GenericWebGraph from "../../../../GenericWebController/GenericWebGraph"
import { CommandIDs } from "../../../../GenericWebController/CommandInterpreter/CommandIDs/CommandIDs"

import { withStyles } from "@mui/styles";
import BatchJobEditModalStyles from "../../../../../ScriptStyles/BatchJobEditModalStyles"

import {
    Grid, Button, TextField, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, LinearProgress,
    FormControlLabel, Switch
} from '@mui/material';


import TBaseDialogModal from "../../../Utilities/TBaseDialogModal";


var TBatchJobEditModal = Class(TBaseDialogModal,
  {
    ObjectType: ObjectTypes.Get("TBatchJobEditModal")
    ,
    constructor: function (_Props) {
      TBatchJobEditModal.BaseObject.constructor.call(this, _Props);
      this.state =
      {
        ...this.state,
        open: false,
        loading: false,
        ID: 0,
        ExecuteFirstWithoutWait: false,
        MaxRetryCount: 0,
        AutoAddExecution: false,
        StopAfterFirstExecution: false,
        TimePeriodMilisecond: 0,
        BatchJobName: ""
      };
      window.App.BatchJobEditModal = this;
    }
    ,
    Destroy: function () {
      TBatchJobEditModal.BaseObject.Destroy.call(this);
    }
    ,
    HandleLoadDetail: function (_ID) {
      var __This = this;


      Actions.GetBatchJobDetail(_ID, function (_Message) {
        CommandIDs.ResultItemCommand.RunIfHas(_Message, function (_Data) {
          var __BatchJobDetail = _Data.Item.BatchJobDetail;
          __This.setState({
            loading: false,
            ExecuteFirstWithoutWait: __BatchJobDetail.ExecuteFirstWithoutWait,
            MaxRetryCount: __BatchJobDetail.MaxRetryCount,
            AutoAddExecution: __BatchJobDetail.AutoAddExecution,
            StopAfterFirstExecution: __BatchJobDetail.StopAfterFirstExecution,
            TimePeriodMilisecond: __BatchJobDetail.TimePeriodMilisecond,
            BatchJobName: __BatchJobDetail.Name
          });

        });

      });


    }
    ,
/*
    Params : _ID
*/
    HandleClickOpen: function (_ParamObject, _CallbackOnClose) {
      TBatchJobEditModal.BaseObject.HandleClickOpen.call(this, _ParamObject, _CallbackOnClose);
      var __This = this;
      this.CallbackOnClose = _CallbackOnClose;
      __This.setState({
        open: true,
        ID: _ParamObject.ID,
        loading: true,
        ExecuteFirstWithoutWait: false,
        MaxRetryCount: 0,
        AutoAddExecution: false,
        StopAfterFirstExecution: false,
        TimePeriodMilisecond: 0,
        BatchJobName: ""
      }, () => {
        __This.HandleLoadDetail(__This.state.ID);
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
    Handle_Insert_Update: function () {
      var __This = this;
      Actions.SaveBatchJobDetail(__This.state.ID, __This.state.TimePeriodMilisecond, __This.state.ExecuteFirstWithoutWait, __This.state.AutoAddExecution, __This.state.StopAfterFirstExecution, __This.state.MaxRetryCount, function (_Message) {
        CommandIDs.SuccessResultCommand.RunIfHas(_Message, function (_Data) {
          __This.HandleClose();
        });
      });

    }
    ,
    HandleChange_Switch: function (event, _Name) {
      var __This = this;
      __This.setState({
        [_Name]: event.target.checked
      });
    },
    HandleChange_TextBox(event) {
      var __This = this;
      __This.setState({
        [event.target.name]: event.target.value
      });
    },
    render: function () {
      const { classes } = this.props;
      var __This = this;
      var __Params = TBatchJobEditModal.BaseObject.render.call(this);
      if (!__Params.NeedRender) {
        return  null;
      }
      return (
        <div>
          <Dialog maxWidth={"md"} classes={{ paper: classes.DialogModal}} open={__This.state.open}  onClose={__This.HandleInnerClose} aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">{__This.state.Language.BatchJobPageDetail}</DialogTitle>
            <DialogContent>
              <DialogContentText style={{ width: "1024px" }}>
                {/* Açıklama konulcaksa buraya konulmalı*/}
              </DialogContentText>
              {__This.state.loading ? (
                <LinearProgress />
              ) : (
                <React.Fragment>

                  <Grid container spacing={3}>
                    <Grid item xs={12}>

                      <TextField
                        id={"txt_Name"}
                        label={__This.state.Language.Name}
                        value={__This.state.Language[__This.state.BatchJobName]}
                        name={"BatchJobName"}
                        onChange={this.HandleChange_TextBox}
                        variant={"outlined"}
                        InputLabelProps={{
                          shrink: true,
                        }}
                        disabled
                        fullWidth
                      />

                    </Grid>
                    <Grid item xs={6}>
                      <TextField
                        id={"txt_TimePeriodMilisecond"}
                        label={__This.state.Language.TimePeriodMilisecond}
                        value={__This.state.TimePeriodMilisecond}
                        name={"TimePeriodMilisecond"}
                        onChange={this.HandleChange_TextBox}
                        variant={"outlined"}
                        type="number"
                        InputLabelProps={{
                          shrink: true,
                        }}
                        fullWidth
                      />
                    </Grid>
                    <Grid item xs={6}>
                      <TextField
                        id={"txt_MaxRetryCount"}
                        label={__This.state.Language.MaxRetryCount}
                        value={__This.state.MaxRetryCount}
                        name={"MaxRetryCount"}
                        onChange={this.HandleChange_TextBox}
                        variant={"outlined"}
                        type="number"
                        InputLabelProps={{
                          shrink: true,
                        }}
                        fullWidth
                      />
                    </Grid>
                    <Grid item xs={12}>
                      <FormControlLabel
                        value="top"
                        control={<Switch checked={this.state.ExecuteFirstWithoutWait} onChange={(_Event) => {
                          __This.HandleChange_Switch(
                            _Event,
                            "ExecuteFirstWithoutWait"
                          );
                        }} name="checkedC" />}
                        label={this.state.Language.ExecuteFirstWithoutWait}
                        labelPlacement="left"
                      />
                    </Grid>
                    <Grid item xs={12}>
                      <FormControlLabel
                        value="top"
                        control={<Switch checked={this.state.AutoAddExecution} onChange={(_Event) => {
                          __This.HandleChange_Switch(
                            _Event,
                            "AutoAddExecution"
                          );
                        }} name="checkedC" />}
                        label={this.state.Language.AutoAddExecution}
                        labelPlacement="left"
                      />
                    </Grid>
                    <Grid item xs={12}>
                      <FormControlLabel
                        value="top"
                        control={<Switch checked={this.state.StopAfterFirstExecution} onChange={(_Event) => {
                          __This.HandleChange_Switch(
                            _Event,
                            "StopAfterFirstExecution"
                          );
                        }} name="checkedC" />}
                        label={this.state.Language.StopAfterFirstExecution}
                        labelPlacement="left"
                      />
                    </Grid>


                  </Grid>
                </React.Fragment>
              )}

            </DialogContent>
            <DialogActions>
              <Button onClick={__This.Handle_Insert_Update} color="primary">
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

export default withStyles(BatchJobEditModalStyles)(TBatchJobEditModal)



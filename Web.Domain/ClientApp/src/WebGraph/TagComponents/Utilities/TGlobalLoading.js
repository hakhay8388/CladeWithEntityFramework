import React from 'react';
import {Class, ObjectTypes, JSTypeOperator} from "../../GenericCoreGraph/ClassFramework/Class"
import TObject from "../../TagComponents/TObject"
import GenericWebGraph from "../../GenericWebController/GenericWebGraph"

import { Grid, Dialog, DialogContent, CircularProgress } from "@mui/material";

var TGlobalLoading = Class(TObject,
  {
    ObjectType: ObjectTypes.Get("TGlobalLoading")
    ,
    constructor: function (_Props) {
      TGlobalLoading.BaseObject.constructor.call(this, _Props);
      this.state =
        {
        ...this.state,
          Text: this.state.Language.Loading,
          Modal: false
        }
      window.App.GlobalLoading = this;
    }
    ,
    Show: function (_Text)
    {
      if (JSTypeOperator.IsDefined(_Text) && JSTypeOperator.IsString(_Text))
      {
        this.setState({
          Text: _Text,
          Modal: true
        });
      }
      else
      {
        this.setState({
          Text: this.state.Language.Loading,
          Modal: true
        });
      }
    }
    ,
    Hide: function () {
      var __This = this;
      this.setState({
        Modal: false
      });
    }
    ,
    HandleOnResizeMain: function (_Size) {
      TGlobalLoading.BaseObject.HandleOnResizeMain.call(this, _Size);
      this.forceUpdate();
    }
    ,
    Destroy: function () {
      TGlobalLoading.BaseObject.Destroy.call(this);
    }
    ,
    HandleGetStyle: function () {
      var __Result = null;
      if (GenericWebGraph.MainContainerSize.Height < 350) {
        __Result = {
          bottom: "120px",
          zIndex: 99999999
        };
      }
        else if (GenericWebGraph.MainContainerSize.Height >= 350 && GenericWebGraph.MainContainerSize.Height < 550) {
        __Result = {
          bottom: "200px",
          zIndex: 99999999
        };
      }
      else {
        __Result = {
          bottom: "400px",
          zIndex: 99999999
        };
      }
      return __Result;
    }
    ,
    render: function () {
      return (
        <Dialog open={this.state.Modal} style={this.HandleGetStyle()}>
          <DialogContent style={{ WebkitBoxShadow: "0px 0px 23px 0px #6D6D6D", boxShadow: "0px 0px 23px 0px #6D6D6D", paddingTop : "14px" }}>
            {/*<CircularProgress/>*/}
            <Grid
              container
              direction="row"
              justifyContent="center"
              alignItems="center"
              style={{ height: "70px" }}
            >
              <Grid item>
                          <h5 style={{ color: "#969696", fontFamily: "Arial" }}>
                  {this.state.Text}{" "}
                </h5>
              </Grid>
              <Grid item style={{ paddingLeft: 10 }}>
                <CircularProgress color={"primary"}/>
              </Grid>
            </Grid>
          </DialogContent>
        </Dialog>
      );
    }
  }, {});

export default TGlobalLoading


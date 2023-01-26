import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, GlobalEval } from "../../../GenericCoreGraph/ClassFramework/Class";

var cActionID = Class(Object,
  {
    ID: 0
    , Name: ""
    , Info: ""
    , Parameters: null
    ,
    constructor: function (Int_ID, String_Name, String_Info, Bool_Enabled, Array_Parameters)
    {
      if (JSTypeOperator.IsNumeric(Int_ID) && JSTypeOperator.IsString(String_Name) && JSTypeOperator.IsBool(Bool_Enabled) && JSTypeOperator.IsString(String_Info) && JSTypeOperator.IsArray(Array_Parameters))
      {
        this.ID = Int_ID;
        this.Name = String_Name;
        this.Info = String_Info;
        this.Parameters = Array_Parameters;
      }
      else
      {
        DebugAlert.Show("cActionID Class'ı Oluşturulurken Tür Uyuşmazlığı Saptandı..!");
      }
    }
    ,
    Destroy: function ()
    {
      delete this.ID;
      delete this.LoginedAction;
      delete this.Name;
      delete this.Info;
      delete this.Parameters;
    }
  }, {});

export default cActionID;

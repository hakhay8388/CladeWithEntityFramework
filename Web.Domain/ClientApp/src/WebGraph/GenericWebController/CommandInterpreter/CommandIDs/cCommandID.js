import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../../../GenericCoreGraph/ClassFramework/Class"

var cCommandID = Class(Object,
    {
        CommandID: 0,
        CommandName: "",
        CommandInfo: "",
        Enabled: true,

        constructor: function (Int_CommandID, String_CommandName, String_CommandInfo, Bool_Enabled) {
            if (JSTypeOperator.IsNumeric(Int_CommandID) && JSTypeOperator.IsString(String_CommandName) && JSTypeOperator.IsBool(Bool_Enabled) && JSTypeOperator.IsString(String_CommandInfo)) {
                this.CommandID = Int_CommandID;
                this.CommandName = String_CommandName;
                this.CommandInfo = String_CommandInfo;
                this.Enabled = Bool_Enabled;
            }
            else {
                DebugAlert.Show("cCommandID Class'ı Oluşturulurken Tür Uyuşmazlığı Saptandı..!");
            }
        }
        ,
        Destroy: function () {
            delete this.CommandID;
            delete this.CommandName;
            delete this.CommandInfo;
        }
        ,
        RunIfHas: function (_MsgObject, _Function) {
            if (JSTypeOperator.IsFunction(_Function)) {
                for (var j = 0; j < _MsgObject.length; j++) {
                    if (this.CommandID == _MsgObject[j].ActionID.ID) {
                        _Function(_MsgObject[j].Data);
                    }
                }
            }
        }
        ,
        RunIfNotHas: function (_MsgObject, _Function) {
            var __Found = false;
            if (JSTypeOperator.IsFunction(_Function)) {
                for (var j = 0; j < _MsgObject.length; j++) {
                    if (this.CommandID == _MsgObject[j].ActionID.ID) {
                        __Found = true;
                    }
                }
            }
            if (!__Found) _Function(_MsgObject);
        }

    }, {});

export default cCommandID

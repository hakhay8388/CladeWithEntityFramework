import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../ClassFramework/Class"
import cBaseObject from "../BaseObject/cBaseObject"
import { WebGraph } from "../WebGraph/WebGraph"

var cList = Class(cBaseObject,
{
    InnerList: null,
    ListItemObject: null,
    ObjectType: ObjectTypes.cList,
    constructor: function(cObjType_Item)
    {
        cList.BaseObject.constructor.call(this);

        this.InnerList = new Array();
        try
        {
            var __Deneme = cObjType_Item.ObjectTypeID;
        }
        catch (ex)
        {
            DebugAlert.Show("Liste Oluşturulurken ObjectTypes Altındaki Tiplerden Seçilmelidir...\nÖrnek : ObjectTypes.Integer");
        }

        this.ListItemObject = cObjType_Item;
    }
    ,
    Add: function(Object_Item)
    {
        if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
        {
            this.InnerList.push(Object_Item);
        }
        else
        {
            DebugAlert.Show("cList.Add Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\nEklenmek İstenen Tür : " + Object_Item.ToString());
        }
    }
    ,
    Insert: function(Insert_Index, Object_Item)
    {
        if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
        {
            var __NewList = new Array();
            var __Added = false;
            var __Count =  this.InnerList.length
            for (var i = 0; i < __Count; i++)
            {
                if (Insert_Index == i)
                {
                    __NewList.push(Object_Item);
                    __Added = true;
                    __Count++;
                }
                else
                {
                    if (__Added)
                    {
                        __NewList.push(this.InnerList[i - 1]);
                    }
                    else
                    {
                        __NewList.push(this.InnerList[i]);
                    }
                }
            }
            if (!__Added)
            {
                this.Add(Object_Item);
                return;
            }
            delete this.InnerList;
            this.InnerList = __NewList;
        }
        else
        {
            DebugAlert.Show("cList.Insert Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\nEklenmek İstenen Tür : " + Object_Item.ToString());
        }
    }
    ,
    Count: function()
    {
        return this.InnerList.length;
    }
    ,
    Remove: function(Object_Item)
    {
//        if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
//        {
            var __RemoveIndex = this.InnerList.indexOf(Object_Item);
            if (__RemoveIndex != -1)
            {
                this.InnerList.splice(__RemoveIndex, 1);
            }
        /*}
        else
        {
            DebugAlert.Show("cList.Remove Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\Silinmek İstenen Tür : " + Object_Item.ToString());
        }*/
    },
    RemoveRange: function(Number_RemoveStartIndex, Number_Count)
    {
        if (JSTypeOperator.IsNumeric(Number_RemoveStartIndex) && JSTypeOperator.IsNumeric(Number_Count))
        {
            if (Number_RemoveStartIndex + Number_Count > this.Count())
            {
                DebugAlert.Show("cList.RemoveRange Fonksiyonunda liste Aşıma Uğradı..!");
            }
            else
            {
                this.InnerList.splice(Number_RemoveStartIndex, Number_Count)
            }
        }
        else
        {
            DebugAlert.Show("cList.RemoveRange Fonksiyonunda Numerik Aralık Verilmeli..!");
        }
    },
    RemoveAt: function(Number_RemoveIndex)
    {
        if (JSTypeOperator.IsNumeric(Number_RemoveIndex))
        {
            if (Number_RemoveIndex > (this.Count() - 1))
            {
                DebugAlert.Show("cList.RemoveAt Fonksiyonunda Liste Aşıma Uğradı..!");
            }
            else
            {
                this.InnerList.splice(Number_RemoveIndex, 1);
            }
        }
        else
        {
            DebugAlert.Show("cList.RemoveAt Fonksiyonuna Sayısal Bir Değer Gönderilmedi..!");
        }
    }
    ,
    ForEach: function (_Function)
    {
        for (var i = 0; i < this.InnerList.length; i++)
        {
            _Function(this.InnerList[i]);
        }
    }
    ,
    Find: function (_Function)
    {
        for (var i = 0; i < this.InnerList.length; i++)
        {
            if (_Function(this.InnerList[i]))
            {
                return this.InnerList[i];
            }
        }
        return false;
    }
    ,
    RemoveAll: function (_Function)
    {
        for (var i = this.InnerList.length - 1; i > -1; i--)
        {
            if (_Function(this.InnerList[i]))
            {
                this.InnerList.splice(i, 1);
            }
        }
        return false;
    }
    ,
    IsContain: function (_Item) {
        for (var i = 0; i < this.InnerList.length; i++)
        {
            if (this.InnerList[i] == _Item)
            {
                return true;
            }
        }
        return false;
    }
    ,
    Clear: function()
    {
        this.InnerList.splice(0, this.Count());
    }
    ,
    IndexOf: function(_Object)
    {
        return this.InnerList.indexOf(_Object);
    }
     ,
    GetItem: function(Number_Index)
    {
        if (JSTypeOperator.IsNumeric(Number_Index))
        {
            if (Number_Index > (this.Count() - 1))
            {
                DebugAlert.Show("cList.GetItem Fonksiyonunda Liste Aşıma Uğradı..!");
            }
            else
            {
                return this.InnerList[Number_Index];
            }
        }
        else
        {
            DebugAlert.Show("cList.GetItem Fonksiyonuna Sayısal Bir Değer Gönderilmeli..!");
        }
        return null;
    }
    ,
    SetItem: function(Number_Index, Object_Item)
    {
        if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
        {
            if (JSTypeOperator.IsNumeric(Number_Index))
            {
                if (Number_Index > (this.Count() - 1))
                {
                    DebugAlert.Show("cListItem.SetItem Fonksiyonunda Liste Aşıma Uğradı..!");
                }
                else
                {
                    this.InnerList[Number_Index] = Object_Item;
                }
            }
            else
            {
                DebugAlert.Show("cListItem.SetItem Index Numerik Olmalı..!");
            }
        }
        else
        {
            DebugAlert.Show("cListItem.SetItem Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\Setlenmek İstenen Tür : " + Object_Item.ToString());
        }
    },
    Destroy: function()
    {
        this.Clear();
        delete this.InnerList;
        delete this.ListItemObject;
        cList.BaseObject.Destroy.call(this);        
    }
    ,
    DestroyWithItems: function()
    {
        var __Count = this.Count();
        for (var i = __Count - 1; i > -1; i--)
		{
		    var __Item = this.InnerList[i];
		    __Item.Destroy();
		}
		this.Clear();
        delete this.InnerList;
        cBaseObject.prototype.Destroy.call(this);        
    }
    ,
    SafeDestroyWithItems: function()
    {
        var __Count = this.Count();
        for (var i = __Count - 1; i > -1; i--)
		{
		    var __Item = this.InnerList[i];
		    __Item.SafeDestroy();
		}
		this.Clear();
        delete this.InnerList;
        cBaseObject.prototype.Destroy.call(this);        
    }
    ,
    KeyListDestroyer :function(_List)
	{
        for (var i = 0; i<  _List.Count(); i++)
		{
		    var __Item = _List.GetItem(i);
		    __Item.Destroy();
		}
    }
    ,
    ToComponent: function ()
    {
        const __ResultItems = this.InnerList.map((_Item, _Index) =>
        {
            return _Item.render();
        });
        return __ResultItems;
    }
    ,
    render()
    {
        return (
            this.ToComponent()
        );
    }

},{});


export default cList



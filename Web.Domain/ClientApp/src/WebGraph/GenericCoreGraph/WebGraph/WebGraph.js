import { JSTypeOperator, DebugAlert, Class, Interface, Abstract, ObjectTypes, cListForBase } from "../ClassFramework/Class"

var WebGraphClass = function ()
{
}

WebGraphClass.prototype.Init = function ()
{
  if (WebGraphClass.prototype.LastObjectID == undefined)
  {
    WebGraphClass.prototype.LastObjectID = 0;
    WebGraphClass.prototype.ObjectList = new cListForWebGraph();
  }
}


WebGraphClass.prototype.GetNewCreateID = function ()
{
  WebGraphClass.prototype.LastObjectID++;
  return WebGraphClass.prototype.LastObjectID;
}

WebGraphClass.prototype.ControlBaseClass = function (Object_DerivedClass, ObjectType)
{
  try
  {
    if (Object_DerivedClass.GetTypeID() == ObjectType.ObjectTypeID)
    {
      return true;
    }
    else if (Object_DerivedClass.GetTypeID() == ObjectTypes.cBaseObject.ObjectTypeID)
    {
      return false;
    }
    else
    {
      return WebGraphClass.prototype.ControlBaseClass(Object_DerivedClass.BaseObject(), ObjectType);
    }
  }
  catch (ex)
  {
    return false;
  }
}


WebGraphClass.prototype.GetInstancesByBaseClass = function (_BaseClassType)
{
  var __Result = [];
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
    /*try
    {
      console.log(_BaseClassType.IsInstance(__Item));
      console.log(__Item.GetObjectType().ObjectName);
    }
    catch(_Ex)
    {      
      console.log(_Ex);
    }*/

    if (WebGraph.ControlBaseClass(__Item, _BaseClassType))
    {
      __Result.push(__Item);
    }
  }
  return __Result;
}


WebGraphClass.prototype.GetMyBaseClass = function (Object_DerivedClass)
{
  if (Object_DerivedClass.GetTypeID() == ObjectTypes.cBaseObject.ObjectTypeID)
  {
    return Object_DerivedClass;
  }
  else
  {
    return WebGraphClass.prototype.GetMyBaseClass(Object_DerivedClass.BaseObject);
  }
}

WebGraphClass.prototype.Add = function (Object)
{
  WebGraphClass.prototype.ObjectList.Add(Object);
  if (WebGraphClass.prototype.ObjectList.Count() > 1000000)
  {
    DebugAlert.Show("Web Obje Sayısı 1.000.000'nu Aştı..!\nSorun Çıkmıyorsa Hata Obje Sayısını Arttırın veya Obje Azaltın...");
  }
}

WebGraphClass.prototype.Remove = function (Object)
{
  WebGraphClass.prototype.ObjectList.Remove(Object);
}

WebGraphClass.prototype.GetItemIndexByCreateID = function (Number_CreateID)
{
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
    if (__Item.CreateID == Number_CreateID)
    {
      return i;
    }
  }
  return -1;
}

WebGraphClass.prototype.ForceUpdateAllForPop = function (_Force, _PageChanged)
{
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    try
    {
      var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
      if (__Item.OnUrlPop)
      {
        __Item.OnUrlPop(_Force, _PageChanged);
      }
    }
    catch (_Ex)
    {
    }
  }
}

WebGraphClass.prototype.ForceUpdateAllWithAsyncLoad = function (_Force)
{
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    try
    {
      var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
      if (__Item.AsyncLoad)
      {
        __Item.AsyncLoad(_Force);
      }
    }
    catch (_Ex)
    {
    }
  }
}

WebGraphClass.prototype.OnUrlChanged = function ()
{
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    try
    {
      var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
      if (__Item.OnUrlChanged)
      {
        __Item.OnUrlChanged();
      }
    }
    catch (_Ex)
    {
    }
  }
}

WebGraphClass.prototype.GetItemByCreateID = function (Number_CreateID)
{
  var __Index = WebGraphClass.prototype.GetItemIndexByCreateID(Number_CreateID);
  if (__Index == -1)
  {
    return null;
  }
  else
  {
    return WebGraphClass.prototype.ObjectList.GetItem(__Index);
  }
}

WebGraphClass.prototype.GetItemIndexByCID = function (Number_CID)
{
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
    if (__Item.CID == Number_CID)
    {
      return i;
    }
  }
  return -1;
}

WebGraphClass.prototype.GetItemsByType = function (_Type)
{
  var __Result = new cListForBase();
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
    if (_Type && _Type.ObjectTypeID && __Item.ObjectType.ObjectTypeID == _Type.ObjectTypeID)
    {
      __Result.Add(__Item);
    }
  }
  return __Result;
}
WebGraphClass.prototype.GetItemsByName = function (_Type)
{
  var __Result = new cListForBase();
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __Item = WebGraphClass.prototype.ObjectList.GetItem(i);
    if (__Item.ObjectType.ObjectName == _Type)
    {
      __Result.Add(__Item);
    }
  }
  return __Result;
}



WebGraphClass.prototype.GetItemByCID = function (Number_CID)
{
  var __Index = WebGraphClass.prototype.GetItemIndexByCID(Number_CID);
  if (__Index == -1)
  {
    return null;
  }
  else
  {
    return WebGraphClass.prototype.ObjectList.GetItem(__Index);
  }
}

WebGraphClass.prototype.ShowObjectList = function ()
{
  var __String = "";
  for (var i = 0; i < WebGraphClass.prototype.ObjectList.Count(); i++)
  {
    var __TempObject = WebGraphClass.prototype.ObjectList.GetItem(i);
    __String += __TempObject.ToString() + "\n";
  }
  DebugAlert.Show(__String);
}

WebGraphClass.prototype.SetItemByCreateID = function (Number_CreateID, Object_Item)
{
  var __Index = WebGraphClass.prototype.GetItemIndexByCreateID(Number_CreateID);
  if (__Index == -1)
  {
    DebugAlert.Show("WebGraph.SetItem Fonsiyonunda Gönderderilen CreateID Bulunamadı..!");
  }
  else
  {
    WebGraphClass.prototype.ObjectList.SetItem(__Index, Object_Item);
  }
}

WebGraphClass.prototype.DeleteItemByCreateID = function (Number_CreateID)
{
  var __Index = WebGraphClass.prototype.GetItemIndexByCreateID(Number_CreateID);
  delete WebGraphClass.prototype.ObjectList.InnerList[__Index];
  WebGraphClass.prototype.ObjectList.RemoveAt(__Index);
}

export let WebGraph = new WebGraphClass();
window.WebGraph = WebGraph;

var cListForWebGraph = Class(Object,
  {
    InnerList: null,
    ListItemObject: ObjectTypes.cBaseObject,
    constructor: function ()
    {
      this.InnerList = new Array();
      this.ListItemObject = ObjectTypes.cBaseObject;
    }
    ,
    Add: function (Object_Item)
    {
      if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
      {
        this.InnerList.push(Object_Item);
      }
      else
      {
        try
        {
          DebugAlert.Show("cListForWebGraph.Add Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\nEklenmek İstenen Tür : " + Object_Item.ToString());
        }
        catch (_Ex)
        {
          DebugAlert.Show("ObjectTypes Eklenmemiş Tür olabilir, Lütfen ObjectNames Sınıfına Ekleyin..", _Ex);
        }
      }
    }
    ,
    Insert: function (Insert_Index, Object_Item)
    {
      if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
      {
        var __NewList = new Array();
        var __Added = false;
        for (var i = 0; i < this.InnerList.length; i++)
        {
          if (Insert_Index == i)
          {
            __NewList.push(Object_Item);
            __Added = true;
          }
          else
          {
            __NewList.push(this.InnerList[i]);
          }
        }
        if (!__Added)
        {
          this.Add(Object_Item);
        }
        delete this.InnerList;
        this.InnerList = __NewList;
      }
      else
      {
        DebugAlert.Show("cListForWebGraph.Insert Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\nEklenmek İstenen Tür : " + Object_Item.ToString());
      }
    }
    ,
    Count: function ()
    {
      return this.InnerList.length;
    }
    ,
    Remove: function (Object_Item)
    {
      //		if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
      //		{
      var __RemoveIndex = this.InnerList.indexOf(Object_Item);
      if (__RemoveIndex != -1)
      {
        this.InnerList.splice(__RemoveIndex, 1);
      }
      /*		}
              else
              {
                  DebugAlert.Show("cListForWebGraph.Remove Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\Silinmek İstenen Tür : " + Object_Item.ToString());
              }*/
    },
    RemoveRange: function (Number_RemoveStartIndex, Number_Count)
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
        DebugAlert.Show("cListForWebGraph.RemoveRange Fonksiyonunda Numerik Aralık Verilmeli..!");
      }
    },
    RemoveAt: function (Number_RemoveIndex)
    {
      if (JSTypeOperator.IsNumeric(Number_RemoveIndex))
      {
        if (Number_RemoveIndex > (this.Count() - 1))
        {
          DebugAlert.Show("cListForWebGraph.RemoveAt Fonksiyonunda Liste Aşıma Uğradı..!");
        }
        else
        {
          this.InnerList.splice(Number_RemoveIndex, 1);
        }
      }
      else
      {
        DebugAlert.Show("cListForWebGraph.RemoveAt Fonksiyonuna Sayısal Bir Değer Gönderilmedi..!");
      }
    }
    ,
    Clear: function ()
    {
      this.InnerList.splice(0, this.Count());
    }
    ,
    IndexOf: function (_Object)
    {
      return this.InnerList.indexOf(_Object);
    }
    ,
    GetItem: function (Number_Index)
    {
      if (JSTypeOperator.IsNumeric(Number_Index))
      {
        if (Number_Index > (this.Count() - 1))
        {
          DebugAlert.Show("cListForWebGraph.GetItem Fonksiyonunda Liste Aşıma Uğradı..!");
        }
        else
        {
          return this.InnerList[Number_Index];
        }
      }
      else
      {
        DebugAlert.Show("cListForWebGraph.GetItem Fonksiyonuna Sayısal Bir Değer Gönderilmeli..!");
      }
      return null;

    }
    ,
    SetItem: function (Number_Index, Object_Item)
    {
      if (WebGraph.ControlBaseClass(Object_Item, this.ListItemObject))
      {
        if (JSTypeOperator.IsNumeric(Number_Index))
        {
          if (Number_Index > (this.Count() - 1))
          {
            DebugAlert.Show("cListForWebGraph.SetItem Fonksiyonunda Liste Aşıma Uğradı..!");
          }
          else
          {
            this.InnerList[Number_Index] = Object_Item;
          }
        }
        else
        {
          DebugAlert.Show("cListForWebGraph.SetItem Index Numerik Olmalı..!");
        }
      }
      else
      {
        DebugAlert.Show("cListForWebGraph.SetItem Fonksiyonunda Tür Uyuşmazlığı..\nListe Turu : " + this.ListItemObject.ObjectName + "\Setlenmek İstenen Tür : " + Object_Item.ToString());
      }
    }
    ,
    Destroy: function ()
    {
      delete this.InnerList;
    }
  }, {});



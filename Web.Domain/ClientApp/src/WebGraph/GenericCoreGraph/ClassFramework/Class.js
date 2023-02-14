import { ObjectNames } from "../ObjectNames/ObjectNames"

export const DebugAlert = function ()
{
}

window.DebugAlert = DebugAlert;

DebugAlert.Enabled = false;
DebugAlert.WriteConsole = true;

DebugAlert.Show = function (String_Msg, _Ex)
{
  if (this.Enabled)
  {
    alert(String_Msg);
  }
  if (this.WriteConsole)
  {
    console.log(String_Msg);
  }
  if (_Ex)
  {

    console.log("######################################################");
    console.log(String_Msg);
    console.log("######################################################");
    console.log(_Ex);
    console.log("######################################################");
  }
}

export const ObjectTypeIDCreater = function ()
{
}


ObjectTypeIDCreater.ID = 0;
ObjectTypeIDCreater.GetID = function ()
{
  ObjectTypeIDCreater.ID++;
  return ObjectTypeIDCreater.ID;
}





export const Abstract = function ()
{
  DebugAlert.Show("Override Edilmeyen Abstract Fonksiyonlar Var..!")
}


var IsInterfaceInstanceFunctionHandler = function (_This)
{
  var This = _This;
  return function (_Object)
  {
    return BaseInterfaceControl(_Object, This);
  }
}

var IsClassInstanceFunctionHandler = function (_This)
{
  var This = _This;
  return function (_Object)
  {
    return BaseClassControl(_Object, This);
  }
}


var BaseObjectFunctionHandler = function (_BaseObject)
{
  var BaseObject = _BaseObject;
  return function ()
  {
    return BaseObject.prototype;
  }
}

var GetBinderConstructor = function (_ConstructorFunction)
{
  return function () {
    for (var __Function in this)
    {
      if (typeof this[__Function] === "function")
      {
        if (__Function.startsWith("Handle"))
        {
          this[__Function] = this[__Function].bind(this);
        }
        else
        {
            var __Temp = this[__Function];
            try {
                if (!IsDefined(__Temp.prototype) || !IsDefined(__Temp.prototype.ObjectType)) {
                    this[__Function] = this[__Function].bind(this);
                }
            }
            catch (e)
            {
                console.log(e);
            }
          
        }
      }
    }
    _ConstructorFunction.apply(this, arguments);
  }
};

export var Interface = {};

export const Class = function ()
{
  // inline overrides
  var __InlineOverrider = function (_Object)
  {
    for (var m in _Object)
    {
      this[m] = _Object[m];
    }
  };
  var __ObjectConstructor = Object.prototype.constructor;

  return function ()
  {
    var _DerivedClass = null;
    var _BaseClass = null;
    var _Overrides = null;


    _DerivedClass = arguments[0];
    _BaseClass = arguments[arguments.length - 2];
    _Overrides = arguments[arguments.length - 1];



    if (typeof _BaseClass == 'object')
    {
      _Overrides = _BaseClass;
      _BaseClass = _DerivedClass;

      if (_Overrides.constructor !== __ObjectConstructor)
      {
        _DerivedClass = GetBinderConstructor(_Overrides.constructor);
      }
      else
      {
        _DerivedClass = GetBinderConstructor(function ()
        {
          _BaseClass.apply(this, arguments);
        });
      }
    }

    var __FreeFunction = function () { };

    var __BaseClassPrototype = null;
    if (Object === _BaseClass)
    {
      __BaseClassPrototype = Object;
    }
    else
    {
      __BaseClassPrototype = _BaseClass.prototype;
    }


    __FreeFunction.prototype = __BaseClassPrototype;
    var __DerivedClassPrototype = _DerivedClass.prototype = new __FreeFunction();
    __DerivedClassPrototype.constructor = _DerivedClass;

    _DerivedClass.BaseObject = __BaseClassPrototype;

    if (__BaseClassPrototype.constructor === __ObjectConstructor)
    {
      __BaseClassPrototype.constructor = _BaseClass;
    }

    _DerivedClass.Override = function (_Object)
    {
      Override(_DerivedClass, _Object);
    };

    __DerivedClassPrototype.Override = __InlineOverrider;

    var __BaseObjectHandler = new BaseObjectFunctionHandler(_BaseClass);
    _DerivedClass.Override({
      BaseObject: __BaseObjectHandler
    });

    Override(_DerivedClass, _Overrides);

    if (arguments.length > 3)
    {
      for (var i = arguments.length - 3; i > 0; i--)
      {
        var __ControlSameMethod = false;
        for (var j = i - 1; j > -1; j--)
        {
          if (ControlInterfaceSameMethod(arguments[i], arguments[j]))
          {
            __ControlSameMethod = true;
            break;
          }
        }
        if (!__ControlSameMethod)
        {
          OverrideInterface(_DerivedClass, arguments[i].prototype);
        }
      }
    }


    _DerivedClass.Extend = function (_Object)
    {
      Class(_DerivedClass, _Object, {});
    };

    if (_BaseClass === Interface)
    {
      _DerivedClass.IsInstance = new IsInterfaceInstanceFunctionHandler(__DerivedClassPrototype);
      _DerivedClass.IsAssignableFrom = new IsInterfaceInstanceFunctionHandler(__DerivedClassPrototype);
    }
    else
    {
      _DerivedClass.IsInstance = new IsClassInstanceFunctionHandler(__DerivedClassPrototype);
      _DerivedClass.IsAssignableFrom = new IsClassInstanceFunctionHandler(__DerivedClassPrototype);
    }

    return _DerivedClass;
  };
}();


var BaseInterfaceControl = function (_Object, _Interface)
{
  var __Temp = null;
  eval("__Temp = _Object." + _Interface.ObjectType.ObjectName);
  if (__Temp === _Interface)
  {
    return true;
  }
  else
  {
    return false;
  }

}

var BaseClassControl = function (_Object, _BaseClass)
{
  try
  {
    if (_Object.ObjectType.ObjectTypeID === _BaseClass.ObjectType.ObjectTypeID)
    {
      return true;
    }
    else if (_Object.ObjectType.ObjectTypeID === ObjectTypes.cBaseObject.ObjectTypeID)
    {
      return false;
    }
    else
    {
      return BaseClassControl(_Object.BaseObject(), _BaseClass);
    }
  }
  catch (ex)
  {
    return false;
  }

}

var ControlInterfaceSameMethod = function (_Interface1, _Interface2)
{
  if (!_Interface1 || !_Interface2)
  {
    console.trace();
  }
  var ___Interface1Prototypes = _Interface1.prototype;
  var ___Interface2Prototypes = _Interface2.prototype;
  for (var _Method1 in ___Interface1Prototypes)
  {
    for (var _Method2 in ___Interface2Prototypes)
    {
      if (_Method1 === _Method2 && _Method1 !== "constructor" && _Method1 !== "Override" && _Method1 !== "ObjectType" && _Method1 !== "toJSONString" && _Method1 !== "parseJSON" && _Method1 !== "BaseObject")
      {
        DebugAlert.Show("BaseClass ve Interface'ler İçinde Aynı İsimde Methoda Rastlandı..!\nAynı Kullanılan Fonksiyon Adı '" + _Method1 + "' ");
        return true;
      }
    }
  }
  return false;
}


var OverrideInterface = function (_OriginalClass, _Overrides)
{
  if (_Overrides)
  {
    var __OriginalClassPrototypes = _OriginalClass.prototype;
    for (var __Method in _Overrides)
    {
      if (__Method !== "ObjectType")
      {
        if (__Method !== "constructor" && __Method !== "Override")
        {
          var __Found = false;
          for (var __OrginalClassMethod in __OriginalClassPrototypes)
          {
            if (__OrginalClassMethod === __Method)
            {
              var __Object1 = _Overrides[__Method];
              var __Object2 = __OriginalClassPrototypes[__OrginalClassMethod];

              if (JSTypeOperator.IsFunction(__Object1) && JSTypeOperator.IsFunction(__Object2))
              {
                __OriginalClassPrototypes[_Overrides["ObjectType"].ObjectName] = _Overrides;
                if (__Object1.length === __Object2.length)
                {
                  __Found = true;
                  break;
                }
                else
                {
                  DebugAlert.Show(_Overrides["InterfaceName"].ObjectName + "." + __Method + "() Fonksiyonu Interface'deki parametreden Farklı Parametre Alıyor.");
                }
              }
              else
              {
                if (!JSTypeOperator.IsFunction(__Object1))
                {
                  DebugAlert.Show(_Overrides["InterfaceName"].ObjectName + "." + __Method + "  Değişken Olarak Tanımlanmış.\nInterface İçinde Değişken Tanımlanamaz..!");
                }
                else if (!JSTypeOperator.IsFunction(__Object2))
                {
                  DebugAlert.Show(_Overrides["InterfaceName"].ObjectName + " Interface'nden Türetilen Class'ta" + __Method + " Değişken Olarak Tanımlanmış..!");
                }

              }
            }
          }
          if (!__Found)
          {
            DebugAlert.Show(__Method + " Override Edilmemiş..!");
          }
        }
      }
    }
  }
};


var Override = function (_OriginalClass, _Overrides)
{
  if (_Overrides)
  {
    var __Prototypes = _OriginalClass.prototype;
    for (var _Method in _Overrides)
    {
      __Prototypes[_Method] = _Overrides[_Method];
    }
  }
};


Interface = Class(Object,
{
}, {});


export const cListForBase = Class(Object,
  {
    InnerList: null,

    constructor: function ()
    {
      this.InnerList = new Array();
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
    Add: function (Object_Item)
    {
      this.InnerList.push(Object_Item);
    }
    ,
    Count: function ()
    {
      return this.InnerList.length;
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
    RemoveAllWithCallback: function (_Function, _Callback)
    {
      for (var i = this.InnerList.length - 1; i > -1; i--)
      {
        if (_Function(this.InnerList[i]))
        {
          if (JSTypeOperator.IsFunction(_Callback))
          {
            _Callback(this.InnerList[i]);
          }
          this.InnerList.splice(i, 1);
        }
      }
      return false;
    }
    ,
    Remove: function (Object_Item)
    {
      var __RemoveIndex = this.InnerList.indexOf(Object_Item);
      if (__RemoveIndex !== -1)
      {
        this.InnerList.splice(__RemoveIndex, 1);
      }
    }
    ,
    RemoveAt: function (Number_RemoveIndex)
    {
      this.InnerList.splice(Number_RemoveIndex, 1);
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
          DebugAlert.Show("cListItemForBase.GetItem Fonksiyonunda Liste Aşıma Uğradı..!");
        }
        else
        {
          return this.InnerList[Number_Index];
        }
      }
      else
      {
        DebugAlert.Show("cListItemForBase.GetItem Fonksiyonuna Sayısal Bir Değer Gönderilmeli..!");
      }
      return null;

    }
    ,
    SetItem: function (Number_Index, Object_Item)
    {
      if (JSTypeOperator.IsNumeric(Number_Index))
      {
        if (Number_Index > (this.Count() - 1))
        {
          DebugAlert.Show("cListItemForBase.SetItem Fonksiyonunda Liste Aşıma Uğradı..!");
        }
        else
        {
          this.InnerList[Number_Index] = Object_Item;;
        }
      }
      else
      {
        DebugAlert.Show("cListItemForBase.SetItem Index Numerik Olmalı..!");
      }
    }
    ,
    Destroy: function ()
    {
      delete this.InnerList;
    }
    ,
    DestroyWithItems: function ()
    {
      var __Count = this.Count();
      for (var i = __Count - 1; i > -1; i--)
      {
        var __Item = this.InnerList[i];
        __Item.Destroy();
      }
      this.Clear();
      delete this.InnerList;
    }

  }, {});




export const JSTypeOperator = function ()
{
}


// Type Identity function.
JSTypeOperator.IsArray = IsArray;
JSTypeOperator.IsDefined = IsDefined;
JSTypeOperator.IsEmpty = IsEmpty;
JSTypeOperator.IsFunction = IsFunction;
JSTypeOperator.IsNull = IsNull;
JSTypeOperator.IsNumeric = IsNumeric;
JSTypeOperator.IsObject = IsObject;
JSTypeOperator.IsString = IsString;
JSTypeOperator.IsBool = IsBool;




function IsBool(obj)
{
  return typeof (obj) == 'boolean';
}

//////////////////////////////////////////////////////////////////////////////
//	IsArray( obj )
//////////////////////////////////////////////////////////////////////////////
function IsArray(obj)
{
  return IsObject(obj) && obj.constructor == Array;
}


//////////////////////////////////////////////////////////////////////////////
//	IsDefined( obj )
//////////////////////////////////////////////////////////////////////////////
function IsDefined(obj)
{
  return typeof (obj) == 'undefined' ? false : true;
}


//////////////////////////////////////////////////////////////////////////////
//	IsEmpty( obj )
//////////////////////////////////////////////////////////////////////////////
function IsEmpty(obj)
{
  var retval = true;
  if (IsObject(obj))
  {
    for (var item in obj)
    {
      retval = false;
      break;
    }
  }

  return retval;
}

//////////////////////////////////////////////////////////////////////////////
//	IsFunction( obj )
//////////////////////////////////////////////////////////////////////////////
function IsFunction(obj)
{
  return typeof (obj) == 'function';
}


//////////////////////////////////////////////////////////////////////////////
//	IsNull( obj )
//////////////////////////////////////////////////////////////////////////////
function IsNull(obj)
{
  return IsObject(obj) && (obj == null);
}


//////////////////////////////////////////////////////////////////////////////
//	IsNumeric( obj )
//////////////////////////////////////////////////////////////////////////////
function IsNumeric(obj)
{
  return typeof (obj) == 'number' && isFinite(obj);
}


//////////////////////////////////////////////////////////////////////////////
//	IsObject( obj )
//////////////////////////////////////////////////////////////////////////////
function IsObject(obj)
{
  return typeof (obj) == 'object';
}


//////////////////////////////////////////////////////////////////////////////
//	IsString( obj )
//////////////////////////////////////////////////////////////////////////////
function IsString(obj)
{
  return typeof (obj) == 'string';
}



const ObjectTypesClass = function ()
{
}

ObjectTypesClass.prototype.TypeList = new cListForBase();


ObjectTypesClass.prototype.Get = function (_Name)
{
  for (var i = 0; i < ObjectTypesClass.prototype.TypeList.Count(); i++)
  {
    if (ObjectTypesClass.prototype.TypeList.GetItem(i).ObjectName === _Name)
    {
      return ObjectTypesClass.prototype.TypeList.GetItem(i);
    }
  }

  var __EvalString = "ObjectTypesClass.prototype." + _Name + " = new cObjType(\"" + _Name + "\");";
  eval(__EvalString);
  var __Result = null;
  __EvalString = "__Result = ObjectTypesClass.prototype." + _Name;
  eval(__EvalString);
  return __Result;
}

export const cObjType = Class(Object,
  {
    ObjectTypeID: 0,
    ObjectName: "",
    constructor: function (String_ObjectName)
    {
      ObjectTypes.TypeList.Add(this);
      if (JSTypeOperator.IsString(String_ObjectName))
      {
        this.ObjectTypeID = ObjectTypeIDCreater.GetID();
        this.ObjectName = String_ObjectName;
      }
      else
      {
        DebugAlert.Show("ObjType Class'ı Oluşturulurken Tür Uyuşmazlığı Saptandı..!");
      }
    }
    ,
    Destroy: function ()
    {
      delete this.ObjectTypeID;
      delete this.ObjectName;
    }
  }, {});


ObjectTypesClass.prototype.ValidateObjectInObjectList = function (_ObjectType)
{
  for (var i = 0; i < ObjectTypes.TypeList.Count(); i++)
  {
    if (_ObjectType.ObjectTypeID === ObjectTypes.TypeList.GetItem(i).ObjectTypeID)
    {
      return true;
    }
  }
  return false;
}

export const ObjectTypes = new ObjectTypesClass();

for (var i = 0; i < ObjectNames.List.length; i++)
{
  var __EvalString = "ObjectTypesClass.prototype." + ObjectNames.List[i] + " = new cObjType(\"" + ObjectNames.List[i] + "\");";
  eval(__EvalString);
}


String.prototype.format = function ()
{
  var __Formatted = this;
  for (var i = 0; i < arguments.length; i++)
  {
    var regexp = new RegExp('\\{' + i + '\\}', 'gi');
    __Formatted = __Formatted.replace(regexp, arguments[i]);
  }
  return __Formatted;
};

export const GlobalEval = function (_JavaScript)
{
  if (window.execScript)
  {
    window.execScript(_JavaScript);
    return;
  }
  var fn = function ()
  {
    window.eval.call(window, _JavaScript);
  };
  fn();
};


export default {
  ObjectTypeIDCreater,
  Interface,
  Abstract,
  cObjType,
  cListForBase
}




function ObjectNameListClass()
{
}

ObjectNameListClass.prototype.List = new Array(
    "cObjType",
    "cBaseObject",
    "cList",
    "cDelegate",
    "cTimer",
    "cObserverable",
    "cFreeObject",

    "cBaseValueType",
    "Integer",
    "Double",
    "Bool",
    "cString",

    //******************** REACT COMPONENET ****************************************
    "Login"
    //*************************************************************************

);

export const ObjectNames = new ObjectNameListClass();

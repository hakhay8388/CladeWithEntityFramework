import { DebugAlert, Class, Interface, Abstract, ObjectTypes, JSTypeOperator } from "../ClassFramework/Class"

var cDelegateFunctionItem = Class(Object,
{
    Sender : null
    , m_Processed : false
    ,
    constructor: function(_Sender, _Function)
    {
        this.Sender = _Sender;
        if (JSTypeOperator.IsFunction(_Function))
        {
            this.FunctionObject = _Function;
        }
        else
        {
            DebugAlert.Show("Delegate Nesnesine Fonksiyon Dışı Obje Ekleme İsteği Yapıldı..!");
        }
    }
	,
	Processed : function(_Value)
	{
        if (JSTypeOperator.IsDefined(_Value))
		{
			this.m_Processed = _Value;
		}
		else
		{
			return this.m_Processed; 
		}
	}
    ,    
    FunctionObject: function()
    {
        DebugAlert.Show("cDelegateFunctionItem Nesnesine Constructor'da Fonksiyon Atanmamış..!"); 
    }
    ,
    Destroy: function()
    {
        delete this.Sender;
        delete this.m_Processed;
    }
}, {});

export default cDelegateFunctionItem

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Bootstrapper.Boundary.nValueTypes.nConstType
{
    public class cBaseConstType<T> : IConstTypeType where T : IConstTypeType
    {
        //static int IdCounter = 0;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public cBaseConstType(string _Name, string _Code, int _ID)
        {
            //IdCounter++;
            ID = _ID;
            Code = _Code;
            Name = _Name;
        }
        public override string ToString()
        {
            return Name;
        }

        protected static DataTable Table(List<T> _List) 
        {
            return _List.ToDataTable<T>();
        }

        protected static T GetByID(List<T> _List, int _ID, T _Default)
        {
            T __Item =  _List.Find(_Item => _Item.ID == _ID);            
            return __Item != null ? __Item : _Default;
        }

        protected static T GetByName(List<T> _List, string _Name, T _Default)
        {
            T __Item = _List.Find(_Item => _Item.Name == _Name);
            return __Item != null ? __Item : _Default;
        }

        protected static T GetByCode(List<T> _List, string _Code, T _Default)
        {
            T __Item = _List.Find(_Item => _Item.Code == _Code);
            return __Item != null ? __Item : _Default;
        }

        protected static string GetVariableName(Expression<Func<T>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }

    }
}

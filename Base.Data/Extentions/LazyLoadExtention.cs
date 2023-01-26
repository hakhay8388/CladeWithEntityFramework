using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class PocoLoadingExtensions
{
    public static TRelatedEntity Load<TRelatedEntity>(this Action<object, string> _Loader, object _Entity, ref TRelatedEntity _NavigationField, [CallerMemberName] string _NavigationName = null)
        where TRelatedEntity : class
    {
        _Loader.Invoke(_Entity, _NavigationName);

        return _NavigationField;
    }


    public static void Load(this Action<object, string> _Loader,  object _Entity, [CallerMemberName] string _NavigationName = null)
    {
        _Loader.Invoke(_Entity, _NavigationName);
    }
}

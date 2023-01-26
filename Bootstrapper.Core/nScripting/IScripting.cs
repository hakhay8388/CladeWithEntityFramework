using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nScripting
{
    public interface IScripting
    {
        /// <summary>
        /// istenilen kodların tamamı bir metodun içine yazılıyormuş gibi yazılmalı, sonra sonucu "Result" içine atılması gerekiyor.
        /// /// istenildiği kadar parametre gönderilebilir. Parametreler sırası ile, _Param0, _Param1 _Param2,....
        /// diye devam eder.
        /// Örnek : 
        /// string __Temp = DenemeServiceContext.Scripting.EvalCode<string>("Result = Console.ReadLine();", new object[] {});
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_CodeSnippet"></param>
        /// <param name="_Args"></param>
        /// <returns></returns>
        T EvalCode<T>(string _CodeSnippet, params object[] _Args);

        /// <summary>
        /// istenilen kodların tamamı bir metodun içine yazılıyormuş gibi yazılmalı.
        /// istenildiği kadar parametre gönderilebilir. Parametreler sırası ile, _Param0, _Param1 _Param2,....
        /// diye devam eder.
        /// Örnek :
        /// DenemeServiceContext.Scripting.EvalCode("Console.WriteLine(_Param0);", __Temp);
        /// </summary>
        /// <param name="_CodeSnippet"></param>
        /// <param name="_Args"></param>
        void EvalCode(string _CodeSnippet, params object[] _Args);

        /// <summary>
        /// Script yazıldıysa ve script içinden bir metot çağrılacaksa veya instance oluşturulu bir method çağıma işmi gerekiyorsa
        /// bu metot kullanılır.
        /// Örnek :
        /// DenemeServiceContext.Scripting.ExecMethod<int>("Scripting.MyDeneme.Mesaj", DenemeServiceContext);
        /// Eğer bu kod varsa çalıştır yoksa hata vermesin isteniliyorsa başına "?" konulur.
        /// Örnek :
        /// DenemeServiceContext.Scripting.ExecMethod<int>("? Scripting.MyDeneme.Mesaj", DenemeServiceContext);
        /// Eğer c# içinde olan bir classtan instance oluşturulup onun içindeki birmethod çağrılacaksa yanı şekilde çağrılabilir
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="_MethodFullName"></param>
        /// <param name="_Args"></param>
        /// <returns></returns>
        TResult ExecMethod<TResult>(string _MethodFullName, params object[] _Args);
    }
}

using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nUtils.nHashUtils
{
    public class cHashUtils : cCoreObject
    {
        public cHashUtils(nApplication.cApp _App)
            :base(_App)
        {
            App.Factories.ObjectFactory.RegisterInstance<cHashUtils>(this);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHashUtils>(this);
        }

        public string GetChecksum(string _Value)
        {
            using (System.Security.Cryptography.SHA1Managed __Sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                var __Hash = __Sha1.ComputeHash(Encoding.UTF8.GetBytes(_Value));
                var __StringBuilder = new StringBuilder(__Hash.Length * 2);

                foreach (byte __Byte in __Hash)
                {
                    __StringBuilder.Append(__Byte.ToString("X2"));
                }

                return __StringBuilder.ToString();
            }
        }
        public int GetRandomNumber(int _Min, int _Max)
        {
            Random __Random = new Random();
            int __Value = __Random.Next(_Min, _Max);

            return __Value;
        }
        public long GetRelativeNumber(long _Start, long _Id)
        {
            long __Value = _Start + _Id;
            return __Value;
        }
        public int GetDigitHashValue(string _Value)
        {
            int __Shorthash = _Value.GetHashCode() % 2000000000; 
            if (__Shorthash < 0) __Shorthash *= -1;
            return __Shorthash;
        }
    }
}

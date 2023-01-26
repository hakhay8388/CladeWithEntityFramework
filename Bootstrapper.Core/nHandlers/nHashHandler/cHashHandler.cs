using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace Bootstrapper.Core.nHandlers.nHashHandler
{
    public class cHashHandler : cCoreObject
    {
        public cHashHandler(nApplication.cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHashHandler>(this);
        }

        public string GetHashValue(string value)
        {
            using (System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        public int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            int value = random.Next(min, max);

            return value;
        }
        public long GetRelativeNumber(long start, long id)
        {
            long value = start + id;
            return value;
        }
        public int GetDigitHashValue(string value)
        {
            int shorthash = value.GetHashCode() % 2000000000;
            if (shorthash < 0) shorthash *= -1;
            return shorthash;
        }

    }
}

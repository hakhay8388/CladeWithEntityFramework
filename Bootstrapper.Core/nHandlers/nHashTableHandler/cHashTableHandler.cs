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

namespace Bootstrapper.Core.nHandlers.nHashTableHandler
{
    public class cHashTableHandler : cCoreObject
    {
        public cHashTableHandler(nApplication.cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHashTableHandler>(this);
        }

        public void SaveHashTableToFile(Hashtable _Table, String _FileName)
        {
            FileStream __FileStream = new FileStream(_FileName, FileMode.Create);
            StreamWriter __StreamWriter = new StreamWriter(__FileStream);
            foreach (DictionaryEntry __Entry in _Table)
            {
                __StreamWriter.WriteLine("[" + __Entry.Key + "]#=#[" + __Entry.Value + "]");
            }
            __StreamWriter.Close();
            __FileStream.Close();
        }

        public Hashtable LoadHashTableFromFile(String _FileName)
        {
            if (!File.Exists(_FileName))
            {
                Hashtable __Hashtable = new Hashtable();
                SaveHashTableToFile(__Hashtable, _FileName);
            }

            StreamReader __StreamReader = new StreamReader(_FileName);
            Hashtable __Result = new Hashtable();
            String __Line = "";
            Regex __Splitter = new Regex("#=#");
            while ((__Line = __StreamReader.ReadLine()) != null)
            {
                String[] __Columns = __Splitter.Split(__Line);
                __Columns[0] = RemoveWrapper(__Columns[0]);
                __Columns[1] = RemoveWrapper(__Columns[1]);
                __Result.Add(__Columns[0], __Columns[1]);
            }
            __StreamReader.Close();
            return __Result;
        }

        private String RemoveWrapper(String _Value)
        {
            _Value = _Value.Remove(0, 1);
            _Value = _Value.Remove(_Value.Length - 1, 1);
            return _Value;
        }

        public int GetLineCount(string _FileName)
        {
            int __Count = 0;
            string __Line;
            TextReader __Reader = new StreamReader(_FileName);
            while ((__Line = __Reader.ReadLine()) != null)
            {
                __Count++;
            }
            __Reader.Close();
            return __Count;
        }
    }
}

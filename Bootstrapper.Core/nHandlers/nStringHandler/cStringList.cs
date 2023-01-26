using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nStringHandler
{
    public class cStringList
    {
        public List<string> Items { get; set; }
        private string m_Delimiter;
        // new StringItemList() : empty list
        // new StringItemList("") : empty item {""}
        // new StringItemList(",") : two item {"", ""}
        public cStringList(string _Value, string _Delimiter)
        {
            this.m_Delimiter = _Delimiter;
            Items = string.IsNullOrEmpty(_Value) ? new List<string>() : _Value.Split(new string[] { _Delimiter }, StringSplitOptions.None).ToList();
        }
        public cStringList(List<string> _Items)
        {
            this.m_Delimiter = ",";
            Items = _Items;
        }
        public cStringList(string _Value)
            : this(_Value, ",")
        {
        }
        public cStringList()
            : this("", ",")
        {
        }
        public cStringList AppendItem(string _Item)
        {
            Items.Add(_Item);
            return this;
        }
        public string GetValue()
        {
            return Items.Count == 0 ? string.Empty : String.Join(m_Delimiter, Items.ToArray());
        }
        public string GetValue(string _Delimiter)
        {
            return String.Join(_Delimiter, Items.ToArray());
        }
        public int FindItem(string _Item)
        {
            return FindItem(_Item, 0);
        }
        public int FindLastItem(string _Item)
        {
            int __CountItem = CountItem(_Item);
            int __LastFoundIndex = FindItem(_Item, __CountItem - 1);
            return __LastFoundIndex;
        }
        /// <summary>
        /// Baştan itibaren kaçıncıyı bulmak istiyorsun
        /// </summary>
        /// <param name="_Item"></param>
        /// <param name="_FindCountFromFirst"></param>
        /// <returns></returns>
        public int FindItem(string _Item, int _FindCountFromFirst)
        {
            if (_FindCountFromFirst >= 0)
            {
                int __Count = -1;

                for (int __StartIndex = 0; __StartIndex < Items.Count; )
                {
                    int __FoundIndex = Items.FindIndex(__StartIndex, x => x.Equals(_Item, StringComparison.InvariantCultureIgnoreCase));

                    if (__FoundIndex >= 0)
                    {
                        __Count++;
                        __StartIndex = __FoundIndex + 1;
                    }
                    else
                        break;

                    if (__Count == _FindCountFromFirst)
                    {
                        return __FoundIndex;
                    }
                }
            }

            return -1;
        }
        public int CountItem(string _Item)
        {
            return Items.Count(x => x.Equals(_Item, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

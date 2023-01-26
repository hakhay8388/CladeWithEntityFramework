using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.FileData.nDatabaseFile.nAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Default : System.Attribute
    {
        public Object DefaultValue;
        public Default(Object _DefaultValue)
        {
            DefaultValue = _DefaultValue;
        }

        public bool IsInteger
        {
            get
            {
                return DefaultValue is sbyte
                    || DefaultValue is byte
                    || DefaultValue is short
                    || DefaultValue is ushort
                    || DefaultValue is int
                    || DefaultValue is uint
                    || DefaultValue is long
                    || DefaultValue is ulong;
            }
        }

        public bool IsDouble
        {
            get
            {
                return DefaultValue is float
                    || DefaultValue is double
                    || DefaultValue is decimal;
            }
        }

        public bool IsString
        {
            get
            {
                return DefaultValue is string;
            }
        }

        public bool IsDateTime
        {
            get
            {
                return DefaultValue is DateTime;
            }
        }
    }
}

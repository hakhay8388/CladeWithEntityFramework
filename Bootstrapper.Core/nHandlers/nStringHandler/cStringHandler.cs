using Bootstrapper.Boundary.nValueTypes.nConstType;
using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nApplication.nCoreLoggers;
//using Bootstrapper.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bootstrapper.Core.nHandlers.nStringHandler
{
    public class cStringHandler : cCoreObject
    {
        public cStringHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cStringHandler>(this);
        }

        public string ByteToHex(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
        public byte[] HexToByte(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

        public string GetRootDomain(string __HostName)
        {
            List<string> __HostNameSplited = __HostName.Split(".").ToList();
            if (__HostNameSplited[0].Length == 2 || __HostNameSplited[0].Length == 3)
            {
                __HostNameSplited[0] = "";
                __HostNameSplited.Remove("");
                __HostName = string.Join(".", __HostNameSplited);
            }
            return __HostName;
        }

        public string ByteToBase64(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
        public byte[] Base64ToByte(string data)
        {
            return Convert.FromBase64String(data);
        }
        public string GetVariableName(Expression<Func<object>> _Expr)
        {
            var __Body = (MemberExpression)_Expr.Body;
            return __Body.Member.Name;
        }


        

        public string GetRandomOrderNumber(string _UserID)
        {
            string __OrderCode = "";
            int __RandomNumber = 0;
            while (__RandomNumber.ToString().Length < 5)
            {
                Random rand = new Random();
                __RandomNumber = rand.Next(00000, 99999);

            }
            __OrderCode = "T3O" + _UserID.PadLeft(5, '0') + __RandomNumber.ToString();
            return __OrderCode;
        }
        public string CompareText(string text1, string text2)
        {
            string[] lines1 = Split(text1, "\r\n");
            string[] lines2 = Split(text2, "\r\n");

            string[] diff1 = lines2.Except(lines1).ToArray();
            string[] diff2 = lines1.Except(lines2).ToArray();

            string[] diff = diff1.Concat(diff2).Distinct().ToArray();
            string value = String.Join("\r\n", diff);

            return value;
        }
        public string[] Split(string baseString, string delimiter)
        {
            return Split(baseString, delimiter, true);
        }
        public string[] Split(string baseString, string delimiter, bool removeEmptyEntries)
        {
            StringSplitOptions options = removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            string[] result = (baseString ?? string.Empty).Split(new string[] { delimiter }, options);

            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();

            return result;
        }
        public byte[] StringToByte(string str)
        {
            if (str.IsNullOrEmpty())
                return new byte[] { };

            return Encoding.UTF8.GetBytes(str);

            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            //return bytes;
        }

        public string HunDate()
        {
            int __Year;
            int __Month;
            int __Day;
            int __Hour;
            int __Minute;
            int __Second;
            System.DateTime __Moment = new System.DateTime();
            __Moment = DateTime.Now;
            __Year = __Moment.Year;
            __Month = __Moment.Month;
            __Day = __Moment.Day;
            __Hour = __Moment.Hour;
            __Minute = __Moment.Minute;
            __Second = __Moment.Second;

            return __Year.ToString() + __Month.ToString().PadLeft(2, '0') + __Day.ToString().PadLeft(2, '0') + __Hour.ToString().PadLeft(2, '0') + __Minute.ToString().PadLeft(2, '0') + __Second.ToString().PadLeft(2, '0');
        }

        public string ByteToString(byte[] bytes)
        {
            return ByteToString(bytes, true);
        }
        public string ByteToString(byte[] bytes, bool removeUTF8Mark)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            if (removeUTF8Mark)
                return Encoding.UTF8.GetString(Remove_UTF8BomMark(bytes)); // generally for files saved by visualstudio
            else
                return Encoding.UTF8.GetString(bytes);
        }
        public byte[] Remove_UTF8BomMark(byte[] value)
        {
            byte[] mark = Encoding.UTF8.GetPreamble();
            bool equals = true;

            for (int i = 0; i < mark.Length; i++)
                if (mark[i] != value[i])
                {
                    equals = false;
                    break;
                }

            if (equals)
                return value.Skip(mark.Length).ToArray();
            else
                return value;
        }
        public string UsernameAndIdMd5(string _Email, string _ID)
        {
            string __HashString = _Email + _ID;
            return ComputeHashAsHex(__HashString);
        }
        public string ComputeHash(string data)
        {
            return ByteToString(ComputeHash(StringToByte(data)));
        }
        public string ComputeHashAsHex(string data)
        {
            return ByteToHex(ComputeHash(StringToByte(data)));
        }
        public byte[] ComputeHash(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }
        public Stream StringToStream(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
        public string StreamToString(Stream value)
        {
            StreamReader reader = new StreamReader(value);
            return reader.ReadToEnd();
        }
        public byte[] StreamToByte(Stream value)
        {
            value.Position = 0;
            MemoryStream memoryStream = new MemoryStream();
            value.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        public Stream ByteToStream(byte[] value)
        {
            return new MemoryStream(value);
        }
        public string ReplaceCharList(string str, string findCharList, string replaceCharList)
        {
            if (str == null)
                return null;

            string ret = String.Copy(str);

            for (int i = 0; i < findCharList.Length; i++)
                ret = ret.Replace(findCharList.Substring(i, 1), replaceCharList.Substring(i, 1));

            return ret;
        }
        public string GetTabPrefix(string baseStr, string findStr)
        {
            // returns a string of which length is equal to number of "tab" before "findStr", mostly used for replacement

            int i = baseStr.IndexOf(findStr);

            if (i == -1)
                return string.Empty;

            string value = string.Empty;

            for (int j = i - 1; j > -1 && baseStr.Substring(j, 1) == "\t"; j--)
                value += "\t";

            return value;
        }
        public string Repeat(string value, int repeat)
        {
            return new StringBuilder().Insert(0, value, repeat).ToString();
        }
        public string EncodeGetUniqueTimeToken()
        {
            byte[] __Time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] __Key = Guid.NewGuid().ToByteArray();
            string __Token = Convert.ToBase64String(__Time.Concat(__Key).ToArray());
            return __Token;
        }
        public bool DecodeGetUniqueTimeToken(string _Token, int _ValidatyHour = 72)
        {
            byte[] __Data = Convert.FromBase64String(_Token);
            DateTime __CreationTime = DateTime.FromBinary(BitConverter.ToInt64(__Data, 0));
            if (__CreationTime > DateTime.UtcNow.AddHours(_ValidatyHour * -1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ComputeLevenshteinDistance(string _Source, string _Target)
        {
            if ((_Source == null) || (_Target == null)) return 0;
            if ((_Source.Length == 0) || (_Target.Length == 0)) return 0;
            if (_Source == _Target) return _Source.Length;

            int __SourceWordCount = _Source.Length;
            int __TargetWordCount = _Target.Length;

            // Step 1
            if (__SourceWordCount == 0)
                return __TargetWordCount;

            if (__TargetWordCount == 0)
                return __SourceWordCount;

            int[,] __Distance = new int[__SourceWordCount + 1, __TargetWordCount + 1];

            // Step 2
            for (int i = 0; i <= __SourceWordCount; __Distance[i, 0] = i++) ;
            for (int j = 0; j <= __TargetWordCount; __Distance[0, j] = j++) ;

            for (int i = 1; i <= __SourceWordCount; i++)
            {
                for (int j = 1; j <= __TargetWordCount; j++)
                {
                    // Step 3
                    int cost = (_Target[j - 1] == _Source[i - 1]) ? 0 : 1;

                    // Step 4
                    __Distance[i, j] = Math.Min(Math.Min(__Distance[i - 1, j] + 1, __Distance[i, j - 1] + 1), __Distance[i - 1, j - 1] + cost);
                }
            }

            return __Distance[__SourceWordCount, __TargetWordCount];
        }

        public double CalculateSimilarity(string _Source, string _Target)
        {
            if ((_Source == null) || (_Target == null)) return 0.0;
            if ((_Source.Length == 0) || (_Target.Length == 0)) return 0.0;
            if (_Source == _Target) return 1.0;

            int __StepsToSame = ComputeLevenshteinDistance(_Source, _Target);
            return (1.0 - ((double)__StepsToSame / (double)Math.Max(_Source.Length, _Target.Length)));
        }

        public string GenerateUserNick(string _UserID)
        {
            string __Usernick = "";
            int __RandomNumber = 0;
            int __UserIDLength = _UserID.Length;
            Random __Random = new Random();
            __Usernick =
                __Random.Next((int)Math.Pow(10, 8 - __UserIDLength), (int)(Math.Pow(10, 9 - __UserIDLength) - 1)) +
                _UserID;
            return __Usernick;
        }

        public string ChangeFormatNameAndSurname(string __Value)
        {
            __Value = Regex.Replace(__Value, "\\s+", " ");
            return Regex.Replace(__Value, @"\w+[^(]\w*[a-z]\w*[^)]\w+", delegate(Match match)
            {
                string v = match.ToString();
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("tr-tr");
                return ci.TextInfo.ToTitleCase(v);
            }).Trim();
        }
    
    }
}

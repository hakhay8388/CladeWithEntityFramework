using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrapper.Core.nHandlers.nValidationHandler
{
    public class cValidationHandler : cCoreObject
    {
        public cValidationHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cValidationHandler>(this);
        }

        public bool IsValidEmail(string _Email)
        {
            if (_Email.IsNullOrEmpty())
            {
                return false;
            }
            try
            {
                var __Addr = new System.Net.Mail.MailAddress(_Email);
                return __Addr.Address == _Email;
            }
            catch(Exception _Ex)
            { 
				return false;
            }
        }
        public string CorrectNumber(string _Number)
        {
            if (_Number == null)
            {
                return "";
            }
            string __TmpNumber = _Number;
            __TmpNumber = __TmpNumber.ClearNonNumeric();
            if (__TmpNumber.Length == 12)
            {
                if (__TmpNumber.Substring(0, 2) == "90")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return __TmpNumber;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else if (__TmpNumber.Length == 11)
            {
                if (__TmpNumber.Substring(0, 2) == "05")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return "9" + __TmpNumber;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else if (__TmpNumber.Length == 10)
            {
                if (__TmpNumber.Substring(0, 1) == "5")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return "90" + __TmpNumber;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public bool IsValidPhoneNumber(string _Number)
        {
            if (_Number == null)
            {
                return false;
            }
            string __TmpNumber = _Number;
            __TmpNumber = __TmpNumber.ClearNonNumeric();
            if (__TmpNumber.Length == 12)
            {
                if (__TmpNumber.Substring(0, 2) == "90")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (__TmpNumber.Length == 11)
            {
                if (__TmpNumber.Substring(0, 2) == "05")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (__TmpNumber.Length == 10)
            {
                if (__TmpNumber.Substring(0, 1) == "5")
                {
                    if (__TmpNumber.IsNumeric())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsValidTCNumber(string text)
        {
            bool returnvalue = false;
            text = text.Replace(" ", "").Replace("\t", "");
            if (text.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;
                bool numarami = text.IsNumeric();
                if (numarami)
                {

                    TcNo = Int64.Parse(text);

                    ATCNO = TcNo / 100;
                    BTCNO = TcNo / 100;

                    C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                    Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                    Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                    returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);

                }
            }

            return returnvalue;
        }
    }
}

using Web.Domain.nUtils.nValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public enum EnumMessageButtons
    {
        YesNo = 1,
        Close = 2,
        YesNoClose = 3,
        OkCancel = 4,
        Ok = 5,
        YesNoCancel = 6,
        None = 7,
    }

    public class EMessageButtons : cBaseConstType<EMessageButtons>
    {
        public static List<EMessageButtons> TypeList { get; set; }

        public static EMessageButtons YesNo = new EMessageButtons("YesNo", EnumMessageButtons.YesNo);
        public static EMessageButtons Close = new EMessageButtons("Close", EnumMessageButtons.Close);
        public static EMessageButtons YesNoClose = new EMessageButtons("YesNoClose", EnumMessageButtons.YesNoClose);
        public static EMessageButtons OkCancel = new EMessageButtons("OkCancel", EnumMessageButtons.OkCancel);
        public static EMessageButtons Ok = new EMessageButtons("Ok", EnumMessageButtons.Ok);
        public static EMessageButtons YesNoCancel = new EMessageButtons("YesNoCancel", EnumMessageButtons.YesNoCancel);
        public static EMessageButtons None = new EMessageButtons("None", EnumMessageButtons.None);


        public EMessageButtons(string _Name, EnumMessageButtons _Value)
            : base(_Name, _Name,(int)_Value)
        {
            TypeList = TypeList ?? new List<EMessageButtons>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMessageButtons GetByID(EnumMessageButtons _ID, EMessageButtons _DefaultMessageButtons)
        {
            return GetByID(TypeList, (int)_ID, _DefaultMessageButtons);
        }
        public static EMessageButtons GetByName(string _Name, EMessageButtons _DefaultMessageButtons)
        {
            return GetByName(TypeList, _Name, _DefaultMessageButtons);
        }
    }
}

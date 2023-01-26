using Web.Domain.nUtils.nValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bootstrapper.Boundary.nValueTypes.nConstType;

namespace Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public enum EnumMessageButton
    {
        Yes = 1,
        No = 2,
        Close = 3,
        Cancel = 4,
        Ok = 5,
    }

    public class EMessageButton : cBaseConstType<EMessageButton>
    {

        public static List<EMessageButton> TypeList { get; set; }


        public static EMessageButton Yes = new EMessageButton("Yes", EnumMessageButton.Yes);
        public static EMessageButton No = new EMessageButton("No", EnumMessageButton.No);
        public static EMessageButton Close = new EMessageButton("Close", EnumMessageButton.Close);
        public static EMessageButton Cancel = new EMessageButton("Cancel", EnumMessageButton.Cancel);
        public static EMessageButton Ok = new EMessageButton("Ok", EnumMessageButton.Ok);

        public EMessageButton(string _Name, EnumMessageButton _ID)
            : base(_Name, _Name,(int)_ID)
        {
            TypeList = TypeList ?? new List<EMessageButton>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMessageButton GetByID(EnumMessageButton _ID, EMessageButton _DefaultMessageButton)
        {
            return GetByID(TypeList, (int)_ID, _DefaultMessageButton);
        }
        public static EMessageButton GetByName(string _Name, EMessageButton _DefaultMessageButton)
        {
            return GetByName(TypeList, _Name, _DefaultMessageButton);
        }
    }

}

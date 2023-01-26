using Bootstrapper.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Domain.nUtils.nValueTypes
{
    public enum EnumColorTypes
    {
        Default =  0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Warning = 4,
        Danger = 5,
        Info = 6,
        Light = 7,
        Dark =  8,
        Link =  9,
        None =  10,
        Error = 11
    }

    public class EColorTypes : cBaseConstType<EColorTypes>
    {

        public static List<EColorTypes> TypeList { get; set; }

        public static EColorTypes Default = new EColorTypes("default", EnumColorTypes.Default);
        public static EColorTypes Primary = new EColorTypes("primary" , EnumColorTypes.Primary);
        public static EColorTypes Secondary = new EColorTypes("secondary", EnumColorTypes.Secondary);
        public static EColorTypes Success = new EColorTypes("success", EnumColorTypes.Success);
        public static EColorTypes Warning = new EColorTypes("warning", EnumColorTypes.Warning);
        public static EColorTypes Danger = new EColorTypes("danger", EnumColorTypes.Danger);
        public static EColorTypes Info = new EColorTypes("info", EnumColorTypes.Info);
        public static EColorTypes Light = new EColorTypes("light", EnumColorTypes.Light);
        public static EColorTypes Dark = new EColorTypes("dark", EnumColorTypes.Dark);
        public static EColorTypes Link = new EColorTypes("link" , EnumColorTypes.Link);
        public static EColorTypes None = new EColorTypes("none", EnumColorTypes.None);
        public static EColorTypes Error = new EColorTypes("error", EnumColorTypes.Error);

        public EColorTypes(string _Name, EnumColorTypes _ColorTypes)
            : base(_Name, _Name, (int)_ColorTypes)
        {
            TypeList = TypeList ?? new List<EColorTypes>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EColorTypes GetByID(EnumColorTypes _ID, EColorTypes _DefaultCommandID)
        {
            return GetByID(TypeList, (int)_ID, _DefaultCommandID);
        }
        public static EColorTypes GetByName(string _Name, EColorTypes _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}

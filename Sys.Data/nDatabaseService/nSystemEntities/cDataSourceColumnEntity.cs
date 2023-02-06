using Base.Data.nDatabaseService.nDatabase;

namespace Sys.Data.nDatabaseService.nSystemEntities
{
    public class cDataSourceColumnEntity : cBaseEntity<cDataSourceColumnEntity>
    {
        public string ColumnName { get; set; }

        public string DataSourceCode { get; set; }

        public int DataSourceID { get; set; }

        public  List<cRoleEntity> Roles { get; set; }


    }
}

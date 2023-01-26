using Base.Data.nDatabaseService.nDatabase;

namespace Data.Domain.nDatabaseService.nSystemEntities
{
    public class cMenuEntity : cBaseEntity<cMenuEntity>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Icon { get; set; }

        public int SortValue { get; set; }

        public string MenuTypeCode { get; set; }

        public cMenuEntity? RootMenu { get; set; }

        public List<cRoleMenuMapEntity> Roles { get; set; }
        public cPageEntity? Page { get; set; }
    }
}

using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.FileData.nDatabaseFile.nEntities
{
    public class cGlobalSettingEntity : cFileEntity<cGlobalSettingEntity>
    {
        [Default(true)]
        public virtual bool ShowProgressBar { get; set; }

        [Default(1000)]
        public virtual int MaxConsoleLine { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.FileData.nFileDataService;

namespace Base.FileData.nDatabaseFile.nEntities
{
    public interface IFileEntity
    {
        long ID { get; set; }
        void Save();
        void Delete();
    }
}

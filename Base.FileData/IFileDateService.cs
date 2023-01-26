using Base.FileData.nDatabaseFile.nEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.FileData
{
    public interface IFileDateService
    {
        TFileEntity FindByID<TFileEntity>(int _ID = 0) where TFileEntity : IFileEntity;        
        void Save<TFileEntity>(TFileEntity _FileEntity) where TFileEntity : IFileEntity;

        void Delete<TFileEntity>(TFileEntity _FileEntity) where TFileEntity : IFileEntity;

        void DeleteAll<TFileEntity>() where TFileEntity : IFileEntity;
    }
}

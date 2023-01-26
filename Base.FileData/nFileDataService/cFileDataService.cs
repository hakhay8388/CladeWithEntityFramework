using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Base.FileData.nConfiguration;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;
using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;

namespace Base.FileData.nFileDataService
{
    [Register(typeof(IFileDateService), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cFileDataService : cCoreService<cFileDataServiceContext>, IFileDateService
    {
        public const string ColumnName_ID = "ID";
        public cFileDataService(cFileDataServiceContext _CoreServiceContext)
            : base(_CoreServiceContext)
        {
        }

        public TFileEntity FindByID<TFileEntity>(int _ID = 0) where TFileEntity : IFileEntity
        {
            lock (typeof(TFileEntity))
            {
                TFileEntity __FileEntity = App.Factories.ObjectFactory.ResolveInstance<TFileEntity>();
                App.Factories.ObjectFactory.ResolveInnerObject(__FileEntity);
                SetDefaultValue(__FileEntity);
                InitializeDatas<TFileEntity>(__FileEntity, _ID);
                return __FileEntity;
            }
        }

        public void Save<TFileEntity>(TFileEntity _FileEntity) where TFileEntity : IFileEntity
        {
            lock (typeof(TFileEntity))
            {
                CoreSave<TFileEntity>(_FileEntity);
            }
        }

        public void Delete<TFileEntity>(TFileEntity _FileEntity) where TFileEntity : IFileEntity
        {
            lock (typeof(TFileEntity))
            {
                CoreDelete<TFileEntity>(_FileEntity);
            }
        }

        public void DeleteAll<TFileEntity>() where TFileEntity : IFileEntity
        {
            lock(typeof(TFileEntity))
            {
                DeleteAll<TFileEntity>();
            }
        }

        private string GetFileDataPath(string _TableName)
        {
            return Path.Combine(App.Cfg<cFileDataConfiguration>().FileDataPath, _TableName);
        }

        private void InitializeDatas<TFileEntity>(IFileEntity _Object, int _ID = 0) where TFileEntity : IFileEntity
        {
            Type __Type = _Object.GetType();
            string __TableName = GetTableName<TFileEntity>();

            PropertyInfo __IsExistsMethod = __Type.GetProperty("IsExists");

            Hashtable __Hashtable = App.Handlers.HashTableHandler.LoadHashTableFromFile(GetFileDataPath(__TableName));
            if (__Hashtable.ContainsKey(ColumnName_ID + "_" + _ID.ToString()))
            {
                if (__IsExistsMethod != null) __IsExistsMethod.SetValue(_Object, true);
                foreach (PropertyInfo __Property in __Type.GetAllProperties())
                {
                    if (__Property.IsVirtual() == true)
                    {
                        Object __Value = __Hashtable[_ID.ToString() + "_" + __Property.Name];
                        if (__Value != null)
                        {
                            __Property.SetValue(_Object, Convert.ChangeType(__Value, __Property.PropertyType));
                        }
                    }

                }
            }
            else
            {
                if (__IsExistsMethod != null) __IsExistsMethod.SetValue(_Object, false);
            }
        }

        private void SetDefaultValue(IFileEntity _Object)
        {
            Type __Type = _Object.GetType();
            foreach (PropertyInfo __Property in __Type.GetAllProperties())
            {
                if (__Property.IsVirtual() == true)
                {
                    Default __Default = __Property.GetCustomAttribute<Default>();
                    if (__Default != null)
                    {
                        if (__Property.PropertyType == typeof(DateTime))
                        {
                            __Property.SetValue(_Object, DateTime.Today);
                        }
                        else
                        {
                            if (__Property.PropertyType == typeof(Decimal))
                            {
                                __Property.SetValue(_Object, Convert.ToDecimal(__Default.DefaultValue));
                            }
                            else
                            {
                                __Property.SetValue(_Object, __Default.DefaultValue);
                            }
                        }
                    }
                }
            }
        }

        private void CoreSave<TFileEntity>(IFileEntity _Object) where TFileEntity : IFileEntity
        {
            CoreUpdate<TFileEntity>(_Object);
        }

        private void CoreUpdate<TFileEntity>(IFileEntity _Object) where TFileEntity : IFileEntity
        {
            Type __Type = _Object.GetType();
            string __TableName = GetTableName<TFileEntity>();
            Hashtable __Hashtable = App.Handlers.HashTableHandler.LoadHashTableFromFile(GetFileDataPath(__TableName));


            long __ID = Convert.ToInt64(__Type.GetProperty(ColumnName_ID).GetValue(_Object));
            if (__ID == 0)
            {
                __ID = GetNewID<TFileEntity>();
                _Object.ID = __ID;
            }
            if (__Hashtable.ContainsKey(ColumnName_ID + "_" + __ID.ToString()))
            {
                __Hashtable[ColumnName_ID + "_" + __ID.ToString()] = __ID;
            }
            else
            {
                __Hashtable.Add(ColumnName_ID + "_" + __ID.ToString(), __ID);
            }

            foreach (PropertyInfo __Property in __Type.GetAllProperties())
            {
                if (__Property.IsVirtual() == true)
                {
                    Object __Temp = __Property.GetValue(_Object);
                    if (__Hashtable.ContainsKey(__ID.ToString() + "_" + __Property.Name))
                    {
                        __Hashtable[__ID.ToString() + "_" + __Property.Name] = __Temp;
                    }
                    else
                    {
                        __Hashtable.Add(__ID.ToString() + "_" + __Property.Name, __Temp);
                    }
                }
            }
            App.Handlers.HashTableHandler.SaveHashTableToFile(__Hashtable, GetFileDataPath(__TableName));
        }

        private long GetNewID<TFileEntity>() where TFileEntity : IFileEntity
        {
            List<TFileEntity> __List = GetAll<TFileEntity>();
            if (__List.Count == 0) return 1;
            long __BigID = 0;
            for (int i = 0; i < __List.Count; i++)
            {
                if (__BigID < __List[i].ID)
                {
                    __BigID = __List[i].ID;
                }
            }
            return (__BigID + 1);
        }

        private void CoreDeleteAll<T>() where T : IFileEntity
        {
            List<T> __All = GetAll<T>();
            for (int i = 0; i < __All.Count; i++)
            {
                //__All[i].Delete();
            }
        }

        public List<TFileEntity> GetAll<TFileEntity>() where TFileEntity : IFileEntity
        {
            List<TFileEntity> __Result = new List<TFileEntity>();
            Type __Type = typeof(TFileEntity);

            string __TableName = GetTableName<TFileEntity>();


            Hashtable __Hashtable = App.Handlers.HashTableHandler.LoadHashTableFromFile(GetFileDataPath(__TableName));

            foreach (DictionaryEntry __Entry in __Hashtable)
            {
                if (__Entry.Key.ToString().StartsWith(ColumnName_ID + "_"))
                {
                    TFileEntity __Item = FindByID<TFileEntity>(Convert.ToInt32(__Entry.Value));
                    __Result.Add(__Item);
                }
            }
            return __Result;
        }

        private void CoreDelete<TFileEntity>(IFileEntity _Object) where TFileEntity : IFileEntity
        {
            Type __Type = _Object.GetType();
            string __TableName = GetTableName<TFileEntity>();

            Hashtable __Hashtable = App.Handlers.HashTableHandler.LoadHashTableFromFile(GetFileDataPath(__TableName));

            __Hashtable.Remove(ColumnName_ID + "_" + _Object.ID.ToString());
            foreach (PropertyInfo __Property in __Type.GetAllProperties())
            {
                if (__Property.IsVirtual() == true)
                {
                    __Hashtable.Remove(_Object.ID.ToString() + "_" + __Property.Name);
                }
            }

            App.Handlers.HashTableHandler.SaveHashTableToFile(__Hashtable, GetFileDataPath(__TableName));
        }

        private string GetTableName<TFileEntity>() where TFileEntity : IFileEntity
        {
            Type __Type = typeof(TFileEntity);
            if (__Type.Name.Substring(0, 1) == "c")
            {
                return __Type.Name.Substring(1);
            }
            else
            {
                return __Type.Name;
            }
        }

    }
}

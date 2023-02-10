using Bootstrapper.Boundary.nValueTypes.nConstType;
using Bootstrapper.Core.nApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Base.Data.nDatabaseService.nDatabase
{
    public abstract class cBaseEntity<TEntity> : cBaseEntityType where TEntity : cBaseEntityType
    {
        [Key]
        public long ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        protected Action<object, string> LazyLoader { get; set; }

        public cBaseEntity()
        {
            InitDefaults();
        }

        private cBaseEntity(Action<object, string> _LazyLoader)
        {
            LazyLoader = _LazyLoader;
            InitDefaults();
        }

        


        private void InitDefaults()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;

            Type __Type = this.GetType();

            PropertyInfo[]  __Temp = __Type.GetAllProperties();

            List<PropertyInfo> __PropertyInfos = __Type.GetAllProperties().ToList().Where(__Item => typeof(System.Collections.IEnumerable).IsAssignableFrom(__Item.PropertyType) && __Item.PropertyType != typeof(string)).ToList();

            
            foreach (PropertyInfo __PropertyInfo in __PropertyInfos)
            {
                if (__PropertyInfo.GetValue(this, new object[] { }) == null)
                {
                    Type __GenericTypes = __PropertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
                    Type __ListType = typeof(List<>);
                    Type __Constructor = __ListType.MakeGenericType(__GenericTypes);
                    __PropertyInfo.SetValue(this, __Constructor.CreateInstance());
                }
            }
        }

        public void LockAndRefresh()
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            string __EntityTableName = typeof(TEntity).Name;
            __EntityTableName = __EntityTableName.Substring(1, __EntityTableName.Length - 7) + "s";

            FormattableString __FormattableString = $"Select * from {__EntityTableName} with (updlock) where ID = {ID}";
            __DbContext.Set<TEntity>().FromSqlInterpolated<TEntity>(__FormattableString).FirstOrDefault();
        }


        public void Save()
        {
            DataService.Save();
        }

        public void Delete()
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            __DbContext.Remove(this);
            __DbContext.SaveChanges();
        }


        public void Load(Expression<Func<TEntity, object>> _ParamPropExpression)
        {
            LazyLoader.Load(this, cApp.App.Handlers.LambdaHandler.GetParamPropName(_ParamPropExpression));
        }

        public static TEntity Add(TEntity _Entity)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>().Add(_Entity).Entity;
        }

        public static void Remove(TEntity _Entity)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            __DbContext.Set<TEntity>().Remove(_Entity);
        }

        public static int RemoveRange(IEnumerable<TEntity> _Entities)
        {
            int __RemoveCount = _Entities.ToList().Count;
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            __DbContext.Set<TEntity>().RemoveRange(_Entities);
            return __RemoveCount;
        }

        public static int RemoveRange(Expression<Func<TEntity, bool>> _Predicate)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            List<TEntity> __WillRemove = __DbContext.Set<TEntity>().Where(_Predicate).ToList();
            int __RemoveCount = __WillRemove.Count;
            __DbContext.Set<TEntity>().RemoveRange(__WillRemove);
            return __RemoveCount;
        }

        public static void Find(Expression<Func<TEntity, bool>> _Predicate)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            __DbContext.Set<TEntity>().Where(_Predicate);
        }

        public static IQueryable<TEntity> Get()
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>();
        }

        public static IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> _Predicate)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>().Where<TEntity>(_Predicate);

        }

        public static IQueryable<TEntity> Get(Expression<Func<TEntity,int, bool>> _Predicate)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>().Where<TEntity>(_Predicate);

        }
        public static IQueryable<TEntity> GetAll()
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>();
        }

        public static TEntity GetEntityByID(long _Id)
        {
            DbContext __DbContext = DataService.GetCoreEFDatabaseContext();
            return __DbContext.Set<TEntity>().Find(_Id);
        }
    }
}

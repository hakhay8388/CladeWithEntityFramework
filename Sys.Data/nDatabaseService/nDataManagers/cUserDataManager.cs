using Base.FileData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Sys.Boundary.nDefaultValueTypes;

using Base.Data.nDatabaseService;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;

namespace Sys.Data.nDataService.nDataManagers
{
    public class cUserDataManager : cBaseDataManager
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cUserDataManager(cDataServiceContext _CoreServiceContext, IDataService _DataService, IFileDateService _FileDataService, cRoleDataManager _RoleDataManager)
          : base(_CoreServiceContext, _DataService, _FileDataService)
        {
            DataService = _DataService;
            RoleDataManager = _RoleDataManager;
        }

        public cUserEntity GetUserByEmail(string _Email)
        {
            cUserEntity? __UserEntity = cUserEntity.Get(__Item => __Item.Email == _Email).FirstOrDefault();
            return __UserEntity;
        }

        public cUserEntity GetUserByEmailAndPassword(string _Email, string _Password)
        {
            return cUserEntity.Get(__Item => __Item.Email == _Email && __Item.Password == _Password).FirstOrDefault();
        }


        /*
        public cQuery<cUserEntity> GetListFromEmail(List<dynamic> __UserList)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __Test = null;

            cBaseHardCodedValues __HardCodedValues = __DataService.Database.Catalogs.DataToolOperationSQLCatalog.GetHardCodedValues(__DataService);

            __HardCodedValues.DefineColumns("ID", "Email", "MergedName", "RoleCode", "ProfileImage");
            for (int i = 0; i < __UserList.Count; i++)
            {

                __HardCodedValues.AddValue((i + 1), __UserList[i].Email, __UserList[i].MergedName, __UserList[i].RoleCode, __UserList[i].ProfileImage);
            }


            cQuery<cUserEntity> __Query =
                __DataService.Database.Query<cUserEntity>(__HardCodedValues, () => __Test)
                .SelectAll().Where().ToQuery();
            return __Query;
        }
        public cQuery<cUserEntity> GetListFromSessionID(List<dynamic> __SessionIDs)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __Test = null;

            cBaseHardCodedValues __HardCodedValues = __DataService.Database.Catalogs.DataToolOperationSQLCatalog.GetHardCodedValues(__DataService);

            __HardCodedValues.DefineColumns("ID", "SessionID", "LastTransactionDate");
            for (int i = 0; i < __SessionIDs.Count; i++)
            {

                __HardCodedValues.AddValue((i + 1), __SessionIDs[i].SessionID, __SessionIDs[i].LastTransactionDate);
            }


            cQuery<cUserEntity> __Query =
                __DataService.Database.Query<cUserEntity>(__HardCodedValues, () => __Test)
                .SelectAll().Where().ToQuery();
            return __Query;
        }
        public cQuery<cUserEntity> GetListFromSignalRIDs(string[] __SessionIDs)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __Test = null;

            cBaseHardCodedValues __HardCodedValues = __DataService.Database.Catalogs.DataToolOperationSQLCatalog.GetHardCodedValues(__DataService);

            __HardCodedValues.DefineColumns("ID", "SignalRID");
            for (int i = 0; i < __SessionIDs.Length; i++)
            {

                __HardCodedValues.AddValue((i + 1), __SessionIDs[i]);
            }


            cQuery<cUserEntity> __Query =
                __DataService.Database.Query<cUserEntity>(__HardCodedValues, () => __Test)
                .SelectAll().Where().ToQuery();
            return __Query;
        }
        public cQuery<cUserEntity> GetAllUserListQuery()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __UserAlias = null;
            cUserActorMapEntity __UserActorMapAlias = null;
            cActorRoleMapEntity __ActorRoleMapAlias = null;
            cActorEntity __ActorAlias = null;
            cRoleEntity __RoleEntity = null;
            cQuery<cUserEntity> __Query = __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                .SelectColumn(__Item => __Item.ID)
                .SelectColumn(__Item => __Item.Name)
                .SelectColumn(__Item => __Item.Surname)
                .SelectColumn(__Item => __Item.RealSurname)
                .SelectColumn(__Item => __Item.Email)
                .SelectColumn(__Item => __Item.Language)
                .SelectColumn(__Item => __Item.CreateDate)
                .SelectColumn(__Item => __Item.UpdateDate)
                .SelectColumn(__Item => __Item.State)
                .SelectAliasColumn<cRoleEntity>(() => __RoleEntity, __Item => __Item.ID,"RoleID");

            __Query.Inner<cUserActorMapEntity>().Join(() => __UserActorMapAlias)
                        .On()
                        .Operand<cUserEntity>(() => __UserAlias, __Item => __Item.ID).Eq<cUserEntity>(() => __UserActorMapAlias)
                        .ToQuery();

            __Query.Inner<cActorEntity>().Join(() => __ActorAlias)
                        .On()
                        .Operand<cActorEntity>(() => __ActorAlias, __Item => __Item.ID).Eq<cActorEntity>(() => __UserActorMapAlias)
                        .ToQuery();

            __Query.Inner<cActorRoleMapEntity>().Join(() => __ActorRoleMapAlias)
                        .On()
                        .Operand<cActorEntity>(() => __ActorAlias, __Item => __Item.ID).Eq<cActorEntity>(() => __ActorRoleMapAlias)
                        .ToQuery();

            __Query.Inner<cRoleEntity>().Join(() => __RoleEntity)
                        .On()
                        .Operand<cRoleEntity>(() => __RoleEntity, __Item => __Item.ID).Eq<cRoleEntity>(() => __ActorRoleMapAlias)
                        .ToQuery();

            __Query.Where()
                .ToQuery();

            return __Query;
        }

        public dynamic GetAllUserPaymentKeys()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __UserAlias = null;
            cUserDetailEntity _UserDetailAlias = null;




            cQuery<cUserEntity> __Query = __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                .SelectAliasColumn<cUserEntity>(() => __UserAlias, __Item => __Item.Email)
                .SelectAliasColumn<cUserDetailEntity>(() => _UserDetailAlias, __Item => __Item.PaymentSubMerchantKey);


            __Query.Inner<cUserDetailEntity>().Join(() => _UserDetailAlias)
                        .On()
                        .Operand<cUserEntity>(() => __UserAlias, __Item => __Item.ID).Eq<cUserEntity>(() => _UserDetailAlias)
                        .ToQuery();

            __Query.Where()
                .Operand<cUserDetailEntity>(() => _UserDetailAlias, __Item => __Item.PaymentSubMerchantKey).NotEq("");


            return __Query.ToDynamicObjectList(); ;
        }

        public List<cUserEntity> GetAllSellerUserList()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __UserAlias = null;
            cUserActorMapEntity __UserActorMapAliasForJoin = null;
            cActorEntity __ActorAlias = null;
            cActorRoleMapEntity __ActorRoleMapAlias = null;

            cQuery<cUserEntity> __Query = __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                .SelectAliasAllColumns<cUserEntity>(() => __UserAlias)
                .Where()
                .Exists(
                    __DataService.Database.Query<cUserActorMapEntity>(() => __UserActorMapAliasForJoin)
                    .SelectID()
                    .Where()
                    .Exists(
                        __DataService.Database.Query<cActorEntity>(() => __ActorAlias)
                        .SelectID()
                        .Where()
                        .Exists(
                            __DataService.Database.Query<cActorRoleMapEntity>(() => __ActorRoleMapAlias)
                            .SelectID()
                            .Where()
                            .Operand<cRoleEntity>().Eq(
                                __DataService.Database.Query<cRoleEntity>()
                                .SelectID()
                                .Where()
                                .Operand(__Item => __Item.Code).Eq(RoleIDs.Seller.Code)
                                .ToQuery()
                            )
                            .And
                            .Operand<cActorEntity>().Eq(() => __ActorAlias.ID)
                            .ToQuery()
                        )
                        .And
                        .Operand(__Item => __Item.ID).Eq(() => __UserActorMapAliasForJoin)
                        .ToQuery()
                    )
                    .And
                    .Operand<cUserEntity>().Eq(() => __UserAlias.ID)
                    .ToQuery()
                )
                .ToQuery();

            return __Query.ToList();
        }

      

        public cUserEntity GetUserByUserNick(string _Usernick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __UserEntity = __DataService.Database.Query<cUserEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Usernick).Eq(_Usernick)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __UserEntity;
        }

      

        public cUserEntity GetUserByActor(cActorEntity _ActorEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __UserAlias = null;
            return __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                .SelectAll()
                .Where()
                .Exists(
                    __DataService.Database.Query<cUserActorMapEntity>()
                    .SelectID()
                    .Where()
                    .Operand<cUserEntity>().Eq(() => __UserAlias.ID)
                    .And
                    .Operand<cActorEntity>().Eq(_ActorEntity.ID)
                    .ToQuery()
                )
                .ToQuery()
                .ToList()
                .FirstOrDefault();
        }


        public cUserEntity AddUser(string _Email, string _Name, string _SurName, string _Password, DateTime _DateOfBirth, string _Telephone, bool _IsSeller, int? _EducationLevel, long? _UniversitySection, string _Usernick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __User = __DataService.Database.CreateNew<cUserEntity>();
            __User.Name = _Name;
            __User.Surname = _SurName[..1]+".";
            __User.RealSurname = _SurName;            
            __User.Email = _Email;
            __User.Password = _Password;
            __User.State = EUserState.Confirmed.ID;
            __User.Save();
            if (_IsSeller)
            {
                cUserEntity __UserEntity = __DataService.Database.GetEntityByID<cUserEntity>(__User.ID);
                string __Usernick = App.Handlers.StringHandler.GenerateUserNick(__UserEntity.ID.ToString());
                __UserEntity.Usernick = __Usernick;
                __UserEntity.Save();
            }
            __User.UserDetail.Telephone = _Telephone;
            __User.UserDetail.DateOfBirth = _DateOfBirth;
            __User.UserDetail.Save(__User);


            cActorEntity __Actor = __User.Actor.CreateNew();
            __Actor.Name = __User.Name;
            __Actor.UserVisibleGroup = EUserVisibleGroups.RealUser.ID;
            __Actor.Save();
            if (_IsSeller)
            {
                cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
                __Actor.Roles.AddValue(__RoleEntity);


                __Actor.SellerDetail.Save(__Actor);




            }
            else
            {
                cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
                __Actor.Roles.AddValue(__RoleEntity);

            }



            __User.Actor.SetValue(__Actor);

            return __User;
        }
       
        

        public void UpdateUser(cUserEntity _User, string _Name, string _SurName, DateTime _DateOfBirth, string _Telephone)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            _User.Name = _Name;
            _User.Surname = _SurName[..1]+".";
            _User.RealSurname = _SurName;
            _User.Save();

            _User.UserDetail.Telephone = _Telephone;
            _User.UserDetail.DateOfBirth = _DateOfBirth;
            _User.UserDetail.Save(_User);
        }

        public void UpdateUserPassword(cUserEntity _User, string _NewPassword)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            _User.Password = _NewPassword;
            _User.Save();
        }

        public cUserEntity GetUserById(long _UserId)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __User = __DataService.Database.GetEntityByID<cUserEntity>(_UserId);
            string __LastSeen = __User.UpdateDate.ToString("g");
            DateTime __LastSeenDate = DateTime.Parse(__LastSeen);
            __User.UpdateDate = __LastSeenDate;
            return __User;
        }


        public cUserEntity GetUserBy_SellerDetailId(long _SellerDetailID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserEntity __UserAlias = null;
            cUserActorMapEntity __UserActorMapAliasForJoin = null;
            cActorType_SellerDetailEntity __ActorType_SellerDetailAlias = null;

            cQuery<cUserEntity> __Query = __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                .SelectAliasAllColumns<cUserEntity>(() => __UserAlias)

                .Inner<cUserActorMapEntity>().Join(() => __UserActorMapAliasForJoin).On().Operand<cUserEntity>().Eq(() => __UserAlias.ID).ToQuery();
            __Query.Inner<cActorType_SellerDetailEntity>().Join(() => __ActorType_SellerDetailAlias).On().Operand<cActorEntity>().Eq<cActorEntity>(() => __UserActorMapAliasForJoin).ToQuery();

            __Query.Where()
                .Operand<cActorType_SellerDetailEntity>(() => __ActorType_SellerDetailAlias, __Item => __Item.ID).Eq(_SellerDetailID)
                .ToQuery();


            return __Query.ToList().FirstOrDefault();
        }
        
        public cUserEntity GetUserBySellerDetailID(cActorType_SellerDetailEntity _SellerDetailEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cActorEntity __ActorAlias = null;
            cUserEntity __UserAlias = null;

            return __DataService.Database.Query<cUserEntity>(() => __UserAlias)
                   .SelectAll()
                   .Where()
                   .Exists(
                                __DataService.Database.Query<cUserActorMapEntity>()
                                .SelectID()
                                .Where()
                                .Operand<cUserEntity>().Eq(() => __UserAlias.ID)
                                .And
                                .Operand<cActorEntity>().Eq(
                                        __DataService.Database.Query<cActorEntity>(() => __ActorAlias)
                                        .SelectID()
                                        .Where()
                                        .Exists(
                                            __DataService.Database.Query<cActorType_SellerDetailEntity>()
                                            .SelectID()
                                            .Where()
                                            .Operand<cActorEntity>().Eq(() => __ActorAlias.ID)
                                            .And
                                            .Operand(__Item => __Item.ID).Eq(_SellerDetailEntity.ID)
                                            .ToQuery()
                                        )
                                        .ToQuery()

                                )
                                .ToQuery()
                 )
                .ToQuery()
                .ToList()
                .FirstOrDefault();
        }



        public bool IsThereNumber(string _Phone)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            var __NumberCount = __DataService.Database.Query<cUserDetailEntity>()
                .SelectCount()
                .Where()
                .Operand(__Item => __Item.Telephone).Eq(_Phone)
                .ToQuery().ToCount();

            return __NumberCount > 0 ? true : false;
        }
        public bool IsThereEmail(string _Email)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            var __EmailCount = __DataService.Database.Query<cUserEntity>()
                .SelectCount()
                .Where()
                .Operand(__Item => __Item.Email).Eq(_Email).ToQuery().ToCount();

            return __EmailCount > 0 ? true : false;
        }

        public bool IsThereUsernick(string _Usernick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            var __UsernickCount = __DataService.Database.Query<cUserEntity>()
                .SelectCount()
                .Where()
                .Operand(__Item => __Item.Usernick).Eq(_Usernick).ToQuery().ToCount();

            return __UsernickCount > 0 ? true : false;
        }

        public bool UsernickControlInUpdate(string _Usernick, cUserEntity _ActiveUser)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __UserEntity = __DataService.Database.Query<cUserEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Usernick).Eq(_Usernick).ToQuery().ToList().FirstOrDefault();
            if (__UserEntity != null)
            {
                return __UserEntity.ID == _ActiveUser.ID;
            }
            else
            {
                return true;
            }
        }

        public bool TelephoneControlInUpdate(string _Telephone, cUserEntity _ActiveUser)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserDetailEntity __UserDetailEntity = __DataService.Database.Query<cUserDetailEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Telephone).Eq(_Telephone).ToQuery().ToList().FirstOrDefault();
            if (__UserDetailEntity != null)
            {
                return __UserDetailEntity.ID == _ActiveUser.UserDetail.ID;
            }
            else
            {
                return true;
            }
        }

        public bool IsThereUserWithUsername(string _UserNick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            var __UsernickCount = __DataService.Database.Query<cUserEntity>()
                .SelectCount()
                .Where()
                .Operand(__Item => __Item.Usernick).Eq(_UserNick).ToQuery().ToCount();

            return __UsernickCount > 0;
        }

        public dynamic GetSellerDetailIDByUsernick(string _Usernick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cActorEntity __ActorEntity = null;
            cUserActorMapEntity __UserActorMapEntity = null;
            cUserEntity __UserEntity = null;
            cActorType_SellerDetailEntity __ActorTypeSellerDetailEntity = null;

            cQuery<cUserEntity> __Query = __DataService.Database.Query<cUserEntity>(() => __UserEntity)
                .SelectAliasColumnWithName<cActorType_SellerDetailEntity>(() => __ActorTypeSellerDetailEntity, "ID")
                .Inner<cUserActorMapEntity>()
                .Join(() => __UserActorMapEntity)
                .On()
                .Operand<cUserEntity>(() => __UserEntity, __UserEntity => __UserEntity.ID)
                .Eq<cUserEntity>(() => __UserActorMapEntity)
                .ToQuery();


            __Query = __Query.Inner<cActorEntity>()
                .Join(() => __ActorEntity)
                .On()
                .Operand<cActorEntity>(() => __ActorEntity, __ActorEntity => __ActorEntity.ID)
                .Eq<cActorEntity>(() => __UserActorMapEntity)
                .ToQuery();

            __Query = __Query.Inner<cActorType_SellerDetailEntity>()
                .Join(() => __ActorTypeSellerDetailEntity)
                .On()
                .Operand<cActorEntity>(() => __ActorEntity, __ActorEntity => __ActorEntity.ID)
                .Eq<cActorEntity>(() => __ActorTypeSellerDetailEntity)
                .ToQuery();


            __Query = __Query.Where()
                .Operand<cUserEntity>(() => __UserEntity, "Usernick")
                .Eq(_Usernick)
                .ToQuery();

            return __Query.ToDynamicObjectList().FirstOrDefault();
        }


        public bool IsValidUsernick(string _Username)
        {
            string __Pattern = @"^(?=.{6,15}$)(?:[a-z0-9._\d]+(?:(?:\.|-|_)[a-z0-9._\d])*)+$";
            Regex __Regex = new Regex(__Pattern);
            return __Regex.IsMatch(_Username);
        }

        public bool IsValidPhoneNUmber(string _Phone)
        {
            string __Pattern = @"^((\d{3})( )(\d{3})-(\d{4}))$";
            Regex __Regex = new Regex(__Pattern);
            _Phone = _Phone.Replace("(", string.Empty).Replace(")", string.Empty);
            return __Regex.IsMatch(_Phone);
        }

        public bool IsValidBirthDate(DateTime _BirthDate, bool _IsSeller)
        {
            cGenericWebScaffoldDataService __DataService =
                (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            int __Year = _BirthDate.Year;
            int __Age = DateTime.Now.Year - __Year;
            bool __Validity;
            if (_IsSeller)
            {
                __Validity = __Age >= __DataService.SellerAgeLimit;
            }
            else
            {
                __Validity = __Age >= __DataService.CustomerAgeLimit;
            }

            return __Validity;
        }

        public void DeleteUserProfileImage(cUserEntity _User)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            _User.UserDetail.ProfileImage = "DefaultProfile.png";
            _User.UserDetail.Save(_User);
        }
        */

    }
}

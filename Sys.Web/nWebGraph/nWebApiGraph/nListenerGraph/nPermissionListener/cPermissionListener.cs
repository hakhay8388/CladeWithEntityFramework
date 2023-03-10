using Base.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Sys.Data.nDatabaseService.nSystemEntities;
using Sys.Data.nDataService.nDataManagers;
using Sys.Boundary.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nMenuResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nPageResultAction;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nPermissionListener
{
    public class cPermissionListener : cBaseListener, IGetMenuListReceiver, IGetPageListReceiver
    {
        public cMenuDataManager MenuDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }

        public cPermissionListener(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService, cMenuDataManager _MenuDataManager, cPageDataManager _PageDataManager)
          : base(_App, _WebGraph, _DataService)
        {
            WebGraph = _WebGraph;
            MenuDataManager = _MenuDataManager;
            PageDataManager = _PageDataManager;
        }

		public cMenuResultProps PrepareMenuResultProps(IController _Controller, cGetMenuListCommandData _ReceivedData)
		{
            cMenuResultProps __PageResultProps = new cMenuResultProps();


            if (_Controller.ClientSession.IsLogined)
            {

                List<cMenuEntity> __Menus = MenuDataManager.GetMenuByUser(_Controller.ClientSession.User, _ReceivedData.MenuTypeCode, _ReceivedData.RootMenuCode, true);

                foreach (cMenuEntity __MenuEntity in __Menus)
                {
                    MenuIDs __MenuID = MenuIDs.GetByCode(__MenuEntity.Code, null);
                    if (__MenuID != null)
                    {
                        if (__MenuID.IsMainMenu)
                        {
                            List<dynamic> __SubMenus = MenuDataManager.GetMenuByActorToDynamicList(_Controller.ClientSession.User, MenuTypes.CenterMenu.Code, __MenuID.Code, true);
                                /*(_Item) =>
                                {
                                    _Item.icon = _Item.Icon;
                                    _Item.name = _Item.Name;
                                    _Item.mainMenu = false;
                                    _Item.subMenu = new object[] { };
                                    _Item.Active = false;
                                    _Item.url = _Item.Url;
                                });*/


                            __PageResultProps.MenuItems.Add(new cMenuItem()
                            {
                                Icon = __MenuID.Icon,
                                Name = __MenuID.Name,
                                SubMenu = __SubMenus,
                                MainMenu = true,
                            });

                        }
                        else
                        {
                            __MenuEntity.Load(__Item => __Item.Page);
                            cPageEntity __Page = __MenuEntity.Page;

                            __PageResultProps.MenuItems.Add(new cMenuItem()
                            {
                                Url = __Page.Url,
                                Icon = __MenuID.Icon,
                                Name = __MenuID.Name,
                                SubMenu = new object[] { },
                                MainMenu = false,
                            });
                          
                        }
                    }
                }
            }
            else
            {
                List<MenuIDs> __MenuIDs = MenuIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Unlogined.Code)).ToList();


                foreach (MenuIDs __MenuEntity in __MenuIDs)
                {
                    if (__MenuEntity.IsMainMenu)
                    {
                        __PageResultProps.MenuItems.Add(new cMenuItem()
                        {
                            Url = "menupage/" + __MenuEntity.Code,
                            Icon = __MenuEntity.Icon,
                            Name = __MenuEntity.Name
                        });
                    }
                    else
                    {
                        PageIDs __PageID = PageIDs.GetByCode(__MenuEntity.Code, PageIDs.UnloginedMainPage);
                        __PageResultProps.MenuItems.Add(new cMenuItem()
                        {
                            //url = __PageID.Code == PageIDs.MainPage.Code ? "global" : __PageID.Url,
                            Url = __PageID.Url,
                            Icon = __MenuEntity.Icon,
                            Name = __MenuEntity.Name
                        });
                    }
                }
            }

            return __PageResultProps;
        }

        public void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData)
        {
            WebGraph.SysActionGraph.MenuResultAction.Action(_Controller, PrepareMenuResultProps(_Controller, _ReceivedData));

        }


        public cPageResultProps PreparePageResultProps(IController _Controller, cGetPageListCommandData _ReceivedData)
        {
            cPageResultProps __PageResultProps = new cPageResultProps();

            if (_Controller.ClientSession.IsLogined)
            {
                List<cPageEntity> __Pages = PageDataManager.GetPageByUser(_Controller.ClientSession.User);

                foreach (cPageEntity __Page in __Pages)
                {
                    __PageResultProps.PagesItems.Add(new cPageItem()
                    {
                        Path = __Page.Url,
                        Name = __Page.Name,
                        OriginalCode = PageIDs.GetByCode(__Page.Code, PageIDs.UserMainPage).OriginalCode,
                        SubParamName = PageIDs.GetByCode(__Page.Code, PageIDs.UserMainPage).SubParamName,
                        Component = __Page.ComponentName,
                        IsMainPage = _Controller.ClientSession.User.MainPage.Code == __Page.Code,
                    });
                }

            }
            else
            {
                List<PageIDs> __Pages = PageIDs.TypeList.Where(__Item => __Item.MainRoles.Any(__Item => __Item.Code == RoleIDs.Unlogined.Code)).ToList();

                for (int i = 0; i < __Pages.Count; i++)
                {
                    __PageResultProps.PagesItems.Add(new cPageItem()
                    {
                        Path = __Pages[i].Url,
                        Name = __Pages[i].Name,
                        OriginalCode = __Pages[i].OriginalCode,
                        SubParamName = PageIDs.GetByCode(__Pages[i].Code, PageIDs.UnloginedMainPage).SubParamName,
                        Component = __Pages[i].Component,
                        IsMainPage = i == 0
                    });
                }
            }
            return __PageResultProps;
        }

        public void ReceiveGetPageListData(cListenerEvent _ListenerEvent, IController _Controller, cGetPageListCommandData _ReceivedData)
        {
            WebGraph.SysActionGraph.PageResultAction.Action(_Controller, PreparePageResultProps(_Controller, _ReceivedData));
        }
    }
}

using Base.Data.nDatabaseService;
using Bootstrapper.Core.nApplication;
using Sys.Data.nDataService.nDataManagers;
using Sys.Web.Controllers;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaAndSettingsCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph.nDataSourceListener
{
    public class cDataSourceListener : cBaseListener
        , IDataSource_ReadReceiver
        , IDataSource_CreateReceiver
        , IDataSource_UpdateReceiver
        , IDataSource_DeleteReceiver
        , IDataSource_GetMetaAndSettingsReceiver
    {
        public cSessionDataManager SessionDataManager { get; set; }
        public cUserDataManager UserDataManager { get; set; }
        public cDataSourceListener(cApp _App, cBaseWebGraph _WebGraph, IDataService _DataService)
           : base(_App, _WebGraph, _DataService)
        {
            WebGraph = _WebGraph;
        }

        public void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_ReadData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_CreateData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_UpdateData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_DeleteData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_GetMetaAndSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaAndSettingsCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_GetMetaAndSettingsData(_ListenerEvent, _Controller, _ReceivedData);
        }
    }
}

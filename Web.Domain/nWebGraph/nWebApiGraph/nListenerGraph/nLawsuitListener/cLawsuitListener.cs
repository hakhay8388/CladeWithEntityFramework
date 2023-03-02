using Bootstrapper.Core.nApplication;
using Sys.Data.nDatabaseService;
using Domain.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nListenerGraph;
using Base.Data.nDatabaseService;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;
using Sys.Web.Controllers;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph;
using Web.Domain.nWebGraph.nWebApiGraph.nActionGraph.nActions.nTestAction;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nTestCommand;
using Web.Domain.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nAddLawsuitCommand;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Domain.Data.nDatabaseService.nEntities;
using Nest;
using Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;

namespace Web.Domain.nWebGraph.nWebApiGraph.nListenerGraph.nLawsuitListener
{
     public class cLawsuitListener : cBaseListener
        , IAddLawsuitReceiver
    {
        public cLawsuitListener(cApp _App, cWebGraph _WebGraph, IDataService _DataService)
               : base(_App, _WebGraph, _DataService)
        {
        }

        public void ReceiveAddLawsuitData(cListenerEvent _ListenerEvent, IController _Controller, cAddLawsuitCommandData _ReceivedData)
        {
            try
            {
                cDomainDatabaseContext __DomainDatabaseContext = DataService.GetDatabaseContext<cDomainDatabaseContext>();

                __DomainDatabaseContext.Perform(() =>
                {
                    cLawsuitEntity __LawsuitEntity = cLawsuitEntity.Add(new cLawsuitEntity()
                    {
                        Name = _ReceivedData.LawsuitName,
                        Key = _ReceivedData.LawsuitKey
                    });

                    __LawsuitEntity.Save();

                    //WebGraph.SysActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps() { Item = __LawsuitEntity});
                });

                List< cLawsuitEntity> __Lawsuits = cLawsuitEntity.GetAll().ToList();

                WebGraph.SysActionGraph.ResultListAction.Action(_Controller, new cResultListProps() { Page = 1, ResultList = __Lawsuits, Total = __Lawsuits.Count });

            }
            catch (Exception ex)
            {
                WebGraph.ErrorMessageManager.ErrorAction(ex, _Controller, "hata", "hata");
            }
        }
    }
}

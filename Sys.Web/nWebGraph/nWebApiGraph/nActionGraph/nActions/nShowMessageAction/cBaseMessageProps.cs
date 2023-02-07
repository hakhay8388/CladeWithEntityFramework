using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Web.Controllers;
using Sys.Web.nUtils.nValueTypes;
using Sys.Web.nWebGraph.nSessionManager;
using Sys.Web.nWebGraph.nWebApiGraph.nCommandGraph;

using Newtonsoft.Json.Linq;

namespace Sys.Web.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cBaseMessageProps<T> : cBaseProps
    {
        public virtual string Header { get; set; }
        public virtual string Message { get; set; }
        public virtual EColorTypes ColorType { get; set; }
        public virtual EMessageButtons MessageButtons { get; set; }
        public virtual EMessageButton DefaultMessageButton { get; set; }
        public virtual bool FirstButtonEnabled { get; set; }
        public virtual string FirstButtonText { get; set; }
        public virtual EColorTypes FirstButtonColorType { get; set; }

        public virtual bool SecondButtonEnabled { get; set; }
        public virtual string SecondButtonText { get; set; }
        public virtual EColorTypes SecondButtonColorType { get; set; }

        public virtual bool ThirdButtonEnabled { get; set; }
        public virtual string ThirdButtonText { get; set; }
        public virtual EColorTypes ThirdButtonColorType { get; set; }

        public virtual T RequestObject { get; set; }

        public virtual bool CloseRequired { get; set; }
        public Action<cBaseCommand, IController, EMessageButton, T> Action { get; set; }

        public cBaseMessageProps()
        {
            Header = "";
            Message = "";
            ColorType = EColorTypes.Primary;
            MessageButtons = EMessageButtons.Close;
            DefaultMessageButton = EMessageButton.Close;
            FirstButtonEnabled = true;
            FirstButtonText = "Kapat";
            FirstButtonColorType = EColorTypes.Primary;
            SecondButtonEnabled = false;
            SecondButtonText = "";
            SecondButtonColorType = EColorTypes.Primary;
            ThirdButtonEnabled = false;
            ThirdButtonText = "";
            ThirdButtonColorType = EColorTypes.Primary;
            CloseRequired = true;
            Action = null;
            RequestObject = default(T);
        }
    }
}

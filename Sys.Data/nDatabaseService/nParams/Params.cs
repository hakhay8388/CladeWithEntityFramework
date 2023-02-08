using Bootstrapper.Core.nApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Data.nDatabaseService.nParams
{
    public static class Params
    {
        static cGlobalParams m_cGlobalParams = null;
        public static cGlobalParams GlobalParams
        {
            get
            {
                if (m_cGlobalParams == null)
                {
                    m_cGlobalParams = new cGlobalParams(cApp.App);
                }
                return m_cGlobalParams;
            }
        }
    }
}

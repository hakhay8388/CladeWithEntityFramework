using Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sys.Web.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public class cBaseDataSourceObject
    {

        public bool Calculated { get; set; }
        public string Title { get; set; }
        public ColumnTypeIDs Type { get; set; }
        public bool Visible { get; set; }
        public bool ElasticSearch { get; set; }
        public string Editable { get; set; }
        public bool Removable { get; set; }
        public bool Readonly { get; set; }
        public bool TranslateValue { get; set; }
        public string FieldName { get; set; }
        public int OrderFromLeft { get; set; }
        public int Width { get; set; }        

        public cBaseDataSourceObject()
        {
            Calculated = false;
            Title = "Atanmadı";
            Visible = true;
            ElasticSearch = false;
            Editable = "never";
            Removable = false;
            Readonly = false;
            TranslateValue = false;
            OrderFromLeft = 1;
            Type = ColumnTypeIDs.String;
            Width = 0;
        }

    }
}

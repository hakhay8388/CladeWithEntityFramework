using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace Bootstrapper.Core.nHandlers.nExcelHandler
{
    public class cExcelHandler : cCoreObject
    {
        public cExcelHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

		public string GetDataItemFullPath(string _ItemCode)
		{
			return Path.Combine(App.Configuration.DefaultDataPath, _ItemCode + ".xlsx");
		}

		public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cExcelHandler>(this);
        }


		public List<TExcelRowItem> ExcelFileToList<TExcelRowItem>(string _FullPathFileName, string _SheetName, int _EndRowsWhenColumnNoIsNullOrEmpty, int _ActiveColumnCount)
		{
			if (File.Exists(_FullPathFileName))
			{
				List<TExcelRowItem> __Result = new List<TExcelRowItem>();
				using (ExcelPackage __Package = new ExcelPackage(new FileInfo(_FullPathFileName)))
				{
					ExcelWorksheet __Sheet = __Package.Workbook.Worksheets[_SheetName];

					Dictionary<int, string> __ColumnNoAndNameMatcher = new Dictionary<int, string>();
					for (int i = 0; i < _ActiveColumnCount; i++)
					{
						string __Value = __Sheet.Cells[1, i + 1].Value.ToString();
						__ColumnNoAndNameMatcher.Add(i, __Value);

					}

					Type __ExcelRowItemType = typeof(TExcelRowItem);
					int __Row = 2;
					while (__Sheet.Cells[__Row, _EndRowsWhenColumnNoIsNullOrEmpty + 1].Value != null && !__Sheet.Cells[__Row, _EndRowsWhenColumnNoIsNullOrEmpty + 1].Value.ToString().Trim().IsNullOrEmpty())
					{
						TExcelRowItem __Item = (TExcelRowItem)__ExcelRowItemType.CreateInstance();
						foreach (var __ColumnNoAndNameMatcherItem in __ColumnNoAndNameMatcher)
						{
							object __ValueObject = __Sheet.Cells[__Row, __ColumnNoAndNameMatcherItem.Key + 1].Value;

							string __Value = __ValueObject == null ? null : __ValueObject.ToString();

							__ExcelRowItemType.SetPropertyValue(__Item, __ColumnNoAndNameMatcherItem.Value, __Value);

						}
						__Result.Add(__Item);
						__Row++;
					}
				}
				return __Result;
			}
			else
			{
				throw new Exception(_FullPathFileName + " dosyası bulunamadı!");
			}
		}

		public string GetCheckSum(string _FileName)
		{
			if (File.Exists(GetDataItemFullPath(_FileName)))
			{
				return App.Handlers.FileHandler.GetFileChecksum(GetDataItemFullPath(_FileName));
			}
			else
			{
				throw new Exception(_FileName + " dosyası bulunamadı!");
			}
		}

		public List<TExcelRowItem> ExcelFileToListInDefaultPath<TExcelRowItem>(string _FileName, string _SheetName, int _EndRowsWhenColumnNoIsNullOrEmpty, int _ActiveColumnCount)
		{
			return ExcelFileToList<TExcelRowItem>(GetDataItemFullPath(_FileName), _SheetName, _EndRowsWhenColumnNoIsNullOrEmpty, _ActiveColumnCount);
		}			
    }
}

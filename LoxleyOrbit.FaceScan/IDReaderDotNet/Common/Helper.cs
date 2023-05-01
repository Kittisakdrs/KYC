using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Common
{
	internal class Helper
	{
		public static DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable("tblNationalID");
			dataTable.Columns.Add(new DataColumn("NationalID", typeof(string)));
			dataTable.Columns.Add(new DataColumn("IssuePlace", typeof(string)));
			dataTable.Columns.Add(new DataColumn("IssueDate", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ExpireDate", typeof(string)));
			dataTable.Columns.Add(new DataColumn("FormatVersion", typeof(string)));
			dataTable.Columns.Add(new DataColumn("AtrString", typeof(string)));
			dataTable.Columns.Add(new DataColumn("CardType", typeof(string)));
			dataTable.Columns.Add(new DataColumn("IssueCode", typeof(string)));
			dataTable.Columns.Add(new DataColumn("IssuerID", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ThaiTitleName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ThaiFirstName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ThaiMiddleName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ThaiLastName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("EnglishTitleName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("EnglishFirstName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("EnglishMiddleName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("EnglishLastName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("BirthDate", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Sex", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Address", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Moo", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Trok", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Soi", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Thanon", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Tumbol", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Amphur", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Province", typeof(string)));
			dataTable.Columns.Add(new DataColumn("PhotoRefNo", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Photo", typeof(byte[])));
			dataTable.Rows.Add(dataTable.NewRow());
			return dataTable;
		}
	}
}

//----------------------------------------------
//    GoogleFu: Google Doc Unity integration
//         Copyright © 2013 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;

namespace GoogleFu
{
	[System.Serializable]
	public class ConstDBRow : IGoogleFuRow
	{
		public int _initialcash;
		public ConstDBRow(string __initialcash) 
		{
			{
			int res;
				if(int.TryParse(__initialcash, out res))
					_initialcash = res;
				else
					Debug.LogError("Failed To Convert initialcash string: "+ __initialcash +" to int");
			}
		}

		public int Length { get { return 1; } }

		public string this[int i]
		{
		    get
		    {
		        return GetStringDataByIndex(i);
		    }
		}

		public string GetStringDataByIndex( int index )
		{
			string ret = System.String.Empty;
			switch( index )
			{
				case 0:
					ret = _initialcash.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID.ToLower() )
			{
				case "initialcash":
					ret = _initialcash.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "initialcash" + " : " + _initialcash.ToString() + "} ";
			return ret;
		}
	}
	public sealed class ConstDB : IGoogleFuDB
	{
		public enum rowIds {
			GameConfig
		};
		public string [] rowNames = {
			"GameConfig"
		};
		public System.Collections.Generic.List<ConstDBRow> Rows = new System.Collections.Generic.List<ConstDBRow>();

		public static ConstDB Instance
		{
			get { return NestedConstDB.instance; }
		}

		private class NestedConstDB
		{
			static NestedConstDB() { }
			internal static readonly ConstDB instance = new ConstDB();
		}

		private ConstDB()
		{
			Rows.Add( new ConstDBRow("20"));
		}
		public IGoogleFuRow GetGenRow(string in_RowString)
		{
			IGoogleFuRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}
		public IGoogleFuRow GetGenRow(rowIds in_RowID)
		{
			IGoogleFuRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public ConstDBRow GetRow(rowIds in_RowID)
		{
			ConstDBRow ret = null;
			try
			{
				ret = Rows[(int)in_RowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( in_RowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public ConstDBRow GetRow(string in_RowString)
		{
			ConstDBRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), in_RowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( in_RowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}

	}

}

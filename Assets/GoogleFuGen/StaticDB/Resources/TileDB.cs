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
	public class TileDBRow : IGoogleFuRow
	{
		public string _prefab;
		public int _profit;
		public int _pop;
		public int _rep;
		public int _power;
		public int _jobs;
		public int _police;
		public int _fire;
		public int _health;
		public TileDBRow(string __prefab, string __profit, string __pop, string __rep, string __power, string __jobs, string __police, string __fire, string __health) 
		{
			_prefab = __prefab;
			{
			int res;
				if(int.TryParse(__profit, out res))
					_profit = res;
				else
					Debug.LogError("Failed To Convert profit string: "+ __profit +" to int");
			}
			{
			int res;
				if(int.TryParse(__pop, out res))
					_pop = res;
				else
					Debug.LogError("Failed To Convert pop string: "+ __pop +" to int");
			}
			{
			int res;
				if(int.TryParse(__rep, out res))
					_rep = res;
				else
					Debug.LogError("Failed To Convert rep string: "+ __rep +" to int");
			}
			{
			int res;
				if(int.TryParse(__power, out res))
					_power = res;
				else
					Debug.LogError("Failed To Convert power string: "+ __power +" to int");
			}
			{
			int res;
				if(int.TryParse(__jobs, out res))
					_jobs = res;
				else
					Debug.LogError("Failed To Convert jobs string: "+ __jobs +" to int");
			}
			{
			int res;
				if(int.TryParse(__police, out res))
					_police = res;
				else
					Debug.LogError("Failed To Convert police string: "+ __police +" to int");
			}
			{
			int res;
				if(int.TryParse(__fire, out res))
					_fire = res;
				else
					Debug.LogError("Failed To Convert fire string: "+ __fire +" to int");
			}
			{
			int res;
				if(int.TryParse(__health, out res))
					_health = res;
				else
					Debug.LogError("Failed To Convert health string: "+ __health +" to int");
			}
		}

		public int Length { get { return 9; } }

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
					ret = _prefab.ToString();
					break;
				case 1:
					ret = _profit.ToString();
					break;
				case 2:
					ret = _pop.ToString();
					break;
				case 3:
					ret = _rep.ToString();
					break;
				case 4:
					ret = _power.ToString();
					break;
				case 5:
					ret = _jobs.ToString();
					break;
				case 6:
					ret = _police.ToString();
					break;
				case 7:
					ret = _fire.ToString();
					break;
				case 8:
					ret = _health.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			var ret = System.String.Empty;
			switch( colID.ToLower() )
			{
				case "prefab":
					ret = _prefab.ToString();
					break;
				case "profit":
					ret = _profit.ToString();
					break;
				case "pop":
					ret = _pop.ToString();
					break;
				case "rep":
					ret = _rep.ToString();
					break;
				case "power":
					ret = _power.ToString();
					break;
				case "jobs":
					ret = _jobs.ToString();
					break;
				case "police":
					ret = _police.ToString();
					break;
				case "fire":
					ret = _fire.ToString();
					break;
				case "health":
					ret = _health.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "prefab" + " : " + _prefab.ToString() + "} ";
			ret += "{" + "profit" + " : " + _profit.ToString() + "} ";
			ret += "{" + "pop" + " : " + _pop.ToString() + "} ";
			ret += "{" + "rep" + " : " + _rep.ToString() + "} ";
			ret += "{" + "power" + " : " + _power.ToString() + "} ";
			ret += "{" + "jobs" + " : " + _jobs.ToString() + "} ";
			ret += "{" + "police" + " : " + _police.ToString() + "} ";
			ret += "{" + "fire" + " : " + _fire.ToString() + "} ";
			ret += "{" + "health" + " : " + _health.ToString() + "} ";
			return ret;
		}
	}
	public sealed class TileDB : IGoogleFuDB
	{
		public enum rowIds {
			RES, IND, CNV, TH
		};
		public string [] rowNames = {
			"RES", "IND", "CNV", "TH"
		};
		public System.Collections.Generic.List<TileDBRow> Rows = new System.Collections.Generic.List<TileDBRow>();

		public static TileDB Instance
		{
			get { return NestedTileDB.instance; }
		}

		private class NestedTileDB
		{
			static NestedTileDB() { }
			internal static readonly TileDB instance = new TileDB();
		}

		private TileDB()
		{
			Rows.Add( new TileDBRow("Tile_Residence",
														"0",
														"2",
														"0",
														"-1",
														"-2",
														"0",
														"0",
														"0"));
			Rows.Add( new TileDBRow("Tile_Industrial",
														"0",
														"0",
														"-1",
														"-1",
														"7",
														"0",
														"0",
														"0"));
			Rows.Add( new TileDBRow("Tile_ConvStore",
														"1",
														"0",
														"0",
														"-1",
														"0",
														"0",
														"0",
														"0"));
			Rows.Add( new TileDBRow("Tile_TownHall",
														"5",
														"0",
														"5",
														"10",
														"10",
														"0",
														"0",
														"0"));
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
		public TileDBRow GetRow(rowIds in_RowID)
		{
			TileDBRow ret = null;
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
		public TileDBRow GetRow(string in_RowString)
		{
			TileDBRow ret = null;
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

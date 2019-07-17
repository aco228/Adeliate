using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senti.Data;
using Adlite.Data;

 

namespace Adlite.Data 
{
  public partial interface IStatInfoManager : IDataManager<StatInfo> 
  {
	

  }

  public partial class StatInfo : AdliteObject<IStatInfoManager> 
  {
		private int _offerID = -1;
		private int _affiliateID = -1;
		private StatType _statType;
		private int? _clicks = -1;
		private int? _transactions = -1;
		private decimal? _revenue = 0;
		

		public int OfferID{ get {return this._offerID; } set { this._offerID = value;} }
		public int AffiliateID{ get {return this._affiliateID; } set { this._affiliateID = value;} }
		public StatType StatType  { get { return this._statType; } set { this._statType = value; } }
		public int? Clicks{ get {return this._clicks; } set { this._clicks = value;} }
		public int? Transactions{ get {return this._transactions; } set { this._transactions = value;} }
		public decimal? Revenue { get { return this._revenue; } set { this._revenue = value;} }
		

    public override bool IsInsertable { get { return true;} }

    public override bool IsDeletable { get { return false;} }

    public override bool IsUpdatable { get { return true;} }

    public StatInfo()
    { 
    }

    public StatInfo(int id): base(id, DateTime.Now, DateTime.Now, true)
    {
    }

    public StatInfo(int id,  int offerID, int affiliateID, StatType statType, int? clicks, int? transactions, decimal? revenue, DateTime updated, DateTime created): base(id, updated, created, false)
    {
			this._offerID = offerID;
			this._affiliateID = affiliateID;
			this._statType = statType;
			this._clicks = clicks;
			this._transactions = transactions;
			this._revenue = revenue;
			
    }

    public override object Clone(bool deepClone)
    {
      return new StatInfo(-1, this.OfferID,this.AffiliateID, this.StatType,this.Clicks,this.Transactions,this.Revenue, DateTime.Now, DateTime.Now);
    }
  }
}


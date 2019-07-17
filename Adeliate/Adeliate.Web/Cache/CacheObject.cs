using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adeliate.Web.Cache
{
  public abstract class CacheObject
  {
    private string _key;
    private int _expireValueInMinutes = 0;
    private DateTime _validUntil;
    private bool _isActive = true;
    private DateTime _created;

    public int ExpireValueInMinutes { get { return this._expireValueInMinutes; } }
    public DateTime ValidUntil { get { return this._validUntil; } }
    public DateTime Created { get { return this._created; } }
    public bool IsActive { get { return this._isActive; } }

    public string Key
    {
      get
      {
        if (string.IsNullOrEmpty(this._key))
          this._key = this.ConstructKey();
        return this._key;
      }
    }

    public CacheObject(int expireValueInMinutes)
    {
      this._expireValueInMinutes = expireValueInMinutes;
      this._validUntil = DateTime.Now.AddMinutes(this._expireValueInMinutes);
      this._created = DateTime.Now;
    }

    public void Ping()
    {
      if (DateTime.Now > this._validUntil)
      {
        this._isActive = false;
        Console.WriteLine("This elemet should be removed " + this._key);
        return;
      }

      this.OnPing();
    }

    public void Requested()
    {
      this._validUntil.AddMinutes(this._expireValueInMinutes);
    }

    protected virtual void OnPing() { }


    public abstract string ConstructKey();
    
  }
}

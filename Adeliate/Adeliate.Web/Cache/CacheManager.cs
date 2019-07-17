using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adeliate.Web.Cache
{
  public class CacheManager
  {
    private SortedDictionary<string, CacheObject> _objects = null;

    protected SortedDictionary<string, CacheObject> Objects { get { return this._objects; } }

    public CacheManager()
    {
      this._objects = new SortedDictionary<string, CacheObject>();
      new Thread(() => {

        for(;;)
        {
          this.Run();
          Thread.Sleep(1000);
        }

      }).Start();
    }

    protected virtual void Run()
    {
      for(int i = this._objects.Count - 1; i >= 0; i--)
      {
        KeyValuePair<string, CacheObject> entry = this._objects.ElementAt(i);
        entry.Value.Ping();
        if(!entry.Value.IsActive)
          this._objects.Remove(entry.Key);
      }
    }

    public virtual void Add(CacheObject cacheObject)
    {
      this._objects.Add(cacheObject.Key, cacheObject);
    }
    
    public T Request<T>(string key) where T : CacheObject
    {
      if (!this._objects.ContainsKey(key))
        return null;

      CacheObject obj = this._objects[key];
      if (obj == null || !obj.IsActive)
        return null;

      obj.Requested();
      return obj as T;
    }
    
  }
}

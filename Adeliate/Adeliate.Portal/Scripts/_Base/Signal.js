function Signal()
{
  this.hubs = new Array();
  this.hubsFolder = '/Scripts/hubs/';

  // SUMMARY: Return hub with specific name
  this.get = function(hubName)
  {
    for (var i = 0; i < this.hubs.length; i++)
      if (typeof (this.hubs[i].name) === 'string' && this.hubs[i].name == hubName)
        return this.hubs[i];
    return null;
  }

  // SUMMARY: This method is called from derivate when that class has finished loading
  this.call = function(obj)
  {
    if (typeof (obj.name) !== 'string') return;

    var hubName = obj.name.toLowerCase().replace('hub', '') + 'Hub';
    if (!$.connection.hasOwnProperty(hubName))
      return;

    obj.hub = eval('$.connection.' + hubName);
    obj.server = eval('$.connection.' + hubName + '.server');
    if (obj.hub == null) return;

    obj.hub.client.update = function (data) {
      var hubObj = _signal.get(obj.name);
      console.log(data);
      console.log(hubObj);
      if (data.FilterCustomer && data.CustomerID != _system.CustomerID) return;
      if (data.FilterAffiliate && data.AffiliateID != _system.AffiliateID) return;
      if (obj.hasOwnProperty(data.MethodName))
        hubObj[data.MethodName].call(hubObj, data.Data);
    };

    if (obj.hasOwnProperty('init'))
      eval('obj.init()');

    obj.hub.client.connected = function () { }
  }

  this.onMessage = function ()
  {

  }

  // SUMMARY: This method is called dynamicly so we can add multiple hubs into system at the same time
  this.add = function(hubScriptName) { _system.include(this.constructScriptName(hubScriptName)); }

  // SUMMARY: Construct string for loading proper derivate javascript
  this.constructScriptName = function(name) { return this.hubsFolder + name + '.js'; }

  $.connection.hub.start().done(function () { });
}
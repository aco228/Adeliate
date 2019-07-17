_signal.hubs.push(new function () {

  this.name = 'portal';
  this.hub = null;
  this.server = null;
  
  this.onportalmessage = function (data)
  {
    _modal.Toast(data.message);
  }

  _signal.call(this);
});
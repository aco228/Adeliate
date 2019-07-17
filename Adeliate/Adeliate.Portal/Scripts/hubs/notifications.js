_signal.hubs.push(new function () {

  this.name = 'notifications';
  this.hub = null;
  this.server = null;
  this.initialLoaded = false;
  this.busy = false;
  this.lastID = 0;

  //signal notificastion
  this.notify = function (data) {
    console.log(data);
    this.appendBadge();
    
    var newElem = $('._internalNotification').first().clone();
    newElem.find('._internalNotificationImage').attr('src', '/Images/Campaign?id=' + data.campaignID);
    newElem.find('._internalNotificationDate').text('Now');
    newElem.find('._internalNotificationTitle').text(data.title);
    newElem.find('._internalNotificationText').text(data.text);
    $('#internalNotificationHolder').prepend(newElem);
  }
  
  this.init = function () {
    this.checkForUnreadNotifications();
    this.onOpen();
  }

  this.checkForUnreadNotifications = function () {
    var notifyID = _system.getCookie('notifyid');
    if (notifyID == null)
      return;
    
    var self = this;
    notifyID = parseInt(notifyID);
    if (notifyID < self.getHighestNotificationID())
      self.appendBadge();
  }

  this.getHighestNotificationID = function () {
    var id = 0;
    $('._internalNotification').each(function (elem) {
      var notificationID = $(this).attr('notificationID');
      if (typeof (notificationID) !== 'string')
        return;

      notificationID = parseInt(notificationID);
      if (notificationID > id)
        id = notificationID;
    });
    return id;
  }

  this.appendBadge = function () {
    var currentBadgeValue = parseInt($('#notificationBadge').text());
    currentBadgeValue++;
    $('#notificationBadge').text(currentBadgeValue);
    $('#notificationBadge').css("display", "block");
  }

  this.onOpen = function () {
    var self = this;
    $('#internalNotificationOpener').click(function () {
      $('#notificationBadge').text('0');
      $('#notificationBadge').css('display', 'none');

      var newID = self.getHighestNotificationID();
      _system.addCookie('notifyid', newID);
      
    });

  }
  
  _signal.call(this);
  this.init();
});
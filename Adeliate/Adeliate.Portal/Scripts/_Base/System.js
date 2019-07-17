function System(session_id, customerID, affiliateID)
{
  this.SessionID = session_id;
  this.CustomerID = customerID;
  this.AffiliateID = affiliateID;
  
  this.init = function()
  {
  }


  this.call = function (url, data, succ_func)
  {
    data.sid = this.SessionID;
    if (typeof succ_func !== 'function')
      return;

    $.ajax({
      url: url,
      data: data,
      method: 'POST',
      success: succ_func
    });
  }

  this.include = function (fileName)
  {
    var appender = '<script src="' + fileName + '"></script>';
    $('body').append(appender);
  }

  this.getCookie = function(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
      var c = ca[i];
      while (c.charAt(0)==' ') c = c.substring(1,c.length);
      if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
  }

  this.addCookie = function (name, value, days) {
    if (typeof (days) === 'undefined')
      days = 365;
    var expires = "";
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days*24*60*60*1000));
      expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
  }

  this.deleteCookie = function(name) { createCookie(name,"",-1); }
  
  // SUMMARY: Returns is specific element viewble to the user
  this.IsViewable = function (elem) {

    var positionY = this.GetScrollPosition();
    var windowHeight = $(window).height();

    var elemPosition = elem.offset().top;
    var elemHeight = elem.height();


    if (elemPosition >= positionY && elemPosition <= (positionY + windowHeight) ||
			(elemPosition + elemHeight) >= positionY && (elemPosition + elemHeight) <= (positionY + windowHeight))
      return true;

    return false;
  }

  // SUMMARY: Returns scroll position of user on browser
  this.GetScrollPosition = function () {
    if (window.pageYOffset != undefined)
      return pageYOffset;
    else {
      var sx, sy, d = document, r = d.documentElement, b = d.body;
      // sx= r.scrollLeft || b.scrollLeft || 0;
      sy = r.scrollTop || b.scrollTop || 0;
      return sy;
    }
  }

  // SUMMARY: Creates ID with given prefix (prefix_id)
  this.ID = function (prefix) {
    if (typeof prefix == undefined) prefix = ""; else prefix += "_";
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < 15; i++) text += possible.charAt(Math.floor(Math.random() * possible.length));
    return prefix + text;
  }

  // SUMMARY: Get random number from min to max
  this.Random = function (min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  this.LaddaButton = function (btn, callback_func)
  {
    var buttonID;
    if (typeof (btn.attr('id')) === 'undefined') {
      buttonID = this.ID('button');
      btn.attr('id', buttonID);
    }
    else
      buttonID = btn.attr('id');
    Ladda.bind('#' + buttonID, { callback: function (instance) { callback_func(instance); } });
  }

  this.init();
}
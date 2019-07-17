function ClickManager() {
  this.init = function () {
    $('#countrySelect, #offersSelect, #platformSelect, #browsersSelect').select2();
    this.filterOpenBtn();
    this.load();

    var self = this;
    $('#loadBtn').click(function () { self.load(); });
  }

  this.filterOpenBtn = function () {
    $('#filterBtnOpener').click(function () {
      var body = $('#filterOpenerBody');
      if (body.hasClass('filterOpenerBody_closed'))
        body.removeClass('filterOpenerBody_closed');
      else
        body.addClass('filterOpenerBody_closed');
    });
  }

  this.select2GetData = function (id) {
    var data = $('#' + id).select2('data');
    var result = '';
    for (var i = 0; i < data.length; i++) 
      result += (result != '' ? ',' : '') + data[i].id;
    return result;
  }

  this.parameters = function () {
    var self = this;
    return {
      Limit: $('#_limit').val(),
      From: $('#_from').val(),
      To: $('#_to').val(),
      ClickID: $('#_clickID').val(),
      SubID: $('#_subID').val(),
      IPAddress: $('_ipAddress').val(),
      Offers: self.select2GetData('offersSelect'),
      Countries: self.select2GetData('countrySelect'),
      Browsers: self.select2GetData('browsersSelect'),
      Platforms: self.select2GetData('platformSelect'),
      OnlyWithTransaction: $('#_onlyWithTransactions').is(':checked') ? '1' : '0'
    };
  }

  this.doubleClick = function (id) {
    var win = window.open('/clickinfo/?id=' + id, '_blank');
    win.focus();
  }

  this.load = function () {
    var self = this;
    _system.call('/ClickApi/Load', self.parameters(), function (response) {
      if (typeof (response.message) !== 'undefined')
      {
        _modal.Danger('Error', response.message);
        return;
      }

      $('#clickDataTableBody').html(response);
    });
  }

  this.init();
}
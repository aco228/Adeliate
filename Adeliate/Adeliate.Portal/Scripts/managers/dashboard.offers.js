function DashboardOffersManager()
{
  this.take = 10;
  this.skip = 0;
  this.currentOffers = 0;
  this.busy = false;
  this.offersCount = 0;

  this.init = function () {
    var self = this;
    this.actions();
    this.load();

    _signal.get('click').clickUpdates.push(function (data) {
      var offerElem = $('#__offersLoadOffer_' + data.offerID);
      if (offerElem.length == 0)
        return;

      var value = parseInt(offerElem.attr('clicks')) + 1;
      offerElem.attr('clicks', value);
      self.updateAll();
    });

    _signal.get('click').transactionUpdates.push(function (data) {
      var offerElem = $('#__offersLoadOffer_' + data.offerID);
      if (offerElem.length == 0)
        return;

      var value = parseInt(offerElem.attr('transactions')) + 1;
      offerElem.attr('transactions', value);
      self.updateAll();
    });
  }

  this.actions = function () {
    var self = this;
    $('#__offersLoadActionsLoadMore').click(function () { self.load(); });
  }

  this.load = function () {
    var self = this;
    if (self.busy) return;
    self.busy = true;

    _system.call('/DashboardApi/DashboardOffersLoadMore',
      { take: self.take, skip: self.skip },
      function (response) {
        self.busy = false;
        self.skip += self.take;

        if (typeof (response.status) !== 'undefined') {
          _modal.Danger(response.message);
          return;
        }
        
        $('#__offersLoadHolder').append(response);
        self.currentOffers = $('.__offersLoadOffer').length;

        if (self.currentOffers >= self.offersCount)
          $('#__offersLoadActionsLoadMore').css('display', 'none');

        self.updateAll();
      });
  }

  this.updateAll = function () {
    $('.__offersLoadOffer').each(function () {
      var clicks = parseInt($(this).attr('clicks'));
      var transactions = parseInt($(this).attr('transactions'));
      var capValue = parseInt($(this).attr('cap'));
      var convertionRate = (transactions / clicks) * 100;
      var finishRate = (transactions / capValue) * 100;

      $(this).find('.__offersLoadOffer_clicks').text(clicks);
      $(this).find('.__offersLoadOffer_transactions').text(transactions);
      $(this).find('.__offersLoadOffer_convertionRate').text(convertionRate.toFixed(2) + '%');
      $(this).find('.__offersLoadOffer_progress').css('width', finishRate + '%');
      $(this).find('.__offersLoadOffer_progressInfo').text(finishRate + '%');
    });
  }


  this.init();
}
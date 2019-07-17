function OffersManager()
{
  this.busy = false;
  this.top = 10;
  this.lastID = 0;

  this.init = function () {
    var self = this;
    
    this.filterOperators();
    this.load(false);

    _system.LaddaButton($('#_button'), function (instance) {
      self.lastID = 0;
      self.load(false, function () { instance.stop(); });
    });

    _system.LaddaButton($('#_buttonAppend'), function (instance) {
      self.load(true, function () { instance.stop(); });
    });
  }

  this.filterOperators = function () {

  }

  select2GetData = function (id) {
    var data = $('#' + id).select2('data');
    var result = '';
    for (var i = 0; i < data.length; i++)
      result += (result != '' ? ',' : '') + data[i].id;
    return result;
  }

  this.collectData = function () {
    var self = this;
    return {
      Keyword: $('#_keyword').val(),
      //CountryID: $('#_country option:selected').attr('value'),
      //MobileOperatorID: $('#_mobileOperator option:selected').attr('value'),
      //Device: $('#_device option:selected').attr('value'),
      //Flow: $('#_flow option:selected').attr('value'),
      //ContentType: $('#_content option:selected').attr('value'),
      CountryID: select2GetData('_country') == "" ? "-1" : select2GetData('_country'),
      MobileOperatorID: select2GetData('_mobileOperator') == "" ? "-1" : select2GetData('_mobileOperator'),
      Device: select2GetData('_device') == "" ? "-1" : select2GetData('_device'),
      Flow: select2GetData('_flow') == "" ? "-1" : select2GetData('_flow'),
      ContentType: select2GetData('_content') == "" ? "-1" : select2GetData('_content'),
      LastID: self.lastID,
      Top: self.top
    };
  }

  this.load = function (append, succ_func) {
    var self = this;
    if (self.busy) return;

    if (!append)
      $('#offerDataHolder').html('');
    
    $('#offersLoader').fadeIn(350);
    _system.call('/Offers/Load', this.collectData(), function (response) {
      self.busy = false;
      self.skip += self.take;

      if (typeof (succ_func) === 'function')
        succ_func();
      
      $('#offerDataHolder').append(response);
      $('#offersLoader').fadeOut(350);

      self.currentOffers = 0;
      $('.__offersLoadOffer').each(function (elem) {
        self.currentOffers++;
        var id = parseInt($(this).attr('offerID'));
        if (self.lastID == 0 || id < self.lastID)
          self.lastID = id;
      });

      if (self.currentOffers >= self.offersCount)
        $('#__offersLoadActionsLoadMore').css('display', 'none');
    });
  }

  this.init();
}
_signal.hubs.push(new function () {
  this.name = 'click';
  this.hub = null;
  this.server = null;
  this.clickUpdates = new Array();
  this.transactionUpdates = new Array();
    
  this.clicks = 0;
  this.transactions = 0;

  // SUMMARY: Prepare everything
  this.init = function () {
    this.clicks = _clicks;
    this.transactions = _transactions;
    this.update();
  }
  
  // SUMMARY: Update new values
  this.update = function () {
    console.log('update ' + this.clicks);
    var convertionRate = (this.transactions / this.clicks) * 100;
    $('.__signal_clicks').text(this.clicks);
    $('.__signal_transactions').text(this.transactions);
    $('.__signal_conversionRate').text(convertionRate.toFixed(2) + '%');
    $('.__signal_conversionRate_bar').css('width', convertionRate.toFixed(2) + '%');
  }
  
  // SUMMARY: On info that new click has came
  this.newClick = function (data) {
    this.clicks++;
    this.update();

    for (var i = 0; i < this.clickUpdates.length; i++)
      if (typeof (this.clickUpdates[i]) === 'function')
        this.clickUpdates[i](data);

  }

  // SUMMARY: On info that new conversion is came
  this.newTransaction = function (data) {
    this.transactions++;
    _modal.Toast("New transaction");
    this.update();
    
    for (var i = 0; i < this.transactionUpdates.length; i++)
      if (typeof (this.transactionUpdates[i]) === 'function')
        this.transactionUpdates[i](data);

    var transactionMessage = $('.transactionMessage').first().clone();
    transactionMessage.find('.transactionMessageImg').attr('src', '/Images/campaign?id=' + data.campaignID);
    transactionMessage.find('.transactionMessageCreated').text('Now');
    transactionMessage.find('.transactionMessageName').text(data.campaignName);
    $('#transactionMessageHolder').prepend(transactionMessage);
  }

  this.updateRevenue = function (data) {
    console.log(data);
    $('.__signal_revenue').text(data.data);
  }
  
  _signal.call(this);
});
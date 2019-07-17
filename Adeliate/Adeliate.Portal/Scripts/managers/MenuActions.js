$(document).ready(function () {

  function basicAction(button, url, title, succ_func) {
    if (button.attr('inuse') == 'true') return;
    var tempText = button.text();
    button.text('Please wait..');
    button.attr('inuse', 'true');

    _system.call(url, {}, function (response) {
      button.attr('inuse', 'false');
      button.text(tempText);
      _modal.Primary(title, response.message);

      if (typeof (succ_func) === 'function')
        succ_func(response);
    });
  }

  $('#action_reloadCache').click(function () {
    basicAction($(this), '/HeaderActionsApi/ReloadCache', 'Reload cache');
  });

  $('#action_restartApplication').click(function () {
    basicAction($(this), '/HeaderActionsApi/RestartApplication', 'Restart application', function () { location.reload(); });
  });

});
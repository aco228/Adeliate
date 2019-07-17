function Modal() {

  this._base = function (modal, className, title, text, confirm, deny, onconfirm_function) {
    var newModalID = _system.ID('modal');
    var modal = modal.clone();
    modal.attr('id', newModalID);
    $('#modalHolder').append(modal);

    modal.find('.modal-title').text(title);
    modal.find('.modal_text').text(text);
    var modalDialog = modal.find('.modal-dialog');
    modalDialog.attr('class', 'modal-dialog ' + className);

    var cancelButton = modal.find('.btn-secondary');
    if (typeof (deny) === 'undefined') deny = 'Close';
    cancelButton.text(deny);
    cancelButton.click(function () { modal.modal('toggle'); });

    var confirmButton = modal.find('.btn-primary');
    if (typeof (confirm) === 'undefined') confirm = 'Confirm';
    confirmButton.text(confirm);
    confirmButton.click(function () {
      modal.modal('toggle');
      setTimeout(function () { modal.remove(); }, 1000);
      if (typeof (onconfirm_function) === 'function')
        onconfirm_function();
    });

    modal.find('.close').click(function () { modal.modal('toggle'); setTimeout(function () { modal.remove(); }, 1000); });
    modal.modal('show');
  }
  
  this.Primary = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = '';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Success = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = 'modal-success';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Warning = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = 'modal-warning';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Danger = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = 'modal-danger';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Info = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = 'modal-info';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Small = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#primaryModal');
    var className = 'modal-sm';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.Large = function (title, text, onconfirm_function, confirm, deny) {
    var modal = $('#successModal');
    var className = 'modal-lg';
    this._base(modal, className, title, text, confirm, deny, onconfirm_function);
  }

  this.ToastBase = function (type, position, duration, text) {
    toastr.options = {
      "closeButton": false, "debug": false,
      "newestOnTop": true, "progressBar": false,
      "positionClass": position, "preventDuplicates": false,
      "onclick": null, "showDuration": "300", "hideDuration": "1000", "timeOut": duration,
      "extendedTimeOut": "1000", "showEasing": "swing", "hideEasing": "linear", "showMethod": "fadeIn", "hideMethod": "fadeOut"
    };
    toastr[type](text);
  }

  this.Toast = function (text, duration, position) {
    if (typeof (position) === 'undefined') position = "top-right";
    if (typeof (duration) === 'undefined') duration = 3000;
    this.ToastBase("success", "toast-" + position, duration, text);
  }

  this.ToastError = function (text, duration, position) {
    if (typeof (position) === 'undefined') position = "top-right";
    if (typeof (duration) === 'undefined') duration = 3000;
    this.ToastBase("error", "toast-" + position, duration, text);
  }

  this.ToastInfo = function (text, duration, position) {
    if (typeof (position) === 'undefined') position = "top-right";
    if (typeof (duration) === 'undefined') duration = 3000;
    this.ToastBase("info", "toast-" + position, duration, text);
  }

  this.ToastWarning = function (text, duration, position) {
    if (typeof (position) === 'undefined') position = "top-right";
    if (typeof (duration) === 'undefined') duration = 3000;
    this.ToastBase("warning", "toast-" + position, duration, text);
  }

}
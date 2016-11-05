(function () {
  'use strict';

  angular.module('app')
    .service('spinnerService', spinnerService);

  spinnerService.$inject = ['$rootScope'];

  function spinnerService($rootScope) {
    this.showSpinner = function () {

      $rootScope.spinnerShown = true;
    }
    this.hideSpinner = function () {

      $rootScope.spinnerShown = false;
    }
  }
})();
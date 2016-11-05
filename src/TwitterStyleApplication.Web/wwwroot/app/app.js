(function (angular) {
  'use strict';

  // Angular module for the application
  angular.module('app', [
    'ngRoute',
    'Alertify',
    'angularSpinner',
    'ui.bootstrap'
  ]);

  angular.module('app').run(['Session', '$rootScope',
    function (Session, $rootScope) {
      Session.fillAuthData();
      $rootScope.isAuth = Session.isAuth;
    }
  ]);
})(angular);
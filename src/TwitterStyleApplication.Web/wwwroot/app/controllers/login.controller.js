(function () {
  'use strict';

  angular
      .module('app')
      .controller('loginController', loginController);

  loginController.$inject = ['$location', 'userService', 'Session', '$rootScope'];

  function loginController($location, userService, Session, $rootScope) {
    var vm = this;
    vm.loginData = {};
    vm.login = login;
    vm.message = "";

    function login(data) {
      return userService.login(data).then(function (result) {
        var token = 'Bearer ' + result.access_token;
        Session.create(token, data.username);
        $rootScope.isAuth = true;
        $rootScope.userName = data.username;
        $location.path('/');

      }).catch(function (err) {
        vm.created = false;
        vm.message = err.data;
      });
    }
  }
})();

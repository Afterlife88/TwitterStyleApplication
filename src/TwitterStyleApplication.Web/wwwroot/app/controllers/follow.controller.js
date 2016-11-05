(function () {
  'use strict';

  angular
      .module('app')
      .controller('followController', followController);

  followController.$inject = ['$location', 'userService', 'Alertify'];

  function followController($location, userService, Alertify) {
    var vm = this;
    vm.users = [];
    vm.follow = follow;
    init();

    function init() {
      userService.getAll().then(function (response) {
        vm.users = response;
      }).catch(function (err) {

        console.log(err);
        vm.message = err.data;
      });
    }

    function follow(userName) {
      var request = {
        username: userName
      }

      userService.follow(request).then(function (response) {
        console.log(response);
        Alertify.success('User followed!');
        init();
      }).catch(function (err) {

        console.log(err);
        vm.message = err.data;
        Alertify.error(err.data);
      });
    }
  }
})();

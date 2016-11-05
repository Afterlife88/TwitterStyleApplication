(function () {
  'use strict';

  angular
      .module('app')
      .controller('relationshipController', relationshipController);

  relationshipController.$inject = ['$location', 'userService', 'Alertify'];

  function relationshipController($location, userService, Alertify) {
    var vm = this;
    vm.followers = [];
    vm.following = [];
    vm.unfollow = unfollow;
    vm.follow = follow;
    init();

    function init() {
      userService.getRelationship().then(function (response) {
        console.log(response);
        vm.followers = response.followers;
        vm.following = response.following;
      }).catch(function (err) {

        console.log(err);
        vm.message = err.data;
      });
    }

    function unfollow(userName) {
      var request = {
        username: userName
      }

      userService.unfollow(request).then(function (response) {
        console.log(response);
        Alertify.success('User unfollowed!');
        init();
      }).catch(function (err) {

        console.log(err);
        vm.message = err.data;
        Alertify.error(err.data);
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

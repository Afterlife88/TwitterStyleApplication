(function (angular) {
  'use strict';

  angular
      .module('app')
      .controller('signupController', signupController);

  signupController.$inject = ['$location', 'userService'];

  function signupController($location, userService) {
    var vm = this;
    vm.signUpForm = {};
    vm.submitSignUp = submitSignUp;
    vm.created = false;
    vm.message = "";

    function submitSignUp(data) {
      return userService.signUp(data).then(function (result) {
        //console.log(result);
        vm.message = "User has been registered successfully. You can login to system right now";
        vm.created = true;
      }).catch(function (err) {
        vm.created = false;
        console.log(err);
        vm.message = err.data;
      });
    }
  }
})(angular);

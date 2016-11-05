(function (angular) {
  'use strict';

  angular
      .module('app')
      .factory('userService', userService);

  userService.$inject = ['$http', '$q', 'spinnerService'];

  function userService($http, $q, spinnerService) {

    var service = {
      signUp: signUp,
      login: login,
      getRelationship: getRelationship,
      unfollow: unfollow,
      follow: follow,
      getAll: getAll
    };
    return service;


    function signUp(credentials) {
      spinnerService.showSpinner();
      return $http.post('/api/users', credentials).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function login(credentials) {
      spinnerService.showSpinner();
      return $http.post('/api/token', getPostTokenBodyForUser(credentials), {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
      }).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function getRelationship() {
      spinnerService.showSpinner();
      return $http.get('/api/users/relationship').then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }


    function getAll() {
      spinnerService.showSpinner();
      return $http.get('/api/users/').then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
    function unfollow(request) {
      spinnerService.showSpinner();
      return $http.post('/api/users/unfollow', request).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
    function follow(request) {
      spinnerService.showSpinner();
      return $http.post('/api/users/follow', request).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
    function getPostTokenBodyForUser(credentials) {
      return 'username=' + credentials.username + '&password=' + credentials.password;
    }
  }
})(angular);

(function (angular) {
  'use strict';

  angular
      .module('app')
      .factory('tweetsService', tweetsService);

  tweetsService.$inject = ['$http', '$q', 'spinnerService'];

  function tweetsService($http, $q, spinnerService) {

    var service = {
      getAllTweets: getAllTweets,
      createTweet: createTweet

    };
    return service;


    function getAllTweets() {
      spinnerService.showSpinner();
      return $http.get('/api/tweets').then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
    function createTweet(request) {
      spinnerService.showSpinner();
      return $http.post('/api/tweets', request).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
  }
})(angular);

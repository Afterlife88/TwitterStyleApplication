(function (angular) {
  'use strict';

  angular
      .module('app')
      .controller('tweetsController', tweetsController);

  tweetsController.$inject = ['$location', 'tweetsService', 'Alertify'];

  function tweetsController($location, tweetsService, Alertify) {
    var vm = this;
    vm.tweets = [];
    vm.postTweet = postTweet;
    vm.tweetData = '';

    init();
    function init() {
      return tweetsService.getAllTweets().then(function (result) {
        console.log(result);
        vm.tweets = result;
      }).catch(function (err) {
        Alertify.error(err.data);
        console.log(err);
        vm.message = err.data;
      });
    }

    function postTweet(data) {
      var request = {
        messsageData: data
      }
      return tweetsService.createTweet(request).then(function (result) {
        console.log(result);
        vm.tweetData = '';
        init();

      }).catch(function (err) {
        Alertify.error(err.data.messsageData[0]);
        console.log(err);
      
      });


    }
  }
})(angular);

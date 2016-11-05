(function () {
  'use strict';

  angular.module('app').factory('authInterceptorService', authInterceptorService);

  authInterceptorService.$inject = ['$q', '$location', 'Session'];

  function authInterceptorService($q, $location, Session) {
    return {
      request: function (request) {
        if (!request.headers['Authorization'] && Session.accessToken) {
          request.headers['Authorization'] = Session.accessToken;
        }
        return request;
      },
      response: function (response) {
        return response;
      },
      responseError: function (rejection) {
        console.log(rejection);
        if (rejection.status === 401) {
          $location.path('/login');
        }
        return $q.reject(rejection);
      }
    };
  }
})();

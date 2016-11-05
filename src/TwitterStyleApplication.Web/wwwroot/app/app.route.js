angular.module('app').config(['$routeProvider', '$locationProvider', '$httpProvider',
  function ($routeProdiver, $locationProvider, $httpProvider) {
    $routeProdiver
      .when('/login', {
        templateUrl: './app/views/login.view.html',
        controller: 'loginController',
        controllerAs: 'vm'
      })
      .when('/signup', {
        templateUrl: './app/views/signup.view.html',
        controller: 'signupController',
        controllerAs: 'vm'
      })
     .when('/tweets', {
       templateUrl: './app/views/tweets.view.html',
       controller: 'tweetsController',
       controllerAs: 'vm'
     })
     .when('/follow', {
       templateUrl: './app/views/follow.view.html',
       controller: 'followController',
       controllerAs: 'vm'
     })
     .when('/relationship', {
       templateUrl: './app/views/relationship.view.html',
       controller: 'relationshipController',
       controllerAs: 'vm'
     })
      .otherwise({
        redirectTo: "/tweets"
      });

    $locationProvider.hashPrefix('');

    $httpProvider.interceptors.push('authInterceptorService');
  }
]);
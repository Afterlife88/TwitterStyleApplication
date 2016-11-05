angular.module('app')
  .service('Session', function () {
    this.accessToken = localStorage.getItem('accessToken');
    this.userName = localStorage.getItem('userName');
    this.isAuth = false;

    this.create = function (accessToken, userName) {
      this.accessToken = accessToken;
      this.userName = userName;

      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('userName', userName);
    };
    this.destroy = function () {
      this.accessToken = null;
      this.userName = null;

      localStorage.removeItem('accessToken');
      localStorage.removeItem('userName');
    };
    this.fillAuthData = function () {
      var userName = localStorage.getItem("userName");
      if (userName != null) {
        this.isAuth = true;
        this.userName = userName;
      }
    };
  });

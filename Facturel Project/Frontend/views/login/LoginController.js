(function () {
  const app = angular.module("invoices");

  const LoginController = function ($scope, $http, $cookies, $location) {
    $scope.login = function () {
      // show the loading animation
      utils.helperFunctions.showProgressOnForm();

      $http
        .post(`${utils.constants.backendUrl}/User/Login`, {
          username: $scope.username,
          password: $scope.password,
        })
        .then(function (res) {
          // save the token
          const token = res.data.token;
          $cookies.put(utils.constants.tokenCookie, token);

          // save the userId
          const userId = res.data.userId;
          $cookies.put(utils.constants.userIdCookie, userId);

          // modify navigationbar
          utils.helperFunctions.showLoggedIn();

          // stop the loading animation
          utils.helperFunctions.hideProgressOnForm();

          // redirect to the dashboard
          $location.path("/dashboard"); // TODO: Redirect to the dashboard
        }, utils.helperFunctions.errorOnFormWithProgress);
    };
  };

  app.controller("LoginController", LoginController);
})();

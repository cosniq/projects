(function () {
  const app = angular.module("invoices");
  const RegisterController = function ($scope, $http, $cookies, $location) {
    const getCurrentDate = function () {
      const currentDate = new Date();
      return `${currentDate.getFullYear()}/${
        currentDate.getMonth() + 1 < 10
          ? "0" + (currentDate.getMonth() + 1).toString()
          : currentDate.getMonth() + 1
      }/${
        currentDate.getDate() < 10
          ? "0" + currentDate.getDate()
          : currentDate.getDate()
      }`;
    };

    $scope.birthDate = getCurrentDate();

    $scope.register = function () {
      const getRegisterData = function () {
        const birthDate = $scope.birthDate;
        return {
          username: $scope.username,
          password: $scope.password,
          email: $scope.email,
          DateOfBirth: birthDate.replaceAll("/", "-"),
        };
      };

      utils.helperFunctions.showProgressOnForm();

      $http
        .post(`${utils.constants.backendUrl}/User/Register`, getRegisterData())
        .then(function () {
          // log the user in
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

              $http
                .post(`${utils.constants.backendUrl}/Location`, {
                  address: $scope.address,
                  Description: $scope.description,
                  userid: userId,
                })
                .then(function (res) {
                  utils.helperFunctions.showLoggedIn();

                  utils.helperFunctions.hideProgressOnForm();

                  createNotification({
                    theme: "success",
                    positionClass: "nfc-bottom-right",
                  })({
                    title: "Success",
                    message: "You have succesfully signed up!",
                  });

                  $location.path("/dashboard");
                }, utils.helperFunctions.error);
            }, utils.helperFunctions.error);
        }, utils.helperFunctions.errorOnFormWithProgress);
    };
  };
  app.controller("RegisterController", RegisterController);
})();

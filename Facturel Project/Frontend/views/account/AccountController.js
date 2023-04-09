(function () {
  const app = angular.module("invoices");
  const AccountController = function ($scope, $http, $cookies, $route) {
    $scope.account = {
      id: 0,
      username: "",
      email: "",
      dateOfBirth: "2023.01.01",
    };

    const initialize = function () {
      utils.helperFunctions.showProgressOnForm();
      $http
        .get(
          `${
            utils.constants.backendUrl
          }/User/GetDetailsOfUser?userId=${$cookies.get(
            utils.constants.userIdCookie
          )}`
        )
        .then(function (res) {
          utils.helperFunctions.hideProgressOnForm();
          $scope.account = res.data;
          $scope.account.dateOfBirth = $scope.account.dateOfBirth.substring(
            0,
            10
          );
        }, utils.helperFunctions.error);
    };

    initialize();

    $scope.submit = function () {
      utils.helperFunctions.showProgressOnForm();
      $http
        .patch(`${utils.constants.backendUrl}/User`, $scope.account)
        .then(function () {
          utils.helperFunctions.hideProgressOnForm();
          createNotification({
            theme: "success",
            positionClass: "nfc-bottom-right",
          })({
            title: "Success",
            message: "Account has been updated!",
          });
          $route.reload();
        }, utils.helperFunctions.error);
    };
  };
  app.controller("AccountController", AccountController);
})();

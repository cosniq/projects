(function () {
  const app = angular.module("invoices");
  const CountersController = function ($scope, $http, $route) {
    $scope.Counters = undefined;
    $scope.activeCall = false;

    const grid = $("#kendoGridCounters").getKendoGrid();

    const initialize = function () {
      utils.helperFunctions.showProgress("#kendoGridCounters");
      $scope.activeCall = true;
      $http.get(`${utils.constants.backendUrl}/Counter`).then(function (res) {
        $scope.Counters = res.data;
        utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
        utils.helperFunctions.addAllDataToDataSource(
          grid.dataSource,
          $scope.Counters
        );

        $scope.activeCall = false;
        utils.helperFunctions.hideProgress("#kendoGridCounters");
      }, utils.helperFunctions.error);
    };

    grid.bind("save", function (e) {
      utils.helperFunctions.showProgress("#kendoGridCounters");
      const ObjectToSend = {
        Id: e.model.id,
        SerialNumber: e.model.serialNumber,
      };
      $http
        .patch(`${utils.constants.backendUrl}/Counter`, ObjectToSend)
        .then(function () {
          utils.helperFunctions.hideProgress("#kendoGridCounters");
          createNotification({
            theme: "success",
            positionClass: "nfc-bottom-right",
          })({
            title: "Success",
            message: "Serial Number has been updated!",
          });
          $route.reload();
        }, utils.helperFunctions.error);
    });

    initialize();
  };
  app.controller("CountersController", CountersController);
})();

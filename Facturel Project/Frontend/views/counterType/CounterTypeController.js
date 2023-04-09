(function () {
  const app = angular.module("invoices");
  const CounterTypeController = function ($scope, $http, $route) {
    $scope.CounterTypes;
    $scope.activeCall = false;
    const grid = $("#kendoCounterTypeGrid").getKendoGrid();

    const initialize = function () {
      utils.helperFunctions.showProgress("#kendoCounterTypeGrid");
      $scope.activeCall = true;
      $http
        .get(`${utils.constants.backendUrl}/CounterType`)
        .then(function (res) {
          utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
          utils.helperFunctions.addAllDataToDataSource(
            grid.dataSource,
            res.data
          );
          $scope.CounterTypes = res.data;
          $scope.activeCall = false;
          utils.helperFunctions.hideProgress("#kendoCounterTypeGrid");
        }, utils.helperFunctions.error);

      grid.bind("dataBound", function (e) {
        if (
          $scope.CounterTypes !== undefined &&
          e.sender.dataSource.data().length < $scope.CounterTypes.length &&
          !$scope.activeCall
        ) {
          $scope.activeCall = true;
          utils.helperFunctions.deleteAllDataFromDataSource(
            e.sender.dataSource
          );
          utils.helperFunctions.addAllDataToDataSource(
            e.sender.dataSource,
            $scope.CounterTypes
          );
          $scope.activeCall = false;
        }
      });

      grid.bind("save", function (e) {
        const onSuccess = function () {
          utils.helperFunctions.hideProgress("#kendoCounterTypeGrid");
          createNotification({
            theme: "success",
            positionClass: "nfc-bottom-right",
          })({
            title: "Success",
            message: "Data has been saved!",
          });
          $route.reload();
        };

        const onError = function (err) {
          utils.helperFunctions.error(err);
          $route.reload();
        };

        const ObjectToSend = {
          Name: e.model.name,
          UnitOfMeasurement: e.model.unitOfMeasurement,
        };
        if (e.model.id === 0) {
          // adding new counter type
          utils.helperFunctions.showProgress("#kendoCounterTypeGrid");
          $http
            .post(`${utils.constants.backendUrl}/CounterType`, ObjectToSend)
            .then(onSuccess, onError);
        } else {
          // modify an existing one
          utils.helperFunctions.showProgress("#kendoCounterTypeGrid");
          ObjectToSend.id = e.model.id;
          $http
            .patch(`${utils.constants.backendUrl}/CounterType`, ObjectToSend)
            .then(onSuccess, onError);
        }
      });
    };

    initialize();
  };
  app.controller("CounterTypeController", CounterTypeController);
})();

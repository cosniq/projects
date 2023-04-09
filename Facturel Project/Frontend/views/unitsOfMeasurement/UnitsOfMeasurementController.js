(function () {
  const app = angular.module("invoices");
  const UnitsOfMeasurementController = function ($http, $route) {
    const grid = $("#kendoUoMGrid").getKendoGrid();

    grid.bind("save", function (e) {
      utils.helperFunctions.showProgress("#kendoUoMGrid");
      $http
        .post(
          `${utils.constants.backendUrl}/UnitOfMeasurements?symbol=${e.model.symbol}`
        )
        .then(function (res) {
          utils.helperFunctions.hideProgress("#kendoUoMGrid");
          createNotification({
            theme: "success",
            positionClass: "nfc-bottom-right",
          })({
            title: "Success",
            message: "New Unit of Measurement has been added!",
          });
          $route.reload();
        }, utils.helperFunctions.error);
    });

    const initialize = function () {
      utils.helperFunctions.showProgress("#kendoUoMGrid");
      $http
        .get(`${utils.constants.backendUrl}/UnitOfMeasurements`)
        .then(function (res) {
          utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
          utils.helperFunctions.addAllDataToDataSource(
            grid.dataSource,
            res.data
          );
          utils.helperFunctions.hideProgress("#kendoUoMGrid");
        }, utils.helperFunctions.error);
    };

    initialize();
  };
  app.controller("UnitsOfMeasurementController", UnitsOfMeasurementController);
})();

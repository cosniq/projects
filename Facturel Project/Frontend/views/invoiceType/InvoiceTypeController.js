(function () {
  const app = angular.module("invoices");
  const InvoiceTypeController = function ($scope, $http, $route) {
    const grid = $("#kendoGridInvoiceTypes").getKendoGrid();
    $scope.InvoiceTypes;
    $scope.activeCall = false;

    const initialize = function () {
      utils.helperFunctions.showProgress("#kendoGridInvoiceTypes");
      $scope.activeCall = true;
      $http
        .get(`${utils.constants.backendUrl}/InvoiceType`)
        .then(function (res) {
          utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
          utils.helperFunctions.addAllDataToDataSource(
            grid.dataSource,
            res.data
          );
          $scope.InvoiceTypes = res.data;
          $scope.activeCall = false;
          utils.helperFunctions.hideProgress("#kendoGridInvoiceTypes");
        }, utils.helperFunctions.error);
    };

    grid.bind("save", function (e) {
      const onSuccess = function () {
        utils.helperFunctions.hideProgress("#kendoGridInvoiceTypes");
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
        CounterType: e.model.counterType,
        CostOnlyDependentOnUsage: e.model.costOnlyDependentOnUsage,
      };
      utils.helperFunctions.showProgress("#kendoGridInvoiceTypes");
      if (e.model.id === 0) {
        // adding new counter type
        $http
          .post(`${utils.constants.backendUrl}/InvoiceType`, ObjectToSend)
          .then(onSuccess, onError);
      } else {
        // modify an existing one
        ObjectToSend.id = e.model.id;
        $http
          .patch(`${utils.constants.backendUrl}/InvoiceType`, ObjectToSend)
          .then(onSuccess, onError);
      }
    });

    initialize();
  };
  app.controller("InvoiceTypeController", InvoiceTypeController);
})();

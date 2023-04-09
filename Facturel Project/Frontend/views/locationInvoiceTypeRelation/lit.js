(function () {
  const app = angular.module("invoices");
  const LocationInvoiceTypeRelationController = function (
    $scope,
    $http,
    $route
  ) {
    const grid = $("#kendoLITGrid").getKendoGrid();
    $scope.LITRelations;
    $scope.activeCall = false;

    const initialize = function () {
      $scope.activeCall = true;
      utils.helperFunctions.showProgress("#kendoLITGrid");
      $http
        .get(`${utils.constants.backendUrl}/LocationInvoiceTypeRelations`)
        .then(function (res) {
          utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
          utils.helperFunctions.addAllDataToDataSource(
            grid.dataSource,
            res.data
          );
          $scope.LITRelations = res.data;
          $scope.activeCall = false;
          utils.helperFunctions.hideProgress("#kendoLITGrid");
        }, utils.helperFunctions.error);
    };

    grid.bind("save", function (e) {
      const onSuccess = function () {
        utils.helperFunctions.hideProgress("#kendoLITGrid");
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
        LocationDescription: e.model.locationDescription,
        InvoiceTypeName: e.model.invoiceTypeName,
      };
      utils.helperFunctions.showProgress("#kendoLITGrid");
      if (e.model.id === 0) {
        // adding new element
        $http
          .post(
            `${utils.constants.backendUrl}/LocationInvoiceTypeRelations`,
            ObjectToSend
          )
          .then(onSuccess, onError);
      }
    });

    grid.bind("remove", function (e) {
      utils.helperFunctions.showProgress("#kendoLITGrid");
      $http
        .delete(
          `${utils.constants.backendUrl}/LocationInvoiceTypeRelations?relationId=${e.model.id}`
        )
        .then(function (res) {
          utils.helperFunctions.hideProgress("#kendoLITGrid");
          createNotification({
            theme: "warning",
            positionClass: "nfc-bottom-right",
          })({
            title: "Success",
            message: "Relation has been deletd!",
          });
          $route.reload();
        }, utils.helperFunctions.error);
    });

    initialize();
  };
  app.controller(
    "LocationInvoiceTypeRelationController",
    LocationInvoiceTypeRelationController
  );
})();

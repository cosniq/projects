(function () {
  const app = angular.module("invoices");
  const DashboardController = function ($scope, $http, $cookies, $route) {
    const customSaveBtn = $(".k-grid-CustomSave");

    customSaveBtn.on("click", function (e) {
      e.preventDefault();

      if (Observables.isDirty) {
        // ===================
        // Submit ViewModel
        // ===================
        utils.helperFunctions.showProgress("#kendoGridDashboard");
        utils.helperFunctions.formatViewModelDatesForBackend();
        $http
          .put(`${utils.constants.backendUrl}/Location`, ViewModel)
          .then(function (res) {
            utils.helperFunctions.hideProgress("#kendoGridDashboard");
            ViewModel = undefined;
            OriginalViewModel = undefined;
            createNotification({
              theme: "success",
              positionClass: "nfc-bottom-right",
            })({
              title: "Success",
              message: "Data has been saved!",
            });
            $route.reload();
          }, utils.helperFunctions.error);
      }
    });

    customSaveBtn.find("span").addClass("k-icon").addClass("k-save");

    const generateNewMonthBtn = $(".k-grid-CustomGenerateNewMonth");
    generateNewMonthBtn.on("click", function (e) {
      e.preventDefault();
      if (generateNewMonthBtn.attr("disabled") !== "disabled") {
        $scope.generateNewMonth();
      }
    });
    //generateNewMonthBtn.find("span").addClass("k-icon").addClass("k-calendar");

    const _changeEvent = function (e) {
      if ($("#kendoGridDashboard").getKendoGrid().select().length) {
        const tds = this.select().find("td");
        const locationAddress = $(tds[1]).text();
        const locationDescription = $(tds[2]).text();

        // show loading
        const editBtn = tds.last();
        utils.helperFunctions.showProgress(editBtn);

        // build up selectedLocation object
        Observables.selectedLocation = {
          Address: locationAddress,
          Description: locationDescription,
          UserId: Number($cookies.get(utils.constants.userIdCookie)),
          DataLoaded: false,
        };

        // make an http call to get that location's id
        $http
          .post(
            `${utils.constants.backendUrl}/Location/GetIdOfLocation`,
            Observables.selectedLocation
          )
          .then(function (res) {
            Observables.selectedLocation.Id = res.data;
            Observables.selectedLocation.DataLoaded = true;

            utils.helperFunctions.hideProgress(editBtn);
          }, utils.helperFunctions.error);
      } else {
        Observables.selectedLocation = undefined;
        if (!["$digest", "$apply"].includes($scope.$$phase)) {
          $scope.$digest();
        }
      }
    };

    const initializeMainGrid = function () {
      const grid = $("#kendoGridDashboard").getKendoGrid();

      utils.helperFunctions.deleteAllDataFromDataSource(grid.dataSource);
      utils.helperFunctions.addAllDataToDataSource(
        grid.dataSource,
        ViewModel.locations
      );
    };

    const initialize = function () {
      utils.helperFunctions.showProgress("#kendoGridDashboard"); // shows a loading animation on an element

      $http.get(`${utils.constants.backendUrl}/Location`).then(function (res) {
        // save the data in the global variable
        ViewModel = res.data;

        // ================================================================
        // Unless done like this, the object's references would be the same
        // ================================================================
        OriginalViewModel = JSON.stringify(res.data);
        OriginalViewModel = JSON.parse(OriginalViewModel);
        // ================================================================
        utils.helperFunctions.sanitizeViewModelDates();
        initializeMainGrid();

        utils.helperFunctions.hideProgress("#kendoGridDashboard"); // stops showing a loading animation on an element
      }, utils.helperFunctions.error);
    };

    initialize();

    Observables.defineNewObservable("selectedLocation", undefined);

    const selectedLocationSubscriptionObject =
      Observables.subscribeToValueChange(
        "selectedLocation",
        function (newValue) {
          if (newValue === undefined) {
            generateNewMonthBtn.attr("disabled", true);
          } else {
            generateNewMonthBtn.attr("disabled", false);
          }
        }
      );

    generateNewMonthBtn.attr("disabled", true);

    Observables.defineNewObservable("monthToBeEdited", undefined);

    Observables.defineNewObservable("currentlyEditedCell", undefined);

    Observables.defineNewObservable("isDirty", false);

    Observables.subscribeToValueChange("isDirty", function (newValue) {
      if (newValue) {
        customSaveBtn.attr("disabled", false);
        $(".k-grid-edit").each(function (index, element) {
          $(element).attr("disabled", true);
        });
      } else {
        customSaveBtn.attr("disabled", true);
        $(".k-grid-edit").each(function (index, element) {
          $(element).attr("disabled", false);
        });
      }
    });

    customSaveBtn.attr("disabled", true);

    const grid = $("#kendoGridDashboard").getKendoGrid();

    grid.bind("save", function (e) {
      if (
        Observables.selectedLocation !== undefined &&
        Observables.selectedLocation.DataLoaded === true
      ) {
        // case when updating an existing location
        const newAddress = e.model.address;
        const newDescription = e.model.description;
        Observables.selectedLocation.Address = newAddress;
        Observables.selectedLocation.Description = newDescription;

        const ObjectToSend = Observables.selectedLocation;
        delete ObjectToSend.DataLoaded;
        ViewModel = undefined;
        OriginalViewModel = undefined;

        e.sender.cancelRow(); // close the edit window

        utils.helperFunctions.showProgress("#kendoGridDashboard");

        $http
          .patch(`${utils.constants.backendUrl}/Location`, ObjectToSend)
          .then(function (res) {
            utils.helperFunctions.hideProgress("#kendoGridDashboard");
            createNotification({
              theme: "success",
              positionClass: "nfc-bottom-right",
            })({
              title: "Success",
              message: "Location has been updated!",
            });
            $route.reload();
          }, utils.helperFunctions.error);
      } else {
        // case when creating a new location
        utils.helperFunctions.showProgress("#kendoGridDashboard");

        ViewModel = undefined;
        OriginalViewModel = undefined;

        e.sender.cancelRow(); // close the edit window

        const ObjectToSend = {
          address: e.model.address,
          Description: e.model.description,
          userId: Number($cookies.get(utils.constants.userIdCookie)),
        };

        $http
          .post(`${utils.constants.backendUrl}/Location`, ObjectToSend)
          .then(function (res) {
            utils.helperFunctions.hideProgress("#kendoGridDashboard");
            createNotification({
              theme: "success",
              positionClass: "nfc-bottom-right",
            })({
              title: "Success",
              message: "New location has been created!",
            });

            $route.reload();
          }, utils.helperFunctions.error);
      }
    });

    grid.bind("change", _changeEvent);

    grid.bind("dataBound", function (e) {
      Observables.selectedLocation = undefined;
      if (!["$digest", "$apply"].includes($scope.$$phase)) {
        $scope.$digest();
      }
      if (e.sender.dataSource.data().length === 0 && ViewModel !== undefined) {
        utils.helperFunctions.addAllDataToDataSource(
          e.sender.dataSource,
          ViewModel.locations
        );
      }
    });

    utils.helperFunctions.makeBindingsForMonthGrid = function (stringId) {
      const monthGrid = $(document.getElementById(stringId)).getKendoGrid();
      monthGrid.bind("save", function (e) {
        const ObjectToSend = {
          OldDescription: Observables.monthToBeEdited,
          NewDescription: e.model.description,
          LocationDescription: this._cellId
            .replace("_active_cell", "")
            .replace("monthGridForLocation:", ""),
        };

        this.cancelRow();

        utils.helperFunctions.showProgress("#kendoGridDashboard");
        $http
          .patch(`${utils.constants.backendUrl}/Months`, ObjectToSend)
          .then(function (res) {
            utils.helperFunctions.hideProgress("#kendoGridDashboard");
            ViewModel = undefined;
            OriginalViewModel = undefined;
            createNotification({
              theme: "success",
              positionClass: "nfc-bottom-right",
            })({
              title: "Success",
              message: "Month description has been updated!",
            });
            $route.reload();
          }, utils.helperFunctions.error);
      });
    };

    utils.helperFunctions.makeBindingsForInvoicesGrid = function (stringId) {
      const invoicesGrid = $(document.getElementById(stringId)).getKendoGrid();

      invoicesGrid.bind("save", function (e) {
        // =============================================
        // Updates the ViewModel with the new values
        // =============================================
        let newValue;
        if (Object.values(e.values).length !== 1) {
          utils.helperFunctions.error("Editing multiple/no objects");
          throw new Error("Editing multiple/no objects");
        } else {
          newValue = Object.values(e.values)[0];

          if (newValue instanceof Date) {
            newValue = utils.helperFunctions.formatDateForDotNet(newValue);
            
          } else if (
            typeof newValue === "string" &&
            !isNaN(newValue.replaceAll(",", "."))
          ) {
            newValue = Number(newValue.replaceAll(",", "."));
          }
        }

        if (Observables.currentlyEditedCell === undefined) {
          utils.helperFunctions.error("Editing unknown object");
          throw new Error("Editing unknown object");
        }

        const fieldNameFromViewModel =
          utils.helperFunctions.getFieldNameFromViewModel(
            Observables.currentlyEditedCell.column
          );

        if (fieldNameFromViewModel === null) {
          utils.helperFunctions.error("Editing unknown object");
          throw new Error("Editing unknown object");
        }

        if (!fieldNameFromViewModel.includes(".")) {
          ViewModel.locations
            .find(
              (l) =>
                l.description ==
                Observables.currentlyEditedCell.locationDescription
            )
            .months.find(
              (m) =>
                m.description ==
                Observables.currentlyEditedCell.monthDescription
            )
            .invoices.find(
              (i) =>
                i.invoiceTypeName ==
                Observables.currentlyEditedCell.invoiceTypeName
            )[fieldNameFromViewModel] = newValue;
          Observables.isDirty = true;
        } else {
          const index = fieldNameFromViewModel.indexOf(".");
          const firstFieldName = fieldNameFromViewModel.substring(0, index);
          const secondFieldName = fieldNameFromViewModel.substring(index + 1);
          ViewModel.locations
            .find(
              (l) =>
                l.description ==
                Observables.currentlyEditedCell.locationDescription
            )
            .months.find(
              (m) =>
                m.description ==
                Observables.currentlyEditedCell.monthDescription
            )
            .invoices.find(
              (i) =>
                i.invoiceTypeName ==
                Observables.currentlyEditedCell.invoiceTypeName
            )[firstFieldName][secondFieldName] = newValue;
          Observables.isDirty = true;
        }
      });

      invoicesGrid.bind("dataBound", function (e) {
        const monthDescription = stringId.replace("invoicesGridForMonth:", "");
        const locationDescription = $(document.getElementById(stringId))
          .parents("[data-role='grid']")
          .first()
          .attr("id")
          .replace("monthGridForLocation:", "");

        let originalValues = OriginalViewModel.locations
          .find((l) => l.description == locationDescription)
          .months.find((m) => m.description == monthDescription).invoices;
        originalValues = JSON.stringify(originalValues);
        ViewModel.locations
          .find((l) => l.description == locationDescription)
          .months.find((m) => m.description == monthDescription).invoices =
          JSON.parse(originalValues);
        Observables.isDirty = utils.helperFunctions.isUiDirty();
      });
    };

    $scope.generateNewMonth = function () {
      utils.helperFunctions.showProgress("#kendoGridDashboard");

      if (
        Observables.selectedLocation &&
        Observables.selectedLocation.DataLoaded
      ) {
        $http
          .post(
            `${utils.constants.backendUrl}/Months?locationDescription=${Observables.selectedLocation.Description}`
          )
          .then(function (res) {
            utils.helperFunctions.hideProgress("#kendoGridDashboard");
            ViewModel = undefined;
            OriginalViewModel = undefined;
            createNotification({
              theme: "success",
              positionClass: "nfc-bottom-right",
            })({
              title: "Success",
              message: "New month has been generated!",
            });
            $route.reload();
          }, utils.helperFunctions.error);
      } else {
        utils.helperFunctions.error();
      }
    };
  };
  app.controller("DashboardController", DashboardController);
})();

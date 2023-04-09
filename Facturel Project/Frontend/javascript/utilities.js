const utils = {
  constants: {
    backendUrl: "https://localhost:7268/api",
    tokenCookie: "Token",
    userIdCookie: "UserId",
    fieldsForWhichEditingIsNotAvailable: [
      "Type Name",
      "Counter's Unit of Measurement",
      "Counter's Serial Number",
    ],
  },
  helperFunctions: {
    error: function (err) {
      // ==================
      // stop showing loading on every element
      // ==================
      $(".k-loading-mask").each(function (index, element) {
        utils.helperFunctions.hideProgress($(element).parent());
      });
      console.error(err);
      createNotification({ theme: "error", positionClass: "nfc-bottom-right" })(
        {
          title: "Error",
          message:
            "Something went wrong! Consider changing your input or try again later!",
        }
      );
    },

    showProgressOnForm: function () {
      $("form").hide();
      $("h1").hide();
      utils.helperFunctions.showProgress(".content");
    },

    showProgress: function (element) {
      setTimeout(function () {
        kendo.ui.progress($(element), true);
      }, 0);
    },

    errorOnFormWithProgress: function (err) {
      utils.helperFunctions.error(err);
      $("form").show();
      $("h1").show();
    },

    hideProgressOnForm: function () {
      utils.helperFunctions.hideProgress(".content");
      $("form").show();
      $("h1").show();
    },

    hideProgress: function (element) {
      kendo.ui.progress($(element), false);
    },

    addAllDataToDataSource: function (kendoDataSource, arrayWithData) {
      for (const element of arrayWithData) {
        kendoDataSource.add(element);
      }
    },

    sanitizeViewModelDates: function () {
      for (const location of ViewModel.locations) {
        for (const month of location.months) {
          for (const invoice of month.invoices) {
            invoice.indexReading.dateOfReading =
              invoice.indexReading.dateOfReading.substring(0, 10);

            invoice.indexReading.dateOfReading = new Date(
              invoice.indexReading.dateOfReading
            );
            invoice.indexReading.dateOfReading = this.formatDateForKendoUI(invoice.indexReading.dateOfReading);
          }
        }
      }

      for (const location of OriginalViewModel.locations) {
        for (const month of location.months) {
          for (const invoice of month.invoices) {
            invoice.indexReading.dateOfReading =
              invoice.indexReading.dateOfReading.substring(0, 10);

            invoice.indexReading.dateOfReading = new Date(
              invoice.indexReading.dateOfReading
            );
            invoice.indexReading.dateOfReading = this.formatDateForKendoUI(invoice.indexReading.dateOfReading);
          }
        }
      }
    },

    formatViewModelDatesForBackend: function () {
      for (const location of ViewModel.locations) {
        for (const month of location.months) {
          for (const invoice of month.invoices) {
            invoice.indexReading.dateOfReading =
              invoice.indexReading.dateOfReading
                .replaceAll(". ", "-")
                .replace(".", "");
          }
        }
      }
    },

    formatDateForKendoUI: function(date) {
      return `${date.getFullYear()}. ${date.getMonth() < 10? '0'+(date.getMonth()+1): date.getMonth()}. ${date.getDate() < 10 ? '0' + date.getDate() :date.getDate()}.`;
    },

    deleteAllDataFromDataSource: function (kendoDataSource) {
      const length = kendoDataSource.data().length;

      for (let i = length - 1; i >= 0; i--) {
        const dataItem = kendoDataSource.at(i);
        kendoDataSource.remove(dataItem);
      }
    },
    getFieldNameFromViewModel: function (ColumnName) {
      switch (ColumnName) {
        case "Price":
          return "price";
        case "Index Readings's value":
          return "indexReading.indexReading";
        case "Index Readings's date":
          return "indexReading.dateOfReading";
        case "Paid":
          return "paid";
        default:
          return null;
      }
    },

    isUiDirty: function () {
      for (let i = 0; i < ViewModel.locations.length; i++) {
        const location = ViewModel.locations[i];
        const oldLocation = OriginalViewModel.locations[i];
        for (let j = 0; j < location.months.length; j++) {
          const month = location.months[j];
          const oldMonth = oldLocation.months[j];
          for (let k = 0; k < month.invoices.length; k++) {
            const invoice = month.invoices[k];
            const oldInvoice = oldMonth.invoices[k];

            if (
              invoice.price !== oldInvoice.price ||
              invoice.indexReading.indexReading !==
                oldInvoice.indexReading.indexReading ||
              invoice.indexReading.dateOfReading !==
                oldInvoice.indexReading.dateOfReading
            ) {
              return true;
            }
          }
        }
      }
      return false;
    },
    formatDateForDotNet: function(date){
      return `${date.getFullYear()}-${date.getMonth() < 10? '0'+(date.getMonth()+1): date.getMonth()}-${date.getDate() < 10 ? '0' + date.getDate() :date.getDate()}`;
    }
  },
};

// ====================
// Global Variables
// ====================
let ViewModel;
let OriginalViewModel;

(function () {
  const app = angular.module("invoices", ["ngRoute", "ngCookies"]);

  app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
      .when("/home", {
        templateUrl: "./views/home/home.html",
      })
      .when("/login", {
        templateUrl: "./views/login/login.html",
        controller: "LoginController",
      })
      .when("/register", {
        templateUrl: "./views/register/register.html",
        controller: "RegisterController",
      })
      .when("/dashboard", {
        templateUrl: "./views/dashboard/dashboard.html",
        controller: "DashboardController",
      })
      .when("/uom", {
        templateUrl: "./views/unitsOfMeasurement/uom.html",
        controller: "UnitsOfMeasurementController",
      })
      .when("/counterType", {
        templateUrl: "./views/counterType/ct.html",
        controller: "CounterTypeController",
      })
      .when("/invoiceType", {
        templateUrl: "./views/invoiceType/it.html",
        controller: "InvoiceTypeController",
      })
      .when("/lit", {
        templateUrl: "./views/locationInvoiceTypeRelation/lit.html",
        controller: "LocationInvoiceTypeRelationController",
      })
      .when("/counters", {
        templateUrl: "./views/counters/counters.html",
        controller: "CountersController",
      })
      .when("/account", {
        templateUrl: "./views/account/account.html",
        controller: "AccountController",
      })
      .otherwise({
        redirectTo: "/home",
      });
  });

  app.config(function ($httpProvider) {
    $httpProvider.interceptors.push(function ($q, $cookies) {
      return {
        request: function (config) {
          const cookie = $cookies.get(utils.constants.tokenCookie);
          if (cookie) {
            config.headers.Authorization = `Bearer ${cookie}`;
          }
          return config;
        },
      };
    });
  });
})();

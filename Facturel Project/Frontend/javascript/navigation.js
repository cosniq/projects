$(document).ready(function () {
  $("#door")
    .mouseenter(function () {
      $("#door").removeClass("fa-door-closed");
      $("#door").addClass("fa-door-open");
      $("#login").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#door").removeClass("fa-door-open");
      $("#door").addClass("fa-door-closed");
      $("#login").removeClass("navLabel");
    });

  $("#home")
    .mouseenter(function () {
      $("#homeDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#homeDiv").removeClass("navLabel");
    });

  $("#card")
    .mouseenter(function () {
      $("#register").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#register").removeClass("navLabel");
    });
  $("#dashboardIcon")
    .mouseenter(function () {
      $("#dashboardDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#dashboardDiv").removeClass("navLabel");
    });

  $("#logoutIcon")
    .mouseenter(function () {
      $("#logoutDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#logoutDiv").removeClass("navLabel");
    });

  $("#invoiceIcon")
    .mouseenter(function () {
      $("#invoiceDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#invoiceDiv").removeClass("navLabel");
    });

  $("#uomIcon")
    .mouseenter(function () {
      $("#uomDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#uomDiv").removeClass("navLabel");
    });

  $("#counterIcon")
    .mouseenter(function () {
      $("#counterDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#counterDiv").removeClass("navLabel");
    });

    $("#counterTypeIcon")
    .mouseenter(function () {
      $("#counterTypeDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#counterTypeDiv").removeClass("navLabel");
    });

    $("#litIcon")
    .mouseenter(function () {
      $("#litDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#litDiv").removeClass("navLabel");
    });

    $("#accountIcon")
    .mouseenter(function () {
      $("#accountDiv").addClass("navLabel");
    })
    .mouseleave(function () {
      $("#accountDiv").removeClass("navLabel");
    });

});

(function () {
  const app = angular.module("invoices");
  const NavigationController = function ($scope, $location, $cookies) {
    $scope.goTo = function (destination) {
      $location.path(destination);
    };

    utils.helperFunctions.showLoggedIn = function () {
      $scope.loggedIn = true;
    };

    utils.helperFunctions.hideLoggenIn = function () {
      $scope.loggedIn = false;
    };

    const initializeValue = function () {
      const tokenCookie = $cookies.get(utils.constants.tokenCookie);
      if (tokenCookie) {
        $scope.loggedIn = true;
      } else {
        $scope.loggedIn = false;
      }
    };
    initializeValue();

    $scope.logout = function (destination) {
      $cookies.remove(utils.constants.tokenCookie);
      $cookies.remove(utils.constants.userIdCookie);

      utils.helperFunctions.hideLoggenIn();

      $location.path(destination);
    };
  };
  app.controller("NavigationController", NavigationController);
})();



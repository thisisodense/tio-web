angular.module("umbraco").controller("Ubolt.OpeningHoursController", function ($scope, assetsService) {
    $scope.model.value = $scope.model.value || {};
    
    assetsService.loadCss("/app_plugins/Ubolt.OpeningHours/openinghours.css");
});
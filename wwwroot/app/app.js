'use strict';

// Declare app level module which depends on views, and components
angular.module('myApp', [
    'ngRoute',
    'restangular',
    'LocalEventService',
    'myApp.home',
   // 'myApp.version',
    'ngSanitize'
]).
    config(['$locationProvider', '$routeProvider', '$sceDelegateProvider',
        function ($locationProvider, $routeProvider, $sceDelegateProvider) {
            $locationProvider.hashPrefix('!');

            $routeProvider.otherwise({ redirectTo: '/home' });

            // Whitelist Google maps to allow embedding Google Maps iframes
            // See: http://stackoverflow.com/questions/20049261/sce-trustasresourceurl-globally/24841974#24841974
            $sceDelegateProvider.resourceUrlWhitelist(['self', 'https://www.google.com/maps/embed/**'])

        }]);

'use strict';

angular.module('LocalEventService', ['restangular'])
    .service('LocalEventService', function (Restangular) {

        this.getAllEvents = function () {
            return Restangular.one('api').getList('localeventsapi');
        };

    });

'use strict';

angular.module('myApp.home', ['ngRoute'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: '/app/home/home.html',
            controller: 'HomeController',
            controllerAs: 'homeCtrl',
        });
    }])
    .controller('HomeController', HomeController);

HomeController.$inject = ['$window', 'LocalEventService'];
function HomeController($window, LocalEventService) {
    var vm = this;

    vm.addToCalendar = addToCalendar;
    vm.localEvents = [];

    activate();

    function activate() {
        loadEvents();
    }

    // FIXME: This function assumes Central Time zone 
    // which is fine for Montgomery, AL website but it should be made generic for future reuse.
    function addToCalendar(localEvent) {
        var startDate = new Date(localEvent.StartDate);
        // Add 5 hours so time is correct for Central Time.
        startDate.setHours(startDate.getHours() + 5);
        var startDateFormatted = dateToGoogleCalendarFormat(startDate);
        // Add 1 hour to automatically create an end date.
        var endDate = startDate;
        endDate.setHours(endDate.getHours() + 1);
        var endDateFormatted = dateToGoogleCalendarFormat(endDate);

        // Note: Useful reference on Google Calendar links is here: http://stackoverflow.com/a/21653600/908677
        var url = 'https://www.google.com/calendar/render?action=TEMPLATE&text=' +
            encodeURIComponent(localEvent.Title) +
            '&dates=' + startDateFormatted + '/' + endDateFormatted +
            '&details=' + encodeURIComponent(localEvent.Description + ' For more details, see ' + localEvent.Url) +
            '&location=' + encodeURIComponent(localEvent.Address) + '&sf=true&output=xml' +
            '&ctz=America/Chicago';
        $window.open(url);
    }

    function dateToGoogleCalendarFormat(date) {
        // Conversion from http://stackoverflow.com/questions/10488831/link-to-add-to-google-calendar#comment43819710_21653600
        return date.toISOString().replace(/-|:|\.\d\d\d/g, "");
    }

    function loadEvents() {
        LocalEventService.getAllEvents().then(function (response) {
            vm.localEvents = _.sortBy(response, 'StartDate');
        });
    }
} 

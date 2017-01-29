﻿define(['durandal/app', 'knockout', 'services/stageService'], function (app, ko, stageService) {
    return {
        displayName: 'Guides',
        Stages: ko.observableArray([]),
        activate: function () {
            var self = this;
            return stageService.get().then(function(results) {
                self.Stages(results);
            });
        }
    };
});
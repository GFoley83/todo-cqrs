/// <reference path="../Lib/jasmine.js" />
/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../Lib/angular-mocks.js" />

/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../../../todocqrs.web/scripts/app/task.js" />

describe('HistoryListController tests', function() {
    var controller;
    var taskResourceSpy;
    var tasks;
    var scope;

    beforeEach(function() {

        module('task');

        tasks = [];

        taskResourceSpy = jasmine.createSpyObj('TaskResource', ['resolved']);
        taskResourceSpy.resolved.and.returnValue(tasks);

        inject(function($controller, $rootScope) {
            scope = $rootScope.$new();
            controller = $controller('HistoryListController', {
                TaskResource: taskResourceSpy,
                $scope: scope
            });
        });
    });

    it('should exist', function() {
        expect(controller).toBeDefined();
    });

    it('should get resolved tasks from resource', function() {
        expect(taskResourceSpy.resolved).toHaveBeenCalled();
        expect(controller.resolvedTasks).toBe(tasks);
    });

    it('should reload resolved tasks when taskResolved event is broadcasted', function() {
        var newTasks = [{ id: 1 }, { id: 2 }];
        taskResourceSpy.resolved.and.returnValue(newTasks);

        scope.$broadcast('task-resolved');

        expect(controller.resolvedTasks).toEqual(newTasks);
    });
});
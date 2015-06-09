/// <reference path="../Lib/jasmine.js" />
/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../Lib/angular-mocks.js" />

/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../../../todocqrs.web/scripts/app/task.js" />

describe('TaskListController tests', function() {
    var controller;
    var taskResourceSpy;
    var tasks;
    var newTask;
    var rootScopeSpy;

    beforeEach(function() {

        module('task');

        tasks = [];
        newTask = { id: 123, text: 'new task' };

        taskResourceSpy = jasmine.createSpyObj('TaskResource', ['query', 'save', 'get', 'delete', 'resolve']);
        taskResourceSpy.query.and.returnValue(tasks);
        taskResourceSpy.save.and.callFake(function(_, callback) {
            callback({ id: 123 });
        });
        taskResourceSpy.get.and.callFake(function(_, callback) {
            callback(newTask);
        });
        taskResourceSpy.delete.and.callFake(function(_, callback) {
            callback();
        });
        taskResourceSpy.resolve.and.callFake(function(_, callback) {
            callback();
        });

        inject(function($controller) {
            rootScopeSpy = jasmine.createSpyObj('$rootScope', ['$broadcast']);
            controller = $controller('TaskListController', {
                TaskResource: taskResourceSpy,
                $rootScope: rootScopeSpy
            });
        });

    });

    it('should exist', function() {
        expect(controller).toBeDefined();
    });

    it('newTaskText should be empty', function() {
        expect(controller.newTaskText).toEqual('');
    });

    it('should query tasks with resource', function() {
        expect(taskResourceSpy.query).toHaveBeenCalled();
        expect(controller.tasks).toBe(tasks);
    });

    describe('adding new task', function() {
        beforeEach(function() {
            controller.newTaskText = 'test';
            controller.add();
        });

        it('should save task with resource', function() {
            expect(taskResourceSpy.save).toHaveBeenCalledWith({ text: 'test' }, jasmine.any(Function));
        });

        it('should get saved task with given id', function() {
            expect(taskResourceSpy.get).toHaveBeenCalledWith({ id: 123 }, jasmine.any(Function));
        });

        it('should add new task to tasks collection', function() {
            expect(controller.tasks).toContain(newTask);
        });

        it('should reset newTask text', function() {
            expect(controller.newTaskText).toEqual('');
        });
    });

    describe('deleting task', function() {
        var taskToDelete;

        beforeEach(function() {
            controller.newTaskText = 'added';
            controller.add();

            taskToDelete = controller.tasks[0];
            controller.delete(taskToDelete);
        });

        it('should delete task with resource', function() {
            expect(taskResourceSpy.delete).toHaveBeenCalledWith({ id: 123 }, jasmine.any(Function));
        });

        it('should remove deleted task fron tasks collection', function() {
            expect(controller.tasks).not.toContain(taskToDelete);
        });
    });

    describe('resolving task', function() {
        var taskToResolve;
        beforeEach(function() {
            controller.newTaskText = 'added';
            controller.add();

            taskToResolve = controller.tasks[0];
            controller.resolve(taskToResolve);
        });

        it('should resolve task with resource', function() {
            expect(taskResourceSpy.resolve).toHaveBeenCalledWith({ id: 123 }, jasmine.any(Function));
        });

        it('should remove resolved task fron tasks collection', function() {
            expect(controller.tasks).not.toContain(taskToResolve);
        });

        it('should broadcast task-resolved event', function() {
            expect(rootScopeSpy.$broadcast).toHaveBeenCalledWith('task-resolved');
        });
    });
});
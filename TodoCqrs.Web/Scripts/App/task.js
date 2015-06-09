(function() {
    'use strict';

    var taskModule = angular.module('task', []);

    taskModule.controller('TaskListController', [
        'TaskResource', '$rootScope',
        function(taskResource, $rootScope) {
            var controller = this;

            this.tasks = taskResource.query();
            this.newTaskText = '';

            this.add = function() {
                taskResource.save({ text: controller.newTaskText }, function(newTask) {
                    taskResource.get({ id: newTask.id }, function(task) {
                        controller.tasks.push(task);
                        controller.newTaskText = '';
                    });
                });
            };

            function removeTask(task) {
                var index = controller.tasks.indexOf(task);
                controller.tasks.splice(index, 1);
            }

            this.delete = function(task) {
                taskResource.delete({ id: task.id }, function() {
                    removeTask(task);
                });
            };

            this.resolve = function(task) {
                taskResource.resolve({ id: task.id }, function() {
                    removeTask(task);
                    $rootScope.$broadcast('task-resolved');
                });
            };
        }
    ]);

    taskModule.factory('TaskResource', [
        '$resource',
        function($resource) {
            return $resource(
                '/api/task/:id',
                { id: '@id' },
                {
                    'resolve': { method: 'PUT', url: '/api/task/resolved/:id' },
                    'resolved': { method: 'GET', url: '/api/task/resolved', isArray: true }
                }
            );
        }
    ]);

    taskModule.controller('HistoryListController', [
        'TaskResource', '$scope',
        function(taskResource, $scope) {
            var controller = this;

            controller.resolvedTasks = taskResource.resolved();

            $scope.$on('task-resolved', function() {
                controller.resolvedTasks = taskResource.resolved();
            });
        }
    ]);
})();
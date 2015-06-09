/// <reference path="../Lib/jasmine.js" />
/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../../../todocqrs.web/scripts/lib/angular-resource.js" />
/// <reference path="../Lib/angular-mocks.js" />

/// <reference path="../../../todocqrs.web/scripts/lib/angular.js" />
/// <reference path="../../../todocqrs.web/scripts/app/task.js" />

describe('TaskResource tests', function() {
    var resource;
    var $httpBackend;

    beforeEach(function() {
        module('ngResource');
        module('task');

        inject(function(TaskResource, _$httpBackend_) {
            $httpBackend = _$httpBackend_;
            resource = TaskResource;
        });
    });

    it('should exist', function() {
        expect(resource).toBeDefined();
    });

    it('query should do correct get request', function() {
        $httpBackend.expectGET('/api/task').respond(200, []);

        resource.query();
        expect($httpBackend.flush).not.toThrow();
    });

    it('$save should do correct post request', function() {
        $httpBackend.expectPOST('/api/task', {text: 'test'}).respond({id: 132});
        resource.save({text: 'test'});

        expect($httpBackend.flush).not.toThrow();
    });

    it('get should do correct request', function() {
        $httpBackend.expectGET('/api/task/123').respond({id: 123});
        resource.get({id: 123});

        expect($httpBackend.flush).not.toThrow();
    });

    it('delete should do correct request', function() {
        $httpBackend.expectDELETE('/api/task/123').respond(200);
        resource.delete({id: 123});

        expect($httpBackend.flush).not.toThrow();
    });

    it('resolve should do correct request', function() {
        $httpBackend.expectPUT('/api/task/resolved/123', {id: 123}).respond(200);
        resource.resolve({id: 123});

        expect($httpBackend.flush).not.toThrow();
    });

    it('resolved should get resolved tasks', function() {
        $httpBackend.expectGET('/api/task/resolved').respond(200, [{id: 123}]);
        resource.resolved();

        expect($httpBackend.flush).not.toThrow();
    });
})
var App = App || {};


App.Api = function() {
    this.pendingCalls = {};
};

App.Api.prototype.get = function(path, userOpts) {
    var opts = Object.apply({
            requestId: '',
            params: {},
            success: () => {},
            error: () => {},
            complete: () => {}
        },
        userOpts);

    // If there is a pending request with the same id, cut it
    if (opts.requestId && this.pendingCalls[opts.requestId]) {
        this.pendingCalls[opts.requestId].abort();
        this.pendingCalls[opts.requestId] = undefined;
    }

    // Make the call and hold on to the request reference
    var xhr = $.ajax(`/api${path}`,
    {
        success: opts.success,
        error: opts.error,
        complete: opts.complete
    });

    // If given a request id, hold on to this request
    if (opts.requestId) {
        this.pendingCalls[opts.requestId] = xhr;
    }
};
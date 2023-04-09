class SubscriberFunction {
    constructor(observable, callback) {
        this.observable = observable;
        this.callback = callback;
    }
}
let Observables = {
    name: "Observables",
    subsriptions: [],
    subscribeToValueChange: function (observable, callback) {
        const subscriber = new SubscriberFunction(observable, callback);
        this.subsriptions.push(subscriber);
        return subscriber;
    },
    defineNewObservable: function (observable, value) {
        Object.defineProperty(this, observable, {
            set: function (newValue) {
                const oldValue = value;
                value = newValue;
                this.subsriptions
                    .filter((sub) => sub.observable === observable)
                    .forEach((sub) => sub.callback(newValue, oldValue));
            },
            get: function () {
                return value;
            },
            configurable: true,
        });
        return value;
    },
    deleteSubscription: function (subscriber) {
        this.subsriptions = this.subsriptions.filter((sub) => sub !== subscriber);
    },
    deleteAllSubscriptions: function (observable) {
        this.subsriptions = this.subsriptions.filter((sub) => sub.observable !== observable);
    },
    deleteObservable: function (observable) {
        if (this.hasOwnProperty(observable)) {
            const success = delete this[observable];
            if (!success) {
                return false;
            }
            this.deleteAllSubscriptions(observable);
            return true;
        }
        else {
            console.warn("The observable you are trying to remove does not exist!");
            return false;
        }
    },
};

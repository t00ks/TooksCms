define('viewmodels/admin/wedding', ['jquery', 'knockout', 'durandal/app', 'dataservices', 'moment', 'utils', 'editor', 'area', 'toastr', 'tk/modal'],
    function ($, ko, app, dataservices, moment, utils, editor, area, toastr, modal) {

        var _model = ko.observable();

        function RSVPModel(guest, food) {
            var self = this;

            this.name = guest.firstName() + ' ' + guest.lastName();

            this.guestId = guest.guestId;

            this.guestType = ko.observable(guest.guestType());

            this.attending = ko.observable(guest.attending());

            this.dietaryRequirements = ko.observable(guest.dietaryRequirements());

            this.accepted = ko.observable(guest.attending());
            this.declined = ko.observable(guest.attending() === false);

            this.accepted.subscribe(function (newValue) {
                if (newValue) {
                    self.attending(true);
                    self.declined(false);
                }
                $('.row.in-out .text-danger').hide();
            });

            this.declined.subscribe(function (newValue) {
                if (newValue) {
                    self.attending(false);
                    self.accepted(false);
                }
                $('.row.in-out .text-danger').hide();
            });

            this.foodChoice = {
                main: ko.observable(!food ? undefined : function () { switch (food.main) { case 0: return 'beef'; case 1: return 'bream'; case 2: return 'gnocchi' } }()),
                starter: ko.observable(!food ? undefined : function () { switch (food.starter) { case 0: return 'ham'; case 1: return 'hal' } }()),
                dessert: ko.observable(!food ? undefined : function () { switch (food.dessert) { case 0: return 'eton'; case 1: return 'tart'; case 2: return 'brulee' } }()),
                isVeggie: ko.observable(!food ? false : food.isVeggie)
            };

            this.rsvpDate = ko.observable(guest.rsvpDate);

            self.foodChoice.starter.subscribe(function () { $('.meal-choice.starter .text-danger').hide(); });
            self.foodChoice.main.subscribe(function () { $('.meal-choice.main .text-danger').hide(); });
            self.foodChoice.dessert.subscribe(function () { $('.meal-choice.dessert .text-danger').hide(); });

            this.submit = function () {
                var valid = true;
                if (self.declined() === false && self.accepted() === null) {
                    $('.row.in-out .text-danger').show();
                    valid = false;
                } else {
                    $('.row.in-out .text-danger').hide();
                }

                if (self.guestType() === 1) {
                    if (self.foodChoice.starter() === undefined) {
                        valid = false;
                        $('.meal-choice.starter .text-danger').show();
                    } else {
                        $('.meal-choice.starter .text-danger').hide();
                    }

                    if (self.foodChoice.main() === undefined) {
                        $('.meal-choice.main .text-danger').show();
                        valid = false;
                    } else {
                        $('.meal-choice.main .text-danger').hide();
                    }

                    if (self.foodChoice.dessert() === undefined) {
                        $('.meal-choice.dessert .text-danger').show();
                        valid = false;
                    } else {
                        $('.meal-choice.dessert .text-danger').hide();
                    }
                }

                if (valid) {
                    dataservices.wedding.rsvp({
                        guestId: self.guestId,
                        foodChoice: {
                            guestId: self.guestId,
                            starter: self.foodChoice.starter(),
                            main: self.foodChoice.main(),
                            dessert: self.foodChoice.dessert(),
                            isVeggie: self.foodChoice.isVeggie()
                        },
                        attending: self.accepted(),
                        dietaryRequirements: self.dietaryRequirements()
                    }).done(function (data) {
                        modal.close();
                        self.rsvpDate(new Date());
                    })
                }
            }

            this.close = function () {
                modal.close();
            }
        }

        function GuestModel(guest, parent) {
            var self = this,
                isNew = !guest;

            this.guestId = guest ? guest.guestId : -1;
            this.firstName = ko.observable(guest ? guest.firstName : null);
            this.lastName = ko.observable(guest ? guest.lastName : null);
            this.address = ko.observable(guest ? guest.address : null);

            this.attending = ko.observable(guest ? guest.attending : null);
            this.code = ko.observable(guest ? guest.code : null);

            this.guestGroupId = ko.observable(guest ? guest.guestGroupId : null);
            this.guestSide = ko.observable(guest ? guest.guestSide : null);
            this.guestType = ko.observable(guest ? guest.guestType : null);
            this.rsvpDate = ko.observable(guest ? guest.rsvpDate : null);

            this.invitationSent = ko.observable(guest ? guest.invitationSent : false);
            this.dietaryRequirements = ko.observable(guest ? guest.dietaryRequirements : null);

            this.editing = ko.observable(!guest);
            this.parent = parent;

            this.formattedAddress = ko.computed(function () {
                if (typeof (self.address()) === 'string') {
                    return self.address().replace(/,/g, ',<br />');
                }
                return '';
            });

            this.name = ko.computed(function () {
                return self.firstName() + " " + self.lastName();
            });

            this.type = ko.computed(function () {
                return self.guestType() === 1 ? 'Day' : 'Evening';
            });

            this.inGroup = ko.computed({
                read: function () {
                    return parent.selectedGroup() == self.guestGroupId();
                },
                write: function () {
                    self.guestGroupId(parent.selectedGroup());
                }
            });

            this.side = ko.computed(function () {
                switch (self.guestSide()) {
                    case 1:
                        return 'Both';
                    case 2:
                        return 'Tims';
                    case 3:
                        return 'Jens';
                }
            });

            this.invitation = ko.computed(function () {
                if (self.invitationSent()) {
                    return 'Yes';
                }
                return 'No';
            });

            this.isAttending = function (type) {
                if (self.attending() === false && type === 'times') {
                    return {
                        'style': 'color:red'
                    };
                } else if (self.attending() && type === 'check') {
                    return {
                        'style': 'color:#00ff00'
                    };
                }
            };


            this.getFoodInfo = function (food) {
                var nochoice = 'Please Choose Me!',
                    foodChoice = parent.food()[self.guestId];

                if (!foodChoice) {
                    return "No Food Found";
                }

                if (food === 'e') {
                    return foodChoice.isVeggie ? "Veggie Option" : "";
                }

                switch (food) {
                    case 's': {
                        switch (foodChoice.starter) {
                            case 0:
                                return 'Ham Hock';
                            case 1:
                                return 'Hallumi';
                        }
                    }
                    case 'm': {
                        switch (foodChoice.main) {
                            case 0:
                                return 'Blade of Beef';
                            case 1:
                                return 'Sea Bream';
                            case 2:
                                return 'Potato Gnocchi';
                        }
                    }
                    case 'd': {
                        switch (foodChoice.dessert) {
                            case 0:
                                return 'Eton Mess';
                            case 1:
                                return 'Chocolate Tart';
                            case 2:
                                return 'Crème Brûlée';
                        }
                    }
                }

                return nochoice;
            }

            this.edit = function () {
                self.editing(true);
            }

            this.rsvp = function () {
                parent.selectedRsvpGuest(new RSVPModel(this, parent.food()[self.guestId]));

                modal.show({
                    type: 'inline',
                    src: '#rsvp-form'
                });
            }

            this.save = function () {

                var guestJs = {
                    guestId: self.guestId,
                    firstName: self.firstName(),
                    lastName: self.lastName(),
                    address: self.address(),
                    attending: self.attending(),
                    code: self.code(),
                    guestGroupId: self.guestGroupId(),
                    guestSide: self.guestSide(),
                    guestType: self.guestType(),
                    invitationSent: self.invitationSent()
                };

                dataservices.wedding.saveGuest([guestJs]).done(function (data) {
                    if (isNew) {
                        self.guestId = data.guestId;
                        isNew = false;
                        parent.guests.push(self);
                        parent.newGuest(new GuestModel(undefined, parent));
                    }
                    self.editing(false);
                });
            }
        }

        function GroupModel(group, parent) {
            var self = this,
                isNew = !group;

            this.guestGroupId = group ? group.guestGroupId : -1;
            this.name = ko.observable(group ? group.name : null);
            this.message = ko.observable(group ? group.message : null);
        }

        function ViewModel(guests, groups, food) {
            var self = this;

            this.guests = ko.observableArray();
            this.groups = ko.observableArray();
            this.food = ko.observable(food);

            this.selectedGroup = ko.observable();
            this.selectedGroup.subscribe(function (newValue) {
                console.log(newValue);
            });

            this.selectedRsvpGuest = ko.observable();

            this.newGuest = ko.observable(new GuestModel(undefined, self));

            guests.forEach(function (g) {
                self.guests.push(new GuestModel(g, self));
            });

            groups.forEach(function (g) {
                self.groups.push(new GroupModel(g, self));
            });

            this.guestTypes = [{ name: 'Day', value: 1 }, { name: 'Evening', value: 2 }]
            this.guestSides = [{ name: 'Both', value: 1 }, { name: 'Tims', value: 2 }, { name: 'Jens', value: 3 }]
            this.invitationSentOptions = [{ name: 'Invitation Sent', value: 1 }, { name: 'Invitation Not Sent', value: 2 }]

            this.filterType = ko.observable();
            this.filterSide = ko.observable();
            this.filterInvitation = ko.observable();
            this.filterName = ko.observable();


            this.filteredGuests = ko.computed(function () {

                return self.guests().filter(function (g) {

                    var s = false,
                        t = false,
                        i = false,
                        n = false;

                    if (!self.filterType() || self.filterType() == g.guestType()) {
                        t = true;
                    }

                    if (!self.filterSide() || self.filterSide() == g.guestSide()) {
                        s = true;
                    }

                    if (!self.filterInvitation() || (self.filterInvitation() == 1 && g.invitationSent()) || (self.filterInvitation() == 2 && !g.invitationSent())) {
                        i = true;
                    }

                    if (self.filterName()) {
                        var reg = new RegExp(self.filterName().toLowerCase() + "([a-zA-Z]*).*");
                        if (reg.test(g.firstName().toLowerCase()) || reg.test(g.lastName().toLowerCase())) {
                            n = true;
                        }
                    } else {
                        n = true;
                    }

                    return s && t && i && n;

                });

            });

            this.addGroup = function () {
                modal.show({
                    type: 'inline',
                    src: '#group-popup'
                });
            }

            this.getTotal = function (side, type) {

                var total = 0;
                self.guests().forEach(function (g) {

                    var s = false,
                        t = false;

                    if ((g.guestSide() === 1 && side === 'Both') || (g.guestSide() === 2 && side === 'Tim') || (g.guestSide() === 3 && side === 'Jen') || side === 'Total') {
                        s = true;
                    }

                    if ((g.guestType() === 1 && type === 'd') || (g.guestType() === 2 && type === 'e') || (type === 'b')) {
                        t = true;
                    }

                    if (t && s) {
                        total += 1;
                    }

                });

                return total;
            }

            this.getStats = function (attendenceType, countType) {
                var total = 0
                self.guests().forEach(function (g) {
                    var count = false;

                    if ((g.guestType() === 1 && attendenceType === 'd') || (g.guestType() === 2 && attendenceType === 'e') || attendenceType === 't' ) {
                        if (g.attending()) {
                            count = true;
                        }
                        if (g.attending() === null && countType === 'p') {
                            count = true;
                        }
                    }

                    if (count) {
                        total += 1;
                    }
                });

                return total;
            }
        }

        return {
            activate: function (id) {

                var calls = [],
                    guests, groups, food;

                calls.push(dataservices.wedding.loadGuests().done(function (data) {
                    guests = data;
                }));

                calls.push(dataservices.wedding.loadGroups().done(function (data) {
                    groups = data;
                }));

                calls.push(dataservices.wedding.loadFood().done(function (data) {
                    food = data;
                }));

                $.when.apply($, calls).done(function () {
                    var vm = new ViewModel(guests, groups, food);
                    _model(vm);
                })

                app.isAdmin(true);
            },
            compositionComplete: function () {

            },
            model: _model
        };
    });
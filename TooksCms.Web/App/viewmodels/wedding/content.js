define('viewmodels/wedding/content', ['durandal/app', 'dataservices', 'area', 'stats', 'tk/menu', 'appcache', 'plugins/router', 'tk/modal'], function (app, dataservices, area, stats, menu, cache, router, modal) {

    var _map,
        _marker,
        _carriageHall = new google.maps.LatLng(52.885024, -1.08073),
        _marker,
        _hotels = ko.observable(),
        _guests = ko.observableArray(),
        _selectedGuest = ko.observable(),
        _group;

    function GuestVM(guest, food) {
        var self = this;

        this.name = guest.firstName + ' ' + guest.lastName;

        this.guestId = guest.guestId;

        this.guestType = ko.observable(guest.guestType);

        this.attending = ko.observable(guest.attending);

        this.dietaryRequirements = ko.observable(guest.dietaryRequirements);

        this.accepted = ko.observable(guest.attending);
        this.declined = ko.observable(guest.attending === false);

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

        this.getFoodInfo = function (food) {
            var nochoice = 'Please Choose Me!';
            switch (food) {
                case 's': {
                    switch (self.foodChoice.starter()) {
                        case 'ham':
                            return 'Ham Hock';
                        case 'hal':
                            return 'Hallumi';
                    }
                }
                case 'm': {
                    switch (self.foodChoice.main()) {
                        case 'beef':
                            return 'Blade of Beef';
                        case 'bream':
                            return 'Sea Bream';
                        case 'gnocchi':
                            return 'Potato Gnocchi';
                    }
                }
                case 'd': {
                    switch (self.foodChoice.dessert()) {
                        case 'eton':
                            return 'Eton Mess';
                        case 'tart':
                            return 'Chocolate Tart';
                        case 'brulee':
                            return 'Crème Brûlée';
                    }
                }
            }

            return nochoice;
        }

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

    function _initialize() {
        var mapOptions = {
            center: _carriageHall,
            zoom: 15,
            panControl: false,
            zoomControl: false,
            scaleControl: true

        };
        _map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

        _initMarker();
    }

    function _initMarker() {
        var content = '<div id="content">' +
          '<div id="siteNotice">' +
          '</div>' +
          '<h3 id="firstHeading" class="firstHeading">The Carriage Hall</h3>' +
          '<div id="bodyContent">' +
          '<p>Station Road,<br />Plumtree,<br />Nottingham,<br />NG12 5NA</p>' +
          '</div>' +
          '</div>';

        var infowindow = new google.maps.InfoWindow({
            content: content
        });

        _marker = new google.maps.Marker({
            position: _carriageHall,
            map: _map,
            title: "The Carriage Hall",
            animation: google.maps.Animation.DROP
        });

        google.maps.event.addListener(_marker, 'click', function () {
            infowindow.open(_map, _marker);
        })
    }

    function _toggleBounce() {

        if (_marker.getAnimation() != null) {
            _marker.setAnimation(null);
        } else {
            _marker.setAnimation(google.maps.Animation.BOUNCE);
        }
    }

    function _showOnMap(hotel) {

        $('.map-host').animate({ top: $('#location').height() + $('#hotel_' + hotel.hotelId).position().top + 65 + 'px' }, 500)

        _map.panTo(new google.maps.LatLng(hotel.latitude, hotel.longitude))
    }

    function _doSlideAnimation() {

    }

    return {
        activate: function () {
            if (cache.session('wedding_guests') != null) {
                var guests = cache.session('wedding_guests').guests,
                    foods = cache.session('wedding_guests').food || [];
                _group = cache.session('wedding_guests').group;

                if (guests && guests.length) {

                    dataservices.wedding.loadHotels().done(function (data) {
                        _hotels({
                            showOnMap: _showOnMap,
                            hotel: ko.observableArray(data)
                        });
                    });

                    guests.forEach(function (g) {
                        var food = foods.filter(function (f) { return f.guestId == g.guestId });
                        food = food.length > 0 ? food[0] : undefined;
                        _guests.push(new GuestVM(g, food));
                    });

                    cache.session('wedding_guests', null);

                } else {
                    router.navigate('/wedding');
                }
            } else {
                router.navigate('/wedding');
            }
        },
        deactivate: function () {

        },
        compositionComplete: function () {
            setTimeout(_initialize, 50);
        },
        hotels: _hotels,
        guests: _guests,
        getGroupName: function () {
            if (_group) {
                return _group.name;
            } else {
                var result = '';
                _guests().forEach(function (g) {
                    result += g.name + ', ';
                });

                return result;
            }
        },
        selectedGuest: _selectedGuest,
        isDay: ko.computed(function () {
            return _guests().some(function (g) {
                return g.guestType() === 1;
            });
        }),
        openRsvp: function (guest) {
            _selectedGuest(guest);
            modal.show({
                src: '#rsvp-form'
            });
        },
        centreCarriageHall: function () {
            $('.map-host').animate({ top: '20px' }, 500)
            _map.panTo(_carriageHall)
        }
    };
});
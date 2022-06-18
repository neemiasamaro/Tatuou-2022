(function ($) {
    "use strict";

    var THE_TATTOOIST = window.THE_TATTOOIST || {};

    ///*-------------------------------------------------------------------*/
    ///*      Toggle
    ///*-------------------------------------------------------------------*/

    //THE_TATTOOIST.toggle = function () {

    //    $('.open .content-toggle').show();
    //    $('.title-toggle').on('click', function (e) {
    //        e.preventDefault();

    //        var $that = $(this),
    //            $toggle = $that.parent(),
    //            $contentToggle = $that.next(),
    //            $accordion = $that.parents('.accordion');

    //        if ($accordion.length > 0) {
    //            $accordion.find('.content-toggle').slideUp('normal', function () {
    //                $(this).parent().removeClass('open');
    //            });
    //            if ($that.next().is(':hidden')) {
    //                $contentToggle.slideDown('normal', function () {
    //                    $toggle.addClass('open');
    //                });
    //            }
    //        } else {
    //            $contentToggle.slideToggle('normal', function () {
    //                $toggle.toggleClass('open');
    //            });
    //        }
    //    });

    //},

    //    /*-------------------------------------------------------------------*/
    //    /*      Scroll to Section (One Page Version)
    //    /*-------------------------------------------------------------------*/

    //    THE_TATTOOIST.scrollToSection = function () {

    //        $('.one-page #nav-menu a[href^="#"]').on('click', function (e) {
    //            e.preventDefault();

    //            var target = this.hash,
    //                $section = $(target);

    //            $(this).parent().addClass('selected');
    //            $('html, body').stop().animate({
    //                scrollTop: $section.offset().top - 79
    //            }, 900, 'swing', function () {
    //                window.location.hash = target;
    //            });
    //            $('body').removeClass('open');
    //            $('#nav-menu').find('li').removeClass('show');

    //        });

    //    },

    //    /*-------------------------------------------------------------------*/
    //    /*      Highlight Navigation Link When Scrolling (One Page Version)
    //    /*-------------------------------------------------------------------*/

    //    THE_TATTOOIST.scrollHighlight = function () {

    //        var scrollPosition = $(window).scrollTop();

    //        if ($('body').hasClass('one-page')) {

    //            if (scrollPosition >= 200) {

    //                $('.section').each(function () {

    //                    var $link = $('#nav-menu a[href="#' + $(this).attr('id') + '"');
    //                    if ($link.length && $(this).position().top <= scrollPosition + 80) {
    //                        $('#nav-menu li').removeClass('selected');
    //                        $link.parent().addClass('selected');
    //                    }
    //                });

    //            } else {

    //                $('#nav-menu li').removeClass('selected');

    //            }
    //        }

    //    },

    //    /*-------------------------------------------------------------------*/
    //    /*      Mobile Menu
    //    /*-------------------------------------------------------------------*/

    //    THE_TATTOOIST.mobileMenu = {

    //        init: function () {

    //            this.toggleMenu();
    //            this.addClassParent();
    //            this.addRemoveClasses();

    //        },

    //        // toggle mobile menu
    //        toggleMenu: function () {

    //            var self = this,
    //                $body = $('body');

    //            $('#nav-toggle').click(function (e) {
    //                e.preventDefault();

    //                if ($body.hasClass('open')) {
    //                    $body.removeClass('open');
    //                    $('#nav-menu').find('li').removeClass('show');
    //                } else {
    //                    $body.addClass('open');
    //                    self.showSubmenu();
    //                }

    //            });

    //        },

    //        // add 'parent' class if a list item contains another list
    //        addClassParent: function () {

    //            $('#nav-menu').find('li > ul').each(function () {
    //                $(this).parent().addClass('parent');
    //            });

    //        },

    //        // add/remove classes to a certain window width
    //        addRemoveClasses: function () {

    //            var $nav = $('#nav-menu');

    //            if ($(window).width() < 992) {
    //                $nav.addClass('mobile');
    //            } else {
    //                $('body').removeClass('open');
    //                $nav.removeClass('mobile').find('li').removeClass('show');
    //            }

    //        },

    //        // show sub menu
    //        showSubmenu: function () {

    //            $('#nav-menu').find('a').each(function () {

    //                var $that = $(this);

    //                if ($that.next('ul').length) {
    //                    $that.one('click', function (e) {
    //                        e.preventDefault();
    //                        $(this).parent().addClass('show');
    //                    });
    //                }

    //            });

    //        }

    //    },

    /*-------------------------------------------------------------------*/
    /*      Sticky Menu
    /*-------------------------------------------------------------------*/

    THE_TATTOOIST.stickyMenu = function () {

        if ($(window).scrollTop() > 50) {
            $('nav').addClass('bg-dark');
        } else {
            $('nav').removeClass('bg-dark');
        }

    },

        /*-------------------------------------------------------------------*/
        /*      Show/Hide Bottom Contacts Bar
        /*-------------------------------------------------------------------*/

        THE_TATTOOIST.contactsBar = function () {

            if ($(window).scrollTop() + $(window).height() > $('footer').offset().top) {
                $('#contacts-bar').fadeOut('fast');
            } else {
                $('#contacts-bar').fadeIn('fast');
            }

        },

        /*-------------------------------------------------------------------*/
        /*      Custom Backgrounds
        /*-------------------------------------------------------------------*/

        THE_TATTOOIST.backgrounds = function () {

            $.each(config.backgrouns, function (key, value) {

                var $el = $(key),
                    $overlay = $('<div class="bg-overlay"></div>');

                if (value.img != null) {
                    $el.addClass('bg').css('background-image', 'url(' + value.img + ')').prepend($overlay);
                }

                if (value.overlay != null && !value.disableOverlay) {
                    $el.find('.bg-overlay').remove();
                }

                if (value.overlayOpacity != null) {
                    $el.find('.bg-overlay').css('opacity', value.overlayOpacity);
                }

                if (value.overlayColor != null) {
                    $el.find('.bg-overlay').css('background-color', value.overlayColor);
                }

                if (value.pattern != null && value.pattern) {
                    $el.addClass('pattern');
                }

                if (value.position != null) {
                    $el.css('background-position', value.position);
                }

                if (value.bgCover != null) {
                    $el.css('background-size', value.bgCover);
                }

                if (value.parallax != null && value.parallax) {
                    $el.addClass('plx');
                }

            });

        },

        /*-------------------------------------------------------------------*/
        /*      Parallax
        /*-------------------------------------------------------------------*/

        THE_TATTOOIST.parallax = function () {

            $('.plx').each(function () {
                $(this).parallax('50%', 0.5);
            });

        },

        /*-------------------------------------------------------------------*/
        /*      Initialize all functions
        /*-------------------------------------------------------------------*/

        $(document).ready(function () {

            THE_TATTOOIST.selectReplacer();
            THE_TATTOOIST.toggle();
            THE_TATTOOIST.tabs();
            THE_TATTOOIST.portfolio.init();
            THE_TATTOOIST.scrollToSection();
            THE_TATTOOIST.mobileMenu.init();
            THE_TATTOOIST.forms();
            THE_TATTOOIST.backgrounds();
            THE_TATTOOIST.parallax();

        });

    // window load scripts
    $(window).load(function () {

        THE_TATTOOIST.pageLoader();
        THE_TATTOOIST.flexslider();

    });

    // window resize scripts
    $(window).resize(function () {

        THE_TATTOOIST.portfolio.layout();
        THE_TATTOOIST.mobileMenu.addRemoveClasses();

    });

    // window scroll scripts
    $(window).scroll(function () {

        THE_TATTOOIST.stickyMenu();
        THE_TATTOOIST.scrollHighlight();
        THE_TATTOOIST.contactsBar();

    });

})(jQuery);
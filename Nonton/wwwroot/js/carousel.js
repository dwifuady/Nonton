export function initSlick() {
    if (document.getElementsByClassName('slick').length > 0) {
        $('.slick').not('.slick-initialized').slick({
            lazyLoad: 'ondemand',
            slidesToShow: 3,
            slidesToScroll: 3,
            infinite: true,
            //nextArrow: `<div class="absolute grid place-items-center right-0 z-10">
            //                <button type="button" class= "w-8 h-8 rounded-full bg-gray-700">&#8250;</button>
            //            </div>`,
            //prevArrow: '<button type="button" class="w-8 h-8 rounded-full bg-gray-700">&#8249;</button>'
        });
    }
}
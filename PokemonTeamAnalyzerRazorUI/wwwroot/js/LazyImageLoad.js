//Call ConfigureLazyLoading after DOMContent is loaded

let lazyImages = null;

function GetAllLazyImages() {

    lazyImages = [].slice.call(document.querySelectorAll("img.lazy-load"));

}

function ConfigureLazyLoading() {

    console.log("Configuring lazy loading...");
    GetAllLazyImages();

    if ("IntersectionObserver" in window) {

        let lazyImageObserver = new IntersectionObserver(function (entries, observer) {

            entries.forEach((entry) => {

                if (entry.isIntersecting) {
                    let lazyImage = entry.target;

                    lazyImage.src = lazyImage.dataset.src;
                    lazyImage.classList.remove("lazy-load");

                    lazyImageObserver.unobserve(lazyImage);

                }

            });

        });


        lazyImages.forEach((lazyImage) => {

            lazyImageObserver.observe(lazyImage);

        });


    } else {

        console.log("Browser does not support Intersection Observer");

    }

    console.log("done");

}
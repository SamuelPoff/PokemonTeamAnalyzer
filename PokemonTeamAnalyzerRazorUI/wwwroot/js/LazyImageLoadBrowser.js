
//Check for support of the loading attribute
function ConfigureLazyLoading() {
    if ("loading" in HTMLImageElement.prototype) {

        console.log("Configuring lazy loading");

        let lazyImages = document.querySelectorAll(".lazy-load img");

        console.log(lazyImages);

        lazyImages.forEach((image) => {

            if (!image.complete) {

                image.parentNode.classList.add("lazy-load-waiting");
                image.addEventListener("load", OnLazyImageLoad, false);

            }

        });

    } else {

        console.log("Loading not supported by this browser");

    }
}

function OnLazyImageLoad(e) {

    let parent = e.currentTarget.parentNode;

    parent.classList.remove("lazy-load-waiting");
    console.log("image loaded");

}

function ConfigureCustomSelects() {

    console.log("Starting custom select up")

    let allCustomSelects = document.getElementsByClassName("select-custom");

    for (const customSelect of allCustomSelects) {

        let selectElement = customSelect.getElementsByTagName("select")[0];

        let customSelectedItem = document.createElement("DIV");
        customSelectedItem.setAttribute("class", "select-selected");
        customSelectedItem.innerHTML = selectElement.options[selectElement.selectedIndex].innerHTML;

        customSelect.appendChild(customSelectedItem);

        //Create div to hold the custom-select items(/options)
        let selectItemsContainer = document.createElement("DIV");
        selectItemsContainer.setAttribute("class", "select-items select-hide");

        //Set currently selected item as customSelectedItem

        //For every element in the select tag, create a new custom DIV to act as the item
        for (const option of selectElement.options) {

            let customOption = document.createElement("DIV");
            customOption.innerHTML = option.innerHTML;

            customOption.addEventListener("click", (evt) => {

                //If an item is clicked, update the html select box and selectd item
                let selectElement = evt.currentTarget.parentNode.parentNode.getElementsByTagName("select")[0];
                let customSelectedItem = evt.currentTarget.parentNode.previousSibling;

                for (let i = 0; i < selectElement.length; i++) {

                    if (selectElement.options[i].innerHTML == evt.currentTarget.innerHTML) {

                        selectElement.selectedIndex = i;

                        customSelectedItem.innerHTML = evt.currentTarget.innerHTML;
                        let sameAsSelected = evt.currentTarget.getElementsByClassName("same-as-selected");

                        //Remove previous "same-as-selected" option
                        for (let j = 0; j < sameAsSelected.length; j++) {
                            sameAsSelected[j].removeAttribute("class");
                        }

                        //assign "same as selected" to this custom select option
                        evt.currentTarget.setAttribute("class", "same-as-selected");
                        break;
                    }

                }

                selectElement.dispatchEvent(new Event('input', { bubbles: true }))
                customSelectedItem.click();

            });

            selectItemsContainer.appendChild(customOption);

        }


        customSelect.appendChild(selectItemsContainer);
        customSelectedItem.addEventListener("click", (evt) => {


            evt.stopPropagation();

            closeSelects(evt.currentTarget);

            evt.currentTarget.nextSibling.classList.toggle("select-hide");
            evt.currentTarget.classList.toggle("select-arrow-active");


        });


    }


    function closeSelects(element) {

        let arr = [];

        let selectItems = document.getElementsByClassName("select-items");
        let selectedItems = document.getElementsByClassName("select-selected");

        for (let i = 0; i < selectedItems.length; i++) {

            if (element == selectedItems[i]) {
                arr.push(i);
            }
            else {
                selectedItems[i].classList.remove("select-arrow-active");
            }

        }

        for (let i = 0; i < selectItems.length; i++) {

            if (arr.indexOf(i)) {
                selectItems[i].classList.add("select-hide");
            }

        }

    }

    document.addEventListener("click", closeSelects);

}
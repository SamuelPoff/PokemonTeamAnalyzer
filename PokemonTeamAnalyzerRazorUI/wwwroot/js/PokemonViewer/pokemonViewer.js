let view_results = document.getElementById("view-results-div");

let nameFilter = document.getElementById('name-filter');
let typeFilter = document.getElementById('type-filter-1');
let typeFilter2 = document.getElementById('type-filter-2');
let abilityFilter = document.getElementById('ability-name-filter');

let searchTimerHandle = null;
let searchDelay = 500;

nameFilter.addEventListener("input", OnFilterInputChanged);
abilityFilter.addEventListener("input", OnFilterInputChanged);
typeFilter.addEventListener("input", OnFilterInputChanged);
typeFilter2.addEventListener("input", OnFilterInputChanged);

/*Saving/Restoring filter data
 ---------------------------------*/
function SaveFilterData() {
    if (typeof Storage !== "undefined") {

        localStorage.nameFilter = nameFilter.value;
        localStorage.abilityFilter = abilityFilter.value;
        localStorage.typeFilter = typeFilter.value;
        localStorage.typeFilter2 = typeFilter2.value;

    }
}
function RestoreFilterData() {

    console.log("restoring filter data");

    if (typeof Storage !== "undefined") {

        if (localStorage.nameFilter != null) {
            nameFilter.value = localStorage.nameFilter;
        }
        if (localStorage.abilityFilter != null) {
            abilityFilter.value = localStorage.abilityFilter;
        }
        if (localStorage.typeFilter != null) {
            typeFilter.value = localStorage.typeFilter;
        }
        if (localStorage.typeFilter2 != null) {
            typeFilter2.value = localStorage.typeFilter2;
        }

    }

}

function OnFilterInputChanged() {

    if (searchTimerHandle != null) {
        clearTimeout(searchTimerHandle);
    }

    searchTimerHandle = setTimeout(updateResults, searchDelay);

}

function updateResults() {

    $.ajax({

        dataType: 'html',
        url: '?handler=CallPokemonViewerSearchResultsViewComponent',
        type: 'GET',
        data: {
            nameSearchString: nameFilter.value,
            typeFilter: typeFilter.value,
            typeFilter2: typeFilter2.value,
            abilityNameSearchString: abilityFilter.value
        },
        success: function (data) {
            view_results.innerHTML = data;
            ConfigureLazyLoading();
        },
        error: function (e) {
            alert("Error: " + e);
        }

    });

    searchTimerHandle = null;

}

//Save filter data on page unload
window.addEventListener("beforeunload", SaveFilterData);

//Reload same results when returning to page, load all results by default on first visit
document.addEventListener("DOMContentLoaded", OnPageLoad);

function OnPageLoad() {
    RestoreFilterData();

    ConfigureCustomSelects();

    updateResults();
}

let analyzeButton = document.getElementById("analyze-button");
let results = document.getElementById("results");

let pokemonSearchBox = document.getElementById("pokemon-search-box");
let liveSearchResultsContainer = document.getElementById("live-search");

let selectedPokemonContainer = document.getElementById("selected-pokemon");

let addPokemonButton = document.getElementById("add-pokemon-button");
let totalSelectedPokemon = 0;

function GetPokemonName() {

    return pokemonSearchBox.value;

}

function GetSelectedPokemonNames() {

    let pkmnNames = [];
    let selectedPokemonTiles = document.getElementsByClassName("TypeAnalysisPokemonTile");

    for (const selectedPokemon of selectedPokemonTiles) {

        pkmnNames.push(selectedPokemon.dataset.pokemonname);

    }

    return pkmnNames;

}

//Perform AJAX call to get the Type Analysis. Also updates the results section with the results
function GetTypeAnalysisResultsAndUpdatePage() {

    let pkmnNames = GetSelectedPokemonNames();

    $.ajax({
        dataType: 'html',
        url: '?handler=CallTypeAnalysisViewComponent',
        type: 'GET',
        data: {
            pokemonNames: JSON.stringify(pkmnNames)
        },
        success: function (data) {

            results.innerHTML = data;

        },
        error: function (e) {
            console.log("failure")
            alert("Error: " + e);
        }

    });

}

function OnLiveSearchOptionClick(evt) {

    pokemonSearchBox.value = "";
    liveSearchResultsContainer.innerHTML = "";

    AddTypeAnalysisPokemon(evt.currentTarget.dataset.name);

}

//Performs AJAX request to add the TypeAnalysisPokemonSelection view component to the page
function AddTypeAnalysisPokemon(pkmnName) {

    $.ajax({
        dataType: 'html',
        url: '?handler=CallTypeAnalysisPokemonSelection',
        type: 'GET',
        data: {
            pokemonName: pkmnName
        },
        success: function (data) {

            if (data.replace(/\s/g, "")) {
                selectedPokemonContainer.innerHTML += data;
                ConfigureTypeAnalysisTileRemoveButtons();
            } else {

                alert("Pokemon name was either incorrectly spelled or does not exist.");

            }



        },
        error: function (e) {
            console.log("failure")
            alert("Error: " + e);
        }

    });

}


function UpdateLiveSearch() {

    $.ajax({
        dataType: 'html',
        url: '?handler=CallPokemonSearchViewComponent',
        type: 'GET',
        data: {
            searchString: pokemonSearchBox.value
        },
        success: function (data) {
            console.log("success");
            liveSearchResultsContainer.innerHTML = data;
            ConfigureLiveSearchSection(liveSearchResultsContainer);

            if (!data.replace(/\s/g, "")) {
                liveSearchResultsContainer.classList.add("Hide");
            } else {
                if (liveSearchResultsContainer.classList.contains("Hide")) {
                    liveSearchResultsContainer.classList.remove("Hide");
                }
            }

        },
        error: function (e) {
            console.log("failure")
            alert("Error: " + e);
        }

    });

}

//Configures a live search option mousedown event and assigns data
function ConfigureLiveSearchSection(liveSearchResultsContainer) {

    var liveSearchOptions = liveSearchResultsContainer.getElementsByClassName("LiveSearchOption");
    for (const option of liveSearchOptions) {
        option.addEventListener("mousedown", OnLiveSearchOptionClick, false);
        option.pokemonName = option.dataset.name;
    }

}

//Check contents of live search and reveal/hide it
function OnPokemonSearchFocusIn(evt) {

    var liveSearchResults = evt.currentTarget.parentElement.getElementsByClassName("LiveSearchResults");

    console.log(liveSearchResultsContainer);

    if (liveSearchResultsContainer.children.length > 0) {

        for (const searchResults of liveSearchResults) {
            searchResults.classList.remove("Hide");
        }

    }

}

//Hide live search results
function OnPokemonSearchFocusOut(evt) {

    var liveSearchResults = evt.currentTarget.parentElement.getElementsByClassName("LiveSearchResults");

    for (var searchResults of liveSearchResults) {
        searchResults.classList.add("Hide");
    }


}

//Configure each remove PokemonTiles button event handlers
function ConfigureTypeAnalysisTileRemoveButtons() {

    let buttons = document.getElementsByClassName("RemoveButton");

    for (const button of buttons) {

        button.addEventListener("click", (evt) => {

            totalSelectedPokemon -= 1;
            evt.currentTarget.parentElement.remove();

        });

    }

}

analyzeButton.addEventListener("click", GetTypeAnalysisResultsAndUpdatePage);


addPokemonButton.addEventListener("click", () => {

    if (totalSelectedPokemon < 6) {

        totalSelectedPokemon += 1;
        AddTypeAnalysisPokemon(GetPokemonName());

    }

});

//Configure search box event handlers
pokemonSearchBox.addEventListener("focusin", OnPokemonSearchFocusIn);
pokemonSearchBox.addEventListener("focusout", OnPokemonSearchFocusOut);
pokemonSearchBox.addEventListener("input", UpdateLiveSearch);

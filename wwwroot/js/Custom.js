let index = 0;

function AddTag() { 
    //get a refernce to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    //create a new Select option
    let newOption = new Option(tagEntry.value, tagEntry.value);
    document.getElementById("TagValues").options[index++] = newOption;

    //clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {

    let tagCount = 1;
    while (tagCount > 0) {
        let tagList = document.getElementById("TagValues");
        let selectedIndex = tagList.selectedIndex;
        if (selectedIndex >= 0) {
            tagList.options[selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
            index--;
        }
    }
}

$('form').on("submit", function () {
    $("#TagValues option").prop("selected", "selected");
})
let index = 0;

function AddTag() { 
    //get a refernce to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    //Lets use the new search function to help detect an error state
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        //trigger my sweet alert for duplicates or empty tags

        MySwal.fire({
            title:`<span>${searchResult}</span>`
        });

       

    }
    else {

        //create a new Select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagValues").options[index++] = newOption;

    }
    
    //clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {

    let tagCount = 1;
    let tagList = document.getElementById("TagValues");
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {

        MySwal.fire({

            title: "<span class='fw-bold'> Choose a tag before deleting. </span>"

        });

        return true;
        

    }

    while (tagCount > 0) {       
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
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

//Look for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up or replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagValues").options[index] = newOption;
}

//The Search function will detect either an empty or a duplicate tag
//and return an error string if an error is detected
function search(str) {
    if (str===null || str.length === 0) {
        return 'Empty tags are not allowed.';
    }

    var tagsElement = document.getElementById("TagValues");
    if (tagsElement) {
        let options = tagsElement.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `All tags must be unique.`;
            }
        }
    }

}

const MySwal = Swal.mixin({

    color: 'black',
    icon: 'info',
    iconColor: '#808080',
    

    buttonsStyling: true,


    showCloseButton: false,
    showCancelButton: false,
    focusConfirm: false,
    confirmButtonText:
        '<i class="fa fa-thumbs-up"></i>',
    confirmButtonAriaLabel: 'Thumbs up, Ok!',
    confirmButtonColor: '#0000FF',
    toast: 'true'
})

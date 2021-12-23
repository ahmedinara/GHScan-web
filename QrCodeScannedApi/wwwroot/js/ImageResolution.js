

function CheckImageResolution(myElementID, myWidth, myHeight, myExtantion) {

    var fi = document.getElementById(myElementID);
    if (fi.files.length > 0)      // FIRST CHECK IF ANY FILE IS SELECTED.
    {
        var fileName, fileExtension;
        // FILE NAME AND EXTENSION.
        fileName = fi.files[0].name;
        fileExtension = fileName.replace(/^.*\./, '');
        // TO GET THE IMAGE WIDTH AND HEIGHT, WE'LL USE fileReader().
        if (fileExtension == 'png' || fileExtension == 'jpg' || fileExtension == 'jpeg') {

            var reader = new FileReader(); // CREATE AN NEW INSTANCE.
            reader.readAsDataURL(fi.files[0]);
            reader.onload = function (e) {
                var img = new Image();
                img.src = e.target.result;
                img.onload = function () {
                    //alert("Current Image Width  : " + this.width);
                    //alert("Required Image Width  : " + myWidth);
                    //alert("Current Image Height  : " + this.height);
                    //alert("Required Image Width  : " + myHeight);
                    if (myExtantion != '') {
                        if (parseInt(this.width) != parseInt(myWidth) || parseInt(this.height) != parseInt(myHeight) || 'png' != myExtantion) {
                            document.getElementById('Checker').value = "1";
                            alert(myExtantion);
                            document.getElementById("ResolutionValidation").removeAttribute("hidden");
                        }
                        else {
                            document.getElementById("ResolutionValidation").setAttribute("hidden", true);
                        }
                    }
                    else {
                        if (parseInt(this.width) != parseInt(myWidth) || parseInt(this.height) != parseInt(myHeight)) {
                            document.getElementById('Checker').value = "1";
                            document.getElementById("ResolutionValidation").removeAttribute("hidden");
                        }
                        else {
                            document.getElementById("ResolutionValidation").setAttribute("hidden", true);
                        }
                    }

                }
            };
        }
    }
}
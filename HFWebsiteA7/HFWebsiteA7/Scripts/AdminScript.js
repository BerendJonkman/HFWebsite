var id = 0;
var imageChanged = false;
var formData;

//Sets cursos to normal
window.onload = function () {
    $("body").css("cursor", "default");

}

//Call to controller to remove Band
function removeBand() {
    $.ajax({
        type: "GET",
        url: '/Admin/RemoveBand',
        data: { 'bandId': id },
    }).done(function () {
        window.location.reload();
    }).fail(function (xhr, status, errorThrown) {
        alert(status);
    });
}

//Call to controller to remove Location
function removeLocation() {
    $.ajax({
        type: "GET",
        url: '/Admin/RemoveLocation',
        data: { 'locationId': id },
    }).done(function () {
        window.location.reload();
    }).fail(function (xhr, status, errorThrown) {
        alert(status);
    });
}

//Call to controller to remove Restaurant
function removeRestaurant() {
    $.ajax({
        type: "GET",
        url: '/Admin/RemoveRestaurant',
        data: { 'restaurantId': id },
    }).done(function () {
        window.location.reload();
    }).fail(function (xhr, status, errorThrown) {
        alert(status);
    });
}

//Call to controller to remove Concert
function removeConcert() {
    $.ajax({
        type: "GET",
        url: '/Admin/RemoveConcert',
        data: { 'concertId': id },
    }).done(function () {
        window.location.reload();
    }).fail(function (xhr, status, errorThrown) {
        alert(status);
    });
}

//Get the selected Band and show it in the editdialog
function showBandDialog(selectedId) {
    id = selectedId;
    $.ajax({
        url: '/Admin/GetBand',
        type: 'GET',
        data: { 'bandId': id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var url = response.ImagePath.substring(1);

            $("#bandImage").attr("src", url);
            document.getElementById("Name").value = response.Name;
            document.getElementById("Description").value = response.Description;
            var addDialog = document.getElementById('addDialog');
            addDialog.showModal();
        },
        error: function (error) {
            $(that).remove();
            DisplayError(error.statusText);
        }
    });
}

//Get the selected Loaction and show it in the editdialog
function showLocationDialog(selectedId) {
    id = selectedId;
    $.ajax({
        url: '/Admin/GetLocation',
        type: 'GET',
        data: { 'locationId': id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            document.getElementById("DialogName").value = response.Name;
            document.getElementById("DialogStreet").value = response.Street;
            document.getElementById("DialogHouseNumber").value = response.HouseNumber;
            document.getElementById("DialogCity").value = response.City;
            document.getElementById("DialogZipCode").value = response.ZipCode;

            var addDialog = document.getElementById('addDialog');
            addDialog.showModal();
        },
        error: function (error) {
            $(that).remove();
            DisplayError(error.statusText);
        }
    });
}

//Get the selected Concert and show it in the editdialog
function showConcertDialog(selectedId) {
    id = selectedId;

    $.ajax({
        url: '/Admin/GetConcert',
        type: 'GET',
        data: { 'ConcertId': id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            document.getElementById("LocationSelect").value = response.Location.Id;
            document.getElementById("HallSelect").value = response.Hall.Id;
            document.getElementById("DurationTextBox").value = response.Duration;
            document.getElementById("StartTimeTextBox").value = toJavaScriptDate(response.StartTime);
            document.getElementById("BandText").innerHTML = response.Band.Name;
            document.getElementById("DayText").innerHTML = response.Day.Name;

            var addDialog = document.getElementById('addDialog');
            addDialog.showModal();
        },
        error: function (error) {
            $(that).remove();
            DisplayError(error.statusText);
        }
    });
}

//Get the selected Restaurant and show it in the editdialog
function showRestaurantDialog(selectedId) {
    id = selectedId;
    $.ajax({
        url: '/Admin/GetAdminRestaurant',
        type: 'GET',
        data: { 'restaurantId': id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var url = response.Restaurant.ImagePath.substring(1);

            $("#ConcertImage").attr("src", url);
            setFoodTypesSelected(response.FoodTypeIdList)
            document.getElementById("NameTextBox").value = response.Restaurant.Name;
            document.getElementById("LocationSelect").value = response.Restaurant.LocationId;
            document.getElementById("DescriptonTextArea").value = response.Restaurant.Description;
            document.getElementById("SessionsTextBox").value = response.Sessions;
            document.getElementById("StartTimeTextBox").value = toJavaScriptDate(response.StartTime);
            document.getElementById("DurationTextBox").value = response.Duration;
            document.getElementById("SeatsTextBox").value = response.Restaurant.Seats;
            document.getElementById("StarsTextBox").value = response.Restaurant.Stars;
            document.getElementById("PriceTextBox").value = response.Restaurant.Price;
            document.getElementById("LessPriceTextBox").value = response.Restaurant.ReducedPrice;

            var addDialog = document.getElementById('addDialog');
            addDialog.showModal();
        },
        error: function (error) {
            $(that).remove();
            DisplayError(error.statusText);
        }
    });
}

//Saves the updated Band data
function saveBandData() {
    var name = document.getElementById("Name");
    var description = document.getElementById("Description");

    var childeren = [];
    childeren.push(name);
    childeren.push(description);

    if (checkValues(childeren)) {
        $.ajax({
            type: "POST",
            url: '/Admin/UpdateBand',
            data: {
                'bandId': id,
                'name': name.value,
                'description': description.value,
                'imageChanged': imageChanged
            },
        }).done(function () {
            var totalFiles = document.getElementById("BandImageFile").files.length;
            if (totalFiles !== 0 && imageChanged) {
                formData = new FormData();
                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementById("BandImageFile").files[i];
                    fileName = name.replace(/\s/g, '') + ".jpg";

                    file.name = fileName;
                    formData.append("bandImage", file, fileName);
                }
                saveBandImage();
            } else {
                location.reload();
            }

        }).fail(function (xhr, status, errorThrown) {
            alert(status);
        });
    }

}

//Saves the updated Location data
function saveLocationData() {

    var name = document.getElementById("DialogName");
    var street = document.getElementById("DialogStreet");
    var houseNumber = document.getElementById("DialogHouseNumber");
    var city = document.getElementById("DialogCity");
    var zipCode = document.getElementById("DialogZipCode");

    var childeren = [];
    childeren.push(name);
    childeren.push(street);
    childeren.push(houseNumber);
    childeren.push(city);
    childeren.push(zipCode);

    if (checkValues(childeren)) {
        $.ajax({
            type: "POST",
            url: '/Admin/UpdateLocation',
            data: {
                'locationId': id,
                'name': name.value,
                'street': street.value,
                'houseNumber': houseNumber.value,
                'city': city.value,
                'zipCode': zipCode.value
            },
        }).done(function () {
            location.reload();
        }).fail(function (xhr, status, errorThrown) {
            alert(status);
        });
    }
}

//Saves the updated Concert data
function saveConcertData() {
    var locationId = document.getElementById("LocationSelect");
    var hallId = document.getElementById("HallSelect");
    var duration = document.getElementById("DurationTextBox");
    var startTime = document.getElementById("StartTimeTextBox");

    var childeren = [];
    childeren.push(locationId);
    childeren.push(hallId);
    childeren.push(duration);
    childeren.push(startTime);

    if (checkValues(childeren)) {
        $.ajax({
            type: "POST",
            url: '/Admin/UpdateConcert',
            data: {
                'eventId': id,
                'locationId': locationId.value,
                'hallId': hallId.value,
                'duration': duration.value,
                'startTime': startTime.value
            },
        }).done(function () {
            location.reload();
        }).fail(function (xhr, status, errorThrown) {
            alert(status);
        });
    }
   
}

//Saves the updated Restaurant data
function saveRestaurantData() {
    var name = document.getElementById("NameTextBox").value;
    var location = document.getElementById("LocationSelect").value;
    var description = document.getElementById("DescriptonTextArea").value;
    var sessions = document.getElementById("SessionsTextBox").value;
    var startTime = document.getElementById("StartTimeTextBox").value;
    var duration = document.getElementById("DurationTextBox").value;
    var seats = document.getElementById("SeatsTextBox").value;
    var stars = document.getElementById("StarsTextBox").value;
    var price = document.getElementById("PriceTextBox").value;
    var reducedPrice = document.getElementById("LessPriceTextBox").value;
    var foodTypes = document.getElementById("FoodTypeSelect").value;

    var childeren = [];
    childeren.push(name);
    childeren.push(location);
    childeren.push(description);
    childeren.push(sessions);
    childeren.push(startTime);
    childeren.push(duration);
    childeren.push(seats);
    childeren.push(stars);
    childeren.push(reducedPrice);
    childeren.push(foodTypes);

    if (checkValues(childeren)) {
        $("body").css("cursor", "progress");
        $.ajax({
            type: "POST",
            url: '/Admin/UpdateAdminRestaurant',
            data: {
                'restaurantId': id,
                'availableSeats': seats.value,
                'name': name.value,
                'locationId': location.value,
                'price': price.value,
                'reducedPrice': reducedPrice.value,
                'stars': stars.value,
                'description': description.value,
                'sessions': sessions.value,
                'startTime': startTime.value,
                'foodTypeArray': foodTypes.value,
                'duration': duration.value,
                'imageChanged': imageChanged
            },
        }).done(function () {
            var totalFiles = document.getElementById("RestaurantImageFile").files.length;
            if (totalFiles !== 0 && imageChanged) {
                formData = new FormData();
                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementById("RestaurantImageFile").files[i];
                    fileName = name.replace(/\s/g, '') + ".jpg";

                    file.name = fileName;
                    formData.append("restaurantImage", file, fileName);
                }
                saveRestaurantImage();

            }
        }).fail(function (xhr, status, errorThrown) {
            alert(status);
        });
    }
}

//Saves image from Band to server
function saveBandImage() {
    $.ajax({
        type: "POST",
        url: '/Admin/UploadBandImage',
        data: formData,
        contentType: false,
        processData: false
    }).done(function () {
        location.reload()
    }).fail(function (xhr, status, errorThrown) {
        alert(xhr + ', ' + status + ', ' + errorThrown);
    });
}

//Saves image from Restaurant to server
function saveRestaurantImage() {
    $.ajax({
        type: "POST",
        url: '/Admin/UploadRestaurantImage',
        data: formData,
        contentType: false,
        processData: false
    }).done(function () {
        location.reload()
    }).fail(function (xhr, status, errorThrown) {
        alert(xhr + ', ' + status + ', ' + errorThrown);
    }); 
}
function checkValues(childeren) {
    for (var i = 0; i < childeren.length; i++) {
        if (childeren[i].value) {
            if (childeren[i].type === "number") {
                var regex = /^[0-9]+$/;
                if (!childeren[i].value.match(regex)) {
                    showSnackbar("One or more fields contain letters where they only should contain numbers.")
                    return false;
                }
            }
        } else {
            showSnackbar("One or more fields are empty")
            return false;
        }
    }
    return true;
}

//Closes the remove dialog
function cancelRemoveDialog() {
    var removeDialog = document.getElementById('removeDialog');
    removeDialog.close();
}

//Closes the edit dialog
function cancelAddDialog() {
    var addDialog = document.getElementById('addDialog');
    addDialog.close();
}

//Show a snackbar with chosen text
function showSnackbar(snackbarText) {
    var snackbar = document.getElementById("snackbar");
    snackbar.textContent = snackbarText;
    snackbar.className = "show";
    setTimeout(function () { snackbar.className = snackbar.className.replace("show", "");; }, 3000);
}

//Shows the remove dialog for the chosen Band
function showRemoveBandDialog(adminBandId, bandName) {
    id = adminBandId;
    document.getElementById('RemoveItemButton').addEventListener("click", removeBand);
    document.getElementById('RemoveText').innerHTML = "Do you really want to remove " + bandName + " ?";
    var removeDialog = document.getElementById('removeDialog');
    removeDialog.showModal();
}

//Shows the remove dialog for the chosen Concert
function showRemoveConcertDialog(concertId, bandName) {
    id = concertId;
    document.getElementById('RemoveItemButton').addEventListener("click", removeConcert);
    document.getElementById('RemoveText').innerHTML = "Do you really want to remove " + bandName + " ?";
    var removeDialog = document.getElementById('removeDialog');
    removeDialog.showModal();
}

//Shows the remove dialog for the chosen Restaurant
function showRemoveRestaurantDialog(restaurantId, restaurantName) {
    id = restaurantId;
    document.getElementById('RemoveItemButton').addEventListener("click", removeRestaurant);
    document.getElementById('RemoveText').innerHTML = "Do you really want to remove " + restaurantName + " ?";
    var removeDialog = document.getElementById('removeDialog');
    removeDialog.showModal();
}

//Shows the remove dialog for the chosen Location
function showRemoveLocationDialog(locationId, locationName) {
    id = locationId;
    document.getElementById('RemoveItemButton').addEventListener("click", removeLocation);
    document.getElementById('RemoveText').innerHTML = "Do you really want to remove " + locationName + " ?";
    var removeDialog = document.getElementById('removeDialog');
    removeDialog.showModal();
}

//Shows the create new item div
function showDiv() {
    if (document.getElementById('divCreate').style.display === "none") {
        document.getElementById('divCreate').style.display = "block";
        document.getElementById('newButton').textContent = "Close";
    } else {
        document.getElementById('divCreate').style.display = "none";
        document.getElementById('newButton').textContent = "New";
    }
}

//Sets a boolean which can be send to the controller to check if the image has been changed
function setImageChanged() {
    imageChanged = true;
}

//Sets the selected foodtypes to selected
function setFoodTypesSelected(foodTypeList) {
    $("#FoodTypeSelect").val(foodTypeList).trigger("chosen:updated");
}

//Changes the date to a date to a more readable one
function toJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    var minutes = dt.getMinutes().toString();
    if (minutes === "0") {
        minutes = "00";
    }
    var startTime = dt.getHours() + ":" + minutes;
    return (startTime);
}
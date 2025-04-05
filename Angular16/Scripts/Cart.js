let itemToDelete = null;

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        document.getElementById("location").innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    var geocodeUrl = "https://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon;

    fetch(geocodeUrl)
        .then(response => response.json())
        .then(data => {
            var address = data.address;
            var fullAddress = [
                address.road,
                address.suburb,
                address.city,
                address.state,
                address.postcode
            ].filter(Boolean).join(", ");
            document.getElementById("pin-code-input").value = fullAddress;
        })
        .catch(error => {
            console.error("Error fetching address:", error);
        });
}

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main-content").classList.add("blur");
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main-content").classList.remove("blur");
}

function confirmDelete(itemId) {
    itemToDelete = itemId;
    const item = document.getElementById(itemId);
    const itemName = item.querySelector('h3').innerText;
    const itemImage = item.querySelector('img').src;
    const itemPrice = item.querySelector('.price').innerText;
    document.getElementById("deleteItemText").innerHTML = `<h3>${itemName}</h3>`;
    document.getElementById("deleteItemImage").src = itemImage;
    document.getElementById("deleteItemPrice").innerText = itemPrice;
    document.getElementById("deleteModal").style.display = "block";
}

function closeModal() {
    document.getElementById("deleteModal").style.display = "none";
}

function deleteItem() {
    if (itemToDelete) {
        var item = document.getElementById(itemToDelete);
        item.parentNode.removeChild(item);
        closeModal();
    }
}
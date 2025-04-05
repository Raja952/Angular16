
const currentUrl = window.location.href;

// Create a URL object from the current URL
const urlObj = new URL(currentUrl);

// Get query parameters
var name = urlObj.searchParams.get('Id'); // e.g., "John" if the URL is ...?name=John&age=30
$("#hdnHiddenItem").val(name);


GetXMLItems(name);




//$(document).ready(function () {   
//        switch (name) {
//            case "Sofa":
//System.import('../app/Iteams/Sofa/main').catch(function (err) { });
//break;

//        }    


//});

function GetXMLItems(itemName) {
    $.ajax({
        type: "POST",
        url: "../Common/GetIteamData", // Use an absolute URL
        data: JSON.stringify({ "Iteam": itemName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // Clear previous items
            $('#SearchItemq').empty();

            // Check if data is not empty
            if (data.length > 0) {
                // Loop through the data and create HTML elements
                $.each(data, function (index, item) {
                    // Debugging: Log the item to see its structure
                    console.log("Item:", item);

                    // Check if properties exist
                    var imageUrl = item.imageUrl || "default-image-url.jpg"; // Fallback image
                    var name = item.name || "No Name";
                    var price = item.price || "N/A";
                    var originalPrice = item.originalPrice || "N/A";
                    var discount = item.discount || "N/A";
                    var quantity = item.quantity || "N/A";
                    var maxQuantity = item.maxQuantity || "N/A";
                    var description = item.description || "No Description";

                    var productCard = `
                <div class="product-card">
                    <img src="${imageUrl}" alt="${name}" />
                    <div class="product-info">
                        <h5>${name}</h5>
                        <p>Price: $${price} <span class="original-price">($${originalPrice})</span></p>
                        <p>Discount: ${discount}%</p>
                        <p>Quantity Available: ${quantity} (Max: ${maxQuantity})</p>
                        <p class="description">${description}</p>
                    </div>
                </div>
            `;
                    $('#SearchItemq').append(productCard);
                });
            } else {
                $('#SearchItemq').append('<p>No items found.</p>');
            }
        
        }
    });
}
//function GetXMLItems(itemName) {
//        var Emailid = "";

//$.ajax({
//    type: "POST",
//url: "../Common/GetIteamData", // Use an absolute URL
//data: JSON.stringify({"Iteam": itemName }),
//contentType: "application/json; charset=utf-8",
//dataType: "json",
//success: function (data) {
//    alert(data);
//            },
//error: function (xhr, status, error) {
//    console.error("Error occurred: ", status, error);
//alert("An error occurred: " + xhr.responseText);
//            }
//        });
//    }



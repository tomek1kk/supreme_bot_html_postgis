// const category = "new";

// const ITEM_NAME = "ZIP IT";
// const COLOR = "GREY";

// const BILLING_INFO = {
//     "first name": "First",
//     "last name": "Last",
//     "email": "email@test.com",
//     "tel": "123456789",
//     "address": "address test",
//     "city": "City",
//     "postal code": "00000",
//     "phone": "123456789"
// }

let url = window.location.href;
const main_url = "https://shop.palaceskateboards.com/";
const cart_url = "https://shop.palaceskateboards.com/cart";
const category_url = main_url + "collections/" + category;
const SIZES = ["Small", "Medium", "Large", "X-Large"];

const delay = ms => new Promise(res => setTimeout(res, ms));

const addToCart = async () => {
    for (let i = 0; i < SIZES.length; i++) {
        let cartCount = document.getElementsByClassName("cart-count")[0].textContent;
        for (let j = 0; j < document.getElementById("product-select").length; j++) {
            if (document.getElementById("product-select").options[j].textContent == SIZES[i]) {
                document.getElementsByClassName("add cart-btn clearfix")[0].disabled = false;
                console.log("Found size " + SIZES[i]);
                document.getElementById("product-select").selectedIndex = j;
                document.getElementsByClassName("add cart-btn clearfix")[0].click();
                while (document.getElementsByClassName("cart-count")[0].textContent == cartCount) {
                    await delay(50);
                }
                break;
            }
        }

    }
    chrome.runtime.sendMessage({ redirect: cart_url });
}


if (url == main_url) {
    chrome.runtime.sendMessage({ redirect: category_url });
}

else if (url == category_url) // in category
{
    let scroll = setInterval(function () {
        document.documentElement.scrollTop = document.documentElement.scrollHeight; // scroll
        lookForItem(scroll);
    }
        , 50);
}

else if (url.includes("https://shop.palaceskateboards.com/products/")) // in product
{
    addToCart();
}

else if (url == cart_url) {
    document.getElementById("terms-checkbox").checked = true;
    document.getElementById("checkout").click();
}

else if (url.includes("checkouts")) {
    let inputs = document.querySelectorAll('input:not([type=submit]):not([type=hidden])');
    console.log(inputs);
    if (inputs.length > 10) {
        inputs.forEach(function (element) {
            let prev_sibling = element.previousElementSibling;
            if (prev_sibling != null) {
                let label_text = prev_sibling.innerHTML.toLowerCase();
                console.log(label_text);
                let value = BILLING_INFO[label_text];
                console.log(value);
                if (value != null)
                    setInput(element, value);
            }
        });
    }
    else {
        document.getElementsByClassName("step__footer__continue-btn")[0].click();

    }
}

function lookForItem(scroll) {
    let products = document.getElementsByClassName("product-grid-item clearfix");
    for (let i = 0; i < products.length; i++) {
        if (products[i].innerHTML.includes(ITEM_NAME) && products[i].innerHTML.includes(COLOR)) {
            clearInterval(scroll);
            chrome.runtime.sendMessage({ redirect: products[i].children[0].href });
        }
    }

}

function wait(cartCount) {
    let wait = setInterval(function () {
        console.log(cartCount + " vs " + document.getElementsByClassName("cart-count")[0].textContent);
        if (cartCount != document.getElementsByClassName("cart-count")[0].textContent) {
            clearInterval(wait);
        }
    }, 50);
}

function setInput(element, value) {
    element.focus();
    element.value = value;
    element.blur();
}

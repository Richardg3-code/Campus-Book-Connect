let cart = [];
let total = 0;

function addToCart(title, price) {
    cart.push({ title, price });
    total += price;
    updateCartDisplay();
}

function updateCartDisplay() {
    const cartList = document.getElementById("cartItems");
    cartList.innerHTML = "";

    if (cart.length === 0) {
        cartList.innerHTML = "<li>No items in cart yet.</li>";
    } else {
        cart.forEach(item => {
            const li = document.createElement("li");
            li.textContent = `${item.title} - $${item.price.toFixed(2)}`;
            cartList.appendChild(li);
        });
    }

    document.getElementById("totalPrice").innerHTML = `<strong>Total:</strong> $${total.toFixed(2)}`;
}

document.getElementById("checkoutBtn").addEventListener("click", function () {
    if (cart.length === 0) {
        alert("Your cart is empty.");
        return;
    }

    // Clear cart
    cart = [];
    total = 0;
    updateCartDisplay();

    // Show thank you message
    const success = document.getElementById("successMessage");
    success.style.display = "block";

    setTimeout(() => {
        success.style.display = "none";
    }, 4000);
});

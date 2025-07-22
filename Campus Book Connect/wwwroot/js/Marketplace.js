
// this is the adding car method 
let cart = [];
let total = 0;

function addToCart(title, price) {

    // adding up the totla price thats inside the cart 
    cart.push({ title, price });
    total += price; 
    renderCart();
}

function renderCart() {
    
    const cartList = document.getElementById('cartItems');
    const totalPrice = document.getElementById('totalPrice');

    cartList.innerHTML = '';


    //looping ech element insidde the cart
    cart.forEach(item => {
        const li = document.createElement('li');
        li.textContent = `${item.title} — $${item.price.toFixed(2)}`;
        cartList.appendChild(li);
    });

    //updating the total price shown on the page 
    totalPrice.innerHTML = `<strong>Total:</strong> $${total.toFixed(2)}`;
}


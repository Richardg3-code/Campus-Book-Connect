let cart = [];

document.querySelectorAll('.buy-btn').forEach(button => {
    button.addEventListener('click', function () {
        const bookId = parseInt(this.getAttribute('data-id'));
        const title = this.getAttribute('data-title');
        const price = parseFloat(this.getAttribute('data-price'));

        if (cart.some(item => item.id === bookId)) {
            alert(`"${title}" is already in your cart.`);
            return;
        }

        cart.push({ id: bookId, title, price });

        const cartItems = document.getElementById('cartItems');
        cartItems.innerHTML = "";
        let total = 0;

        cart.forEach(item => {
            const li = document.createElement('li');
            li.textContent = `${item.title} - $${item.price.toFixed(2)}`;
            cartItems.appendChild(li);
            total += item.price;
        });

        document.getElementById('totalPrice').innerHTML = `<strong>Total:</strong> $${total.toFixed(2)}`;
    });
});

document.getElementById('checkoutBtn').addEventListener('click', function () {
    const bookIdsInput = document.getElementById('bookIdsInput');
    const bookIds = cart.map(item => item.id);
    bookIdsInput.value = bookIds.join(',');
});

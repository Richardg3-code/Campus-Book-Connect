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

        cart.push({ id: bookId, title: title, price: price });

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
    cart.forEach(item => {
        const bookCard = document.getElementById(`book-${item.id}`);
        if (bookCard) {
            bookCard.remove();
        }
    });

    cart = [];
    document.getElementById('cartItems').innerHTML = '<li>No items in cart yet.</li>';
    document.getElementById('totalPrice').innerHTML = '<strong>Total:</strong> $0.00';

    const successMessage = document.getElementById('successMessage');
    successMessage.style.display = 'block';
    setTimeout(() => {
        successMessage.style.display = 'none';
    }, 3000);
});

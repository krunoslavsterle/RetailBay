function addToCart(url, productId) {

    $.ajax({
        url: url,
        type: 'POST',
        data: {
            productId: productId
        },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    })
    .done(function (result) {
        console.log('AddToCart result: ' + result);
        updateCart(result);
    });    
}

function updateCart(productsCount) {
    $('#cart-count').html(productsCount);
}

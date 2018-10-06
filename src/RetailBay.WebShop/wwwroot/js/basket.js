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
    if (productsCount <= 0) {
        $('#itemCount').html('').css('display', 'none');
        $('#cartItems').html('');
    } else {
        $('#itemCount').html(productsCount).css('display', 'block');
    }
}

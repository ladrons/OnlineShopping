// Herhangi bir View'da kullanılırsa ekran 5 saniye sonra lokasyon'da yazan adrese gider.

$(function () {
    setTimeout(function () {
        window.location = "/Home/Login"
    }, 5000)
})
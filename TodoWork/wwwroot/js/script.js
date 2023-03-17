// UserIndex

var editUser = document.getElementById("editUser");
var userName = document.getElementById("userName");
editUser.style.display = "none";
userName.onmouseover = function () { mouseHoverIn() }
editUser.onmouseout = function () { mouseHoverOut() }
function mouseHoverIn() {
    editUser.style.display = "block";
    userName.style.display = "none";
}
function mouseHoverOut() {
    userName.style.display = "block";
    editUser.style.display = "none";
}

var exampleModal = document.getElementById('DeleteModal')
exampleModal.addEventListener('show.bs.modal', function (event) {
    // Button that triggered the modal
    var button = event.relatedTarget
    // Extract info from data-bs-* attributes
    var recipient = button.getAttribute('data-bs-whatever')
    var recipient2 = button.getAttribute('data-bs-user')
    // If necessary, you could initiate an AJAX request here
    // and then do the updating in a callback.
    //
    // Update the modal's content.
    var modalTitle = exampleModal.querySelector('.modal-title')
    var modalBodyInput = exampleModal.querySelector('.modal-body input')
    var inputVaule = document.getElementById("recipient-name")
    var inputVaule2 = document.getElementById("user")
    // modalTitle.textContent = 'New message to ' + recipient
    inputVaule.value = recipient
    inputVaule2.value = recipient2
})

var element = document.getElementById("myToast");
var myToast = new bootstrap.Toast(element);
myToast.show();
function SubmitToBlock() {
    let operationTypeInput = document.getElementById('operationTypeInput');
    operationTypeInput.value = 1;
    let form = document.getElementById('form1');
    form.submit();
}
function SubmitToUnblock() {
    let operationTypeInput = document.getElementById('operationTypeInput');
    operationTypeInput.value = 2;
    let form = document.getElementById('form1');
    form.submit();
}
function SubmitToDelete() {
    let operationTypeInput = document.getElementById('operationTypeInput');
    operationTypeInput.value = 3;
    let form = document.getElementById('form1');
    form.submit();
}
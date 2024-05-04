function showInformationToast(opts) {

    let toast = document.querySelector('#informationToast');
    showToast(toast, opts);

}

function showSuccessToast(opts) {

    let toast = document.querySelector('#successToast');
    showToast(toast, opts);

}

function showWarningToast(opts) {

    let toast = document.querySelector('#warningToast');
    showToast(toast, opts);

}

function showErrorToast(opts) {

    let toast = document.querySelector('#errorToast');
    showToast(toast, opts);

}

function showToast(toast, opts) {

    if (toast === null) { return false; }
    if (toast === undefined) { return false; }

    let title = opts['Title'];
    let message = opts['Message'];
    let delay = opts['Delay'];

    let toastClone = toast.cloneNode(true);
    let container = document.querySelector('.toast-container');
    container.appendChild(toastClone);

    let toastHeader = toastClone ? toastClone.querySelector('.toast-header') : null;
    let toastTitle = toastHeader ? toastHeader.querySelector('strong') : null;
    let toastBody = toastClone ? toastClone.querySelector('.toast-body') : null;

    if (!isNullOrEmpty(title)) {
        toastTitle.innerHTML = title.trim();
    }

    if (!isNullOrEmpty(message)) {
        toastBody.innerHTML = message;
    }

    if (!isNullOrEmpty(delay)) {
        if (!isNaN(parseFloat(delay.trim()))) {
            if (delay.trim() === '0') {
                toastClone.dataset.bsAutohide = false;
            } else {
                toastClone.dataset.bsDelay = delay;
            }
        }
    }

    toastClone.addEventListener('hidden.bs.toast', () => {
        container.removeChild(toastClone);
    });

    let toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastClone);
    toastBootstrap.show();

}

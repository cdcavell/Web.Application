const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('errorToast')

if (toastTrigger) {
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
    toastTrigger.addEventListener('click', () => {
        toastBootstrap.show()
    })
}
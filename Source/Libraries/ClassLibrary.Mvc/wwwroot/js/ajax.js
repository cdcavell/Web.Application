/* Example Post Call:
 *
 * ajaxPost(url, token, model)
 *     .then(function (data) {
 *         console.debug(data);
 *     })
 *     .catch((error) => {
 *         console.log(error)
 *     });
 */

function ajaxPost(url, token, model) {

    wait();

    return new Promise((resolve, reject) => {
        console.debug('-- AJax POST: ' + url + ' token: ' + token);
        console.debug(model);
            
        $.ajax({
            url: url,
            method: "POST",
            async: true,
            cache: false,
            dataType: "json",
            data: {
                __RequestVerificationToken: token,
                model: model
            },
            success: function (data) {
                console.debug('-- AJax Success');
                console.debug(data);
                resolve(data);
            },
            complete: function () {
                console.debug('-- AJax Complete');
                noWait();
            },
            error: function (error) {
                console.debug('-- AJax Error');
                ajaxError(error);
                reject(error)
            }
        });       
    })
}


/* Example Get Call:
 *
 * ajaxGet(url)
 *     .then(function (data) {
 *         console.debug(data);
 *     })
 *     .catch((error) => {
 *         console.log(error)
 *     });
 */
function ajaxGet(url) {

    wait();

    return new Promise((resolve, reject) => {
        console.debug('-- AJax GET: ' + url);
        $.ajax({
            url: url,
            method: "GET",
            async: true,
            cache: false,
            dataType: "json",
            success: function (data) {
                console.debug('-- AJax Success');
                console.debug(data);
                resolve(data);
            },
            complete: function () {
                console.debug('-- AJax Complete');
                noWait();
            },
            error: function (error) {
                console.debug('-- AJax Error');
                ajaxError(error);
                reject(error)
            }
        });
    })
}

async function ajaxError(error) {

    let statusCode = error.status;
    let title = error.statusText;
    let detail = error.responseText;

    if (statusCode === 401) {
        title = 'Unauthorized';
        detail = 'The request requires user authentication.';
    }

    if (error.responseJSON != null && error.responseJSON != undefined) {

        statusCode = error.responseJSON.status;
        title = error.responseJSON.title;
        detail = error.responseJSON.detail;

    }

    console.error('Status Code : ' + statusCode);
    console.error('Message     : ' + title);
    console.error('Detail      : ' + detail);

    if ((statusCode < 500) && (statusCode != 401)) {

        showWarningToast({ 'Title': title, 'Message': detail });

    } else {

        showErrorToast({ 'Title': title, 'Message': detail });

    }

}


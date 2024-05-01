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
            },
            error: function (error) {
                console.debug('-- AJax Error');
                ajaxError(error);
                reject(error)
            }
        });
    })
}

function ajaxError(error) {

    console.error('Status Code : ' + error.responseJSON.status);
    console.error('Status Text : ' + error.responseJSON.title);
    console.error('Detail      : ' + error.responseJSON.detail);

    alert('Status Code: ' + error.responseJSON.status + '\r\nStatus Text: ' + error.responseJSON.title + '\r\nDetail: ' + error.responseJSON.detail);

}


function isNullOrEmpty(value) {

    if (value === null) { return true; }
    if (value === undefined) { return true; }

    if (typeof value === 'string' || value instanceof String) {
        if (value.trim() === '') { return true; }
    } else {
        return true;
    }

    return false;
}
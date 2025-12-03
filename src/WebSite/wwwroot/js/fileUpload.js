
async function uploadFile(inputElement, token) {
    const files = inputElement.files;
    if (!files || files.length === 0) {
        return { success: false, status: 400, body: "No file selected." };
    }

    const file = files[0];
    const fd = new FormData();
    fd.append("request", file, file.name);

    const headers = {};
    if (token) {
        headers["RequestVerificationToken"] = token;
    }

    try {
        const resp = await fetch("api/cnab/upload", {
            method: "POST",
            body: fd,
            headers: headers,
            credentials: 'same-origin'
        });

        const status = resp.status;
        const text = await resp.text();

        if (!resp.ok) {
            return { success: false, status: status, body: text };
        }

        try {
            const json = text ? JSON.parse(text) : null;
            return {
                success: true, status: status, body: text, summary: json.summary || null
            };
        } catch {
            return { success: true, status: status, body: text, summary: null };
        }
    } catch (err) {
        return { success: false, status: 0, body: String(err) };
    }
}

async function getStores() {
    const resp = await fetch('/api/cnab/stores',
        {
            method: "GET",
            credentials: 'same-origin'
        });
    if (!resp.ok) {
        throw new Error('Failed to fetch stores: ' + resp.status + ' ' + resp.statusText);
    }
    return await resp.text();
}

window.getStores = getStores;
window.uploadFile = uploadFile;
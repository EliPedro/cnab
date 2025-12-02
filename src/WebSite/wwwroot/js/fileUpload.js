window.cnabUpload = {
    uploadFile: async function (inputElement, url, token) {
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
            const resp = await fetch(url, {
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
                return { success: true, status: status, body: text, summary: json };
            } catch {
                return { success: true, status: status, body: text, summary: null };
            }
        } catch (err) {
            return { success: false, status: 0, body: String(err) };
        }
    }
};
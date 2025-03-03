import { useState } from "react";
import axios from "axios";

const useApi = (url, method = "GET", config = {}) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [uploadProgress, setUploadProgress] = useState(0);

    const callApi = async (body = null) => {
        setIsLoading(true);
        setError(null);
        setUploadProgress(0);
        const formData = new FormData();
        formData.append("file", body);

        try {
            const response = await axios({
                url, method, data: formData, ...config, onUploadProgress: (progressEvent) => {
                    if (progressEvent.total) {
                        const progress = Math.round((progressEvent.loaded * 100) / progressEvent.total
                        );
                        setUploadProgress(progress);
                    }
                },
            });
            setData(response.data);
        } catch (err) {
            setError(err);
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, data, error, uploadProgress, callApi };
};

export default useApi;
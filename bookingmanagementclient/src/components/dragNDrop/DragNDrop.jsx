import React, { useEffect, useState } from "react";
import { AiOutlineCloudUpload } from "react-icons/ai";
import { MdClear } from "react-icons/md";
import "./drag-drop.css";
import toast from "react-hot-toast";

const MAX_FILE_SIZE = 15 * 1024 * 1024;
const MAX_FILE_SIZE_ERROR = "File size cannot be greater than 15 MB";

const DragNdrop = ({ onFilesSelected, width, height, title = "Upload FIle", onUpload }) => {
    const [file, setFile] = useState(null);
    const inputId = `browse-${Math.random().toString(36).substring(2, 9)}`;

    const handleFileChange = (event) => {
        event.preventDefault();
        const selectedFile = event.target.files?.[0];
        if (selectedFile) {
            if (selectedFile.size > MAX_FILE_SIZE) {
                toast.error(MAX_FILE_SIZE_ERROR, {
                    duration: 3000,
                });
                return;
            }
            setFile(selectedFile);
        }
    };

    const handleDrop = (event) => {
        event.preventDefault();
        const droppedFile = event.dataTransfer.files?.[0];

        if (droppedFile) {
            if (droppedFile.size > MAX_FILE_SIZE) {
                toast.error(MAX_FILE_SIZE_ERROR, {
                    duration: 3000,
                });
                return;
            }
            setFile(droppedFile);
        }
    };

    const handleRemoveFile = () => {
        setFile(null);
    };

    const handleUpload = () => {
        if (file) {
            onUpload?.();
        }
    };

    useEffect(() => {
        if (file) {
            onFilesSelected(file);
        }
    }, [file, onFilesSelected]);

    return (
        <>
            <h4 className="fileTitle">{title}</h4>
            <section className="drag-drop" style={{ width: width, height: height }}>
                <div
                    className={`document-uploader ${file ? "active" : ""}`}
                    onDrop={handleDrop}
                    onDragOver={(event) => event.preventDefault()}
                >
                    <>
                        <div className="upload-info">
                            <AiOutlineCloudUpload />
                            <div>
                                <p>Drag and drop your files here</p>
                                <p>Limit 15MB per file. Supported files: .XLS, .XLSX, .CSV</p>
                            </div>
                        </div>
                        <input
                            type="file"
                            id={inputId}
                            hidden
                            onChange={handleFileChange}
                            multiple={false}
                            accept=".xls,.xlsx,.csv"
                        />
                        <label htmlFor={inputId} className="browse-btn">
                            Browse files
                        </label>
                    </>

                    {file && (
                        <div className="file-list">
                            <div className="file-list__container">
                                <div className="file-item">
                                    <div className="file-info">
                                        <p>{file.name}</p>
                                    </div>
                                    <div className="file-actions">
                                        <MdClear onClick={handleRemoveFile} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}

                    {file && (
                        <button className="upload-btn" onClick={handleUpload}>
                            Upload File
                        </button>
                    )}
                </div>
            </section>
        </>
    );
};

export default DragNdrop;
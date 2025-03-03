import { useCallback, useState } from "react";
import DragNdrop from "./components/dragNDrop/DragNDrop";
import React from "react";
import "./App.css";
import { Toaster } from "react-hot-toast";
import useApi from "./hooks/useApi";

function App() {
    const [memberInfoFile, setMemberInfoFile] = useState(null);
    const [inventoryFile, setInventoryFile] = useState(null);

    // Using the updated useApi hook for both member and inventory file uploads
    const {
        isLoading: isMemberLoading,
        data: memberData,
        error: memberError,
        uploadProgress: memberProgress,
        callApi: uploadMemberFile,
    } = useApi("https://localhost:7016/Booking/ImportMembers", "POST", { headers: { "Content-Type": "multipart/form-data" }, });

    const {
        isLoading: isInventoryLoading,
        data: inventoryData,
        error: inventoryError,
        uploadProgress: inventoryProgress,
        callApi: uploadInventoryFile,
    } = useApi("https://localhost:7016/Booking/ImportInventory", "POST", { headers: { "Content-Type": "multipart/form-data" } });

    const handleMemberFileUpload = useCallback(() => {
        uploadMemberFile(memberInfoFile);
    }, [memberInfoFile]);

    const handleInventoryFileUpload = useCallback(() => {
        uploadInventoryFile(inventoryFile);
    }, [inventoryFile]);

    return (
        <div className="section">
            <div>
                <DragNdrop
                    title="Member Info"
                    onFilesSelected={setMemberInfoFile}
                    width="300px"
                    height="auto"
                    onUpload={handleMemberFileUpload}
                />
                {isMemberLoading && <p>Uploading Member Info...</p>}
                {memberProgress > 0 && (<p>Member Info Upload Progress: {memberProgress}%</p>)}
                {memberError && (<p style={{ color: "red" }}>Error: {memberError.message}</p>)}
                {memberData && <p>Member Info upload successful!</p>}
            </div>
            <div>
                <DragNdrop
                    title="Inventory"
                    onFilesSelected={setInventoryFile}
                    width="300px"
                    height="auto"
                    onUpload={handleInventoryFileUpload}
                />
                {isInventoryLoading && <p>Uploading Inventory...</p>}
                {inventoryProgress > 0 && (<p>Inventory Upload Progress: {inventoryProgress}%</p>)}
                {inventoryError && (<p style={{ color: "red" }}>Error: {inventoryError.message}</p>)}
                {inventoryData && <p>Inventory upload successful!</p>}
            </div>

            <Toaster />
        </div>
    );
}

export default App;
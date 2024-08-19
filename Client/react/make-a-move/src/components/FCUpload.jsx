import React, { useState, useEffect } from "react";
import axios from "axios";
import { makeAmoveUserServer } from "../services";
import { FCImagesErrorDialog } from "../screens/ImagesDialog/FCImageErrorDialog";

function UploadImage({ obj }) {
  const [file, setFile] = useState(null);
  const [message, setMessage] = useState("");
  const [icon, setIcon] = useState("");
  const [showErrorModal, setShowErrorModal] = useState(false); // State to manage modal visibility

  const handleFileChange = (e) => {
    if (e.target.files[0].name.includes(" ")) {
      setShowErrorModal(true);
    } else {
      setFile(e.target.files[0]);
      setIcon("✔️");
    }
  };

  const handleUpload = async (e) => {
    e.preventDefault();
    console.log(file);
    if (!file) {
      setMessage("קודם צריך לבחור תמונה");
      return;
    }
    if (obj.image.length >= 4) {
      setMessage("ניתן להוסיף עד 4 תמונות.");
      return;
    }
    const formData = new FormData();
    formData.append("files", file);
    console.log(file);
    console.log(formData.getAll("files"));
    try {
      const response = await axios.post(
        `${import.meta.env.VITE_MAKE_A_MOVE_SERVER_URL}/Users/changeImages/${
          obj.email
        }`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      console.log(response);
      console.log(obj);
      if (response.data) {
        // setMessage(response.data);
        obj.image.push(String(response.data));
        console.log(obj);
        makeAmoveUserServer
          .updateUser(obj)
          .then((res) => {
            console.log(res + " image added successfuly.");
            location.reload();
          })
          .catch((res) => console.log(res));
      }
    } catch (error) {
      setMessage("Error uploading image: " + error.message);
    }
    return;
  };

  return (
    <div style={{ width: "100%" }}>
      <FCImagesErrorDialog
        open={showErrorModal}
        setClose={() => {
          setShowErrorModal(false);
        }}
      />

      <label className="custom-file-label">
        + הוסף תמונה<span> {icon}</span>
      </label>

      <input type="file" className="input-file" onChange={handleFileChange} />
      <button className="custom-file-upload" onClick={handleUpload}>
        העלאה
      </button>
      {message && <p>{message}</p>}
    </div>
  );
}

export default UploadImage;

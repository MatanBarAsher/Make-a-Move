import React, { useState, useEffect } from "react";
import axios from "axios";
import RemoveCircleOutlineRoundedIcon from "@mui/icons-material/RemoveCircleOutlineRounded";
import { makeAmoveUserServer } from "../services";

function DisplayImage({ image, user, imageIndex }) {
  const [imageSrc, setImageSrc] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    const endOfUrl = image; // check how to add spaces at this string
    const imageUrl = `${import.meta.env.VITE_SERVER_IMAGE_SRC_URL}${endOfUrl}`;
    console.log(imageUrl);
    setImageSrc(imageUrl);
  }, [image]);

  if (error) {
    return <p>{error}</p>;
  }

  const deleteImage = (e, imageIndex) => {
    e.preventDefault();
    console.log(imageIndex);
    console.log(user.image);
    user.image.splice(imageIndex, 1);
    makeAmoveUserServer
      .updateUser(user)
      .then((res) => {
        console.log(res.data + " image removed.");
        location.reload();
      })
      .catch((res) => console.log(res + " error removing image."));
  };

  return (
    <div>
      <h3>
        <button
          className="delete-btn"
          id={image}
          onClick={(e) => deleteImage(e, imageIndex)}
        >
          <RemoveCircleOutlineRoundedIcon
            style={{ fontSize: "18px", color: "#3C0753" }}
          />
        </button>
      </h3>
      {imageSrc ? (
        <img
          src={imageSrc}
          alt="User Image"
          style={{ width: "100px", height: "100px", objectFit: "cover" }}
        />
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}

export default DisplayImage;

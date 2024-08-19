import React, { useState, useEffect } from "react";
import FCCustomBtn from "../components/FCCustomBtn";
import { makeAmoveUserServer } from "../services";
import { useNavigate } from "react-router-dom";
import FCImageInp from "../components/FCImageInp";
import FCUpload from "../components/FCUpload";
import FCGetImage from "../components/FCGetImage";
import { CleaningServices } from "@mui/icons-material";

export default function FCSetImages() {
  const [user, setUser] = useState({});
  const navigate = useNavigate(); // Assuming you might need this for navigation
  const origin = JSON.parse(localStorage.getItem("imageOrigin"));

  useEffect(() => {
    const currentEmail = JSON.parse(localStorage.getItem("current-email"));
    if (currentEmail) {
      makeAmoveUserServer
        .getUserByEmail(currentEmail)
        .then((userData) => {
          setUser(userData); // Set user state with retrieved data
          console.log(userData);
        })
        .catch((error) => {
          console.error("Error:", error);
          // Handle error
        });
    }
  }, []);

  const handleImgInput = (e) => {
    e.preventDefault();
    console.log(origin);
    console.log(user);
    if (origin === "myProfile") {
      navigate("/myProfile");
    } else {
      navigate("/preferences");
    }
  };

  return (
    <>
      <h1>תמונות</h1>
      <form onSubmit={handleImgInput}>
        <div className="images-inp-container">
          {user ? <FCUpload obj={user} /> : <></>}{" "}
          <div className="image-view">
            {/* Render existing images */}
            {user.image && user.image.length > 0 ? (
              user.image.map((img) => (
                <FCGetImage
                  key={img}
                  image={img}
                  user={user}
                  imageIndex={user.image.indexOf(img)}
                />
              ))
            ) : (
              <p onClick={() => console.log(user)}>אין תמונות להציג</p>
            )}
          </div>
        </div>
        <FCCustomBtn title="סיום" type="submit" />
      </form>
    </>
  );
}

import React, { useEffect, useState } from "react";
import FCCustomX from "../components/FCCustomX";
import background from "../assets/images/Matan.jpg";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import StarBorderRoundedIcon from "@mui/icons-material/StarBorderRounded";
import WavingHandOutlinedIcon from "@mui/icons-material/WavingHandOutlined";
import { Navigate, useNavigate } from "react-router";
import FCMyProfile from "./FCMyProfile/components/FCMyProfile";
import { makeAmoveUserServer } from "../services";
import LocationOnOutlinedIcon from "@mui/icons-material/LocationOnOutlined";
import ModeEditOutlineOutlinedIcon from "@mui/icons-material/ModeEditOutlineOutlined";
import { ProfileLogout } from "./FCMyProfile/components/DialogsProfiles/profileLogout";
import GradingIcon from "@mui/icons-material/Grading";

export default function FCSideMenu({ name }) {
  const [UserData, setUserData] = useState({});
  const [firstImage, setFirstImage] = useState("");
  const [showSuccessModal, setShowSuccessModal] = useState(false);

  const Navigate = useNavigate();
  const email = JSON.parse(localStorage.getItem("current-email"));
  console.log(email);
  const origin = JSON.parse(localStorage.getItem("origin"));
  makeAmoveUserServer.GetImagesByEmail(email).then((res) => console.log(res));
  console.log(window.refferer);

  useEffect(() => {
    makeAmoveUserServer
      .getUserByEmail(email)
      .then((res) => {
        console.log(res);
        setUserData(res);
      })
      .catch((res) => console.log(res));
    console.log(UserData);
  }, []);

  useEffect(() => {
    if (UserData.image) {
      console.log(UserData.image[0]);
      setFirstImage(UserData.image[0]);
      console.log(firstImage);
    }
  }, [UserData]);

  const Logout = () => {
    setShowSuccessModal(true);
  };

  return (
    <div className="side-menu">
      <ProfileLogout
        open={showSuccessModal}
        setClose={() => {
          setShowSuccessModal(false);
          Navigate("/");
        }}
      />
      <a onClick={() => Navigate(`/${origin}`)}>
        <FCCustomX color="white" />
      </a>
      <div className="upper-side-menu">
        <h2>{name}</h2>
        <div
          className="p-image"
          style={{
            backgroundImage: `url(${
              import.meta.env.VITE_SERVER_IMAGE_SRC_URL
            }${firstImage})`,
            height: 100,
            width: 100,
            border: "4px solid white",
            borderRadius: "50%",
          }}
        ></div>
      </div>
      <div className="lower-side-menu">
        <a onClick={() => Navigate("/myProfile")} className="side-menu-option">
          <PersonOutlineOutlinedIcon />
          <p>אזור אישי</p>
        </a>
        <a onClick={() => Navigate("/matches")} className="side-menu-option">
          <FavoriteBorderIcon />
          <p>רשימת התאמות</p>
        </a>
        {/* <a onClick={() => Navigate("/matches")} className="side-menu-option">
          <GradingIcon />
          <p>משוב המשך</p>
        </a> */}
        <a
          onClick={() => Navigate("/recommendations")}
          className="side-menu-option"
        >
          <StarBorderRoundedIcon />
          <p>המלצות</p>
        </a>
        <a
          onClick={() => Navigate("/updatePreferences")}
          className="side-menu-option"
        >
          <ModeEditOutlineOutlinedIcon color="white" />
          <p>עריכת העדפות</p>
        </a>
        <a onClick={() => Navigate("/location")} className="side-menu-option">
          <LocationOnOutlinedIcon color="white" />
          <p>עדכון מיקום</p>
        </a>
      </div>
      {/* <div className="footer-side-menu">
        <a className="side-menu-option">
          <WavingHandOutlinedIcon color="white" onClick={() => Logout()} />
          <p>התנתקות</p>
        </a>
      </div> */}
    </div>
  );
}

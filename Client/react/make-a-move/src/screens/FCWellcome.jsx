import React from "react";
import FCCustomBtn from "../components/FCCustomBtn";
import { useNavigate } from "react-router-dom";
import logo from "../assets/images/Logo.png";
import FCUpload from "../components/FCUpload";
import FCGetImage from "../components/FCGetImage";
import { makeAmoveUserServer } from "../services";
const FCWellcome = () => {
  const navigate = useNavigate();

  return (
    <span>
      <img src={"." + logo} className="logo" />
      {/* <img
        src={"https://proj.ruppin.ac.il/cgroup52/uploadedFiles/Matan.jpg"}
        className="logo"
      /> */}
      <p style={{ color: "white" }}>Location based dating app</p>
      <FCCustomBtn title={"התחברות"} onClick={() => navigate("/signin")} />
      <FCCustomBtn title={"הרשמה"} onClick={() => navigate("/signup")} />
    </span>
  );
};

export default FCWellcome;

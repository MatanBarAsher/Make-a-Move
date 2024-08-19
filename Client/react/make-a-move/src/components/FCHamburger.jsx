import React from "react";
import MenuIcon from "@mui/icons-material/Menu";
import { useNavigate } from "react-router-dom";

export default function FCHamburger() {
  const navigate = useNavigate();

  return (
    <button
      className="hamburger"
      style={{ position: "fixed", top: "10px", left: "10px", zIndex: "1000" }}
      onClick={() => navigate("/sideMenu")}
    >
      <MenuIcon color="#3C0753" style={{ width: "32px", height: "32px" }} />
    </button>
  );
}

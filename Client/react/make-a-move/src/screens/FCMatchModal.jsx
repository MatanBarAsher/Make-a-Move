import React, { useState } from "react";
import FCCustomBtn from "../components/FCCustomBtn";
import FCCustomX from "../components/FCCustomX";
import CloseIcon from "@mui/icons-material/Close";
import { Dialog } from "@mui/material";

export default function FCMatchModal({ details, onClose, open }) {
  return (
    <Dialog open={open} sx={{ backgroundColor: "none" }}>
      <div className="match-modal">
        <CloseIcon className="match-modal-x" onClick={onClose} />
        <h2>יש לנו MATCH!</h2>
        <img
          //   src="https://proj.ruppin.ac.il/cgroup52/test2/tar1/images/Matan.jpg"
          src={`${import.meta.env.VITE_SERVER_IMAGE_SRC_URL}${
            details.image[0]
          }`}
          alt=""
        />
        <FCCustomBtn title={"צפייה בפרופיל"} onClick={onClose} />
        <div className="match-text-container">
          <div className="match-text" dir="ltr">
            MAKE a MOVE!
          </div>
          {/* <div className="match-text-decoration" dir="ltr">
          MAKE a MOVE!
        </div> */}
        </div>
      </div>
    </Dialog>
  );
}

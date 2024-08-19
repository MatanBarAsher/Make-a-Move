import React from "react";
import EditIcon from "@mui/icons-material/Edit";

export default function FCCustomEdit(color) {
  return (
    <>
      <p
        style={{
          fontSize: 16,
          color: "white",
          position: "absolute",
          margin: 0,
          top: 15,
          left: 25,
        }}
      >
        עריכה
      </p>
      <button
        className="x-btn"
        style={{
          background: "none",
          color: "white",
          fontSize: 16,
          position: "absolute",
          top: 10,
          left: 65,
        }}
      >
        <EditIcon />
      </button>
    </>
  );
}

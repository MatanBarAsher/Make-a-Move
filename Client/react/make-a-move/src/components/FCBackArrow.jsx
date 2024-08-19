import React from "react";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";

export default function FCBackArrow(color) {
  return (
    <button style={{ background: "none" }}>
      <ChevronLeftIcon style={{ color: color }} />
    </button>
  );
}

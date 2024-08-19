import React from "react";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";

export default function FCNextArrow(color) {
  return (
    <button style={{ background: "none" }}>
      <ChevronRightIcon style={{ color: color }} />
    </button>
  );
}

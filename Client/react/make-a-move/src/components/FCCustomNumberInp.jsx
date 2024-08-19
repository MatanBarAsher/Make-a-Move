import React from "react";

export default function FCCustomNumberInp({ ph, min, ...rest }) {
  return (
    <input
      type="number"
      className="text-inp"
      placeholder={ph}
      min={min}
      {...rest}
    ></input>
  );
}

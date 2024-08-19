import React from "react";

export default function FCCustomPhoneInp({ ph, ...rest }) {
  return (
    <input
      type="tel"
      className="text-inp"
      placeholder={ph}
      {...rest}
      style={{ direction: "rtl" }}
    ></input>
  );
}

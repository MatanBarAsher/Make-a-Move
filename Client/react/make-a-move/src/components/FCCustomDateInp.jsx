import React from "react";

export default function FCCustomDateInp({ ph, ...rest }) {
  return (
    <input type="date" className="text-inp" placeholder={ph} {...rest}></input>
  );
}

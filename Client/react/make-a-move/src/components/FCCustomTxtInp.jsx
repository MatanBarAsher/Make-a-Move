import React from "react";

export default function FCCustomTxtInp({ ph, ...rest }) {
  return (
    <input type="text" className="text-inp" placeholder={ph} {...rest}></input>
  );
}

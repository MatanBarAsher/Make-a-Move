import React from "react";

export default function FCCustomMailInp({ ph, ...rest }) {
  return (
    <input type="email" className="text-inp" placeholder={ph} {...rest}></input>
  );
}

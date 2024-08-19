import React from "react";

export default function FCCustomPasswordInp({ ph, ...rest }) {
  return (
    <input type="password" className="text-inp" placeholder={ph} {...rest} />
  );
}

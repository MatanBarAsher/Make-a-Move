import React, { useState } from "react";
import axios from "axios";

export default function FCImageInp({ name, onChange }) {
  const [imageUrl, setImageUrl] = useState(null);

  const handleChange = (e) => {
    const file = e.target.files[0];
    onChange(name, file);
    const reader = new FileReader();
    reader.onload = () => {
      setImageUrl(reader.result);
    };
    if (file) {
      reader.readAsDataURL(file);
    } else {
      setImageUrl(null);
    }
  };

  return (
    <div className="custom-file-input">
      <label
        htmlFor={name}
        className="custom-file-label"
        style={{ backgroundImage: `url(${imageUrl})` }}
      >
        +
      </label>
      <input
        type="file"
        id={name}
        name={name}
        className="input-file"
        accept=".jpg, .jpeg .png"
        onChange={handleChange}
      />
    </div>
  );
}

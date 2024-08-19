import React, { useState } from "react";

export default function FCCustomRangeInp({ min, max, ...rest }) {
  const [values, setValues] = useState({ min: min || 0, max: max || 100 });

  const handleMinChange = (event) => {
    const newMin = parseInt(event.target.value);
    setValues((prevValues) => ({
      min: newMin,
      max: Math.max(newMin, prevValues.max),
    }));
  };

  const handleMaxChange = (event) => {
    const newMax = parseInt(event.target.value);
    setValues((prevValues) => ({
      min: Math.min(newMax, prevValues.min),
      max: newMax,
    }));
  };

  return (
    <div>
      <input
        type="range"
        min={min}
        max={values.max}
        value={values.min}
        onChange={handleMinChange}
        {...rest}
      />
      <input
        type="range"
        min={values.min}
        max={max}
        value={values.max}
        onChange={handleMaxChange}
        {...rest}
      />
      <div>
        <span> {values.min}</span>
        <span>-{values.max}</span>
      </div>
    </div>
  );
}

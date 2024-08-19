import React, { useEffect, useState } from "react";
import Select from "react-select";
import CreatableSelect from "react-select/creatable";

const AutoCompleteWithAddOption = ({ friends, onValueChange }) => {
  const [options, setOptions] = useState([]);

  useEffect(() => {
    setOptions(friends);
  }, []);

  useEffect(() => {
    console.log(options);
  }, [options]);

  const handleCreateOption = (inputValue) => {
    const newOption = { value: inputValue.toLowerCase(), label: inputValue };
    setOptions((prevOptions) => [...prevOptions, newOption]);
    onValueChange(newOption.value); // Return the new value to the parent
    console.log(newOption);
    console.log(newOption.value);
  };

  const handleChange = (selectedOption) => {
    console.log(selectedOption);
    onValueChange(selectedOption ? selectedOption.value : ""); // Return the selected value to the parent
  };

  return (
    <CreatableSelect
      className="AutoCompleteWithAddOption"
      isClearable
      onChange={handleChange}
      onCreateOption={handleCreateOption}
      options={options}
      placeholder="כאן מקלידים..."
    />
  );
};

export default AutoCompleteWithAddOption;

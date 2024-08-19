import { MenuItem, OutlinedInput, Select } from "@mui/material";

export const FCSelect = ({ options, value, label, onChange }) => {
  return (
    <Select
      id="demo-multiple-name"
      value={value}
      placeholder={label}
      onChange={onChange}
      className="select"
      input={<OutlinedInput label={label} />}
    >
      {options.map((option) => (
        <MenuItem key={option} value={option}>
          {option}
        </MenuItem>
      ))}
    </Select>
  );
};

import {
  Checkbox,
  ListItemText,
  MenuItem,
  OutlinedInput,
  Select,
} from "@mui/material";

export const FCMultiSelect = ({ options, value, label, onChange }) => {
  return (
    <Select
      id="multiple-checkbox"
      className="select"
      multiple
      value={value}
      onChange={onChange}
      placeholder={label}
      input={<OutlinedInput label={label} />}
      renderValue={(selected) => selected.join(", ")}
    >
      {options.map((option) => (
        <MenuItem key={option} value={option}>
          <Checkbox checked={value.indexOf(option) > -1} />
          <ListItemText primary={option} />
        </MenuItem>
      ))}
    </Select>
  );
};

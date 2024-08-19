import React from "react";
import { AlertDialog } from "../../../../components";

export const PreferenceSuccessDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"סגור"}
      title={"העדכון נקלט בהצלחה!"}
      content={"כעת נציע התאמות שתואמות להעדפות החדשות שהגדרת"}
    />
  );
};

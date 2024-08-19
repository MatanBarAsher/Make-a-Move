import React from "react";
import { AlertDialog } from "../../../../components";

export const ErrorDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"הירשם מחדש"}
      title={"הרשמה נכשלה!"}
      content={"לא ניתן להכניס נתונים אלו"}
    />
  );
};

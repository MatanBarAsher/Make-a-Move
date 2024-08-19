import React from "react";
import { AlertDialog } from "../../../../components";

export const PreferenceErrorDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"נסה שוב "}
      title={"העדכון נכשל!"}
      content={"ההעדפות שהגדרת לא עודכנו בפרופיל שלך"}
    />
  );
};

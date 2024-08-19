import React from "react";
import { AlertDialog } from "../../../../components";

export const ProfileSuccessDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      className="match-modal"
      sx={{ backgroundColor: "none", textAline: "center" }}
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={" סגור"}
      title={"העדכון בוצע בהצלחה"}
      content={"כעת הפרטים שלך מעודכנים"}
    />
  );
};

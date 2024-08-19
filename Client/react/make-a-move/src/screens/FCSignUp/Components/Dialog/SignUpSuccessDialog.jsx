import React from "react";
import { AlertDialog } from "../../../../components";

export const SuccessDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"יאללה בואו נתחיל"}
      title={"ההרשמה נקלטה בהצלחה!"}
      content={"כעת נעבור להכנסת תמונות לפרופיל שלך"}
    />
  );
};

import React from "react";
import { AlertDialog } from "../../../../components";

export const SuccessDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"יאללה בואו נתחיל"}
      title={"התחברת בהצלחה"}
      content={
        "ההתחברות שלך לאפליקציית make a move עברה בהצלחה. בעת לחיצה על הכפתור נעבור לבחירת המיקום שלך"
      }
    />
  );
};

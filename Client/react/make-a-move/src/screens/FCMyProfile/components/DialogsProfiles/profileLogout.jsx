import React from "react";
import { AlertDialog } from "../../../../components";

export const ProfileLogout = ({ open, setClose, setCloseCancel }) => {
  return (
    <AlertDialog
      className="match-modal"
      sx={{ backgroundColor: "none", textAline: "center" }}
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={" בצע התנתקות"}
      cancelButtonAction={setCloseCancel}
      cancelButtonText={"בטל"}
      title={"התנתקות מהאפליקציה"}
      content={"בטוח שתרצי/ה להתנתק? באישור לא תהי/ה עוד מחובר/ת ומיקומך ימחק"}
    />
  );
};

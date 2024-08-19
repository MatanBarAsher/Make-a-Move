import React from "react";
import { AlertDialog } from "../../../../components";

export const ProfileDelete = ({ open, setClose, setCloseCancel }) => {
  return (
    <AlertDialog
      className="match-modal"
      sx={{ backgroundColor: "none", textAline: "center" }}
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={" בצע מחיקה"}
      cancelButtonAction={setCloseCancel}
      cancelButtonText={"בטל"}
      title={"מחיקת פרופיל"}
      content={"בטוח שתרצה למחוק? באישור הפרופיל שלך ימחק ולא תוכל להתחבר שוב"}
    />
  );
};

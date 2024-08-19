import React from "react";
import { AlertDialog } from "../../components";

export const FCImagesErrorDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"נסה שוב"}
      title={"אופס..."}
      content={"שם הקובץ לא יכול להכיל תווים מסוג רווח"}
    />
  );
};

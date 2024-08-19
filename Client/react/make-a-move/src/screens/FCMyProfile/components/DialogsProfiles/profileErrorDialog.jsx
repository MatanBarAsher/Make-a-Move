import React from "react";
import { AlertDialog } from "../../../../components";

export const ProfileErrorDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      confirmButtonText={"נסה שוב"}
      title={"העדכון נכשל!"}
      content={"פרטייך לא התעדכנו "}
    />
  );
};

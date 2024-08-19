import React from "react";
import { AlertDialog } from "../../../../components";
import FCCustomBtn from "../../../../components/FCCustomBtn";
import { Padding } from "@mui/icons-material";

export const FeedbackErrorDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      title={"המשוב לא נקלט!"}
      content={"אנא נסה שוב על מנת שנוכל לשפר ולהמליץ לך בעתיד"}
      confirmButtonText={<FCCustomBtn title={"סגור"} />}
    />
  );
};

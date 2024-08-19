import React from "react";
import { AlertDialog } from "../../../../components";
import FCCustomBtn from "../../../../components/FCCustomBtn";
import { Padding } from "@mui/icons-material";

export const FeedbackSuccessDialog = ({ open, setClose }) => {
  return (
    <AlertDialog
      open={open}
      confirmButtonAction={setClose}
      title={"תודה על המשוב!"}
      content={"נשמח להיות איתך בקשר בעתיד על מנת לבדוק אם זה ה - Match!"}
      confirmButtonText={<FCCustomBtn title={"סגור"} />}
    />
  );
};

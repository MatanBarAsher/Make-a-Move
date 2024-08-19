import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";

export const AlertDialog = ({
  open,
  confirmButtonAction,
  cancelButtonAction,
  confirmButtonText,
  cancelButtonText,
  content,
  title,
}) => {
  return (
    <div>
      <Dialog
        open={open}
        onClose={cancelButtonAction}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <div className="dialog">
          <DialogTitle
            style={{ color: "#3c0753", fontFamily: "Heebo" }}
            id="alert-dialog-title"
          >
            {title}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              {content}
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            {cancelButtonAction && cancelButtonText && (
              <Button
                // style={{ color: "black" }}
                className="dialogBtn"
                style={{ color: "#3c0753" }}
                onClick={cancelButtonAction}
              >
                {cancelButtonText}
              </Button>
            )}
            {confirmButtonAction && confirmButtonText && (
              <Button
                style={{ color: "#3c0753" }}
                onClick={confirmButtonAction}
                autoFocus
              >
                {confirmButtonText}
              </Button>
            )}
          </DialogActions>
        </div>
      </Dialog>
    </div>
  );
};

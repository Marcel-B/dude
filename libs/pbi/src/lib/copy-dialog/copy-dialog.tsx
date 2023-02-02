import {
  Box, Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField
} from "@mui/material";
import { getFormattedDateByDate } from "@dude/util";
import { useEffect } from "react";

interface IProps {
  type: string;
  pbi: string;
  open: boolean;
  onClose: () => void;
}

export function CopyDialog({ type, pbi, open, onClose }: IProps) {

  return (
    <>
      <Dialog onClose={onClose} open={open}>
        <DialogTitle>{type}</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Geben Sie bitte hier die Tätigkeit und die Dauer in Stunden ein.
          </DialogContentText>
          <Box component="form" justifyContent="space-between">
            <TextField
              sx={{ mt: 2 }}
              fullWidth
              id="copy-dialog-field"
              rows={3}
              type="text"
              variant="outlined"
              value={pbi}
              onFocus={(e) => {
                e.target.select();
              }}
              label={"Text"} />
          </Box>
        </DialogContent>
        <DialogActions sx={{ mr: 2, mb: 2 }}>
          <Button color="primary" onClick={() => onClose()}>OK</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}

export default CopyDialog;

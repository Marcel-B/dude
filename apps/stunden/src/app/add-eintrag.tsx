import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField
} from "@mui/material";
import React, { useState } from "react";


export interface SimpleDialogProps {
  open: boolean;
  onClose: (result: { taetigkeit: string, dauer: number } | null) => void;
}

export const AddEintrag = (props: SimpleDialogProps) => {
  const [taetigkeit, setTaetigkeit] = useState("");
  const [dauer, setDauer] = useState("");
  const { onClose, open } = props;

  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
    console.log("onChange", e);
    if (e.target.id === "taetigkeit") {
      setTaetigkeit(e.target.value);
    } else if (e.target.id === "dauer") {
      setDauer(e.target.value);
    }
  }

  const handleClose = () => {
    onClose(null);
  };

  const handleSave = () => {
    console.log(taetigkeit);
    console.log(dauer);
    if (dauer && taetigkeit) {
      onClose({ taetigkeit, dauer: Number(dauer) });
    }
    setDauer("");
    setTaetigkeit("");
    onClose(null);
  };

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Neuer Eintrag</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Geben Sie bitte hier die Tätigkeit und die Dauer in Stunden ein.
        </DialogContentText>
        <Box component="form" justifyContent="space-between">
          <TextField
            sx={{ mt: 2 }}
            fullWidth
            id="taetigkeit" type="text" variant="outlined" onChange={onChange}
            label="Tätigkeit" />
          <TextField
            sx={{ mt: 2 }}
            fullWidth id="dauer"
            type="number"
            variant="outlined"
            onChange={onChange}
            label="Dauer" />
        </Box>
      </DialogContent>
      <DialogActions sx={{ mr: 2, mb: 2 }}>
        <Button color="secondary" onClick={handleClose}>Abbrechen</Button>
        <Button onClick={handleSave}>Speichern</Button>
      </DialogActions>
    </Dialog>
  );
};

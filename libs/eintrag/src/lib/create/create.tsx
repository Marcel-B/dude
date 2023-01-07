import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  DialogContentText,
  TextField
} from "@mui/material";
import { useState } from "react";

interface IProps {
  open: boolean;
  datum: string;
  onClose: (result: { taetigkeit: string, dauer: number, datum: string } | null) => void;
}

export function Create({ onClose, open, datum }: IProps) {
  const [taetigkeit, setTaetigkeit] = useState("");
  const [dauer, setDauer] = useState("");

  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
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
    if (dauer && taetigkeit) {
      onClose({ taetigkeit, dauer: Number(dauer), datum });
    }
    setDauer("");
    setTaetigkeit("");
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
        <Button color="secondary" onClick={() => handleClose()}>Abbrechen</Button>
        <Button onClick={() => handleSave()}>Speichern</Button>
      </DialogActions>
    </Dialog>
  );
}

export default Create;

import {
  Box,
  Button,
  Dialog, DialogActions, DialogContent, DialogContentText,
  DialogTitle, FormControl, Grid,
  TextField
} from "@mui/material";
import React, { useState } from "react";


export interface SimpleDialogProps {
  open: boolean;
  //selectedValue: string;
  onClose: (value: string) => void;
}

function useInput(defaultValue: string) {
  const [value, setValue] = useState(defaultValue);

  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
    console.log("onChange", e);
    setValue(e.target.value);
  }

  return {
    value,
    onChange
  };
}

export const AddEintrag = (props: SimpleDialogProps) => {
  const [value, setValue] = useState("");
  const [valueDauer, setValueDauer] = useState("");
  const { onClose,/* selectedValue, */open } = props;
  const inputTaetigkeit = useInput("");
  const inputDauer = useInput("");


  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
    console.log("onChange", e);
    if (e.target.id === "taetigkeit") {
      setValue(e.target.value);
    } else if (e.target.id === "dauer") {
      setValueDauer(e.target.value);
    }
  }

  const handleClose = () => {
    //onClose(selectedValue);
  };

  const handleSave = () => {
    console.log(value);
    console.log(valueDauer);
    if (value && valueDauer) {
      onClose(value);
    }
    setValue("");
    setValueDauer("");
    //onClose(selectedValue);
  };

  const handleListItemClick = (value: string) => {
    onClose(value);
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
            fullWidth id="dauer" type="number" variant="outlined" onChange={onChange} label="Dauer" />
        </Box>
      </DialogContent>
      <DialogActions sx={{ mr: 2, mb: 2 }}>
        <Button color="secondary" onClick={handleClose}>Abbrechen</Button>
        <Button onClick={handleSave}>Speichern</Button>
      </DialogActions>
    </Dialog>
  );
};

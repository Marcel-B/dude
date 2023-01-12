import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  DialogContentText,
  TextField, Autocomplete, debounce, Checkbox, FormGroup, FormControlLabel
} from "@mui/material";
import { useCallback, useState } from "react";
import { useAppSelector } from "@dude/store";

interface IProps {
  open: boolean;
  datum: string;
  onClose: (result: { taetigkeit: string, dauer: number, datum: string, abrechenbar: boolean } | null) => void;
}

export function Create({ onClose, open, datum }: IProps) {
  const [taetigkeit, setTaetigkeit] = useState("");
  const [dauer, setDauer] = useState("");
  const [abrechenbar, setAbrechenbar] = useState(true);
  const { projekte } = useAppSelector(state => state.abrechnung);

  const debouncedTaetigkeit = useCallback(debounce((value: string) => {
    setTaetigkeit(value);
  }, 500), []);

  const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDauer(e.target.value);
  };

  const taetigkeitChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    const taetigkeit = event.target.value;
    debouncedTaetigkeit(taetigkeit);
  };

  const abrechenbarChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    const abrechenbar = event.target.checked;
    setAbrechenbar(abrechenbar);
  };

  const handleClose = () => {
    onClose(null);
  };

  const handleSave = () => {
    if (dauer && taetigkeit) {
      onClose({ taetigkeit, dauer: Number(dauer), datum, abrechenbar });
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
          <Autocomplete
            options={projekte}
            freeSolo
            sx={{ mt: 2 }}
            onSelect={taetigkeitChanged}
            renderInput={(params) => <TextField {...params} onChange={taetigkeitChanged} label="Projekte" />} />
          <TextField
            sx={{ mt: 2 }}
            fullWidth id="dauer"
            type="number"
            variant="outlined"
            onChange={onChange}
            label="Dauer" />
          <FormGroup>
            <FormControlLabel
              control={<Checkbox checked={abrechenbar} onChange={abrechenbarChanged} />}
              label="Abrechenbar" />
          </FormGroup>
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

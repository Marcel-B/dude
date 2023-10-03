import { ChangeEvent, useState } from "react";
import {
  FormControl,
  Grid,
  InputAdornment,
  InputLabel,
  MenuItem,
  Select,
  OutlinedInput,
  SelectChangeEvent
} from "@mui/material";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import SaveIcon from "@mui/icons-material/Save";
import { addPbi, projekteSelectors, useAppDispatch, useAppSelector } from "app-store";
import { LoadingButton } from "@mui/lab";
import { PbiDto } from "client/pbi/index";

export const Create = () => {
  const projekte = useAppSelector(projekteSelectors.selectAll);
  const dispatch = useAppDispatch();

  const [pbi, setPbi] = useState<PbiDto>({name: "", projektId: 0});
  const [loading, setLoading] = useState(false);

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const text = event.target.value;
    const re = new RegExp(/Product Backlog Item /);
    if (text.match(re)) {
      const newTest = text.trim().replace(re, "#").replace(/:/, "");
      setPbi({...pbi, name: newTest});
    } else {
      setPbi({...pbi, name: text});
    }

  };
  const handleSelectChange = (event: SelectChangeEvent) => {
    setPbi({...pbi, projektId: parseInt(event.target.value)});
  };

  const handleSave = () => {
    dispatch(addPbi(pbi));
  };

  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <FormControl fullWidth>
            <InputLabel htmlFor="pbi">Product Backlog Item hier einfügen</InputLabel>
            <OutlinedInput
              label="Product Backlog Item hier einfügen"
              onChange={handleChange}
              value={pbi.name}
              startAdornment={<InputAdornment position="start"><ContentCopyIcon color="action"/></InputAdornment>}/>
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel id="select-label">Projekt</InputLabel>
            <Select
              labelId="select-label"
              id="select"
              value={pbi.projektId.toString()}
              label="Projekte"
              onChange={handleSelectChange}>
              {projekte.map((projekt) => (
                <MenuItem key={projekt.id} value={projekt.id}>{projekt.name}</MenuItem>))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={2}>
          <LoadingButton
            sx={{height: "100%"}}
            onClick={handleSave}
            loading={loading}
            loadingPosition={"start"}
            variant="contained"
            startIcon={
              <SaveIcon fontSize="inherit"/>
            }
            size="large">
            Speichern
          </LoadingButton>
        </Grid>
      </Grid>
    </>
  );
};

export default Create;



import styles from './pbi.module.scss';
import SaveIcon from '@mui/icons-material/Save';
import {
  Button, Divider,
  FormControl,
  Grid,
  IconButton,
  InputAdornment,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
  TextField, Typography
} from "@mui/material";
import React, { ChangeEvent, useState } from "react";

/* eslint-disable-next-line */
export interface PbiProps {
}

export const Pbi = (props: PbiProps) => {
  const [pbi, setPbi] = useState('');
  const [project, setProject] = useState('');
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const text = event.target.value;
    const re = new RegExp(/Product Backlog Item /);
    if (text.match(re)) {
      const newTest = text.trim().replace(re, '#').replace(/:/, '');
      setPbi(newTest);
    } else {
      setPbi(text);
    }

  }
  const handleSelectChange = (event: SelectChangeEvent) => {
    setProject(event.target.value as string);
  }

  const handleSave = () => {
    console.log("Save", pbi, project);
  }

  return (
    <>
      <Typography variant="subtitle2">Product Backlog Item Erfasser&trade;</Typography>
      <Divider light sx={{mb: 3}}/>
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <FormControl fullWidth>
            <InputLabel htmlFor="pbi">Product Backlog Item</InputLabel>
            <OutlinedInput
              label="Product Backlog Item"
              onChange={handleChange}
              value={pbi}
              startAdornment={<InputAdornment position="start">Paste</InputAdornment>}/>
          </FormControl>
        </Grid>
        <Grid item xs={4}>
          <FormControl fullWidth>
            <InputLabel id="select-label">Projekt</InputLabel>
            <Select
              labelId="select-label"
              id="select"
              value={project}
              label="Projekte"
              onChange={handleSelectChange}>
              <MenuItem value="P22002">NCP / DC-Kampagnen-SST</MenuItem>
              <MenuItem value="P22001">NCP / DC-Echtzeitpunktevbg.</MenuItem>
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={1}>
          <IconButton onClick={handleSave}>
            <SaveIcon/>
          </IconButton>
        </Grid>
      </Grid>
    </>
  );
}

export default Pbi;

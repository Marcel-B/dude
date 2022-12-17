import SaveIcon from "@mui/icons-material/Save";
import {
  Divider,
  FormControl,
  Grid,
  IconButton,
  InputAdornment,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
  Typography
} from "@mui/material";
import React, { ChangeEvent, useState } from "react";
import { Project } from "@dude/pbi-shared";

export interface PbiProps {
  projects: Project[];
}

export const PbiCreate = ({ projects }: PbiProps) => {
  const [pbi, setPbi] = useState("");
  const [project, setProject] = useState("");

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const text = event.target.value;
    const re = new RegExp(/Product Backlog Item /);
    if (text.match(re)) {
      const newTest = text.trim().replace(re, "#").replace(/:/, "");
      setPbi(newTest);
    } else {
      setPbi(text);
    }

  };
  const handleSelectChange = (event: SelectChangeEvent) => {
    setProject(event.target.value as string);
  };

  const handleSave = () => {
    console.log("Save", pbi, project);
    fetch("http://localhost:3333/api/pbi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ pbi, project })
    })
      .then(res => res.json())
      .then(data => console.log(data))
      .catch(err => console.error(err));
  };

  return (
    <>
      <Typography variant="subtitle2">Product Backlog Item Erfasser&trade;</Typography>
      <Divider light sx={{ mb: 3 }} />
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <FormControl fullWidth>
            <InputLabel htmlFor="pbi">Product Backlog Item</InputLabel>
            <OutlinedInput
              label="Product Backlog Item"
              onChange={handleChange}
              value={pbi}
              startAdornment={<InputAdornment position="start">Paste</InputAdornment>} />
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
              {projects.map((project) => (
                <MenuItem key={project.projectId} value={project.projectId}>{project.name}</MenuItem>))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={1}>
          <IconButton onClick={handleSave}>
            <SaveIcon />
          </IconButton>
        </Grid>
      </Grid>
    </>
  );
};

export default PbiCreate;

import SaveIcon from "@mui/icons-material/Save";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import {
  Divider,
  FormControl,
  Grid,
  InputAdornment,
  InputLabel,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
  Typography
} from "@mui/material";
import React, { ChangeEvent, useState } from "react";
import { Pbi, Project } from "@dude/pbi-shared";
import { LoadingButton } from "@mui/lab";

export interface PbiProps {
  projects: Project[];
  addPbi: (pbi: Pbi) => void;
}

export const PbiCreate = ({ projects, addPbi }: PbiProps) => {
  const [pbi, setPbi] = useState("");
  const [project, setProject] = useState("");
  const [loading, setLoading] = useState(false);

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
    setLoading(true);
    const newPbi: Pbi = {
      id: 0,
      name: pbi,
      project: project
    };
    fetch("http://localhost:3333/api/pbi", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(newPbi)
    })
      .then(res => res.json() as Promise<Pbi>)
      .then((data) => {
        addPbi({ ...data, project: projects.find((p) => p.projectId === data.project)?.name ?? "n/a" });
        setPbi("");
        setProject("");
      })
      .catch(err => console.error(err))
      .finally(() => setLoading(false));
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
              value={pbi}
              startAdornment={<InputAdornment position="start"><ContentCopyIcon color="action" /></InputAdornment>} />
          </FormControl>
        </Grid>
        <Grid item xs={3}>
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
        <Grid item xs={2}>
          <LoadingButton
            sx={{height: "100%"}}
            onClick={handleSave}
            loading={loading}
            loadingPosition={"start"}
            variant="contained"
            startIcon={
              <SaveIcon fontSize="inherit" />
            }
            size="large">
            Speichern
          </LoadingButton>
        </Grid>
      </Grid>
    </>
  );
};

export default PbiCreate;

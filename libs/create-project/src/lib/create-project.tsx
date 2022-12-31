import styles from "./create-project.module.scss";
import {
  FormControl,
  Grid,
  InputLabel,
  OutlinedInput
} from "@mui/material";
import { Project } from "@dude/pbi-shared";
import { LoadingButton } from "@mui/lab";
import SaveIcon from "@mui/icons-material/Save";
import React from "react";

export interface CreateProjectProps {
  addProject: (project: Project) => void;
  triggerSnackbar: (message: string, severity: "success" | "error" | "info") => void;
}

export const CreateProject = ({ addProject, triggerSnackbar }: CreateProjectProps) => {
  const [project, setProject] = React.useState<Project>({ name: "", projectId: "" });
  const [loading, setLoading] = React.useState(false);
  const handleSave = () => {

    setLoading(true);
    addProject(project);
    setProject({ name: "", projectId: "" });
    triggerSnackbar(`Projekt '${project.name}' angelegt`, "success");
    setLoading(false);
  };

  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <FormControl fullWidth>
            <InputLabel htmlFor="project">Name</InputLabel>
            <OutlinedInput
              label="Name"
              onChange={(e) => setProject({ ...project, name: e.target.value })}
              value={project.name} />
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel htmlFor="project">Projekt ID</InputLabel>
            <OutlinedInput
              label="Project ID"
              onChange={(event) => setProject({ ...project, projectId: event.target.value })}
              value={project.projectId} />
          </FormControl>
        </Grid>
        <Grid item xs={2}>
          <LoadingButton
            sx={{ height: "100%" }}
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

export default CreateProject;

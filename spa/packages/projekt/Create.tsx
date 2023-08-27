import React from "react";
import { Projekt } from "domain/index";
import { addProjekt, useAppDispatch } from "app-store/index";
import { FormControl, Grid, InputLabel, OutlinedInput } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import SaveIcon from "@mui/icons-material/Save";

export interface IProps {
  triggerSnackbar: (message: string, severity: "success" | "error" | "info") => void;
}

export const Create = ({triggerSnackbar}: IProps) => {
  const [projekt, setProjekt] = React.useState<Projekt>({name: "", id: ""});
  const [loading, setLoading] = React.useState(false);
  const dispatch = useAppDispatch();

  const handleSave = () => {
    // const projekt: Projekt = { name: project.name, id: project.projectId };
    dispatch(addProjekt(projekt));
    triggerSnackbar(`Projekt '${projekt.name}' angelegt`, "success");
  };

  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <FormControl fullWidth>
            <InputLabel htmlFor="project">Name</InputLabel>
            <OutlinedInput
              label="Name"
              onChange={(e) => setProjekt({...projekt, name: e.target.value})}
              value={projekt.name}/>
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel htmlFor="project">Projekt ID</InputLabel>
            <OutlinedInput
              label="Project ID"
              onChange={(event) => setProjekt({...projekt, id: event.target.value})}
              value={projekt.id}/>
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

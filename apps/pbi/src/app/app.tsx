// eslint-disable-next-line @typescript-eslint/no-unused-vars
import React, { useEffect, useState } from "react";
import { Pbi, Project } from "@dude/pbi-shared";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary, Alert, Box,
  Container, Divider, Snackbar,
  ThemeProvider,
  Typography
} from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { PbiCreate } from "@dude/pbi";
import { CreateProject, Projects } from "@dude/create-project";
import { PbiList } from "@dude/pbi-list";


export function App() {
  const [projects, setProject] = useState<Project[]>([]);
  const [pbi, setPbi] = useState<Pbi[]>([]);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");
  const [snackbarSeverity, setSnackbarSeverity] = useState<"success" | "error" | "info">("success");

  useEffect(() => {
    if (projects.length === 0) {
      fetch("http://localhost:3333/api/project")
        .then((response) => response.json())
        .then((data) => {
          setProject(data);
        });
    }

    if (projects.length > 0 && pbi.length === 0) {
      fetch("http://localhost:3333/api/pbi")
        .then((response) => response.json())
        .then((data) => {
          const pbis = data.map((pbi: Pbi) => {
            return {
              ...pbi,
              project: projects.find((p) => p.projectId === pbi.project)?.name ?? "n/a"
            };
          });
          setPbi(pbis);
        });
    }
  }, [projects, setProject]);

  const handleAddPbi = (p: Pbi) => {
    setPbi([...pbi, p]);
    setSnackbarMessage(`P.B.I. '${p.name}' hinzugefügt`);
    setSnackbarSeverity("success");
    setOpenSnackbar(true);
  };

  const handleDeletePbi = (p: Pbi) => {
    setPbi(pbi.filter((pbi: Pbi) => pbi.id !== p.id));
    setSnackbarMessage(`P.B.I. '${p.name}' gelöscht`);
    setSnackbarSeverity("info");
    setOpenSnackbar(true);
  };

  const handleSnackbarClose = () => {
    setOpenSnackbar(false);
  };

  const handleAddProject = (p: Project) => {
    setProject([...projects, p]);
  };

  const triggerSnackbar = (message: string, severity: "success" | "error" | "info") => {
    setSnackbarMessage(message);
    setSnackbarSeverity(severity);
    setOpenSnackbar(true);
  };

  return (
      <Container>
        <Typography variant="h1">pbi-O-mat&trade;</Typography>
        <Divider sx={{ borderBottom: "solid 4px #be2edd " }} />
        <Accordion sx={{ mt: 4 }}>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <Typography variant="h2">Product Backlog Item erfassen</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Box sx={{ p: 2 }}>
              <PbiCreate projects={projects} addPbi={handleAddPbi}></PbiCreate>
            </Box>
          </AccordionDetails>
        </Accordion>
        <Accordion>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <Typography variant="h2">Projekt erfassen</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Box sx={{ p: 2 }}>
              <CreateProject addProject={handleAddProject} triggerSnackbar={triggerSnackbar}></CreateProject>
            </Box>
          </AccordionDetails>
        </Accordion>
        <Accordion>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <Typography variant="h2">Projekte</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Projects triggerSnackbar={triggerSnackbar}></Projects>
          </AccordionDetails>
        </Accordion>
        <Accordion>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <Typography variant="h2">P.B.I. Liste</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <PbiList pbis={pbi} deletePbi={handleDeletePbi} triggerSnackbar={triggerSnackbar}></PbiList>
          </AccordionDetails>
        </Accordion>
        <Snackbar open={openSnackbar} autoHideDuration={4_000} onClose={handleSnackbarClose}>
          <Alert severity={snackbarSeverity}>{snackbarMessage}</Alert>
        </Snackbar>
      </Container>
  );
}

export default App;

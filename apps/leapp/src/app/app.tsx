import "./app.module.scss";
import { PbiCreate } from "@dude/pbi";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import React, { useEffect, useState } from "react";
import { PbiList } from "@dude/pbi-list";
import {
  Accordion, AccordionDetails,
  AccordionSummary,
  Alert,
  Box,
  Container,
  createTheme,
  Divider,
  Snackbar,
  ThemeProvider,
  Typography
} from "@mui/material";
import { Pbi, Project } from "@dude/pbi-shared";
import { CreateProject } from "@dude/create-project";

const theme = createTheme({
  typography: {
    fontFamily: ["Ubuntu"].join(",")
  }
});
theme.palette.primary.main = "#686de0";
theme.palette.primary.dark = "#4834d4";
theme.palette.primary.contrastText = "#dff9fb";
theme.palette.background.default = "#987baa";
theme.palette.background.paper = "#dff9fb";
theme.palette.success.main = "#6ab04c";
theme.palette.error.main = "#eb4d4b";
theme.palette.warning.main = "#be2edd";
theme.palette.grey[50] = "#6ab04c";
theme.palette.text.primary = "#130f40";
theme.palette.text.secondary = "#30336b";
theme.palette.divider = "#be2edd";

theme.typography.h1 = {
  fontSize: "3rem",
  color: "#eb4d4b",
  fontFamily: "Audiowide"
};
theme.typography.h2 = {
  fontSize: "1.2rem",
  color: "#be2edd",
  fontFamily: "Roboto Condensed"
  // fontFamily: "Gloria Hallelujah"
};

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
  }

  const triggerSnackbar = (message: string, severity: "success" | "error" | "info") => {
    setSnackbarMessage(message);
    setSnackbarSeverity(severity);
    setOpenSnackbar(true);
  };

  return (
    <ThemeProvider theme={theme}>
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
    </ThemeProvider>
  );
}

export default App;

import React, { useEffect, useState } from "react";
import { Pbi, Projekt } from "@dude/stunden-domain";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary, Alert,
  Box,
  Container,
  Divider,
  Snackbar,
  Typography
} from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { Create as CreateProjekt, List as ListProjekt } from "@dude/projekt";
import { Create as CreatePbi, List as ListPbi } from "@dude/pbi";

export function App() {
  const [projects, setProject] = useState<Projekt[]>([]);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");
  const [snackbarSeverity, setSnackbarSeverity] = useState<"success" | "error" | "info">("success");

  const handleSnackbarClose = () => {
    setOpenSnackbar(false);
  };

  const handleAddProject = (p: Projekt) => {
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
            <CreatePbi></CreatePbi>
          </Box>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary expandIcon={<ExpandMoreIcon />}>
          <Typography variant="h2">Projekt erfassen</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Box sx={{ p: 2 }}>
            <CreateProjekt triggerSnackbar={triggerSnackbar}></CreateProjekt>
          </Box>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary expandIcon={<ExpandMoreIcon />}>
          <Typography variant="h2">Projekte</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <ListProjekt></ListProjekt>
        </AccordionDetails>
      </Accordion>
      <Accordion>
        <AccordionSummary expandIcon={<ExpandMoreIcon />}>
          <Typography variant="h2">P.B.I. Liste</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <ListPbi triggerSnackbar={triggerSnackbar}></ListPbi>
        </AccordionDetails>
      </Accordion>
      <Snackbar open={openSnackbar} autoHideDuration={4_000} onClose={handleSnackbarClose}>
        <Alert severity={snackbarSeverity}>{snackbarMessage}</Alert>
      </Snackbar>
    </Container>
  );
}

export default App;

import * as React from "react";
import NxWelcome from "./nx-welcome";
import { Link, Route, Routes } from "react-router-dom";
import { AppBar, Box, Button, Container, Divider, Toolbar, Typography } from "@mui/material";

const Pbi = React.lazy(() => import("pbi/Module"));
const Stunden = React.lazy(() => import("stunden/Module"));


export function App() {
  return (
    <React.Suspense fallback={null}>
      <AppBar position="static" sx={{ m: 0, mb: 2 }}>
        <Container maxWidth="xl">
          <Toolbar disableGutters>
            <Box>
              <Button
                component={Link}
                to="/"
                key="/"
                sx={{ my: 2, textDecoration: "none", color: "white" }}
              >
                Startseite
              </Button>
              <Button
                component={Link}
                to="/pbi"
                key="/pbi"
                sx={{ my: 2, textDecoration: "none", color: "whitesmoke" }}
              >
                pbi-O-mat&trade;
              </Button>
              <Button
                component={Link}
                to="/stunden"
                key="/stunden"
                sx={{ my: 2, textDecoration: "none", color: "white" }}
              >
                Stunden
              </Button>
            </Box>
          </Toolbar>
        </Container>
      </AppBar>
      <Routes>
        <Route path="/" element={<NxWelcome title="host" />} />
        <Route path="/pbi" element={<Pbi />} />
        <Route path="/stunden" element={<Stunden />} />
      </Routes>;
    </React.Suspense>
  )
    ;
}

export default App;

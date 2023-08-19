import * as React from "react";
import NxWelcome from "./nx-welcome";
import { Link, Route, Routes } from "react-router-dom";
import { AppBar, Box, Button, Container, Toolbar } from "@mui/material";
import { resetToClipboard, useAppDispatch, useAppSelector } from "@dude/store";
import { useEffect } from "react";

const PbiOMat = React.lazy(() => import("pbi-o-mat/Module"));
const Stunden = React.lazy(() => import("stunden/Module"));
const About = React.lazy(() => import("auth-o-mat/Module"));

export function App() {
  const { toClipboard } = useAppSelector((state) => state.appState);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (toClipboard) {
      navigator.clipboard.writeText(toClipboard).then(() => {
        dispatch(resetToClipboard());
      }).catch((err) => {
        console.error(err);
      });
    }
  }, [toClipboard]);


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
                to="/pbi-o-mat"
                key="/pbi-o-mat"
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
              <Button
                component={Link}
                to="/auth"
                key="/auth"
                sx={{ my: 2, textDecoration: "none", color: "white" }}
              >
                Auth
              </Button>
            </Box>
          </Toolbar>
        </Container>
      </AppBar>
      <Routes>
        <Route path="/" element={<NxWelcome title="host" />} />
        <Route path="/pbi-o-mat" element={<PbiOMat writeToClipboard={toClipboard} />} />
        <Route path="/stunden" element={<Stunden />} />
        <Route path="/auth" element={<About />} />
      </Routes>
    </React.Suspense>
  );
}

export default App;

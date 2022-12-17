import "./app.module.scss";
import { PbiCreate } from "@dude/pbi";
import React, { useEffect, useState } from "react";
import { PbiList } from "@dude/pbi-list";
import { Card, createTheme, Divider, ThemeProvider, Typography } from "@mui/material";
import { Project } from "@dude/pbi-shared";

const theme = createTheme({
  typography: {
    fontFamily: [
      "Ubuntu",
      "sans-serif"
    ].join(",")
  }
});

export function App() {
  const [projects, setProject] = useState<Project[]>([]);

  useEffect(() => {
    fetch("http://localhost:3333/api/project")
      .then((response) => response.json())
      .then((data) => {
        setProject(data);
      });
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <Typography variant="h1">pbi-O-mat</Typography>
      <Divider />
      <Card sx={{ p: 4, mt: 2, mb: 2 }}>
        <Typography variant="h2">Product Backlog Item erfassen</Typography>
        <PbiCreate projects={projects}></PbiCreate>
      </Card>
      <Card sx={{ p: 4 }}>
        <Typography variant="h2">PBI Liste</Typography>
        <Divider />
        <PbiList projects={projects}></PbiList>
      </Card>
    </ThemeProvider>
  );
}

export default App;

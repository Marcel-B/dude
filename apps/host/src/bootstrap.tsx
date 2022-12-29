import React, { StrictMode } from "react";
import * as ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";

import App from "./app/app";
import { Provider } from "react-redux";
import { store } from "@dude/stunden-store";
import { createTheme, ThemeProvider } from "@mui/material";

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
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </ThemeProvider>
    </Provider>
  </StrictMode>
);

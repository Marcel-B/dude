import React, { StrictMode } from "react";
import * as ReactDOM from "react-dom/client";

import App from "./app/app";
import { Provider } from "react-redux";
import { stundenStore } from "@dude/stunden-store";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <StrictMode>
    <Provider store={stundenStore}>
      <App />
    </Provider>
  </StrictMode>
);

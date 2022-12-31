import { WocheView } from "./woche-view";
import React, { useEffect } from "react";
import { Eintrag } from "@dude/stunden-domain";
import { setEintraege, useAppDispatch } from "@dude/stunden-store";
import { apiClient } from "@dude/api-client";

export const App = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    apiClient.stunden.getEintraege().then((result: Eintrag[]) => dispatch(setEintraege(result)));
  }, []);

  return (
    <WocheView titel={"Stunden"} />
  );
};

export default App;

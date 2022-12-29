import { WocheView } from "./woche-view";
import React, { useEffect } from "react";
import { Eintrag } from "@dude/stunden-domain";
import { setEintraege, useAppDispatch } from "@dude/stunden-store";

export const App = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    fetch("http://localhost:3333/api/eintrag")
      .then((response) => response.json())
      .then((data: Eintrag[]) => {
        console.log(data);
        dispatch(setEintraege(data));
      });
  }, []);

  return (
    <WocheView titel={"Stunden"} />
  );
};

export default App;

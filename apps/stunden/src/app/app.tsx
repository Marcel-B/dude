import { WocheView } from "./woche-view";
import React, { useEffect } from "react";
import { setDatum, useAppDispatch } from "@dude/stunden-store";

export const App = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(setDatum(new Date()));
  }, []);

  return (
    <WocheView titel={"Stunden"} />
  );
};

export default App;

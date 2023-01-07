import React from "react";
import { Create, Woche, WocheHeader } from "@dude/eintrag";
import { Box, Divider } from "@mui/material";
import { Eintrag } from "@dude/stunden-domain";
import { addEintrag, setOpenCreate, useAppDispatch, useAppSelector } from "@dude/store";

export const App = () => {
  const { openCreate, selectedDatum } = useAppSelector(state => state.eintrag);
  const dispatch = useAppDispatch();
  const onClose = (result: { taetigkeit: string, dauer: number, datum: string } | null) => {
    if (result) {
      const eintrag: Eintrag = {
        id: 0,
        datum: result.datum,
        text: result?.taetigkeit ?? "",
        stunden: result?.dauer ?? 0,
        abrechenbar: true
      };
      dispatch(addEintrag(eintrag));
    }
    dispatch(setOpenCreate({ openCreate: false, selectedDatum: "" }));
  };
  return (
    <Box sx={{ m: "2rem" }}>
      <WocheHeader></WocheHeader>
      <Divider sx={{ mb: 2 }} />
      <Woche></Woche>
      <Create open={openCreate} onClose={onClose} datum={selectedDatum} />
    </Box>
  );
};

export default App;

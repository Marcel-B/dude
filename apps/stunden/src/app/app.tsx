import React from "react";
import { Create, Woche, WocheHeader, Zusammenfassung } from "@dude/eintrag";
import { Box, Divider, Stack } from "@mui/material";
import { Eintrag } from "@dude/stunden-domain";
import { addEintrag, fetchProjektnamen, setOpenCreate, useAppDispatch, useAppSelector } from "@dude/store";

export const App = () => {
  const { openCreate, selectedDatum } = useAppSelector(state => state.eintrag);
  const dispatch = useAppDispatch();
  const onClose = (result: { taetigkeit: string, dauer: number, datum: string, abrechenbar: boolean } | null) => {
    if (result) {
      const eintrag: Eintrag = {
        id: 0,
        datum: result.datum,
        text: result?.taetigkeit ?? "",
        stunden: result?.dauer ?? 0,
        abrechenbar: result?.abrechenbar ?? false
      };
      dispatch(addEintrag(eintrag));
      dispatch(fetchProjektnamen());
    }
    dispatch(setOpenCreate({ openCreate: false, selectedDatum: "" }));
  };
  return (
    <Box sx={{ m: "2rem" }}>
      <WocheHeader></WocheHeader>
      <Divider sx={{ mb: 2 }} />
      <Stack direction={"row"}>
        <Woche></Woche>
        <Zusammenfassung></Zusammenfassung>
      </Stack>
      <Create open={openCreate} onClose={onClose} datum={selectedDatum} />
    </Box>
  );
};

export default App;

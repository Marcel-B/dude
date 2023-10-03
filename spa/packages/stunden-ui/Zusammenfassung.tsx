import { Autocomplete, Card, debounce, Divider, TextField, Typography } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import {
  fetchAbrechnungJahr,
  fetchAbrechnungKalenderwoche,
  fetchAbrechnungMonat, fetchProjektnamen,
  useAppDispatch,
  useAppSelector
} from "app-store";
import { format } from "date-fns";
import ZusammenfassungItem from "./ZusammenfassungItem";

export function Zusammenfassung() {
  const { monat, jahr, kalenderwoche, projekte } = useAppSelector(state => state.abrechnung);
  const { datum } = useAppSelector(state => state.eintrag);
  const appDispatch = useAppDispatch();

  const [stundensatz, setStundensatz] = useState(1);
  const [kunde, setKunde] = useState("");

  const debouncedKunde = useCallback(debounce((value: string) => {
    setKunde(value);
  }, 500), []);

  const debouncedStundensatz = useCallback(debounce((value: number) => {
    setStundensatz(value);
  }, 500), []);

  const stundensatzChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    const stundensatz = event.target.value;
    debouncedStundensatz(Number(stundensatz));
  };
  const kundeChanged = (event: React.ChangeEvent<HTMLInputElement>) => {
    const kunde = event.target.value;
    debouncedKunde(kunde);
  };

  useEffect(() => {
    const d = new Date(datum);
    appDispatch(
      fetchAbrechnungMonat({
        monat: d.getMonth() + 1,
        jahr: d.getFullYear(),
        text: kunde
      }));

    appDispatch(fetchProjektnamen());

    appDispatch(
      fetchAbrechnungKalenderwoche({
        kalenderwoche: Number(format(d, "w")),
        jahr: d.getFullYear(),
        text: kunde
      }));

    appDispatch(
      fetchAbrechnungJahr({
        jahr: d.getFullYear(),
        text: kunde
      }));

  }, [datum, kunde, stundensatz]);

  return (
    <Card sx={{ p: 2, width: 222 }}>
      <Typography variant="h2">Abrechnung</Typography>
      <Divider />
      <TextField
        label="Stundensatz"
        type="number"
        onChange={stundensatzChanged}
        sx={{ mt: 2, mb: 2, justifyContent: "space-between" }} />
      <Autocomplete
        options={projekte}
        freeSolo
        sx={{mb: 1}}
        onSelect={kundeChanged}
        renderInput={(params) => <TextField {...params} onChange={kundeChanged} label="Projekte" />} />
      <ZusammenfassungItem title={"Woche"} stundensatz={stundensatz} stunden={kalenderwoche} />
      <Divider />
      <ZusammenfassungItem title={"Monat"} stundensatz={stundensatz} stunden={monat} />
      <Divider />
      <ZusammenfassungItem stunden={jahr} stundensatz={stundensatz} title={"Jahr"} />
    </Card>
  );
}

export default Zusammenfassung;

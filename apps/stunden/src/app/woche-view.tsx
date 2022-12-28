import { Container, Divider, Grid, IconButton, Typography } from "@mui/material";
import React, { useEffect } from "react";
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import { addWeeks, lastDayOfWeek, subDays } from "date-fns";
import { eineWoche, Eintrag, Wochentag } from "@dude/stunden-domain";
import { TagItem } from "./tag-item";
import { EintragItem } from "./eintrag-item";
import { useDispatch, useSelector } from "react-redux";
import { RootState, setDatum } from "@dude/stunden-store";

const getDaDay = (date: Date, day: Day) => {
  const sonntag = lastDayOfWeek(date, { weekStartsOn: 1 });
  return subDays(sonntag, day).getDate();
};

interface IProps {
  titel: string;
}

export const WocheView = ({ titel }: IProps) => {
  const [eintraege, setEintrage] = React.useState<Eintrag[]>();
  const dispatch = useDispatch();
  const datum = useSelector((state: RootState) => state.datum.datum);

  useEffect(() => {
    const sonntag = lastDayOfWeek(new Date(), { weekStartsOn: 1 });
    const montag = subDays(sonntag, 6);
    const dienstag = subDays(sonntag, 5);
    const mittwoch = subDays(sonntag, 4);
    const donnerstag = subDays(sonntag, 3);
    const freitag = subDays(sonntag, 2);
    const samstag = subDays(sonntag, 1);
    const eintraege: Eintrag[] = [
      { text: "Buchhaltung", stunden: 2, datum: montag },
      { text: "Kunde 1", stunden: 8, datum: montag },
      { text: "Buchhaltung", stunden: 1.25, datum: montag },
      { text: "Kunde 1", stunden: 8, datum: dienstag },
      //{ text: "Am Mittwoch", stunden: 8, datum: mittwoch },
      { text: "Kunde 3", stunden: 8, datum: donnerstag },
      { text: "Orga", stunden: 8, datum: freitag },
      { text: "Arbeit", stunden: 8, datum: samstag }
    ];
    setEintrage(eintraege);
  }, []);

  const nextWeek = () => {
    const newDatum = addWeeks(datum, 1);
    dispatch(setDatum(newDatum));
  };

  const lastWeek = () => {
    const newDatum = addWeeks(datum, -1);
    dispatch(setDatum(newDatum));
  };

  return (
    <Container style={{ background: "#b8e994" }}>
      <Typography variant="subtitle2">{titel}!</Typography>
      <Grid
        container
        alignItems={"flex-start"}
        justifyContent={"space-between"}
      >

        <Grid item xs={0}>
          <IconButton onClick={() => lastWeek()}>
            <KeyboardArrowLeftIcon />
          </IconButton>
        </Grid>
        {eineWoche.map((wochentag: Wochentag) => {
          return (
            <Grid item xs={2} key={wochentag.tag}>
              <TagItem wochentag={wochentag} style={{ background: "#82ccdd" }} />
              {eintraege && eintraege.filter(e => e.datum.getDate() === getDaDay(datum, wochentag.tag)).map(e =>
                <EintragItem
                  key={`${e.text}-${e.datum}-${e.stunden}`}
                  text={e.text}
                  style={{ background: "#fad390" }}
                  stunden={e.stunden} />)}
              <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
              <EintragItem
                text="Gesamt"
                style={{ background: "#78e08f" }}
                stunden={eintraege?.filter(e => e.datum.getDate() === getDaDay(datum, wochentag.tag))
                  .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></EintragItem>
            </Grid>
          );
        })}
        <Grid item xs={0}>
          <IconButton onClick={() => nextWeek()}>
            <KeyboardArrowRightIcon />
          </IconButton>
        </Grid>
      </Grid>
    </Container>
  );
};

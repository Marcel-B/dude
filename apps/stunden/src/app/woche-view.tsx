import { Container, Divider, Grid, Typography } from "@mui/material";
import React, { useEffect } from "react"; // a plugin!
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import { lastDayOfWeek, subDays } from "date-fns";
import { eineWoche, Eintrag, Wochentag } from "@dude/stunden-domain";
import { TagItem } from "./tag-item";
import { EintragItem } from "./eintrag-item";

const getDaDay = (date: Date, day: Day) => {
  const sonntag = lastDayOfWeek(date, { weekStartsOn: 1 });
  return subDays(sonntag, day).getDate();
};

export const WocheView = ({ title }: { title: string }) => {
  const [date, setDate] = React.useState(new Date());
  const [eintraege, setEintrage] = React.useState<Eintrag[]>();

  useEffect(() => {
    setDate(new Date());
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


  return (
    <Container style={{ background: "#b8e994" }}>
      <Typography variant="subtitle2">Welcome to {title}!</Typography>
      <Grid container
            alignItems={"flex-start"}
            justifyContent={"space-between"}
      >

        <Grid item xs={0}>
          <KeyboardArrowLeftIcon />
        </Grid>
        {eineWoche.map((wochentag: Wochentag) => {
          return (
            <Grid item xs={2}>
              <TagItem wochentag={wochentag} style={{ background: "#82ccdd" }} />
              {eintraege && eintraege.filter(e => e.datum.getDate() === getDaDay(date, wochentag.tag)).map(e =>
                <EintragItem
                  text={e.text}
                  style={{ background: "#fad390" }}
                  stunden={e.stunden} />)}
              <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
              <EintragItem
                text="Gesamt"
                style={{ background: "#78e08f" }}
                stunden={eintraege?.filter(e => e.datum.getDate() === getDaDay(date, wochentag.tag))
                  .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></EintragItem>
            </Grid>
          );
        })}
        <Grid item xs={0}>
          <KeyboardArrowRightIcon />
        </Grid>
      </Grid>
    </Container>
  );
};

export default WocheView;

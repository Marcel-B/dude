import { Box, Button, Container, Divider, Grid, IconButton, Paper, Stack, TextField, Typography } from "@mui/material";
import FullCalendar from "@fullcalendar/react"; // must go before plugins
import dayGridPlugin from "@fullcalendar/daygrid";
import React, { useEffect } from "react"; // a plugin!
import AddIcon from "@mui/icons-material/Add";
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import format from "date-fns/format";
import { lastDayOfWeek, subDays } from "date-fns";

const getDaDay = (date: Date, day: Day) => {
  const sonntag = lastDayOfWeek(date, { weekStartsOn: 1 });
  return subDays(sonntag, day).getDate();
};

enum day {
  Montag = 6,
  Dienstag = 5,
  Mittwoch = 4,
  Donnerstag = 3,
  Freitag = 2,
  Samstag = 1,
}

class Wochentag {
  constructor(public name: string, public tag: day) {
  }
}

class Woche {
  static readonly Montag = new Wochentag("Montag", day.Montag);
  static readonly Dienstag = new Wochentag("Dienstag", day.Dienstag);
  static readonly Mittwoch = new Wochentag("Mittwoch", day.Mittwoch);
  static readonly Donnerstag = new Wochentag("Donnerstag", day.Donnerstag);
  static readonly Freitag = new Wochentag("Freitag", day.Freitag);
  static readonly Samstag = new Wochentag("Samstag", day.Samstag);
}

interface IProps {
  theDate?: Date;
  wochentag: Wochentag;
  style?: React.CSSProperties;
}

export const EinTag = ({ theDate, wochentag, style }: IProps) => {
  const [date, setDate] = React.useState(new Date());
  useEffect(() => {
    const sonntag = lastDayOfWeek(theDate ?? new Date(), { weekStartsOn: 1 });
    const result = subDays(sonntag, wochentag.tag);
    setDate(result);

  }, []);
  return (
    <>
      <Paper sx={{ p: 1 }} style={style}>
        <Typography variant="body2">{wochentag.name}</Typography>
        <Divider />
        <Grid container
              direction={"row"}
              justifyContent={"space-between"}
              alignItems={"center"}>
          <Grid item xs={9}>
            <Typography variant="body1">{format(date, "dd.MM.")}</Typography>
          </Grid>
          <Grid item xs={3}>
            <IconButton aria-label="add" color="primary">
              <AddIcon />
            </IconButton>
          </Grid>
        </Grid>
      </Paper>
      <Divider sx={{ mt: 2, mb: 2 }} />
    </>
  );
};

interface IEintragProps {
  text: string;
  stunden: number;
  style?: any;
}

interface Eintrag {
  text: string;
  stunden: number;
  datum: Date;
}

const Eintrag = ({ text, stunden, style }: IEintragProps) => {
  const formatStunden = (stunden: number) => {
    const h = Math.floor(stunden);
    const m = Math.round((stunden - h) * 60);
    return `${h}h ${m}m`;
  };

  return (
    <Paper sx={{ p: 1 }} style={style}>
      <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
        <Typography variant="body1">{text}</Typography>
        <Typography variant="body1">{formatStunden(stunden)}</Typography>
      </Stack>
    </Paper>
  );
};


const eineWoche = [
  Woche.Montag,
  Woche.Dienstag,
  Woche.Mittwoch,
  Woche.Donnerstag,
  Woche.Freitag];

export function NxWelcome({ title }: { title: string }) {
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
        {eineWoche.map((wochentag) => {
          return (
            <Grid item xs={2}>
              <EinTag wochentag={wochentag} style={{ background: "#82ccdd" }} />
              {eintraege && eintraege.filter(e => e.datum.getDate() === getDaDay(date, wochentag.tag)).map(e => <Eintrag
                text={e.text}
                style={{ background: "#fad390" }}
                stunden={e.stunden} />)}
              <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
              <Eintrag text="Gesamt"
                       style={{ background: "#78e08f" }}
                       stunden={eintraege?.filter(e => e.datum.getDate() === getDaDay(date, wochentag.tag))
                         .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></Eintrag>
            </Grid>
          );
        })}
        <Grid item xs={0}>
          <KeyboardArrowRightIcon />
        </Grid>
      </Grid>
    </Container>
  );
}

export default NxWelcome;

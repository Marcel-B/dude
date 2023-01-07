import { Divider, Grid, IconButton } from "@mui/material";
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import { eineWoche, Eintrag, Tag, Wochentag } from "@dude/stunden-domain";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import React, { useEffect } from "react";
import { eintragSelectors, fetchEintraege, RootState, setDatum, useAppDispatch, useAppSelector } from "@dude/store";
import EintragItemHeader from "../eintrag-item-header/eintrag-item-header";
import { getDateByTag, parsedDate, sameDate } from "@dude/util";
import { addWeeks, startOfDay } from "date-fns";
import EintragItem from "../eintrag-item/eintrag-item";
import EintragFooter from "../eintrag-footer/eintrag-footer";

export function Woche() {
  const dispatch = useAppDispatch();
  const { datum } = useAppSelector((state: RootState) => state.eintrag);
  const eintraege = useAppSelector(eintragSelectors.selectAll);

  const getEintragForTag = (eintraege: Eintrag[], tag: Tag) => {
    return eintraege.filter(e =>
      sameDate(parsedDate(e.datum), getDateByTag(datum, tag))
    );
  };

  useEffect(() => {
    dispatch(fetchEintraege());
  }, []);

  const changeWeek = (amount: number) => {
    const date = parsedDate(datum);
    const newDatum = addWeeks(date, amount);
    dispatch(setDatum(startOfDay(newDatum).toISOString()));
  };

  return (
    <Grid
      container
      alignItems={"flex-start"}
      justifyContent={"space-between"}
    >

      <Grid item xs={0}>
        <IconButton onClick={() => changeWeek(-1)}>
          <KeyboardArrowLeftIcon />
        </IconButton>
      </Grid>
      {eineWoche.map((wochentag: Wochentag) => {
        return (
          <Grid item xs={2} key={wochentag.tag}>
            <EintragItemHeader wochentag={wochentag} style={{ background: "#7ed6df" }}></EintragItemHeader>
            <Divider sx={{ mt: 2, mb: 2 }} />
            {eintraege && getEintragForTag(eintraege, wochentag.tag)
              .map(e =>
                <EintragItem key={e.id} text={e.text} stunden={e.stunden}></EintragItem>)}
            <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
            <EintragFooter
              key={wochentag.tag}
              stunden={getEintragForTag(eintraege, wochentag.tag)
                .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></EintragFooter>
          </Grid>
        );
      })}
      <Grid item xs={0}>
        <IconButton onClick={() => changeWeek(1)}>
          <KeyboardArrowRightIcon />
        </IconButton>
      </Grid>
    </Grid>
  );
}

export default Woche;

import { Divider, Grid, IconButton } from "@mui/material";
import KeyboardArrowLeftIcon from "@mui/icons-material/KeyboardArrowLeft";
import { eineWoche, Eintrag, Tag, Wochentag } from "domain/stunden";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import React, { useEffect } from "react";
import { eintragSelectors, fetchEintraege, RootState, setDatum, useAppDispatch, useAppSelector } from "app-store";
import EintragItemHeader from "./EintragItemHeader";
import { changeWeek, getDateByTag, getDateTimeAsISO, parsedDate, sameDate, setCommonTime } from "werkzeug/index";
import EintragItem from "./EintragItem";
import EintragFooter from "./EintragFooter";

export function Woche() {
  const dispatch = useAppDispatch();
  const { datum } = useAppSelector((state: RootState) => state.eintrag);
  const eintraege = useAppSelector(eintragSelectors.selectAll);

  /**
   * Returns Eintraege for Tag
   * @param eintraege Eintraege
   * @param tag Tag
   */
  const getEintragForTag = (eintraege: Eintrag[], tag: Tag) => {
    return eintraege.filter(e =>
      sameDate(parsedDate(e.datum), getDateByTag(datum, tag))
    );
  };

  useEffect(() => {
    dispatch(fetchEintraege());
  }, []);

  const handleChangeWeek = (amount: number) => {
    const date = parsedDate(datum);
    const newDatum = changeWeek(date, amount);
    const commonTime = setCommonTime(newDatum);
    dispatch(setDatum(getDateTimeAsISO(commonTime)));
  };

  return (
    <Grid
      container
      alignItems={"flex-start"}
      justifyContent={"space-between"}
    >

      <Grid item xs={0}>
        <IconButton onClick={() => handleChangeWeek(-1)}>
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
                <EintragItem key={e.id} text={e.text} stunden={e.stunden} abrechenbar={e.abrechenbar}></EintragItem>)}
            <Divider sx={{ mt: 2, mb: 2, borderBottom: "2px solid black" }} />
            <EintragFooter
              key={wochentag.tag}
              stunden={getEintragForTag(eintraege, wochentag.tag)
                .reduce((acc, e) => acc + e.stunden, 0) ?? 0}></EintragFooter>
          </Grid>
        );
      })}
      <Grid item xs={0}>
        <IconButton onClick={() => handleChangeWeek(1)}>
          <KeyboardArrowRightIcon />
        </IconButton>
      </Grid>
    </Grid>
  );
}

export default Woche;

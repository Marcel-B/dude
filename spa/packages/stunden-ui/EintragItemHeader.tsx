import { Divider, Grid, IconButton, Paper, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { Wochentag } from "domain/stunden";
import { setOpenCreate, useAppDispatch, useAppSelector } from "app-store";
import { getDateByTagISO, getFormattedDate } from "werkzeug/index";
import React, { Fragment } from "react";

interface IProps {
  wochentag: Wochentag;
  style?: React.CSSProperties;
}

export function EintragItemHeader({ style, wochentag }: IProps) {
  const { datum } = useAppSelector(state => state.eintrag);
  const dispatch = useAppDispatch();

  return (
    <Fragment>
      <Paper sx={{ p: 1 }} style={style}>
        <Typography variant="body2">{wochentag.name}</Typography>
        <Divider />
        <Grid container
              justifyContent={"space-between"}
              alignItems={"center"}
              direction="row">
          <Grid item xs={9}>
            <Typography variant="body1">{getFormattedDate(wochentag, datum)}</Typography>
          </Grid>
          <Grid item xs={3}>
            <IconButton onClick={() => dispatch(setOpenCreate({
              openCreate: true,
              selectedDatum: getDateByTagISO(datum, wochentag.tag)
            }))} aria-label="add" color="primary">
              <AddIcon />
            </IconButton>
          </Grid>
        </Grid>
      </Paper>
    </Fragment>
  );
}

export default EintragItemHeader;

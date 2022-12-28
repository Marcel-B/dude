import React, { Fragment, useEffect } from "react";
import { lastDayOfWeek, subDays } from "date-fns";
import { Divider, Grid, IconButton, Paper, Typography } from "@mui/material";
import format from "date-fns/format";
import AddIcon from "@mui/icons-material/Add";
import { Wochentag } from "@dude/stunden-domain";
import { AddEintrag } from "./add-eintrag";
import { RootState, useAppDispatch } from "@dude/stunden-store";
import { useSelector } from "react-redux";

interface IProps {
  wochentag: Wochentag;
  style?: React.CSSProperties;
}

export const TagItem = ({ wochentag, style }: IProps) => {
  const [open, setOpen] = React.useState(false);
  const [date, setDate] = React.useState(new Date());

  //const dispatch = useAppDispatch();
  const datum = useSelector((state: RootState) => state.datum.datum);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = (value: string) => {
    setOpen(false);
    //setSelectedValue(value);
  };

  useEffect(() => {
    const sonntag = lastDayOfWeek(datum ?? new Date(), { weekStartsOn: 1 });
    const result = subDays(sonntag, wochentag.tag);
    setDate(result);
  }, [datum]);

  const addEintrag = () => {
    console.log("addEintrag", date);
    setOpen(true);
  };

  return (
    <Fragment>
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
            <IconButton aria-label="add" color="primary" onClick={addEintrag}>
              <AddIcon />
            </IconButton>
          </Grid>
        </Grid>
      </Paper>
      <Divider sx={{ mt: 2, mb: 2 }} />
      <AddEintrag open={open} onClose={handleClose} />
    </Fragment>
  );
};

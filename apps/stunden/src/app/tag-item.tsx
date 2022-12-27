import React, { useEffect } from "react";
import { lastDayOfWeek, subDays } from "date-fns";
import { Divider, Grid, IconButton, Paper, Typography } from "@mui/material";
import format from "date-fns/format";
import AddIcon from "@mui/icons-material/Add";
import { Wochentag } from "@dude/stunden-domain";

interface IProps {
  theDate?: Date;
  wochentag: Wochentag;
  style?: React.CSSProperties;
}

export const TagItem = ({ theDate, wochentag, style }: IProps) => {
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

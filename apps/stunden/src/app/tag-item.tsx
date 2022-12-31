import React, { Fragment } from "react";
import { lastDayOfWeek, parse, startOfDay, subDays } from "date-fns";
import { Divider, Grid, IconButton, Paper, Typography } from "@mui/material";
import format from "date-fns/format";
import AddIcon from "@mui/icons-material/Add";
import { Eintrag, Wochentag } from "@dude/stunden-domain";
import { AddEintrag } from "./add-eintrag";
import { addEintrag, RootState, useAppDispatch, useAppSelector } from "@dude/store";

interface IProps {
  wochentag: Wochentag;
  style?: React.CSSProperties;
}

export const TagItem = ({ wochentag, style }: IProps) => {
    const [open, setOpen] = React.useState(false);
    const dispatch = useAppDispatch();
    const { datum } = useAppSelector((state: RootState) => state.datum);

    const handleClickOpen = () => {
      setOpen(true);
    };

    const handleClose = (result: { taetigkeit: string, dauer: number } | null) => {
      setOpen(false);
      if (result) {
        console.log(result);
        const eintrag: Eintrag = {
          text: result.taetigkeit,
          stunden: result.dauer,
          datum: format(getTagItemDate(datum), "dd.MM.yyyy"),
          abrechenbar: true
        };
        dispatch(addEintrag(eintrag));

        fetch("http://192.168.2.103:3333/api/eintrag", {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(eintrag)
        }).then(
          (response) => {
            console.log(response);
          }
        ).catch(err => console.log(err));
      }
    };

    const createEintrag = () => {
      //console.log("addEintrag", date);
      setOpen(true);
    };

    const getTagItemDate = (date: string) => {
      const asDate = parse(date, "dd.MM.yyyy", startOfDay(new Date()));
      const sonntag = lastDayOfWeek(asDate ?? startOfDay(new Date()), { weekStartsOn: 1 });
      return subDays(sonntag, wochentag.tag);
    };
    const getFormattedDate = (d: string) => {
      const date = getTagItemDate(d);
      return format(date, "dd.MM.");
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
              <Typography variant="body1">{getFormattedDate(datum)}</Typography>
            </Grid>
            <Grid item xs={3}>
              <IconButton aria-label="add" color="primary" onClick={createEintrag}>
                <AddIcon />
              </IconButton>
            </Grid>
          </Grid>
        </Paper>
        <Divider sx={{ mt: 2, mb: 2 }} />
        <AddEintrag open={open} onClose={handleClose} />
      </Fragment>
    );
  }
;

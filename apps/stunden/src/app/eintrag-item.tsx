import { Paper, Stack, Typography } from "@mui/material";
import React from "react";

interface IProps {
  text: string;
  stunden: number;
  style?: any;
}

export const EintragItem = ({ text, stunden, style }: IProps) => {
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

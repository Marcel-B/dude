import { formatStunden } from "@dude/util";
import { Paper, Stack, Typography } from "@mui/material";

interface IProps {
  text: string;
  stunden: number;
  style?: React.CSSProperties;
}

export function EintragItem({ text, stunden, style }: IProps) {
  return (
    <Paper sx={{ p: 1, mb: .2 }} style={style}>
      <Stack direction={"row"} justifyContent={"space-between"} alignItems={"center"}>
        <Typography variant="body1">{text}</Typography>
        <Typography variant="body1">{formatStunden(stunden)}</Typography>
      </Stack>
    </Paper>
  );
}

export default EintragItem;

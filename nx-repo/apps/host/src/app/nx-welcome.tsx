import { Box, Container, Typography } from "@mui/material";
import image from "../assets/octocat.png";

export function NxWelcome({ title }: { title: string }) {
  return (
    <>
      <Container>
        <Box sx={{ display: "flex", mt: "calc(100vh / 4)" }} justifyContent="space-around">
          <img
            width={200}
            src={image}
          />
          <Typography sx={{ fontFamily: "ubuntu", mt: 7 }}
                      variant="h1">b-velop Dev
            Portal &lt;/&gt;</Typography>
        </Box>
      </Container>
    </>
  )
}

export default NxWelcome;

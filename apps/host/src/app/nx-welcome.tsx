import { Container, Typography } from "@mui/material";

export function NxWelcome({ title }: { title: string }) {
  return (
    <>
      <Container>
        <Typography sx={{ textAlign: "center", fontFamily: "ubuntu", mt: "calc(100vh / 4)" }}
                    variant="h1">b-velop Dev
          Portal &lt;/&gt;</Typography>
      </Container>
    </>
  );
}

export default NxWelcome;

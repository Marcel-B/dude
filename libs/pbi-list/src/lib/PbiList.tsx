import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import { Grid, IconButton, TextField, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Pbi } from "@dude/pbi-shared";

/* eslint-disable-next-line */
export interface PbiListProps {
}

export const PbiList = (props: PbiListProps) => {
  const [pbiList, setPbiList] = useState<Pbi[]>([]);

  useEffect(() => {
    console.log("PbiList mounted");
    fetch("http://localhost:3333/api/pbi")
      .then((response) => response.json())
      .then((data: Pbi[]) => {
        setPbiList(data.map(i => {
          return {
            ...i,
            description: ""
          };
        }));
      })
      .catch((error) => {
        console.error(error);
      });
    return () => {
      console.log("PbiList unmounted");
    };
  }, []);

  const handleCopy = async (id: number) => {
    const pbi = pbiList.find(p => p.id === id);
    if (pbi) {
      const forClipboard = `${pbi.name} (${pbi.description})`;
      await navigator.clipboard.writeText(forClipboard);
      setPbiList(pbiList.map(p => {
        return p.id === id ? {
          ...p,
          description: ""
        } : p;
      }));
    } else {
      console.error("Pbi not found");
    }
  };

  const handleDescritionChange = (id: number, description: string) => {
    setPbiList(pbiList.map(p => {
      if (p.id === id) {
        return { ...p, description };
      } else {
        return p;
      }
    }));
  };

  return (
    <>
      {pbiList && pbiList.map((pbi) => (
        <Grid container spacing={2} sx={{ mt: 2 }} key={pbi.id}>
          <Grid item xs={8}>
            <Typography variant="body2">{pbi.name}</Typography>
          </Grid>
          <Grid item xs={2}>
            <TextField
              variant="standard"
              label="Beschreibung"
              value={pbi.description}
              onChange={(v) => handleDescritionChange(pbi.id, v.target.value)} />
          </Grid>
          <Grid item xs={2}>
            <IconButton edge="end" aria-label="copy" onClick={() => handleCopy(pbi.id)}>
              <ContentCopyIcon />
            </IconButton>
          </Grid>
        </Grid>
      ))}
    </>
  );
};

export default PbiList;

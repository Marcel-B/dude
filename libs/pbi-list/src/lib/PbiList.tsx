import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import React, { useEffect, useState } from "react";
import { Pbi, Project } from "@dude/pbi-shared";
import { DataGrid, GridActionsCellItem, GridColumns, GridRowParams } from "@mui/x-data-grid";

export interface PbiListProps {
  projects: Project[];
}

export const PbiList = ({ projects }: PbiListProps) => {
  const [rows, setRows] = useState<Pbi[]>([]);

  const cols: GridColumns = [
    { field: "id", headerName: "ID", width: 70 },
    { field: "name", headerName: "P.B.I.", width: 444 },
    { field: "description", headerName: "Beschreibung", editable: true, width: 220 },
    { field: "project", headerName: "Projekt", width: 300 },
    {
      field: "copy",
      type: "actions",
      width: 80,
      getActions: (params: GridRowParams<Pbi>) => [
        <GridActionsCellItem label="Copy" icon={<ContentCopyIcon />} onClick={() => {
          console.log("Copy", params.row, params.row.id);
          const forClipboard = `${params.row.name} (${params.row.description})`;
          navigator.clipboard.writeText(forClipboard).then(() => {
            params.row.description = "";
          });
        }}
        />
      ]
    }
  ];


  useEffect(() => {
    console.log("ProjektbiList mounted");
    fetch("http://localhost:3333/api/pbi")
      .then((response) => response.json())
      .then((data: Pbi[]) => {
        setRows(data.map(i => {
          return {
            id: i.id,
            name: i.name,
            project: projects.find(p => p.projectId === i.project)?.name ?? "",
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
  }, [projects]);

  return (
    <DataGrid
      rows={rows}
      autoHeight
      columns={cols}
      pageSize={5}
      rowsPerPageOptions={[5]}
      disableSelectionOnClick
      experimentalFeatures={{ newEditingApi: true }}
    />
  );
};

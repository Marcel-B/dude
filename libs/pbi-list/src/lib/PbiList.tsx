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
    { field: "name", headerName: "P.B.I.", width: 480 },
    { field: "description", headerName: "Beschreibung", editable: true, width: 300 },
    { field: "project", headerName: "Projekt", width: 240 },
    {
      field: "copy",
      type: "actions",
      width: 40,
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
      initialState={{
        columns: {
          columnVisibilityModel: {
            id: false
          }
        }
      }
      }
      autoHeight
      columns={cols}
      pageSize={10}
      rowsPerPageOptions={[10, 50, 110]}
      disableSelectionOnClick
      experimentalFeatures={{ newEditingApi: true }}
    />
  );
};

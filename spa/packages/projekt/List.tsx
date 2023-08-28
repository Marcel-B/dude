import {
  deleteProjekt,
  fetchProjekte,
  projekteSelectors,
  useAppDispatch,
  useAppSelector
} from "app-store";
import React, { useEffect } from "react";
import { DataGrid, GridActionsCellItem, GridColumns, GridRowParams } from "@mui/x-data-grid";
import DeleteIcon from "@mui/icons-material/Delete";
import { Projekt } from "domain/projekt";

export const List = () => {
  const projekte = useAppSelector(projekteSelectors.selectAll);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchProjekte());
  }, []);

  const cols: GridColumns = [
    { field: "id", headerName: "Projekt ID" },
    { field: "name", headerName: "Projekt" },
    {
      field: "actions",
      type: "actions",
      width: 120,
      getActions: (params: GridRowParams<Projekt>) => [
        <GridActionsCellItem label="Löschen" showInMenu key={params.row.id} icon={<DeleteIcon color="warning" />} onClick={() => {
          dispatch(deleteProjekt(params.row.id.toString()));
        }}
        />
      ]
    }
  ];

  return (
    <DataGrid
      rows={projekte}
      initialState={{
        columns: {
          columnVisibilityModel: {
            id: true
          }
        }
      }}
      autoHeight
      columns={cols}
      pageSize={10}
      rowsPerPageOptions={[10, 50, 110]}
      disableSelectionOnClick
      experimentalFeatures={{ newEditingApi: true }}
    />
  );
};

export default List;

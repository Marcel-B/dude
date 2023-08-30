import {
  deleteProjekt,
  fetchProjekte,
  projekteSelectors,
  useAppDispatch,
  useAppSelector
} from "app-store";
import React, { useEffect } from "react";
import { DataGrid, GridActionsCellItem, GridColDef, GridRowParams } from "@mui/x-data-grid";

export const List = () => {
  const projekte = useAppSelector(projekteSelectors.selectAll);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchProjekte());
  }, []);

  const cols: GridColDef[] = [
    {field: "id", headerName: "Projekt ID"},
    {field: "name", headerName: "Projekt"},
    // {
    //   field: "actions",
    //   type: "actions",
    //   width: 120,
    //   getActions: (params: GridRowParams<Projekt>) => [
    //     <GridActionsCellItem label="Löschen" showInMenu key={params.row.id} icon={<DeleteIcon color="warning" />} onClick={() => {
    //       dispatch(deleteProjekt(params.row.id.toString()));
    //     }}
    //     />
    //   ]
    // }
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
      pageSizeOptions={[10]}
      disableRowSelectionOnClick
    />
  );
};

export default List;

import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Projekt } from "domain";
import { apiClient } from "client";
import { RootState } from "./store";

const projektAdapter = createEntityAdapter<Projekt>({
  selectId: (projekt) => projekt.id!,
  sortComparer: (a, b) => a.name.localeCompare(b.name)
});

export const fetchProjekte = createAsyncThunk("projekt/fetchProjekte", async () => {
  const response = await apiClient.projekte.getProjekte();
  return response;
});

export const addProjekt = createAsyncThunk("projekt/addProjekt", async (projekt: Projekt) => {
  const response = await apiClient.projekte.addProjekt(projekt);
  return response;
});

export const deleteProjekt = createAsyncThunk("projekt/deleteProjekt", async (id: number) => {
  await apiClient.projekte.deleteProjekt(id);
  return id;
});

export const projektSlice = createSlice({
  name: "projekt",
  initialState: projektAdapter.getInitialState(),
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchProjekte.fulfilled, (state, action: PayloadAction<Projekt[]>) => {
      projektAdapter.setAll(state, action.payload);
    });
    builder.addCase(addProjekt.fulfilled, (state, action: PayloadAction<Projekt>) => {
      projektAdapter.addOne(state, action.payload);
    });
    builder.addCase(deleteProjekt.fulfilled, (state, action: PayloadAction<number>) => {
      projektAdapter.removeOne(state, action.payload);
    });
  }
});

// Can create a set of memoized selectors based on the location of this entity state
export const projekteSelectors = projektAdapter.getSelectors(
  (state: RootState) => state.projekt
);

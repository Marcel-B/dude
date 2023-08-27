import { createAsyncThunk, createEntityAdapter, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Eintrag } from "domain/stunden";
import { apiClient } from "client";
import { RootState } from "./store";
import { getTodayAsIso } from "werkzeug";

const eintragAdapter = createEntityAdapter<Eintrag>({
  selectId: (eintrag) => eintrag.id,
  sortComparer: (a, b) => a.datum.localeCompare(b.datum)
});

export const fetchEintraege = createAsyncThunk("eintrag/fetchEintraege", async () => {
  return await apiClient.eintraege.getEintraege();
});

export const addEintrag = createAsyncThunk("eintrag/addEintrag", async (eintrag: Eintrag) => {
  return await apiClient.eintraege.addEintrag(eintrag);
});

export const eintragSlice = createSlice({
  name: "eintrag",
  initialState: eintragAdapter.getInitialState({
    datum: getTodayAsIso(),
    loading: false,
    openCreate: false,
    selectedDatum: ""
  }),
  reducers: {
    setDatum: (state, action: PayloadAction<string>) => {
      state.datum = action.payload;
    },
    setOpenCreate: (state, action: PayloadAction<{ openCreate: boolean, selectedDatum: string }>) => {
      state.openCreate = action.payload.openCreate;
      state.selectedDatum = action.payload.selectedDatum;
    },
    reset: (state) => {
      state.datum = getTodayAsIso();
      state.openCreate = false;
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchEintraege.fulfilled, (state, action: PayloadAction<Eintrag[]>) => {
      eintragAdapter.setAll(state, action.payload);
    });
    builder.addCase(addEintrag.fulfilled, (state, action: PayloadAction<Eintrag>) => {
      eintragAdapter.addOne(state, action.payload);
    });
  }
});

export const eintragSelectors = eintragAdapter.getSelectors(
  (state: RootState) => state.eintrag
);

export const {setDatum, reset, setOpenCreate} = eintragSlice.actions;

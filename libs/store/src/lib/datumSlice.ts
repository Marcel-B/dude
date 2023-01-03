import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import startOfToday from "date-fns/startOfToday";
import { format } from "date-fns";

export interface DatumState {
  datum: string;
  toClipboard: string;
}

const initialState: DatumState = {
  datum: format(startOfToday(), "dd.MM.yyyy")
} as DatumState;

export const datumSlice = createSlice({
  name: "datum",
  initialState,
  reducers: {
    setDatum: (state, action: PayloadAction<string>) => {
      state.datum = action.payload;
    },
    setToClipboard: (state, action: PayloadAction<string>) => {
      state.toClipboard = action.payload;
    },
    resetToClipboard: (state) => {
      state.toClipboard = "";
    }
  }
});


export const { setDatum, setToClipboard, resetToClipboard} = datumSlice.actions;
export const selectDatum = (state: DatumState) => state.datum;

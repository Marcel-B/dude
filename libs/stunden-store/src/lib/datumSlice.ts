import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import startOfToday from "date-fns/startOfToday";
import { format } from "date-fns";

export interface DatumState {
  datum: string;
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
    }
  }
});


export const { setDatum } = datumSlice.actions;
export const selectDatum = (state: DatumState) => state.datum;

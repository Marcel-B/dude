import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Eintrag } from "@dude/stunden-domain";
import "immer";

export interface EintragState {
  eintraege: Eintrag[];
}

const initialState: EintragState = {
  eintraege: []
} as EintragState;

export const eintragSlice = createSlice({
  name: "eintrag",
  initialState,
  reducers: {
    setEintraege: (state, action: PayloadAction<Eintrag[]>) => {
      state.eintraege = action.payload;
    },
    addEintrag: (state, action: PayloadAction<Eintrag>) => {
      state.eintraege = [...state.eintraege, action.payload];
    }
  }
});


export const { addEintrag, setEintraege } = eintragSlice.actions;
export const selectEintraege = (state: EintragState) => state.eintraege;

import { configureStore } from "@reduxjs/toolkit";
import { datumSlice } from "./datumSlice";
import { eintragSlice } from "./eintragSlice";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

export const store = configureStore({
  reducer: {
    datum: datumSlice.reducer,
    eintrag: eintragSlice.reducer
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch: () => AppDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

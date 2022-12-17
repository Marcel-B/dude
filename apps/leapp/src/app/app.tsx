import "./app.module.scss";
import { Cart } from "@dude/cart";
import { PbiCreate } from "@dude/pbi";
import React from "react";
import { SharedUi } from "../../../../dist/libs/shared/ui";
import { PbiList } from "@dude/pbi-list";

export function App() {
  return (
    <>
      <SharedUi></SharedUi>
      <Cart></Cart>
      <PbiCreate></PbiCreate>
      <PbiList></PbiList>
      <div />
    </>
  );
}

export default App;

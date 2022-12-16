import "./app.module.scss";
import { SharedUi } from "@dude/shared/ui";
import { Cart } from "@dude/cart";
import { Pbi } from "@dude/pbi";

export function App() {
  return (
    <>
      <SharedUi></SharedUi>
      <Cart></Cart>
      <Pbi></Pbi>
      <div/>
    </>
  );
}

export default App;

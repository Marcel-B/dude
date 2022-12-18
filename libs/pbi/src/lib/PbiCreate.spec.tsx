import { render } from "@testing-library/react";

import PbiCreate from "./PbiCreate";

describe("Pbi", () => {
  it("should render successfully", () => {
    const { baseElement } = render(<PbiCreate addPbi={() => console.log("foo")} projects={[]} />);
    expect(baseElement).toBeTruthy();
  });
});

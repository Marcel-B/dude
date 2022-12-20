import { render } from "@testing-library/react";

import { PbiList } from "./PbiList";

describe("PbiList", () => {
  it("should render successfully", () => {
    const { baseElement } = render(<PbiList pbis={[]} deletePbi={() => null} triggerSnackbar={() => null} />);
    expect(baseElement).toBeTruthy();
  });
});

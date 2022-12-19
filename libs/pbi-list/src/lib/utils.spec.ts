import { toBranch } from "./utils";

describe("utils", () => {
  it.each([
    { name: "#88016 Hasse nich gesehen", expected: "#88016-Hasse-nich-gesehen" },
    { name: "#88016 Hasse / und / nich / gesehen", expected: "#88016-Hasse-und-nich-gesehen" }
  ])("should $name $expected", ({ name, expected }: { name: string, expected: string }) => {
    // Arrange
    // Act
    const actual = toBranch(name);

    // Assert
    expect(actual).toBe(expected);
  });
});

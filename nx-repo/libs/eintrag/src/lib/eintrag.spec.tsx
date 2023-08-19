import { render } from '@testing-library/react';

import Eintrag from './eintrag';

describe('Eintrag', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<Eintrag />);
    expect(baseElement).toBeTruthy();
  });
});

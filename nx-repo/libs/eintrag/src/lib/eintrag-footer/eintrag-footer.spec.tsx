import { render } from '@testing-library/react';

import EintragFooter from './eintrag-footer';

describe('EintragFooter', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<EintragFooter />);
    expect(baseElement).toBeTruthy();
  });
});

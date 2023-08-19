import { render } from '@testing-library/react';

import EintragItemHeader from './eintrag-item-header';

describe('EintragItemHeader', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<EintragItemHeader />);
    expect(baseElement).toBeTruthy();
  });
});

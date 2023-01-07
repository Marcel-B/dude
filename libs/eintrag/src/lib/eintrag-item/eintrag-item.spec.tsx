import { render } from '@testing-library/react';

import EintragItem from './eintrag-item';

describe('EintragItem', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<EintragItem />);
    expect(baseElement).toBeTruthy();
  });
});

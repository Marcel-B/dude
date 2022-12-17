import { render } from '@testing-library/react';

import PbiCreate from './PbiCreate';

describe('Pbi', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<PbiCreate />);
    expect(baseElement).toBeTruthy();
  });
});

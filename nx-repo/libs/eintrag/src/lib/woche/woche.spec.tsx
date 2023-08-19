import { render } from '@testing-library/react';

import Woche from './woche';

describe('Woche', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<Woche />);
    expect(baseElement).toBeTruthy();
  });
});

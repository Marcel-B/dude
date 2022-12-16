import { render } from '@testing-library/react';

import Pbi from './pbi';

describe('Pbi', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<Pbi />);
    expect(baseElement).toBeTruthy();
  });
});

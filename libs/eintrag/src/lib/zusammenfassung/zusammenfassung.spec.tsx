import { render } from '@testing-library/react';

import Zusammenfassung from './zusammenfassung';

describe('Zusammenfassung', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<Zusammenfassung />);
    expect(baseElement).toBeTruthy();
  });
});

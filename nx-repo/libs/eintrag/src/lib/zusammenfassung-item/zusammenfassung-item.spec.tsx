import { render } from '@testing-library/react';

import ZusammenfassungItem from './zusammenfassung-item';

describe('ZusammenfassungItem', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<ZusammenfassungItem />);
    expect(baseElement).toBeTruthy();
  });
});

import { render } from '@testing-library/react';

import WocheHeader from './woche-header';

describe('WocheHeader', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<WocheHeader />);
    expect(baseElement).toBeTruthy();
  });
});

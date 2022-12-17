import { render } from '@testing-library/react';

import PbiList from './pbi-list';

describe('PbiList', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<PbiList />);
    expect(baseElement).toBeTruthy();
  });
});

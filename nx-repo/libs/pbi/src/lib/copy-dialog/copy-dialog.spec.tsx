import { render } from '@testing-library/react';

import CopyDialog from './copy-dialog';

describe('CopyDialog', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<CopyDialog />);
    expect(baseElement).toBeTruthy();
  });
});

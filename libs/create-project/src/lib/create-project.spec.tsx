import { render } from '@testing-library/react';

import CreateProject from './create-project';

describe('CreateProject', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<CreateProject />);
    expect(baseElement).toBeTruthy();
  });
});

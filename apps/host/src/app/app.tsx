import * as React from 'react';
import NxWelcome from './nx-welcome';
import { Link, Route, Routes } from 'react-router-dom';

const Shop = React.lazy(() => import('shop/Module'));

const Carto = React.lazy(() => import('carto/Module'));

const About = React.lazy(() => import('about/Module'));

export function App() {
  return (
    <React.Suspense fallback={null}>
      <ul>
        <li>
          <Link to="/">Home</Link>
        </li>

        <li>
          <Link to="/shop">Shop</Link>
        </li>

        <li>
          <Link to="/carto">Carto</Link>
        </li>

        <li>
          <Link to="/about">About</Link>
        </li>
      </ul>
      <Routes>
        <Route path="/" element={<NxWelcome title="host" />} />

        <Route path="/shop" element={<Shop />} />

        <Route path="/carto" element={<Carto />} />

        <Route path="/about" element={<About />} />
      </Routes>
    </React.Suspense>
  );
}

export default App;

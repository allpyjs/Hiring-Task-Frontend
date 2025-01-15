import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';

const Navbar = () => {
  const { user, logout } = useContext(AuthContext);

  return (
    <nav>
      <h1>Todo App</h1>
      {user && <button onClick={logout}>Logout</button>}
    </nav>
  );
};

export default Navbar;

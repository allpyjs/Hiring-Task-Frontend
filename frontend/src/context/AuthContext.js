import React, { createContext, useState } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState({ token: null, isAuthenticated: false });

  const login = (token) => {
    setAuth({ token, isAuthenticated: true });
    localStorage.setItem("token", token);
  };

  const logout = () => {
    setAuth({ token: null, isAuthenticated: false });
    localStorage.removeItem("token");
  };

  return (
    <AuthContext.Provider value={{ auth, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

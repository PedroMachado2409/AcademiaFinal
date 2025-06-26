import React, { createContext, useState, useContext, useEffect } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(null);
  const [user, setUser] = useState(null);
  const [loadingAuth, setLoadingAuth] = useState(true);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      setToken(storedToken);
      fetchUserInfo(storedToken);
    }
    setLoadingAuth(false);
  }, []);

  const fetchUserInfo = async (token) => {
    try {
      const response = await fetch('http://localhost:5004/api/login/eu', {
        method: 'GET',
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (response.ok) {
        const data = await response.json();
        setUser(data);
      }
    } catch (error) {
      console.error('Erro ao carregar informações do usuário', error);
    }
  };

  const login = async (loginData) => {
    const response = await fetch('http://localhost:5004/api/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(loginData),
    });

    if (!response.ok) {
      const errorData = await response.text();
      throw new Error(errorData || 'Login falhou');
    }

    const data = await response.json();
    setToken(data.token);
    localStorage.setItem('token', data.token);
    await fetchUserInfo(data.token);
    return data.token;
  };

  const register = async (registerData) => {
    const response = await fetch('http://localhost:5004/api/login/registrar', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(registerData),
    });

    if (!response.ok) {
      const errorData = await response.text();
      throw new Error(errorData || 'Falha ao registrar');
    }

    return await response.json(); 
  };

  const logout = () => {
    localStorage.removeItem('token');
    setToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ token, user, login, logout, register, loadingAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);

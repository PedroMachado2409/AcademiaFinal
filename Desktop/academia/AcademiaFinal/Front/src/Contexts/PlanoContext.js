import React, { createContext, useContext, useState, useEffect } from "react";
import { useAuth } from "./AuthContext";

const PlanoContext = createContext();

export const PlanoProvider = ({ children }) => {
  const { token } = useAuth();
  const [planos, setPlanos] = useState([]);
  const [loading, setLoading] = useState(false);

  const apiUrl = "http://localhost:5004/api/plano";

  const buscarPlanos = async () => {
    if (!token) return;
    setLoading(true);
    try {
      const response = await fetch(apiUrl, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await response.json();
      setPlanos(data);
    } catch (error) {
      console.error("Erro ao buscar planos:", error);
    } finally {
      setLoading(false);
    }
  };

  const cadastrarPlano = async (plano) => {
    if (!token) return;
    await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(plano),
    });
    await buscarPlanos();
  };

  const atualizarPlano = async (id, plano) => {
    if (!token) return;
    await fetch(`${apiUrl}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(plano),
    });
    await buscarPlanos();
  };

  const inativarPlano = async (id) => {
    if (!token) return;
    await fetch(`${apiUrl}/inativar/${id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    await buscarPlanos();
  };

  const ativarPlano = async (id) => {
    if (!token) return;
    await fetch(`${apiUrl}/ativar/${id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    await buscarPlanos();
  };

  useEffect(() => {
    if (token) buscarPlanos();
  }, [token]);

  return (
    <PlanoContext.Provider
      value={{
        planos,
        loading,
        buscarPlanos,
        cadastrarPlano,
        atualizarPlano,
        inativarPlano,
        ativarPlano,
      }}
    >
      {children}
    </PlanoContext.Provider>
  );
};

export const usePlano = () => useContext(PlanoContext);

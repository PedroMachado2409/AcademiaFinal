import React, { createContext, useContext, useState, useEffect } from "react";
import { useAuth } from "./AuthContext"; // se estiver usando autenticação

const EquipamentoContext = createContext();

export const EquipamentoProvider = ({ children }) => {
  const { token } = useAuth(); // opcional
  const [equipamentos, setEquipamentos] = useState([]);
  const [loading, setLoading] = useState(false);

  const apiUrl = "http://localhost:5004/api/equipamento";

  const buscarEquipamentos = async () => {
    if (!token) return; // se usar AuthContext
    setLoading(true);
    try {
      const response = await fetch(apiUrl, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`, // se estiver autenticando
        },
      });
      if (!response.ok) throw new Error("Erro ao buscar equipamentos");
      const data = await response.json();
      setEquipamentos(data);
    } catch (error) {
      console.error("Erro ao buscar equipamentos:", error);
    } finally {
      setLoading(false);
    }
  };

  const cadastrarEquipamento = async (equipamento) => {
    try {
      const response = await fetch(apiUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(equipamento),
      });
      if (!response.ok) throw new Error("Erro ao cadastrar equipamento");
      await buscarEquipamentos();
    } catch (error) {
      console.error("Erro ao cadastrar:", error);
    }
  };

  useEffect(() => {
    if (token) {
      buscarEquipamentos();
    }
  }, [token]);

  return (
    <EquipamentoContext.Provider
      value={{ equipamentos, loading, buscarEquipamentos, cadastrarEquipamento }}
    >
      {children}
    </EquipamentoContext.Provider>
  );
};

export const useEquipamento = () => useContext(EquipamentoContext);

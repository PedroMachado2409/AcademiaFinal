import React, { createContext, useContext, useState, useEffect } from "react";
import { useAuth } from "./AuthContext";

const ClienteContext = createContext();

export const ClienteProvider = ({ children }) => {
  const { token } = useAuth();
  const [clientes, setClientes] = useState([]);
  const [loading, setLoading] = useState(false);

  const apiUrl = "http://localhost:5004/api/cliente"; // ajustar porta e path

  const localizarClientes = async () => {
    setLoading(true);
    try {
      const response = await fetch(apiUrl, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Erro ao buscar clientes");
      }

      const data = await response.json();
      setClientes(data);
    } catch (error) {
      console.error("Erro ao carregar clientes:", error);
    } finally {
      setLoading(false);
    }
  };

  const cadastrarCliente = async (cliente) => {
    try {
      const response = await fetch(apiUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(cliente),
      });

      if (!response.ok) {
        const erro = await response.text();
        throw new Error(erro || "Erro ao cadastrar cliente");
      }

      const novoCliente = await response.json();
      setClientes((prev) => [...prev, novoCliente]);
    } catch (error) {
      console.error("Erro ao cadastrar cliente:", error);
      throw error;
    }
  };

  const atualizarCliente = async (cliente) => {
    try {
      const response = await fetch(`${apiUrl}/${cliente.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(cliente),
      });

      if (!response.ok) {
        const erro = await response.text();
        throw new Error(erro || "Erro ao atualizar cliente");
      }

      const clienteAtualizado = await response.json();

      setClientes((prevClientes) =>
        prevClientes.map((c) => (c.id === clienteAtualizado.id ? clienteAtualizado : c))
      );
    } catch (error) {
      console.error("Erro ao atualizar cliente:", error);
      throw error;
    }
  };

  useEffect(() => {
    if (token) {
      localizarClientes();
    }
  }, [token]);

  return (
    <ClienteContext.Provider
      value={{
        clientes,
        localizarClientes,
        cadastrarCliente,
        atualizarCliente,
        loading,
      }}
    >
      {children}
    </ClienteContext.Provider>
  );
};

export const useClientes = () => useContext(ClienteContext);

import React, { createContext, useContext, useState, useEffect } from "react";

const PlanoClienteContext = createContext();

export function PlanoClienteProvider({ children }) {
  const [planosClientes, setPlanosClientes] = useState([]);

  const baseUrl = "http://localhost:5004/api/PlanoCliente";

  async function carregarPlanosClientes() {
    const res = await fetch(baseUrl);
    if (res.ok) {
      const data = await res.json();
      setPlanosClientes(data);
    } else {
      console.error("Erro ao carregar planos clientes");
    }
  }

async function obterPlanoClientePorId(id) {
  const res = await fetch(`${baseUrl}/${id}`);
  if (res.ok) {
    // Se status 204 (No Content), não tem corpo JSON
    if (res.status === 204) return null;

    // Tentar ler o corpo JSON
    try {
      return await res.json();
    } catch {
      // Corpo vazio ou inválido
      return null;
    }
  }
  return null;
}

  async function adicionarPlanoCliente(plano) {
    const res = await fetch(baseUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(plano),
    });
    if (res.ok) {
      const novoPlano = await res.json();
      setPlanosClientes((old) => [...old, novoPlano]);
      return novoPlano;
    }
    return null;
  }

  async function atualizarPlanoCliente(id, plano) {
    const res = await fetch(`${baseUrl}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(plano),
    });
    if (res.ok) {
      const atualizado = await res.json();
      setPlanosClientes((old) =>
        old.map((p) => (p.id === id ? atualizado : p))
      );
      return atualizado;
    }
    return null;
  }

  async function ativarPlanoCliente(id) {
    const res = await fetch(`${baseUrl}/${id}/ativar`, { method: "PUT" });
    if (res.ok) {
      const ativado = await res.json();
      setPlanosClientes((old) =>
        old.map((p) => (p.id === id ? ativado : p))
      );
      return ativado;
    }
    return null;
  }

  async function inativarPlanoCliente(id) {
    const res = await fetch(`${baseUrl}/${id}/inativar`, { method: "PUT" });
    if (res.ok) {
      const inativado = await res.json();
      setPlanosClientes((old) =>
        old.map((p) => (p.id === id ? inativado : p))
      );
      return inativado;
    }
    return null;
  }

  useEffect(() => {
    carregarPlanosClientes();
  }, []);

  return (
    <PlanoClienteContext.Provider
      value={{
        planosClientes,
        carregarPlanosClientes,
        obterPlanoClientePorId,
        adicionarPlanoCliente,
        atualizarPlanoCliente,
        ativarPlanoCliente,
        inativarPlanoCliente,
      }}
    >
      {children}
    </PlanoClienteContext.Provider>
  );
}

export function usePlanoCliente() {
  const context = useContext(PlanoClienteContext);
  if (!context) {
    throw new Error(
      "usePlanoCliente deve ser usado dentro do PlanoClienteProvider"
    );
  }
  return context;
}

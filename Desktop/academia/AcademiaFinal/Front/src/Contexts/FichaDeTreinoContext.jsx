import { createContext, useContext, useEffect, useState } from 'react';

const FichaDeTreinoContext = createContext();

export const FichaDeTreinoProvider = ({ children }) => {
  const [fichas, setFichas] = useState([]);
  const [carregando, setCarregando] = useState(false);
  const [erro, setErro] = useState(null);

  const API_URL = 'http://localhost:5004/api/FichaDeTreino';

  const listarFichas = async () => {
    setCarregando(true);
    setErro(null);
    try {
      const res = await fetch(API_URL);
      if (!res.ok) throw new Error('Erro ao buscar fichas de treino');
      const data = await res.json();
      setFichas(data);
    } catch (err) {
      setErro(err.message);
    } finally {
      setCarregando(false);
    }
  };

  const obterFichaPorId = async (id) => {
    try {
      const res = await fetch(`${API_URL}/${id}`);
      if (!res.ok) throw new Error('Erro ao buscar ficha pelo ID');
      return await res.json();
    } catch (err) {
      console.error(err);
      return null;
    }
  };

  const criarFicha = async (novaFicha) => {
    try {
      const res = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(novaFicha),
      });
      if (!res.ok) throw new Error('Erro ao criar ficha');
      const criada = await res.json();
      setFichas((prev) => [...prev, criada]);
      return criada;
    } catch (err) {
      console.error(err);
      return null;
    }
  };

  const atualizarFicha = async (id, fichaAtualizada) => {
    try {
      const res = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(fichaAtualizada),
      });
      if (!res.ok) throw new Error('Erro ao atualizar ficha');
      const atualizada = await res.json();
      setFichas((prev) =>
        prev.map((f) => (f.id === id ? atualizada : f))
      );
      return atualizada;
    } catch (err) {
      console.error(err);
      return null;
    }
  };

  // 🔄 Ativar ficha usando novo endpoint
  const ativarFicha = async (id) => {
    try {
      const res = await fetch(`${API_URL}/${id}/ativar`, {
        method: 'PUT'
      });
      if (!res.ok) throw new Error('Erro ao ativar ficha');
      const fichaAtivada = await res.json();
      setFichas((prev) =>
        prev.map((f) => (f.id === id ? fichaAtivada : f))
      );
      return fichaAtivada;
    } catch (err) {
      console.error(err);
      return null;
    }
  };

  // 🔄 Inativar ficha usando novo endpoint
  const inativarFicha = async (id) => {
    try {
      const res = await fetch(`${API_URL}/${id}/desativar`, {
        method: 'PUT'
      });
      if (!res.ok) throw new Error('Erro ao inativar ficha');
      const fichaDesativada = await res.json();
      setFichas((prev) =>
        prev.map((f) => (f.id === id ? fichaDesativada : f))
      );
      return fichaDesativada;
    } catch (err) {
      console.error(err);
      return null;
    }
  };

  useEffect(() => {
    listarFichas();
  }, []);

  return (
    <FichaDeTreinoContext.Provider
      value={{
        fichas,
        carregando,
        erro,
        listarFichas,
        obterFichaPorId,
        criarFicha,
        atualizarFicha,
        ativarFicha,
        inativarFicha,
      }}
    >
      {children}
    </FichaDeTreinoContext.Provider>
  );
};

export const useFichaDeTreino = () => useContext(FichaDeTreinoContext);

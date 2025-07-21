import { createContext, useState, useContext } from 'react';

const RegisterContext = createContext();

export const useRegister = () => useContext(RegisterContext);

export const RegisterProvider = ({ children }) => {
  const [loading, setLoading] = useState(false);

  const register = async (userData) => {
    setLoading(true);
    try {
      const response = await fetch('http://localhost:8080/auth/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData),
      });

      if (response.ok) {
        const message = await response.text(); // Agora estamos usando .text() para obter a resposta como uma string
        return message;  // Retorna a mensagem recebida
      } else {
        throw new Error("Erro ao registrar: " + response.statusText);
      }
    } catch (error) {
      throw new Error(error.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <RegisterContext.Provider value={{ register, loading }}>
      {children}
    </RegisterContext.Provider>
  );
};

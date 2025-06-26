
import { BrowserRouter } from 'react-router-dom';
import { ChakraProvider } from '@chakra-ui/react';
import RoutesApp from './Routes/RoutesApp';
import { AuthProvider } from './Contexts/AuthContext';
import { RegisterProvider } from './Contexts/RegisterContext';
import { ClienteProvider } from './Contexts/ClienteContext';
import { PlanoProvider } from './Contexts/PlanoContext';
import { PlanoClienteProvider } from './Contexts/PlanoClienteContext';

import { EquipamentoProvider } from './Contexts/EquipamentoContext';
import { FichaDeTreinoProvider } from './Contexts/FichaDeTreinoContext';

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>

        <PlanoProvider>
          <ClienteProvider>
            <PlanoClienteProvider>

              <EquipamentoProvider>
                <RegisterProvider>

                  <FichaDeTreinoProvider>
                    <ChakraProvider>
                      <RoutesApp />
                    </ChakraProvider>
                  </FichaDeTreinoProvider>

                </RegisterProvider>
              </EquipamentoProvider>

            </PlanoClienteProvider>
          </ClienteProvider>
        </PlanoProvider>

      </AuthProvider>

    </BrowserRouter >
  )
}
export default App;

import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Box,
  FormControl,
  FormLabel,
  Input,
  Button,
  useColorMode,
  Heading,
  VStack,
  Flex,
  Alert,
  AlertIcon,
  AlertTitle,
  AlertDescription,
} from '@chakra-ui/react';
import { useAuth } from '../Contexts/AuthContext';

const LoginScreen = () => {
  const { colorMode, toggleColorMode } = useColorMode();
  const { login } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e) => {
    e.preventDefault();
    setError(null);

    if (!email || !senha) {
      setError('Por favor, preencha todos os campos.');
      return;
    }

    setLoading(true);
    try {
      await login({ email, senha });
      navigate('/home');
    } catch (err) {
      const fullMessage = err.message || 'Erro ao fazer login';
      const firstLine = fullMessage.split('\n')[0];
      setError(firstLine);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Flex
      minH="100vh"
      justifyContent="center"
      alignItems="center"
      bg={colorMode === 'light' ? 'gray.100' : 'gray.800'}
    >
      <Box
        p={8}
        rounded="md"
        borderWidth="1px"
        boxShadow="lg"
        bg={colorMode === 'light' ? 'white' : 'gray.700'}
        w={['90%', '80%', '50%']}
        maxW="500px"
      >
        <VStack spacing={4}>
          <Heading as="h1" size="lg">Login</Heading>

          {error && (
            <Alert status="error">
              <AlertIcon />
              <AlertTitle>Erro ao entrar</AlertTitle>
              <AlertDescription>{error}</AlertDescription>
            </Alert>
          )}

          <form style={{ width: '100%' }} onSubmit={handleLogin}>
            <FormControl id="email" isRequired>
              <FormLabel fontSize={18}>Email</FormLabel>
              <Input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Insira seu email"
                size="lg"
                variant="flushed"
              />
            </FormControl>

            <FormControl id="senha" isRequired mt={5}>
              <FormLabel fontSize={18}>Senha</FormLabel>
              <Input
                type="password"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
                placeholder="Insira sua senha"
                size="lg"
                variant="flushed"
              />
            </FormControl>

            <Button
              type="submit"
              colorScheme="blue"
              size="lg"
              w="100%"
              mt={6}
              isLoading={loading}
            >
              Entrar
            </Button>
          </form>

          <Button
            onClick={toggleColorMode}
            colorScheme={colorMode === 'light' ? 'blue' : 'gray'}
            variant="link"
          >
            {colorMode === 'light' ? 'Tema Escuro' : 'Tema Claro'}
          </Button>
        </VStack>
      </Box>
    </Flex>
  );
};

export default LoginScreen;

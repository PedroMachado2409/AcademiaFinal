import React, { useState } from 'react';
import {
  Box,
  Flex,
  VStack,
  Heading,
  Text,
  FormControl,
  FormLabel,
  Input,
  Button,
  Alert,
  AlertIcon,
  AlertTitle,
  AlertDescription,
  useColorModeValue,
} from '@chakra-ui/react';
import Sidebar from '../Componentes/Siderbar';
import { useAuth } from '../Contexts/AuthContext';

export default function Cadastro() {
  const { register } = useAuth();

  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [formSubmitted, setFormSubmitted] = useState(false);
  const [error, setError] = useState(false);

  const bgCard = useColorModeValue('white', 'gray.800');
  const bgContainer = useColorModeValue('gray.100', 'gray.900');
  const borderColor = useColorModeValue('gray.200', 'gray.700');

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!nome || !email || !senha) {
      setError(true);
      return;
    }

    try {
      await register({ nome, email, senha });
      setFormSubmitted(true);
      setError(false);
      setNome('');
      setEmail('');
      setSenha('');
    } catch (err) {
      setError(true);
      console.error(err.message);
    }
  };

  return (
    <Flex minH="100vh">
      <Sidebar />

      <Flex flex="1" bg={bgContainer} align="center" justify="center" px={4}>
        <Box
          w="full"
          maxW="500px"
          bg={bgCard}
          borderRadius="2xl"
          boxShadow="xl"
          border="1px solid"
          borderColor={borderColor}
          p={[6, 8, 10]}
        >
          <VStack spacing={6} align="stretch">
            <Box textAlign="center">
              <Heading size="lg">Cadastro de Usuário</Heading>
              <Text color="gray.500" fontSize="md" mt={2}>
                Preencha as informações para cadastrar um novo usuário
              </Text>
            </Box>

            {error && (
              <Alert status="error" rounded="md">
                <AlertIcon />
                <Box>
                  <AlertTitle>Erro!</AlertTitle>
                  <AlertDescription>
                    Preencha todos os campos corretamente.
                  </AlertDescription>
                </Box>
              </Alert>
            )}

            {formSubmitted && (
              <Alert status="success" rounded="md">
                <AlertIcon />
                <Box>
                  <AlertTitle>Sucesso!</AlertTitle>
                  <AlertDescription>
                    Usuário cadastrado com sucesso.
                  </AlertDescription>
                </Box>
              </Alert>
            )}

            <form onSubmit={handleSubmit}>
              <VStack spacing={5}>
                <FormControl isRequired>
                  <FormLabel>Nome</FormLabel>
                  <Input
                    placeholder="Digite o nome"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    size="lg"
                    focusBorderColor="blue.500"
                  />
                </FormControl>

                <FormControl isRequired>
                  <FormLabel>Email</FormLabel>
                  <Input
                    type="email"
                    placeholder="Digite o email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    size="lg"
                    focusBorderColor="blue.500"
                  />
                </FormControl>

                <FormControl isRequired>
                  <FormLabel>Senha</FormLabel>
                  <Input
                    type="password"
                    placeholder="Digite a senha"
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                    size="lg"
                    focusBorderColor="blue.500"
                  />
                </FormControl>

                <Button
                  type="submit"
                  colorScheme="blue"
                  size="lg"
                  w="full"
                  _hover={{ transform: 'scale(1.02)', transition: '0.2s' }}
                >
                  Cadastrar
                </Button>
              </VStack>
            </form>
          </VStack>
        </Box>
      </Flex>
    </Flex>
  );
}

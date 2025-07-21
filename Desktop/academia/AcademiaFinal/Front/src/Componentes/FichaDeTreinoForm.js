import React, { useState } from 'react';
import {
  Box,
  VStack,
  Heading,
  Text,
  FormControl,
  FormLabel,
  Input,
  Select,
  Button,
  useColorModeValue,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  useToast,
} from '@chakra-ui/react';
import { FiPlus } from 'react-icons/fi';
import { useClientes } from '../Contexts/ClienteContext';
import { useAuth } from '../Contexts/AuthContext';
import { useEquipamento } from '../Contexts/EquipamentoContext';
import { useFichaDeTreino } from '../Contexts/FichaDeTreinoContext';

const FichaDeTreinoForm = ({ onClose }) => {
  const { clientes } = useClientes();
  const { user } = useAuth();
  const { criarFicha } = useFichaDeTreino();
  const { equipamentos } = useEquipamento();

  // Controle do modal interno para adicionar equipamento
  const { isOpen, onOpen, onClose: fecharEquipamentoModal } = useDisclosure();

  const [clienteId, setClienteId] = useState('');
  const [tipoTreino, setTipoTreino] = useState('');
  const [itensFicha, setItensFicha] = useState([]);
  const [equipamentoSelecionado, setEquipamentoSelecionado] = useState('');
  const [repeticoes, setRepeticoes] = useState('');
  const [erros, setErros] = useState({});

  const toast = useToast();

  const bgCard = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');

  const validarFormulario = () => {
    const errosTemp = {};
    if (!clienteId) errosTemp.clienteId = 'Selecione um cliente';
    if (!tipoTreino) errosTemp.tipoTreino = 'Informe o tipo de treino';
    if (itensFicha.length === 0) errosTemp.itens = 'Adicione pelo menos um equipamento';
    setErros(errosTemp);
    return Object.keys(errosTemp).length === 0;
  };

  const handleAdicionarEquipamento = () => {
    if (!equipamentoSelecionado || !repeticoes) {
      toast({
        title: 'Erro',
        description: 'Selecione um equipamento e informe as repetições',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
      return;
    }

    setItensFicha((prev) => [
      ...prev,
      {
        equipamentoId: parseInt(equipamentoSelecionado),
        repeticoes: parseInt(repeticoes),
      },
    ]);
    setEquipamentoSelecionado('');
    setRepeticoes('');
    fecharEquipamentoModal();
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validarFormulario()) {
      toast({
        title: 'Erro',
        description: 'Preencha todos os campos obrigatórios',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
      return;
    }

    const fichaDTO = {
      clienteId: parseInt(clienteId),
      usuarioId: user?.id,
      tipoTreino,
      itens: itensFicha,
    };

    try {
      await criarFicha(fichaDTO);
      toast({
        title: 'Sucesso',
        description: 'Ficha de treino cadastrada com sucesso',
        status: 'success',
        duration: 3000,
        isClosable: true,
      });
      onClose(); // fecha o formulário principal (recebido via props)
    } catch (error) {
      toast({
        title: 'Erro',
        description: error.message || 'Erro ao cadastrar ficha',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    }
  };

  return (
    <Box
      w="full"
      maxW="600px"
      bg={bgCard}
      borderRadius="xl"
      boxShadow="lg"
      border="1px solid"
      borderColor={borderColor}
      p={8}
      mx="auto"
    >
      <VStack spacing={6} align="stretch">
        <Box textAlign="center">
          <Heading size="lg">Nova Ficha de Treino</Heading>
          <Text color="gray.500" fontSize="sm">
            Preencha os campos abaixo
          </Text>
        </Box>

        <form onSubmit={handleSubmit}>
          <VStack spacing={5}>
            <FormControl isInvalid={!!erros.clienteId} isRequired>
              <FormLabel>Cliente</FormLabel>
              <Select
                placeholder="Selecione o cliente"
                value={clienteId}
                onChange={(e) => setClienteId(e.target.value)}
              >
                {clientes.map((cliente) => (
                  <option key={cliente.id} value={cliente.id}>
                    {cliente.nome}
                  </option>
                ))}
              </Select>
              {erros.clienteId && (
                <Text color="red.500" fontSize="sm">
                  {erros.clienteId}
                </Text>
              )}
            </FormControl>

            <FormControl>
              <FormLabel>Professor</FormLabel>
              <Input value={user?.nome || '---'} isReadOnly />
            </FormControl>

            <FormControl isInvalid={!!erros.tipoTreino} isRequired>
              <FormLabel>Tipo de Treino</FormLabel>
              <Input
                placeholder="Informe o tipo de treino"
                value={tipoTreino}
                onChange={(e) => setTipoTreino(e.target.value)}
              />
              {erros.tipoTreino && (
                <Text color="red.500" fontSize="sm">
                  {erros.tipoTreino}
                </Text>
              )}
            </FormControl>

            <FormControl isInvalid={!!erros.itens}>
              <FormLabel>Equipamentos</FormLabel>
              {itensFicha.length === 0 ? (
                <Text color="gray.500">Nenhum equipamento adicionado</Text>
              ) : (
                itensFicha.map((item, i) => {
                  const equipamento = equipamentos.find((e) => e.id === item.equipamentoId);
                  return (
                    <Text key={i}>
                      {equipamento?.nome || 'Equipamento'} - {item.repeticoes} repetições
                    </Text>
                  );
                })
              )}
              {erros.itens && (
                <Text color="red.500" fontSize="sm">
                  {erros.itens}
                </Text>
              )}
              <Button
                leftIcon={<FiPlus />}
                colorScheme="blue"
                variant="outline"
                size="sm"
                mt={2}
                onClick={onOpen} // abre modal equipamentos
              >
                Adicionar Equipamento
              </Button>
            </FormControl>

            <Button type="submit" colorScheme="blue" size="lg" w="full">
              Cadastrar Ficha
            </Button>
          </VStack>
        </form>
      </VStack>

      {/* Modal Equipamento */}
      <Modal isOpen={isOpen}  isCentered>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Adicionar Equipamento</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <FormControl mb={4} isRequired>
              <FormLabel>Equipamento</FormLabel>
              <Select
                placeholder="Selecione"
                value={equipamentoSelecionado}
                onChange={(e) => setEquipamentoSelecionado(e.target.value)}
              >
                {equipamentos.map((e) => (
                  <option key={e.id} value={e.id}>
                    {e.nome}
                  </option>
                ))}
              </Select>
            </FormControl>

            <FormControl isRequired>
              <FormLabel>Repetições</FormLabel>
              <Input
                type="number"
                placeholder="Ex: 10"
                value={repeticoes}
                onChange={(e) => setRepeticoes(e.target.value)}
              />
            </FormControl>
          </ModalBody>
          <ModalFooter>
            <Button colorScheme="blue" onClick={handleAdicionarEquipamento}>
              Adicionar
            </Button>
            <Button variant="ghost" ml={2} onClick={fecharEquipamentoModal}>
              Cancelar
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </Box>
  );
};

export default FichaDeTreinoForm;

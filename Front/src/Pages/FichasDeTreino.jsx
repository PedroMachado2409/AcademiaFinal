import React, { useEffect, useState } from "react";
import {
  Box,
  Flex,
  Button,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  IconButton,
  Spinner,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  useDisclosure,
  useColorMode,
  Input,
  InputGroup,
  InputRightElement,
  Text,
} from "@chakra-ui/react";
import { FiPlus, FiEye, FiSlash, FiCheckCircle, FiSearch } from "react-icons/fi"; // <- importei FiSlash e FiCheckCircle
import Sidebar from "../Componentes/Siderbar";
import FichaDeTreinoForm from "../Componentes/FichaDeTreinoForm";
import { useFichaDeTreino } from "../Contexts/FichaDeTreinoContext";
import DetalhesFichaModal from "../Componentes/DetalhesFichaModal";
import { useClientes } from "../Contexts/ClienteContext";

const FichasDeTreino = () => {
  const { fichas, carregando, listarFichas, ativarFicha, inativarFicha } = useFichaDeTreino();
  const { clientes, loading: clientesLoading } = useClientes();
  const { colorMode } = useColorMode();

  const {
    isOpen: isOpenCadastro,
    onOpen: onOpenCadastro,
    onClose: onCloseCadastro,
  } = useDisclosure();

  const {
    isOpen: isOpenDetalhes,
    onOpen: onOpenDetalhes,
    onClose: onCloseDetalhes,
  } = useDisclosure();

  const [fichaSelecionada, setFichaSelecionada] = useState(null);
  const [filtroCliente, setFiltroCliente] = useState("");
  const [fichasFiltradas, setFichasFiltradas] = useState([]);

  useEffect(() => {
    listarFichas();
  }, []);

  useEffect(() => {
    if (!filtroCliente) {
      setFichasFiltradas(fichas);
    } else {
      const filtro = filtroCliente.toLowerCase();
      setFichasFiltradas(
        fichas.filter((f) => {
          const cliente = clientes.find((c) => c.id === f.clienteId);
          return cliente?.nome.toLowerCase().includes(filtro);
        })
      );
    }
  }, [filtroCliente, fichas, clientes]);

  const abrirModalDetalhes = (ficha) => {
    setFichaSelecionada(ficha);
    onOpenDetalhes();
  };

  const obterNomeCliente = (clienteId) => {
    const cliente = clientes.find((c) => c.id === clienteId);
    return cliente ? cliente.nome : clientesLoading ? "Carregando..." : "---";
  };

const handleAtivarInativar = async (ficha) => {
  try {
    let fichaAtualizada = null;
    if (ficha.ativo) {
      fichaAtualizada = await inativarFicha(ficha.id);
    } else {
      fichaAtualizada = await ativarFicha(ficha.id);
    }

    if (fichaAtualizada) {
      setFichasFiltradas((prev) =>
        prev.map((f) => (f.id === fichaAtualizada.id ? fichaAtualizada : f))
      );
  
      fichas.map((f) => (f.id === fichaAtualizada.id ? fichaAtualizada : f));
    }
  } catch (error) {
    console.error("Erro ao atualizar status da ficha:", error);
  }
};

  return (
    <>
      <Sidebar />
      <Flex
        minH="100vh"
        pt={10}
        bg={colorMode === "light" ? "gray.100" : "gray.800"}
        justify="center"
      >
        <Box w="80%" maxW="1000px">
          <Flex justify="space-between" mb={4}>
            <Button leftIcon={<FiPlus />} colorScheme="teal" onClick={onOpenCadastro}>
              Nova Ficha
            </Button>
            <InputGroup maxW="300px">
              <Input
                placeholder="Pesquisar Aluno"
                value={filtroCliente}
                onChange={(e) => setFiltroCliente(e.target.value)}
                size="md"
              />
              <InputRightElement pointerEvents="none" children={<FiSearch color="gray" />} />
            </InputGroup>
          </Flex>

          {carregando ? (
            <Box textAlign="center" mt={10}>
              <Spinner size="xl" />
            </Box>
          ) : (
            <Box
              overflow="auto"
              borderRadius="md"
              borderWidth="1px"
              bg={colorMode === "light" ? "gray.50" : "gray.700"}
              p={4}
            >
              <Table variant="simple" size="sm">
                <Thead>
                  <Tr>
                    <Th>ID</Th>
                    <Th>Cliente</Th>
                    <Th>Tipo de Treino</Th>
                    <Th>Ativo</Th>
                    <Th>Ações</Th>
                  </Tr>
                </Thead>
                <Tbody>
                  {fichasFiltradas.length === 0 ? (
                    <Tr>
                      <Td colSpan={5} textAlign="center">
                        <Text color="gray.500">Nenhuma ficha encontrada.</Text>
                      </Td>
                    </Tr>
                  ) : (
                    fichasFiltradas.map((ficha) => (
                      <Tr key={ficha.id}>
                        <Td>{ficha.id}</Td>
                        <Td>{obterNomeCliente(ficha.clienteId)}</Td>
                        <Td>{ficha.tipoTreino || "---"}</Td>
                        <Td>{ficha.ativo ? "Sim" : "Não"}</Td>
                        <Td>
                          {/* Botão para abrir detalhes */}
                          <IconButton
                            icon={<FiEye />}
                            colorScheme="blue"
                            size="sm"
                            onClick={() => abrirModalDetalhes(ficha)}
                            aria-label="Ver detalhes"
                            mr={2}
                          />
                          {/* Botão para ativar/inativar com ícones diferentes */}
                          <IconButton
                            icon={ficha.ativo ? <FiSlash /> : <FiCheckCircle />}
                            colorScheme={ficha.ativo ? "red" : "green"}
                            size="sm"
                            onClick={() => handleAtivarInativar(ficha)}
                            aria-label={ficha.ativo ? "Inativar ficha" : "Ativar ficha"}
                          />
                        </Td>
                      </Tr>
                    ))
                  )}
                </Tbody>
              </Table>
            </Box>
          )}
        </Box>
      </Flex>

      {/* Modal de Cadastro */}
      <Modal
        isOpen={isOpenCadastro}
        onClose={onCloseCadastro}
        size="xl"
        isCentered
        scrollBehavior="inside"
      >
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Cadastrar Ficha de Treino</ModalHeader>
          <ModalCloseButton />
          <ModalBody p={6}>
            <FichaDeTreinoForm />
          </ModalBody>
        </ModalContent>
      </Modal>

      {/* Modal de Detalhes */}
      {fichaSelecionada && (
        <DetalhesFichaModal
          isOpen={isOpenDetalhes}
          onClose={onCloseDetalhes}
          ficha={fichaSelecionada}
        />
      )}
    </>
  );
};

export default FichasDeTreino;

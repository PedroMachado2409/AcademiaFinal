import React, { useState, useEffect } from "react";
import {
  Box,
  Input,
  IconButton,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Spinner,
  Flex,
  Button,
  Text,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  useDisclosure,
  useColorMode,
} from "@chakra-ui/react";
import {
  FiSearch,
  FiRefreshCw,
  FiArrowLeft,
  FiArrowRight,
  FiEye,
  FiEdit2,
  FiPlusCircle, // para o novo botão
} from "react-icons/fi";

import { useClientes } from "../Contexts/ClienteContext";
import { usePlanoCliente } from "../Contexts/PlanoClienteContext";

import Sidebar from "../Componentes/Siderbar";
import DetalhesCliente from "../Componentes/DetalhesCliente";
import CadastroCliente from "../Componentes/ClienteFormModal";
import { VincularPlanoModal } from "../Componentes/VincularPlanoModal";

const ListaClientes = () => {
  const { clientes, loading, localizarClientes } = useClientes();
  const { colorMode } = useColorMode();
  const { planosCliente, buscarPlanosPorCliente } = usePlanoCliente();

  const [clientesFiltrados, setClientesFiltrados] = useState([]);
  const [clientePesquisado, setClientePesquisado] = useState("");
  const [pagina, setPagina] = useState(1);
  const [clientesPorPagina] = useState(10);

  const { isOpen, onOpen, onClose } = useDisclosure();
  const [clienteSelecionado, setClienteSelecionado] = useState(null);
  const [modoModal, setModoModal] = useState(null); // 'detalhes' | 'editar' | 'vincularPlano' | null

  useEffect(() => {
    setClientesFiltrados(clientes);
  }, [clientes]);

  const pesquisarClientes = () => {
    if (!clientePesquisado) {
      setClientesFiltrados(clientes);
      setPagina(1);
      return;
    }
    const filtrados = clientes.filter((c) =>
      c.nome.toLowerCase().includes(clientePesquisado.toLowerCase())
    );
    setClientesFiltrados(filtrados);
    setPagina(1);
  };

  const proximaPagina = () => {
    if (pagina * clientesPorPagina < clientesFiltrados.length) {
      setPagina(pagina + 1);
    }
  };

  const paginaAnterior = () => {
    if (pagina > 1) {
      setPagina(pagina - 1);
    }
  };

  const ultimoCliente = pagina * clientesPorPagina;
  const primeiroCliente = ultimoCliente - clientesPorPagina;
  const clientesListados = clientesFiltrados.slice(primeiroCliente, ultimoCliente);

  const abrirModalDetalhes = (cliente) => {
    setClienteSelecionado(cliente);
    setModoModal("detalhes");
    onOpen();
  };

  const abrirModalEditar = (cliente) => {
    setClienteSelecionado(cliente);
    setModoModal("editar");
    onOpen();
  };

  const abrirModalCadastro = () => {
    setClienteSelecionado(null);
    setModoModal("editar");
    onOpen();
  };

  const abrirModalVincularPlano = (cliente) => {
    setClienteSelecionado(cliente);
    setModoModal("vincularPlano");
    onOpen();
  };

  const fecharModal = () => {
    setClienteSelecionado(null);
    setModoModal(null);
    onClose();
  };

  return (
    <>
      <Sidebar />
      <Flex
        minH="100vh"
        justifyContent="center"
        pt={10}
        bg={colorMode === "light" ? "gray.100" : "gray.800"}
      >
        <Box w="80%" maxW="1000px">
          <Flex justifyContent="flex-end" mb={2}>
            <Button colorScheme="teal" onClick={abrirModalCadastro}>
              Cadastrar Cliente
            </Button>
          </Flex>

          <Flex mb={4}>
            <Input
              placeholder="Pesquisar Clientes"
              size="lg"
              mr={4}
              flex="1"
              value={clientePesquisado}
              onChange={(e) => setClientePesquisado(e.target.value)}
            />
            <IconButton
              icon={<FiSearch />}
              aria-label="Pesquisar"
              colorScheme="blue"
              onClick={pesquisarClientes}
            />
            <IconButton
              icon={<FiRefreshCw />}
              aria-label="Atualizar"
              colorScheme="green"
              ml={2}
              onClick={localizarClientes}
            />
          </Flex>

          {loading ? (
            <Box textAlign="center" mt={10}>
              <Spinner size="xl" />
            </Box>
          ) : (
            <Box
              overflow="auto"
              borderRadius="md"
              borderWidth="1px"
              bg={colorMode === "light" ? "gray.50" : "gray.600"}
              p={4}
            >
              <Table variant="simple" size="sm">
                <Thead>
                  <Tr>
                    <Th>ID</Th>
                    <Th>Nome</Th>
                    <Th>CPF</Th>
                    <Th>Ações</Th>
                  </Tr>
                </Thead>
                <Tbody>
                  {clientesListados.map((cliente) => (
                    <Tr key={cliente.id}>
                      <Td>{cliente.id}</Td>
                      <Td>{cliente.nome}</Td>
                      <Td>{cliente.cpf}</Td>
                      <Td>
                        <IconButton
                          size="sm"
                          colorScheme="blue"
                          aria-label="Ver Detalhes"
                          icon={<FiEye />}
                          onClick={() => abrirModalDetalhes(cliente)}
                          mr={2}
                        />
                        <IconButton
                          size="sm"
                          colorScheme="green"
                          aria-label="Editar Cliente"
                          icon={<FiEdit2 />}
                          onClick={() => abrirModalEditar(cliente)}
                          mr={2}
                        />
                        <IconButton
                          size="sm"
                          colorScheme="purple"
                          aria-label="Vincular Plano"
                          icon={<FiPlusCircle />}
                          onClick={() => abrirModalVincularPlano(cliente)}
                        />
                      </Td>
                    </Tr>
                  ))}
                </Tbody>
              </Table>

              <Flex mt={4} alignItems="center" justifyContent="space-between">
                <Button
                  onClick={paginaAnterior}
                  isDisabled={pagina === 1}
                  colorScheme="blue"
                >
                  <FiArrowLeft />
                </Button>

                <Text fontWeight="bold" fontSize="lg">
                  Página {pagina}
                </Text>

                <Button
                  onClick={proximaPagina}
                  isDisabled={pagina * clientesPorPagina >= clientesFiltrados.length}
                  colorScheme="blue"
                >
                  <FiArrowRight />
                </Button>
              </Flex>
            </Box>
          )}
        </Box>
      </Flex>

      <Modal
        isOpen={isOpen}
        onClose={fecharModal}
        size="xl"
        isCentered
        scrollBehavior="inside"
      >
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>
            {modoModal === "detalhes" && "Detalhes do Cliente"}
            {(modoModal === "editar" && clienteSelecionado) && "Editar Cliente"}
            {(modoModal === "editar" && !clienteSelecionado) && "Cadastrar Cliente"}
            {modoModal === "vincularPlano" && `Vincular Plano ao Cliente: ${clienteSelecionado?.nome}`}
          </ModalHeader>
          <ModalCloseButton />
          <ModalBody p={6}>
            {modoModal === "detalhes" && clienteSelecionado && (
              <DetalhesCliente cliente={clienteSelecionado} />
            )}
            {modoModal === "editar" && (
              <CadastroCliente cliente={clienteSelecionado} onClose={fecharModal} />
            )}
            {modoModal === "vincularPlano" && clienteSelecionado && (
              <VincularPlanoModal
                clienteId={clienteSelecionado.id}
                onClose={fecharModal}
              />
            )}
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export default ListaClientes;

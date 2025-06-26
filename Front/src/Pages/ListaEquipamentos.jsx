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
  FiEdit2,
} from "react-icons/fi";
import Sidebar from "../Componentes/Siderbar";
import { useEquipamento } from "../Contexts/EquipamentoContext";
import EquipamentoFormModal from "../Componentes/EquipamentoFormModal";

const ListaEquipamentos = () => {
  const { equipamentos, loading, buscarEquipamentos } = useEquipamento();
  const [equipamentosFiltrados, setEquipamentosFiltrados] = useState([]);
  const [equipamentoPesquisado, setEquipamentoPesquisado] = useState("");
  const [pagina, setPagina] = useState(1);
  const [equipamentosPorPagina] = useState(10);
  const { colorMode } = useColorMode();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [equipamentoSelecionado, setEquipamentoSelecionado] = useState(null);

  useEffect(() => {
    setEquipamentosFiltrados(equipamentos);
  }, [equipamentos]);

  const pesquisar = () => {
    if (!equipamentoPesquisado) {
      setEquipamentosFiltrados(equipamentos);
      setPagina(1);
      return;
    }
    const filtrados = equipamentos.filter((e) =>
      e.nome.toLowerCase().includes(equipamentoPesquisado.toLowerCase())
    );
    setEquipamentosFiltrados(filtrados);
    setPagina(1);
  };

  const proximaPagina = () => {
    if (pagina * equipamentosPorPagina < equipamentosFiltrados.length) {
      setPagina(pagina + 1);
    }
  };

  const paginaAnterior = () => {
    if (pagina > 1) {
      setPagina(pagina - 1);
    }
  };

  const abrirModalCadastro = () => {
    setEquipamentoSelecionado(null);
    onOpen();
  };

  const abrirModalEditar = (equipamento) => {
    setEquipamentoSelecionado(equipamento);
    onOpen();
  };

  const fecharModal = () => {
    setEquipamentoSelecionado(null);
    onClose();
  };

  const listados = equipamentosFiltrados.slice(
    (pagina - 1) * equipamentosPorPagina,
    pagina * equipamentosPorPagina
  );

  return (
    <>
      <Sidebar />
      <Flex minH="100vh" justifyContent="center" pt={10} bg={colorMode === "light" ? "gray.100" : "gray.800"}>
        <Box w="80%" maxW="1000px">
          <Flex justifyContent="flex-end" mb={2}>
            <Button colorScheme="teal" onClick={abrirModalCadastro}>Cadastrar Equipamento</Button>
          </Flex>

          <Flex mb={4}>
            <Input
              placeholder="Pesquisar Equipamentos"
              size="lg"
              mr={4}
              flex="1"
              value={equipamentoPesquisado}
              onChange={(e) => setEquipamentoPesquisado(e.target.value)}
            />
            <IconButton icon={<FiSearch />} aria-label="Pesquisar" colorScheme="blue" onClick={pesquisar} />
            <IconButton icon={<FiRefreshCw />} aria-label="Atualizar" colorScheme="green" ml={2} onClick={buscarEquipamentos} />
          </Flex>

          {loading ? (
            <Box textAlign="center" mt={10}><Spinner size="xl" /></Box>
          ) : (
            <Box overflow="auto" borderRadius="md" borderWidth="1px" bg={colorMode === "light" ? "gray.50" : "gray.600"} p={4}>
              <Table variant="simple" size="sm">
                <Thead>
                  <Tr>
                    <Th>ID</Th>
                    <Th>Nome</Th>
                    <Th>Marca</Th>
                    <Th>Peso (kg)</Th>
                    <Th>Ações</Th>
                  </Tr>
                </Thead>
                <Tbody>
                  {listados.map((equipamento) => (
                    <Tr key={equipamento.id}>
                      <Td>{equipamento.id}</Td>
                      <Td>{equipamento.nome}</Td>
                      <Td>{equipamento.marca}</Td>
                      <Td>{equipamento.peso}</Td>
                      <Td>
                        <IconButton size="sm" colorScheme="green" aria-label="Editar" icon={<FiEdit2 />} onClick={() => abrirModalEditar(equipamento)} />
                      </Td>
                    </Tr>
                  ))}
                </Tbody>
              </Table>

              <Flex mt={4} alignItems="center" justifyContent="space-between">
                <Button onClick={paginaAnterior} isDisabled={pagina === 1} colorScheme="blue"><FiArrowLeft /></Button>
                <Text fontWeight="bold" fontSize="lg">Página {pagina}</Text>
                <Button onClick={proximaPagina} isDisabled={pagina * equipamentosPorPagina >= equipamentosFiltrados.length} colorScheme="blue"><FiArrowRight /></Button>
              </Flex>
            </Box>
          )}
        </Box>
      </Flex>

      <Modal isOpen={isOpen} onClose={fecharModal} size="xl" isCentered scrollBehavior="inside">
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>{equipamentoSelecionado ? "Editar Equipamento" : "Cadastrar Equipamento"}</ModalHeader>
          <ModalCloseButton />
          <ModalBody p={6}>
            <EquipamentoFormModal equipamento={equipamentoSelecionado} onClose={fecharModal} />
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export default ListaEquipamentos;

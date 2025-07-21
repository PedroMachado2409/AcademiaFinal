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
  FiEyeOff,
  FiEye,
} from "react-icons/fi";
import { usePlano } from "../Contexts/PlanoContext";
import Sidebar from "../Componentes/Siderbar";
import CadastroPlano from "../Componentes/PlanoFormModal";

const ListaPlanos = () => {
  const {
    planos,
    loading,
    buscarPlanos,
    inativarPlano,
    ativarPlano,
  } = usePlano();

  const [planosFiltrados, setPlanosFiltrados] = useState([]);
  const [planoPesquisado, setPlanoPesquisado] = useState("");
  const [pagina, setPagina] = useState(1);
  const [planosPorPagina] = useState(10);
  const { colorMode } = useColorMode();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [planoSelecionado, setPlanoSelecionado] = useState(null);

  useEffect(() => {
    setPlanosFiltrados(planos);
  }, [planos]);

  const pesquisarPlanos = () => {
    if (!planoPesquisado) {
      setPlanosFiltrados(planos);
      setPagina(1);
      return;
    }
    const filtrados = planos.filter((p) =>
      p.nome.toLowerCase().includes(planoPesquisado.toLowerCase())
    );
    setPlanosFiltrados(filtrados);
    setPagina(1);
  };

  const proximaPagina = () => {
    if (pagina * planosPorPagina < planosFiltrados.length) {
      setPagina(pagina + 1);
    }
  };

  const paginaAnterior = () => {
    if (pagina > 1) {
      setPagina(pagina - 1);
    }
  };

  const ultimoPlano = pagina * planosPorPagina;
  const primeiroPlano = ultimoPlano - planosPorPagina;
  const planosListados = planosFiltrados.slice(primeiroPlano, ultimoPlano);

  const abrirModalEditar = (plano) => {
    setPlanoSelecionado(plano);
    onOpen();
  };

  const abrirModalCadastro = () => {
    setPlanoSelecionado(null);
    onOpen();
  };

  const fecharModal = () => {
    setPlanoSelecionado(null);
    onClose();
  };

  return (
    <>
      <Sidebar />
      <Flex minH="100vh" justifyContent="center" pt={10} bg={colorMode === "light" ? "gray.100" : "gray.800"}>
        <Box w="80%" maxW="1000px">
          <Flex justifyContent="flex-end" mb={2}>
            <Button colorScheme="teal" onClick={abrirModalCadastro}>
              Cadastrar Plano
            </Button>
          </Flex>

          <Flex mb={4}>
            <Input
              placeholder="Pesquisar Planos"
              size="lg"
              mr={4}
              flex="1"
              value={planoPesquisado}
              onChange={(e) => setPlanoPesquisado(e.target.value)}
            />
            <IconButton icon={<FiSearch />} aria-label="Pesquisar" colorScheme="blue" onClick={pesquisarPlanos} />
            <IconButton icon={<FiRefreshCw />} aria-label="Atualizar" colorScheme="green" ml={2} onClick={buscarPlanos} />
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
                    <Th>Valor</Th>
                    <Th>Duração</Th>
                    <Th>Ativo</Th>
                    <Th>Ações</Th>
                  </Tr>
                </Thead>
                <Tbody>
                  {planosListados.map((plano) => (
                    <Tr key={plano.id}>
                      <Td>{plano.id}</Td>
                      <Td>{plano.nome}</Td>
                      <Td>R$ {Number(plano.valor).toFixed(2)}</Td>
                      <Td>{plano.duracaoMeses} meses</Td>
                      <Td>{plano.ativo ? "Sim" : "Não"}</Td>
                      <Td>
                        <IconButton size="sm" colorScheme="green" icon={<FiEdit2 />} mr={2} onClick={() => abrirModalEditar(plano)} />
                        {plano.ativo ? (
                          <IconButton size="sm" colorScheme="red" icon={<FiEyeOff />} onClick={() => inativarPlano(plano.id)} />
                        ) : (
                          <IconButton size="sm" colorScheme="blue" icon={<FiEye />} onClick={() => ativarPlano(plano.id)} />
                        )}
                      </Td>
                    </Tr>
                  ))}
                </Tbody>
              </Table>

              <Flex mt={4} alignItems="center" justifyContent="space-between">
                <Button onClick={paginaAnterior} isDisabled={pagina === 1} colorScheme="blue"><FiArrowLeft /></Button>
                <Text fontWeight="bold" fontSize="lg">Página {pagina}</Text>
                <Button onClick={proximaPagina} isDisabled={pagina * planosPorPagina >= planosFiltrados.length} colorScheme="blue"><FiArrowRight /></Button>
              </Flex>
            </Box>
          )}
        </Box>
      </Flex>

      <Modal isOpen={isOpen} onClose={fecharModal} size="xl" isCentered scrollBehavior="inside">
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>{planoSelecionado ? "Editar Plano" : "Cadastrar Plano"}</ModalHeader>
          <ModalCloseButton />
          <ModalBody p={6}>
            <CadastroPlano plano={planoSelecionado} onClose={fecharModal} />
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export default ListaPlanos;

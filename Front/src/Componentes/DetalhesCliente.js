import React, { useEffect, useState } from "react";
import {
  Box,
  Text,
  Stack,
  Heading,
  SimpleGrid,
  Divider,
  Badge,
  useColorModeValue,
  Spinner,
  Button,
  Select,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  useDisclosure,
  useToast,
  FormControl,
  FormLabel,
  FormErrorMessage,
} from "@chakra-ui/react";
import { usePlanoCliente } from "../Contexts/PlanoClienteContext";
import { usePlano } from "../Contexts/PlanoContext";

const formatarData = (dataISO) => {
  if (!dataISO) return "-";
  const data = new Date(dataISO);
  const dia = String(data.getDate()).padStart(2, "0");
  const mes = String(data.getMonth() + 1).padStart(2, "0");
  const ano = data.getFullYear();
  return `${dia}/${mes}/${ano}`;
};

const DetalhesCliente = ({ cliente }) => {
  const { obterPlanoClientePorId, atualizarPlanoCliente } = usePlanoCliente();
  const { planos, carregando: loadingPlanos } = usePlano();

  const [planoClienteAtual, setPlanoClienteAtual] = useState(null);
  const [loading, setLoading] = useState(false);

  // Modal estado
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [planoSelecionado, setPlanoSelecionado] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const toast = useToast();

  useEffect(() => {
    async function carregarPlano() {
      if (cliente?.id) {
        setLoading(true);
        const plano = await obterPlanoClientePorId(cliente.id);
        setPlanoClienteAtual(plano);
        setLoading(false);
      } else {
        setPlanoClienteAtual(null);
      }
    }
    carregarPlano();
  }, [cliente, obterPlanoClientePorId]);

  // Função para abrir modal atualizar plano
  const abrirModalAtualizar = () => {
    setPlanoSelecionado("");
    onOpen();
  };

  // Função para confirmar atualização do plano
  const handleAtualizarPlano = async () => {
    if (!planoSelecionado) {
      toast({
        title: "Selecione um plano para atualizar.",
        status: "warning",
        duration: 3000,
        isClosable: true,
      });
      return;
    }
    setSubmitting(true);
    try {
      const dto = {
        clienteId: cliente.id,
        planoId: parseInt(planoSelecionado, 10),
        dataInicio: new Date().toISOString(),
        ativo: true,
      };
      const atualizado = await atualizarPlanoCliente(planoClienteAtual.id, dto);
      setPlanoClienteAtual(atualizado);
      toast({
        title: "Plano atualizado com sucesso.",
        status: "success",
        duration: 3000,
        isClosable: true,
      });
      onClose();
    } catch {
      toast({
        title: "Erro ao atualizar plano.",
        status: "error",
        duration: 3000,
        isClosable: true,
      });
    }
    setSubmitting(false);
  };

  const bgBox = useColorModeValue("white", "gray.700");
  const borderColor = useColorModeValue("gray.200", "gray.600");

  if (!cliente) return null;

  return (
    <Box
      bg={bgBox}
      borderWidth="1px"
      borderRadius="md"
      borderColor={borderColor}
      boxShadow="md"
      p={6}
      maxW="600px"
      mx="auto"
    >
      <Heading size="md" mb={4} textAlign="center" color="teal.600">
        Detalhes do Cliente
      </Heading>

      <SimpleGrid columns={{ base: 1, md: 2 }} spacing={4} mb={6}>
        <Box>
          <Stack spacing={2}>
            <Text>
              <b>ID:</b> {cliente.id}
            </Text>
            <Text>
              <b>Nome:</b> {cliente.nome}
            </Text>
            <Text>
              <b>Email:</b> {cliente.email || "-"}
            </Text>
            <Text>
              <b>Telefone:</b> {cliente.telefone || "-"}
            </Text>
            <Text>
              <b>CPF:</b> {cliente.cpf || "-"}
            </Text>
          </Stack>
        </Box>

        <Box>
          <Stack spacing={2}>
            
          </Stack>
        </Box>
      </SimpleGrid>

      <Divider mb={4} />

      <Box>
        <Heading size="sm" mb={2} color="teal.500">
          Plano Atual
        </Heading>

        {loading ? (
          <Spinner size="md" />
        ) : planoClienteAtual ? (
          <>
            <Stack spacing={1} mb={4}>
              <Text>
                <b>Plano:</b>{" "}
                <Badge colorScheme="purple" fontSize="0.9em">
                  {planoClienteAtual.nomePlano || "Não informado"}
                </Badge>
              </Text>
              <Text>
                <b>Início:</b> {formatarData(planoClienteAtual.dataInicio)}
              </Text>
              <Text>
                <b>Término:</b> {formatarData(planoClienteAtual.dataFim)}
              </Text>
              <Text>
                <b>Status:</b> {planoClienteAtual.ativo ? "Ativo" : "Inativo"}
              </Text>
            </Stack>

            <Button
              colorScheme="blue"
              onClick={abrirModalAtualizar}
              isDisabled={!planoClienteAtual.ativo || loading}
            >
              Atualizar Plano
            </Button>
          </>
        ) : (
          <Text color="gray.500" fontStyle="italic">
            Cliente não possui plano vinculado.
          </Text>
        )}
      </Box>

      {/* Modal para atualizar plano */}
      <Modal isOpen={isOpen} onClose={onClose} isCentered size="md">
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Atualizar Plano do Cliente</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            {loadingPlanos ? (
              <Spinner />
            ) : (
              <FormControl isRequired isInvalid={!planoSelecionado && submitting}>
                <FormLabel>Selecione o novo plano</FormLabel>
                <Select
                  placeholder="Selecione um plano"
                  value={planoSelecionado}
                  onChange={(e) => setPlanoSelecionado(e.target.value)}
                >
                  {planos.map((p) => (
                    <option key={p.id} value={p.id}>
                      {p.nome} - {p.duracao} dias
                    </option>
                  ))}
                </Select>
                {!planoSelecionado && submitting && (
                  <FormErrorMessage>Plano é obrigatório</FormErrorMessage>
                )}
              </FormControl>
            )}
          </ModalBody>
          <ModalFooter>
            <Button
              colorScheme="blue"
              mr={3}
              onClick={handleAtualizarPlano}
              isLoading={submitting}
              isDisabled={!planoSelecionado}
            >
              Confirmar
            </Button>
            <Button variant="ghost" onClick={onClose}>
              Cancelar
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </Box>
  );
};

export default DetalhesCliente;

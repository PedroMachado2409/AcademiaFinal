import React, { useState } from "react";
import {
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Select,
  Spinner,
  useDisclosure,
  useToast,
  FormControl,
  FormLabel,
  FormErrorMessage,
  Box,
  Text,
  Icon,
} from "@chakra-ui/react";
import { FiCheckCircle } from "react-icons/fi";

import { usePlanoCliente } from "../Contexts/PlanoClienteContext";
import { usePlano } from "../Contexts/PlanoContext";

export function VincularPlanoModal({ clienteId }) {
  const { adicionarPlanoCliente, obterPlanoClientePorId } = usePlanoCliente();
  const { planos, carregando: loadingPlanos } = usePlano();

  const { isOpen, onOpen, onClose } = useDisclosure();
  const toast = useToast();

  const [planoSelecionado, setPlanoSelecionado] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const [loadingPlanoCliente, setLoadingPlanoCliente] = useState(false);
  const [planoClienteAtual, setPlanoClienteAtual] = useState(null);

  const handleOpen = async () => {
    setLoadingPlanoCliente(true);
    const planoAtual = await obterPlanoClientePorId(clienteId);
    setPlanoClienteAtual(planoAtual);
    setLoadingPlanoCliente(false);
    onOpen();
  };

  const handleClose = () => {
    setPlanoSelecionado("");
    setSubmitting(false);
    setPlanoClienteAtual(null);
    onClose();
  };

  const handleVincular = async () => {
    if (!planoSelecionado) {
      toast({
        title: "Selecione um plano",
        status: "warning",
        duration: 3000,
        isClosable: true,
      });
      return;
    }

    setSubmitting(true);
    try {
      await adicionarPlanoCliente({
        clienteId,
        planoId: parseInt(planoSelecionado, 10),
        dataInicio: new Date().toISOString(),
      });

      toast({
        title: "Plano vinculado com sucesso!",
        status: "success",
        duration: 3000,
        isClosable: true,
      });

      handleClose();
    } catch (error) {
      toast({
        title: "Erro ao vincular plano",
        description: error.message || "Verifique os dados e tente novamente.",
        status: "error",
        duration: 3000,
        isClosable: true,
      });
      setSubmitting(false);
    }
  };

  // Bloqueia botão se já tiver plano ativo
  const temPlanoAtivo = planoClienteAtual?.ativo;

  return (
    <>
      <Button
        onClick={handleOpen}
        colorScheme="blue"
        size="sm"
        leftIcon={<FiCheckCircle />}
        isDisabled={temPlanoAtivo}
      >
        {temPlanoAtivo ? "Plano já vinculado" : "Vincular Plano"}
      </Button>

      <Modal isOpen={isOpen} onClose={handleClose} isCentered size="md">
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Vincular plano ao cliente</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            {loadingPlanoCliente ? (
              <Spinner />
            ) : temPlanoAtivo ? (
              <Box textAlign="center" py={4}>
                <Icon as={FiCheckCircle} boxSize={8} color="green.400" />
                <Text fontWeight="bold" mt={2}>
                  Este cliente já possui um plano ativo:
                </Text>
                <Text mt={1} color="teal.600">
                  {planoClienteAtual.nomePlano || "Plano desconhecido"}
                </Text>
              </Box>
            ) : loadingPlanos ? (
              <Spinner />
            ) : (
              <FormControl
                isRequired
                isInvalid={!planoSelecionado && submitting}
              >
                <FormLabel>Plano</FormLabel>
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
              onClick={handleVincular}
              isLoading={submitting}
              isDisabled={temPlanoAtivo}
            >
              Confirmar
            </Button>
            <Button onClick={handleClose} variant="ghost">
              Cancelar
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
}

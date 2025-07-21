import React from "react";
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  VStack,
  Box,
  Text,
  List,
  ListItem,
  useColorModeValue,
} from "@chakra-ui/react";
import { useClientes } from "../Contexts/ClienteContext";
import { useEquipamento } from "../Contexts/EquipamentoContext";

const DetalhesFichaModal = ({ isOpen, onClose, ficha }) => {
  const { clientes } = useClientes();
  const { equipamentos } = useEquipamento();

  const cliente = React.useMemo(() => {
    if (!ficha?.clienteId || !clientes) return null;
    return clientes.find((c) => c.id === ficha.clienteId) || null;
  }, [ficha?.clienteId, clientes]);

  const nomeEquipamentoPorId = React.useCallback(
    (id) => {
      const equipamento = equipamentos.find((e) => e.id === id);
      return equipamento ? equipamento.nome : "Equipamento não encontrado";
    },
    [equipamentos]
  );

  // Cores e estilos baseados no exemplo que você enviou
  const bgBox = useColorModeValue("white", "gray.700");
  const borderColor = useColorModeValue("gray.200", "gray.600");
  const headerColor = useColorModeValue("teal.600", "teal.300");
  const labelColor = useColorModeValue("teal.600", "teal.300");
  const textColor = useColorModeValue("gray.800", "gray.100");
  const cardBg = useColorModeValue("blue.50", "blue.700");

  return (
    <Modal isOpen={isOpen} onClose={onClose} size="md" isCentered>
      <ModalOverlay bg="blackAlpha.600" backdropFilter="blur(4px)" />
      <ModalContent
        bg={bgBox}
        borderRadius="md"
        borderWidth="1px"
        borderColor={borderColor}
        boxShadow="md"
      >
        <ModalHeader
          color={headerColor}
          fontWeight="bold"
          fontSize="lg"  // menor que xl
          px={6}
          py={3}        // menos padding vertical
        >
          Itens da Ficha de Treino
        </ModalHeader>
        <ModalCloseButton _focus={{ boxShadow: "none" }} />

        <ModalBody px={6} py={4}>
          <VStack spacing={4} align="stretch"> {/* menos espaçamento */}
            {/* Dados do Cliente */}
            <Box borderBottom="1px solid" borderColor={borderColor} pb={3}>
              <Text
                fontSize="sm"          // fonte menor
                fontWeight="semibold"
                color={labelColor}
                mb={1}
                textTransform="uppercase"
                letterSpacing="wider"
              >
                Cliente
              </Text>
              <Text fontSize="md" fontWeight="medium" color={textColor}>
                {cliente ? cliente.nome : "Cliente não encontrado"}
              </Text>
            </Box>

            {/* Tipo de Treino */}
            <Box borderBottom="1px solid" borderColor={borderColor} pb={3}>
              <Text
                fontSize="sm"
                fontWeight="semibold"
                color={labelColor}
                mb={1}
                textTransform="uppercase"
                letterSpacing="wider"
              >
                Tipo de Treino
              </Text>
              <Text fontSize="md" color={textColor}>
                {ficha?.tipoTreino || "Não informado"}
              </Text>
            </Box>

            {/* Itens da ficha */}
            {ficha?.itens?.length ? (
              <List spacing={3}> {/* menos espaçamento */}
                {ficha.itens.map((item, idx) => (
                  <ListItem
                    key={idx}
                    p={3}             // menos padding
                    bg={cardBg}
                    borderRadius="md"
                    boxShadow="sm"
                    transition="all 0.2s"
                    _hover={{ boxShadow: "md", transform: "scale(1.02)" }}
                    cursor="default"
                  >
                    <VStack spacing={0.5} align="start"> {/* menos espaçamento */}
                      <Text
                        fontSize="xs"         // fonte menor ainda
                        fontWeight="semibold"
                        color={labelColor}
                        textTransform="uppercase"
                        letterSpacing="wider"
                      >
                        Equipamento
                      </Text>
                      <Text fontSize="sm" fontWeight="medium" color={textColor}>
                        {nomeEquipamentoPorId(item.equipamentoId)}
                      </Text>

                      <Text
                        fontSize="xs"
                        fontWeight="semibold"
                        color={labelColor}
                        textTransform="uppercase"
                        letterSpacing="wider"
                        mt={2}
                      >
                        Repetições
                      </Text>
                      <Text fontSize="sm" color={textColor}>
                        {item.repeticoes ?? "Não informado"}
                      </Text>
                    </VStack>
                  </ListItem>
                ))}
              </List>
            ) : (
              <Text
                color={labelColor}
                fontStyle="italic"
                textAlign="center"
                fontSize="sm"
                mt={3}
              >
                Nenhum item encontrado na ficha.
              </Text>
            )}
          </VStack>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default DetalhesFichaModal;

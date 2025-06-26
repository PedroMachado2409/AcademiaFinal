import React, { useState, useEffect } from "react";
import {
  Button,
  FormControl,
  FormLabel,
  Input,
  FormErrorMessage,
  VStack,
  useToast,
} from "@chakra-ui/react";
import { useClientes } from "../Contexts/ClienteContext";

const CadastroCliente = ({ cliente, onClose }) => {
  const { cadastrarCliente, atualizarCliente } = useClientes();
  const toast = useToast();

  const [form, setForm] = useState({
    nome: "",
    email: "",
    cpf: "",
    telefone: "",
  });

  const [erros, setErros] = useState({});

  useEffect(() => {
    if (cliente) {
      setForm({
        nome: cliente.nome || "",
        email: cliente.email || "",
        cpf: cliente.cpf || "",
        telefone: cliente.telefone || "",
      });
    } else {
      setForm({
        nome: "",
        email: "",
        cpf: "",
        telefone: "",
      });
    }
  }, [cliente]);

  const validar = () => {
    const novosErros = {};
    if (!form.nome) novosErros.nome = "Nome é obrigatório";
    if (!form.email) novosErros.email = "Email é obrigatório";
    if (!form.cpf) novosErros.cpf = "CPF é obrigatório";
    if (!form.telefone) novosErros.telefone = "Telefone é obrigatório";

    setErros(novosErros);
    return Object.keys(novosErros).length === 0;
  };

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!validar()) {
      toast({
        title: "Erro de validação",
        description: "Por favor, preencha todos os campos obrigatórios.",
        status: "error",
        duration: 4000,
        isClosable: true,
      });
      return;
    }

    try {
      if (cliente?.id) {
        await atualizarCliente({ ...form, id: cliente.id });
        toast({
          title: "Cliente atualizado",
          description: "Cliente atualizado com sucesso!",
          status: "success",
          duration: 3000,
          isClosable: true,
        });
      } else {
        await cadastrarCliente(form);
        toast({
          title: "Cliente cadastrado",
          description: "Cliente cadastrado com sucesso!",
          status: "success",
          duration: 3000,
          isClosable: true,
        });
      }
      onClose();
    } catch (error) {
      console.error("Erro no cadastro/atualização:", error);
      toast({
        title: "Erro no cadastro/atualização",
        description: error.message,
        status: "error",
        duration: 4000,
        isClosable: true,
      });
    }
  };

  return (
    <VStack spacing={4} as="form" onSubmit={handleSubmit}>
      <FormControl isInvalid={!!erros.nome}>
        <FormLabel>Nome</FormLabel>
        <Input
          name="nome"
          value={form.nome}
          onChange={handleChange}
          placeholder="Nome completo"
        />
        <FormErrorMessage>{erros.nome}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.email}>
        <FormLabel>Email</FormLabel>
        <Input
          name="email"
          type="email"
          value={form.email}
          onChange={handleChange}
          placeholder="email@exemplo.com"
        />
        <FormErrorMessage>{erros.email}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.cpf}>
        <FormLabel>CPF</FormLabel>
        <Input
          name="cpf"
          value={form.cpf}
          onChange={handleChange}
          placeholder="000.000.000-00"
        />
        <FormErrorMessage>{erros.cpf}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.telefone}>
        <FormLabel>Telefone</FormLabel>
        <Input
          name="telefone"
          value={form.telefone}
          onChange={handleChange}
          placeholder="(00) 00000-0000"
        />
        <FormErrorMessage>{erros.telefone}</FormErrorMessage>
      </FormControl>

      <Button colorScheme="teal" w="full" type="submit">
        {cliente ? "Atualizar Cliente" : "Salvar Cliente"}
      </Button>
    </VStack>
  );
};

export default CadastroCliente;

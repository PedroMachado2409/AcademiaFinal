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
import { usePlano } from "../Contexts/PlanoContext";

const PlanoFormModal = ({ plano, onClose }) => {
  const { cadastrarPlano, atualizarPlano } = usePlano();
  const toast = useToast();

  const [form, setForm] = useState({
    nome: "",
    descricao: "",
    valor: "",
    duracaoMeses: "",
  });

  const [erros, setErros] = useState({});

  useEffect(() => {
    if (plano) {
      setForm({
        nome: plano.nome || "",
        descricao: plano.descricao || "",
        valor: plano.valor || "",
        duracaoMeses: plano.duracaoMeses || "",
      });
    } else {
      setForm({
        nome: "",
        descricao: "",
        valor: "",
        duracaoMeses: "",
      });
    }
  }, [plano]);

  const validar = () => {
    const novosErros = {};
    if (!form.nome) novosErros.nome = "Nome é obrigatório";
    if (!form.descricao) novosErros.descricao = "Descrição é obrigatória";
    if (!form.valor) novosErros.valor = "Valor é obrigatório";
    if (!form.duracaoMeses) novosErros.duracaoMeses = "Duração é obrigatória";
    setErros(novosErros);
    return Object.keys(novosErros).length === 0;
  };

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validar()) {
      toast({
        title: "Erro de validação",
        description: "Preencha todos os campos obrigatórios.",
        status: "error",
        duration: 4000,
        isClosable: true,
      });
      return;
    }

    try {
      if (plano?.id) {
        await atualizarPlano(plano.id, form);
        toast({ title: "Plano atualizado", status: "success", duration: 3000 });
      } else {
        await cadastrarPlano(form);
        toast({ title: "Plano cadastrado", status: "success", duration: 3000 });
      }
      onClose();
    } catch (error) {
      console.error("Erro ao salvar plano:", error);
      toast({
        title: "Erro",
        description: "Não foi possível salvar o plano.",
        status: "error",
        duration: 4000,
        isClosable: true,
      });
    }
  };

  return (
    <VStack spacing={4}>
      <FormControl isInvalid={!!erros.nome}>
        <FormLabel>Nome</FormLabel>
        <Input name="nome" value={form.nome} onChange={handleChange} />
        <FormErrorMessage>{erros.nome}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.descricao}>
        <FormLabel>Descrição</FormLabel>
        <Input name="descricao" value={form.descricao} onChange={handleChange} />
        <FormErrorMessage>{erros.descricao}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.valor}>
        <FormLabel>Valor</FormLabel>
        <Input name="valor" type="number" value={form.valor} onChange={handleChange} />
        <FormErrorMessage>{erros.valor}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.duracaoMeses}>
        <FormLabel>Duração (meses)</FormLabel>
        <Input
          name="duracaoMeses"
          type="number"
          value={form.duracaoMeses}
          onChange={handleChange}
        />
        <FormErrorMessage>{erros.duracaoMeses}</FormErrorMessage>
      </FormControl>

      <Button colorScheme="teal" w="full" onClick={handleSubmit}>
        {plano ? "Atualizar Plano" : "Cadastrar Plano"}
      </Button>
    </VStack>
  );
};

export default PlanoFormModal;

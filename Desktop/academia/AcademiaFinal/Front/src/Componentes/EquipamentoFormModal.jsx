import React, { useState, useEffect } from "react";
import {
  Button,
  FormControl,
  FormLabel,
  Input,
  NumberInput,
  NumberInputField,
  Checkbox,
  FormErrorMessage,
  VStack,
  useToast,
} from "@chakra-ui/react";
import { useEquipamento } from "../Contexts/EquipamentoContext";

const CadastroEquipamento = ({ equipamento, onClose }) => {
  const { cadastrarEquipamento, atualizarEquipamento, buscarEquipamentos } = useEquipamento();
  const toast = useToast();

  const [form, setForm] = useState({
    nome: "",
    descricao: "",
    marca: "",
    peso: 0,
    ativo: true,
  });

  const [erros, setErros] = useState({});

  useEffect(() => {
    if (equipamento) {
      setForm({
        nome: equipamento.nome || "",
        descricao: equipamento.descricao || "",
        marca: equipamento.marca || "",
        peso: equipamento.peso || 0,
        ativo: equipamento.ativo ?? true,
      });
    } else {
      setForm({
        nome: "",
        descricao: "",
        marca: "",
        peso: 0,
        ativo: true,
      });
    }
  }, [equipamento]);

  const validar = () => {
    const novosErros = {};
    if (!form.nome) novosErros.nome = "Nome é obrigatório";
    if (!form.descricao) novosErros.descricao = "Descrição é obrigatória";
    if (!form.marca) novosErros.marca = "Marca é obrigatória";
    if (form.peso <= 0) novosErros.peso = "Peso deve ser maior que zero";

    setErros(novosErros);
    return Object.keys(novosErros).length === 0;
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handlePesoChange = (valueString) => {
    const valor = parseInt(valueString);
    setForm((prev) => ({
      ...prev,
      peso: isNaN(valor) ? 0 : valor,
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!validar()) {
      toast({
        title: "Erro de validação",
        description: "Por favor, preencha todos os campos obrigatórios corretamente.",
        status: "error",
        duration: 4000,
        isClosable: true,
      });
      return;
    }

    try {
      if (equipamento?.id) {
        if (atualizarEquipamento) {
          await atualizarEquipamento({ ...form, id: equipamento.id });
          toast({
            title: "Equipamento atualizado",
            description: "Equipamento atualizado com sucesso!",
            status: "success",
            duration: 3000,
            isClosable: true,
          });
        }
      } else {
        await cadastrarEquipamento(form);
        toast({
          title: "Equipamento cadastrado",
          description: "Equipamento cadastrado com sucesso!",
          status: "success",
          duration: 3000,
          isClosable: true,
        });
      }
      if (buscarEquipamentos) {
        await buscarEquipamentos();
      }
      onClose();
    } catch (error) {
      console.error("Erro no cadastro/atualização:", error);
      toast({
        title: "Erro no cadastro/atualização",
        description: error.message || "Erro desconhecido",
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
          placeholder="Nome do equipamento"
        />
        <FormErrorMessage>{erros.nome}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.descricao}>
        <FormLabel>Descrição</FormLabel>
        <Input
          name="descricao"
          value={form.descricao}
          onChange={handleChange}
          placeholder="Descrição do equipamento"
        />
        <FormErrorMessage>{erros.descricao}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.marca}>
        <FormLabel>Marca</FormLabel>
        <Input
          name="marca"
          value={form.marca}
          onChange={handleChange}
          placeholder="Marca do equipamento"
        />
        <FormErrorMessage>{erros.marca}</FormErrorMessage>
      </FormControl>

      <FormControl isInvalid={!!erros.peso}>
        <FormLabel>Peso (kg)</FormLabel>
        <NumberInput min={1} value={form.peso} onChange={handlePesoChange}>
          <NumberInputField name="peso" />
        </NumberInput>
        <FormErrorMessage>{erros.peso}</FormErrorMessage>
      </FormControl>

      <FormControl>
        <Checkbox
          name="ativo"
          isChecked={form.ativo}
          onChange={handleChange}
        >
          Ativo
        </Checkbox>
      </FormControl>

      <Button colorScheme="teal" w="full" type="submit">
        {equipamento ? "Atualizar Equipamento" : "Salvar Equipamento"}
      </Button>
    </VStack>
  );
};

export default CadastroEquipamento;

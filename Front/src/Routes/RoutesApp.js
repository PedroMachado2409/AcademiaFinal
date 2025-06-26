import { Route, Routes } from "react-router-dom";
import LoginScreen from "../Pages/LoginScreen";
import Home from "../Pages/Home";
import Cadastro from "../Pages/CadastroUsuarios";
import AtualizarUsuario from "../Pages/AtualizarUsuario";
import ListagemClientes from "../Pages/CadastroCliente";
import ListaPlanos from "../Pages/ListaPlanos";
import ListaEquipamentos from "../Pages/ListaEquipamentos";
import FichasDeTreino from "../Pages/FichasDeTreino";

import PrivateRoute from "./PrivateRoute";

function RoutesApp() {
  return (
    <Routes>
      <Route path="/" element={<LoginScreen />} />

      <Route element={<PrivateRoute />}>
        <Route path="/Home" element={<Home />} />
        <Route path="/Cadastro" element={<Cadastro />} />
        <Route path="/Equipamentos" element={<ListaEquipamentos />} />
        <Route path="/AtualizarUsuario" element={<AtualizarUsuario />} />
        <Route path="/Clientes" element={<ListagemClientes />} />
        <Route path="/Planos" element={<ListaPlanos />} />
        <Route path="/FichasTreino" element={<FichasDeTreino />} />
      </Route>
    </Routes>
  );
}

export default RoutesApp;

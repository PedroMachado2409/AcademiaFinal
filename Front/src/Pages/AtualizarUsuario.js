// import React, { useState } from 'react';
// import {
//   Box,
//   FormControl,
//   FormLabel,
//   Input,
//   Button,
//   useColorMode,
//   Heading,
//   VStack,
//   Flex,
//   Alert,
//   AlertIcon,
//   AlertTitle,
//   AlertDescription,
// } from '@chakra-ui/react';
// import Sidebar from '../Componentes/Siderbar';
// import { useUpdateUser } from '../Contexts/UpdateUserContext';

// export default function AtualizarUsuario() {
//   const { colorMode } = useColorMode();
//   const { updateUser } = useUpdateUser();

//   const [senhaAtual, setSenhaAtual] = useState('');
//   const [novaSenha, setNovaSenha] = useState('');
//   const [novoLogin, setNovoLogin] = useState('');
//   const [formSubmitted, setFormSubmitted] = useState(false);
//   const [error, setError] = useState(false);

//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     if (!senhaAtual || !novaSenha) {
//       setError(true);
//       return;
//     }

//     const token = localStorage.getItem('token');

//     try {
//       await updateUser({ senhaAtual, novaSenha, novoLogin }, token);
//       setFormSubmitted(true);
//       setError(false);

//       setSenhaAtual('');
//       setNovaSenha('');
//       setNovoLogin('');
//     } catch (err) {
//       setError(true);
//       console.error(err.message);
//     }
//   };

//   return (
//     <div>
//       <Sidebar />

//       <Flex
//         minH="100vh"
//         justifyContent="center"
//         alignItems="center"
//         bg={colorMode === 'light' ? 'gray.100' : 'gray.900'}
//         p={4}
//       >
//         <Box
//           p={10}
//           rounded="2xl"
//           borderWidth="1px"
//           boxShadow="xl"
//           bg={colorMode === 'light' ? 'white' : 'gray.800'}
//           w={['100%', '90%', '500px']}
//         >
//           <VStack spacing={6} align="stretch">
//             <Heading as="h1" size="lg" textAlign="center">
//               Atualizar Usuário
//             </Heading>

//             {error && (
//               <Alert status="error" rounded="md">
//                 <AlertIcon />
//                 <Box>
//                   <AlertTitle>Erro ao atualizar</AlertTitle>
//                   <AlertDescription>
//                     Verifique os campos e tente novamente.
//                   </AlertDescription>
//                 </Box>
//               </Alert>
//             )}

//             {formSubmitted && (
//               <Alert status="success" rounded="md">
//                 <AlertIcon />
//                 <Box>
//                   <AlertTitle>Sucesso!</AlertTitle>
//                   <AlertDescription>
//                     Usuário atualizado com sucesso.
//                   </AlertDescription>
//                 </Box>
//               </Alert>
//             )}

//             <form onSubmit={handleSubmit}>
//               <VStack spacing={5}>
//                 <FormControl id="senhaAtual" isRequired>
//                   <FormLabel>Senha Atual</FormLabel>
//                   <Input
//                     type="password"
//                     value={senhaAtual}
//                     onChange={(e) => setSenhaAtual(e.target.value)}
//                     placeholder="Digite sua senha atual"
//                     size="lg"
//                     variant="outline"
//                   />
//                 </FormControl>

//                 <FormControl id="novaSenha" isRequired>
//                   <FormLabel>Nova Senha</FormLabel>
//                   <Input
//                     type="password"
//                     value={novaSenha}
//                     onChange={(e) => setNovaSenha(e.target.value)}
//                     placeholder="Digite sua nova senha"
//                     size="lg"
//                     variant="outline"
//                   />
//                 </FormControl>

//                 <FormControl id="novoLogin">
//                   <FormLabel>Novo Login (opcional)</FormLabel>
//                   <Input
//                     type="text"
//                     value={novoLogin}
//                     onChange={(e) => setNovoLogin(e.target.value)}
//                     placeholder="Digite o novo login"
//                     size="lg"
//                     variant="outline"
//                   />
//                 </FormControl>

//                 <Button
//                   type="submit"
//                   colorScheme="blue"
//                   size="lg"
//                   w="100%"
//                   mt={2}
//                   _hover={{ opacity: 0.9 }}
//                 >
//                   Atualizar
//                 </Button>
//               </VStack>
//             </form>
//           </VStack>
//         </Box>
//       </Flex>
//     </div>
//   );
// }

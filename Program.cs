using System;
using System.Collections.Generic;

namespace ExemploCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            //1 - instanciar banco de dados
            // 2- instanciar classe categoria
            //3 obter titulo categoria
            //definir titulo do objeto categoria
            //chamar metodo adicionar objeto banco de dados

            try
            {
                Console.Clear();
                string opt = "";
                string confirma = "";
                BancoDados bd;
                Categoria ct;
                Cliente cli;
                // List<Categoria> categorias;             
                do
                {
                    System.Console.WriteLine("CRUD NO BANCO PAPELARIA");
                    System.Console.WriteLine("-----------------------");
                    System.Console.WriteLine("Informe uma das opções abaixo: ");
                    System.Console.WriteLine("\n1- Cadastrar" +
                                              "\n2- Atualizar" +
                                              "\n3- Deletar" +
                                              "\n4- Consultar" +
                                              "\n5- Consulta todos os dados da categoria" +
                                              "\n9- Sair: ");
                    opt = Console.ReadLine();
                    switch (opt)
                    {
                        case "1":
                            bd = new BancoDados();

                            System.Console.WriteLine("Escolha entre: ");
                            System.Console.WriteLine("\n1 - Cadastrar Categoria" +
                                                     "\n2 - Cadastrar Cliente");
                            opt = Console.ReadLine();

                            switch (opt)
                            {
                                case "1":
                                    ct = new Categoria();
                                    System.Console.WriteLine("Digite os dados da categoria a ser adicionada:");
                                    System.Console.WriteLine("Titulo: ");
                                    ct.Titulo = Console.ReadLine();

                                    System.Console.WriteLine("Nova categoria {0}. \nConfirma? (s/n)", ct.Titulo);
                                    confirma = Console.ReadLine();
                                    if (confirma == "s")
                                    {
                                        bd.AdicionarCategoria(ct);
                                        System.Console.WriteLine("Nova categoria adicionada !");
                                    }
                                    break;

                                case "2":
                                    cli = new Cliente();
                                    Console.WriteLine("Informe os dados do cliente a ser cadastrado: ");
                                    System.Console.Write("Nome: ");
                                    cli.NomeCliente = Console.ReadLine();

                                    Console.Write("Email: ");
                                    cli.Email = Console.ReadLine();

                                    Console.Write("CPF: ");
                                    cli.Cpf = Console.ReadLine();

                                    Console.WriteLine("Confirma os dados abaixo? (s/n) ");
                                    Console.WriteLine("Nome    | Email     | CPF ");
                                    Console.WriteLine("{0} | {1} | {2} ", cli.NomeCliente, cli.Email, cli.Cpf);

                                    confirma = Console.ReadLine();
                                    if (confirma == "s")
                                        bd.AdicionarCliente(cli);

                                    break;
                            }

                            break;

                        case "2":
                            bd = new BancoDados();
                            ct = new Categoria();
                            System.Console.WriteLine("Atualizar");
                            ct.IdCategoria = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("Informe o novo titulo");
                            ct.Titulo = Console.ReadLine();
                            bool at = bd.AtualizarCategoria(ct);
                            if (at)
                            {
                                System.Console.WriteLine("Dados atualizados com sucesso!");
                            }
                            break;
                        case "3":

                            bd = new BancoDados();
                            ct = new Categoria();
                            System.Console.WriteLine("Informe o Id a para ser deletado");
                            ct.IdCategoria = int.Parse(Console.ReadLine());
                            bool ap = bd.ApagarCategoria(ct);
                            if (ap)
                            {
                                System.Console.WriteLine("Dados apagados com sucesso!");
                                Console.Clear();
                            }
                            break;

                        case "4"
                                            :
                            bd = new BancoDados();
                            ct = new Categoria();
                            System.Console.WriteLine("Seleciona todos dados da tabela Categoria");
                            System.Console.WriteLine("Informe o nome do título da categoria: ");
                            ct.Titulo = Console.ReadLine();
                            List<Categoria> lista = bd.ListarCategorias(ct.Titulo);
                            foreach (var item in lista)
                            {
                                System.Console.WriteLine("Id: {0},\nTitulo: {1}. ", item.IdCategoria, item.Titulo);

                            }

                            break;

                        case "5"
                                            :
                            bd = new BancoDados();
                            ct = new Categoria();
                            System.Console.WriteLine("Seleciona todos dados da tabela Categoria");                         
                            List<Categoria> listatudo = bd.ListarCategorias();
                            foreach (var item in listatudo)
                            {
                                System.Console.WriteLine("Id: {0},Titulo: {1}. ", item.IdCategoria, item.Titulo);

                            }

                            break;

                        default:
                            break;
                    }

                } while (opt != "9");
            }
            catch (System.Exception exe)
            {
                System.Console.WriteLine(exe.Message);
            }

        }

    }
}

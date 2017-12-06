using System;

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
             string opt ="";
             BancoDados bd;
             Categoria ct;
             
                        
             
             do             
             {
                System.Console.WriteLine("CRUD NO BANCO PAPELARIA");
                System.Console.WriteLine("-----------------------");
                System.Console.WriteLine("Informe uma opção");
                System.Console.WriteLine("\n1- Inserir/Adicionar \n2- Atualizar/Update \n3- Apagar/Deletar \n4- Selecionar/Select: ");
                opt = Console.ReadLine(); 
                 switch (opt)
                 {
                     case "1" : 
                     bd = new BancoDados();
                     ct = new Categoria();                     
                     System.Console.WriteLine("Informe o título: ");
                     ct.Titulo = Console.ReadLine();
                     bool ad = bd.Adicionar(ct);
                     if (ad)
                     {
                         System.Console.WriteLine("Dados inseridos com sucesso!");
                     }                        
                        break;

                    case "2" :
                        bd = new BancoDados();
                        ct = new Categoria(); 
                        System.Console.WriteLine("Informe o Id da categoria a ser atualizado");
                        ct.IdCategoria = int.Parse(Console.ReadLine());
                        System.Console.WriteLine("Informe o novo titulo");
                        ct.Titulo = Console.ReadLine();
                        bool at = bd.Atualizar(ct);
                        if (at)
                        {
                            System.Console.WriteLine("Dados atualizados com sucesso!");
                            
                        }                        
                        break;
                        case "3":

                        bd = new BancoDados();
                        ct = new Categoria();
                        System.Console.WriteLine("Informe o Id a para ser deletado");
                        ct.IdCategoria =int.Parse(Console.ReadLine());
                        bool ap = bd.Apagar(ct);
                        if (ap)
                        {
                            System.Console.WriteLine("Dados apagados com sucesso!");                            
                            Console.Clear();
                        } 
                        break; 

                    case "4"                      :
                       // bd = new BancoDados();
                        //ct = new Categoria();
                        //System.Console.WriteLine("Seleciona todos dados da tabela Categoria");                      
                                              
                        

                        break;                     

                     default:
                     break;
                 }

                 
             } while (opt!="9");
         }
         catch (System.Exception)
         {
             
             throw;
         }
         
        }
        
    }
}

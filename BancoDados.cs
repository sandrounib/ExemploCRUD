using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExemploCRUD
{
    public class BancoDados
    {
        SqlConnection cn;
        SqlCommand comandos;
        SqlDataReader rd;

        /// <summary>
        /// Adiciona linhas a categorias
        /// </summary>
        /// <param name="cat">Categoria a ser adicionada</param>
        /// <returns>tru se executado com sucesso</returns>
        public bool AdicionarCategoria(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();//criar a conexão
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;
                                      user id=sa;password=sandro@123";//localização e permissão de acesso ao banco Papelaria
                cn.Open();
                comandos =  new SqlCommand();//instancia objeto comandos para ser usados comandos sql
                comandos.Connection = cn;// Estabelece a relação entre comandos e cn
                                        // Onde os comandos sql devem ser executados?
                                         // Resposta: conexão cn (que tem o endereço do banco,,usuario e senha)

                                        
                                       //tipos de comandos vou executar Obs. o Comandtype após o sinal de = não vem do sqlclient
                                       // pois é uma definição que pode ser tratada de várias bases de dados
                                       //ao ficar vermelho = CommandType.Text Adicione using System.Data
                comandos.CommandType = CommandType.Text; //indica o tipo de comando como texto
                //qual é o comando de texto que vou executar?
                comandos.CommandText = "insert into categorias(titulo)values (@vt)";//comando a ser executado com valor do parâmetro @vt indicado a seguir
                //passando os parametros
                comandos.Parameters.AddWithValue("@vt",cat.Titulo);

                //executar e cadastrar -Executenoquery é o que faz a execução mesmo, quando ele faz ele retorna um valor inteiro
                int r = comandos.ExecuteNonQuery();/*execução dos comandos acima e retorna um valor referente ao total de linhas afetadas
                                                     caso o valor retorne como 0, significa que houve erro*/
                
                if(r > 0 ) //se for zero não cadastrou nada
                    rs = true;

                    //serve para limpar o campo @vt pois coloquei um valor/parametro lá                    
                    comandos.Parameters.Clear();//limpar os parâmetros utilizados para a próxima execução
                    
            }
            catch (SqlException se)
            {
                
                throw new Exception("Erro ao tentar cadastrar. "+ se.Message);
            }

            catch(Exception ex){
                throw new Exception("Erro inesperado. "+ex.Message);
            }

            finally{
                cn.Close();//fechar o banco antes de terminar a execução
            }
            return rs;                
        }  

        /// <summary>
        /// Atualização de parâmetros em Categorias
        /// </summary>
        /// <param name="cat">Categoria a ser atualizada</param>
        /// <returns>true se executado com sucesso</returns>
         public bool AtualizarCategoria(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();//cria conexão
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=sandro@123";
                cn.Open();
                comandos =  new SqlCommand();//instancia objeto comandos
                comandos.Connection = cn;// commandos.connection é para estabelecer a relação onde os comandos sql devem ser executados? R. no objeto cn que tem o banco usuario e senha

                //tipos de comandos vou executar
                                       //esse Comandtype não vem do sqlclient pois é uma definição que pode ser tratada de várias bases de dados
                                       //adicionou using system.data
                comandos.CommandType = CommandType.Text;
                //qual é o comando de texto que vou executar?
                comandos.CommandText = "update categorias set titulo=@vt where idcategoria=@vi";
                //passando os parametros
                comandos.Parameters.AddWithValue("@vt",cat.Titulo);
                comandos.Parameters.AddWithValue("@vi",cat.IdCategoria);

                //executar e cadastrar
                //executenoquery é o que faz a execução mesmo, quando ele faz ele retorna um valor inteiro
                int r = comandos.ExecuteNonQuery();
                //se for zero não cadastrou nada
                if(r > 0 )
                    rs = true;

                    //
                    //serve para limpar o campo @vt pois coloquei um valor/parametro lá
                    //não corra o risco coloque o comando abaixo para limpar
                    comandos.Parameters.Clear();
                    
            }
            catch (SqlException se)
            {
                
                throw new Exception("Erro ao tentar atualizar. "+ se.Message);
            }

            catch(Exception ex){
                throw new Exception("Erro inesperado. "+ex.Message);
            }

            finally{
                cn.Close();
            }
            return rs;                
        }      


        /// <summary>
        /// Exclui linhas da Categorias
        /// </summary>
        /// <param name="cat">Categoria a ser xcluida</param>
        /// <returns>true se executado com êxito</returns>    
         public bool ApagarCategoria(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=sandro@123";
                cn.Open();
                comandos =  new SqlCommand();//instancia objeto comandos
                comandos.Connection = cn;// commandos.connection é para estabelecer a relação onde os comandos sql devem ser executados? R. no objeto cn que tem o banco usuario e senha

                //tipos de comandos vou executar - esse Comandtype não vem do sqlclient pois é uma definição que pode ser tratada de várias bases de dados
                //adicionou using system.data
                comandos.CommandType = CommandType.Text;
                //qual é o comando de texto que vou executar?
                comandos.CommandText = "delete categorias where idCategoria=@vi";
                //passando os parametros
                comandos.Parameters.AddWithValue("@vi",cat.IdCategoria);                

                //executar e cadastrar
                //executenoquery é o que faz a execução mesmo, quando ele faz ele retorna um valor inteiro
                int r = comandos.ExecuteNonQuery();
                //se for zero não cadastrou nada
                if(r > 0 )
                    rs = true;

                
                    //serve para limpar o campo @vt pois coloquei um valor/parametro lá
                    //não corra o risco coloque o comando abaixo para limpar
                    comandos.Parameters.Clear();
                    
            }
            catch (SqlException se)
            {
                
                throw new Exception("Erro ao tentar apagar. "+ se.Message);
            }

            catch(Exception ex){
                throw new Exception("Erro inesperado. "+ex.Message);
            }

            finally{
                cn.Close();
            }
            return rs;                
        }   

        /// <summary>
        /// Pesquisar Categoria por ID
        /// </summary>
        /// <param name="id">ID da categoria - se o ID for zero exibe todas categorias</param>
        /// <returns>Lista de categorias encontradas</returns>
        public List<Categoria> ListarCategorias(int id){
            
            List<Categoria> lista= new List<Categoria>();

            try{                
                cn = new SqlConnection();
                cn.ConnectionString=@"Data source=.\sqlexpress;initial catalog=Papelaria;
                                    user=sa;password=sandro@123";
                cn.Open();
                comandos= new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                if(id == 0){
                
                comandos.CommandText="SELECT * FROM categoria";
                }
                else{
                    comandos.CommandText = "SELECT * FROM categorias where IdCategoria = @vi";
                    comandos.Parameters.AddWithValue("@vi",id); /* a consulta é parametrizada então eu boto um parametro
                    não me preocupo qual é o tipo dele,passo o parametro. Não pode ser o ExecuteNoQuery pois ele retorna um valor numérico
                    Não quero isso quero os dados que estão la na base nao vou retornar nenhun dado
                    ele é um leitor de dados ExecuteReader - só um leitor  -- não retorna nada*/
                }
                
                rd = comandos.ExecuteReader();  /* executa o leitor e traga os valores e coloca dentro do rd
                se tiver muitos dados quero que ele pegue a primeira linha e coloca na lista
                pega a segunda e coloca na lista e assim por diante*/
                while (rd.Read()) // enquanto rd for capaz de ler linhas/ conteúdo dentro de rd
                {
                    /*poderia ser assim tambem  -- (mas farei o de baixo)
                    categorias ct = new categorias(){
                        idcategoria = rd.GetInt32(0),
                        titulo = rd.GetString(1)
                };
                lista.Add(ct);
                    }
                     */
                    
                    lista.Add(new Categoria{//pega a lista e rotorna uma nova categoria que está lá como um meio de passagem
                                IdCategoria=rd.GetInt32(0),//q tenho que fazer new pq a lista não é um simples lista ela é um novo objeto
                                Titulo = rd.GetString(1)
                                });                    
                } 
                comandos.Parameters.Clear();             

                
            }
            catch (SqlException se)
            {
                
                throw new Exception("Erro ao tentar listar. "+se.Message);
            }
            catch (Exception ex){
                throw new Exception("Erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return lista;
        }

        /// <summary>
        /// Lista categoria por título
        /// </summary>
        /// <param name="titulo">Título a ser pesquisado</param>
        /// <returns>Lista de categorias encontradas</returns>
        public List<Categoria> ListarCategorias(string titulo){            
            List<Categoria> lista= new List<Categoria>();
            try
            {                
                cn = new SqlConnection();
                cn.ConnectionString=@"Data source=.\sqlexpress;initial catalog=Papelaria;
                                    user=sa;password=sandro@123";
                cn.Open();
                comandos= new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                //qual é o comando que eu vou utilizar? é o commandtext com esse comando abaixo
                comandos.CommandText="SELECT * FROM Categorias where titulo like @vt";
                comandos.Parameters.AddWithValue("@vt",titulo); /*a consulta é parametrizada então eu boto um parametro
                não me preocupo qual tipo dele é passo o parametro, não pode ser o ExecuteNoQuery pois ele retorna um valor numérico
                 não quero isso quero os dados que estão la na base nao vou retornar nenun dado
                ele é um leitor de dados ExecuteReader - só um leitor  -- não retorna nada*/
                
                rd = comandos.ExecuteReader();  //executa o leitor e traga os valores e coloca dentro do rd
                /*se tiver muitos dados quero que ele pegue a primeira linha e coloca na lista
                pega a segunda e coloca na lista e assim por diante*/
                while (rd.Read()) // enquanto rd for capaz de ler linhas/ conteúdo dentro de rd
                {
                    /*poderia ser assim mas, fazei igua abaixo
                    categorias ct = new categorias(){
                        idcategoria = rd.GetInt32(0),
                        titulo = rd.GetString(1)
                };
                lista.Add(ct);
                    }
                     */
                    
                    lista.Add(new Categoria{//pega a lista e rotorna uma nova categoria que está lá como um meio de passagem
                                IdCategoria=rd.GetInt32(0),//q tenho que fazer new pq a lista não é um simples lista ela é um novo objeto
                                Titulo = rd.GetString(1)
                                });                    
                } 
                comandos.Parameters.Clear();           

            }
            catch (SqlException se)
            {
                
                throw new Exception("Erro ao tentar listar. "+se.Message);
            }
            catch (Exception ex){
                throw new Exception("Erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return lista;
        }


        public bool AdicionarCliente(Cliente cliente){
            bool rs = false;
            try
            {
               cn = new SqlConnection();
                cn.ConnectionString=@"Data Source=.\sqlexpress;initial catalog=Papelaria;user id=sa;password=sandro@123";
                cn.Open();
                comandos = new SqlCommand();
                comandos.Connection= cn;
                comandos.CommandType = CommandType.StoredProcedure;
                comandos.CommandText = "CadClientes"; 

                SqlParameter pnome = new SqlParameter("@nome",SqlDbType.VarChar,50);
                pnome.Value = cliente.NomeCliente;
                comandos.Parameters.Add(pnome);
                
                SqlParameter pemail = new SqlParameter("@email",SqlDbType.VarChar,100);
                pemail.Value = cliente.Email;
                comandos.Parameters.Add(pemail);

                SqlParameter pcpf = new SqlParameter("@cpf",SqlDbType.VarChar,20);
                pcpf.Value = cliente.Cpf;
                comandos.Parameters.Add(pcpf);

                int r = comandos.ExecuteNonQuery();
                if (r > 0)
                {
                    rs = true;                    
                }
                comandos.Parameters.Clear();
            }


            catch (SqlException se)
            {
                
                throw new Exception("Erro inesperado "+se.Message);
            }
            catch(Exception ex){
                throw new Exception("Erro inesperado "+ex.Message);
            }
            finally{
                cn.Close();
            }
            return rs;                 
                           
        }
    
    }
}
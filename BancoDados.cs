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

        public bool Adicionar(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=senai@123";
                cn.Open();
                comandos =  new SqlCommand();//instancia objeto comandos
                comandos.Connection = cn;// commandos.connection é para estabelecer a relação onde os comandos sql devem ser executados? R. no objeto cn que tem o banco usuario e senha

                //tipos de comandos vou executar
                                       //esse Comandtype não vem do sqlclient pois é uma definição que pode ser tratada de várias bases de dados
                                       //adicionou using system.data
                comandos.CommandType = CommandType.Text;
                //qual é o comando de texto que vou executar?
                comandos.CommandText = "insert into categorias(titulo)values (@vt)";
                //passando os parametros
                comandos.Parameters.AddWithValue("@vt",cat.Titulo);

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
                
                throw new Exception("Erro ao tentar cadastrar. "+ se.Message);
            }

            catch(Exception ex){
                throw new Exception("Erro inesperado. "+ex.Message);
            }

            finally{
                cn.Close();
            }
            return rs;                
        }  



         public bool Atualizar(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=senai@123";
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


         public bool Apagar(Categoria cat){
            bool rs = false;
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa;password=senai@123";
                cn.Open();
                comandos =  new SqlCommand();//instancia objeto comandos
                comandos.Connection = cn;// commandos.connection é para estabelecer a relação onde os comandos sql devem ser executados? R. no objeto cn que tem o banco usuario e senha

                //tipos de comandos vou executar
                                       //esse Comandtype não vem do sqlclient pois é uma definição que pode ser tratada de várias bases de dados
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

                    //
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

        public List<Categoria> ListarCategorias(int id){
            
            List<Categoria> lista= new List<Categoria>();

            try
            {                
                cn = new SqlConnection();
                cn.ConnectionString=@"Data source=.\sqlexpress;initial catalog=Papelaria;user=sa;password=senai@123";
                cn.Open();
                comandos= new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                //qual é o comando que eu vou utilizar? é o commandtext com esse comando abaixo
                comandos.CommandText="select * from categoria where idcategoria =@vi";
                comandos.Parameters.AddWithValue("@vi",id); // a consulta é parametrizada então eu boto um parametro
                                                            //não me preocupo qual tipo dele é passo o parametro

                //não pode ser o executenoquery pois ele retorna um valor numérico
                // não quero isso quero os dados que estão la na base nao vou retornar nenun dado
                //ele é um leitor de dados ExecuteReader - só um leitor  -- não retorna nada
                rd = comandos.ExecuteReader();  //executa o leitor e traga os valores e coloca dentro do rd
                //se tiver muitos dados quero que ele pegue a primeira linha e coloca na lista
                //pega a segunda e coloca na lista e assim por diante
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
                    }
                    );                    
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


        public List<Categoria> ListarCategorias(string titulo){
            
            List<Categoria> lista= new List<Categoria>();

            try
            {                
                cn = new SqlConnection();
                cn.ConnectionString=@"Data source=.\sqlexpress;initial catalog=Papelaria;user=sa;password=senai@123";
                cn.Open();
                comandos= new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                //qual é o comando que eu vou utilizar? é o commandtext com esse comando abaixo
                comandos.CommandText="select * from categoria where titulo like @vt";
                comandos.Parameters.AddWithValue("@vi",titulo); // a consulta é parametrizada então eu boto um parametro
                                                            //não me preocupo qual tipo dele é passo o parametro

                //não pode ser o executenoquery pois ele retorna um valor numérico
                // não quero isso quero os dados que estão la na base nao vou retornar nenun dado
                //ele é um leitor de dados ExecuteReader - só um leitor  -- não retorna nada
                rd = comandos.ExecuteReader();  //executa o leitor e traga os valores e coloca dentro do rd
                //se tiver muitos dados quero que ele pegue a primeira linha e coloca na lista
                //pega a segunda e coloca na lista e assim por diante
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
                    }
                    );                    
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
    
    }
}
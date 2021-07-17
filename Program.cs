using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Script_DBS02
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa pes = new Pessoa();
            List<Pessoa> listPes = new List<Pessoa>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-DCBREFR;Initial Catalog=tempdb;Integrated Security=True");
            bool var = true;
            while(var){
               Console.WriteLine("!!! S1STEMA DE L1ST4 FURQUIM v1.0 !!!");
               Console.WriteLine("1 - ADICIONAR PESSOA");
               Console.WriteLine("2 - DELETAR PESSOA");
               Console.WriteLine("3 - ATUALIZAR PESSOA");
               Console.WriteLine("4 - VER BANCO DE DADOS");
               string escolha = Console.ReadLine();
               if(escolha == "1")
               {
                  AddDB(pes,conn);
               }
               if(escolha == "2")
               {
                  DeleteDB(pes,conn);
               }
               if(escolha == "3")
               {
                  UpdateDB(pes,conn);
               }
               if(escolha == "4")
               {
                  ViewDB(conn);
               }
            }

        }

        public static void ViewDB(SqlConnection conn)
        {
            Console.Clear();

            //SELECT, SERVER PARA VER O QUE FOI ARMAZENADO NO BANCO DE DADOS
            SqlCommand cmd;
            string select = "SELECT * FROM dbo.Pessoa";
            cmd = new SqlCommand(select,conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
               Console.WriteLine("ID > " + "( " + dr["idPessoa"] + " )");
               Console.WriteLine("Nome > " + dr[1]);
               Console.WriteLine("Cpf > " + dr[2]);
               Console.WriteLine("Idade > " + dr["Idade"]);
               Console.WriteLine("======================");
            }
            dr.Close();
            conn.Close();
            //===============================================================
        }

        public static void UpdateDB(Pessoa pes, SqlConnection conn)
        {
            SqlCommand cmd;
           Console.WriteLine(" <--> EDITANDO...");
           Console.WriteLine("| Dígite o ID da pessoa para edita-la |");
           string id = Console.ReadLine();
           Console.WriteLine("| NOME - EDIT |");
           string nome = Console.ReadLine();
           Console.WriteLine("| CPF - EDIT |");
           string cpf = Console.ReadLine();
           Console.WriteLine("| IDADE - EDIT |");
           string idade = Console.ReadLine();
           string update = $"UPDATE dbo.Pessoa Set Nome = '{nome}', Cpf = '{cpf}', Idade = '{idade}' WHERE idPessoa = '{id}'";
           cmd = new SqlCommand(update, conn);
           conn.Open();
           cmd.ExecuteNonQuery();
           conn.Close();
        }
        public static void DeleteDB(Pessoa pes, SqlConnection conn)
        {
            SqlCommand cmd;
            Console.WriteLine("!!! DÍGITE O ID PARA DELETAR !!!");
            string id = Console.ReadLine();
            string delete = $"DELETE from dbo.Pessoa WHERE idPessoa = '{id}'";
            cmd = new SqlCommand(delete, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void AddDB(Pessoa pes, SqlConnection conn)
        {
            bool var = true;
            SqlCommand cmd;
            while(var){
            Console.Clear();
            Console.WriteLine("!!! VOCÊ PODE INSERIR ATÉ 1K DE PESSOAS EM UMA TACADA !!!");
            Console.WriteLine("Dígite COMEÇAR || Dígite V");
            string escolha = Console.ReadLine().ToUpper();
            if(escolha == "COMEÇAR"){
                for (int i = 0; i < 1000; i++)
                {
                   Console.WriteLine("Insira o {0}° Usúario", i+1);
                   Console.Write("Nome : ");
                   pes.Nome = Console.ReadLine();
                   Console.Write("CPF : ");
                   pes.Cpf = Console.ReadLine();
                   Console.Write("Idade : ");
                   pes.Idade = int.Parse(Console.ReadLine());

                   //INSERT NO BANCO DE DADOS=====
                   string insert = $"INSERT into dbo.Pessoa (Nome,Cpf,Idade) values ('{pes.Nome}', '{pes.Cpf}', '{pes.Idade}')";
                   cmd = new SqlCommand(insert, conn);
                   conn.Open();
                   cmd.ExecuteNonQuery();
                   conn.Close();
                   //==============================

                   Console.WriteLine("(ENTER) PARA CONTINUAR | (P) PARA PARAR");
                   string escolha2 = Console.ReadLine().ToUpper();
                   if(escolha2 == "P")
                   {
                     break;
                   }
                }            
            }
             else if(escolha == "V"){
                var = false;
            }
          }
        }
    }
}

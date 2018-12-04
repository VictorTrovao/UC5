using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MarioLike.Model;

namespace MarioLikeGame.DAL
{
    public class gameDal
    {
        //declarar o objeto de conexao com o bd
        private SqlConnection conexao;

        //exibir erros
        public string MensagemErro { get; set; }

        public gameDal()
        {
            //criar o objeto para ler a conf
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            //instanciar conexao
            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.LerConexao();
        }

        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            //limpa mensagem de erro
            MensagemErro = "";

            //declarar comando sql
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "Insert into Jogador (Nome_Jogador, Score_Jogador, Data_Score_Jogador, Tempo_Jogador)" +
                "VALUES (@Nome, @Score, @Data, @Tempo);";

            //criar os parametros
            comando.Parameters.AddWithValue("@Nome", placar.NomeJogador);
            comando.Parameters.AddWithValue("@Score", placar.ScoreJogador);
            comando.Parameters.AddWithValue("@Data", placar.DataScoreJogador);
            comando.Parameters.AddWithValue("@Tempo", placar.TempoJogador);

            //executar os comando
            try
            {
                //abrir a conexao
                conexao.Open();

                //executar o comando
                comando.ExecuteNonQuery();

                //se chegou aki fununcio
                resultado = true;
            }
            catch (Exception ex)
            {
                //se entrou aki deu ruim
                MensagemErro = ex.Message;
            }

            finally
            {
                //finalizar fechando a conexao
                conexao.Close();
            }

            return resultado;
        }
        public List<Placar> Listar()
        {
            //inserir a lista
           List<Placar> resultado = new List<Placar>();


            //declarar o comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT TOP 10 Id_Jogador, Nome_Jogador, Score_Jogador, Data_Score_Jogador, Tempo_Jogador " +
                " FROM Jogador ORDER BY Score_Jogador DESC, Tempo_Jogador, Data_Score_Jogador";

            //executar o comando
            try
            {
                //abrir a conexao
                conexao.Open();

                //executar o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //verificar se achou algo
                while (leitor.Read() == true)
                {
                    //instanciar o objeto
                    Placar placar = new Placar();
                    placar.IdJogador = Convert.ToInt32(leitor["Id_Jogador"]);
                    placar.NomeJogador = leitor["Nome_Jogador"].ToString();
                    placar.ScoreJogador = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.DataScoreJogador = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
                    placar.TempoJogador = leitor["Tempo_Jogador"].ToString();

                    //add na lista
                    resultado.Add(placar);
                }
                leitor.Close();
            }
            catch (Exception ex)
            {
                string mensage = ex.Message;

            }
            finally
            {
                conexao.Close();
            }
            return resultado;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MarioLikeGames.Model
{
    class Placar
    {
        private int idJogador;
        private string nomeJogador;
        private int scoreJogador;
        private DateTime dataScoreJogador;
        public Placar()
        {

        }

        public Placar(int idJogador, string nomeJogador, int scoreJogador, DateTime dataScoreJogador)
        {
            IdJogador = idJogador;
            NomeJogador = nomeJogador;
            ScoreJogador = scoreJogador;
            DataScoreJogador = dataScoreJogador;
        }

        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string NomeJogador { get => nomeJogador; set => nomeJogador = value; }
        public int ScoreJogador { get => scoreJogador; set => scoreJogador = value; }
        public DateTime DataScoreJogador { get => dataScoreJogador; set => dataScoreJogador = value; }
    }

}
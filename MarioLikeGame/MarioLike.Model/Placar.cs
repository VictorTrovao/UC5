using System;

namespace MarioLike.Model
{
    public class Placar
    {
        private int idJogador;
        private string nomeJogador;
        private int scoreJogador;
        private DateTime dataScoreJogador;
        private string tempoJogador;

        public Placar()
        {

        }

        public Placar(int idJogador, string nomeJogador, int scoreJogador, DateTime dataScoreJogador, string tempoJogador)
        {
            IdJogador = idJogador;
            NomeJogador = nomeJogador;
            ScoreJogador = scoreJogador;
            DataScoreJogador = dataScoreJogador;
            TempoJogador = tempoJogador;
        }

        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string NomeJogador { get => nomeJogador; set => nomeJogador = value; }
        public int ScoreJogador { get => scoreJogador; set => scoreJogador = value; }
        public DateTime DataScoreJogador { get => dataScoreJogador; set => dataScoreJogador = value; }
        public string TempoJogador { get => tempoJogador; set => tempoJogador = value; }
    }
}

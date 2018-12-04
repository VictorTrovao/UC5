using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace MarioLikeGame.DAL
{
    class LeitorConfiguracao
    {
        public string LerConexao()
        {
            string resultado = "";

            //ler a string de conexao
            resultado = ConfigurationManager.ConnectionStrings
                ["MarioLikeGame.Properties.Settings.Conexao"].ConnectionString;
            return resultado;
        }
    }
}

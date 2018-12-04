using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLikeGame.DAL;

namespace MarioLikeGame
{
    public partial class frmTelaInicial : Form
    {

        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();
        public frmTelaInicial()
        {
            InitializeComponent();
            playSound("smb_world_clear.wav");
            txtNome.MaxLength = 10;

        }

        private void PreencherGrid()
        {
            // declarar a dal
            gameDal GameDal;

            //instanciando a dal na construção do formulario
            GameDal = new gameDal();

            //limpar o data source
            dgvListaRecorde.DataSource = null;

            //listar a dal
            dgvListaRecorde.DataSource = GameDal.Listar();

            //remover coluna id_Jogador
            dgvListaRecorde.Columns.Remove("IdJogador");

            Mudafonte();


        }

        private void pbMario2_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaInicial_Load(object sender, EventArgs e)
        {
            //carregar o grid
            PreencherGrid();

            //Setar o foco para o TextBox: nome do jogador
            txtNome.Focus();
            txtNome.Select();

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
                if (txtNome is TextBox && string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Digite um Nome!");
                    
                }
                else
                {
                    //Não exibir a instância atual da classe
                    this.Visible = false;
                    //Cria uma nova instância do frmTelaJogo()
                    var frm = new frmTelaJogo();
                    //pega o nome do jogador 
                    frm.nomeGamer = txtNome.Text;

                    //Exibir p formulário
                    frm.ShowDialog();
                    PreencherGrid();

                    //Exibir a nova instância da classe
                    this.Visible = true;
                }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void playSound(string nome)
        {
            string url = Application.StartupPath + @"\" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);
        }
        private void stopSound()
        {
            for (int i = sounds.Count - 1; i >= 0; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);
            }

        }

        private void Mudafonte()
        {
            dgvListaRecorde.Columns[0].HeaderText = "Jogador";
            dgvListaRecorde.Columns[1].HeaderText = "Pontos";
            dgvListaRecorde.Columns[2].HeaderText = "Data/Hora";
            dgvListaRecorde.Columns[3].HeaderText = "Tempo";

            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Font = new Font("Tekton Pro", 30, FontStyle.Regular);
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListaRecorde.AutoSize = true;
            dgvListaRecorde.EnableHeadersVisualStyles = false;

            if (dgvListaRecorde.RowCount > 0)
            {
                dgvListaRecorde.CurrentRow.DefaultCellStyle.BackColor = Color.OrangeRed;
            }

            dgvListaRecorde.DefaultCellStyle.Font = new Font("Tekton Pro", 30, FontStyle.Regular);
            dgvListaRecorde.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dgvListaRecorde.DefaultCellStyle.BackColor = Color.Blue;
            dgvListaRecorde.DefaultCellStyle.ForeColor = Color.White;



        }

        private void lblErro_Click(object sender, EventArgs e)
        {

        }
    }
}

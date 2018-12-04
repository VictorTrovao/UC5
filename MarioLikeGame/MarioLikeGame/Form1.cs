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
using MarioLike.Model;

namespace MarioLikeGame
{
    public partial class frmTelaJogo : Form
    {
        //declarando dal
        gameDal GameDal;

        //criar atibuto para pegar nome do jogador
        public string nomeGamer { get; set; }

        // Atributos para controle da movimentação do personagem
        private bool paraCima;
        private bool paraBaixo;
        private bool paraEsquerda;
        private bool paraDireita;

        //Variável para condições de vitória/derrota
        private bool vitoria = false;

        //variável para contagem de pontos
        private int pontos = 0;

        //variável para controlar o cronômetro do jogo
        private int segundos = 0;
        private int minutos = 0;

        //Atributo responsável pela velocidade de locomoção do personagem
        private int velocidade = 20;

        //Biblioteca do Windows Media Player
        //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();

        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();



        private void frmTelaJogo_Load(object sender, EventArgs e)
        {
            //Audio("SMB.mp3", "Play");
            
            playSound("SMB.mp3");
        }

        public frmTelaJogo()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void personagem_Click(object sender, EventArgs e)
        {

        }

        //Movimentar o personagem quando precionar a tecla
        private void frmTelaJogo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                paraDireita = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                paraCima = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = true;
            }

        }

        //Parar o movimento quando soltar a tecla
        private void frmTelaJogo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                paraDireita = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                paraCima = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
            if (paraEsquerda)
            {
                //Movimenta o personagem para esquerda
                personagem.Left -= velocidade;
            }
            if (paraDireita)
            {
                personagem.Left += velocidade;
            }
            if (paraCima)
            {
                personagem.Top -= velocidade;
            }
            if (paraBaixo)
            {
                personagem.Top += velocidade;
            }
            //Posicionamento do personagem dentro da área do formulário (tela)
            if (personagem.Left < 0)
            {
                personagem.Left = 0;
            }
            if (personagem.Left > 1085)
            {
                personagem.Left = 1085;
            }
            if (personagem.Top < 70)
            {
                personagem.Top = 70;
            }
            if (personagem.Top > 680)
            {
                personagem.Top = 680;
            }



            //Loop para checar todos os componentes inseridos no form
            foreach (Control item in this.Controls)
            {

                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {
                    //checa colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        vitoria = false;
                        GameOver(vitoria);
                        removePictureBox();
                        //Audio("smb_mariodie.wav", "Play");
                        playSound("smb_mariodie.wav");
                        GravaHiScore();
                        
                    }
                    
                }
                //Verifica se o jogador colidiu com o coletavel, caso positivo o destrua
                if (item is PictureBox && (string)item.Tag == "coletaveis" || (string)item.Tag == "coletaveis2")
                {
                    //checa a colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        if ((string)item.Tag == "coletaveis")
                        {
                            playSound("smb_coin.wav");
                        }
                        else
                        {
                            playSound("smb_powerup_appears.wav");
                        }
                        
                        //remove o item coletável
                        this.Controls.Remove(item);

                        //Incrementar a variável pontos
                        pontos++;
                        //Audio("smb_coin.wav", "Play");
                        

                        if (pontos == 15)
                        {
                            vitoria = true;
                            GameOver(vitoria);
                            removePictureBox();
                            GravaHiScore();
                            
                        }
                        

                    }
                }

            }

            lblPontos.Text = "Pontos: " + pontos;

            

        }

        private void removePictureBox()
        {
            foreach (Control item in this.Controls)
            {
                if(item is PictureBox && (string)item.Tag !="pbGameOver")
                {
                    ((PictureBox)item).Image = null;
                }
            }
        }

        private void GameOver(bool ganhou)
        {
                        
            personagem.Visible = false;
            btnRestart.Visible = true;
            btnRestart.Focus();
            //Audio("SMB.mp3", "Play");
            
            stopSound();
            if (ganhou)
            {
                pbWin.Visible = true;
                playSound("smb_stage_clear.wav");
            }
            else
            {
                pbGameOver.Visible = true;

                
            }
            timer1.Stop();
            timer2.Stop();
            

        }
        private void GravaHiScore()
        {
            //istanciar a dal
            GameDal = new gameDal();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();

            placar.NomeJogador = this.nomeGamer;

            if (!this.nomeGamer.Equals(""))
            {
                 placar.NomeJogador = this.nomeGamer;
            }
            else
            {
                placar.NomeJogador = "Player 1";
            }

            placar.ScoreJogador = pontos;

            placar.DataScoreJogador = DateTime.Now;

            placar.TempoJogador = minutos.ToString("00") + ":" + segundos.ToString("00");

            //chama o metodo inserir da dal passando o objeto populado como parametro
            if (!GameDal.Inserir(placar))
            {
                // deu ruim
                MessageBox.Show("Erro ao inserir os dados \r\n\r\n" + 
                    GameDal.MensagemErro, "Mario Like Game");
            }
            
        }
        

       

        private void lblGameOver_Click(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            segundos++;

            if (segundos == 60)
            {
                minutos++;
                segundos = 0;                
            }
            lblTempo.Text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            playSound("smb_gameover.wav");
            this.Close();
        }

        //private void Audio(string caminho, string estadoMP)
        //{
        //    //Verifica se ocorreu erro ao instanciar o Windows Media Player
        //    Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //    Tocador.URL = caminho;
        //    if (estadoMP.Equals("Play"))
        //    {
        //        Tocador.controls.play();
        //    }
        //    else if (estadoMP.Equals("Stop"))
        //    {
        //        Tocador.controls.stop();
        //    }
        //}
        //private void Tocador_MediaError(object pMediaObject)
        //{
        //    MessageBox.Show("Deu pau nessa merda");
        //    this.Close();
        //}

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
            for (int i = sounds.Count -1; i >= 0  ; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);
            }
        }

    }
}

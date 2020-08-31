using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using Domain;
using ModelEF;
using Persistence;
using Util;

namespace Client
{
    public partial class AulaForm : System.Web.UI.Page
    {
        //AulaPersistence aulaPersistence = new AulaPersistence();
        AulaDBEntities context = new AulaDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNomeDisciplina.Focus();
                LoadDataPage();
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (IsInvalidForm())
            {
                SendMessage(Message.MSG_REQUIRED_FIELDS, Color.Red);
            }
            else
            {
                try
                {
                    /*Aula aula = new Aula()
                    {
                        NomeDisciplina = txtNomeDisciplina.Text,
                        QuantidadeAluno = int.Parse(txtQuantidadeAluno.Text),
                        NomeProfessor = txtNomeProfessor.Text,
                        NomeFaculdade = txtNomeFaculdade.Text
                    };*/
                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        //aula.Id = int.Parse(txtId.Text);
                        int id = int.Parse(txtId.Text);
                        //aulaPersistence.Update(aula);
                        aula aulaResult = context.aula.First(x => x.id == id);
                        aulaResult.nome_disciplina = txtNomeDisciplina.Text;
                        aulaResult.quantidade_aluno = int.Parse(txtQuantidadeAluno.Text);
                        aulaResult.nome_professor = txtNomeProfessor.Text;
                        aulaResult.nome_faculdade = txtNomeFaculdade.Text;
                        SendMessage(Message.MSG_UPDATE_SUCCESS, Color.Green);
                    }
                    else
                    {
                        aula aula = new aula()
                        {
                            nome_disciplina = txtNomeDisciplina.Text,
                            quantidade_aluno = int.Parse(txtQuantidadeAluno.Text),
                            nome_professor = txtNomeProfessor.Text,
                            nome_faculdade = txtNomeFaculdade.Text
                        };
                        //aulaPersistence.Create(aula);
                        context.aula.Add(aula);
                        SendMessage(Message.MSG_CREATION_SUCCESS, Color.Green);
                    }
                    context.SaveChanges();
                    ResetForm(true);
                }
                catch (Exception ex)
                {
                    SendMessage($"{Message.MSG_ERROR} {ex.Message}", Color.Red);
                }
            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            ResetForm(false);
        }
        private bool IsInvalidForm()
        {
            return string.IsNullOrWhiteSpace(txtNomeDisciplina.Text) ||
                   string.IsNullOrWhiteSpace(txtQuantidadeAluno.Text) ||
                   string.IsNullOrWhiteSpace(txtNomeProfessor.Text) ||
                   string.IsNullOrWhiteSpace(txtNomeFaculdade.Text);
        }
        private void ResetForm(bool clearId)
        {
            if (clearId)
            {
                txtId.Text = String.Empty;
            }
            txtNomeDisciplina.Text = String.Empty;
            txtQuantidadeAluno.Text = String.Empty;
            txtNomeProfessor.Text = String.Empty;
            txtNomeFaculdade.Text = String.Empty;
            txtNomeDisciplina.Focus();
        }
        private void LoadDataPage()
        {
            string id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id))
            {
                //Aula aula = aulaPersistence.FindOne(int.Parse(id));
                int newId = Convert.ToInt32(id);
                var aulaResult = context.aula.First(x => x.id == newId);
                txtId.Text = aulaResult.id.ToString();
                txtNomeDisciplina.Text = aulaResult.nome_disciplina;
                txtQuantidadeAluno.Text = aulaResult.quantidade_aluno.ToString();
                txtNomeProfessor.Text = aulaResult.nome_professor;
                txtNomeFaculdade.Text = aulaResult.nome_faculdade;
            }
        }

        private void SendMessage(string message, Color color)
        {
            lblMensagem.Text = message;
            lblMensagem.ForeColor = color;
            lblMensagem.Font.Bold = true;
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "showMessage();", true);
        }
    }
}
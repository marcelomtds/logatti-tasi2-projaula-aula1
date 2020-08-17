using System;
using System.Drawing;
using System.Web.UI;
using Domain;
using Persistence;
using Util;

namespace Client
{
    public partial class AulaForm : System.Web.UI.Page
    {
        AulaPersistence aulaPersistence = new AulaPersistence();
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
                    Aula aula = new Aula()
                    {
                        NomeDisciplina = txtNomeDisciplina.Text,
                        QuantidadeAluno = int.Parse(txtQuantidadeAluno.Text),
                        NomeProfessor = txtNomeProfessor.Text,
                        NomeFaculdade = txtNomeFaculdade.Text
                    };
                    if (!string.IsNullOrWhiteSpace(txtId.Text))
                    {
                        aula.Id = int.Parse(txtId.Text);
                        aulaPersistence.Update(aula);
                        SendMessage(Message.MSG_UPDATE_SUCCESS, Color.Green);
                    }
                    else
                    {
                        aulaPersistence.Create(aula);
                        SendMessage(Message.MSG_CREATION_SUCCESS, Color.Green);
                    }
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
                Aula aula = aulaPersistence.FindOne(int.Parse(id));
                txtId.Text = aula.Id.ToString();
                txtNomeDisciplina.Text = aula.NomeDisciplina;
                txtQuantidadeAluno.Text = aula.QuantidadeAluno.ToString();
                txtNomeProfessor.Text = aula.NomeProfessor;
                txtNomeFaculdade.Text = aula.NomeFaculdade;
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
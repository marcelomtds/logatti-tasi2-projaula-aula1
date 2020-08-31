using Domain;
using ModelEF;
using Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace Client
{
    public partial class AulaList : System.Web.UI.Page
    {
        //AulaPersistence aulaPersistence = new AulaPersistence();
        AulaDBEntities context = new AulaDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
        protected void GVResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int line = int.Parse(e.CommandArgument.ToString());
            lblSelectedId.Text = gvResult.Rows[line].Cells[0].Text;
            if (e.CommandName == "A")
            {
                Response.Redirect("AulaForm.aspx?id=" + int.Parse(lblSelectedId.Text));
            }
            else if (e.CommandName == "E")
            {
                DisplayModal(this);
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lblSelectedId.Text);
                //aulaPersistence.Delete(int.Parse(lblSelectedId.Text));
                context.aula.Remove(context.aula.First(x => x.id == id));
                context.SaveChanges();
                SendMessage(Message.MSG_DELETE_SUCCESS, Color.Green);
                LoadGridView();
            }
            catch (Exception ex)
            {
                SendMessage($"{Message.MSG_ERROR} {ex.Message}", Color.Red);
            }
        }
        private void LoadGridView()
        {
            //gvResult.DataSource = aulaPersistence.FindAll();
            gvResult.DataSource = context.aula.ToList();
            gvResult.DataBind();
        }
        private void DisplayModal(Page page)
        {
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "openModalConfirmation();", true);
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
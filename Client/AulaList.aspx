<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AulaList.aspx.cs" Inherits="Client.AulaList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js" integrity="sha384-B4gt1jrGC7Jh4AgTPSdUtOBvfO8shuf57BaghqFfPlYxofvL8/KUEfYiJOMMV+rV" crossorigin="anonymous"></script>
    <title>Aula</title>
    <script type="text/javascript">
        function openModalConfirmation() {
            $('#modalConfirmation').modal('show');
        }
        function showMessage() {
            $('.toast').toast('show');
        }
    </script>
</head>
<body>
    <div class="toast ml-auto" role="alert" data-delay="5000" data-autohide="true" style="margin-right: 15px; margin-top: 15px;">
        <div class="toast-header">
            <strong class="mr-auto text-primary">Mensagem</strong>
            <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                <span aria-hidden="true">×</span>
            </button>
        </div>
        <div class="toast-body">
            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="container">
        <form id="form" runat="server">
            <asp:Label ID="lblSelectedId" runat="server" Visible="false" Text=""></asp:Label>
            <div class="row" style="margin-top: 15px;">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button class="btn btn-success" ID="btnNovo" runat="server" Text="Criar Nova Aula" PostBackUrl="~/AulaForm.aspx" />
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" OnRowCommand="GVResult_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="NomeDisciplina" HeaderText="Disciplina" />
                        <asp:BoundField DataField="QuantidadeAluno" HeaderText="Quantidde de Aluno" />
                        <asp:BoundField DataField="NomeProfessor" HeaderText="Professor" />
                        <asp:BoundField DataField="NomeFaculdade" HeaderText="Faculdade" />
                        <asp:ButtonField ButtonType="Image" HeaderText="Alterar" CommandName="A" ControlStyle-Width="18" ImageUrl="~/img/alterar.png"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" HeaderText="Excluir" CommandName="E" ControlStyle-Width="18" ImageUrl="~/img/excluir.png"></asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal fade" id="modalConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Confirmação de Exclusão</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Deseja excluir o registro selecionado?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                            <asp:Button ID="btnConfirm" CssClass="btn btn-danger" runat="server" Text="Confirmar" OnClick="btnConfirm_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script>
        var table = document.getElementById("gvResult");
        table.classList.add("table");
        table.classList.add("table-hover");
    </script>
</body>
</html>

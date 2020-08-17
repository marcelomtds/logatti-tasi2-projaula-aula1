using Connection;
using Domain;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistence
{
    public class AulaPersistence
    {
        StringBuilder sb;
        SQLServer connection;
        public void Delete(int id)
        {
            sb = new StringBuilder();
            sb.Append($"DELETE FROM aula WHERE id = {id};");
            using (connection = new SQLServer())
            {
                connection.ExecuteCommand(sb.ToString());
            }
        }
        public void Create(Aula aula)
        {
            sb = new StringBuilder();
            sb.Append("INSERT INTO aula (nome_disciplina, quantidade_aluno, nome_professor, nome_faculdade) ");
            sb.Append($"VALUES ('{aula.NomeDisciplina}', {aula.QuantidadeAluno}, '{aula.NomeProfessor}', '{aula.NomeFaculdade}');");
            using (connection = new SQLServer())
            {
                connection.ExecuteCommand(sb.ToString());
            }
        }
        public void Update(Aula aula)
        {
            sb = new StringBuilder();
            sb.Append($"UPDATE aula SET nome_disciplina = '{aula.NomeDisciplina}', quantidade_aluno = {aula.QuantidadeAluno}, nome_professor = '{aula.NomeProfessor}', nome_faculdade = '{aula.NomeFaculdade}' ");
            sb.Append($"WHERE id = {aula.Id};");
            using (connection = new SQLServer())
            {
                connection.ExecuteCommand(sb.ToString());
            }
        }
        public List<Aula> FindAll()
        {
            using (connection = new SQLServer())
            {
                var sql = "SELECT " +
                             "id, " +
                             "nome_disciplina, " +
                             "quantidade_aluno, " +
                             "nome_professor, " +
                             "nome_faculdade " +
                          "FROM aula; ";
                return ProduceResult(connection.ExecuteCommandWithReturn(sql));
            }
        }
        public Aula FindOne(int id)
        {
            using (connection = new SQLServer())
            {
                var sql = "SELECT " +
                             "id, " +
                             "nome_disciplina, " +
                             "quantidade_aluno, " +
                             "nome_professor, " +
                             "nome_faculdade " +
                          "FROM aula " +
                         $"WHERE id = {id};";
                return ProduceResult(connection.ExecuteCommandWithReturn(sql))[0];
            }
        }
        private List<Aula> ProduceResult(SqlDataReader reader)
        {
            List<Aula> aulas = new List<Aula>();
            while (reader.Read())
            {
                Aula aula = new Aula()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    NomeDisciplina = reader["nome_disciplina"].ToString(),
                    QuantidadeAluno = int.Parse(reader["quantidade_aluno"].ToString()),
                    NomeProfessor = reader["nome_professor"].ToString(),
                    NomeFaculdade = reader["nome_faculdade"].ToString(),
                };
                aulas.Add(aula);
            }
            return aulas;
        }
    }
}
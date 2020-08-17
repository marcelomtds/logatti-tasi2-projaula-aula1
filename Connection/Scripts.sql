CREATE TABLE aula (
	id INT IDENTITY (1, 1) NOT NULL,
	nome_disciplina VARCHAR (100) NOT NULL,
	quantidade_aluno INT NOT NULL,
	nome_professor VARCHAR (100) NOT NULL,
	nome_faculdade VARCHAR (100) NOT NULL,
	CONSTRAINT pk_aula PRIMARY KEY (id)
);
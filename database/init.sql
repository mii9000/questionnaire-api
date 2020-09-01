CREATE DATABASE [questionnaire_db]
GO

USE [questionnaire_db];
GO

-- BEGIN TABLE dbo.answers
CREATE TABLE dbo.answers (
	id int NOT NULL IDENTITY(1,1),
	[text] nvarchar(250) NOT NULL,
	[order] int NOT NULL
);
GO

ALTER TABLE dbo.answers ADD CONSTRAINT PK__answer_o__3213E83FFF59AE02 PRIMARY KEY (id);
GO

-- Inserting 13 rows into dbo.answers
-- Insert batch #1
SET IDENTITY_INSERT dbo.answers ON
GO

INSERT INTO dbo.answers (id, [text], [order]) VALUES
(1, 'Mr.', 0),
(2, 'Ms.', 1),
(3, 'Mrs.', 2),
(4, 'Thailand', 0),
(5, 'Australia', 1),
(6, 'Canada', 2),
(7, 'Vietnam', 3),
(8, 'Cambodia', 4),
(9, 'Engineer', 0),
(10, 'Architect', 1),
(11, 'Doctor', 2),
(12, 'Administrator', 3),
(13, 'Executive', 4);

SET IDENTITY_INSERT dbo.answers OFF
GO

-- END TABLE dbo.answers

-- BEGIN TABLE dbo.question_answers
CREATE TABLE dbo.question_answers (
	question_id int NOT NULL,
	answer_id int NOT NULL
);
GO

ALTER TABLE dbo.question_answers ADD CONSTRAINT PK__question__BDF53178F41E4059 PRIMARY KEY (question_id, answer_id);
GO

-- Inserting 13 rows into dbo.question_answers
-- Insert batch #1
INSERT INTO dbo.question_answers (question_id, answer_id) VALUES
(1, 1),
(1, 2),
(1, 3),
(7, 4),
(7, 5),
(7, 6),
(7, 7),
(7, 8),
(12, 9),
(12, 10),
(12, 11),
(12, 12),
(12, 13);

-- END TABLE dbo.question_answers

-- BEGIN TABLE dbo.question_groups
CREATE TABLE dbo.question_groups (
	id int NOT NULL IDENTITY(1,1),
	[text] nvarchar(150) NOT NULL,
	[order] int NOT NULL
);
GO

ALTER TABLE dbo.question_groups ADD CONSTRAINT PK__question__3213E83F871F89C6 PRIMARY KEY (id);
GO

-- Inserting 3 rows into dbo.question_groups
-- Insert batch #1
SET IDENTITY_INSERT dbo.question_groups ON
GO

INSERT INTO dbo.question_groups (id, [text], [order]) VALUES
(1, 'Personal Information', 0),
(2, 'Address', 1),
(3, 'Occupation', 2);

SET IDENTITY_INSERT dbo.question_groups OFF
GO

-- END TABLE dbo.question_groups

-- BEGIN TABLE dbo.question_types
CREATE TABLE dbo.question_types (
	id int NOT NULL IDENTITY(1,1),
	[text] nvarchar(100) NOT NULL
);
GO

ALTER TABLE dbo.question_types ADD CONSTRAINT PK__question__3213E83F4EC342C5 PRIMARY KEY (id);
GO

-- Inserting 3 rows into dbo.question_types
-- Insert batch #1
SET IDENTITY_INSERT dbo.question_types ON
GO

INSERT INTO dbo.question_types (id, [text]) VALUES
(1, 'input_text'),
(2, 'select_one'),
(3, 'multi_select');

SET IDENTITY_INSERT dbo.question_types OFF
GO

-- END TABLE dbo.question_types

-- BEGIN TABLE dbo.questionnaire_answers
CREATE TABLE dbo.questionnaire_answers (
	id int NOT NULL IDENTITY(1,1),
	questionnaire_question_id int NOT NULL,
	answer_id int NULL DEFAULT (NULL),
	answer_text nvarchar(250) NULL DEFAULT ('NULL')
);
GO

ALTER TABLE dbo.questionnaire_answers ADD CONSTRAINT PK__question__3213E83F20BFD925 PRIMARY KEY (id);
GO

-- Inserting 6 rows into dbo.questionnaire_answers
-- Insert batch #1

SET IDENTITY_INSERT dbo.questionnaire_answers ON
GO

INSERT INTO dbo.questionnaire_answers (id, questionnaire_question_id, answer_id, answer_text) VALUES
(13, 13, 1, NULL),
(14, 14, NULL, 'John'),
(15, 15, NULL, 'Doe'),
(16, 16, NULL, '18th Oct 1989'),
(17, 17, 4, NULL),
(18, 17, 5, NULL);

SET IDENTITY_INSERT dbo.questionnaire_answers OFF
GO

-- END TABLE dbo.questionnaire_answers

-- BEGIN TABLE dbo.questionnaire_questions
CREATE TABLE dbo.questionnaire_questions (
	id int NOT NULL IDENTITY(1,1),
	questionnaire_id int NOT NULL,
	question_group_id int NOT NULL,
	question_id int NOT NULL
);
GO

ALTER TABLE dbo.questionnaire_questions ADD CONSTRAINT PK__question__3213E83F7BD5A37F PRIMARY KEY (id);
GO

-- Inserting 5 rows into dbo.questionnaire_questions
-- Insert batch #1

SET IDENTITY_INSERT dbo.questionnaire_questions ON
GO

INSERT INTO dbo.questionnaire_questions (id, questionnaire_id, question_group_id, question_id) VALUES
(13, 7, 1, 1),
(14, 7, 1, 2),
(17, 7, 1, 7),
(16, 7, 1, 8),
(15, 7, 1, 9);

SET IDENTITY_INSERT dbo.questionnaire_questions OFF
GO

-- END TABLE dbo.questionnaire_questions

-- BEGIN TABLE dbo.questionnaires
CREATE TABLE dbo.questionnaires (
	id int NOT NULL IDENTITY(1,1),
	user_id int NOT NULL,
	is_completed bit NOT NULL,
	last_updated datetime NOT NULL
);
GO

ALTER TABLE dbo.questionnaires ADD CONSTRAINT PK__question__3213E83F25B3E8C3 PRIMARY KEY (id);
GO

-- Inserting 1 row into dbo.questionnaires
-- Insert batch #1

SET IDENTITY_INSERT dbo.questionnaires ON
GO

INSERT INTO dbo.questionnaires (id, user_id, is_completed, last_updated) VALUES
(7, 1, 0, '2020-09-01 08:43:35.617');

SET IDENTITY_INSERT dbo.questionnaires OFF
GO

-- END TABLE dbo.questionnaires

-- BEGIN TABLE dbo.questions
CREATE TABLE dbo.questions (
	id int NOT NULL IDENTITY(1,1),
	question_group_id int NOT NULL,
	question_type_id int NOT NULL,
	[order] int NOT NULL,
	[text] nvarchar(250) NOT NULL
);
GO

ALTER TABLE dbo.questions ADD CONSTRAINT PK__question__3213E83FF3097C15 PRIMARY KEY (id);
GO

-- Inserting 10 rows into dbo.questions
-- Insert batch #1

SET IDENTITY_INSERT dbo.questions ON
GO

INSERT INTO dbo.questions (id, question_group_id, question_type_id, [order], [text]) VALUES
(1, 1, 2, 0, 'Title'),
(2, 1, 1, 1, 'First Name'),
(7, 1, 2, 4, 'Country of residence'),
(8, 1, 1, 3, 'Date of birth'),
(9, 1, 1, 2, 'Last Name'),
(10, 3, 1, 2, 'Business Type'),
(11, 3, 1, 1, 'Job Title'),
(12, 3, 2, 0, 'Occupation'),
(13, 2, 1, 1, 'Work'),
(14, 2, 1, 0, 'House');

SET IDENTITY_INSERT dbo.questions OFF
GO

-- END TABLE dbo.questions

-- BEGIN TABLE dbo.restricted_question_answers
CREATE TABLE dbo.restricted_question_answers (
	restriction_id int NOT NULL,
	question_id int NOT NULL,
	answer_id int NOT NULL
);
GO

ALTER TABLE dbo.restricted_question_answers ADD CONSTRAINT PK__restrict__6254316FDA122F81 PRIMARY KEY (restriction_id, question_id, answer_id);
GO

-- Inserting 2 rows into dbo.restricted_question_answers
-- Insert batch #1
INSERT INTO dbo.restricted_question_answers (restriction_id, question_id, answer_id) VALUES
(2, 7, 6),
(2, 7, 8);

-- END TABLE dbo.restricted_question_answers

-- BEGIN TABLE dbo.restrictions
CREATE TABLE dbo.restrictions (
	id int NOT NULL IDENTITY(1,1),
	[text] nvarchar(250) NOT NULL
);
GO

ALTER TABLE dbo.restrictions ADD CONSTRAINT PK__restrict__3213E83F196BB53D PRIMARY KEY (id);
GO

-- Inserting 1 row into dbo.restrictions
-- Insert batch #1

SET IDENTITY_INSERT dbo.restrictions ON
GO

INSERT INTO dbo.restrictions (id, [text]) VALUES
(2, 'Blocked Countries');

SET IDENTITY_INSERT dbo.restrictions OFF
GO

-- END TABLE dbo.restrictions

-- BEGIN TABLE dbo.users
CREATE TABLE dbo.users (
	id int NOT NULL IDENTITY(1,1),
	email nvarchar(100) NOT NULL,
	name nvarchar(150) NOT NULL
);
GO

ALTER TABLE dbo.users ADD CONSTRAINT PK__users__3213E83F09FD8054 PRIMARY KEY (id);
GO

-- Inserting 1 row into dbo.users
-- Insert batch #1

SET IDENTITY_INSERT dbo.users ON
GO

INSERT INTO dbo.users (id, email, name) VALUES
(1, 'john.doe@email.com', 'Joh Doe');

SET IDENTITY_INSERT dbo.users OFF
GO

-- END TABLE dbo.users

IF OBJECT_ID('dbo.questionnaire_answers', 'U') IS NOT NULL AND OBJECT_ID('dbo.answers', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaire_answers
	ADD CONSTRAINT FK_questionnaire_answer_id
	FOREIGN KEY (answer_id)
	REFERENCES dbo.answers (id);

IF OBJECT_ID('dbo.questionnaire_answers', 'U') IS NOT NULL AND OBJECT_ID('dbo.questionnaire_questions', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaire_answers
	ADD CONSTRAINT FK_questionnaire_questionnaire_question_id
	FOREIGN KEY (questionnaire_question_id)
	REFERENCES dbo.questionnaire_questions (id);

IF OBJECT_ID('dbo.questionnaire_questions', 'U') IS NOT NULL AND OBJECT_ID('dbo.question_groups', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaire_questions
	ADD CONSTRAINT FK_questionnaire_question_group_id
	FOREIGN KEY (question_group_id)
	REFERENCES dbo.question_groups (id);

IF OBJECT_ID('dbo.questionnaire_questions', 'U') IS NOT NULL AND OBJECT_ID('dbo.questions', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaire_questions
	ADD CONSTRAINT FK_questionnaire_question_id
	FOREIGN KEY (question_id)
	REFERENCES dbo.questions (id);

IF OBJECT_ID('dbo.questionnaire_questions', 'U') IS NOT NULL AND OBJECT_ID('dbo.questionnaires', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaire_questions
	ADD CONSTRAINT FK_questionnaire_questionnaire_id
	FOREIGN KEY (questionnaire_id)
	REFERENCES dbo.questionnaires (id);

IF OBJECT_ID('dbo.questionnaires', 'U') IS NOT NULL AND OBJECT_ID('dbo.users', 'U') IS NOT NULL
	ALTER TABLE dbo.questionnaires
	ADD CONSTRAINT FK_user_id
	FOREIGN KEY (user_id)
	REFERENCES dbo.users (id);

IF OBJECT_ID('dbo.questions', 'U') IS NOT NULL AND OBJECT_ID('dbo.question_groups', 'U') IS NOT NULL
	ALTER TABLE dbo.questions
	ADD CONSTRAINT FK_question_group_id
	FOREIGN KEY (question_group_id)
	REFERENCES dbo.question_groups (id);

IF OBJECT_ID('dbo.questions', 'U') IS NOT NULL AND OBJECT_ID('dbo.question_types', 'U') IS NOT NULL
	ALTER TABLE dbo.questions
	ADD CONSTRAINT FK_question_type_id
	FOREIGN KEY (question_type_id)
	REFERENCES dbo.question_types (id);

IF OBJECT_ID('dbo.restricted_question_answers', 'U') IS NOT NULL AND OBJECT_ID('dbo.questions', 'U') IS NOT NULL
	ALTER TABLE dbo.restricted_question_answers
	ADD CONSTRAINT FK_restrictions_question_id
	FOREIGN KEY (question_id)
	REFERENCES dbo.questions (id);

IF OBJECT_ID('dbo.restricted_question_answers', 'U') IS NOT NULL AND OBJECT_ID('dbo.restrictions', 'U') IS NOT NULL
	ALTER TABLE dbo.restricted_question_answers
	ADD CONSTRAINT FK_restrictions_restriction_id
	FOREIGN KEY (restriction_id)
	REFERENCES dbo.restrictions (id);

IF OBJECT_ID('dbo.restricted_question_answers', 'U') IS NOT NULL AND OBJECT_ID('dbo.answers', 'U') IS NOT NULL
	ALTER TABLE dbo.restricted_question_answers
	ADD CONSTRAINT FK_restrictions_answer_id
	FOREIGN KEY (answer_id)
	REFERENCES dbo.answers (id);
    
----------------------------
-- ���ƣ� ɭ����߿���ϵͳ���ݿ�ű�(MySQL)
-- ���ߣ� �ܽ���
-- ʱ�䣺 2016.05.05
----------------------------

-- CREATE DATABASE SLOES

------------------------------------------ �γ̼��½� ------------------------------------------

-- �γ���Ϣ��
CREATE TABLE Course(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
Name VARCHAR(20),			    		-- �γ�����
Description VARCHAR(200),				-- �γ�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- �½���Ϣ��
CREATE TABLE Chapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �γ�����ID
ChapterIndex INT,						-- �½����
Name VARCHAR(30),						-- �½�����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_chapter_course FOREIGN KEY (CourseID) REFERENCES Course(ID) ON DELETE CASCADE
);

------------------------------------------ ������� ------------------------------------------

    --********************  ����  *********************
    --*
    --*  1����ѡ��
    --*  2����ѡ��
    --*  3���ж���
	--*  4�������
	--*  5�������
    --*
	--*************************************************
	
-- ��ѡ�����
CREATE TABLE SingleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChapterID INT,							-- �½�ID

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(1),							-- ��
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_singleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ��ѡ�����
CREATE TABLE MultipleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChapterID INT,							-- �½�ID

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(20),						-- �� (�����ĶٺŸ���,�� A��B �� B��C��D)
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_multipleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- �ж������
CREATE TABLE JudgeItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChapterID INT,							-- �½�ID

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Answer INT,								-- �� (0: ����  1: ��ȷ)
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_judgeitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ��������
CREATE TABLE BlankItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChapterID INT,							-- �½�ID

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_blankitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- ������
CREATE TABLE BlankAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
BlankItemID INT,						-- ���������ID
AnswerIndex INT,						-- �����
Answer VARCHAR(500),					-- ��
Annotation VARCHAR(500),				-- ע��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_blankanswer_blankitem FOREIGN KEY (BlankItemID)REFERENCES BlankItem(ID) ON DELETE CASCADE
);

-- ��������
CREATE TABLE WordItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChapterID INT,							-- �½�ID

Title VARCHAR(1000),					-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Answer VARCHAR(2000),					-- ��
Annotation VARCHAR(500),				-- ע��
Difficulty INT,							-- �������׶� (1-5������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_worditem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

------------------------------------------ �Ծ����� ------------------------------------------

-- �Ծ���Ϣ��
CREATE TABLE Paper(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
CourseID INT,							-- �Ծ������γ�����ID
PaperType INT,							-- �Ծ����� (0��ģ���Ծ�  1�������Ծ�)
Name VARCHAR(100),						-- �Ծ�����
Duration INT,							-- ����ʱ�� (��λ:����)
AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_paper_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE
);

-- �Ծ�ѡ��
CREATE TABLE PaperSingle(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(500),						-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(1),							-- ��
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_papersingle_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- �Ծ��ѡ��
CREATE TABLE PaperMultiple(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(500),						-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
A VARCHAR(500),							-- ѡ��A
B VARCHAR(500),							-- ѡ��B
C VARCHAR(500),							-- ѡ��C
D VARCHAR(500),							-- ѡ��D
Answer CHAR(20),						-- �� (�����ĶٺŸ���,�� A��B �� B��C��D)
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_papermultiple_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- �Ծ��ж���
CREATE TABLE PaperJudge(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ParperID INT,							-- �Ծ�����ID
ItemIndex INT,							-- �������

Title VARCHAR(500),						-- ��������
Image MEDIUMBLOB,						-- ��ĿͼƬ(�ɿ�)
Answer INT,								-- �� (0: ����  1: ��ȷ)
Annotation VARCHAR(500),				-- ע��

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_paperjudge_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

------------------------------------------ �˺���Ϣ ------------------------------------------

-- ��ʦ�˺���Ϣ��
CREATE TABLE Teacher(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
ChineseName VARCHAR(20),				-- ����
UserName VARCHAR(30),					-- �û���
Password VARCHAR(32),					-- ����
Level INT,								-- �˺ż��� (0: ������Ա  1��ϵͳ����Ա)
IsCanMarkPaper INT,						-- �Ƿ�ӵ���ľ���(0����  1����)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ѧ����Ϣ��
CREATE TABLE Student(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID

ChineseName VARCHAR(20),				-- ����
Phone VARCHAR(20),						-- �绰����
UserName VARCHAR(30),					-- �û���(Ĭ��Ϊ�绰)
Password VARCHAR(50),					-- ����(Ĭ��Ϊ�绰����λ)
State INT DEFAULT 0,					-- �˺�״̬ (0������  1������)

AddPerson VARCHAR(20),					-- �����
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

----------------------------------------- ѧ������ -----------------------------------------

-- ѧ��������Ϣ��
CREATE TABLE StudentError(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
StudentID INT,							-- ѧ������ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID
Note VARCHAR(200),						-- �û��ʼ�
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_studenterror_user FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE
);

-- ѧ���ղ���Ϣ��
CREATE TABLE StudentFavorite(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
StudentID INT,							-- ѧ������ID
ItemType INT,							-- ����
IsPaperItem INT,						-- �Ƿ�Ϊ�Ծ����� (0������  1����)
ItemID INT,								-- ��������ID
Note VARCHAR(100),						-- �û��ʼ�
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_studentfavorite_user FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE
);

-- ѧ���ɼ���Ϣ��
CREATE TABLE StudentScore(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
StudentID INT,							-- ѧ������ID
PaperID INT,							-- �Ծ�����ID
UsedTime INT,							-- ��ʱ (��λ����)
Score INT,								-- ����
IsNeedMark INT,							-- �Ƿ���Ҫ�˹��ľ� (0����  1����)
IsFinishedMark INT,						-- �Ƿ����ľ� (0����  1����)
MarkTeacherID INT,						-- �ľ���ʦ����ID(�ɿ�)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0,  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
CONSTRAINT fk_studentscore_student FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE,
CONSTRAINT fk_studentscore_paper FOREIGN KEY (PaperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

---------------------------------------  ��¼���� ------------------------------------------------

-- �ͻ��˵�¼��¼
CREATE TABLE ClientLoginRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
StudentID INT,							-- ѧ������ID
IP VARCHAR(15),							-- ��¼IP
TerminalType INT,						-- �ն�����  (0��PC  1��Android   2: IOS   3: Web)
PlatformVersion VARCHAR(30),			-- �ն�����ƽ̨�汾���� Windows7�� Android 4.2.2�� IOS 8.1  �� IE10
AppVersion VARCHAR(10),					-- App�汾
LoginTime DATETIME,						-- ��¼ʱ��
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

-- ��ʦ������¼
CREATE TABLE TeacherDoRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
TeacherID INT,							-- ��ʦ����ID
TeacherName VARCHAR(50),				-- ��ʦ����
DoTime DATETIME,						-- ����ʱ��
DoName VARCHAR(100),					-- ��������
DoContent VARCHAR(100),					-- ��������
Remark VARCHAR(100),					-- ��ע
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);

--���������Ϣ��
CREATE TABLE Feedback(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- ����ID
Content VARCHAR(500),					-- ��������
Contact VARCHAR(30),					-- ��ϵ��ʽ
TerminalType INT,						-- �����ն�;�� (0��PC  1��Android  2��IOS  3��Web)
AddTime DATETIME DEFAULT NOW(),			-- ���ʱ��
IsDeleted INT DEFAULT 0  				-- �Ƿ���ɾ�� (0��δɾ��  1����ɾ��)
);








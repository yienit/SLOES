----------------------------
-- 名称： 森岚在线考试系统数据库脚本(MySQL)
-- 作者： 曹江波
-- 时间： 2016.05.05
----------------------------

-- CREATE DATABASE SLOES

------------------------------------------ 课程及章节 ------------------------------------------

-- 课程信息表
CREATE TABLE Course(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
Name VARCHAR(20),			    		-- 课程名称
Description VARCHAR(200),				-- 课程描述
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 章节信息表
CREATE TABLE Chapter(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 课程主键ID
ChapterIndex INT,						-- 章节序号
Name VARCHAR(30),						-- 章节名称
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_chapter_course FOREIGN KEY (CourseID) REFERENCES Course(ID) ON DELETE CASCADE
);

------------------------------------------ 题库数据 ------------------------------------------

    --********************  题型  *********************
    --*
    --*  1：单选题
    --*  2：多选题
    --*  3：判断题
	--*  4：填空题
	--*  5：简答题
    --*
	--*************************************************
	
-- 单选题题库
CREATE TABLE SingleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChapterID INT,							-- 章节ID

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(1),							-- 答案
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_singleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 多选题题库
CREATE TABLE MultipleItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChapterID INT,							-- 章节ID

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(20),						-- 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_multipleitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 判断题题库
CREATE TABLE JudgeItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChapterID INT,							-- 章节ID

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Answer INT,								-- 答案 (0: 错误  1: 正确)
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_judgeitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 填空题题库
CREATE TABLE BlankItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChapterID INT,							-- 章节ID

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_blankitem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

-- 填空题答案
CREATE TABLE BlankAnswer(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
BlankItemID INT,						-- 填空题主键ID
AnswerIndex INT,						-- 答案序号
Answer VARCHAR(500),					-- 答案
Annotation VARCHAR(500),				-- 注解
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_blankanswer_blankitem FOREIGN KEY (BlankItemID)REFERENCES BlankItem(ID) ON DELETE CASCADE
);

-- 简答题题库
CREATE TABLE WordItem(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChapterID INT,							-- 章节ID

Title VARCHAR(1000),					-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Answer VARCHAR(2000),					-- 答案
Annotation VARCHAR(500),				-- 注解
Difficulty INT,							-- 试题难易度 (1-5：评级)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_worditem_chapter FOREIGN KEY (ChapterID)REFERENCES Chapter(ID) ON DELETE CASCADE
);

------------------------------------------ 试卷数据 ------------------------------------------

-- 试卷信息表
CREATE TABLE Paper(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
CourseID INT,							-- 试卷所属课程主键ID
PaperType INT,							-- 试卷类型 (0：模拟试卷  1：历届试卷)
Name VARCHAR(100),						-- 试卷名称
Duration INT,							-- 考试时长 (单位:分钟)
AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_paper_course FOREIGN KEY (CourseID)REFERENCES Course(ID) ON DELETE CASCADE
);

-- 试卷单选题
CREATE TABLE PaperSingle(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(500),						-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(1),							-- 答案
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_papersingle_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- 试卷多选题
CREATE TABLE PaperMultiple(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(500),						-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
A VARCHAR(500),							-- 选项A
B VARCHAR(500),							-- 选项B
C VARCHAR(500),							-- 选项C
D VARCHAR(500),							-- 选项D
Answer CHAR(20),						-- 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_papermultiple_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

-- 试卷判断题
CREATE TABLE PaperJudge(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ParperID INT,							-- 试卷主键ID
ItemIndex INT,							-- 试题序号

Title VARCHAR(500),						-- 标题文字
Image MEDIUMBLOB,						-- 题目图片(可空)
Answer INT,								-- 答案 (0: 错误  1: 正确)
Annotation VARCHAR(500),				-- 注解

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_paperjudge_paper FOREIGN KEY (ParperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

------------------------------------------ 账号信息 ------------------------------------------

-- 老师账号信息表
CREATE TABLE Teacher(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
ChineseName VARCHAR(20),				-- 姓名
UserName VARCHAR(30),					-- 用户名
Password VARCHAR(32),					-- 密码
Level INT,								-- 账号级别 (0: 题库管理员  1：系统管理员)
IsCanMarkPaper INT,						-- 是否拥有阅卷功能(0：否  1：是)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 学生信息表
CREATE TABLE Student(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID

ChineseName VARCHAR(20),				-- 姓名
Phone VARCHAR(20),						-- 电话号码
UserName VARCHAR(30),					-- 用户名(默认为电话)
Password VARCHAR(50),					-- 密码(默认为电话后六位)
State INT DEFAULT 0,					-- 账号状态 (0：正常  1：禁用)

AddPerson VARCHAR(20),					-- 添加人
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

----------------------------------------- 学生数据 -----------------------------------------

-- 学生错题信息表
CREATE TABLE StudentError(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
StudentID INT,							-- 学生主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID
Note VARCHAR(200),						-- 用户笔记
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_studenterror_user FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE
);

-- 学生收藏信息表
CREATE TABLE StudentFavorite(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
StudentID INT,							-- 学生主键ID
ItemType INT,							-- 题型
IsPaperItem INT,						-- 是否为试卷试题 (0：不是  1：是)
ItemID INT,								-- 试题主键ID
Note VARCHAR(100),						-- 用户笔记
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_studentfavorite_user FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE
);

-- 学生成绩信息表
CREATE TABLE StudentScore(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
StudentID INT,							-- 学生主键ID
PaperID INT,							-- 试卷主键ID
UsedTime INT,							-- 用时 (单位分钟)
Score INT,								-- 分数
IsNeedMark INT,							-- 是否需要人工阅卷 (0：否  1：是)
IsFinishedMark INT,						-- 是否已阅卷 (0：否  1：是)
MarkTeacherID INT,						-- 阅卷老师主键ID(可空)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0,  				-- 是否已删除 (0：未删除  1：已删除)
CONSTRAINT fk_studentscore_student FOREIGN KEY (StudentID)REFERENCES Student(ID) ON DELETE CASCADE,
CONSTRAINT fk_studentscore_paper FOREIGN KEY (PaperID)REFERENCES Paper(ID) ON DELETE CASCADE
);

---------------------------------------  记录数据 ------------------------------------------------

-- 客户端登录记录
CREATE TABLE ClientLoginRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
StudentID INT,							-- 学生主键ID
IP VARCHAR(15),							-- 登录IP
TerminalType INT,						-- 终端类型  (0：PC  1：Android   2: IOS   3: Web)
PlatformVersion VARCHAR(30),			-- 终端所在平台版本，如 Windows7、 Android 4.2.2、 IOS 8.1  、 IE10
AppVersion VARCHAR(10),					-- App版本
LoginTime DATETIME,						-- 登录时间
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

-- 老师操作记录
CREATE TABLE TeacherDoRecord(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
TeacherID INT,							-- 老师主键ID
TeacherName VARCHAR(50),				-- 老师姓名
DoTime DATETIME,						-- 操作时间
DoName VARCHAR(100),					-- 操作名称
DoContent VARCHAR(100),					-- 操作内容
Remark VARCHAR(100),					-- 备注
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);

--意见反馈信息表
CREATE TABLE Feedback(
ID INT PRIMARY KEY AUTO_INCREMENT,		-- 主键ID
Content VARCHAR(500),					-- 反馈内容
Contact VARCHAR(30),					-- 联系方式
TerminalType INT,						-- 反馈终端途径 (0：PC  1：Android  2：IOS  3：Web)
AddTime DATETIME DEFAULT NOW(),			-- 添加时间
IsDeleted INT DEFAULT 0  				-- 是否已删除 (0：未删除  1：已删除)
);








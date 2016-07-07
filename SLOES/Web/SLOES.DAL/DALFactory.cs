
namespace SLOES.DAL
{
    /// <summary>
    /// Factory for DAL.
    /// </summary>
    public class DALFactory
    {
        private static DALFactory instance;

        // Item data
        private CourseDAL courseDAL;
        private ChapterDAL chapterDAL;
        private SingleItemDAL singleItemDAL;
        private MultipleItemDAL multipleItemDAL;
        private JudgeItemDAL judgeItemDAL;
        private BlankItemDAL blankItemDAL;
        private WordItemDAL wordItemDAL;

        // Paper data
        private PaperDAL paperDAL;
        private PaperSingleDAL paperSingleDAL;
        private PaperMultipleDAL paperMultipleDAL;
        private PaperJudgeDAL paperJudgeDAL;

        // Account data
        private TeacherDAL teacherDAL;
        private StudentDAL studentDAL;
        private StudentErrorDAL studentErrorDAL;
        private StudentFavoriteDAL studentFavoriteDAL;
        private StudentScoreDAL studentScoreDAL;

        // Record data
        private ClientLoginRecordDAL clientLoginRecordDAL;
        private TeacherDoRecordDAL teacherDoRecordDAL;
        private FeedbackDAL feedbackDAL;

        //========================= Locker  ===================================

        private static readonly object instanceLocker = new object();

        // Item data
        private static readonly object courseDALLocker = new object();
        private static readonly object chapterDALLocker = new object();
        private static readonly object singleItemDALLocker = new object();
        private static readonly object multipleItemDALLocker = new object();
        private static readonly object judgeItemDALLocker = new object();
        private static readonly object blankItemDALLocker = new object();
        private static readonly object wordItemDALLocker = new object();

        // Paper Data
        private static readonly object paperDALLocker = new object();
        private static readonly object paperSingleDALLocker = new object();
        private static readonly object paperMultipleDALLocker = new object();
        private static readonly object paperJudgeDALLocker = new object();

        // Account data
        private static readonly object teacherDALLocker = new object();
        private static readonly object studentDALLocker = new object();
        private static readonly object studentErrorDALLocker = new object();
        private static readonly object studentFavoriteDALLocker = new object();
        private static readonly object studentScoreDALLocker = new object();

        // Record data
        private static readonly object clientLoginRecordDALLocker = new object();
        private static readonly object teacherDoRecordDALLocker = new object();
        private static readonly object feedbackDALLocker = new object();

        #region Constructor

        private DALFactory() { }

        #endregion

        /// <summary>
        /// Gets DAL factory instance.
        /// </summary>
        public static DALFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new DALFactory();
                        }
                    }
                }
                return instance;
            }
        }

        //===================================================

        #region Item Data DAL

        /// <summary>
        /// Gets CourseDAL instance.
        /// </summary>
        public CourseDAL CourseDAL
        {
            get
            {
                if (courseDAL == null)
                {
                    lock (courseDALLocker)
                    {
                        if (courseDAL == null)
                        {
                            courseDAL = new CourseDAL();
                        }
                    }
                }
                return courseDAL;
            }
        }

        /// <summary>
        /// Gets CourseDAL instance.
        /// </summary>
        public ChapterDAL ChapterDAL
        {
            get
            {
                if (chapterDAL == null)
                {
                    lock (chapterDALLocker)
                    {
                        if (chapterDAL == null)
                        {
                            chapterDAL = new ChapterDAL();
                        }
                    }
                }
                return chapterDAL;
            }
        }

        /// <summary>
        /// Gets SingleItemDAL instance.
        /// </summary>
        public SingleItemDAL SingleItemDAL
        {
            get
            {
                if (singleItemDAL == null)
                {
                    lock (singleItemDALLocker)
                    {
                        if (singleItemDAL == null)
                        {
                            singleItemDAL = new SingleItemDAL();
                        }
                    }
                }
                return singleItemDAL;
            }
        }

        /// <summary>
        /// Gets MultipleItemDAL instance.
        /// </summary>
        public MultipleItemDAL MultipleItemDAL
        {
            get
            {
                if (multipleItemDAL == null)
                {
                    lock (multipleItemDALLocker)
                    {
                        if (multipleItemDAL == null)
                        {
                            multipleItemDAL = new MultipleItemDAL();
                        }
                    }
                }
                return multipleItemDAL;
            }
        }

        /// <summary>
        /// Gets JudgeItemDAL instance.
        /// </summary>
        public JudgeItemDAL JudgeItemDAL
        {
            get
            {
                if (judgeItemDAL == null)
                {
                    lock (judgeItemDALLocker)
                    {
                        if (judgeItemDAL == null)
                        {
                            judgeItemDAL = new JudgeItemDAL();
                        }
                    }
                }
                return judgeItemDAL;
            }
        }

        /// <summary>
        /// Gets BlankItemDAL instance.
        /// </summary>
        public BlankItemDAL BlankItemDAL
        {
            get
            {
                if (blankItemDAL == null)
                {
                    lock (blankItemDALLocker)
                    {
                        if (blankItemDAL == null)
                        {
                            blankItemDAL = new BlankItemDAL();
                        }
                    }
                }
                return blankItemDAL;
            }
        }

        /// <summary>
        /// Gets WordItemDAL instance.
        /// </summary>
        public WordItemDAL WordItemDAL
        {
            get
            {
                if (wordItemDAL == null)
                {
                    lock (wordItemDALLocker)
                    {
                        if (wordItemDAL == null)
                        {
                            wordItemDAL = new WordItemDAL();
                        }
                    }
                }
                return wordItemDAL;
            }
        }

        #endregion

        #region Paper Data DAL

        /// <summary>
        /// Gets PaperDAL instance.
        /// </summary>
        public PaperDAL PaperDAL
        {
            get
            {
                if (paperDAL == null)
                {
                    lock (paperDALLocker)
                    {
                        if (paperDAL == null)
                        {
                            paperDAL = new PaperDAL();
                        }
                    }
                }
                return paperDAL;
            }
        }

        /// <summary>
        /// Gets PaperSingleDAL instance.
        /// </summary>
        public PaperSingleDAL PaperSingleDAL
        {
            get
            {
                if (paperSingleDAL == null)
                {
                    lock (paperSingleDALLocker)
                    {
                        if (paperSingleDAL == null)
                        {
                            paperSingleDAL = new PaperSingleDAL();
                        }
                    }
                }
                return paperSingleDAL;
            }
        }

        /// <summary>
        /// Gets PaperMultipleDAL instance.
        /// </summary>
        public PaperMultipleDAL PaperMultipleDAL
        {
            get
            {
                if (paperMultipleDAL == null)
                {
                    lock (paperMultipleDALLocker)
                    {
                        if (paperMultipleDAL == null)
                        {
                            paperMultipleDAL = new PaperMultipleDAL();
                        }
                    }
                }
                return paperMultipleDAL;
            }
        }

        /// <summary>
        /// Gets PaperJudgeDAL instance.
        /// </summary>
        public PaperJudgeDAL PaperJudgeDAL
        {
            get
            {
                if (paperJudgeDAL == null)
                {
                    lock (paperJudgeDALLocker)
                    {
                        if (paperJudgeDAL == null)
                        {
                            paperJudgeDAL = new PaperJudgeDAL();
                        }
                    }
                }
                return paperJudgeDAL;
            }
        }

        #endregion

        #region Account Data DAL

        /// <summary>
        /// Gets TeacherDAL instance object.
        /// </summary>
        public TeacherDAL TeacherDAL
        {
            get
            {
                if (teacherDAL == null)
                {
                    lock (teacherDALLocker)
                    {
                        if (teacherDAL == null)
                        {
                            teacherDAL = new TeacherDAL();
                        }
                    }
                }
                return teacherDAL;
            }
        }

        /// <summary>
        /// Gets StudentDAL instance object.
        /// </summary>
        public StudentDAL StudentDAL
        {
            get
            {
                if (studentDAL == null)
                {
                    lock (studentDALLocker)
                    {
                        if (studentDAL == null)
                        {
                            studentDAL = new StudentDAL();
                        }
                    }
                }
                return studentDAL;
            }
        }

        /// <summary>
        /// Gets StudentErrorDAL instance.
        /// </summary>
        public StudentErrorDAL StudentErrorDAL
        {
            get
            {
                if (studentErrorDAL == null)
                {
                    lock (studentErrorDALLocker)
                    {
                        if (studentErrorDAL == null)
                        {
                            studentErrorDAL = new StudentErrorDAL();
                        }
                    }
                }
                return studentErrorDAL;
            }
        }

        /// <summary>
        /// Gets StudentFavoriteDAL instance.
        /// </summary>
        public StudentFavoriteDAL StudentFavoriteDAL
        {
            get
            {
                if (studentFavoriteDAL == null)
                {
                    lock (studentFavoriteDALLocker)
                    {
                        if (studentFavoriteDAL == null)
                        {
                            studentFavoriteDAL = new StudentFavoriteDAL();
                        }
                    }
                }
                return studentFavoriteDAL;
            }
        }

        /// <summary>
        /// Gets StudentScoreDAL instance.
        /// </summary>
        public StudentScoreDAL StudentScoreDAL
        {
            get
            {
                if (studentScoreDAL == null)
                {
                    lock (studentScoreDALLocker)
                    {
                        if (studentScoreDAL == null)
                        {
                            studentScoreDAL = new StudentScoreDAL();
                        }
                    }
                }
                return studentScoreDAL;
            }
        }

        #endregion

        #region Record Data DAL

        /// <summary>
        /// Gets ClientLoginRecordDAL instance.
        /// </summary>
        public ClientLoginRecordDAL ClientLoginRecordDAL
        {
            get
            {
                if (clientLoginRecordDAL == null)
                {
                    lock (clientLoginRecordDALLocker)
                    {
                        if (clientLoginRecordDAL == null)
                        {
                            clientLoginRecordDAL = new ClientLoginRecordDAL();
                        }
                    }
                }
                return clientLoginRecordDAL;
            }
        }

        /// <summary>
        /// Gets TeacherDoRecordDAL instance.
        /// </summary>
        public TeacherDoRecordDAL TeacherDoRecordDAL
        {
            get
            {
                if (teacherDoRecordDAL == null)
                {
                    lock (teacherDoRecordDALLocker)
                    {
                        if (teacherDoRecordDAL == null)
                        {
                            teacherDoRecordDAL = new TeacherDoRecordDAL();
                        }
                    }
                }
                return teacherDoRecordDAL;
            }
        }

        /// <summary>
        /// Gets FeedbackDAL instance object.
        /// </summary>
        public FeedbackDAL FeedbackDAL
        {
            get
            {
                if (feedbackDAL == null)
                {
                    lock (feedbackDALLocker)
                    {
                        if (feedbackDAL == null)
                        {
                            feedbackDAL = new FeedbackDAL();
                        }
                    }
                }
                return feedbackDAL;
            }
        }

        #endregion
    }
}

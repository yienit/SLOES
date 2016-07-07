
namespace SLOES.Service
{
    /// <summary>
    /// Factory for service.
    /// </summary>
    public class ServiceFactory
    {
        private static ServiceFactory instance;

        private AccountDataService accountDataService;
        private ItemDataService itemDataService;
        private PaperDataService paperDataService;
        private RecordDataService recordDataService;
        private SecurityService securityService;

        private static readonly object instanceLocker = new object();
        private static readonly object accountDataServiceLocker = new object();
        private static readonly object itemDataServiceLocker = new object();
        private static readonly object paperDataServiceLocker = new object();
        private static readonly object recordDataServiceLocker = new object();
        private static readonly object securityServiceLocker = new object();

        private ServiceFactory() { }

        /// <summary>
        /// Gets instance for ServiceFactory.
        /// </summary>
        public static ServiceFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceFactory();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets instance of AccountDataService.
        /// </summary>
        public AccountDataService AccountDataService
        {
            get
            {
                if (accountDataService == null)
                {
                    lock (accountDataServiceLocker)
                    {
                        if (accountDataService == null)
                        {
                            accountDataService = new AccountDataService();
                        }
                    }
                }
                return accountDataService;
            }
        }

        /// <summary>
        /// Gets instance of ItemDataService.
        /// </summary>
        public ItemDataService ItemDataService
        {
            get
            {
                if (itemDataService == null)
                {
                    lock (itemDataServiceLocker)
                    {
                        if (itemDataService == null)
                        {
                            itemDataService = new ItemDataService();
                        }
                    }
                }
                return itemDataService;
            }
        }

        /// <summary>
        /// Gets instance of PaperDataService.
        /// </summary>
        public PaperDataService PaperDataService
        {
            get
            {
                if (paperDataService == null)
                {
                    lock (paperDataServiceLocker)
                    {
                        if (paperDataService == null)
                        {
                            paperDataService = new PaperDataService();
                        }
                    }
                }
                return paperDataService;
            }
        }

        /// <summary>
        /// Gets instance of RecordDataService.
        /// </summary>
        public RecordDataService RecordDataService
        {
            get
            {
                if (recordDataService == null)
                {
                    lock (recordDataServiceLocker)
                    {
                        if (recordDataService == null)
                        {
                            recordDataService = new RecordDataService();
                        }
                    }
                }
                return recordDataService;
            }
        }

        /// <summary>
        /// Gets instance of SecurityService.
        /// </summary>
        public SecurityService SecurityService
        {
            get
            {
                if (securityService == null)
                {
                    lock (securityServiceLocker)
                    {
                        if (securityService == null)
                        {
                            securityService = new SecurityService();
                        }
                    }
                }
                return securityService;
            }
        }
    }
}

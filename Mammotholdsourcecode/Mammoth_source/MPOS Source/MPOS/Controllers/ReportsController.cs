namespace MPOS.Controllers
{
    using System.IO;
    using System.Web;
    using Telerik.Reporting.Cache.File;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.WebApi;

	//The class name determines the service URL. 
	//ReportsController class name defines /api/report/ service URL.
    public class ReportsController : ReportsControllerBase
    {
        static readonly ReportServiceConfiguration configurationInstance;

        static ReportsController()
        {
			//This is the folder that contains the XML (trdx) report definitions
            //In this case this is the app folder
            string reportPath = HttpContext.Current.Server.MapPath(path: "~/");
			
			//Add resolver for trdx report definitions, 
			//then add resolver for class report definitions as fallback resolver; 
			//finally create the resolver and use it in the ReportServiceConfiguration instance.
            ReportResolverBase resolver = new ReportFileResolver(reportPath)
                .AddFallbackResolver(new ReportTypeResolver());

			//Setup the ReportServiceConfiguration
            configurationInstance = new ReportServiceConfiguration
            {
                HostAppId = "Html5DemoApp",
                Storage = new FileStorage(),
                ReportResolver = resolver,
                // ReportSharingTimeout = 0,
                // ClientSessionTimeout = 15,
            };
        }

        public ReportsController()
        {
			//Initialize the service configuration
            this.ReportServiceConfiguration = configurationInstance;
        }
    }
}
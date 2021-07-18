using System;
namespace Greeter.Common
{
    public static class BuildEnvironment
    {
        // Decides base url
        static readonly BuildEnvironments BUILD_ENV = BuildEnvironments.StriveDev;

        // Gives respective base Url or base endpoint for api Call
        public static string BaseUrl
        {
            get
            {
                string baseUrl = string.Empty;

                if (BUILD_ENV == BuildEnvironments.StriveDev)
                {
                    baseUrl = Urls.STRIVE_DEV_BASE_URL;
                }
                else if (BUILD_ENV == BuildEnvironments.MamoothDev)
                {
                    baseUrl = Urls.MAMMOTH_DEV_BASE_URL;
                }
                else if (BUILD_ENV == BuildEnvironments.MamoothQA)
                {
                    baseUrl = Urls.MAMMOTH_DEV_QA_BASE_URL;
                }
                else if (BUILD_ENV == BuildEnvironments.Live)
                {
                    baseUrl = Urls.LIVE_BASE_URL;
                }
                return baseUrl;
            }
        }
    }

    enum BuildEnvironments
    {
        StriveDev,
        MamoothDev,
        MamoothQA,
        Live
    }
}

using System;
namespace Greeter.Common
{
    public class BuildEnvironment
    {
        public static string BASE_URL = BuildEnvironment.GetBaseUrl(BuildEnvironments.MamoothDev);

        // Gives respective base Url or base endpoint for an api Call
        static string GetBaseUrl(BuildEnvironments buildEnv)
        {
                string baseUrl = string.Empty;

                if (buildEnv == BuildEnvironments.StriveDev)
                {
                    baseUrl = BaseUrls.STRIVE_DEV_BASE_URL;
                }
                else if (buildEnv == BuildEnvironments.MamoothDev)
                {
                    baseUrl = BaseUrls.MAMMOTH_DEV_BASE_URL;
                }
                else if (buildEnv == BuildEnvironments.MamoothQA)
                {
                    baseUrl = BaseUrls.MAMMOTH_DEV_QA_BASE_URL;
                }
                else if (buildEnv == BuildEnvironments.Live)
                {
                    baseUrl = BaseUrls.LIVE_BASE_URL;
                }
                return baseUrl;
        }

        class BaseUrls
        {
            //Strive Dev By Zahir
            internal const string STRIVE_DEV_BASE_URL = "https://strivedev.azurewebsites.net";

            // Mamooth Dev
            internal const string MAMMOTH_DEV_BASE_URL = "https://mammothuatapi-dev.azurewebsites.net";

            //Mamooth QA
            internal const string MAMMOTH_DEV_QA_BASE_URL = "https://mammothuatapi-qa.azurewebsites.net";

            //Live
            internal const string LIVE_BASE_URL = "https://strivedev.azurewebsites.net";
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

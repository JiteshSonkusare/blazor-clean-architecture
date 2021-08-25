namespace BlazorCleanArchitecture.Shared.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
            public const string SendUpdateDashboard = "UpdateDashboardAsync";
            public const string ReceiveUpdateDashboard = "UpdateDashboard";

            public const string OnConnect = "OnConnectAsync";
        }
        public static class Cache
        {
            public const string GetAllBrandsCacheKey = "all-brands";
        }
    }
}
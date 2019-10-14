namespace DownTime.Services.Interface
{
    public interface IHangfireBootstrapper
    {
        /// <summary>
        /// Sets jobs for web sites
        /// </summary>
        void SetJobsWebSites();
    }
}

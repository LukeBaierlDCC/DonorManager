namespace DonorManager2024.Models.PowerBI
{
    using Microsoft.PowerBI.Api.Models;
    using System;

    public class DashboardEmbedConfig
    {
        public Guid DashboardId { get; set; }

        public string EmbedUrl { get; set; }

        public EmbedToken EmbedToken { get; set; }
    }
}

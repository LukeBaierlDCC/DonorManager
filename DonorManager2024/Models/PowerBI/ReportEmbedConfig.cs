namespace DonorManager2024.Models.PowerBI
{
    using Microsoft.PowerBI.Api.Models;
    using System.Collections.Generic;

    public class ReportEmbedConfig
    {
        public List<EmbedReport> EmbedReports { get; set; }

        public EmbedToken EmbedToken { get; set; }
    }
}

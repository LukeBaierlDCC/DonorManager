using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DonorManager.Models;
using System.ComponentModel;

namespace DonorManager2024.Models.CampaignsRelated
{
    public class CampaignCSV : ClassMap<Campaign>
    {
        //public CampaignCSV()
        //{

        //    //Map(m => m.CampaignName).Name("campaignName").Index(0); /*.Name("campaignName"); .Index(0); */
        //    //Map(m => m.CampaignCode).Name("campaignCode").Index(1); /*.Name("campaignCode"); .Index(1);*/
        //    //Map(m => m.JobName).Name("jobName").Index(2); /*.Name("jobName"); .Index(2);*/
        //    //Map(m => m.FinalPkgCost).Name("finalPkgCost").Index(3); /*.Name("finalPkgCost"); .Index(3);*/
        //    //Map(m => m.MailDate).Name("mailDate").Index(4); /*.Name("clientName"); .Index(4);*/
        //    //Map(m => m.Client).Name("clientName").Index(5); /*.Name("channelName").Optional(); .Index(5);*/
        //    //Map(m => m.Channels).Name("channelName").Optional().Index(6); /*.Name("promotionName").Optional(); .Index(6);*/
        //    //Map(m => m.Promotions).Name("promotionName").Optional().Index(7); /*.Name("mailDate"); .Index(7);*/
        //}

        //Campaign Name, Campaign Code, JobName, Final PKG Cost, Client Name, Channel Name, Promotion Name and MailDate
        [Index(0)]
        [DisplayName("Campaign Name")]
        public string CampaignName { get; set; }

        [Index(1)]
        [DisplayName("Campaign Code")]
        public string CampaignCode { get; set; }

        [Index(2)]
        [DisplayName("Job Name")]
        public string JobName { get; set; }

        [Index(3)]
        [DisplayName("Final Package Cost")]
        public string FinalPkgCost { get; set; }

        [Index(4)]
        [DisplayName("Mail Date")]
        public string MailDate { get; set; }

        //Add restriction for Client Name to be unique.
        [Index(5)]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }

        [Index(6)]
        [DisplayName("Promotion Name")]
        public string PromotionCode { get; set; }

        [Index(7)]
        [DisplayName("Channel Name")]
        public string ChannelCode { get; set; }

        

        
    }
}

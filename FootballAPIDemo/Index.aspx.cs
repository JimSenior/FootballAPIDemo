using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FootballAPIDemo.Model;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;


namespace FootballAPIDemo
{
    
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var url = "https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id=4328";

            if (ddlTeams.Items.Count == 0)
            {
                var teams = PopulatControls(url);
            }
        }

        private async Task PopulatControls(string url)
        {
            var teams = await GetTeams(url);
            if (teams != null)
            {
                foreach (Team t in teams.teams)
                {
                    TeamList.TeamNames.Add(t.strTeam);
                }

                ddlTeams.DataSource = TeamList.TeamNames;
                ddlTeams.DataBind();

                imgTeamLogo.ImageUrl = teams.teams.First().strTeamLogo;
                lblTeamDescription.Text = teams.teams.First().strDescriptionEN.Replace("\r\n", "<br />");

                ddlLanguage.Items.Add(new ListItem("English", "EN"));
                ddlLanguage.Items.Add(new ListItem("Espanol", "ES"));
                ddlLanguage.Items.Add(new ListItem("Deutsche", "DE"));

                lnkWebSite.Text = "Visit " + teams.teams.First().strTeam + " website";
                lnkWebSite.PostBackUrl = "https://" + teams.teams.First().strWebsite;
            }
        }

        protected  void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
        {

            var url = "https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id=4328";
            RerenderPage(url);
        }

        protected async Task<Teams> GetTeams(string url)
        {
            
            var data = await ApiCaller.GetData(url);
            var teams = JsonConvert.DeserializeObject<Teams>(data);
            return teams;
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var url = "https://www.thesportsdb.com/api/v1/json/1/lookup_all_teams.php?id=4328";
            RerenderPage(url);
        }

        protected async void RerenderPage(string url)
        {
            
            var teams = await GetTeams(url);
            var selected = from items in teams.teams.AsEnumerable()
                           where items.strTeam == ddlTeams.SelectedItem.Text
                           select items;
            StringBuilder sb = new StringBuilder();

            switch (ddlLanguage.SelectedValue)
            {
                case "EN":
                    sb.Append(selected.FirstOrDefault().strDescriptionEN.Replace("\r\n", "<br />"));
                    break;
                case "ES":
                    if (!string.IsNullOrEmpty(selected.FirstOrDefault().strDescriptionES))
                    {
                        sb.Append(selected.FirstOrDefault().strDescriptionES.Replace("\r\n", "<br />"));
                    }
                    break;
                case "DE":
                    if (!string.IsNullOrEmpty(selected.FirstOrDefault().strDescriptionDE))
                    {
                        sb.Append(selected.FirstOrDefault().strDescriptionDE.Replace("\r\n", "<br />"));
                    }
                    break;
            }
            imgTeamLogo.ImageUrl = selected.FirstOrDefault().strTeamLogo;
            lblTeamDescription.Text = sb.ToString();
            lnkWebSite.PostBackUrl = "https://"+selected.FirstOrDefault().strWebsite;
            lnkWebSite.Text = "Visit " + selected.FirstOrDefault().strTeam + " website";
        }
    }
}
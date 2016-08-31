using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TooksCms.Core.Enums;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Support;
using TooksCms.Core.Interfaces.Repository;
using TooksCms.Core.Objects;

namespace TooksCms.ServiceLayer.Services
{
    public static class StatisticsService
    {
        public static PageVisit RegisterPageVisit(HttpRequest request, AreaType area, int? itemId = null, string searchTerm = null, string linkType = null, PageVisit previous = null)
        {
            var repository = DependencyResolver.Current.GetService<IStatsRepository>();
            try
            {
                var visit = new PageVisit
                {
                    AreaType = area,
                    ItemId = itemId,
                    SearchTerm = searchTerm,
                    Referer = request.UrlReferrer == null ? "" : request.UrlReferrer.AbsoluteUri,
                    Url = request.Url.AbsoluteUri,
                    LinkType = linkType,
                    PreviousId = previous == null ? null : (int?)previous.Id,
                    UserAgent = request.UserAgent,
                    UserLanguages = request.UserLanguages != null ? string.Join("|", request.UserLanguages) : "",
                    BrowserVersion = request.Browser != null ? request.Browser.Version : "",
                    BrowserName = request.Browser != null ? request.Browser.Browser : "",
                    IpAddress = request.ServerVariables["REMOTE_ADDR"],
                    DateTime = DateTime.Now
                };
                return new PageVisit(repository.InsertPageVisit(visit));
            }
            catch (Exception ex)
            {
                Logger.LogException(EventLogType.Error, "TooksCms.ServiceLayer", ex, "RegisterPageVisit", 0);
            }
            return null;
        }

        public static FlotChart GetPageVisitChart(IStatsRepository repository)
        {
            var visits = repository.FetchUniqueVisits(null, null).ToList();

            var chart = new FlotChart();

            var unique = chart.AddSeries("Unique Visitors", new FlotChartLine());
            var clicks = chart.AddSeries("Page Clicks", new FlotChartDashed(true, 2));

            foreach (var date in visits.Select(v => v.Date).Distinct())
            {
                chart.AddData(unique, date.Value.ToLongDateString(), visits.Where(v => v.Date == date).Count());
                chart.AddData(clicks, date.Value.ToLongDateString(), visits.Where(v => v.Date == date).Sum(v => v.Count.HasValue ? v.Count.Value : 0));
            }

            return chart;
        }

        public static List<dynamic> GetPageVisitAggregation(IStatsRepository repository)
        {
            var clicks = new List<dynamic>();

            var visits = repository.FetchUniqueVisits(null, null).ToList();

            foreach (var date in visits.Select(v => v.Date).Distinct())
            {
                clicks.Add(new
                {
                    date = date.Value,
                    unique = visits.Where(v => v.Date == date).Count(),
                    clicks = visits.Where(v => v.Date == date).Sum(v => v.Count.HasValue ? v.Count.Value : 0)
                });
            }

            return clicks;
        }

        public static List<dynamic> GetBrowserStats()
        {
            var result = new List<dynamic>();
            var repository = DependencyResolver.Current.GetService<IStatsRepository>();
            var browsers = repository.FetchBrowserStats().ToList();

            foreach (var browser in browsers.Select(b => b.BrowserName).Distinct())
            {
                result.Add(new
                {
                    browser = browser,
                    count = browsers.Where(b => b.BrowserName == browser).Sum(v => v.Count.HasValue ? v.Count.Value : 0)
                });
            }

            return result;
        }

        public static FlotPie GetBrowserStats(IStatsRepository repository)
        {
            var browsers = repository.FetchBrowserStats().ToList();

            var chart = new FlotPie();

            var browserList = browsers.Select(b => b.BrowserName).Distinct().ToDictionary(browser => browser, chart.AddSeries);

            foreach (var kvp in browserList)
            {
                chart.SetData(kvp.Value, browsers.Where(b => b.BrowserName == kvp.Key).Sum(v => v.Count.HasValue ? v.Count.Value : 0));
            }

            return chart;
        }
    }
}

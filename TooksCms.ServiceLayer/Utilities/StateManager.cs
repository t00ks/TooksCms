using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TooksCms.ServiceLayer.Authentication;
using TooksCms.ServiceLayer.Gadgets;
using TooksCms.ServiceLayer.Objects.Account;
using TooksCms.ServiceLayer.Objects.Security;
using TooksCms.ServiceLayer.Objects;
using TooksCms.ServiceLayer.Services;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Utilities
{
    public static class StateManager
    {
        #region Statistics

        public static void RegisterPageVisit(AreaType area, int? itemId = null, string linkType = null)
        {
            StateManager.PreviousPageVisit = StatisticsService.RegisterPageVisit(HttpContext.Current.Request, area, itemId, null, linkType, StateManager.PreviousPageVisit);
        }

        public static void RegisterPageVisit(AreaType area, string searchTerm, string linkType = null)
        {
            StateManager.PreviousPageVisit = StatisticsService.RegisterPageVisit(HttpContext.Current.Request, area, null, searchTerm, linkType, StateManager.PreviousPageVisit);
        }

        #endregion

        #region Session

        private const string CURRENTUSER = "User.Current";
        public static UserPrincipal CurrentUser
        {
            get { return (UserPrincipal)HttpContext.Current.Session[CURRENTUSER]; }
            set { HttpContext.Current.Session[CURRENTUSER] = value; }
        }

        private const string GADGETCOLLECTION = "Gadget.Collection";
        public static GadgetCollection Gadgets
        {
            get { return (GadgetCollection)HttpContext.Current.Session[GADGETCOLLECTION]; }
            set { HttpContext.Current.Session[GADGETCOLLECTION] = value; }
        }

        private const string CURRENTGUEST = "Guest.Current";
        public static GuestPrincipal CurrentGuest
        {
            get { return (GuestPrincipal)HttpContext.Current.Session[CURRENTGUEST]; }
            set { HttpContext.Current.Session[CURRENTGUEST] = value; }
        }

        private const string PREVIOUSPAGEVISIT = "PageVisit.Previous";
        public static PageVisit PreviousPageVisit
        {
            get { return HttpContext.Current.Session[PREVIOUSPAGEVISIT] == null ? null : (PageVisit)HttpContext.Current.Session[PREVIOUSPAGEVISIT]; }
            set { HttpContext.Current.Session[PREVIOUSPAGEVISIT] = value; }
        }

        private const string CURRENTSITE = "Site.Current";
        public static Site CurrentSite
        {
            get { return HttpContext.Current.Session[CURRENTSITE] == null ? null : (Site)HttpContext.Current.Session[CURRENTSITE]; }
            set { HttpContext.Current.Session[CURRENTSITE] = value; }
        }

        #endregion

        #region Application

        private const string USERSONLINE = "UsersOnline";
        public static int UsersOnline
        {
            get
            {
                
                if (HttpContext.Current.Application[USERSONLINE] == null)
                {
                    return 0;
                }
                return (int) HttpContext.Current.Application[USERSONLINE];
            }
            set { HttpContext.Current.Application[USERSONLINE] = value; }
        }

        private const string SITES = "Sites";
        public static List<Site> Sites
        {
            get
            {
                if (HttpContext.Current.Application[SITES] == null)
                {
                    return null;
                }
                return (List<Site>)HttpContext.Current.Application[SITES];
            }
            set { HttpContext.Current.Application[SITES] = value; }
        }

        private const string TWITTERBEARERTOKEN = "TwitterBearerToken";
        private const string TWITTERBEARERTOKENLASTCHECK = "TwitterBearerTokenLastCheck";
        public static string TwitterBearerToken
        {
            get
            {
                if (HttpContext.Current.Application[TWITTERBEARERTOKENLASTCHECK] == null || ((DateTime)HttpContext.Current.Application[TWITTERBEARERTOKENLASTCHECK]).AddMinutes(15) < DateTime.Now)
                {
                    HttpContext.Current.Application[TWITTERBEARERTOKEN] = TwitterApiClient.GetBearerToken();
                    HttpContext.Current.Application[TWITTERBEARERTOKENLASTCHECK] = DateTime.Now;
                }

                return (string)HttpContext.Current.Application[TWITTERBEARERTOKEN];
            }
        }

        #endregion

        #region Assembly

        private const string VERSION = "Version";
        public static string Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        #endregion
    }
}
﻿using System.Collections.Generic;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Admin.Notification;
using Orchard.UI.Notify;
using Sunkist.OlarkLiveChat.Models;

namespace Sunkist.OlarkLiveChat.Services {
    public class MissingSettingsBanner : INotificationProvider {
        private readonly IOrchardServices _orchardServices;

        public MissingSettingsBanner(IOrchardServices orchardServices) {
            _orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications() {
            var workContext = _orchardServices.WorkContext;
            var settings = workContext.CurrentSite.As<LiveChatSettingsPart>();

            if (settings == null || !settings.IsValid()) {
                var urlHelper = new UrlHelper(workContext.HttpContext.Request.RequestContext);
                var url = urlHelper.Action("Live Chat", "Admin", new {Area = "Settings"});
                yield return new NotifyEntry {Message = T("The <a href=\"{0}\">Live Chat Settings</a> needs to be configured.", url), Type = NotifyType.Warning};
            }
        }
    }
}
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Caching;
using Orchard.Data;
using Orchard.Mvc.Filters;
using Orchard.UI.Admin;
using Sunkist.OlarkLiveChat.Models;

namespace Sunkist.OlarkLiveChat.Filters {
    public class LiveChatFilter : FilterProvider, IResultFilter {
        private const string CacheKey = "599DBF8C-E28A-4835-85EF-CC68D118B9C2";

        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IRepository<LiveChatSettingsPartRecord> _repository;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;

        public LiveChatFilter(
            IWorkContextAccessor workContextAccessor,
            IRepository<LiveChatSettingsPartRecord> repository,
            ICacheManager cacheManager,
            ISignals signals) {
            _workContextAccessor = workContextAccessor;
            _repository = repository;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            // Do Not Display on Admin Pages
            if (AdminFilter.IsApplied(filterContext.RequestContext))
                return;

            // Only Display on Full Pages
            if (!(filterContext.Result is ViewResult))
                return;

            var script = _cacheManager.Get(CacheKey,
                ctx => {
                    ctx.Monitor(_signals.When(LiveChatEvent.SettingsChanged));
                    var settings = _repository.Table.FirstOrDefault();
                    return settings == null || !settings.Enable ? null : settings.Script;
                });

            if (string.IsNullOrEmpty(script))
                return;

            var context = _workContextAccessor.GetContext();
            var tail = context.Layout.Tail;
            tail.Add(new MvcHtmlString(script));
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {}
    }
}
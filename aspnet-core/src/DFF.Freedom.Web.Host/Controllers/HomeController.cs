using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using Abp.Timing;
using DFF.Freedom.Controllers;
using Microsoft.AspNetCore.Mvc;
using Abp.Extensions;

namespace DFF.Freedom.Web.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : FreedomControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationPublisher"></param>
        public HomeController(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect("/swagger/ui");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                    "App.SimpleMessage",
                    new MessageNotificationData(message),
                    severity: NotificationSeverity.Info,
                    userIds: new[] { defaultTenantAdmin, hostAdmin }
                 );

            return Content("Sent notification: " + message);
        }
    }
}
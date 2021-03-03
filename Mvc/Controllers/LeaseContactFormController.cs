using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Ganss.XSS;
using SitefinityWebApp.Extensions;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Mvc;

// ReSharper disable Mvc.ViewNotResolved
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "LeaseContactForm", Title = "LeaseContactForm", SectionName = "Mvc Widgets")]
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner))]

    public class LeaseContactFormController : Controller
    {
        #region Public properties

        /// <summary>
        /// Sets internal recipients.
        /// </summary>
        [Category("String Properties")]
        public string InternalRecipients { get; set; }

        /// <summary>
        /// Sets email subject line.
        /// </summary>
        [Category("String Properties")]
        public string EmailSubjectLine { get; set; }

        #endregion

        private readonly IMailService mailService;

        public LeaseContactFormController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public ActionResult Index()
        {
            LeaseContactFormModel model = new LeaseContactFormModel();

            return View("Default", model);
        }

        [HttpPost, ValidateHeaderAntiForgeryToken] 
        // Custom ValidateHeaderAntiForgeryToken attribute validates anti-forgery token
        public ActionResult Submit(LeaseContactFormModel data)
        {
            // Sanitize
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Clear();
            sanitizer.AllowedTags.Clear();
            data.Name = sanitizer.Sanitize(data.Name.Trim());
            data.Email = sanitizer.Sanitize(data.Email.Trim());
            data.Phone = sanitizer.Sanitize(data.Phone.Trim());
            data.PageTitle = sanitizer.Sanitize(data.PageTitle.Trim());

            // Validate input data
            TryValidateModel(data);
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(x => x.Errors).First();
                Log.Write($"LeaseContactFormController - Submit: {firstError.ErrorMessage}",
                    ConfigurationPolicy.ErrorLog);

                return Json(new {status = "error"});
            }

            // Save data to database
            // TODO

            // Send email notification use InternalRecipients and EmailSubjectLine properties
            // TODO

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
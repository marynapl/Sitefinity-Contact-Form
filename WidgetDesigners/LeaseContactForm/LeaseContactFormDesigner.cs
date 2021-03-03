using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SitefinityWebApp.WidgetDesigners.LeaseContactForm;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace SitefinityWebApp.WidgetDesigners.LeaseContactForm
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.Mvc.Controllers.LeaseContactFormController"/> widget
    /// </summary>
    public class LeaseContactFormDesigner : ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return LeaseContactFormDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        protected virtual Control InternalRecipients
        {
            get
            {
                return this.Container.GetControl<Control>("InternalRecipients", true);
            }
        }

        protected virtual Control EmailSubjectLine
        {
            get
            {
                return this.Container.GetControl<Control>("EmailSubjectLine", true);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("internalRecipients", this.InternalRecipients.ClientID);
            descriptor.AddElementProperty("emailSubjectLine", this.EmailSubjectLine.ClientID);


            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(LeaseContactFormDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/WidgetDesigners/LeaseContactForm/LeaseContactFormDesigner.ascx";
        public const string scriptReference = "~/WidgetDesigners/LeaseContactForm/LeaseContactFormDesigner.js";
        #endregion
    }
}
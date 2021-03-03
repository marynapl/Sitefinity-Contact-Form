Type.registerNamespace('SitefinityWebApp.WidgetDesigners.LeaseContactForm');

SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner = function (element) {
    /* Initialize Message fields */
    this._internalRecipients = null;
    this._emailSubjectLine = null;

    /* Calls the base constructor */
    SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find('#' + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control().Settings; /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI Message */
        jQuery(this.get_internalRecipients()).val(controlData.InternalRecipients);
        jQuery(this.get_emailSubjectLine()).val(controlData.EmailSubjectLine);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control().Settings;

        /* ApplyChanges Message */
        controlData.InternalRecipients = jQuery(this.get_internalRecipients()).val();
        controlData.EmailSubjectLine = jQuery(this.get_emailSubjectLine()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* InternalRecipients properties */
    get_internalRecipients: function () { return this._internalRecipients; },
    set_internalRecipients: function (value) { this._internalRecipients = value; },
    /* EmailSubjectLine properties */
    get_emailSubjectLine: function () { return this._emailSubjectLine; },
    set_emailSubjectLine: function (value) { this._emailSubjectLine = value; }
}

SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner.registerClass('SitefinityWebApp.WidgetDesigners.LeaseContactForm.LeaseContactFormDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

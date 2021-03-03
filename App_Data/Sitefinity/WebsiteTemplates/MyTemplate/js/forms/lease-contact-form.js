$(function () {
    $(document).ready(function () {
        leaseContactApp.init();
    });
});

var leaseContactApp = leaseContactApp ? leaseContactApp : function () {

    var serviceUrl = gLeaseContactSubmitUrl;

    var submitButton;
    var name;
    var email;
    var phone;
    var selectFacility;
    var selectConsultation;
    var consent;
    var successMessage;
    var invalidParameters;
    var errorOccurred;

    function init() {
        initControls();

        $(submitButton).on('click', function (e) {
            e.preventDefault();
            submit();
        });
    }

    function initControls() {
        submitButton = '#LeaseContactButtonSubmit';
        name = '#Name';
        email = '#Email';
        phone = '#Phone';
        successMessage = '#SuccessMessage';
        selectFacility = 'input[name=Facility]:checked';
        selectConsultation = 'input[name=Consultation]:checked';
        consent = 'input[name=Consent]';
        invalidParameters = '#InvalidParameters';
        errorOccurred = '#ErrorOccurred';
    }

    function submit() {

        // Clear errors
        clearErrors();

        // Validate the form
        var consultation = $(selectConsultation).map(function () {
            return (this.value);
        }).get();

        var facilityType = $(selectFacility).map(function () {
            return (this.value);
        }).get();

        var isConsent = $(consent).is(':checked');

        var pageTitle = $(document).attr('title');

        var data = {
            Consultation: consultation,
            Facility: facilityType,
            Name: $(name).val(),
            Email: $(email).val(),
            Phone: $(phone).val(),
            Consent: isConsent,
            PageTitle: pageTitle
        };

        var isValid = validate(data);
        if (!isValid) {
            return;
        }

        $(submitButton).attr('disabled', true);

        // Submit data
        var promise = submitLeaseContactForm(data);
        $.when(promise).done(function (result) {

            if (result.status !== 'success') {
                $(errorOccurred).addClass('is-visible');;
                $(submitButton).removeAttr('disabled');
                return;
            }

            clear();
            $(successMessage).removeClass('hide');
            $(submitButton).removeAttr('disabled');

            // GTM tracking
            window.dataLayer = window.dataLayer || [];
            window.dataLayer.push({
                event: 'customFormSubmit',
                formData: {
                    name: 'leaseForm',
                    consent: isConsent
                }
            });
        }).fail(function (jqxhr, textStatus, error) {
            $(errorOccurred).addClass('is-visible');;
            $(submitButton).removeAttr('disabled');
        });
    }

    function validate(data) {
        var valid = true;

        if (data.Consultation.length <= 0) {
            $('#ConsultationRequired').addClass('is-visible');
            valid = false;
        }
        if (data.Facility.length <= 0) {
            $('#FacilityRequired').addClass('is-visible');
            valid = false;
        }
        if (data.Name.length <= 0) {
            $('#NameRequired').addClass('is-visible');
            valid = false;
        }
        if (data.Email.length <= 0) {
            $('#EmailRequired').addClass('is-visible');
            valid = false;
        }
        else {
            var emailFormat = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!data.Email.match(emailFormat)) {
                $('#InvalidEmail').addClass('is-visible');
                valid = false;
            }
        }
        if (data.Phone.length > 0) {
            var phoneFormat = /^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s\./0-9]{8,14}$/;
            if (!data.Phone.match(phoneFormat)) {
                $('#InvalidPhone').addClass('is-visible');
                valid = false;
            }
        }

        if (valid === false) {
            $(invalidParameters).addClass('is-visible');
        }

        return valid;
    }

    function submitLeaseContactForm(data) {
        return $.ajax({
            url: serviceUrl,
            data: JSON.stringify(data),
            dataType: 'json',
            type: 'POST',
            headers: {
                '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            contentType: 'application/json; charset=utf-8'
        });
    }

    function clear() {
        clearErrors();
        $(email).val('');
        $(name).val('');
        $(phone).val('');
        $(consent).prop('checked', false);
        $(selectFacility).each(function () { this.checked = false; });
        $(selectConsultation).each(function () { this.checked = false; });
    }

    function clearErrors() {
        $(successMessage).addClass('hide');
        $('.form-error').removeClass('is-visible');
    }

    return {
        init: init,
        clear: clear
    };
}();
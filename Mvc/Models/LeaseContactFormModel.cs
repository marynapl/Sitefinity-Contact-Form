using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack.FluentValidation;
using SitefinityWebApp.Extensions;
using SitefinityWebApp.Mvc.Resources;

// ReSharper disable StringLiteralTypo

namespace SitefinityWebApp.Mvc.Models
{
    public enum ConsultationList
    {
        [Display(Name = "LeaseContactConsultationLeasingOption",
                 ResourceType = typeof(FormsResources))]
        LeasingOption,
        [Display(Name = "LeaseContactConsultationPlanYourSpace",
                 ResourceType = typeof(FormsResources))]
        PlanYourSpace,
        [Display(Name = "LeaseContactConsultationCalculateRoi",
                 ResourceType = typeof(FormsResources))]
        CalculateMyRoi
    }

    public enum FacilityType
    {
        [Display(Name = "LeaseContactFacilityPilatesStudio",
                 ResourceType = typeof(FormsResources))]
        PilatesStudio,
        [Display(Name = "LeaseContactFacilityFitnessFacility",
                 ResourceType = typeof(FormsResources))]
        FitnessFacility,
        [Display(Name = "LeaseContactFacilityMedicalRehabFacility",
                 ResourceType = typeof(FormsResources))]
        MedicalRehabFacility
    }

    public class LeaseContactFormModel : IValidatableObject
    {
        public LeaseContactFormModel()
        {
            Consultation = new List<string>();
            Facility = new List<string>();
            Phone = "";
            Consent = false;
            PageTitle = "";
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> Consultation { get; set; }
        public List<string> Facility { get; set; }
        public bool Consent { get; set; }
        public string PageTitle { get; set; }

        public IEnumerable<ValidationResult> Validate(
            System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            LeaseContactFormValidator validator = new LeaseContactFormValidator();
            var result = validator.Validate(this);

            return result.Errors.Select(item =>
                new ValidationResult(
                    item.ErrorMessage,
                    new[] { item.PropertyName }
                ));
        }
    }

    public class LeaseContactFormValidator : AbstractValidator<LeaseContactFormModel>
    {
        public LeaseContactFormValidator()
        {
            Regex phoneFormat = new Regex(@"^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s\./0-9]{8,14}$");
            RuleFor(leaseContact => leaseContact.Name)
                .NotEmpty()
                .Must(x => x.Length <= 50);

            RuleFor(leaseContact => leaseContact.Email)
                .NotEmpty()
                .Must(x => x.Length <= 50)
                .EmailAddress();

            RuleFor(leaseContact => leaseContact.Phone)
                .Must(x => x.Length <= 50)
                .Matches(phoneFormat)
                .When(leaseContact => !string.IsNullOrEmpty(leaseContact.Phone));

            RuleFor(leaseContact => leaseContact.Consultation)
                .NotEmpty();

            RuleForEach(leaseContact => leaseContact.Consultation)
                .NotEmpty()
                .Must(consultation => Enum.IsDefined(typeof(ConsultationList), consultation));

            RuleFor(leaseContact => leaseContact.Facility)
                .NotEmpty()
                .Must(facility => facility.Count == 1);

            RuleForEach(leaseContact => leaseContact.Facility)
                .NotEmpty()
                .Must(facility => Enum.IsDefined(typeof(FacilityType), facility));

            RuleFor(leaseContact => leaseContact.PageTitle)
                .NotNull();
        }
    }
}
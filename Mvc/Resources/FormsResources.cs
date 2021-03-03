using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp.Mvc.Resources
{
    public static class FormsResources
    {
        // General
        public static string ErrorRequiredEmail => Res.Get("FormsResources", "ErrorRequiredEmail");
        public static string ErrorRequiredFirstName => Res.Get("FormsResources", "ErrorRequiredFirstName");
        public static string ErrorRequiredLastName => Res.Get("FormsResources", "ErrorRequiredLastName");
        public static string ErrorRequiredPhone => Res.Get("FormsResources", "ErrorRequiredPhone");
        public static string ErrorRequiredCompany => Res.Get("FormsResources", "ErrorRequiredCompany");
        public static string ErrorRequiredDescription => Res.Get("FormsResources", "ErrorRequiredDescription");
        public static string ErrorRequiredSelectOne => Res.Get("FormsResources", "ErrorRequiredSelectOne");
        public static string ErrorRequiredTerms => Res.Get("FormsResources", "ErrorRequiredTerms");
        public static string ErrorGeneral => Res.Get("FormsResources", "ErrorGeneral");
        public static string ErrorInvalidEmail => Res.Get("FormsResources", "ErrorInvalidEmail");
        public static string ErrorRequiredCountry => Res.Get("FormsResources", "ErrorRequiredCountry");
        public static string ErrorRequiredName => Res.Get("FormsResources", "ErrorRequiredName");

        // Lease Contact form
        public static string LeaseContactConsultationLeasingOption => Res.Get("FormsResources", "LeaseContactConsultationLeasingOption");
        public static string LeaseContactConsultationPlanYourSpace => Res.Get("FormsResources", "LeaseContactConsultationPlanYourSpace");
        public static string LeaseContactConsultationCalculateRoi => Res.Get("FormsResources", "LeaseContactConsultationCalculateRoi");
        public static string LeaseContactFacilityPilatesStudio => Res.Get("FormsResources", "LeaseContactFacilityPilatesStudio");
        public static string LeaseContactFacilityFitnessFacility => Res.Get("FormsResources", "LeaseContactFacilityFitnessFacility");
        public static string LeaseContactFacilityMedicalRehabFacility => Res.Get("FormsResources", "LeaseContactFacilityMedicalRehabFacility");
        public static string LeaseContactErrorSpecifyFacility => Res.Get("FormsResources", "LeaseContactErrorSpecifyFacility");
    }
}
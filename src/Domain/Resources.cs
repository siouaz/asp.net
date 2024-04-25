using Microsoft.Extensions.Localization;

namespace siwar.Domain
{
    /// <summary>
    /// <see cref="IResources"/> implementation.
    /// </summary>
    public class Resources : IResources
    {
        private readonly IStringLocalizer<Resources> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        public Resources(IStringLocalizer<Resources> localizer) =>
            _localizer = localizer;

        public string AddedUserEmailSubject => _localizer.GetString("AddedUserEmailSubject");

        public string AgreementNumberExistent => _localizer.GetString("AgreementNumberExistent");

        public string AgreementItemAgreementRequired => _localizer.GetString("AgreementItemAgreementRequired");

        public string AgreementItemTypeRequired => _localizer.GetString("AgreementItemTypeRequired");

        public string AgreementItemStartDateRequired => _localizer.GetString("AgreementItemStartDateRequired");

        public string AgreementNumberRequired => _localizer.GetString("AgreementNumberRequired");

        public string AgreementAdmissibilityDateRequired => _localizer.GetString("AgreementAdmissibilityDateRequired");

        public string AgreementInactiveRequired => _localizer.GetString("AgreementInactiveRequired");

        public string AgreementRequestDateRequired => _localizer.GetString("AgreementRequestDateRequired");

        public string AgreementStructureRequired => _localizer.GetString("AgreementStructureRequired");

        public string AgreementValidityDateRequired => _localizer.GetString("AgreementValidityDateRequired");

        public string AcquisitionDateGreaterThanExpiryDate =>
            _localizer.GetString("AcquisitionDateGreaterThanExpiryDate");

        public string AcquisitionDateRequired => _localizer.GetString("AcquisitionDateRequired");

        public string BirthDateRequired => _localizer.GetString("BirthDateRequired");

        public string AssignedUser => _localizer.GetString("AssignedUser");

        public string DeniedListEdit => _localizer.GetString("DeniedListEdit");

        public string DependentItem => _localizer.GetString("DependentItem");

        public string ControlAuthor => _localizer.GetString("ControlAuthor");

        public string ControlDateRequired => _localizer.GetString("ControlDateRequired");

        public string ControlConcurrencyStampException => _localizer.GetString("ControlConcurrencyStampException");

        public string ControlReportSubmissionDateRequired =>
            _localizer.GetString("ControlReportSubmissionDateRequired");

        public string ControlReportSubmissionDateGreater => _localizer.GetString("ControlReportSubmissionDateGreater");

        public string ControlActivityRequired => _localizer.GetString("ControlActivityRequired");

        public string ControlConclusionRequired => _localizer.GetString("ControlConclusionRequired");

        public string ControlNozzleRequired => _localizer.GetString("ControlNozzleRequired");

        public string ControlNozzleOperationRequired => _localizer.GetString("ControlNozzleOperationRequired");

        public string ControlPreControlRequired => _localizer.GetString("ControlPreControlRequired");

        public string ControlTypeRequired => _localizer.GetString("ControlTypeRequired");

        public string ControlDepartmentRequired => _localizer.GetString("ControlDepartmentRequired");

        public string ControlCityRequired => _localizer.GetString("ControlCityRequired");

        public string ControlFlowRateRequired => _localizer.GetString("ControlFlowRateRequired");

        public string ControlUsageModeIdRequired => _localizer.GetString("ControlUsageModeIdRequired");

        public string ControlEquipmentRequired => _localizer.GetString("ControlEquipmentRequired");

        public string ControlStructureRequired => _localizer.GetString("ControlStructureRequired");

        public string ControlSprayerRequired => _localizer.GetString("ControlSprayerRequired");

        public string DuplicatedFault => _localizer.GetString("DuplicatedFault");

        public string DuplicatedSprayer => _localizer.GetString("DuplicatedSprayer");

        public string DuplicatedEquipment => _localizer.GetString("DuplicatedEquipment");

        public string ControlFaultTypeRequired => _localizer.GetString("ControlFaultTypeRequired");

        public string ControlFaultRequired => _localizer.GetString("ControlFaultRequired");

        public string ControlImpossibleCauseRequired => _localizer.GetString("ControlImpossibleCauseRequired");

        public string DependentCertification => _localizer.GetString("DependentCertification");

        public string CertificationCodeRequired => _localizer.GetString("CertificationCodeRequired");

        public string CertificationNameRequired => _localizer.GetString("CertificationNameRequired");

        public string CertificationScopesRequired => _localizer.GetString("CertificationScopesRequired");

        public string CertificationUniqueCode => _localizer.GetString("CertificationUniqueCode");

        public string CertificationReadOnlyCode => _localizer.GetString("CertificationReadOnlyCode");

        public string EquipmentCategoryRequired => _localizer.GetString("EquipmentCategoryRequired");

        public string EquipmentConcurencyStampException => _localizer.GetString("EquipmentConcurencyStampException");

        public string EquipmentNumberRequired => _localizer.GetString("EquipmentNumberRequired");

        public string EquipmentSprayerRequired => _localizer.GetString("EquipmentSprayerRequired");

        public string EquipmentTypeRequired => _localizer.GetString("EquipmentTypeRequired");

        public string EquipmentWorkingWidthRequired => _localizer.GetString("EquipmentWorkingWidthRequired");

        public string EquipmentProhibtedDeletion => _localizer.GetString("EquipmentProhibtedDeletion");

        public string ExpiredDelayToUpdateControl => _localizer.GetString("ExpiredDelayToUpdateControl");

        public string ExpiryDateRequired => _localizer.GetString("ExpiryDateRequired");

        public string FirstNameRequired => _localizer.GetString("FirstNameRequired");

        public string LastNameRequired => _localizer.GetString("LastNameRequired");

        public string LoginIncorrectCredentials => _localizer.GetString("LoginIncorrectCredentials");

        public string LoginPasswordLength => _localizer.GetString("LoginPasswordLength");

        public string LoginPasswordRequired => _localizer.GetString("LoginPasswordRequired");

        public string LoginUsernameRequired => _localizer.GetString("LoginUsernameRequired");

        public string InvalidCertification => _localizer.GetString("InvalidCertification");

        public string InvalidInspector => _localizer.GetString("InvalidInspector");

        public string InvalidStructure => _localizer.GetString("InvalidStructure");

        public string NotAllowed => _localizer.GetString("NotAllowed");

        public string NumberIncorrectLength => _localizer.GetString("NumberIncorrectLength");

        public string UsedAgreement => _localizer.GetString("UsedAgreement");

        public string PageNotFound => _localizer.GetString("PageNotFound");

        public string PageTitleRequired => _localizer.GetString("PageTitleRequired");

        public string PageContentRequired => _localizer.GetString("PageContentRequired");

        public string PageDuplicate => _localizer.GetString("PageDuplicate");

        public string PasswordPasswordConfirmDontMatch => _localizer.GetString("PasswordPasswordConfirmDontMatch");

        public string ResetPasswordEmailSubject => _localizer.GetString("ResetPasswordEmailSubject");

        public string ResetPasswordIdRequired => _localizer.GetString("ResetPasswordIdRequired");

        public string ResetPasswordTokenRequired => _localizer.GetString("ResetPasswordTokenRequired");

        public string ResetPasswordPasswordRequired => _localizer.GetString("ResetPasswordPasswordRequired");

        public string ResetPasswordPasswordLength => _localizer.GetString("ResetPasswordPasswordLength");

        public string ResetPasswordPasswordConfirmMatch => _localizer.GetString("ResetPasswordPasswordConfirmMatch");

        public string SearchInspectorBirthDateRequired => _localizer.GetString("SearchInspectorBirthDateRequired");

        public string SearchInspectorFisrtNameRequired => _localizer.GetString("SearchInspectorFisrtNameRequired");

        public string SearchInspectorLastNameRequired => _localizer.GetString("SearchInspectorLastNameRequired");

        public string SprayerBrandRequired => _localizer.GetString("SprayerBrandRequired");

        public string SprayerCategoryRequired => _localizer.GetString("SprayerCategoryRequired");

        public string SprayerConcurrencyStampException => _localizer.GetString("SprayerConcurrencyStampException");

        public string SprayerConstructionYearRequired => _localizer.GetString("SprayerConstructionYearRequired");

        public string SprayerHitchRequired => _localizer.GetString("SprayerHitchRequired");

        public string SprayerModelRequired => _localizer.GetString("SprayerModelRequired");

        public string SprayerNumberRequired => _localizer.GetString("SprayerNumberRequired");

        public string SprayerOperationRequired => _localizer.GetString("SprayerOperationRequired");

        public string SprayerPreviousException => _localizer.GetString("SprayerPreviousException");

        public string SprayerRegulationRequired => _localizer.GetString("SprayerRegulationRequired");

        public string SprayerTypeRequired => _localizer.GetString("SprayerTypeRequired");

        public string SprayerVolumeRequired => _localizer.GetString("SprayerVolumeRequired");

        public string SprayerWorkingWidthRequired => _localizer.GetString("SprayerWorkingWidthRequired");

        public string UniqueEquipmentNumberException => _localizer.GetString("UniqueEquipmentNumberException");

        public string SprayerProhibtedDeletion => _localizer.GetString("SprayerProhibtedDeletion");

        public string StructureNameRequired => _localizer.GetString("StructureNameRequired");

        public string StructureAddressRequired => _localizer.GetString("StructureAddressRequired");

        public string StructureTypeRequired => _localizer.GetString("StructureTypeRequired");

        public string StructureCityRequired => _localizer.GetString("StructureCityRequired");

        public string StructureZipCodeRequired => _localizer.GetString("StructureZipCodeRequired");

        public string StructurePhoneNumberRequired => _localizer.GetString("StructurePhoneNumberRequired");

        public string StructureRequired => _localizer.GetString("StructureRequired");

        public string DependantStructure => _localizer.GetString("DependantStructure");

        public string UniqueKeyException => _localizer.GetString("UniqueKeyException");

        public string UniqueSprayerNumberException => _localizer.GetString("UniqueSprayerNumber");

        public string UserDisabled => _localizer.GetString("UserDisabled");

        public string UserLocked => _localizer.GetString("UserLocked");

        public string TrainingCenterRequired => _localizer.GetString("TrainingCenterRequired");

        public string UserStructureLoginRequired => _localizer.GetString("UserStructureLoginRequired");

        public string UserStructureRolesRequired => _localizer.GetString("UserStructureRolesRequired");

        public string UserStructureStartDateGreaterThanEndDate =>
            _localizer.GetString("UserStructureStartDateGreaterThanEndDate");

        public string UserStructureStartDateRequired => _localizer.GetString("UserStructureStartDateRequired");

        public string UserStructureStructureIdRequired => _localizer.GetString("UserStructureIdRequired");

        public string UserStructureUserIdRequired => _localizer.GetString("UserStructureUserIdRequired");


        public string UserNotFound => _localizer.GetString("UserNotFound");

        public string UserWithoutEmail => _localizer.GetString("UserWithoutEmail");

        public string ExistentEmail => _localizer.GetString("ExistentEmail");

        public string OldPasswordIncorrect => _localizer.GetString("OldPasswordIncorrect");

        public string UserStructureNotFound => _localizer.GetString("UserStructureNotFound");

        public string HaveControls => _localizer.GetString("HaveControls");
    }
}

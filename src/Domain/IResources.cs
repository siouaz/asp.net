namespace OeuilDeSauron.Domain
{
    /// <summary>
    /// Shared resource messages.
    /// </summary>
    public interface IResources
    {
        string AgreementNumberExistent { get; }

        string AddedUserEmailSubject { get; }

        string AgreementItemAgreementRequired { get; }

        string AgreementItemTypeRequired { get; }

        string AgreementItemStartDateRequired { get; }

        string AgreementNumberRequired { get; }

        string AgreementAdmissibilityDateRequired { get; }

        string AgreementInactiveRequired { get; }

        string AgreementRequestDateRequired { get; }

        string AgreementStructureRequired { get; }

        string AgreementValidityDateRequired { get; }

        string AssignedUser { get; }

        string DeniedListEdit { get; }

        string DependentItem { get; }

        string ControlAuthor { get; }

        string ControlConcurrencyStampException { get; }

        string ControlDateRequired { get; }

        string ControlReportSubmissionDateRequired { get; }

        string ControlReportSubmissionDateGreater { get; }

        string ControlActivityRequired { get; }

        string ControlConclusionRequired { get; }

        string ControlNozzleRequired { get; }

        string ControlNozzleOperationRequired { get; }

        string ControlPreControlRequired { get; }

        string ControlTypeRequired { get; }

        string ControlDepartmentRequired { get; }

        string ControlCityRequired { get; }

        string ControlFlowRateRequired { get; }

        string ControlUsageModeIdRequired { get; }

        string ControlEquipmentRequired { get; }

        string ControlStructureRequired { get; }

        string ControlSprayerRequired { get; }

        string DuplicatedFault { get; }

        string DuplicatedSprayer { get; }

        string DuplicatedEquipment { get; }

        string ControlFaultTypeRequired { get; }

        string ControlFaultRequired { get; }

        string ControlImpossibleCauseRequired { get; }

        string DependentCertification { get; }

        string CertificationCodeRequired { get; }

        string CertificationNameRequired { get; }

        string CertificationScopesRequired { get; }

        string CertificationUniqueCode { get; }

        string CertificationReadOnlyCode { get; }

        string EquipmentCategoryRequired { get; }

        string EquipmentConcurencyStampException { get; }

        string EquipmentNumberRequired { get; }

        string EquipmentSprayerRequired { get; }

        string EquipmentTypeRequired { get; }

        string EquipmentWorkingWidthRequired { get; }

        string EquipmentProhibtedDeletion { get; }

        string ExpiredDelayToUpdateControl { get; }

        string LoginIncorrectCredentials { get; }

        string LoginPasswordLength { get; }

        string LoginPasswordRequired { get; }

        string LoginUsernameRequired { get; }

        string NotAllowed { get; }

        string NumberIncorrectLength { get; }

        string UsedAgreement { get; }

        string InvalidCertification { get; }

        string InvalidInspector { get; }

        string InvalidStructure { get; }

        public string PageContentRequired { get; }

        public string PageDuplicate { get; }

        public string PageNotFound { get; }

        public string PageTitleRequired { get; }

        string ResetPasswordEmailSubject { get; }

        string ResetPasswordIdRequired { get; }

        string ResetPasswordTokenRequired { get; }

        string ResetPasswordPasswordRequired { get; }

        string SearchInspectorBirthDateRequired { get; }

        string SearchInspectorFisrtNameRequired { get; }

        string SearchInspectorLastNameRequired { get; }

        string SprayerBrandRequired { get; }

        string SprayerCategoryRequired { get; }

        string SprayerConcurrencyStampException { get; }

        string SprayerConstructionYearRequired { get; }

        string SprayerHitchRequired { get; }

        string SprayerModelRequired { get; }

        string SprayerNumberRequired { get; }

        string SprayerOperationRequired { get; }

        string SprayerPreviousException { get; }

        string SprayerRegulationRequired { get; }

        string SprayerTypeRequired { get; }

        string SprayerVolumeRequired { get; }

        string SprayerWorkingWidthRequired { get; }

        string SprayerProhibtedDeletion { get; }

        string StructureNameRequired { get; }

        string StructureAddressRequired { get; }

        string StructureTypeRequired { get; }

        string StructureCityRequired { get; }

        string StructureZipCodeRequired { get; }

        string StructurePhoneNumberRequired { get; }

        string DependantStructure { get; }

        string UniqueEquipmentNumberException { get; }

        string UniqueKeyException { get; }

        string UniqueSprayerNumberException { get; }

        string UserDisabled { get; }

        string UserLocked { get; }

        string UserStructureLoginRequired { get; }

        string UserStructureStructureIdRequired { get; }

        string UserStructureUserIdRequired { get; }

        string UserStructureStartDateRequired { get; }

        string UserNotFound { get; }

        string UserWithoutEmail { get; }

        string ExistentEmail { get; }

        string OldPasswordIncorrect { get; }

        string UserStructureNotFound { get; }

        string HaveControls { get; }
    }
}

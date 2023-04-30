namespace DVG.AP.Cms.CarInfo.Application.Contracts.Constant
{
    public static class ValidationMessage
    {

        public const string MustIsLongType = "'{PropertyName}' is not a Long Data Type";
        
        public const string Required = "{PropertyName}: is required.";
        public const string MobileInValid = "{PropertyName}: is invalid.";
        public const string EmailWrongFormat = "{PropertyName}: is invalid.";
        public const string Maxlength = "{PropertyName}: must not exceed {MaxLength} characters.";
        public const string GreaterThan = "{PropertyName}: must greater than {PropertyValue}.";
        public const string LessThan = "{PropertyName}: must less than {PropertyValue}.";
        public const string GreaterThanOrEqual = "{PropertyName}: must greater than or equal {PropertyValue}.";
        public const string LessThanOrEqual = "{PropertyName}: must less than or equal {PropertyValue}.";
        public const string Minlength = "{PropertyName}: must not exceed {MinLength} characters.";
        public const string DirectoryNotEmpty = "Directory not empty, you can't delete it.";
        public const string IsNotValid = "{PropertyName}: is invalid";
        public const string AtleastMinItems = " must be at least {MinItems} items.";

     

        public const string Duplicate = "Dublicate items";
     
    }


}
#pragma warning disable 1591
namespace PFRandomDataProcessor
{

    public enum enNameType
    {
        NotSpecified,
        Business,
        Person
    }

    public enum enCountry
    {
        NotSpecified,
        Canada,
        Mexico,
        UnitedStates
    }

    public enum enGender
    {
        NotSpecified,
        Male,
        Female
    }

    public enum enNameLanguage
    {
        NotSpecified = 0,
        US = 1,
        English = 2,
        French = 3,
        Spanish = 4
    }

    public enum enStreetNameLanguage
    {
        NotSpecified = 0,
        US = 1,
        English = 2,
        French = 3,
        Spanish = 4,
        PuertoRico = 5
    }

    public enum enCountryRandomDataSpecification
    {
        NotSpecified,
        Country,
        Name,
        Gender,
        AgeGroup,
        AddressLine,
        Region,
        SubRegion,
        StateProvince,
        Msa_AreaCode,
        Cmsa_LargeCity,
    }

    public enum enRandomDataType
    {
        NotSpecified = 0,
        RandomNamesAndLocations = 1,    //person and/or business names plus personal and address data are generated dynamically at runtime
        RandomNumbers = 2,              //numbers are generated dynamically at runtime
        RandomWords = 3,                //words, sentences, paragraphs and documents are generated dynamically at runtime
        RandomDatesAndTimes = 4,        //dates are generated dynamically at runtime
        RandomBooleans = 5,             //True/False boolean values are generated dynamically at runtime
        CustomRandomValues = 6,         //contains a list of random values only (e.g. a randomized list of product names)
        RandomStrings = 7,              //Strings of random characters are generated dynamically at runtime
        RandomBytes = 8,                //bytes and chars and byte arrays and char arrays are generated dynamically at runtime
        RandomDataElements = 9,         //random values for inidividual data elements (names, national ids, etc.)
    }

    public enum enRandomNameField
    {
        NotSpecified = 0,
        NameType = 1,
        Country = 2,
        FirstName = 3,
        MiddleInitial = 4,
        LastName = 5,
        Gender = 6,
        BirthDate = 7,
        AddressLine1 = 8,
        AddressLine2 = 9,
        City = 10,
        Neighborhood = 11,
        StateProvince = 12,
        ZipPostalCode = 13,
        StateProvinceName = 14,
        RegionName = 15,
        SubRegionName = 16,
        AreaCode = 17,
        TelephoneNumber = 18,
        EmailAddress = 19,
        NationalId = 20,
        CountryCode = 21
    }


}//end namespace
#pragma warning restore 1591

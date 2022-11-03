using System.ComponentModel;

namespace Domain.Entities.GeneralModule
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("FeMale")]
        FeMale = 2,
        [Description("Prefer Not To Answer")]
        PreferNotToAnswer = 3
    }
    public enum Ethnicity
    {
        [Description("American Indian")]
        AmericanIndian = 1,
        [Description("Asian")]
        Asian = 2,
        [Description("Black Or African American")]
        BlackOrAfricanAmerican = 3,
        [Description("Hispanic Or Latino")]
        HispanicOrLatino = 4,
        [Description("White")]
        White = 5,
    }
    public enum CommunicationPreference
    {
        [Description("Email")]
        Email = 1,
        [Description("Text")]
        Text = 2,
        [Description("Both")]
        Both = 3,
    }
    public enum BillAfter
    {
        [Description("Day(s)")]
        Day = 1,
        [Description("Week(s)")]
        Week = 2,
        [Description("Month(s)")]
        Month = 3,
        [Description("Year(s)")]
        Year = 4,
    }
    public enum LevelOfEdujcation
    {
        [Description("Matric/O-Levels")]
        Matric = 1,
        [Description("Intermediate/A-Levels")]
        Intermediate = 2,
        [Description("Diploma")]
        Diploma = 2,
        [Description("Bachelors")]
        Bachelors = 3,
        [Description("Masters")]
        Masters = 4,
        [Description("Dr. of Philosopy")]
        Philosophy = 5,
    }
    public enum WorkPlaceType
    {
        [Description("Remote")]
        Remote = 1,
        [Description("Hybrid")]
        Hybrid = 2,
        [Description("On-Site")]
        OnSite = 3
    }
    public enum JobType
    {
        [Description("Full-time")]
        FullTime = 1,
        [Description("Part-time")]
        PartTime = 2,
        [Description("Contract")]
        Contract = 3,
        [Description("Temporary")]
        Temporary = 4,
        [Description("Other")]
        Other = 5,
        [Description("Voluteer")]
        Volunteer = 6,
        [Description("Internship")]
        Internship = 7,
    }
}

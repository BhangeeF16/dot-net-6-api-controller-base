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
    public enum EventType
    {
        [Description("None")]
        None = 1,
        [Description("Reservation")]
        Reservation = 2,
        [Description("Vacation")]
        Vacation = 3,
        [Description("DayOff")]
        DayOff = 4,
        [Description("Booked")]
        Booked = 5,
    }

}

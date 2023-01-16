using System.Globalization;

namespace Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static int GenerateOrderNumber()
        {
            var init = DateTime.UtcNow.Ticks.ToString();
            var orderNumber = Convert.ToInt32(init.Remove(init.Length - 10)) + new Random().Next();
            return orderNumber < 0 ? orderNumber * -1 : orderNumber;
        }
        public static List<string> ToStringList(this string thisString, char seperationChar = ',')
        {
            if (string.IsNullOrEmpty(thisString))
            {
                return new List<string>();
            }
            return thisString.Split(seperationChar).Distinct().ToList();
        }
        public static string ToCommaSeperatedString(this List<string> theseStrings)
        {
            return string.Join(", ", theseStrings);
        }
        public static string ToCommaSeperatedString(this List<int> theseIntegers)
        {
            return string.Join(", ", theseIntegers);
        }
        public static List<int> ToIntList(this string thisString, char seperationChar = ',')
        {
            if (string.IsNullOrEmpty(thisString))
            {
                return new List<int>();
            }
            var intList = new List<int>();
            foreach (var item in thisString.Split(seperationChar).Distinct().ToList())
            {
                intList.Add(Convert.ToInt32(item));
            }
            return intList;
        }
        public static DateTime ToDateTime(this string HexaDecimalTimeStamp)
        {
            var timestamp = HexaDecimalTimeStamp.ToInt();
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(timestamp).ToLocalTime();
        }
        public static TimeSpan ToTimeSpan(this string HexaDecimalTimeStamp)
        {
            var timestamp = HexaDecimalTimeStamp.ToInt();
            return new TimeSpan(timestamp);
        }
        public static int ToInt(this string HexaDecimalTimeStamp)
        {
            return int.Parse(HexaDecimalTimeStamp, NumberStyles.HexNumber);
        }
        public static string ToLowerCase(this string QuestionItemTitle)
        {
            if (!string.IsNullOrEmpty(QuestionItemTitle))
            {
                QuestionItemTitle = QuestionItemTitle.Replace(" ", "");
                if (int.TryParse(QuestionItemTitle, out int intNumber))
                {
                    return intNumber.ToString();
                }
                else if (decimal.TryParse(QuestionItemTitle, out decimal decimalNumber))
                {
                    return decimalNumber.ToString();
                }
                else if (double.TryParse(QuestionItemTitle, out double doubleNumber))
                {
                    return doubleNumber.ToString();
                }
                else
                {
                    return QuestionItemTitle.ToLower();
                }
            }
            return string.Empty;
        }
    }
}


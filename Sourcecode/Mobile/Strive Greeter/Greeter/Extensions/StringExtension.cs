using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Greeter.Extensions
{
    public static class StringExtension
    {
        // It will consider White space as empty
        public static bool IsEmpty(this string txt) => string.IsNullOrWhiteSpace(txt);

        public static bool IsEmail(this string txt) =>
              Regex.IsMatch(txt, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);


        public static T ParseJsonString<T>(this string jsonString) => JsonConvert.DeserializeObject<T>(jsonString,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
    }
}

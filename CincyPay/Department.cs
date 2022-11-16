
namespace DepartmentData
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Department
    {
        [JsonProperty("department_codes")]
        public string? DepartmentCodes { get; set; }

        [JsonProperty("department")]
        public string? DepartmentDepartment { get; set; }

        [JsonProperty("director", NullValueHandling = NullValueHandling.Ignore)]
        public string? Director { get; set; }

        [JsonProperty("department_main_website")]
        public Uri? DepartmentMainWebsite { get; set; }

        [JsonProperty("department_contact_information")]
        public Uri? DepartmentContactInformation { get; set; }

        [JsonProperty("performance_goals_mission", NullValueHandling = NullValueHandling.Ignore)]
        public string? PerformanceGoalsMission { get; set; }
    }

    public partial class Department
    {
        public static List<Department> FromJson(string json) => JsonConvert.DeserializeObject<List<Department>>(json, DepartmentData.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Department> self) => JsonConvert.SerializeObject(self, DepartmentData.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

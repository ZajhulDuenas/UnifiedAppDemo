using Newtonsoft.Json;

namespace common
{
    public class IgnoreEmptyStringsConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
                                        JsonSerializer serializer)
        {
            if (reader == null)
                return default;

            var theValue = reader.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(theValue))
            {
                return theValue.Trim();
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (writer == null || value == null)
                return;

            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                writer.WriteValue(value.ToString()?.Trim());

                return;
            }

            writer.WriteNull();
        }

        #endregion Methods
    }
}

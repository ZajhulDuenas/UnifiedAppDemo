using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace common
{
    public static class StringExtensions
    {
        public static StringContent SerializeJsonContent<T>(this T model, NullValueHandling nullValue = NullValueHandling.Include)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = nullValue
            };

            return new StringContent(
                JsonConvert.SerializeObject(model, settings),
                Encoding.UTF8,
                "application/json"
            );
        }

        public static string SerializeJson<T>(this T model, bool indented = false, IList<JsonConverter>? jsonConverters = null)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Newtonsoft.Json.Formatting.None,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Include,
                Converters = [new IgnoreEmptyStringsConverter()]
            };

            if (indented)
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            if (jsonConverters != null && jsonConverters.Any())
            {
                foreach (var converter in jsonConverters)
                {
                    serializerSettings.Converters.Add(converter);
                }
            }

            return JsonConvert.SerializeObject(model, serializerSettings);
        }

        public static T? DeserializeJson<T>(this string json, string? dateFormatString = null, IList<JsonConverter>? jsonConverters = null)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                Converters = [new IgnoreEmptyStringsConverter()]
            };

            if (jsonConverters != null && jsonConverters.Any())
            {
                foreach (var converter in jsonConverters)
                {
                    serializerSettings.Converters.Add(converter);
                }
            }
            if (!string.IsNullOrEmpty(dateFormatString))
            {
                serializerSettings.DateFormatString = dateFormatString;
                serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            }

            return JsonConvert.DeserializeObject<T>(json, serializerSettings);
        }

        public static bool IsJwtExpired(this string token)
        {
            try
            {
                // Decodificar el JWT sin verificar la firma
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                    throw new ArgumentException("El token no es válido.");

                // Obtener la fecha de expiración (exp) del payload
                var exp = jsonToken.Payload.Exp;

                // Si no existe la propiedad 'exp', el token no tiene expiración
                if (!exp.HasValue)
                    return false;

                // Comprobar si la fecha de expiración ha pasado
                var expiryDate = DateTimeOffset.FromUnixTimeSeconds(exp.Value);
                var currentDate = DateTimeOffset.UtcNow;

                return expiryDate <= currentDate;
            }
            catch (Exception ex)
            {
                // Manejo de errores, por ejemplo si el JWT no es válido
                Console.WriteLine($"Error al validar el token: {ex.Message}");
                return true; // Si el token es inválido, lo consideramos "expirado"
            }
        }
    }
}

using Microsoft.JSInterop;
using System.Text.Json;

namespace WepApp.Core.Services
{

    public class VarsService
    {
        private readonly IJSRuntime _js;

        public VarsService()
        {

        }

        public VarsService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<bool> Exist(string tag)
        {
            var value = await _js.InvokeAsync<string>("localStorage.getItem", tag);
            return !string.IsNullOrEmpty(value);
        }

        public async Task<T> ExtractObject<T>(string tag)
        {
            T data = default;

            if (await Exist(tag))
            {
                var info = await _js.InvokeAsync<string>("localStorage.getItem", tag);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                data = JsonSerializer.Deserialize<T>(info, options);

                return data;
            }
            return data;

        }

        public async Task<bool> SetObject<T>(string tag, T data)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(data);
                await _js.InvokeVoidAsync("localStorage.setItem", tag, jsonData);
                return true;
            }
            catch (Exception)
            {
                // Opcionalmente, puedes hacer un log del error aquí
                return false;
            }
        }

        public async Task<bool> RemoveObject(string tag)
        {
            try
            {
                await _js.InvokeVoidAsync("localStorage.removeItem", tag);

                return true;
            }
            catch (Exception)
            {
                // Opcionalmente, puedes hacer un log del error aquí
                return false;
            }
        }
    }

}

using Microsoft.Extensions.Localization;
using System.Runtime.CompilerServices;

namespace ClassLibrary.Mvc.Http
{
    public static class StatusCodeDefinitions
    {
        private static readonly List<KeyValuePair<int, string>> _StatusCodeList = [];

        private static void Load()
        {
            if (_StatusCodeList.Count == 0)
            {
                _StatusCodeList.Add(new KeyValuePair<int, string>(400, "Bad Request - The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications."));
                _StatusCodeList.Add(new KeyValuePair<int, string>(401, "Unauthorized - The request requires user authentication."));
                _StatusCodeList.Add(new KeyValuePair<int, string>(404, "Not Found - The server has not found anything matching the Request-URI. No indication is given of whether the condition is temporary or permanent."));
                _StatusCodeList.Add(new KeyValuePair<int, string>(500, "Internal Server Error - The server encountered an unexpected condition which prevented it from fulfilling the request."));
            }
        }

        public static KeyValuePair<int, string> GetCodeDefinition(int code)
        {
            Load();

            KeyValuePair<int, string> definition = _StatusCodeList.Find(x => x.Key == code);
            if (definition.Key != code || code == 0)
                definition = new KeyValuePair<int, string>(600, "Unknown status code");

            return definition;

        }

        public static string ToString(int code)
        {
            KeyValuePair<int, string> definition = GetCodeDefinition(code);
            return $"[{definition.Key}] {definition.Value}";
        }
    }
}
